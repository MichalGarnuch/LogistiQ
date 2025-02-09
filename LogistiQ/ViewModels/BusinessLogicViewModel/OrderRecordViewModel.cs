using LogistiQ.Helper;
using LogistiQ.Models.BusinessLogic.SharedLogic;
using LogistiQ.Models.BusinessLogic;
using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using System;
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
    /// OrderRecordViewModel odpowiada za prezentację danych dotyczących zamówień.
    /// Dziedziczy po AllViewModel, gdzie typ elementów to <see cref="OrderRecordForAllView"/>.
    /// Implementuje funkcje ładowania danych, sortowania, wyszukiwania oraz eksportu danych do formatu CSV.
    /// </summary>
    public class OrderRecordViewModel : AllViewModel<OrderRecordForAllView>
    {
        // Logika biznesowa dotycząca operacji na rekordach zamówień (np. pobieranie danych z bazy)
        private readonly OrderRecordB orderRecordB;
        // Logika biznesowa dotycząca operacji na zamówieniach (m.in. pobieranie danych klienta)
        private readonly OrderB orderB;

        #region Konstruktor

        /// <summary>
        /// Konstruktor inicjalizuje widok zamówień.
        /// Ustawia nazwę wyświetlaną, inicjalizuje logikę biznesową, pobiera listę klientów, 
        /// ustawia domyślnie wybranego klienta oraz przypisuje komendy odświeżania i eksportu do CSV.
        /// Na końcu wywołuje metodę Load() do załadowania początkowych danych.
        /// </summary>
        public OrderRecordViewModel()
            : base()
        {
            // Ustawienie tytułu widoku
            base.DisplayName = "Order Records";

            // Inicjalizacja logiki biznesowej, korzystającej z wspólnego kontekstu bazy (logistiq_Entities)
            orderRecordB = new OrderRecordB(logistiq_Entities);
            orderB = new OrderB(logistiq_Entities);

            // Pobranie listy klientów (jako pary klucz-wartość) z logiki biznesowej
            CustomersList = new ObservableCollection<KeyAndValue>(orderRecordB.GetCustomerKeyAndValueItems());
            // Ustawienie domyślnego wybranego klienta: jeśli lista nie jest pusta, wybierany jest pierwszy element, w przeciwnym razie 0.
            SelectedCustomerId = CustomersList.Any() ? CustomersList.First().Key : 0;

            // Inicjalne załadowanie danych
            Load();

            // Przypisanie komend do odświeżania danych oraz eksportu do CSV
            RefreshCommand = new BaseCommand(Load);
            ExportToCsvCommand = new BaseCommand(ExportToCsv);
        }
        #endregion

        #region Właściwości

        /// <summary>
        /// Kolekcja klientów w postaci pary klucz-wartość, wykorzystywana do filtrowania zamówień.
        /// </summary>
        private ObservableCollection<KeyAndValue> _customersList;
        public ObservableCollection<KeyAndValue> CustomersList
        {
            get { return _customersList; }
            set
            {
                _customersList = value;
                OnPropertyChanged(() => CustomersList);
            }
        }

        /// <summary>
        /// Wybrany identyfikator klienta. Zmiana tej właściwości powoduje ponowne załadowanie danych.
        /// </summary>
        private int _selectedCustomerId;
        public int SelectedCustomerId
        {
            get { return _selectedCustomerId; }
            set
            {
                if (_selectedCustomerId != value)
                {
                    _selectedCustomerId = value;
                    OnPropertyChanged(() => SelectedCustomerId);
                    // Za każdym razem, gdy zmienimy klienta, ponownie ładujemy dane.
                    Load();
                }
            }
        }

        /// <summary>
        /// Łączna wartość zamówień dla wybranego klienta.
        /// </summary>
        private decimal _totalOrderValue;
        public decimal TotalOrderValue
        {
            get { return _totalOrderValue; }
            set
            {
                _totalOrderValue = value;
                OnPropertyChanged(() => TotalOrderValue);
            }
        }

        /// <summary>
        /// Średnia wartość zamówienia, obliczana na podstawie sumy wartości zamówień i łącznej ilości.
        /// </summary>
        private decimal _averageOrderValue;
        public decimal AverageOrderValue
        {
            get { return _averageOrderValue; }
            set
            {
                _averageOrderValue = value;
                OnPropertyChanged(() => AverageOrderValue);
            }
        }

        #endregion

        #region Metody pomocnicze

        /// <summary>
        /// Metoda Load pobiera dane zamówień dla wybranego klienta i aktualizuje właściwości widoku.
        /// Ustawia kolekcję List na podstawie danych pobranych z logiki biznesowej.
        /// Dodatkowo oblicza łączną wartość oraz średnią wartość zamówienia.
        /// </summary>
        public override void Load()
        {
            if (SelectedCustomerId != 0)
            {
                // Pobranie listy zamówień dla wybranego klienta
                List = new ObservableCollection<OrderRecordForAllView>(
                    orderRecordB.GetOrdersByCustomer(SelectedCustomerId));

                // Obliczenie łącznej wartości zamówień
                TotalOrderValue = orderRecordB.GetTotalOrderValue(SelectedCustomerId);
                // Obliczenie średniej wartości zamówienia
                AverageOrderValue = orderRecordB.GetAverageOrderValue(SelectedCustomerId);
            }
        }

        /// <summary>
        /// Eksportuje dane zamówień do pliku CSV.
        /// Otwiera okno dialogowe do zapisu pliku i zapisuje dane, jeśli lista zamówień nie jest pusta.
        /// </summary>
        private void ExportToCsv()
        {
            if (List == null || !List.Any())
                return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Save Order Report",
                FileName = "OrderReport.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    // Zapis nagłówka CSV
                    writer.WriteLine("Order ID,Customer,Order Date,Status,Total Order Value,Payment Status,Product,Quantity");

                    // Zapis danych dla każdego rekordu
                    foreach (var item in List)
                    {
                        writer.WriteLine($"{item.OrderID},{item.CustomerName},{item.OrderDate},{item.Status},{item.TotalOrderValue},{item.PaymentStatus},{item.ProductName},{item.Quantity}");
                    }
                }
            }
        }

        #endregion

        #region Sortowanie i wyszukiwanie

        /// <summary>
        /// Zwraca listę kryteriów sortowania dostępnych dla widoku.
        /// </summary>
        /// <returns>Lista kryteriów sortowania jako string.</returns>
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "customer", "order date", "status", "product" };
        }

        /// <summary>
        /// Sortuje listę zamówień na podstawie wybranego kryterium (SortField).
        /// </summary>
        public override void Sort()
        {
            if (SortField == "customer")
                List = new ObservableCollection<OrderRecordForAllView>(List.OrderBy(item => item.CustomerName));
            if (SortField == "order date")
                List = new ObservableCollection<OrderRecordForAllView>(List.OrderBy(item => item.OrderDate));
            if (SortField == "status")
                List = new ObservableCollection<OrderRecordForAllView>(List.OrderBy(item => item.Status));
            if (SortField == "product")
                List = new ObservableCollection<OrderRecordForAllView>(List.OrderBy(item => item.ProductName));
        }

        /// <summary>
        /// Zwraca listę kryteriów wyszukiwania dostępnych w widoku.
        /// </summary>
        /// <returns>Lista kryteriów wyszukiwania jako string.</returns>
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "customer", "product" };
        }

        /// <summary>
        /// Filtruje listę zamówień na podstawie wybranego kryterium wyszukiwania (FindField)
        /// i tekstu wpisanego w polu wyszukiwania (FindTextBox).
        /// </summary>
        public override void Find()
        {
            // Najpierw odświeżamy dane, aby operować na pełnym zbiorze
            Load();
            if (FindField == "customer")
                List = new ObservableCollection<OrderRecordForAllView>(List.Where(item => item.CustomerName != null && item.CustomerName.StartsWith(FindTextBox)));
            if (FindField == "product")
                List = new ObservableCollection<OrderRecordForAllView>(List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
        }

        #endregion

        #region Komendy

        /// <summary>
        /// Komenda odświeżania danych.
        /// </summary>
        public ICommand RefreshCommand { get; set; }
        /// <summary>
        /// Komenda eksportu danych do pliku CSV.
        /// </summary>
        public ICommand ExportToCsvCommand { get; set; }

        #endregion

        #region Metody pomocnicze

        /// <summary>
        /// Metoda CreateNewViewModel nie jest zaimplementowana w tym widoku i rzuca wyjątek.
        /// Jeśli tworzenie nowych rekordów byłoby wspierane, należałoby tu zaimplementować logikę tworzenia nowego widoku.
        /// </summary>
        /// <returns>Nowy obiekt WorkspaceViewModel.</returns>
        public override WorkspaceViewModel CreateNewViewModel()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
