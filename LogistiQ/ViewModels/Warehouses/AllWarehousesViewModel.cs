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
            List = new ObservableCollection<LogistiQ.Models.Entities.Warehouses>
                (
                    logistiq_Entities.Warehouses.ToList()
                );
        }
        #endregion
    }
}