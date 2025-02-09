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
        #region Helpers
        public override void Save()
        {
            logistiQ_Entities.Invoices.Add(item);
            logistiQ_Entities.SaveChanges();
        }

        #endregion

        #region Validation

        public string Error => string.Empty;
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
                    validateMessage = StringValidator.ValidateIsNotEmpty(CustomerID?.ToString());
                }
                else if (properties == nameof(DocumentID))
                {
                    validateMessage = StringValidator.ValidateIsNotEmpty(DocumentID?.ToString());
                }
                else if (properties == nameof(PaymentStatus))
                {
                    validateMessage = StringValidator.ValidateIsFirstLetterUpper(PaymentStatus);
                }

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
            return !_validationMessages.Any();
        }

        #endregion
    }
}
