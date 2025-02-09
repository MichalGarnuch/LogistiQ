using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using LogistiQ.Validators;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Views.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel;

namespace LogistiQ.ViewModels.Orders
{   
    public class NewOrderViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Orders>, IDataErrorInfo
    {

        #region Konstruktor
        public NewOrderViewModel()
            : base("Orders")
        {
            item = new Models.Entities.Orders();
        }

        #endregion

        #region Properties

        public int OrderID
        {
            get
            {
                return item.OrderID;
            }
            set
            {
                item.OrderID = value;
                OnPropertyChanged(() => OrderID);
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
        public DateTime? OrderDate
        {
            get
            {
                return item.OrderDate;
            }
            set
            {
                item.OrderDate = value;
                OnPropertyChanged(() => OrderDate);
            }
        }
        public string Status
        {
            get
            {
                return item.Status;
            }
            set
            {
                item.Status = value;
                OnPropertyChanged(() => Status);
            }
        }
        public decimal? Total
        {
            get
            {
                return item.Total;
            }
            set
            {
                item.Total = value;
                OnPropertyChanged(() => Total);
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

        #endregion

        #region Helpers
        public override void Save()
        {
            logistiQ_Entities.Orders.Add(item);
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

                if (properties == nameof(CustomerID))
                {
                    validateMessage = StringValidator.ValidateIsNotEmpty(CustomerID?.ToString());
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
