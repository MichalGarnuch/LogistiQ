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
    public class DeliveryDetailsViewModel : AllViewModel<DeliveryDetailForAllView>
    {
        #region Constructor

        public DeliveryDetailsViewModel()
            : base()
        {
            base.DisplayName = "DeliveryDetails";
            IsAddVisible = false;

        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewDeliveryViewModel(); // Zwracamy odpowiedni ViewModel
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<DeliveryDetailForAllView>
                (
                    from deliveryDetail in logistiq_Entities.DeliveryDetails
                    select new DeliveryDetailForAllView
                    {
                        DeliveryDetailID = deliveryDetail.DeliveryDetailID,
                        DeliverySupplierIDName = deliveryDetail.Deliveries.Suppliers.Name,
                        ProductName = deliveryDetail.Products.Name,
                        Quantity = deliveryDetail.Quantity,
                        UnitPrice = deliveryDetail.UnitPrice
                    }
                );
        }
        #endregion
    }
}
