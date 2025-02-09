using LogistiQ.Models.Entities;                      // Import encji bazy danych, w tym przypadku klasy reprezentujące produkty.
using LogistiQ.Models.EntitiesForView.BaseWorkspace; // Import klasy DTO KeyAndValue, używanej do przekazywania danych w formacie klucz-wartość.
using System;                                        // Import podstawowych typów systemowych.
using LogistiQ.Models.BusinessLogic.BaseWorkspace;  // Import bazowej klasy DatabaseClass, która udostępnia wspólny kontekst bazy danych.
using System.Collections.Generic;                   // Import kolekcji generycznych, np. List<T>.
using System.Linq;                                  // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.BusinessLogic
{
    /// <summary>
    /// Klasa ProductB zawiera logikę biznesową dotyczącą operacji na produktach.
    /// Dziedziczy po klasie DatabaseClass, dzięki czemu ma dostęp do wspólnego kontekstu bazy danych (db).
    /// </summary>
    public class ProductB : DatabaseClass
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor klasy ProductB przyjmuje instancję kontekstu bazy danych (LogistiQ_Entities)
        /// i przekazuje ją do klasy bazowej DatabaseClass.
        /// Dzięki temu wszystkie metody tej klasy mają dostęp do danych w bazie poprzez właściwość db.
        /// </summary>
        /// <param name="db">Instancja kontekstu bazy danych LogistiQ_Entities.</param>
        public ProductB(LogistiQ_Entities db)
            : base(db) // Przekazujemy instancję kontekstu db do klasy bazowej, co umożliwia korzystanie z tego samego kontekstu bazy danych.
        { }

        #endregion

        #region Funkcje biznesowe

        /// <summary>
        /// Metoda GetProductKeyAndValueItems pobiera listę produktów z bazy danych i mapuje je na obiekty KeyAndValue.
        /// Każdy obiekt KeyAndValue zawiera:
        /// - Key: unikalny identyfikator produktu (ProductID),
        /// - Value: ciąg znaków składający się z nazwy produktu oraz jego typu, oddzielonych spacją.
        /// Wynik zwracany jest jako IQueryable, co umożliwia dalsze operacje zapytań na tym zbiorze.
        /// </summary>
        /// <returns>IQueryable<KeyAndValue> zawierający pary klucz-wartość dla produktów.</returns>
        public IQueryable<KeyAndValue> GetProductKeyAndValueItems()
        {
            // Zapytanie LINQ:
            // 1. Iteruje po encjach z tabeli Products dostępnych w kontekście bazy danych (db.Products).
            // 2. Dla każdego rekordu tworzy obiekt KeyAndValue, gdzie:
            //    - Key ustawiany jest na ProductID,
            //    - Value to złączenie nazwy produktu (Name) i jego typu (Type), oddzielonych spacją.
            // 3. Wynik zapytania jest najpierw konwertowany na listę, a następnie na IQueryable,
            //    co pozwala na dalsze operacje na wynikowym zbiorze danych.
            return
                (
                    from product in db.Products // Iteracja po wszystkich produktach w tabeli Products.
                    select new KeyAndValue      // Dla każdego produktu tworzymy nowy obiekt KeyAndValue.
                    {
                        Key = product.ProductID,               // Ustawienie identyfikatora produktu jako klucza.
                        Value = product.Name + " " + product.Type // Łączenie nazwy produktu i jego typu w jeden ciąg znaków.
                    }
                ).ToList().AsQueryable(); // Konwersja wyników zapytania na listę, a następnie na IQueryable.
        }

        #endregion
    }
}
