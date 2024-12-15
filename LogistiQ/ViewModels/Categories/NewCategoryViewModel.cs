using LogistiQ.Models.Entities;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Views.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.ViewModels.Categories
{
    public class NewCategoryViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Categories>
    {

        #region Konstruktor
        public NewCategoryViewModel()
            : base("Categories")
        {
            item = new Models.Entities.Categories();
        }

        #endregion

        #region Properties

        //dla każdego pola na interfejsie tworzymy properties
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
        #endregion

        #region Helpers
        public override void Save()
        {
            logistiQ_Entities.Categories.Add(item);//dodaje towar do lokalnej kolekcji
            logistiQ_Entities.SaveChanges();//zapisuje zmiany dokonane w bazie danych
        }

        #endregion


    }
}
