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
    public class AllOrdersViewModel : AllViewModel<OrderForAllView>
    {
        #region Constructor

        public AllOrdersViewModel()
            : base()
        {
            base.DisplayName = "Orders";

        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewOrderViewModel(); // Zwracamy odpowiedni ViewModel
        }
        #endregion

        #region Sort And Find
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "customer first name", "customer last name" };
        }
        public override void Sort()
        {
            if (SortField == "customer first name")
                List = new ObservableCollection<OrderForAllView>(List.OrderBy(item => item.CustomerFirstName));
            if (SortField == "customer last name")
                List = new ObservableCollection<OrderForAllView>(List.OrderBy(item => item.CustomerLastName));
        }
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "customer first name", "customer last name" };
        }
        public override void Find()
        {
            Load();
            if (FindField == "customer first name")
                List = new ObservableCollection<OrderForAllView>(List.Where(item => item.CustomerFirstName != null && item.CustomerFirstName.StartsWith(FindTextBox)));
            if (FindField == "customer last name")
                List = new ObservableCollection<OrderForAllView>(List.Where(item => item.CustomerLastName != null && item.CustomerLastName.StartsWith(FindTextBox)));
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<OrderForAllView>
                (
                    from orders in logistiq_Entities.Orders
                    select new OrderForAllView
                    {
                        OrderID = orders.OrderID,
                        CustomerFirstName = orders.Customers.FirstName,
                        CustomerLastName = orders.Customers.LastName,
                        CustomerNIP = orders.Customers.NIP,
                        OrderDate = orders.OrderDate,
                        Status = orders.Status,
                        Total = orders.Total
                    }
                );
        }
        #endregion
    }
}