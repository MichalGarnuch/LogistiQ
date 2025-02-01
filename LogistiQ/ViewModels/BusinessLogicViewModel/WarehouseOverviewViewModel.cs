using LogistiQ.Helper;
using LogistiQ.Models.BusinessLogic.SharedLogic;
using LogistiQ.Models.BusinessLogic;
using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.ViewModels.BusinessLogicViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

            Load();

            RefreshCommand = new BaseCommand(Load);
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

        #region Komendy
        public ICommand RefreshCommand { get; set; }
        #endregion

        #region Metody pomocnicze
        public override void Load()
        {
            if (SelectedWarehouseId != 0)
            {
                List = new ObservableCollection<WarehouseOverviewForAllView>(
                    warehouseOverviewB.GetWarehouseStock(SelectedWarehouseId));
            }
        }

        public override WorkspaceViewModel CreateNewViewModel()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
