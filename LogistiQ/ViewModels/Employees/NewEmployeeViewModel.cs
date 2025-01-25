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

namespace LogistiQ.ViewModels.Employees
{ 
    public class NewEmployeeViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Employees>, IDataErrorInfo
    {

        #region Konstruktor
        public NewEmployeeViewModel()
            : base("Employees")
        {
            item = new Models.Entities.Employees();
        }

        #endregion

        #region Properties

        //dla każdego pola na interfejsie tworzymy properties
        public int EmployeeID
        {
            get
            {
                return item.EmployeeID;
            }
            set
            {
                item.EmployeeID = value;
                OnPropertyChanged(() => EmployeeID);
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
        public string Position
        {
            get
            {
                return item.Position;
            }
            set
            {
                item.Position = value;
                OnPropertyChanged(() => Position);
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
            logistiQ_Entities.Employees.Add(item);//dodaje towar do lokalnej kolekcji
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

                if (properties == nameof(FirstName))
                {
                    validateMessage = StringValidator.ValidateIsFirstLetterUpper(FirstName);
                }
                else if (properties == nameof(LastName))
                {
                    validateMessage = StringValidator.ValidateIsFirstLetterUpper(LastName);
                }
                else if (properties == nameof(WarehouseID))
                {
                    validateMessage = StringValidator.ValidateIsNotEmpty(WarehouseID?.ToString());
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
