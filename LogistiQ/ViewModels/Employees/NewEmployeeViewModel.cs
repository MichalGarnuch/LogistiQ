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
            logistiQ_Entities.Employees.Add(item);
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
