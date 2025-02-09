using LogistiQ.Models.Entities; // Import przestrzeni nazw zawierającej klasy encji (model danych) bazy danych.
using System;                   // Import podstawowych klas systemowych, np. Exception.
using System.Collections.Generic; // Import dla kolekcji generycznych, choć w tym pliku nie są bezpośrednio wykorzystywane.
using System.Linq;              // Import dla języka LINQ, umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;              // Import umożliwiający operacje na tekście (np. StringBuilder).
using System.Threading.Tasks;   // Import dla obsługi operacji asynchronicznych.

namespace LogistiQ.Models.BusinessLogic.BaseWorkspace
{
    /// <summary>
    /// Klasa `DatabaseClass` działa jako warstwa dostępu do bazy danych w ramach logiki biznesowej.
    /// Umożliwia przekazywanie kontekstu bazy danych do innych klas.
    /// 
    /// Jest to klasa bazowa, co oznacza, że inne klasy logiki biznesowej mogą po niej dziedziczyć,
    /// dzięki czemu mają dostęp do obiektu `db` bez potrzeby jego ponownej inicjalizacji.
    /// </summary>
    public class DatabaseClass
    {
        #region Context

        /// <summary>
        /// Obiekt `db` przechowuje instancję kontekstu bazy danych `LogistiQ_Entities`.
        /// Dzięki temu klasa ma dostęp do tabel i zapytań LINQ na danych w bazie.
        /// </summary>
        public LogistiQ_Entities db { get; set; } // Właściwość udostępniająca kontekst bazy danych dla operacji na danych.

        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktor klasy `DatabaseClass` przyjmuje instancję `LogistiQ_Entities`
        /// i przypisuje ją do właściwości `db`, co umożliwia dostęp do bazy danych.
        /// 
        /// Dzięki temu każda klasa dziedzicząca po `DatabaseClass` korzysta z tego samego kontekstu,
        /// co zapobiega niepotrzebnemu tworzeniu nowych połączeń z bazą.
        /// </summary>
        /// <param name="db">Instancja kontekstu bazy danych, przekazywana do klasy.</param>
        public DatabaseClass(LogistiQ_Entities db)
        {
            this.db = db; // Przypisanie przekazanego kontekstu bazy danych do właściwości `db`
        }

        #endregion
    }
}
