// Importujemy przestrzenie nazw – to biblioteki, które udostępniają gotowe funkcje i klasy.
// Dzięki nim nie musimy pisać podstawowych mechanizmów od zera.
using LogistiQ.Models.Entities; // Importujemy model encji z bazy danych, np. `Categories`.
using LogistiQ.Validators; // Importujemy walidator tekstów (`StringValidator`).
using LogistiQ.ViewModels.BaseWorkspace; // Klasy bazowe dla ViewModeli.
using LogistiQ.Views.BaseWorkspace; // Widoki bazowe, np. `SingleRecordViewBase`.
using System; // Podstawowe funkcje systemowe.
using System.Collections.Generic; // Obsługa kolekcji, np. list i słowników.
using System.Linq; // Funkcje LINQ do pracy z kolekcjami (np. filtrowanie danych).
using System.Text; // Manipulacja tekstami.
using System.Threading.Tasks; // Zadania asynchroniczne.

namespace LogistiQ.ViewModels.Categories // Deklaracja przestrzeni nazw (namespace) dla naszego ViewModelu.
{
    // Definiujemy klasę `NewCategoryViewModel`, która dziedziczy z klasy bazowej `SingleRecordViewModel`.
    // `SingleRecordViewModel` prawdopodobnie zawiera logikę wspólną dla zarządzania pojedynczym rekordem danych.
    public class NewCategoryViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Categories>
    {
        // #region – grupuje kod w sekcje, ułatwiając czytelność.
        #region Konstruktor

        // Konstruktor to specjalna metoda, która uruchamia się automatycznie podczas tworzenia obiektu tej klasy.
        public NewCategoryViewModel()
            : base("Categories") // Wywołanie konstruktora klasy bazowej z parametrem "Categories".
        {
            // Tworzymy nowy obiekt encji `Categories`, który będziemy edytować lub dodawać.
            item = new Models.Entities.Categories();
        }

        #endregion

        #region Properties

        // Właściwości (Properties) – to coś w rodzaju pól w klasie, które możemy odczytywać i ustawiać.

        // Property dla ID kategorii.
        public int CategoryID
        {
            get
            {
                // Zwracamy wartość pola `CategoryID` z obiektu encji (np. z bazy danych).
                return item.CategoryID;
            }
            set
            {
                // Ustawiamy wartość pola `CategoryID` i informujemy UI o zmianie.
                item.CategoryID = value;
                OnPropertyChanged(() => CategoryID); // Powiadomienie UI o zmianie wartości (binding działa automatycznie).
            }
        }

        // Property dla nazwy kategorii.
        public string Name
        {
            get
            {
                // Pobieramy nazwę kategorii.
                return item.Name;
            }
            set
            {
                // Ustawiamy nazwę kategorii i powiadamiamy UI.
                item.Name = value;
                OnPropertyChanged(() => Name);
            }
        }

        // Property dla opisu kategorii.
        public string Description
        {
            get
            {
                // Pobieramy opis kategorii.
                return item.Description;
            }
            set
            {
                // Ustawiamy opis kategorii i powiadamiamy UI.
                item.Description = value;
                OnPropertyChanged(() => Description);
            }
        }

        #endregion

        #region Helpers

        // Nadpisujemy metodę `Save`, która jest zdefiniowana w klasie bazowej.
        public override void Save()
        {
            // Dodajemy aktualny obiekt `item` (kategorię) do lokalnej kolekcji w bazie danych.
            logistiQ_Entities.Categories.Add(item);

            // Zapisujemy wszystkie zmiany w bazie danych.
            logistiQ_Entities.SaveChanges();
        }

        #endregion

        #region Validation

        // Właściwość `Error` jest wymagana przez interfejs `IDataErrorInfo` (obsługa walidacji danych w UI).
        public string Error => string.Empty;

        // Słownik przechowujący komunikaty walidacji dla poszczególnych właściwości.
        private readonly Dictionary<string, string> _validationMessages = new Dictionary<string, string>();

        // Walidacja dla pojedynczych właściwości – metoda uruchamiana automatycznie podczas zmiany danych w UI.
        public string this[string properties]
        {
            get
            {
                // Tworzymy zmienną, która przechowa komunikat błędu.
                string validateMessage = string.Empty;

                // Jeśli sprawdzamy pole `Name` (nazwę kategorii)...
                if (properties == nameof(Name))
                {
                    // Używamy walidatora, aby sprawdzić, czy nazwa zaczyna się od dużej litery.
                    validateMessage = StringValidator.ValidateIsFirstLetterUpper(Name);
                }

                // Jeśli walidator zwróci komunikat błędu...
                if (!string.IsNullOrEmpty(validateMessage))
                {
                    // Dodajemy komunikat do słownika błędów.
                    _validationMessages[properties] = validateMessage;
                }
                else
                {
                    // Jeśli nie ma błędu, usuwamy wpis ze słownika.
                    _validationMessages.Remove(properties);
                }

                // Zwracamy komunikat błędu (lub pusty string, jeśli walidacja przeszła pomyślnie).
                return validateMessage;
            }
        }

        // Metoda sprawdzająca, czy wszystkie pola są poprawne.
        public override bool IsValid()
        {
            // Jeśli słownik błędów `_validationMessages` jest pusty, dane są poprawne.
            return !_validationMessages.Any();
        }

        #endregion
    }
}
