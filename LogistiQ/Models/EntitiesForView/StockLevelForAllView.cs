using System;                        // Import podstawowych typów systemowych, np. DateTime.
using System.Collections.Generic;    // Import przestrzeni nazw dla kolekcji generycznych, choć w tej klasie nie są używane.
using System.Linq;                   // Import LINQ umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw dla operacji na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla obsługi operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Definicja przestrzeni nazw, w której znajdują się modele widokowe (view models).
{
    /// <summary>
    /// Klasa StockLevelForAllView reprezentuje model widoku stanu magazynowego.
    /// Zawiera właściwości opisujące stan magazynowy produktu, takie jak:
    /// - StockLevelID: unikalny identyfikator rekordu stanu magazynowego,
    /// - ProductName: nazwa produktu,
    /// - ProductType: typ produktu (np. kategoria lub rodzaj),
    /// - WarehouseName: nazwa magazynu, w którym produkt jest przechowywany,
    /// - WarehouseLocation: lokalizacja magazynu,
    /// - Quantity: dostępna ilość produktu w magazynie,
    /// - LastUpdated: data ostatniej aktualizacji stanu magazynowego.
    /// </summary>
    public class StockLevelForAllView
    {
        // Unikalny identyfikator rekordu stanu magazynowego.
        public int StockLevelID { get; set; }

        // Nazwa produktu, którego stan magazynowy jest monitorowany.
        public string ProductName { get; set; }

        // Typ produktu, np. kategoria lub rodzaj produktu.
        public string ProductType { get; set; }

        // Nazwa magazynu, w którym znajduje się produkt.
        public string WarehouseName { get; set; }

        // Lokalizacja magazynu, np. adres lub opis miejsca.
        public string WarehouseLocation { get; set; }

        // Ilość produktu dostępna w magazynie.
        public int Quantity { get; set; }

        // Data ostatniej aktualizacji stanu magazynowego.
        public DateTime LastUpdated { get; set; }
    }
}
