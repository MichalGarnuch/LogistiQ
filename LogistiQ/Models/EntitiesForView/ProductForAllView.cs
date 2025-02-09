using System;                        // Import podstawowych typów systemowych, takich jak int, string, decimal.
using System.Collections.Generic;    // Import przestrzeni nazw umożliwiającej korzystanie z kolekcji generycznych.
using System.Linq;                   // Import LINQ, pozwala na wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw dla operacji na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Definicja przestrzeni nazw dla encji widokowych, które służą do prezentacji danych w UI.
{
    /// <summary>
    /// Klasa ProductForAllView reprezentuje model widoku produktu.
    /// Zawiera właściwości opisujące produkt, takie jak:
    /// - ProductID: unikalny identyfikator produktu,
    /// - Name: nazwa produktu,
    /// - Type: typ produktu (np. kategoria lub rodzaj),
    /// - Brand: marka produktu,
    /// - UnitPrice: cena jednostkowa produktu,
    /// - Description: opis produktu,
    /// - VAT: wartość podatku VAT,
    /// - CategoryName: nazwa kategorii, do której produkt należy.
    /// Ten model jest wykorzystywany do prezentacji danych o produkcie w interfejsie użytkownika.
    /// </summary>
    public class ProductForAllView
    {
        // Unikalny identyfikator produktu.
        public int ProductID { get; set; }

        // Nazwa produktu.
        public string Name { get; set; }

        // Typ produktu, który może określać jego kategorię lub rodzaj.
        public string Type { get; set; }

        // Marka produktu.
        public string Brand { get; set; }

        // Cena jednostkowa produktu.
        public decimal UnitPrice { get; set; }

        // Opis produktu, zawierający dodatkowe informacje lub specyfikacje.
        public string Description { get; set; }

        // Wartość podatku VAT przypisanego do produktu.
        public decimal VAT { get; set; }

        // Nazwa kategorii, do której produkt należy.
        public string CategoryName { get; set; }
    }
}
