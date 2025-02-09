using LogistiQ.Models.BusinessLogic.BaseWorkspace; // Import klasy bazowej dla logiki biznesowej, która zapewnia dostęp do kontekstu bazy danych.
using LogistiQ.Models.Entities;                      // Import encji bazy danych, np. tabeli Customers.
using LogistiQ.Models.EntitiesForView.BaseWorkspace; // Import encji widokowych (DTO), np. klasy KeyAndValue, służącej do prezentacji danych.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.BusinessLogic
{
    /// <summary>
    /// Klasa CustomerB obsługuje logikę biznesową dotyczącą klientów.
    /// Dziedziczy po klasie DatabaseClass, dzięki czemu ma dostęp do kontekstu bazy danych (db).
    /// </summary>
    public class CustomerB : DatabaseClass
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor klasy CustomerB przyjmuje instancję kontekstu bazy danych
        /// i przekazuje ją do klasy bazowej DatabaseClass.
        /// Dzięki temu wszystkie metody tej klasy mają dostęp do danych w bazie poprzez właściwość db.
        /// </summary>
        /// <param name="db">Instancja kontekstu bazy danych LogistiQ_Entities.</param>
        public CustomerB(LogistiQ_Entities db)
            : base(db) // Przekazanie kontekstu bazy danych do klasy bazowej, co umożliwia korzystanie z tego samego obiektu db.
        {
            // Konstruktor nie zawiera dodatkowej logiki.
        }

        #endregion

        #region Funkcje biznesowe

        /// <summary>
        /// Metoda GetCustomerKeyAndValueItems pobiera dane klientów i mapuje je na obiekty KeyAndValue.
        /// Każdy obiekt KeyAndValue zawiera:
        /// - Key: identyfikator klienta,
        /// - Value: ciąg znaków zawierający imię, nazwisko oraz NIP klienta.
        /// Wynik zwracany jest jako IQueryable, co umożliwia dalsze operacje zapytań na tym zbiorze.
        /// </summary>
        /// <returns>
        /// IQueryable<KeyAndValue> zawierający pary klucz-wartość dla klientów.
        /// </returns>
        public IQueryable<KeyAndValue> GetCustomerKeyAndValueItems()
        {
            // Zapytanie LINQ, które:
            // 1. Iteruje po encjach z tabeli Customers w kontekście bazy danych.
            // 2. Dla każdego rekordu tworzy obiekt KeyAndValue,
            //    gdzie kluczem jest CustomerID, a wartością połączone imię, nazwisko i NIP.
            // 3. Wynik zapytania konwertowany jest najpierw do listy, a następnie na IQueryable,
            //    co umożliwia dalsze operacje zapytań na tym zbiorze danych.
            return
                (
                    from customer in db.Customers // Iteracja po tabeli Customers.
                    select new KeyAndValue       // Tworzenie nowego obiektu KeyAndValue dla każdego klienta.
                    {
                        Key = customer.CustomerID, // Ustawienie identyfikatora klienta jako klucza.
                        Value = customer.FirstName + " " + customer.LastName + " " + customer.NIP // Łączenie imienia, nazwiska i NIP w jeden ciąg znaków.
                    }
                ).ToList().AsQueryable(); // Konwersja wyniku zapytania do listy, a następnie na IQueryable.
        }

        #endregion
    }
}
