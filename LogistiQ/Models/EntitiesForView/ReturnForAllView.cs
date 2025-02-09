using System;                        // Import podstawowych typów systemowych, takich jak int, string, DateTime.
using System.Collections.Generic;    // Import przestrzeni nazw umożliwiającej korzystanie z kolekcji generycznych.
using System.Linq;                   // Import LINQ, który umożliwia wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw do operacji na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla obsługi operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Definicja przestrzeni nazw dla encji widokowych, używanych do prezentacji danych w interfejsie użytkownika.
{
    /// <summary>
    /// Klasa ReturnForAllView reprezentuje model widoku zwrotu towaru.
    /// Zawiera właściwości opisujące szczegóły zwrotu, takie jak:
    /// - ReturnID: unikalny identyfikator zwrotu,
    /// - Dane klienta powiązanego ze zwrotem (pobrane jako klucze obce),
    /// - Nazwa produktu, który został zwrócony,
    /// - Ilość zwróconego towaru,
    /// - Powód zwrotu oraz datę, kiedy zwrot został dokonany.
    /// Komentarze "klucz obcy poczatek" i "klucz obcy koniec" wskazują na grupy właściwości pochodzących z powiązanej encji.
    /// </summary>
    public class ReturnForAllView
    {
        // Unikalny identyfikator zwrotu.
        public int ReturnID { get; set; }

        // klucz obcy poczatek
        // Poniższe właściwości reprezentują dane klienta powiązanego ze zwrotem.
        public string OrderCustomerIDFirstName { get; set; }  // Imię klienta.
        public string OrderCustomerIDLastName { get; set; }   // Nazwisko klienta.
        public string OrderCustomerIDNIP { get; set; }        // Numer identyfikacji podatkowej (NIP) klienta.
        public string OrderCustomerIDAddress { get; set; }    // Adres klienta.
        public string OrderCustomerIDPhone { get; set; }      // Numer telefonu klienta.
        public string OrderCustomerIDEmail { get; set; }      // Adres email klienta.
        // klucz obcy koniec

        // klucz obcy poczatek
        // Właściwość reprezentująca nazwę produktu, który został zwrócony.
        public string ProductName { get; set; }
        // klucz obcy koniec

        // Ilość produktu, który został zwrócony.
        public int Quantity { get; set; }

        // Powód zwrotu, wyjaśniający przyczynę zwrotu produktu.
        public string Reason { get; set; }

        // Data, kiedy zwrot został dokonany.
        // Typ DateTime? (nullable) oznacza, że wartość może być pusta (null), jeśli data nie została ustalona.
        public DateTime? Date { get; set; }
    }
}
