using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogistiQ.ViewModels.Payments
{
    public class AllPaymentsViewModel : AllViewModel<PaymentForAllView>
    {
        #region Constructor

        public AllPaymentsViewModel()
            : base()
        {
            base.DisplayName = "Payments";
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
            List = new ObservableCollection<PaymentForAllView>
                (
                    from payments in logistiq_Entities.Payments
                    select new PaymentForAllView
                    {
                        PaymentID = payments.PaymentID,
                        OrderCustomerIDFirstName = payments.Orders.Customers.FirstName,
                        OrderCustomerIDLastName = payments.Orders.Customers.LastName,
                        OrderCustomerIDNIP = payments.Orders.Customers.NIP,
                        OrderCustomerIDAddress = payments.Orders.Customers.Address,
                        OrderCustomerIDPhone = payments.Orders.Customers.Phone,
                        OrderCustomerIDEmail = payments.Orders.Customers.Email,
                        PaymentMethod = payments.PaymentMethod,
                        Amount = payments.Amount,
                        PaymentDate = payments.PaymentDate,
                    }
                );
        }
        #endregion
    }
}