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
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "warehouse name", "product name" };
        }
        public override void Sort()
        {
            if (SortField == "warehouse name")
                List = new ObservableCollection<InventoryForAllView>(List.OrderBy(item => item.WarehouseName));
            if (SortField == "product name")
                List = new ObservableCollection<InventoryForAllView>(List.OrderBy(item => item.ProductName));
        }
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "warehouse name", "product name" };
        }
        public override void Find()
        {
            Load();
            if (FindField == "warehouse name")
                List = new ObservableCollection<InventoryForAllView>(List.Where(item => item.WarehouseName != null && item.WarehouseName.StartsWith(FindTextBox)));
            if (FindField == "product name")
                List = new ObservableCollection<InventoryForAllView>(List.Where(item => item.ProductName != null && item.ProductName.StartsWith(FindTextBox)));
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
