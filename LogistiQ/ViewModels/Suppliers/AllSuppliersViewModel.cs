using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogistiQ.ViewModels.Suppliers
{
    public class AllSuppliersViewModel : AllViewModel<LogistiQ.Models.Entities.Suppliers>
    {
        #region Constructor

        public AllSuppliersViewModel()
            : base()
        {
            base.DisplayName = "Suppliers";

        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewSupplierViewModel(); // Zwracamy odpowiedni ViewModel
        }
        #endregion

        #region Sort And Find
        //tu decydujemy po czym sortować do comboboxa
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "name", "address" };
        }
        //tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "name")
                List = new ObservableCollection<LogistiQ.Models.Entities.Suppliers>(List.OrderBy(item => item.Name));
            if (SortField == "address")
                List = new ObservableCollection<LogistiQ.Models.Entities.Suppliers>(List.OrderBy(item => item.Address));
        }
        //tu decydujemy po czym wyszukiwać
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "name", "address" };
        }
        //tu decydujemy jak wyszukiwać
        public override void Find()
        {
            Load();
            if (FindField == "name")
                List = new ObservableCollection<LogistiQ.Models.Entities.Suppliers>(List.Where(item => item.Name != null && item.Name.StartsWith(FindTextBox)));
            if (FindField == "address")
                List = new ObservableCollection<LogistiQ.Models.Entities.Suppliers>(List.Where(item => item.Address != null && item.Address.StartsWith(FindTextBox)));
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<LogistiQ.Models.Entities.Suppliers>
                (
                    logistiq_Entities.Suppliers.ToList()
                );
        }
        #endregion
    }
}