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
            return new NewDeliveryViewModel(); // Zwracamy odpowiedni ViewModel
        }
        #endregion

        #region Sort And Find
        //tu decydujemy po czym sortować do comboboxa
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "cost", "status" };
        }
        //tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "cost")
                List = new ObservableCollection<DeliveryForAllView>(List.OrderBy(item => item.Cost));
            if (SortField == "status")
                List = new ObservableCollection<DeliveryForAllView>(List.OrderBy(item => item.Status));
        }
        //tu decydujemy po czym wyszukiwać
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "warehouses", "status" };
        }
        //tu decydujemy jak wyszukiwać
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