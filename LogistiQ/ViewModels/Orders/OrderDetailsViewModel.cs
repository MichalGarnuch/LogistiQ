using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.ViewModels.Orders
{
    public class OrderDetailsViewModel : AllViewModel<OrderDetailForAllView>
    {
        #region Constructor

        public OrderDetailsViewModel()
            : base()
        {
            base.DisplayName = "OrderDetails";
            IsAddVisible = false;
        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return null;
        }
        public new void AddNew() { }
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
            List = new ObservableCollection<OrderDetailForAllView>
                (
                    from orderDetail in logistiq_Entities.OrderDetails
                    select new OrderDetailForAllView
                    {
                        OrderDetailID = orderDetail.OrderDetailID,
                        OrderCustomerIDFirstName = orderDetail.Orders.Customers.FirstName,
                        OrderCustomerIDLastName = orderDetail.Orders.Customers.LastName,
                        OrderCustomerIDNIP = orderDetail.Orders.Customers.NIP,
                        ProductName = orderDetail.Products.Name,
                        Quantity = orderDetail.Quantity,
                        UnitPrice = orderDetail.UnitPrice,
                        VAT = orderDetail.VAT
                    }
                );
        }
        #endregion
    }
}
