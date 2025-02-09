using System;                        // Import podstawowych typów systemowych, np. int, string.
using System.Collections.Generic;    // Import kolekcji generycznych, np. List<T> (choć nie są bezpośrednio używane w tej klasie).
using System.Linq;                   // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw do operacji na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Definicja przestrzeni nazw dla encji widokowych, które są używane do prezentacji danych w interfejsie użytkownika.
{
    /// <summary>
    /// Klasa EmployeeForAllView reprezentuje model widoku pracownika.
    /// Zawiera właściwości opisujące dane pracownika, w tym dane osobowe, stanowisko oraz informacje dotyczące magazynu,
    /// z którym pracownik jest powiązany. Dodatkowo, można przechowywać uwagi dotyczące pracownika.
    /// </summary>
    public class EmployeeForAllView
    {
        // Unikalny identyfikator pracownika.
        public int EmployeeID { get; set; }

        // Poniższe właściwości reprezentują dane osobowe pracownika oraz informacje związane z magazynem.
        // "klucz obcy początek" - komentarz wskazujący, że dane te mogą być powiązane z innymi encjami (np. magazynem) w bazie danych.
        public string FirstName { get; set; }   // Imię pracownika.
        public string LastName { get; set; }    // Nazwisko pracownika.
        public string Position { get; set; }    // Stanowisko lub rola pracownika w firmie.
        public string WarehouseName { get; set; }      // Nazwa magazynu, z którym pracownik jest powiązany.
        public string WarehouseLocation { get; set; }  // Lokalizacja magazynu.

        // "klucz obcy koniec" - kończy sekcję właściwości powiązanych z kluczami obcymi.
        public string Remarks { get; set; }     // Dodatkowe uwagi lub notatki dotyczące pracownika.
    }
}
