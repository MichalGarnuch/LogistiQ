using LogistiQ.Helper;
using LogistiQ.Models.BusinessLogic;
using LogistiQ.Models.BusinessLogic.SharedLogic;
using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using System.IO;
using Microsoft.Win32;

namespace LogistiQ.ViewModels.BusinessLogicViewModel
{
    /// <summary>
    /// DeliveryRecordViewModel odpowiada za prezentację danych dotyczących dostaw w widoku.
    /// Dziedziczy po AllViewModel z typem elementów DeliveryRecordForAllView, co umożliwia
    /// korzystanie z mechanizmu ładowania, sortowania i filtrowania danych.
    /// </summary>
    public class DeliveryRecordViewModel : AllViewModel<DeliveryRecordForAllView>
    {
        // Logika biznesowa odpowiedzialna za operacje na rekordach dostaw
        private readonly DeliveryRecordB deliveryRecordB;
        // Logika biznesowa odpowiedzialna za operacje na dostawach, m.in. pobieranie listy magazynów
        private readonly DeliveryB deliveryB;

        #region Konstruktor

        /// <summary>
        /// Konstruktor inicjalizuje widok dostaw.
        /// Ustawia nazwę wyświetlaną, inicjalizuje logikę biznesową, pobiera listę magazynów,
        /// ustawia wybrany magazyn oraz przypisuje komendy do odświeżania danych i eksportu do CSV.
        /// Na końcu wywołuje RefreshData() do załadowania początkowych danych.
        /// </summary>
        public DeliveryRecordViewModel()
            : base()
        {
            // Ustawienie nazwy widoku
            base.DisplayName = "Delivery Records";

            // Inicjalizacja obiektów logiki biznesowej
            deliveryRecordB = new DeliveryRecordB(logistiq_Entities);
            deliveryB = new DeliveryB(logistiq_Entities);

            // Pobranie listy magazynów (klucz-wartość) z logiki biznesowej dla dostaw
            WarehousesList = new ObservableCollection<KeyAndValue>(deliveryB.GetWarehouseKeyAndValueItems());
            // Ustawienie domyślnego wybranego magazynu (jeśli lista nie jest pusta)
            SelectedWarehouseId = WarehousesList.Any() ? WarehousesList.First().Key : 0;

            // Inicjalizacja komend: RefreshCommand do odświeżania danych i ExportToCsvCommand do eksportu do CSV
            RefreshCommand = new BaseCommand(RefreshData);
            ExportToCsvCommand = new BaseCommand(ExportToCsv);

            // Załadowanie danych przy tworzeniu widoku
            RefreshData();
        }
        #endregion

        #region Właściwości

        /// <summary>
        /// Kolekcja magazynów (klucz-wartość) dostępna w widoku.
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
        /// Identyfikator wybranego magazynu. Zmiana tej właściwości powoduje odświeżenie danych.
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
                    RefreshData();
                }
            }
        }

        /// <summary>
        /// Łączna wartość dostaw, obliczana jako suma TotalPrice dla wszystkich rekordów.
        /// </summary>
        private decimal _totalDeliveryValue;
        public decimal TotalDeliveryValue
        {
            get { return _totalDeliveryValue; }
            set
            {
                _totalDeliveryValue = value;
                OnPropertyChanged(() => TotalDeliveryValue);
            }
        }

        /// <summary>
        /// Średnia cena produktu, obliczana na podstawie sumy (UnitPrice * Quantity) i łącznej ilości.
        /// </summary>
        private decimal _averageProductPrice;
        public decimal AverageProductPrice
        {
            get { return _averageProductPrice; }
            set
            {
                _averageProductPrice = value;
                OnPropertyChanged(() => AverageProductPrice);
            }
        }

        #endregion

        #region Metody biznesowe

        /// <summary>
        /// RefreshData pobiera dane dostaw dla wybranego magazynu.
        /// Aktualizuje właściwość List, oblicza łączną wartość dostaw oraz średnią cenę produktu.
        /// </summary>
        private void RefreshData()
        {
            if (SelectedWarehouseId != 0)
            {
                // Pobranie rekordów dostaw dla wybranego magazynu
                List = new ObservableCollection<DeliveryRecordForAllView>(
                    deliveryRecordB.GetDeliveriesByWarehouse(SelectedWarehouseId));

                // Obliczenie łącznej wartości dostaw (suma TotalPrice dla wszystkich rekordów)
                TotalDeliveryValue = List.Sum(x => x.TotalPrice);

                // Obliczenie średniej ceny produktu
                int totalQuantity = List.Sum(x => x.Quantity);
                AverageProductPrice = totalQuantity > 0 ? List.Sum(x => x.UnitPrice * x.Quantity) / totalQuantity : 0;
            }
        }

        #endregion

        #region Sortowanie i wyszukiwanie

        /// <summary>
        /// Zwraca listę kryteriów sortowania do wyświetlenia w ComboBox.
        /// </summary>
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "supplier", "delivery date", "product", "quantity" };
        }

        /// <summary>
        /// Sortuje dane w liście na podstawie wybranego kryterium.
        /// W zależności od wartości SortField sortuje według SupplierName, DeliveryDate, ProductName lub Quantity.
        /// </summary>
        public override void Sort()
        {
            if (SortField == "supplier")
                List = new ObservableCollection<DeliveryRecordForAllView>(List.OrderBy(item => item.SupplierName));
            if (SortField == "delivery date")
                List = new ObservableCollection<DeliveryRecordForAllView>(List.OrderBy(item => item.DeliveryDate));
            if (SortField == "product")
                List = new ObservableCollection<DeliveryRecordForAllView>(List.OrderBy(item => item.ProductName));
            if (SortField == "quantity")
                List = new ObservableCollection<DeliveryRecordForAllView>(List.OrderBy(item => item.Quantity));
        }

        /// <summary>
        /// Zwraca listę kryteriów wyszukiwania do wyświetlenia w ComboBox.
        /// </summary>
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "supplier", "product" };
        }

        /// <summary>
        /// Filtruje listę rekordów na podstawie wpisanego tekstu.
        /// Najpierw odświeża dane, a następnie filtruje je według wybranego kryterium (FindField).
        /// </summary>
        public override void Find()
        {
            RefreshData();
            if (FindField == "supplier")
                List = new ObservableCollection<DeliveryRecordForAllView>(List.Where(item => item.SupplierName != null && item.SupplierName.StartsWith(FindTextBox)));
            if (FindField == "product")
                List = new ObservableCollection<DeliveryRecordForAllView>(List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
        }

        #endregion

        #region Komendy

        /// <summary>
        /// Komenda do odświeżania danych.
        /// </summary>
        public ICommand RefreshCommand { get; }

        /// <summary>
        /// Komenda do eksportu danych do formatu CSV.
        /// </summary>
        public ICommand ExportToCsvCommand { get; }

        #endregion

        #region Eksport CSV

        /// <summary>
        /// Eksportuje dane widoku dostaw do pliku CSV.
        /// Otwiera okno dialogowe do zapisu pliku i zapisuje dane w formacie CSV.
        /// </summary>
        private void ExportToCsv()
        {
            // Sprawdzenie, czy lista danych jest pusta
            if (List == null || !List.Any())
                return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Save Delivery Report",
                FileName = "DeliveryReport.csv"
            };

            // Jeśli użytkownik zatwierdzi wybór pliku, zapisujemy dane
            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    // Zapisanie nagłówka pliku CSV
                    writer.WriteLine("Delivery ID,Supplier,Date,Product,Quantity,Unit Price,Total Price");

                    // Zapisanie danych dla każdego rekordu
                    foreach (var item in List)
                    {
                        writer.WriteLine($"{item.DeliveryID},{item.SupplierName},{item.DeliveryDate},{item.ProductName},{item.Quantity},{item.UnitPrice},{item.Quantity * item.UnitPrice}");
                    }
                }
            }
        }

        #endregion

        #region Metody pomocnicze

        /// <summary>
        /// Metoda Load jest wymagana przez klasę bazową, jednak w tym widoku nie wykonuje dodatkowych operacji.
        /// </summary>
        public override void Load()
        {
            // Metoda nie zawiera implementacji, gdyż dane są ładowane przez RefreshData()
        }

        /// <summary>
        /// Metoda CreateNewViewModel jest abstrakcyjna i musi być zaimplementowana, jeśli widok ma wspierać tworzenie nowych rekordów.
        /// W tej implementacji rzuca wyjątek, ponieważ nie jest wspierane.
        /// </summary>
        /// <returns>Nowy obiekt WorkspaceViewModel.</returns>
        public override WorkspaceViewModel CreateNewViewModel()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
