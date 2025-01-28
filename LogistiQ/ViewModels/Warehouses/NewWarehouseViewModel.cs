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

namespace LogistiQ.ViewModels.Warehouses
{
    public class NewWarehouseViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Warehouses>, IDataErrorInfo
    {

        #region Konstruktor
        public NewWarehouseViewModel()
            : base("Warehouses")
        {
            item = new Models.Entities.Warehouses();
        }

        #endregion

        #region Properties

        //dla każdego pola na interfejsie tworzymy properties
        public int WarehouseID
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
        public string Location
        {
            get
            {
                return item.Location;
            }
            set
            {
                item.Location = value;
                OnPropertyChanged(() => Location);
            }
        }
        public int Capacity
        {
            get
            {
                return item.Capacity;
            }
            set
            {
                item.Capacity = value;
                OnPropertyChanged(() => Capacity);
            }
        }

        #endregion

        #region Helpers
        public override void Save()
        {
            logistiQ_Entities.Warehouses.Add(item);//dodaje towar do lokalnej kolekcji
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

                if (properties == nameof(Name))
                {
                    validateMessage = StringValidator.ValidateIsFirstLetterUpper(Name);
                }
                else if (properties == nameof(Location))
                {
                    validateMessage = StringValidator.ValidateIsNotEmpty(Location);
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
