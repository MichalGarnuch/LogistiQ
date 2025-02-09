using System;                        // Import podstawowych typów systemowych, np. DateTime, int, string.
using System.Collections.Generic;    // Import kolekcji generycznych, przydatnych przy pracy z listami, choć w tej klasie nie są bezpośrednio używane.
using System.Linq;                   // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw do operacji na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla obsługi operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Deklaracja przestrzeni nazw, w której znajdują się modele widokowe używane do prezentacji danych w interfejsie użytkownika.
{
    /// <summary>
    /// Klasa OrderForAllView reprezentuje model widoku zamówienia.
    /// Zawiera właściwości, które opisują podstawowe dane zamówienia, takie jak identyfikator, dane klienta, datę zamówienia, status oraz łączną kwotę.
    /// </summary>
    public class OrderForAllView
    {
        // Unikalny identyfikator zamówienia.
        public int OrderID { get; set; }

        // Imię klienta powiązanego z zamówieniem.
        public string CustomerFirstName { get; set; }

        // Nazwisko klienta powiązanego z zamówieniem.
        public string CustomerLastName { get; set; }

        // Numer identyfikacji podatkowej (NIP) klienta.
        public string CustomerNIP { get; set; }

        // Data zamówienia. Typ nullable (DateTime?) pozwala na brak przypisanej wartości.
        public DateTime? OrderDate { get; set; }

        // Status zamówienia, np. "Completed", "Pending" itp.
        public string Status { get; set; }

        // Łączna kwota zamówienia. Typ nullable (decimal?) umożliwia brak przypisanej wartości, gdy kwota nie została obliczona.
        public decimal? Total { get; set; }
    }
}
