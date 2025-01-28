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
            throw new System.NotImplementedException();
        }
        //tu decydujemy jak sortować
        public override void Sort()
        {
            throw new System.NotImplementedException();
        }
        //tu decydujemy po czym wyszukiwać
        public override List<string> GetComboboxFindList()
        {
            throw new System.NotImplementedException();
        }
        //tu decydujemy jak wyszukiwać
        public override void Find()
        {
            throw new System.NotImplementedException();
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