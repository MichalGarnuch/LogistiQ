using LogistiQ.Models.Entities; // Importujemy model encji z bazy danych, np. `Categories`.
using LogistiQ.Validators; // Importujemy walidator tekstów (`StringValidator`).
using LogistiQ.ViewModels.BaseWorkspace; // Klasy bazowe dla ViewModeli.
using LogistiQ.Views.BaseWorkspace; // Widoki bazowe, np. `SingleRecordViewBase`.
using System; // Podstawowe funkcje systemowe.
using System.Collections.Generic; // Obsługa kolekcji, np. list i słowników.
using System.ComponentModel;
using System.Linq; // Funkcje LINQ do pracy z kolekcjami (np. filtrowanie danych).
using System.Text; // Manipulacja tekstami.
using System.Threading.Tasks; // Zadania asynchroniczne.

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
