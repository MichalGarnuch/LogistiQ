using LogistiQ.Models.BusinessLogic.BaseWorkspace;
using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using System.Linq;

namespace LogistiQ.Models.BusinessLogic
{
    public class DeliveryB : DatabaseClass
    {
        #region Konstruktor
        public DeliveryB(LogistiQ_Entities db)
            : base(db) { }
        #endregion

        #region Funkcje biznesowe

        // Pobranie listy dostaw w formacie KeyAndValue (ID + Nazwa dostawcy)
        public IQueryable<KeyAndValue> GetDeliveryKeyAndValueItems()
        {
            return db.Deliveries
                .Select(delivery => new KeyAndValue
                {
                    Key = delivery.DeliveryID,
                    Value = delivery.Suppliers.Name + " | " + delivery.DeliveryDate
                })
                .ToList()
                .AsQueryable();
        }

        // Pobranie listy magazynów w formacie KeyAndValue
        public IQueryable<KeyAndValue> GetWarehouseKeyAndValueItems()
        {
            return db.Warehouses
                .Select(warehouse => new KeyAndValue
                {
                    Key = warehouse.WarehouseID,
                    Value = warehouse.Name + " | " + warehouse.Location
                })
                .ToList()
                .AsQueryable();
        }

        // Pobranie dostaw dla danego magazynu
        public IQueryable<KeyAndValue> GetDeliveriesByWarehouse(int warehouseId)
        {
            return db.Deliveries
                .Where(delivery => delivery.WarehouseID == warehouseId)
                .Select(delivery => new KeyAndValue
                {
                    Key = delivery.DeliveryID,
                    Value = delivery.Suppliers.Name + " | " + delivery.DeliveryDate
                })
                .ToList()
                .AsQueryable();
        }

        // Pobranie dostaw dla danego dostawcy
        public IQueryable<KeyAndValue> GetDeliveriesBySupplier(int supplierId)
        {
            return db.Deliveries
                .Where(delivery => delivery.SupplierID == supplierId)
                .Select(delivery => new KeyAndValue
                {
                    Key = delivery.DeliveryID,
                    Value = "Warehouse: " + delivery.Warehouses.Name + " | " + delivery.DeliveryDate
                })
                .ToList()
                .AsQueryable();
        }

        #endregion
    }
}
