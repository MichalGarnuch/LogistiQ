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

namespace LogistiQ.ViewModels.Categories
{
    public class NewCategoryViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Categories>, IDataErrorInfo
    {
        public NewCategoryViewModel()
            : base("Categories")
        {
            item = new Models.Entities.Categories();
        }

        public int CategoryID
        {
            get
            {
                return item.CategoryID;
            }
            set
            {
                item.CategoryID = value;
                OnPropertyChanged(() => CategoryID);
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

        public string Description
        {
            get
            {
                return item.Description;
            }
            set
            {
                item.Description = value;
                OnPropertyChanged(() => Description);
            }
        }

        public override void Save()
        {
            logistiQ_Entities.Categories.Add(item);
            logistiQ_Entities.SaveChanges();
        }

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
