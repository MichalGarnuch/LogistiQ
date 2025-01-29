using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.ViewModels.Categories;
using LogistiQ.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.ViewModels.Categories
{
    public class AllCategoriesViewModel :AllViewModel<CategoryForAllView>
    {
        #region Constructor
            public AllCategoriesViewModel()
            : base()
        {
            base.DisplayName = "Categories";

        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewCategoryViewModel();
        }
        #endregion

        #region Sort And Find
        //tu decydujemy po czym sortować do comboboxa
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "name", "description"};
        }
        //tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "name")
                List = new ObservableCollection<CategoryForAllView>(List.OrderBy(item => item.Name));
            if (SortField == "description")
                List = new ObservableCollection<CategoryForAllView>(List.OrderBy(item => item.Description));
        }
        //tu decydujemy po czym wyszukiwać
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "name", "description" };
        }
        //tu decydujemy jak wyszukiwać
        public override void Find()
        {
            Load();
            if (FindField == "name")
                List = new ObservableCollection<CategoryForAllView>(List.Where(item => item.Name != null && item.Name.StartsWith(FindTextBox)));
            if (FindField == "description")
                List = new ObservableCollection<CategoryForAllView>(List.Where(item => item.Description != null && item.Description.StartsWith(FindTextBox)));
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<CategoryForAllView>
                (
                    from categories in logistiq_Entities.Categories
                    select new CategoryForAllView
                    {
                        CategoryID = categories.CategoryID,
                        Name = categories.Name,
                        Description = categories.Description
                    });
        }
       

        #endregion
    }
}
