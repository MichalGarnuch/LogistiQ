using LogistiQ.Models.BusinessLogic.BaseWorkspace; // Import przestrzeni nazw zawierającej klasę bazową DatabaseClass, która zapewnia dostęp do kontekstu bazy danych (db).
using LogistiQ.Models.Entities;                      // Import encji bazy danych, np. klasy reprezentującej magazyny (Warehouses).
using LogistiQ.Models.EntitiesForView.BaseWorkspace; // Import klasy DTO KeyAndValue, która umożliwia przekazywanie danych w formacie klucz-wartość.
using System;                                      // Import podstawowych typów i funkcji systemowych.
using System.Collections.Generic;                  // Import kolekcji generycznych, takich jak List<T>.
using System.Linq;                                 // Import LINQ umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.BusinessLogic
{
    /// <summary>
    /// Klasa WarehouseB zawiera logikę biznesową dotyczącą operacji na magazynach.
    /// Dziedziczy po klasie DatabaseClass, co umożliwia dostęp do wspólnego kontekstu bazy danych (db).
    /// </summary>
    public class WarehouseB : DatabaseClass
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor klasy WarehouseB przyjmuje instancję kontekstu bazy danych i przekazuje ją do klasy bazowej DatabaseClass.
        /// Dzięki temu wszystkie metody tej klasy mają dostęp do danych bazy poprzez właściwość db.
        /// </summary>
        /// <param name="db">Instancja kontekstu bazy danych LogistiQ_Entities.</param>
        public WarehouseB(LogistiQ_Entities db)
            : base(db) { }

        #endregion

        #region Funkcje biznesowe

        /// <summary>
        /// Metoda GetWarehouseKeyAndValueItems pobiera listę magazynów z bazy danych i mapuje je na obiekty KeyAndValue.
        /// Każdy obiekt KeyAndValue zawiera:
        /// - Key: unikalny identyfikator magazynu (WarehouseID),
        /// - Value: ciąg znaków złożony z nazwy magazynu oraz jego lokalizacji.
        /// Wynik zwracany jest jako IQueryable, co umożliwia dalsze operacje zapytań na tym zbiorze.
        /// </summary>
        /// <returns>IQueryable<KeyAndValue> zawierający pary klucz-wartość dla magazynów.</returns>
        public IQueryable<KeyAndValue> GetWarehouseKeyAndValueItems()
        {
            // Zapytanie LINQ, które:
            // 1. Iteruje po encjach z tabeli Warehouses w kontekście bazy danych.
            // 2. Dla każdej encji tworzy nowy obiekt KeyAndValue z przypisanymi właściwościami.
            //    - Key ustawiony jest na WarehouseID,
            //    - Value to połączenie nazwy magazynu (Name) i jego lokalizacji (Location), oddzielone spacją.
            // 3. Wynik zapytania jest konwertowany na listę, a następnie na IQueryable, co umożliwia dalsze operacje na tym zbiorze.
            return
                (
                    from warehouse in db.Warehouses // Iteracja po wszystkich magazynach.
                    select new KeyAndValue       // Mapowanie każdej encji magazynu na obiekt KeyAndValue.
                    {
                        Key = warehouse.WarehouseID,                    // Przypisanie identyfikatora magazynu jako klucza.
                        Value = warehouse.Name + " " + warehouse.Location // Łączenie nazwy magazynu i lokalizacji w jeden ciąg znaków.
                    }
                ).ToList().AsQueryable(); // Konwersja wyniku na listę, a następnie na IQueryable.
        }

        #endregion
    }
}
