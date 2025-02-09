using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Views.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using LogistiQ.Validators;
using System.Xml.Linq;

namespace LogistiQ.ViewModels.Returns
{

    public class NewReturnViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Returns>, IDataErrorInfo
    {

        #region Konstruktor
        public NewReturnViewModel()
            : base("Returns")
        {
            item = new Models.Entities.Returns();
        }

        #endregion

        #region Properties

        public int ReturnID
        {
            get
            {
                return item.ReturnID;
            }
            set
            {
                item.ReturnID = value;
                OnPropertyChanged(() => ReturnID);
            }
        }
        public int? OrderID
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
        public int? ProductID
        {
            get
            {
                return item.ProductID;
            }
            set
            {
                item.ProductID = value;
                OnPropertyChanged(() => ProductID);
            }
        }
        public int Quantity
        {
            get
            {
                return item.Quantity;
            }
            set
            {
                item.Quantity = value;
                OnPropertyChanged(() => Quantity);
            }
        }
        public string Reason
        {
            get
            {
                return item.Reason;
            }
            set
            {
                item.Reason = value;
                OnPropertyChanged(() => Reason);
            }
        }
        public DateTime Date
        {
            get
            {
                return item.Date;
            }
            set
            {
                item.Date = value;
                OnPropertyChanged(() => Date);
            }
        }

        #endregion

        #region PropertiesForCombobox

        public IQueryable<KeyAndValue> OrderKeyAndValueItems
        {
            get
            {
                return new LogistiQ.Models.BusinessLogic.
                    OrderB(logistiQ_Entities).GetOrderKeyAndValueItems();
            }
        }
        public IQueryable<KeyAndValue> ProductKeyAndValueItems
        {
            get
            {
                return new LogistiQ.Models.BusinessLogic.
                    ProductB(logistiQ_Entities).GetProductKeyAndValueItems();
            }
        }
        #endregion

        #region Helpers
        public override void Save()
        {
            logistiQ_Entities.Returns.Add(item);
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

                if (properties == nameof(OrderID))
                {
                    validateMessage = StringValidator.ValidateIsNotEmpty(OrderID?.ToString());
                }
                else if (properties == nameof(ProductID))
                {
                    validateMessage = StringValidator.ValidateIsNotEmpty(ProductID?.ToString());
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
