using System;                        // Import podstawowych typów systemowych, np. DateTime.
using System.Collections.Generic;    // Import przestrzeni nazw dla kolekcji generycznych (np. List<T>).
using System.Linq;                   // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw do operacji na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla obsługi operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Deklaracja przestrzeni nazw dla encji widokowych.
{
    /// <summary>
    /// Klasa DeliveryRecordForAllView reprezentuje rekord dostawy przeznaczony do wyświetlenia w interfejsie użytkownika.
    /// Zawiera informacje o dostawie, takie jak:
    /// - DeliveryID: unikalny identyfikator dostawy,
    /// - SupplierName: nazwa dostawcy,
    /// - DeliveryDate: data dostawy,
    /// - ProductName: nazwa produktu,
    /// - Quantity: ilość dostarczonych produktów,
    /// - UnitPrice: cena jednostkowa produktu,
    /// - WarehouseName: nazwa magazynu,
    /// oraz oblicza TotalPrice jako iloczyn Quantity i UnitPrice.
    /// </summary>
    public class DeliveryRecordForAllView
    {
        // Unikalny identyfikator dostawy.
        public int DeliveryID { get; set; }

        // Nazwa dostawcy, który zrealizował dostawę.
        public string SupplierName { get; set; }

        // Data, w której dokonano dostawy.
        public DateTime DeliveryDate { get; set; }

        // Nazwa produktu, który został dostarczony.
        public string ProductName { get; set; }

        // Ilość dostarczonych jednostek produktu.
        public int Quantity { get; set; }

        // Cena jednostkowa produktu w dostawie.
        public decimal UnitPrice { get; set; }

        // Łączna cena dostawy, obliczana jako iloczyn Quantity i UnitPrice.
        public decimal TotalPrice => Quantity * UnitPrice;

        // Nazwa magazynu, do którego dostawa została skierowana.
        public string WarehouseName { get; set; }
    }
}
