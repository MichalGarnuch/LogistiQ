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