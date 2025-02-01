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
    public class WarehouseOverviewViewModel : AllViewModel<WarehouseOverviewForAllView>
    {
        private readonly WarehouseOverviewB warehouseOverviewB;
        private readonly WarehouseB warehouseB;

        #region Konstruktor
        public WarehouseOverviewViewModel()
            : base()
        {
            base.DisplayName = "Warehouse Overview";

            warehouseOverviewB = new WarehouseOverviewB(logistiq_Entities);
            warehouseB = new WarehouseB(logistiq_Entities);

            WarehousesList = new ObservableCollection<KeyAndValue>(warehouseB.GetWarehouseKeyAndValueItems());
            SelectedWarehouseId = WarehousesList.Any() ? WarehousesList.First().Key : 0;

            RefreshStockCommand = new BaseCommand(Load);
            ExportToCsvCommand = new BaseCommand(ExportToCsv);

            Load();
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
                    Load();
                }
            }
        }

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
        public ICommand RefreshStockCommand { get; set; }
        public ICommand ExportToCsvCommand { get; set; }
        #endregion

        #region Sortowanie i wyszukiwanie

        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "product", "warehouse", "quantity" };
        }

        public override void Sort()
        {
            if (SortField == "product")
                List = new ObservableCollection<WarehouseOverviewForAllView>(List.OrderBy(item => item.ProductName));
            if (SortField == "warehouse")
                List = new ObservableCollection<WarehouseOverviewForAllView>(List.OrderBy(item => item.WarehouseName));
            if (SortField == "quantity")
                List = new ObservableCollection<WarehouseOverviewForAllView>(List.OrderBy(item => item.Quantity));
        }

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "product", "warehouse" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "product")
                List = new ObservableCollection<WarehouseOverviewForAllView>(List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
            if (FindField == "warehouse")
                List = new ObservableCollection<WarehouseOverviewForAllView>(List.Where(item => item.WarehouseName != null && item.WarehouseName.StartsWith(FindTextBox)));
        }
        #endregion

        #region Metody pomocnicze
        public override void Load()
        {
            if (SelectedWarehouseId != 0)
            {
                List = new ObservableCollection<WarehouseOverviewForAllView>(
                    warehouseOverviewB.GetWarehouseStock(SelectedWarehouseId));

                TotalStockValue = List.Sum(x => x.TotalStockValue);
                OnPropertyChanged(() => TotalStockValue);

                int totalQuantity = List.Sum(x => x.Quantity);
                AverageStockPrice = totalQuantity > 0 ? List.Sum(x => x.UnitPrice * x.Quantity) / totalQuantity : 0;
                OnPropertyChanged(() => AverageStockPrice);

                LastDeliveryValue = List.Sum(x => x.LastDeliveryValue);
                OnPropertyChanged(() => LastDeliveryValue);

                AverageDeliveryPrice = List.Average(x => x.AverageDeliveryPrice);
                OnPropertyChanged(() => AverageDeliveryPrice); // ✅ To mogło być brakujące!
            }
        }



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
                    writer.WriteLine("Product,Warehouse,Quantity,Unit Price,Total Stock Value,Last Delivery Value,Average Delivery Price");

                    foreach (var item in List)
                    {
                        writer.WriteLine($"{item.ProductName},{item.WarehouseName},{item.Quantity},{item.UnitPrice},{item.TotalStockValue},{item.LastDeliveryValue},{item.AverageDeliveryPrice}");
                    }
                }
            }
        }
        #endregion

        #region **🔹 Brakująca Implementacja CreateNewViewModel()**
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return null; // 🔥 Nie tworzymy nowego widoku – WarehouseOverview to tylko podgląd
        }
        #endregion
    }
}
