using LogistiQ.Models.BusinessLogic.BaseWorkspace;
using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using LogistiQ.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.BusinessLogic.SharedLogic
{
    public class WarehouseOverviewB : DatabaseClass
    {
        #region Konstruktor
        public WarehouseOverviewB(LogistiQ_Entities db)
            : base(db) { }
        #endregion

        #region Funkcje biznesowe
        public IQueryable<KeyAndValue> GetWarehouseKeyAndValueItems()
        {
            return (from warehouse in db.Warehouses
                    select new KeyAndValue
                    {
                        Key = warehouse.WarehouseID,
                        Value = warehouse.Name + " - " + warehouse.Location
                    }).ToList().AsQueryable();
        }

        public List<WarehouseOverviewForAllView> GetWarehouseStock(int warehouseId)
        {
            return (from stock in db.StockLevels
                    where stock.WarehouseID == warehouseId
                    select new WarehouseOverviewForAllView
                    {
                        ProductName = stock.Products.Name,
                        WarehouseName = stock.Warehouses.Name,
                        Quantity = stock.Quantity
                    }).ToList();
        }
        #endregion
    }
}
