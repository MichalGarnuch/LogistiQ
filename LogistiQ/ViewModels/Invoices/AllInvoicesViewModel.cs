using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogistiQ.Models.Entities;
using LogistiQ.ViewModels.Products;


namespace LogistiQ.ViewModels.Invoices
{
    public class AllInvoicesViewModel : AllViewModel<InvoiceForAllView>
    {
        #region Constructor

        public AllInvoicesViewModel()
            : base()
        {
            base.DisplayName = "Invoices";

        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewInvoiceViewModel(); // Zwracamy odpowiedni ViewModel
        }
        #endregion

        #region Sort And Find
        //tu decydujemy po czym sortować do comboboxa
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "document number", "first name customer"};
        }
        //tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "document number")
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.DocumentDocumentNumber));
            if (SortField == "first name customer")
                List = new ObservableCollection<InvoiceForAllView>(List.OrderBy(item => item.CustomerFirstName));
        }
        //tu decydujemy po czym wyszukiwać
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "document number", "first name customer" };
        }
        //tu decydujemy jak wyszukiwać
        public override void Find()
        {
            Load();
            if (FindField == "document number")
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.DocumentDocumentNumber != null && item.DocumentDocumentNumber.StartsWith(FindTextBox)));
            if (FindField == "first name customer")
                List = new ObservableCollection<InvoiceForAllView>(List.Where(item => item.CustomerFirstName != null && item.CustomerFirstName.StartsWith(FindTextBox)));
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<InvoiceForAllView>
                (
                    from invoices in logistiq_Entities.Invoices
                    select new InvoiceForAllView
                    {
                        InvoiceID = invoices.InvoiceID,
                        DocumentType = invoices.Documents.Type,
                        DocumentDocumentNumber = invoices.Documents.DocumentNumber,
                        CustomerFirstName = invoices.Customers.FirstName,
                        CustomerLastName = invoices.Customers.LastName,
                        CustomerNIP = invoices.Customers.NIP,
                        IssueDate = invoices.IssueDate,
                        DueDate = invoices.DueDate,
                        TotalAmount = invoices.TotalAmount,
                        PaymentStatus = invoices.PaymentStatus
                    }
                );
        }
        #endregion
    }
}