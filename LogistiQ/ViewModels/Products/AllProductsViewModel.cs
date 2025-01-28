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
            return new NewProductViewModel(); // Zwracamy odpowiedni ViewModel
        }

        #endregion

        #region Sort And Find
        //tu decydujemy po czym sortować do comboboxa
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "name", "type", "brand" };
        }
        //tu decydujemy jak sortować
        public override void Sort()
        {
            throw new System.NotImplementedException();
        }
        //tu decydujemy po czym wyszukiwać
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "name", "type" };
        }
        //tu decydujemy jak wyszukiwać
        public override void Find()
        {
            throw new System.NotImplementedException();
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
