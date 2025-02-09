using System;                        // Import podstawowych typów i funkcji systemowych.
using System.Collections.Generic;    // Import kolekcji generycznych, np. List<T>.
using System.Linq;                   // Import LINQ umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw do operacji na tekstach.
using System.Threading.Tasks;        // Import przestrzeni nazw do obsługi operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Deklaracja przestrzeni nazw, w której znajduje się klasa CategoryForAllView.
{
    /// <summary>
    /// Klasa CategoryForAllView reprezentuje model widoku dla kategorii.
    /// Zawiera właściwości, które będą wykorzystywane do wyświetlania danych kategorii w interfejsie użytkownika.
    /// </summary>
    public class CategoryForAllView
    {
        // Właściwość przechowująca unikalny identyfikator kategorii.
        public int CategoryID { get; set; }

        // Właściwość przechowująca nazwę kategorii.
        public string Name { get; set; }

        // Właściwość przechowująca opis kategorii.
        public string Description { get; set; }
    }
}
