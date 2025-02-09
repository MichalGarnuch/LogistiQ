using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogistiQ.ViewModels.Returns
{
    public class AllReturnsViewModel : AllViewModel<ReturnForAllView>
    {
        #region Constructor

        public AllReturnsViewModel()
            : base()
        {
            base.DisplayName = "Returns";

        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewReturnViewModel(); 
        }
        #endregion

        #region Sort And Find
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "product name", "customer email" };
        }
        public override void Sort()
        {
            if (SortField == "product name")
                List = new ObservableCollection<ReturnForAllView>(List.OrderBy(item => item.ProductName));
            if (SortField == "customer email")
                List = new ObservableCollection<ReturnForAllView>(List.OrderBy(item => item.OrderCustomerIDEmail));
        }
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "product name", "customer email" };
        }
        public override void Find()
        {
            Load();
            if (FindField == "product name")
                List = new ObservableCollection<ReturnForAllView>(List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
            if (FindField == "customer email")
                List = new ObservableCollection<ReturnForAllView>(List.Where(item => item.OrderCustomerIDEmail != null && item.OrderCustomerIDEmail.StartsWith(FindTextBox)));
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<ReturnForAllView>
                (
                    from returns in logistiq_Entities.Returns
                    select new ReturnForAllView
                    {
                        ReturnID = returns.ReturnID,
                        OrderCustomerIDFirstName = returns.Orders.Customers.FirstName,
                        OrderCustomerIDLastName = returns.Orders.Customers.LastName,
                        OrderCustomerIDNIP = returns.Orders.Customers.NIP,
                        OrderCustomerIDAddress = returns.Orders.Customers.Address,
                        OrderCustomerIDPhone = returns.Orders.Customers.Phone,
                        OrderCustomerIDEmail = returns.Orders.Customers.Email,
                        ProductName = returns.Products.Name,
                        Quantity = returns.Quantity,
                        Reason = returns.Reason,
                        Date = returns.Date
                    }
                );
        }
        #endregion
    }
}
