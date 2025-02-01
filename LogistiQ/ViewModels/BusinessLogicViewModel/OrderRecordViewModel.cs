using LogistiQ.Helper;
using LogistiQ.Models.BusinessLogic.SharedLogic;
using LogistiQ.Models.BusinessLogic;
using LogistiQ.Models.Entities;
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
    public class OrderRecordViewModel : AllViewModel<OrderRecordForAllView>
    {
        private readonly OrderRecordB orderRecordB;
        private readonly OrderB orderB;

        #region Konstruktor
        public OrderRecordViewModel()
            : base()
        {
            base.DisplayName = "Order Records";

            orderRecordB = new OrderRecordB(logistiq_Entities);
            orderB = new OrderB(logistiq_Entities);

            CustomersList = new ObservableCollection<KeyAndValue>(orderRecordB.GetCustomerKeyAndValueItems());
            SelectedCustomerId = CustomersList.Any() ? CustomersList.First().Key : 0;

            Load();

            RefreshCommand = new BaseCommand(Load);
        }
        #endregion

        #region Właściwości

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
                    Load();
                }
            }
        }
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

        private void RefreshData()
        {
            List = new ObservableCollection<OrderRecordForAllView>(
                orderRecordB.GetOrdersByCustomer(SelectedCustomerId)); // ✅ Używamy istniejącej metody!

            TotalOrderValue = List.Sum(x => x.TotalOrderValue);
            AverageOrderValue = List.Count > 0 ? List.Sum(x => x.TotalOrderValue) / List.Count : 0;
        }


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
                    writer.WriteLine("Order ID,Customer,Order Date,Status,Total Order Value,Payment Status,Product,Quantity");

                    foreach (var item in List)
                    {
                        writer.WriteLine($"{item.OrderID},{item.CustomerName},{item.OrderDate},{item.Status},{item.TotalOrderValue},{item.PaymentStatus},{item.ProductName},{item.Quantity}");
                    }
                }
            }
        }


        #endregion

        #region Sortowanie i wyszukiwanie

        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "customer", "order date", "status", "product" };
        }

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

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "customer", "product" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "customer")
                List = new ObservableCollection<OrderRecordForAllView>(List.Where(item => item.CustomerName != null && item.CustomerName.StartsWith(FindTextBox)));
            if (FindField == "product")
                List = new ObservableCollection<OrderRecordForAllView>(List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
        }
        #endregion

        #region Komendy
        public ICommand RefreshCommand { get; set; }
        #endregion

        #region Metody pomocnicze
        public override void Load()
        {
            if (SelectedCustomerId != 0)
            {
                List = new ObservableCollection<OrderRecordForAllView>(
                    orderRecordB.GetOrdersByCustomer(SelectedCustomerId));
            }
        }

        public override WorkspaceViewModel CreateNewViewModel()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
