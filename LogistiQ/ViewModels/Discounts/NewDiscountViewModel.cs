using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using LogistiQ.Validators;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Views.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LogistiQ.ViewModels.Discounts
{
    public class NewDiscountViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Discounts>, IDataErrorInfo
    {

        #region Konstruktor
        public NewDiscountViewModel()
            : base("Discounts")
        {
            item = new Models.Entities.Discounts();
        }

        #endregion

        #region Properties

        //dla każdego pola na interfejsie tworzymy properties
        public int DiscountID
        {
            get
            {
                return item.DiscountID;
            }
            set
            {
                item.DiscountID = value;
                OnPropertyChanged(() => DiscountID);
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
        public DateTime StartDate
        {
            get
            {
                return item.StartDate;
            }
            set
            {
                item.StartDate = value;
                OnPropertyChanged(() => StartDate);
            }
        }
        public DateTime EndDate
        {
            get
            {
                return item.EndDate;
            }
            set
            {
                item.EndDate = value;
                OnPropertyChanged(() => EndDate);
            }
        }
        public decimal DiscountPercent
        {
            get
            {
                return item.DiscountPercent;
            }
            set
            {
                item.DiscountPercent = value;
                OnPropertyChanged(() => DiscountPercent);
            }
        }

        #endregion

        #region PropertiesForCombobox

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
            logistiQ_Entities.Discounts.Add(item);//dodaje towar do lokalnej kolekcji
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

                if (properties == nameof(ProductID))
                {
                    validateMessage = StringValidator.ValidateIsNotEmpty(ProductID?.ToString());
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
