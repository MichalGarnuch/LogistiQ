using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace LogistiQ.ViewModels.Inventory
{
        public class InventoryViewModel : AllViewModel<InventoryForAllView>
        {
        #region Constructor

            public InventoryViewModel()
                : base()
            {
                base.DisplayName = "Inventory";
                IsAddVisible = false;
        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return null;
        }
        public new void AddNew() { }

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
                List = new ObservableCollection<InventoryForAllView>
                    (
                    from inventory in logistiq_Entities.Inventory
                    select new InventoryForAllView
                    {
                        InventoryID = inventory.InventoryID,
                        WarehouseName = inventory.Warehouses.Name,
                        WarehouseLocation = inventory.Warehouses.Location,
                        Date = inventory.Date,
                        ProductName = inventory.Products.Name,
                        ProductType = inventory.Products.Type,
                        RecordedQuantity = inventory.RecordedQuantity
                    }
                );
            }
            #endregion
        }
}
