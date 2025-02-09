using LogistiQ.Models.BusinessLogic.BaseWorkspace; // Import klasy bazowej (DatabaseClass), która udostępnia kontekst bazy danych (db).
using LogistiQ.Models.Entities;                      // Import encji bazy danych, np. tabeli Suppliers.
using LogistiQ.Models.EntitiesForView.BaseWorkspace; // Import klasy DTO KeyAndValue, która służy do przekazywania danych w formacie klucz-wartość.
using System;                                      // Import podstawowych typów systemowych.
using System.Collections.Generic;                  // Import kolekcji generycznych, np. List<T>.
using System.Linq;                                 // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.BusinessLogic
{
    /// <summary>
    /// Klasa SupplierB zawiera logikę biznesową dotyczącą operacji na dostawcach.
    /// Dziedziczy po klasie DatabaseClass, dzięki czemu ma dostęp do wspólnego kontekstu bazy danych (db).
    /// </summary>
    public class SupplierB : DatabaseClass
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor klasy SupplierB przyjmuje instancję kontekstu bazy danych (LogistiQ_Entities)
        /// i przekazuje ją do klasy bazowej DatabaseClass.
        /// Dzięki temu wszystkie metody tej klasy mają dostęp do danych bazy poprzez właściwość db.
        /// </summary>
        /// <param name="db">Instancja kontekstu bazy danych LogistiQ_Entities.</param>
        public SupplierB(LogistiQ_Entities db)
            : base(db) { } // Przekazanie instancji bazy danych do klasy bazowej.

        #endregion

        #region Funkcje biznesowe

        /// <summary>
        /// Metoda GetSupplierKeyAndValueItems pobiera listę dostawców i mapuje je na obiekty KeyAndValue.
        /// Każdy obiekt KeyAndValue zawiera:
        /// - Key: unikalny identyfikator dostawcy (SupplierID),
        /// - Value: ciąg znaków łączący nazwę dostawcy oraz jego adres.
        /// Wynik zwracany jest jako IQueryable, co umożliwia dalsze operacje zapytań na tym zbiorze.
        /// </summary>
        /// <returns>IQueryable<KeyAndValue> zawierający pary klucz-wartość dla dostawców.</returns>
        public IQueryable<KeyAndValue> GetSupplierKeyAndValueItems()
        {
            // Zapytanie LINQ:
            // 1. Iteruje po tabeli Suppliers w kontekście bazy danych (db.Suppliers).
            // 2. Dla każdego rekordu tworzy nowy obiekt KeyAndValue, gdzie:
            //    - Key ustawiony jest na SupplierID,
            //    - Value to połączenie nazwy dostawcy i jego adresu.
            // 3. Wynik konwertowany jest na listę, a następnie na IQueryable, co umożliwia dalsze operacje.
            return
                (
                    from supplier in db.Suppliers // Iteracja po wszystkich dostawcach.
                    select new KeyAndValue       // Mapowanie każdej encji dostawcy na obiekt KeyAndValue.
                    {
                        Key = supplier.SupplierID,                     // Ustawienie identyfikatora dostawcy jako klucza.
                        Value = supplier.Name + " " + supplier.Address // Łączenie nazwy dostawcy i adresu w jeden ciąg znaków.
                    }
                ).ToList().AsQueryable(); // Konwersja wyniku zapytania do listy, a następnie do IQueryable.
        }

        #endregion
    }
}
