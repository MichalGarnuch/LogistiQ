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
    public class DeliveryRecordViewModel : AllViewModel<DeliveryRecordForAllView>
    {
        private readonly DeliveryRecordB deliveryRecordB;
        private readonly DeliveryB deliveryB;

        #region Konstruktor
        public DeliveryRecordViewModel()
            : base()
        {
            base.DisplayName = "Delivery Records";

            deliveryRecordB = new DeliveryRecordB(logistiq_Entities);
            deliveryB = new DeliveryB(logistiq_Entities);

            WarehousesList = new ObservableCollection<KeyAndValue>(deliveryB.GetWarehouseKeyAndValueItems());
            SelectedWarehouseId = WarehousesList.Any() ? WarehousesList.First().Key : 0;

            RefreshCommand = new BaseCommand(RefreshData);
            ExportToCsvCommand = new BaseCommand(ExportToCsv);

            RefreshData();
        }
        #endregion

        #region Właściwości

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

        private void RefreshData()
        {
            if (SelectedWarehouseId != 0)
            {
                List = new ObservableCollection<DeliveryRecordForAllView>(
                    deliveryRecordB.GetDeliveriesByWarehouse(SelectedWarehouseId));

                TotalDeliveryValue = List.Sum(x => x.TotalPrice); 

                int totalQuantity = List.Sum(x => x.Quantity); 
                AverageProductPrice = totalQuantity > 0 ? List.Sum(x => x.UnitPrice * x.Quantity) / totalQuantity : 0; 
            }
        }


        #endregion

        #region Sortowanie i wyszukiwanie

        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "supplier", "delivery date", "product", "quantity" };
        }

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

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "supplier", "product" };
        }

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
        public ICommand RefreshCommand { get; }
        public ICommand ExportToCsvCommand { get; }
        #endregion

        #region Eksport CSV

        private void ExportToCsv()
        {
            if (List == null || !List.Any())
                return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Save Delivery Report",
                FileName = "DeliveryReport.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    writer.WriteLine("Delivery ID,Supplier,Date,Product,Quantity,Unit Price,Total Price");

                    foreach (var item in List)
                    {
                        writer.WriteLine($"{item.DeliveryID},{item.SupplierName},{item.DeliveryDate},{item.ProductName},{item.Quantity},{item.UnitPrice},{item.Quantity * item.UnitPrice}");
                    }
                }
            }
        }

        #endregion

        #region Metody pomocnicze

        public override void Load()
        {
            
        }

        public override WorkspaceViewModel CreateNewViewModel()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
