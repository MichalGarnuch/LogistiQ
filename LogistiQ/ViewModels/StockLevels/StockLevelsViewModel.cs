using LogistiQ.Models.EntitiesForView;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogistiQ.ViewModels.StockLevels
{
    public class StockLevelsViewModel : AllViewModel<StockLevelForAllView>
    {
        #region Constructor

        public StockLevelsViewModel()
            : base()
        {
            base.DisplayName = "StockLevels";
            IsAddVisible = false;
        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return null;
        }
        public new void AddNew() { }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<StockLevelForAllView>
                (
                    from stockLevels in logistiq_Entities.StockLevels
                    select new StockLevelForAllView
                    {
                        StockLevelID = stockLevels.StockLevelID,
                        ProductName = stockLevels.Products.Name,
                        ProductType = stockLevels.Products.Type,
                        WarehouseName = stockLevels.Warehouses.Name,
                        WarehouseLocation = stockLevels.Warehouses.Location,
                        Quantity = stockLevels.Quantity,
                        LastUpdated = stockLevels.LastUpdated
                    }
                );
        }
        #endregion
    }
}