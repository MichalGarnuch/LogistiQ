using LogistiQ.Models.BusinessLogic.BaseWorkspace; // Import klasy bazowej, która umożliwia dostęp do kontekstu bazy danych.
using LogistiQ.Models.Entities;                        // Import encji bazy danych, np. StockLevels, Products, Warehouses, DeliveryDetails.
using LogistiQ.Models.EntitiesForView;                 // Import encji widokowych (DTO), np. WarehouseOverviewForAllView, służących do prezentacji danych.
using System.Collections.Generic;                    // Import kolekcji generycznych, np. List<T>.
using System.Linq;                                   // Import LINQ umożliwiający wykonywanie zapytań na kolekcjach.

namespace LogistiQ.Models.BusinessLogic.SharedLogic
{
    // Klasa WarehouseOverviewB zawiera logikę biznesową dotyczącą przeglądu stanu magazynowego.
    // Dziedziczy po DatabaseClass, co pozwala na korzystanie z kontekstu bazy danych (db).
    public class WarehouseOverviewB : DatabaseClass
    {
        #region Konstruktor

        // Konstruktor przyjmujący instancję kontekstu bazy danych i przekazujący ją do klasy bazowej.
        public WarehouseOverviewB(LogistiQ_Entities db)
            : base(db) { }

        #endregion

        #region Funkcje biznesowe

        // Metoda GetWarehouseStock pobiera stan magazynowy dla określonego magazynu.
        // Zwraca listę obiektów WarehouseOverviewForAllView, które zawierają szczegółowe informacje o stanie magazynowym.
        public List<WarehouseOverviewForAllView> GetWarehouseStock(int warehouseId)
        {
            return (from stock in db.StockLevels                          // Rozpoczynamy zapytanie od tabeli StockLevels, która przechowuje dane o stanach magazynowych.
                    join product in db.Products                         // Łączymy StockLevels z tabelą Products, aby uzyskać informacje o produktach.
                        on stock.ProductID equals product.ProductID
                    join warehouse in db.Warehouses                      // Łączymy StockLevels z tabelą Warehouses, aby uzyskać dane o magazynach.
                        on stock.WarehouseID equals warehouse.WarehouseID
                    // Łączymy z tabelą DeliveryDetails, tworząc grupę dostaw (deliveries) dla danego produktu.
                    join deliveryDetail in db.DeliveryDetails
                        on stock.ProductID equals deliveryDetail.ProductID into deliveries
                    // Wybieramy ostatnią dostawę z grupy deliveries (sortując malejąco według DeliveryID).
                    // Użycie DefaultIfEmpty() zapewnia, że jeśli nie ma żadnej dostawy, wynik nie spowoduje błędu (left join).
                    from lastDelivery in deliveries.OrderByDescending(d => d.DeliveryID).Take(1).DefaultIfEmpty()
                        // Filtrowanie wyników dla określonego magazynu.
                    where stock.WarehouseID == warehouseId
                    // Tworzymy nowy obiekt WarehouseOverviewForAllView z pobranych danych.
                    select new WarehouseOverviewForAllView
                    {
                        ProductName = product.Name,                            // Nazwa produktu.
                        WarehouseName = warehouse.Name,                        // Nazwa magazynu.
                        Quantity = stock.Quantity,                             // Ilość produktu dostępna w magazynie.
                        UnitPrice = product.UnitPrice,                         // Cena jednostkowa produktu.
                        TotalStockValue = stock.Quantity * product.UnitPrice,  // Łączna wartość stanu magazynowego (ilość × cena jednostkowa).
                        // Obliczenie wartości ostatniej dostawy:
                        // Jeśli istnieje ostatnia dostawa, mnożymy ilość przez cenę jednostkową; w przeciwnym razie ustawiamy na 0.
                        LastDeliveryValue = lastDelivery != null ? lastDelivery.Quantity * lastDelivery.UnitPrice : 0m,
                        // Obliczenie średniej ceny dostawy:
                        // Jeśli istnieją dostawy, obliczamy średnią cenę jednostkową; w przeciwnym razie ustawiamy na 0.
                        AverageDeliveryPrice = deliveries.Any() ? deliveries.Average(d => d.UnitPrice) : 0m
                    }).ToList(); // Konwersja wyniku zapytania na listę obiektów WarehouseOverviewForAllView.
        }

        #endregion
    }
}
