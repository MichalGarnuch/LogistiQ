using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogistiQ.ViewModels.Products;


namespace LogistiQ.ViewModels.Deliveries
{
    public class AllDeliveriesViewModel : AllViewModel<DeliveryForAllView>
    {
        #region Constructor

        public AllDeliveriesViewModel()
            : base()
        {
            base.DisplayName = "Deliveries";

        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewDeliveryViewModel(); 
        }
        #endregion

        #region Sort And Find
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "cost", "status" };
        }
        public override void Sort()
        {
            if (SortField == "cost")
                List = new ObservableCollection<DeliveryForAllView>(List.OrderBy(item => item.Cost));
            if (SortField == "status")
                List = new ObservableCollection<DeliveryForAllView>(List.OrderBy(item => item.Status));
        }
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "warehouses", "status" };
        }
        public override void Find()
        {
            Load();
            if (FindField == "warehouses")
                List = new ObservableCollection<DeliveryForAllView>(List.Where(item => item.WarehouseName != null && item.WarehouseName.StartsWith(FindTextBox)));
            if (FindField == "status")
                List = new ObservableCollection<DeliveryForAllView>(List.Where(item => item.Status != null && item.Status.StartsWith(FindTextBox)));
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<DeliveryForAllView>
                (
                    from deliveries in logistiq_Entities.Deliveries
                    select new DeliveryForAllView
                    {
                        DeliveryID = deliveries.DeliveryID,
                        SupplierName = deliveries.Suppliers.Name,
                        SupplierAddress = deliveries.Suppliers.Address,
                        DeliveryDate = deliveries.DeliveryDate,
                        Cost = deliveries.Cost,
                        Status = deliveries.Status,
                        WarehouseName = deliveries.Warehouses.Name,
                        WarehouseLocation = deliveries.Warehouses.Location
                    }
                );
        }
        #endregion
    }
}