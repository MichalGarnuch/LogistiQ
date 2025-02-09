using System;                        // Import podstawowych typów systemowych, takich jak int, string, DateTime, decimal.
using System.Collections.Generic;    // Import kolekcji generycznych, np. List<T>, chociaż w tej klasie nie są bezpośrednio używane.
using System.Linq;                   // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw do operacji na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla obsługi operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Definicja przestrzeni nazw dla encji widokowych (view models).
{
    /// <summary>
    /// Klasa OrderRecordForAllView reprezentuje model widoku szczegółów zamówienia.
    /// Zawiera właściwości niezbędne do wyświetlenia szczegółowych informacji o zamówieniu,
    /// takie jak dane klienta, produkt, ilość, cena oraz status płatności.
    /// </summary>
    public class OrderRecordForAllView
    {
        // Unikalny identyfikator zamówienia.
        public int OrderID { get; set; }

        // Nazwa klienta składająca się zazwyczaj z imienia i nazwiska,
        // używana do identyfikacji klienta powiązanego z zamówieniem.
        public string CustomerName { get; set; }

        // Data, kiedy zamówienie zostało złożone.
        public DateTime OrderDate { get; set; }

        // Status zamówienia, np. "Completed", "Pending", "Cancelled" itp.
        public string Status { get; set; }

        // Cena jednostkowa produktu zamówionego.
        public decimal UnitPrice { get; set; }

        // Ilość zamówionego produktu.
        public int Quantity { get; set; }

        // Łączna wartość zamówienia, która zazwyczaj jest obliczana jako iloczyn UnitPrice i Quantity.
        public decimal TotalOrderValue { get; set; }

        // Status płatności zamówienia, np. "Paid" lub "Unpaid".
        public string PaymentStatus { get; set; }

        // Nazwa produktu, który został zamówiony.
        public string ProductName { get; set; }
    }
}
