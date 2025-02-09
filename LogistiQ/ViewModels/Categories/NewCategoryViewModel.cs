using LogistiQ.Models.Entities;                // Importuje definicje encji, w tym encję Categories.
using LogistiQ.Validators;                     // Importuje klasy walidacyjne (np. StringValidator).
using LogistiQ.ViewModels.BaseWorkspace;       // Importuje klasę bazową SingleRecordViewModel, która jest częścią architektury MVVM.
using LogistiQ.Views.BaseWorkspace;            // Import przestrzeni nazw zawierającej widoki bazowe (używane przy tworzeniu widoku, jeśli potrzeba).
using System;
using System.Collections.Generic;            // Import kolekcji generycznych, np. Dictionary.
using System.ComponentModel;                   // Import interfejsu IDataErrorInfo, wykorzystywanego do walidacji.
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.ViewModels.Categories
{
    /// <summary>
    /// NewCategoryViewModel odpowiada za tworzenie nowej kategorii.
    /// Dziedziczy po <see cref="SingleRecordViewModel{T}"/> z typem <see cref="LogistiQ.Models.Entities.Categories"/>
    /// oraz implementuje interfejs IDataErrorInfo, który umożliwia walidację danych w widoku.
    /// </summary>
    public class NewCategoryViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Categories>, IDataErrorInfo
    {
        /// <summary>
        /// Konstruktor inicjalizuje nowy obiekt kategorii oraz ustawia nazwę widoku.
        /// </summary>
        public NewCategoryViewModel()
            : base("Categories")  // Wywołanie konstruktora klasy bazowej z parametrem "Categories" jako nazwą wyświetlaną.
        {
            // Inicjalizacja obiektu encji Categories, który będzie modyfikowany w widoku.
            item = new Models.Entities.Categories();
        }

        /// <summary>
        /// Właściwość CategoryID umożliwia dostęp do identyfikatora kategorii.
        /// </summary>
        public int CategoryID
        {
            get
            {
                return item.CategoryID;  // Zwraca identyfikator z obiektu encji.
            }
            set
            {
                item.CategoryID = value;  // Ustawia identyfikator w obiekcie encji.
                OnPropertyChanged(() => CategoryID);  // Powiadamia widok o zmianie właściwości.
            }
        }

        /// <summary>
        /// Właściwość Name umożliwia dostęp do nazwy kategorii.
        /// </summary>
        public string Name
        {
            get
            {
                return item.Name;  // Zwraca nazwę kategorii.
            }
            set
            {
                item.Name = value;  // Ustawia nazwę w obiekcie encji.
                OnPropertyChanged(() => Name);  // Powiadamia widok o zmianie właściwości.
            }
        }

        /// <summary>
        /// Właściwość Description umożliwia dostęp do opisu kategorii.
        /// </summary>
        public string Description
        {
            get
            {
                return item.Description;  // Zwraca opis kategorii.
            }
            set
            {
                item.Description = value;  // Ustawia opis w obiekcie encji.
                OnPropertyChanged(() => Description);  // Powiadamia widok o zmianie właściwości.
            }
        }

        /// <summary>
        /// Metoda Save zapisuje nową kategorię do bazy danych.
        /// Dodaje obiekt kategorii do kolekcji Categories w kontekście bazy i wywołuje SaveChanges().
        /// </summary>
        public override void Save()
        {
            logistiQ_Entities.Categories.Add(item);  // Dodaje nową kategorię do kontekstu bazy danych.
            logistiQ_Entities.SaveChanges();           // Zapisuje zmiany w bazie danych.
        }

        #region Validation

        /// <summary>
        /// Właściwość Error implementowana z interfejsu IDataErrorInfo.
        /// W tej implementacji zwraca pusty ciąg, ponieważ szczegółowa walidacja odbywa się dla poszczególnych właściwości.
        /// </summary>
        public string Error => string.Empty;

        // Prywatny słownik przechowujący komunikaty walidacyjne dla poszczególnych właściwości.
        private readonly Dictionary<string, string> _validationMessages = new Dictionary<string, string>();

        /// <summary>
        /// Indeksator umożliwia walidację właściwości na bieżąco.
        /// Dla właściwości "Name" sprawdzana jest walidacja, czy pierwszy znak jest dużą literą.
        /// Wynik walidacji (komunikat błędu) jest przechowywany w słowniku _validationMessages.
        /// </summary>
        /// <param name="properties">Nazwa właściwości, która ma być walidowana.</param>
        /// <returns>Komunikat o błędzie walidacji lub pusty ciąg, jeśli walidacja jest poprawna.</returns>
        public string this[string properties]
        {
            get
            {
                string validateMessage = string.Empty;

                // Walidacja właściwości "Name" za pomocą StringValidator
                if (properties == nameof(Name))
                {
                    validateMessage = StringValidator.ValidateIsFirstLetterUpper(Name);
                }

                // Jeśli pojawił się komunikat błędu, zapisujemy go w słowniku walidacyjnym
                if (!string.IsNullOrEmpty(validateMessage))
                {
                    _validationMessages[properties] = validateMessage;
                }
                else
                {
                    // Jeśli walidacja jest poprawna, usuwamy ewentualne wcześniejsze komunikaty dla tej właściwości
                    _validationMessages.Remove(properties);
                }

                return validateMessage;
            }
        }

        /// <summary>
        /// Metoda IsValid zwraca true, jeśli nie ma żadnych błędów walidacyjnych,
        /// czyli słownik _validationMessages jest pusty.
        /// </summary>
        /// <returns>True, jeśli dane są poprawne; w przeciwnym razie false.</returns>
        public override bool IsValid()
        {
            return !_validationMessages.Any();
        }
        #endregion
    }
}
