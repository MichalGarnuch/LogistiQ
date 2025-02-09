using LogistiQ.Models.BusinessLogic.BaseWorkspace; // Import bazowych klas logiki biznesowej (m.in. DatabaseClass), które umożliwiają współdzielenie kontekstu bazy danych.
using LogistiQ.Models.Entities;                      // Import encji bazy danych, takich jak Deliveries, DeliveryDetails, Suppliers, Warehouses itp.
using LogistiQ.Models.EntitiesForView;               // Import encji widokowych (DTO), m.in. DeliveryRecordForAllView, które są wykorzystywane do prezentacji danych w UI.
using System;                                        // Import podstawowych klas systemowych.
using System.Collections.Generic;                    // Import kolekcji generycznych, np. List<T>.
using System.Linq;                                   // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach.

namespace LogistiQ.Models.BusinessLogic.SharedLogic
{
    /// <summary>
    /// Klasa `DeliveryRecordB` obsługuje logikę biznesową dotyczącą dostaw.
    /// Dziedziczy po `DatabaseClass`, dzięki czemu ma dostęp do kontekstu bazy danych.
    /// </summary>
    public class DeliveryRecordB : DatabaseClass
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor przekazuje kontekst bazy danych do klasy nadrzędnej (`DatabaseClass`).
        /// </summary>
        /// <param name="db">Instancja kontekstu bazy danych `LogistiQ_Entities`.</param>
        public DeliveryRecordB(LogistiQ_Entities db)
            : base(db) // Przekazuje instancję kontekstu bazy do klasy bazowej, co umożliwia współdzielenie połączenia z bazą.
        { } // Konstruktor nie posiada dodatkowej logiki, ponieważ inicjalizacja odbywa się w klasie bazowej.

        #endregion

        #region Funkcje biznesowe

        /// <summary>
        /// Pobiera listę dostaw przypisanych do konkretnego magazynu.
        /// </summary>
        /// <param name="warehouseId">ID magazynu, dla którego mają zostać pobrane dostawy.</param>
        /// <returns>
        /// Lista obiektów `DeliveryRecordForAllView` zawierających szczegóły dostaw, 
        /// z połączeniem danych z tabel dostaw oraz ich szczegółów.
        /// </returns>
        public List<DeliveryRecordForAllView> GetDeliveriesByWarehouse(int warehouseId)
        {
            // Zapytanie LINQ, które:
            // 1. Iteruje po wszystkich dostawach w tabeli Deliveries.
            // 2. Filtruje te dostawy, aby wybrać tylko te powiązane z podanym magazynem.
            // 3. Dla każdej pasującej dostawy iteruje po szczegółach dostawy (DeliveryDetails),
            //    łącząc je na podstawie zgodności DeliveryID.
            // 4. Tworzy obiekty DTO DeliveryRecordForAllView zawierające potrzebne dane do widoku.
            // 5. Konwertuje wynik zapytania do listy.
            return (from delivery in db.Deliveries                      // Iteracja po tabeli dostaw.
                    where delivery.WarehouseID == warehouseId             // Filtrowanie dostaw wg ID magazynu.
                    from detail in db.DeliveryDetails                      // Iteracja po szczegółach dostaw.
                    where detail.DeliveryID == delivery.DeliveryID        // Łączenie szczegółów z odpowiednią dostawą.
                    select new DeliveryRecordForAllView                  // Tworzenie obiektu DTO do widoku.
                    {
                        DeliveryID = delivery.DeliveryID,                // Przypisanie ID dostawy.
                        SupplierName = delivery.Suppliers.Name,           // Pobranie nazwy dostawcy z powiązanej encji Suppliers.
                        DeliveryDate = delivery.DeliveryDate,             // Przypisanie daty dostawy.
                        ProductName = detail.Products.Name,               // Pobranie nazwy produktu z powiązanej encji Products.
                        Quantity = detail.Quantity,                       // Przypisanie ilości produktu dostarczonego.
                        UnitPrice = detail.UnitPrice,                     // Przypisanie ceny jednostkowej produktu.
                        WarehouseName = delivery.Warehouses.Name          // Pobranie nazwy magazynu z powiązanej encji Warehouses.
                    }).ToList(); // Konwersja wyniku zapytania na listę obiektów DeliveryRecordForAllView.
        }

        /// <summary>
        /// Oblicza łączną wartość wszystkich dostaw do danego magazynu.
        /// </summary>
        /// <param name="warehouseId">ID magazynu, dla którego liczymy sumę wartości dostaw.</param>
        /// <returns>Łączna wartość dostaw jako `decimal`.</returns>
        public decimal GetTotalDeliveryValue(int warehouseId)
        {
            // Filtruje szczegóły dostaw, wybierając te, których powiązana dostawa należy do zadanego magazynu.
            // Następnie sumuje wartość każdej pozycji jako iloczyn ilości i ceny jednostkowej.
            return db.DeliveryDetails
                .Where(detail => detail.Deliveries.WarehouseID == warehouseId) // Filtrowanie szczegółów dostaw według magazynu.
                .Sum(detail => detail.Quantity * detail.UnitPrice);            // Sumowanie wartości każdej pozycji: ilość * cena jednostkowa.
        }

        /// <summary>
        /// Oblicza średnią cenę jednostkową produktów dostarczonych do magazynu.
        /// </summary>
        /// <param name="warehouseId">ID magazynu, dla którego liczymy średnią cenę produktów.</param>
        /// <returns>
        /// Średnia cena jednostkowa produktów w danym magazynie jako `decimal`. 
        /// Zwraca 0, jeśli nie znaleziono żadnych produktów.
        /// </returns>
        public decimal GetAverageProductPrice(int warehouseId)
        {
            // Pobiera ceny jednostkowe produktów ze szczegółów dostaw, które należą do zadanego magazynu.
            var prices = db.DeliveryDetails
                .Where(detail => detail.Deliveries.WarehouseID == warehouseId) // Filtrowanie według magazynu.
                .Select(detail => detail.UnitPrice);                            // Wybór samej ceny jednostkowej.

            // Jeśli lista cen zawiera przynajmniej jeden element, oblicza średnią cenę.
            // W przeciwnym razie zwraca 0.
            return prices.Any() ? prices.Average() : 0;
        }

        #endregion
    }
}
