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
            Load();
            if (FindField == "supplier")
                List = new ObservableCollection<DeliveryRecordForAllView>(List.Where(item => item.SupplierName != null && item.SupplierName.StartsWith(FindTextBox)));
            if (FindField == "product")
                List = new ObservableCollection<DeliveryRecordForAllView>(List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
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
                List = new ObservableCollection<DeliveryRecordForAllView>(
                    deliveryRecordB.GetDeliveriesByWarehouse(SelectedWarehouseId));
            }
        }

        public override WorkspaceViewModel CreateNewViewModel()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
