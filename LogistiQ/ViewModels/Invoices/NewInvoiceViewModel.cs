using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using LogistiQ.Validators;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Views.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LogistiQ.ViewModels.Invoices
{

    public class NewInvoiceViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Invoices>, IDataErrorInfo
    {

        #region Konstruktor
        public NewInvoiceViewModel()
            : base("Invoices")
        {
            item = new Models.Entities.Invoices();
            item.IssueDate = DateTime.Now; 
            item.DueDate = DateTime.Now.AddDays(14);
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

        //
        #region PropertiesForCombobox

        public IQueryable<KeyAndValue> CustomerKeyAndValueItems
        {
            get
            {
                return new LogistiQ.Models.BusinessLogic.
                    CustomerB(logistiQ_Entities).GetCustomerKeyAndValueItems();
            }
        }
        public IQueryable<KeyAndValue> DocumentKeyAndValueItems
        {
            get
            {
                return new LogistiQ.Models.BusinessLogic.
                    DocumentB(logistiQ_Entities).GetDocumentKeyAndValueItems();
            }
        }
        #endregion
        //
        #region Helpers
        public override void Save()
        {
            logistiQ_Entities.Invoices.Add(item);//dodaje towar do lokalnej kolekcji
            logistiQ_Entities.SaveChanges();//zapisuje zmiany dokonane w bazie danych
        }

        #endregion

        #region Validation

        public string Error => string.Empty;
        // Słownik przechowujący komunikaty błędów dla każdej właściwości
        private readonly Dictionary<string, string> _validationMessages = new Dictionary<string, string>();

        public string this[string properties]
        {
            get
            {
                string validateMessage = string.Empty;

                if (properties == nameof(TotalAmount))
                {
                    validateMessage = BusinessValidator.ValidateIsPricePositive(TotalAmount);
                }
                else if (properties == nameof(CustomerID))
                {
                    // Walidacja, czy wybrano klienta
                    validateMessage = StringValidator.ValidateIsNotEmpty(CustomerID?.ToString());
                }
                else if (properties == nameof(DocumentID))
                {
                    // Walidacja, czy wybrano dokument
                    validateMessage = StringValidator.ValidateIsNotEmpty(DocumentID?.ToString());
                }
                else if (properties == nameof(PaymentStatus))
                {
                    // Walidacja statusu płatności (czy zaczyna się wielką literą)
                    validateMessage = StringValidator.ValidateIsFirstLetterUpper(PaymentStatus);
                }

                // Aktualizujemy słownik błędów
                if (!string.IsNullOrEmpty(validateMessage))
                {
                    _validationMessages[properties] = validateMessage;
                }
                else
                {
                    _validationMessages.Remove(properties);
                }

                return validateMessage;
            }
        }

        public override bool IsValid()
        {
            // Jeśli w słowniku nie ma błędów, wszystkie pola są poprawne
            return !_validationMessages.Any();
        }

        #endregion
    }
}
