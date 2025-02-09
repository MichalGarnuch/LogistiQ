using LogistiQ.Models.BusinessLogic.BaseWorkspace; // Import klasy bazowej, która zapewnia dostęp do kontekstu bazy danych (db).
using LogistiQ.Models.Entities;                        // Import encji bazy danych, np. tabel Deliveries i Warehouses.
using LogistiQ.Models.EntitiesForView.BaseWorkspace; // Import klasy DTO KeyAndValue, używanej do prezentacji danych w formacie klucz-wartość.
using System.Linq;                                   // Import LINQ umożliwiający wykonywanie zapytań na kolekcjach.

namespace LogistiQ.Models.BusinessLogic
{
    // Klasa DeliveryB zawiera logikę biznesową związaną z operacjami na dostawach i magazynach.
    // Dziedziczy po klasie DatabaseClass, co umożliwia dostęp do wspólnego kontekstu bazy danych.
    public class DeliveryB : DatabaseClass
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor klasy DeliveryB przyjmuje instancję kontekstu bazy danych
        /// i przekazuje ją do klasy bazowej DatabaseClass.
        /// Dzięki temu wszystkie metody tej klasy mogą korzystać z właściwości db.
        /// </summary>
        /// <param name="db">Instancja kontekstu bazy danych LogistiQ_Entities.</param>
        public DeliveryB(LogistiQ_Entities db)
            : base(db) { }

        #endregion

        #region Funkcje biznesowe

        /// <summary>
        /// Pobiera listę dostaw w formacie KeyAndValue.
        /// Każdy element zawiera:
        /// - Key: identyfikator dostawy,
        /// - Value: nazwę dostawcy oraz datę dostawy oddzielone " | ".
        /// </summary>
        /// <returns>IQueryable<KeyAndValue> zawierający pary klucz-wartość dla dostaw.</returns>
        public IQueryable<KeyAndValue> GetDeliveryKeyAndValueItems()
        {
            return db.Deliveries                           // Pobieramy wszystkie rekordy z tabeli Deliveries.
                .Select(delivery => new KeyAndValue        // Dla każdej dostawy tworzymy obiekt KeyAndValue.
                {
                    Key = delivery.DeliveryID,              // Ustawiamy identyfikator dostawy jako klucz.
                    Value = delivery.Suppliers.Name + " | " + delivery.DeliveryDate // Łączymy nazwę dostawcy i datę dostawy.
                })
                .ToList()                                 // Wykonujemy zapytanie i konwertujemy wyniki na listę.
                .AsQueryable();                           // Przekształcamy listę z powrotem do IQueryable, umożliwiając dalsze operacje.
        }

        /// <summary>
        /// Pobiera listę magazynów w formacie KeyAndValue.
        /// Każdy element zawiera:
        /// - Key: identyfikator magazynu,
        /// - Value: nazwę magazynu wraz z lokalizacją oddzieloną " | ".
        /// </summary>
        /// <returns>IQueryable<KeyAndValue> zawierający pary klucz-wartość dla magazynów.</returns>
        public IQueryable<KeyAndValue> GetWarehouseKeyAndValueItems()
        {
            return db.Warehouses                          // Pobieramy wszystkie rekordy z tabeli Warehouses.
                .Select(warehouse => new KeyAndValue      // Dla każdego magazynu tworzymy obiekt KeyAndValue.
                {
                    Key = warehouse.WarehouseID,           // Ustawiamy identyfikator magazynu jako klucz.
                    Value = warehouse.Name + " | " + warehouse.Location // Łączymy nazwę magazynu i lokalizację.
                })
                .ToList()                                 // Wykonujemy zapytanie i konwertujemy wyniki na listę.
                .AsQueryable();                           // Przekształcamy listę z powrotem do IQueryable.
        }

        /// <summary>
        /// Pobiera listę dostaw dla danego magazynu.
        /// Argument warehouseId określa ID magazynu, dla którego mają być pobrane dostawy.
        /// Każdy element zawiera:
        /// - Key: identyfikator dostawy,
        /// - Value: nazwę dostawcy oraz datę dostawy oddzielone " | ".
        /// </summary>
        /// <param name="warehouseId">ID magazynu.</param>
        /// <returns>IQueryable<KeyAndValue> zawierający dostawy dla określonego magazynu.</returns>
        public IQueryable<KeyAndValue> GetDeliveriesByWarehouse(int warehouseId)
        {
            return db.Deliveries                           // Pobieramy wszystkie rekordy z tabeli Deliveries.
                .Where(delivery => delivery.WarehouseID == warehouseId) // Filtrowanie rekordów po ID magazynu.
                .Select(delivery => new KeyAndValue         // Mapowanie wyników na obiekty KeyAndValue.
                {
                    Key = delivery.DeliveryID,              // Ustawiamy identyfikator dostawy jako klucz.
                    Value = delivery.Suppliers.Name + " | " + delivery.DeliveryDate // Łączymy nazwę dostawcy i datę dostawy.
                })
                .ToList()                                 // Wykonujemy zapytanie i konwertujemy wyniki na listę.
                .AsQueryable();                           // Przekształcamy listę z powrotem do IQueryable.
        }

        /// <summary>
        /// Pobiera listę dostaw dla danego dostawcy.
        /// Argument supplierId określa ID dostawcy, dla którego mają być pobrane dostawy.
        /// Każdy element zawiera:
        /// - Key: identyfikator dostawy,
        /// - Value: nazwę magazynu (prefiks "Warehouse: ") oraz datę dostawy oddzielone " | ".
        /// </summary>
        /// <param name="supplierId">ID dostawcy.</param>
        /// <returns>IQueryable<KeyAndValue> zawierający dostawy dla określonego dostawcy.</returns>
        public IQueryable<KeyAndValue> GetDeliveriesBySupplier(int supplierId)
        {
            return db.Deliveries                           // Pobieramy wszystkie rekordy z tabeli Deliveries.
                .Where(delivery => delivery.SupplierID == supplierId) // Filtrowanie rekordów po ID dostawcy.
                .Select(delivery => new KeyAndValue         // Mapowanie wyników na obiekty KeyAndValue.
                {
                    Key = delivery.DeliveryID,              // Ustawiamy identyfikator dostawy jako klucz.
                    Value = "Warehouse: " + delivery.Warehouses.Name + " | " + delivery.DeliveryDate // Łączymy nazwę magazynu z datą dostawy, poprzedzone prefiksem.
                })
                .ToList()                                 // Wykonujemy zapytanie i konwertujemy wyniki na listę.
                .AsQueryable();                           // Przekształcamy listę z powrotem do IQueryable.
        }

        #endregion
    }
}
