using LogistiQ.Models.BusinessLogic.BaseWorkspace; // Import przestrzeni nazw zawierającej bazowe klasy logiki biznesowej, m.in. DatabaseClass.
using LogistiQ.Models.Entities; // Import przestrzeni nazw z encjami bazy danych, np. Category.
using LogistiQ.Models.EntitiesForView.BaseWorkspace; // Import przestrzeni nazw zawierającej klasy DTO (Data Transfer Object), np. KeyAndValue, używane do prezentacji danych w widoku.
using System; // Import podstawowych typów i funkcji systemowych.
using System.Collections.Generic; // Import kolekcji generycznych, takich jak List<T>.
using System.Linq; // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach danych.
using System.Text; // Import przestrzeni nazw do operacji na tekście (opcjonalne, ale często używane).
using System.Threading.Tasks; // Import przestrzeni nazw dla obsługi asynchronicznych operacji (opcjonalne).

namespace LogistiQ.Models.BusinessLogic // Definicja przestrzeni nazw dla logiki biznesowej w aplikacji.
{
    // Klasa CategoryB służy do obsługi logiki biznesowej związanej z kategoriami produktów.
    // Dziedziczy po klasie DatabaseClass, co zapewnia dostęp do wspólnego kontekstu bazy danych (db).
    public class CategoryB : DatabaseClass
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor klasy CategoryB przyjmuje instancję kontekstu bazy danych i przekazuje ją do klasy bazowej DatabaseClass.
        /// Dzięki temu wszystkie metody tej klasy mają dostęp do danych w bazie poprzez właściwość db.
        /// </summary>
        /// <param name="db">Instancja kontekstu bazy danych LogistiQ_Entities.</param>
        public CategoryB(LogistiQ_Entities db)
            : base(db) // Przekazujemy instancję db do konstruktora klasy bazowej, aby umożliwić korzystanie z tego samego kontekstu.
        {
            // Konstruktor nie wymaga dodatkowej logiki, gdyż inicjalizacja odbywa się w klasie bazowej.
        }

        #endregion

        #region Funkcje biznesowe

        /// <summary>
        /// Metoda GetCategoryKeyAndValueItems zwraca kolekcję par klucz-wartość reprezentujących kategorie.
        /// Każdy obiekt KeyAndValue zawiera:
        /// - Key: unikalny identyfikator kategorii (CategoryID),
        /// - Value: nazwę kategorii.
        /// Wynik zwracany jest jako IQueryable, co umożliwia dalsze operacje zapytań.
        /// </summary>
        /// <returns>IQueryable<KeyAndValue> zawierający dane kategorii.</returns>
        public IQueryable<KeyAndValue> GetCategoryKeyAndValueItems()
        {
            // Zapytanie LINQ:
            // 1. Iterujemy po encjach z tabeli Categories w kontekście bazy danych.
            // 2. Dla każdej encji tworzymy nowy obiekt KeyAndValue z odpowiednio przypisanymi polami.
            // 3. Wynik konwertujemy najpierw na listę, a następnie z powrotem na IQueryable,
            //    co umożliwia dalsze operacje zapytań na wynikowym zbiorze danych.
            return
                (
                    from category in db.Categories // Przechodzimy przez wszystkie rekordy w tabeli Categories.
                    select new KeyAndValue       // Dla każdej kategorii tworzymy obiekt KeyAndValue.
                    {
                        Key = category.CategoryID, // Ustawiamy pole Key jako unikalny identyfikator kategorii.
                        Value = category.Name      // Ustawiamy pole Value jako nazwę kategorii.
                    }
                ).ToList().AsQueryable(); // Konwersja wyniku zapytania do listy, a następnie do IQueryable.
        }

        #endregion
    }
}
