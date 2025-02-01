using LogistiQ.Models.BusinessLogic.BaseWorkspace;
using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogistiQ.Models.BusinessLogic.SharedLogic
{
    public class DeliveryRecordB : DatabaseClass
    {
        #region Konstruktor
        public DeliveryRecordB(LogistiQ_Entities db)
            : base(db) { }
        #endregion

        #region Funkcje biznesowe

        // Pobranie dostaw dla danego magazynu
        public List<DeliveryRecordForAllView> GetDeliveriesByWarehouse(int warehouseId)
        {
            return (from delivery in db.Deliveries
                    where delivery.WarehouseID == warehouseId
                    from detail in db.DeliveryDetails
                    where detail.DeliveryID == delivery.DeliveryID
                    select new DeliveryRecordForAllView
                    {
                        DeliveryID = delivery.DeliveryID,
                        SupplierName = delivery.Suppliers.Name,
                        DeliveryDate = delivery.DeliveryDate,
                        ProductName = detail.Products.Name,
                        Quantity = detail.Quantity,
                        UnitPrice = detail.UnitPrice,
                        WarehouseName = delivery.Warehouses.Name
                    }).ToList();
        }

        #endregion
    }
}
