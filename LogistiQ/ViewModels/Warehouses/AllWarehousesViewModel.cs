using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogistiQ.ViewModels.Warehouses
{
    public class AllWarehousesViewModel : AllViewModel<LogistiQ.Models.Entities.Warehouses>
    {
        #region Constructor

        public AllWarehousesViewModel()
            : base()
        {
            base.DisplayName = "Warehouses";

        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewWarehouseViewModel(); // Zwracamy odpowiedni ViewModel
        }
        #endregion

        #region Sort And Find
        //tu decydujemy po czym sortować do comboboxa
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "name", "location" };
        }
        //tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "name")
                List = new ObservableCollection<LogistiQ.Models.Entities.Warehouses>(List.OrderBy(item => item.Name));
            if (SortField == "location")
                List = new ObservableCollection<LogistiQ.Models.Entities.Warehouses>(List.OrderBy(item => item.Location));
        }
        //tu decydujemy po czym wyszukiwać
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "name", "location" };
        }
        //tu decydujemy jak wyszukiwać
        public override void Find()
        {
            Load();
            if (FindField == "name")
                List = new ObservableCollection<LogistiQ.Models.Entities.Warehouses>(List.Where(item => item.Name != null && item.Name.StartsWith(FindTextBox)));
            if (FindField == "location")
                List = new ObservableCollection<LogistiQ.Models.Entities.Warehouses>(List.Where(item => item.Location != null && item.Location.StartsWith(FindTextBox)));
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<LogistiQ.Models.Entities.Warehouses>
                (
                    logistiq_Entities.Warehouses.ToList()
                );
        }
        #endregion
    }
}