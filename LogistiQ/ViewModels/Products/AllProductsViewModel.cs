using LogistiQ.Helper;
using LogistiQ.Models.Entities;
using LogistiQ.ViewModels.BaseWorkspace;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using LogistiQ.Views.BaseWorkspace;
using System.Linq;
using LogistiQ.Models.EntitiesForView;

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
