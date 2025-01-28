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