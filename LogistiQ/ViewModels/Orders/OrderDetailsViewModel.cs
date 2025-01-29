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
            return new List<string> { "customer first name", "customer last name" };
        }
        //tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "customer first name")
                List = new ObservableCollection<OrderDetailForAllView>(List.OrderBy(item => item.OrderCustomerIDFirstName));
            if (SortField == "customer last name")
                List = new ObservableCollection<OrderDetailForAllView>(List.OrderBy(item => item.OrderCustomerIDLastName));
        }
        //tu decydujemy po czym wyszukiwać
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "customer first name", "customer last name" };
        }
        //tu decydujemy jak wyszukiwać
        public override void Find()
        {
            Load();
            if (FindField == "customer first name")
                List = new ObservableCollection<OrderDetailForAllView>(List.Where(item => item.OrderCustomerIDFirstName != null && item.OrderCustomerIDFirstName.StartsWith(FindTextBox)));
            if (FindField == "customer last name")
                List = new ObservableCollection<OrderDetailForAllView>(List.Where(item => item.OrderCustomerIDLastName != null && item.OrderCustomerIDLastName.StartsWith(FindTextBox)));
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
