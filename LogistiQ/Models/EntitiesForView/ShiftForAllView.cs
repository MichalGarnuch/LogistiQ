using System;                        // Import podstawowych typów systemowych, takich jak DateTime i TimeSpan.
using System.Collections.Generic;    // Import kolekcji generycznych (np. List<T>), choć w tej klasie nie są bezpośrednio używane.
using System.Linq;                   // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw dla operacji na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla obsługi operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Deklaracja przestrzeni nazw dla modeli widokowych.
{
    /// <summary>
    /// Klasa ShiftForAllView reprezentuje model widoku zmiany pracy (shift) pracownika.
    /// Zawiera właściwości opisujące szczegóły zmiany, takie jak identyfikator zmiany, dane pracownika,
    /// datę oraz czas rozpoczęcia i zakończenia zmiany, a także informacje o magazynie.
    /// </summary>
    public class ShiftForAllView
    {
        // Unikalny identyfikator zmiany.
        public int ShiftID { get; set; }

        // Imię pracownika, który odbywa zmianę.
        public string EmployeeFirstName { get; set; }

        // Nazwisko pracownika.
        public string EmployeeLastName { get; set; }

        // Stanowisko pracownika (np. "Kierownik", "Magazynier").
        public string EmployeePosition { get; set; }

        // Data, w której odbywa się zmiana.
        public DateTime Date { get; set; }

        // Czas rozpoczęcia zmiany.
        public TimeSpan StartTime { get; set; }

        // Czas zakończenia zmiany.
        public TimeSpan EndTime { get; set; }

        // Nazwa magazynu, w którym pracownik odbywa zmianę.
        public string WarehouseName { get; set; }

        // Lokalizacja magazynu (np. adres lub opis miejsca).
        public string WarehouseLocation { get; set; }
    }
}
