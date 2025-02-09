using System;                        // Import podstawowych typów systemowych, np. int, string, decimal.
using System.Collections.Generic;    // Import kolekcji generycznych, choć w tej klasie nie są bezpośrednio używane.
using System.Linq;                   // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw do operacji na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla obsługi operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Deklaracja przestrzeni nazw dla encji widokowych.
{
    /// <summary>
    /// Klasa OrderDetailForAllView reprezentuje szczegóły zamówienia przeznaczone do wyświetlenia w interfejsie użytkownika.
    /// Zawiera informacje dotyczące szczegółów zamówienia, takie jak:
    /// - OrderDetailID: unikalny identyfikator szczegółu zamówienia,
    /// - OrderCustomerIDFirstName, OrderCustomerIDLastName, OrderCustomerIDNIP: dane klienta powiązanego z zamówieniem,
    /// - ProductName: nazwa produktu w zamówieniu,
    /// - Quantity: ilość zamówionego produktu,
    /// - UnitPrice: cena jednostkowa produktu,
    /// - VAT: wartość podatku VAT.
    /// </summary>
    public class OrderDetailForAllView
    {
        // Unikalny identyfikator szczegółu zamówienia.
        public int OrderDetailID { get; set; }

        // Imię klienta powiązanego z zamówieniem.
        public string OrderCustomerIDFirstName { get; set; }

        // Nazwisko klienta powiązanego z zamówieniem.
        public string OrderCustomerIDLastName { get; set; }

        // Numer identyfikacji podatkowej (NIP) klienta powiązanego z zamówieniem.
        public string OrderCustomerIDNIP { get; set; }

        // Nazwa produktu, który został zamówiony.
        public string ProductName { get; set; }

        // Ilość zamówionego produktu.
        public int Quantity { get; set; }

        // Cena jednostkowa produktu.
        public decimal UnitPrice { get; set; }

        // Wartość podatku VAT dla zamówionego produktu.
        public decimal VAT { get; set; }
    }
}
