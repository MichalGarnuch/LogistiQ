using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogistiQ.ViewModels.Products;


namespace LogistiQ.ViewModels.Discounts
{
    public class AllDiscountsViewModel : AllViewModel<DiscountForAllView>
    {
        #region Constructor

        public AllDiscountsViewModel()
            : base()
        {
            base.DisplayName = "Discounts";

        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewDiscountViewModel(); 
        }
        #endregion

        #region Sort And Find
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "product name", "product type"};
        }
        public override void Sort()
        {
            if (SortField == "product name")
                List = new ObservableCollection<DiscountForAllView>(List.OrderBy(item => item.ProductName));
            if (SortField == "product type")
                List = new ObservableCollection<DiscountForAllView>(List.OrderBy(item => item.ProductType));
        }
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "product name", "product type" };
        }
        public override void Find()
        {
            Load();
            if (FindField == "product name")
                List = new ObservableCollection<DiscountForAllView>(List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
            if (FindField == "product type")
                List = new ObservableCollection<DiscountForAllView>(List.Where(item => item.ProductType != null && item.ProductType.StartsWith(FindTextBox)));
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<DiscountForAllView>
                (
                    from discount in logistiq_Entities.Discounts
                    select new DiscountForAllView
                    {
                        DiscountID = discount.DiscountID,
                        ProductName = discount.Products.Name,
                        ProductType = discount.Products.Type,
                        StartDate = discount.StartDate,
                        EndDate = discount.EndDate,
                        DiscountPercent = discount.DiscountPercent
                    }
                );
        }
        #endregion
    }
}