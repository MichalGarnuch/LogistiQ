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
            return new NewDiscountViewModel(); // Zwracamy odpowiedni ViewModel
        }
        #endregion

        #region Sort And Find
        //tu decydujemy po czym sortować do comboboxa
        public override List<string> GetComboboxSortList()
        {
            throw new System.NotImplementedException();
        }
        //tu decydujemy jak sortować
        public override void Sort()
        {
            throw new System.NotImplementedException();
        }
        //tu decydujemy po czym wyszukiwać
        public override List<string> GetComboboxFindList()
        {
            throw new System.NotImplementedException();
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