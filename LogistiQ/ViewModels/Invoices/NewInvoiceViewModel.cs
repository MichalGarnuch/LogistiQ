using LogistiQ.Models.Entities;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Views.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.ViewModels.Invoices
{

    public class NewInvoiceViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Invoices>
    {

        #region Konstruktor
        public NewInvoiceViewModel()
            : base("Invoices")
        {
            item = new Models.Entities.Invoices();
        }

        #endregion

        #region Properties

        //dla każdego pola na interfejsie tworzymy properties
        public int InvoiceID
        {
            get
            {
                return item.InvoiceID;
            }
            set
            {
                item.InvoiceID = value;
                OnPropertyChanged(() => InvoiceID);
            }
        }
        public int? DocumentID
        {
            get
            {
                return item.DocumentID;
            }
            set
            {
                item.DocumentID = value;
                OnPropertyChanged(() => DocumentID);
            }
        }
        public int? CustomerID
        {
            get
            {
                return item.CustomerID;
            }
            set
            {
                item.CustomerID = value;
                OnPropertyChanged(() => CustomerID);
            }
        }
        public DateTime IssueDate
        {
            get
            {
                return item.IssueDate;
            }
            set
            {
                item.IssueDate = value;
                OnPropertyChanged(() => IssueDate);
            }
        }
        public DateTime DueDate
        {
            get
            {
                return item.DueDate;
            }
            set
            {
                item.DueDate = value;
                OnPropertyChanged(() => DueDate);
            }
        }
        public decimal TotalAmount
        {
            get
            {
                return item.TotalAmount;
            }
            set
            {
                item.TotalAmount = value;
                OnPropertyChanged(() => TotalAmount);
            }
        }
        public string PaymentStatus
        {
            get
            {
                return item.PaymentStatus;
            }
            set
            {
                item.PaymentStatus = value;
                OnPropertyChanged(() => PaymentStatus);
            }
        }

        #endregion

        #region Helpers
        public override void Save()
        {
            logistiQ_Entities.Invoices.Add(item);//dodaje towar do lokalnej kolekcji
            logistiQ_Entities.SaveChanges();//zapisuje zmiany dokonane w bazie danych
        }

        #endregion
    }
}
