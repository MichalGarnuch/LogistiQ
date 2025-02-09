using LogistiQ.Helper;
using LogistiQ.Models.BusinessLogic.SharedLogic;
using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using LogistiQ.Models.BusinessLogic;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;

namespace LogistiQ.ViewModels.BusinessLogicViewModel
{
    /// <summary>
    /// WarehouseOverviewViewModel odpowiada za prezentację przeglądu stanu magazynowego.
    /// Dziedziczy po AllViewModel, gdzie typ elementów to <see cref="WarehouseOverviewForAllView"/>.
    /// Umożliwia filtrowanie danych według wybranego magazynu oraz sortowanie i wyszukiwanie rekordów.
    /// Dodatkowo, widok wspiera eksport danych do pliku CSV.
    /// </summary>
    public class WarehouseOverviewViewModel : AllViewModel<WarehouseOverviewForAllView>
    {
        // Logika biznesowa odpowiedzialna za operacje na przeglądzie magazynu.
        private readonly WarehouseOverviewB warehouseOverviewB;
        // Logika biznesowa do pobierania listy magazynów (jako pary klucz-wartość).
        private readonly WarehouseB warehouseB;

        #region Konstruktor

        /// <summary>
        /// Konstruktor inicjalizuje widok przeglądu magazynu.
        /// Ustawia nazwę wyświetlaną, inicjalizuje logikę biznesową,
        /// pobiera listę magazynów, ustawia domyślnie wybrany magazyn,
        /// przypisuje komendy do odświeżania danych (RefreshStockCommand) oraz eksportu do CSV,
        /// a na końcu wywołuje metodę Load() do załadowania początkowych danych.
        /// </summary>
        public WarehouseOverviewViewModel()
            : base()
        {
            base.DisplayName = "Warehouse Overview";

            // Inicjalizacja logiki biznesowej przy użyciu wspólnego kontekstu bazy danych (logistiq_Entities)
            warehouseOverviewB = new WarehouseOverviewB(logistiq_Entities);
            warehouseB = new WarehouseB(logistiq_Entities);

            // Pobranie listy magazynów w postaci pary klucz-wartość
            WarehousesList = new ObservableCollection<KeyAndValue>(warehouseB.GetWarehouseKeyAndValueItems());
            // Ustawienie domyślnego wybranego magazynu: jeśli lista nie jest pusta, wybierany jest pierwszy element; w przeciwnym razie 0.
            SelectedWarehouseId = WarehousesList.Any() ? WarehousesList.First().Key : 0;

            // Inicjalizacja komend: RefreshStockCommand do odświeżania danych oraz ExportToCsvCommand do eksportu do CSV
            RefreshStockCommand = new BaseCommand(Load);
            ExportToCsvCommand = new BaseCommand(ExportToCsv);

            // Załadowanie początkowych danych
            Load();
        }
        #endregion

        #region Właściwości

        /// <summary>
        /// Kolekcja magazynów (jako pary klucz-wartość), wykorzystywana do filtrowania danych.
        /// </summary>
        private ObservableCollection<KeyAndValue> _warehousesList;
        public ObservableCollection<KeyAndValue> WarehousesList
        {
            get { return _warehousesList; }
            set
            {
                _warehousesList = value;
                OnPropertyChanged(() => WarehousesList);
            }
        }

        /// <summary>
        /// Identyfikator wybranego magazynu. Zmiana tej właściwości powoduje ponowne załadowanie danych.
        /// </summary>
        private int _selectedWarehouseId;
        public int SelectedWarehouseId
        {
            get { return _selectedWarehouseId; }
            set
            {
                if (_selectedWarehouseId != value)
                {
                    _selectedWarehouseId = value;
                    OnPropertyChanged(() => SelectedWarehouseId);
                    Load();
                }
            }
        }

        /// <summary>
        /// Łączna wartość zapasów w magazynie.
        /// </summary>
        private decimal _totalStockValue;
        public decimal TotalStockValue
        {
            get { return _totalStockValue; }
            set
            {
                _totalStockValue = value;
                OnPropertyChanged(() => TotalStockValue);
            }
        }

        /// <summary>
        /// Średnia cena zapasów, obliczana jako suma (UnitPrice * Quantity) podzielona przez łączną ilość.
        /// </summary>
        private decimal _averageStockPrice;
        public decimal AverageStockPrice
        {
            get { return _averageStockPrice; }
            set
            {
                _averageStockPrice = value;
                OnPropertyChanged(() => AverageStockPrice);
            }
        }

        /// <summary>
        /// Łączna wartość ostatniej dostawy.
        /// </summary>
        private decimal _lastDeliveryValue;
        public decimal LastDeliveryValue
        {
            get { return _lastDeliveryValue; }
            set
            {
                _lastDeliveryValue = value;
                OnPropertyChanged(() => LastDeliveryValue);
            }
        }

        /// <summary>
        /// Średnia cena dostawy obliczana na podstawie historycznych danych.
        /// </summary>
        private decimal _averageDeliveryPrice;
        public decimal AverageDeliveryPrice
        {
            get { return _averageDeliveryPrice; }
            set
            {
                _averageDeliveryPrice = value;
                OnPropertyChanged(() => AverageDeliveryPrice);
            }
        }

        #endregion

        #region Komendy

        /// <summary>
        /// Komenda odświeżająca dane stanu magazynowego.
        /// </summary>
        public ICommand RefreshStockCommand { get; set; }

        /// <summary>
        /// Komenda eksportu danych do pliku CSV.
        /// </summary>
        public ICommand ExportToCsvCommand { get; set; }

        #endregion

        #region Sortowanie i wyszukiwanie

        /// <summary>
        /// Zwraca listę kryteriów sortowania dostępnych w ComboBox.
        /// </summary>
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "product", "warehouse", "quantity" };
        }

        /// <summary>
        /// Sortuje listę danych według wybranego kryterium.
        /// Sortowanie odbywa się względem nazwy produktu, magazynu lub ilości.
        /// </summary>
        public override void Sort()
        {
            if (SortField == "product")
                List = new ObservableCollection<WarehouseOverviewForAllView>(List.OrderBy(item => item.ProductName));
            if (SortField == "warehouse")
                List = new ObservableCollection<WarehouseOverviewForAllView>(List.OrderBy(item => item.WarehouseName));
            if (SortField == "quantity")
                List = new ObservableCollection<WarehouseOverviewForAllView>(List.OrderBy(item => item.Quantity));
        }

        /// <summary>
        /// Zwraca listę kryteriów wyszukiwania dostępnych w ComboBox.
        /// </summary>
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "product", "warehouse" };
        }

        /// <summary>
        /// Filtruje listę danych na podstawie wybranego kryterium wyszukiwania oraz wpisanego tekstu.
        /// </summary>
        public override void Find()
        {
            // Najpierw załaduj pełny zbiór danych
            Load();
            if (FindField == "product")
                List = new ObservableCollection<WarehouseOverviewForAllView>(List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
            if (FindField == "warehouse")
                List = new ObservableCollection<WarehouseOverviewForAllView>(List.Where(item => item.WarehouseName != null && item.WarehouseName.StartsWith(FindTextBox)));
        }
        #endregion

        #region Metody pomocnicze

        /// <summary>
        /// Metoda Load pobiera dane stanu magazynowego dla wybranego magazynu.
        /// Aktualizuje właściwości widoku, takie jak TotalStockValue, AverageStockPrice, LastDeliveryValue oraz AverageDeliveryPrice.
        /// </summary>
        public override void Load()
        {
            if (SelectedWarehouseId != 0)
            {
                // Pobranie danych stanu magazynowego dla wybranego magazynu
                List = new ObservableCollection<WarehouseOverviewForAllView>(
                    warehouseOverviewB.GetWarehouseStock(SelectedWarehouseId));

                // Obliczenie łącznej wartości zapasów
                TotalStockValue = List.Sum(x => x.TotalStockValue);
                OnPropertyChanged(() => TotalStockValue);

                // Obliczenie średniej ceny zapasów
                int totalQuantity = List.Sum(x => x.Quantity);
                AverageStockPrice = totalQuantity > 0 ? List.Sum(x => x.UnitPrice * x.Quantity) / totalQuantity : 0;
                OnPropertyChanged(() => AverageStockPrice);

                // Obliczenie łącznej wartości ostatniej dostawy
                LastDeliveryValue = List.Sum(x => x.LastDeliveryValue);
                OnPropertyChanged(() => LastDeliveryValue);

                // Obliczenie średniej ceny dostawy
                AverageDeliveryPrice = List.Average(x => x.AverageDeliveryPrice);
                OnPropertyChanged(() => AverageDeliveryPrice);
            }
        }

        /// <summary>
        /// Metoda ExportToCsv eksportuje dane widoku do pliku CSV.
        /// Wyświetla okno dialogowe zapisu pliku i zapisuje dane, jeśli lista nie jest pusta.
        /// </summary>
        private void ExportToCsv()
        {
            if (List == null || !List.Any())
                return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Save Warehouse Overview Report",
                FileName = "WarehouseOverviewReport.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    // Zapisanie nagłówka CSV
                    writer.WriteLine("Product,Warehouse,Quantity,Unit Price,Total Stock Value,Last Delivery Value,Average Delivery Price");

                    // Zapisanie danych dla każdego rekordu
                    foreach (var item in List)
                    {
                        writer.WriteLine($"{item.ProductName},{item.WarehouseName},{item.Quantity},{item.UnitPrice},{item.TotalStockValue},{item.LastDeliveryValue},{item.AverageDeliveryPrice}");
                    }
                }
            }
        }
        #endregion

        #region **🔹 Brakująca Implementacja CreateNewViewModel()**
        /// <summary>
        /// Metoda CreateNewViewModel nie jest zaimplementowana w tym widoku.
        /// W tej implementacji zwraca null, co oznacza, że widok nie wspiera tworzenia nowych rekordów.
        /// </summary>
        /// <returns>Null</returns>
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return null;
        }
        #endregion
    }
}
