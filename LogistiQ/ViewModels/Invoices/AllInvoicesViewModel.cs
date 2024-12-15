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