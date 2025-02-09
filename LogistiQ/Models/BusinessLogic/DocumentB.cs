using LogistiQ.Models.BusinessLogic.BaseWorkspace; // Import klasy bazowej DatabaseClass, która udostępnia kontekst bazy danych (db).
using LogistiQ.Models.Entities;                      // Import encji bazy danych, w tym przypadku klasy Document.
using LogistiQ.Models.EntitiesForView.BaseWorkspace; // Import klasy DTO KeyAndValue, używanej do prezentacji danych w formacie klucz-wartość.
using System;
using System.Collections.Generic;
using System.Linq;                                   // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.BusinessLogic
{
    // Klasa DocumentB zawiera logikę biznesową dotyczącą dokumentów.
    // Dziedziczy po klasie DatabaseClass, dzięki czemu ma dostęp do wspólnego kontekstu bazy danych (db).
    public class DocumentB : DatabaseClass
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor klasy DocumentB.
        /// Przyjmuje instancję kontekstu bazy danych (LogistiQ_Entities) i przekazuje ją do klasy bazowej DatabaseClass.
        /// Dzięki temu wszystkie metody w tej klasie mają dostęp do danych w bazie poprzez właściwość db.
        /// </summary>
        /// <param name="db">Instancja kontekstu bazy danych LogistiQ_Entities.</param>
        public DocumentB(LogistiQ_Entities db)
            : base(db) // Przekazanie instancji db do klasy bazowej, co umożliwia korzystanie z tego samego kontekstu w całej klasie.
        { }

        #endregion

        #region Funkcje biznesowe

        /// <summary>
        /// Metoda GetDocumentKeyAndValueItems pobiera listę dokumentów i mapuje je na obiekty KeyAndValue.
        /// Każdy obiekt KeyAndValue zawiera:
        /// - Key: identyfikator dokumentu (DocumentID),
        /// - Value: ciąg znaków składający się z typu dokumentu oraz numeru dokumentu.
        /// Wynik zwracany jest jako IQueryable, co umożliwia dalsze operacje zapytań.
        /// </summary>
        /// <returns>IQueryable<KeyAndValue> zawierający pary klucz-wartość dla dokumentów.</returns>
        public IQueryable<KeyAndValue> GetDocumentKeyAndValueItems()
        {
            // Zapytanie LINQ:
            // 1. Iterujemy po encjach z tabeli Documents w kontekście bazy danych (db.Documents).
            // 2. Dla każdego dokumentu tworzymy nowy obiekt KeyAndValue, gdzie:
            //    - Key ustawiamy jako document.DocumentID,
            //    - Value tworzymy jako złączenie typu dokumentu i numeru dokumentu oddzielonych spacją.
            // 3. Wynik zapytania konwertujemy na listę, a następnie na IQueryable, co umożliwia dalsze operacje na zbiorze danych.
            return
                (
                    from document in db.Documents // Iteracja po tabeli Documents.
                    select new KeyAndValue       // Mapowanie każdej encji dokumentu na obiekt KeyAndValue.
                    {
                        Key = document.DocumentID,                      // Ustawienie identyfikatora dokumentu jako klucza.
                        Value = document.Type + " " + document.DocumentNumber // Łączenie typu dokumentu i numeru dokumentu w jeden string.
                    }
                ).ToList().AsQueryable(); // Konwersja wyniku zapytania na listę, a następnie na IQueryable.
        }

        #endregion
    }
}
