using LogistiQ.Models.BusinessLogic;
using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using LogistiQ.Validators;
using LogistiQ.Views.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.ViewModels.Documents
{
    public class NewDocumentViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Documents>, IDataErrorInfo
    {

        #region Konstruktor
        public NewDocumentViewModel()
            : base("Document")
        {
            item = new Models.Entities.Documents();
        }

        #endregion

        #region Properties

        public int DocumentID
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
        public string Type
        {
            get
            {
                return item.Type;
            }
            set
            {
                item.Type = value;
                OnPropertyChanged(() => Type);
            }
        }
        public string DocumentNumber
        {
            get
            {
                return item.DocumentNumber;
            }
            set
            {
                item.DocumentNumber = value;
                OnPropertyChanged(() => DocumentNumber);
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
        public int? WarehouseID
        {
            get
            {
                return item.WarehouseID;
            }
            set
            {
                item.WarehouseID = value;
                OnPropertyChanged(() => WarehouseID);
            }
        }
        public decimal TotalValue
        {
            get
            {
                return item.TotalValue;
            }
            set
            {
                item.TotalValue = value;
                OnPropertyChanged(() => TotalValue);
            }
        }
        public string Notes
        {
            get
            {
                return item.Notes;
            }
            set
            {
                item.Notes = value;
                OnPropertyChanged(() => Notes);
            }
        }

        #endregion

        #region PropertiesForCombobox

        public IQueryable<KeyAndValue> WarehouseKeyAndValueItems
        {
            get
            {
                return new LogistiQ.Models.BusinessLogic.
                    WarehouseB(logistiQ_Entities).GetWarehouseKeyAndValueItems();
            }
        }
        #endregion

        #region Helpers
        public override void Save()
        {
            logistiQ_Entities.Documents.Add(item);//dodaje towar do lokalnej kolekcji
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

                if (properties == nameof(Type))
                {
                    validateMessage = StringValidator.ValidateIsNotEmpty(Type?.ToString());
                }
                else if (properties == nameof(DocumentNumber))
                {
                    validateMessage = StringValidator.ValidateIsNotEmpty(DocumentNumber);
                }
                else if (properties == nameof(WarehouseID))
                {
                    validateMessage = StringValidator.ValidateIsNotEmpty(WarehouseID?.ToString());
                }
                else if (properties == nameof(TotalValue))
                {
                    validateMessage = BusinessValidator.ValidateIsPricePositive(TotalValue);
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
