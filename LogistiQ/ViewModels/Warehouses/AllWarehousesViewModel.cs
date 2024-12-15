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