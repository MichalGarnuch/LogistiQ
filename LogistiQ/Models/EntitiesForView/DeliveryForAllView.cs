using System;                        // Import podstawowych typów systemowych, np. DateTime.
using System.Collections.Generic;    // Import kolekcji generycznych, np. List<T> (choć nie używane bezpośrednio w tej klasie).
using System.Linq;                   // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw umożliwiający operacje na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla operacji asynchronicznych (nie jest używane w tej klasie).

namespace LogistiQ.Models.EntitiesForView  // Deklaracja przestrzeni nazw, w której znajdują się encje widokowe.
{
    /// <summary>
    /// Klasa DeliveryForAllView reprezentuje dane dostawy przeznaczone do wyświetlenia w interfejsie użytkownika.
    /// Zawiera właściwości opisujące szczegóły dostawy, dane dostawcy oraz informacje o magazynie.
    /// </summary>
    public class DeliveryForAllView
    {
        // Unikalny identyfikator dostawy.
        public int DeliveryID { get; set; }

        // Nazwa dostawcy, który zrealizował dostawę.
        public string SupplierName { get; set; }

        // Adres dostawcy.
        public string SupplierAddress { get; set; }

        // Data, w której dokonano dostawy.
        public DateTime DeliveryDate { get; set; }

        // Całkowity koszt dostawy.
        public decimal Cost { get; set; }

        // Status dostawy (np. "Completed", "Pending").
        public string Status { get; set; }

        // Nazwa magazynu, do którego dostawa została skierowana.
        public string WarehouseName { get; set; }

        // Lokalizacja magazynu.
        public string WarehouseLocation { get; set; }
    }
}
