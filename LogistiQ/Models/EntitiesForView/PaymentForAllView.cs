using System;                        // Import podstawowych typów systemowych, np. int, decimal, DateTime.
using System.Collections.Generic;    // Import kolekcji generycznych, np. List<T> (nie jest bezpośrednio używany w tej klasie).
using System.Linq;                   // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw do operacji na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla obsługi operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Deklaracja przestrzeni nazw dla encji widokowych.
{
    /// <summary>
    /// Klasa PaymentForAllView reprezentuje model widoku płatności.
    /// Zawiera właściwości opisujące płatność, takie jak:
    /// - PaymentID: unikalny identyfikator płatności,
    /// - Dane klienta powiązanego z zamówieniem (pobrane jako klucze obce),
    /// - PaymentMethod: metoda płatności,
    /// - Amount: kwota zapłacona,
    /// - PaymentDate: data dokonania płatności.
    /// </summary>
    public class PaymentForAllView
    {
        // Unikalny identyfikator płatności.
        public int PaymentID { get; set; }

        // klucz obcy poczatek
        // Poniższe właściwości reprezentują dane klienta powiązanego z zamówieniem, który dokonał płatności.
        // Są one pobierane z powiązanej encji klienta i służą jako klucze obce.
        public string OrderCustomerIDFirstName { get; set; }  // Imię klienta.
        public string OrderCustomerIDLastName { get; set; }   // Nazwisko klienta.
        public string OrderCustomerIDNIP { get; set; }        // Numer identyfikacyjny klienta (NIP).
        public string OrderCustomerIDAddress { get; set; }    // Adres klienta.
        public string OrderCustomerIDPhone { get; set; }      // Numer telefonu klienta.
        public string OrderCustomerIDEmail { get; set; }      // Adres email klienta.
        // klucz obcy koniec

        // Metoda płatności, np. "Credit Card", "Cash", "Bank Transfer".
        public string PaymentMethod { get; set; }

        // Kwota zapłacona w ramach płatności.
        public decimal Amount { get; set; }

        // Data dokonania płatności. Typ nullable (DateTime?) oznacza, że data może nie być ustalona.
        public DateTime? PaymentDate { get; set; }
    }
}
