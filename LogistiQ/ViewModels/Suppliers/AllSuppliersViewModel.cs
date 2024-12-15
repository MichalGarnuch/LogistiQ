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