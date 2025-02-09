using System;                         // Import podstawowych typów systemowych, m.in. DateTime.
using System.Collections.Generic;     // Import kolekcji generycznych, np. List<T> (nieużywane bezpośrednio w tej klasie, ale przydatne w innych przypadkach).
using System.Linq;                    // Import LINQ, umożliwia wykonywanie zapytań na kolekcjach.
using System.Text;                    // Import przestrzeni nazw do operacji na ciągach znakowych.
using System.Threading.Tasks;         // Import przestrzeni nazw dla obsługi operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Definicja przestrzeni nazw dla encji widokowych.
{
    /// <summary>
    /// Klasa DocumentForAllView reprezentuje model widoku dokumentu.
    /// Zawiera właściwości opisujące dokument, takie jak jego identyfikator, typ, numer, datę,
    /// informacje o magazynie, łączną wartość oraz dodatkowe notatki.
    /// </summary>
    public class DocumentForAllView
    {
        // Unikalny identyfikator dokumentu.
        public int DocumentID { get; set; }

        // Typ dokumentu, np. faktura, paragon, zamówienie itp.
        public string Type { get; set; }

        // Numer dokumentu, używany do identyfikacji lub wyszukiwania.
        public string DocumentNumber { get; set; }

        // Data wystawienia lub utworzenia dokumentu.
        public DateTime Date { get; set; }

        // Nazwa magazynu, z którym dokument jest powiązany.
        public string WarehouseName { get; set; }

        // Lokalizacja magazynu, gdzie dokument został wystawiony lub jest przechowywany.
        public string WarehouseLocation { get; set; }

        // Całkowita wartość dokumentu, np. suma wartości produktów lub usług.
        public decimal TotalValue { get; set; }

        // Dodatkowe notatki lub komentarze związane z dokumentem.
        public string Notes { get; set; }
    }
}
