using LogistiQ.Models.BusinessLogic.BaseWorkspace;
using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView;
using System.Collections.Generic;
using System.Linq;

namespace LogistiQ.Models.BusinessLogic.SharedLogic
{
    public class WarehouseOverviewB : DatabaseClass
    {
        #region Konstruktor
        public WarehouseOverviewB(LogistiQ_Entities db)
            : base(db) { }
        #endregion

        #region Funkcje biznesowe

        public List<WarehouseOverviewForAllView> GetWarehouseStock(int warehouseId)
        {
            return (from stock in db.StockLevels
                    join product in db.Products on stock.ProductID equals product.ProductID
                    join warehouse in db.Warehouses on stock.WarehouseID equals warehouse.WarehouseID
                    join deliveryDetail in db.DeliveryDetails on stock.ProductID equals deliveryDetail.ProductID into deliveries
                    from lastDelivery in deliveries.OrderByDescending(d => d.DeliveryID).Take(1).DefaultIfEmpty()
                    where stock.WarehouseID == warehouseId
                    select new WarehouseOverviewForAllView
                    {
                        ProductName = product.Name,
                        WarehouseName = warehouse.Name,
                        Quantity = stock.Quantity,
                        UnitPrice = product.UnitPrice,
                        TotalStockValue = stock.Quantity * product.UnitPrice,
                        LastDeliveryValue = lastDelivery != null ? lastDelivery.Quantity * lastDelivery.UnitPrice : 0m,
                        AverageDeliveryPrice = deliveries.Any() ? deliveries.Average(d => d.UnitPrice) : 0m
                    }).ToList();
        }

        #endregion
    }
}
