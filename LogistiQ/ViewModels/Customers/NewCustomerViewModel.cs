using LogistiQ.Models.BusinessLogic;
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

namespace LogistiQ.ViewModels.Customers
{
    public class NewCustomersViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Customers>, IDataErrorInfo
    {

        #region Konstruktor
        public NewCustomersViewModel()
            : base("Customers")
        {
            item = new Models.Entities.Customers();
        }

        #endregion

        #region Properties

        //dla każdego pola na interfejsie tworzymy properties
        public int CustomerID
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
        public string FirstName
        {
            get
            {
                return item.FirstName;
            }
            set
            {
                item.FirstName = value;
                OnPropertyChanged(() => FirstName);
            }
        }
        public string LastName
        {
            get
            {
                return item.LastName;
            }
            set
            {
                item.LastName = value;
                OnPropertyChanged(() => LastName);
            }
        }
        public string NIP
        {
            get
            {
                return item.NIP;
            }
            set
            {
                item.NIP = value;
                OnPropertyChanged(() => NIP);
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
            logistiQ_Entities.Customers.Add(item);
            logistiQ_Entities.SaveChanges();
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

                if (properties == nameof(FirstName))
                {
                    validateMessage = StringValidator.ValidateIsFirstLetterUpper(FirstName);
                }
                else if (properties == nameof(LastName))
                {
                    validateMessage = StringValidator.ValidateIsFirstLetterUpper(LastName);
                }
                else if (properties == nameof(NIP))
                {
                    // Walidacja, czy wybrano dokument
                    validateMessage = StringValidator.ValidateIsNotEmpty(NIP?.ToString());
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
            return !_validationMessages.Any();
        }

        #endregion
    }
}
