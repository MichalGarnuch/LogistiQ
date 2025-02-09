using System;                        // Import podstawowych typów systemowych, m.in. DateTime.
using System.Collections.Generic;    // Import kolekcji generycznych (np. List<T>), choć nie używane bezpośrednio w tej klasie.
using System.Linq;                   // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw do operacji na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla obsługi operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Deklaracja przestrzeni nazw, w której znajduje się klasa DiscountForAllView.
{
    /// <summary>
    /// Klasa DiscountForAllView reprezentuje dane rabatu, które mają zostać wyświetlone w interfejsie użytkownika.
    /// Zawiera właściwości opisujące rabat, takie jak identyfikator, nazwa produktu, typ produktu, okres obowiązywania rabatu 
    /// oraz procent rabatu.
    /// </summary>
    public class DiscountForAllView
    {
        // Unikalny identyfikator rabatu.
        public int DiscountID { get; set; }

        // Nazwa produktu, dla którego ustalony jest rabat.
        public string ProductName { get; set; }

        // Typ produktu, do którego odnosi się rabat.
        public string ProductType { get; set; }

        // Data rozpoczęcia obowiązywania rabatu.
        public DateTime StartDate { get; set; }

        // Data zakończenia obowiązywania rabatu.
        public DateTime EndDate { get; set; }

        // Procent rabatu, wyrażony jako wartość dziesiętna (np. 0.15 oznacza 15% rabatu).
        public decimal DiscountPercent { get; set; }
    }
}
