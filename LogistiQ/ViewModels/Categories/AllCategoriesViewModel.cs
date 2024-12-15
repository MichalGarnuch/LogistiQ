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
    public class AllCategoriesViewModel : AllViewModel<LogistiQ.Models.Entities.Categories>
    {
        #region Constructor
            public AllCategoriesViewModel()
            : base()
        {
            base.DisplayName = "Categories";

        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<LogistiQ.Models.Entities.Categories>
                (
                    logistiq_Entities.Categories.ToList()
                );
        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewCategoryViewModel();
        }

        #endregion
    }
}
