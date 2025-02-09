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
            return new NewDeliveryViewModel(); 
        }
        #endregion

        #region Sort And Find
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "name supplier", "name product"};
        }
        public override void Sort()
        {
            if (SortField == "name supplier")
                List = new ObservableCollection<DeliveryDetailForAllView>(List.OrderBy(item => item.DeliverySupplierIDName));
            if (SortField == "name product")
                List = new ObservableCollection<DeliveryDetailForAllView>(List.OrderBy(item => item.ProductName));
        }
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "name supplier", "name product" };
        }
        public override void Find()
        {
            Load();
            if (FindField == "name supplier")
                List = new ObservableCollection<DeliveryDetailForAllView>(List.Where(item => item.DeliverySupplierIDName != null && item.DeliverySupplierIDName.StartsWith(FindTextBox)));
            if (FindField == "name product")
                List = new ObservableCollection<DeliveryDetailForAllView>(List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
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
