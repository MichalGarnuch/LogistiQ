using LogistiQ.Models.Entities;
using LogistiQ.Validators;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Views.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.ViewModels.Suppliers
{
    public class NewSupplierViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Suppliers>, IDataErrorInfo
    {

        #region Konstruktor
        public NewSupplierViewModel()
            : base("Suppliers")
        {
            item = new Models.Entities.Suppliers();
        }

        #endregion

        #region Properties

        //dla każdego pola na interfejsie tworzymy properties
        public int SupplierID
        {
            get
            {
                return item.SupplierID;
            }
            set
            {
                item.SupplierID = value;
                OnPropertyChanged(() => SupplierID);
            }
        }
        public string Name
        {
            get
            {
                return item.Name;
            }
            set
            {
                item.Name = value;
                OnPropertyChanged(() => Name);
            }
        }
        public string Address
        {
            get
            {
                return item.Address;
            }
            set
            {
                item.Address = value;
                OnPropertyChanged(() => Address);
            }
        }
        public string Phone
        {
            get
            {
                return item.Phone;
            }
            set
            {
                item.Phone = value;
                OnPropertyChanged(() => Phone);
            }
        }
        public string Email
        {
            get
            {
                return item.Email;
            }
            set
            {
                item.Email = value;
                OnPropertyChanged(() => Email);
            }
        }
        public string Remarks
        {
            get
            {
                return item.Remarks;
            }
            set
            {
                item.Remarks = value;
                OnPropertyChanged(() => Remarks);
            }
        }

        #endregion

        #region Helpers
        public override void Save()
        {
            logistiQ_Entities.Suppliers.Add(item);
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

                if (properties == nameof(Name))
                {
                    validateMessage = StringValidator.ValidateIsFirstLetterUpper(Name);
                }
                else if (properties == nameof(Email))
                {
                    validateMessage = StringValidator.ValidateIsNotEmpty(Email);
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
