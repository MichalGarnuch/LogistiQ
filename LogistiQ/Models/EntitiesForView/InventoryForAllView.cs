using System;                        // Import podstawowych typów systemowych, np. DateTime.
using System.Collections.Generic;    // Import kolekcji generycznych (np. List<T>), przydatnych przy pracy z kolekcjami.
using System.Linq;                   // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw do operacji na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla obsługi operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Definicja przestrzeni nazw, w której znajdują się encje widokowe.
{
    /// <summary>
    /// Klasa InventoryForAllView reprezentuje dane inwentaryzacji przeznaczone do wyświetlenia w interfejsie użytkownika.
    /// Zawiera właściwości opisujące inwentaryzację, takie jak:
    /// - InventoryID: unikalny identyfikator inwentaryzacji,
    /// - WarehouseName i WarehouseLocation: informacje o magazynie,
    /// - Date: data wykonania inwentaryzacji (wartość opcjonalna),
    /// - ProductName i ProductType: informacje o produkcie,
    /// - RecordedQuantity: zarejestrowana ilość produktu (wartość opcjonalna).
    /// </summary>
    public class InventoryForAllView
    {
        // Unikalny identyfikator inwentaryzacji.
        public int InventoryID { get; set; }

        // Nazwa magazynu, w którym przeprowadzono inwentaryzację.
        public string WarehouseName { get; set; }

        // Lokalizacja magazynu.
        public string WarehouseLocation { get; set; }

        // Data przeprowadzenia inwentaryzacji.
        // Użycie typu nullable (DateTime?) oznacza, że wartość może być pusta.
        public DateTime? Date { get; set; }

        // Nazwa produktu będącego przedmiotem inwentaryzacji.
        public string ProductName { get; set; }

        // Typ produktu.
        public string ProductType { get; set; }

        // Zarejestrowana ilość produktu podczas inwentaryzacji.
        // Użycie typu nullable (int?) pozwala na brak wartości, jeśli ilość nie została zarejestrowana.
        public int? RecordedQuantity { get; set; }
    }
}
