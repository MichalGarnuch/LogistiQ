using LogistiQ.Models.BusinessLogic.BaseWorkspace; // Import bazowej klasy logiki biznesowej (DatabaseClass) zapewniającej dostęp do kontekstu bazy danych.
using LogistiQ.Models.Entities;                      // Import encji bazy danych, np. tabeli Orders oraz Customers.
using LogistiQ.Models.EntitiesForView.BaseWorkspace; // Import klasy DTO KeyAndValue używanej do prezentacji danych w formacie klucz-wartość.
using System;                                      // Import podstawowych typów systemowych.
using System.Collections.Generic;                  // Import kolekcji generycznych, takich jak List<T>.
using System.Linq;                                 // Import LINQ umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.BusinessLogic
{
    /// <summary>
    /// Klasa OrderB zawiera logikę biznesową dotyczącą operacji na zamówieniach.
    /// Dziedziczy po klasie DatabaseClass, co zapewnia dostęp do kontekstu bazy danych.
    /// </summary>
    public class OrderB : DatabaseClass
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor klasy OrderB przyjmuje instancję kontekstu bazy danych
        /// i przekazuje ją do klasy bazowej DatabaseClass.
        /// Dzięki temu wszystkie metody tej klasy mają dostęp do danych bazy poprzez właściwość db.
        /// </summary>
        /// <param name="db">Instancja kontekstu bazy danych LogistiQ_Entities.</param>
        public OrderB(LogistiQ_Entities db)
            : base(db) // Przekazujemy instancję db do klasy bazowej, co umożliwia korzystanie z tego samego kontekstu bazy danych.
        { }

        #endregion

        #region Funkcje biznesowe

        /// <summary>
        /// Metoda GetOrderKeyAndValueItems zwraca listę zamówień w formacie KeyAndValue.
        /// Dla każdego zamówienia zwracany jest:
        /// - Key: unikalny identyfikator zamówienia (OrderID),
        /// - Value: ciąg znaków zawierający dane klienta powiązanego z zamówieniem,
        ///          w tym imię, nazwisko, NIP, adres, telefon oraz email.
        /// Wynik zwracany jest jako IQueryable, co umożliwia dalsze operacje zapytań na tym zbiorze.
        /// </summary>
        /// <returns>IQueryable<KeyAndValue> zawierający pary klucz-wartość reprezentujące zamówienia.</returns>
        public IQueryable<KeyAndValue> GetOrderKeyAndValueItems()
        {
            // Zapytanie LINQ:
            // 1. Iteruje po encjach z tabeli Orders w bazie danych (db.Orders).
            // 2. Dla każdego zamówienia tworzy nowy obiekt KeyAndValue, gdzie:
            //    - Key ustawiony jest jako identyfikator zamówienia (OrderID).
            //    - Value jest ciągiem znaków złożonym z danych klienta powiązanego z zamówieniem:
            //      imienia, nazwiska, NIP, adresu, telefonu i emaila, oddzielonych spacjami.
            // 3. Wynik zapytania jest konwertowany do listy, a następnie do IQueryable,
            //    co umożliwia dalsze operacje zapytań na tym zbiorze.
            return
                (
                    from order in db.Orders // Iterujemy po wszystkich zamówieniach.
                    select new KeyAndValue // Dla każdego zamówienia tworzymy obiekt KeyAndValue.
                    {
                        Key = order.OrderID, // Ustawiamy klucz jako unikalny identyfikator zamówienia.
                        // Łączymy dane klienta powiązanego z zamówieniem:
                        // imię, nazwisko, NIP, adres, telefon i email.
                        Value = order.Customers.FirstName + " " +
                                order.Customers.LastName + " " +
                                order.Customers.NIP + " " +
                                order.Customers.Address + " " +
                                order.Customers.Phone + " " +
                                order.Customers.Email
                    }
                ).ToList().AsQueryable(); // Konwersja wyniku zapytania na listę, a następnie na IQueryable.
        }

        #endregion
    }
}
