using LogistiQ.Helper;
using LogistiQ.Models.Entities;
using LogistiQ.ViewModels.BaseWorkspace;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using LogistiQ.Views.BaseWorkspace;
using System.Linq;
using LogistiQ.Models.EntitiesForView;
using System.Collections.Generic;

namespace LogistiQ.ViewModels.Products
{
    public class AllProductsViewModel : AllViewModel<ProductForAllView>
    {
        #region Constructor

        public AllProductsViewModel()
            :base()
        {
            base.DisplayName = "Products";

        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewProductViewModel();
        }

        #endregion

        #region Sort And Find
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "id", "name", "type", "brand", "name and type" };
        }
        public override void Sort()
        {
            if(SortField=="id")
                List=new ObservableCollection<ProductForAllView> (List.OrderBy(item => item.ProductID));
            if(SortField=="name")
                List=new ObservableCollection<ProductForAllView> (List.OrderBy(item => item.Name));
            if(SortField=="type")
                List=new ObservableCollection<ProductForAllView> (List.OrderBy(item => item.Type));
            if(SortField=="brand")
                List=new ObservableCollection<ProductForAllView> (List.OrderBy(item => item.Brand));
            if(SortField=="name and type")
                List=new ObservableCollection<ProductForAllView> (List.OrderBy(item => item.Name).OrderBy(item => item.Type));
        }
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "name", "type" };
        }
        public override void Find()
        {
            Load();
            if (FindField == "name")
                List = new ObservableCollection<ProductForAllView>(List.Where(item => item.Name != null && item.Name.StartsWith(FindTextBox)));
            if (FindField == "type")
                List = new ObservableCollection<ProductForAllView>(List.Where(item => item.Type != null && item.Type.StartsWith(FindTextBox)));
        }
     
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<ProductForAllView>
                (
                    from products in logistiq_Entities.Products
                    select new ProductForAllView
                    {
                        ProductID = products.ProductID,
                        Name = products.Name,
                        Type = products.Type,
                        Brand = products.Brand,
                        UnitPrice = products.UnitPrice,
                        Description = products.Description,
                        VAT = products.VAT,
                        CategoryName = products.Categories.Name
                    }
                );
        }

        #endregion
    }
}
