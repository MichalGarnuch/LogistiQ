using System;                            // Import przestrzeni nazw System, zawiera podstawowe typy i funkcje.
using System.Collections.Generic;        // Import przestrzeni nazw dla kolekcji generycznych (np. List<T>).
using System.Linq;                       // Import LINQ, umożliwia wykonywanie zapytań na kolekcjach.
using System.Text;                       // Import przestrzeni nazw dla operacji na tekstach.
using System.Threading.Tasks;            // Import przestrzeni nazw dla obsługi operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView.BaseWorkspace
{
    /// <summary>
    /// Klasa KeyAndValue reprezentuje prostą strukturę danych przechowującą parę:
    /// - Key: identyfikator (klucz) w postaci liczby całkowitej,
    /// - Value: odpowiadającą wartość w postaci ciągu znaków.
    /// 
    /// Jest wykorzystywana głównie do prezentacji danych w widokach, gdzie potrzebna jest reprezentacja pary klucz-wartość.
    /// </summary>
    public class KeyAndValue
    {
        // Właściwość Key przechowuje identyfikator (klucz) obiektu.
        public int Key { get; set; }

        // Właściwość Value przechowuje wartość odpowiadającą kluczowi, zwykle opis lub nazwa.
        public String Value { get; set; }
    }
}
