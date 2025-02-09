using System;                        // Import podstawowych typów systemowych, w tym obsługi wyjątków.
using System.Collections.Generic;    // Import przestrzeni nazw dla kolekcji generycznych (choć nie są używane bezpośrednio w tej klasie).
using System.Linq;                   // Import LINQ, umożliwia wykonywanie zapytań na kolekcjach (nie używane w tym kodzie).
using System.Text;                   // Import przestrzeni nazw do operacji na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla operacji asynchronicznych (nie używane w tym kodzie).
using System.Windows.Input;          // Import przestrzeni nazw dla interfejsu ICommand, przydatny w aplikacjach WPF.

namespace LogistiQ.Validators
{
    /// <summary>
    /// Klasa StringValidator zawiera metody walidujące tekst zgodnie z określonymi zasadami.
    /// Jest zadeklarowana jako statyczna, co pozwala na jej użycie bez konieczności tworzenia instancji.
    /// </summary>
    public static class StringValidator
    {
        /// <summary>
        /// Waliduje, czy pierwszy znak przekazanego tekstu jest dużą literą.
        /// Jeśli tekst jest pusty lub null, zwraca komunikat "Nazwa jest polem wymaganym.".
        /// Jeśli pierwszy znak nie jest dużą literą, zwraca komunikat "Rozpocznij dużą literą.".
        /// W przeciwnym razie zwraca pusty string, co oznacza, że walidacja zakończyła się pomyślnie.
        /// W przypadku wystąpienia wyjątku, zwraca wiadomość wyjątku.
        /// </summary>
        /// <param name="text">Tekst, który ma zostać sprawdzony.</param>
        /// <returns>
        /// Pusty string, jeśli pierwszy znak jest dużą literą, lub komunikat o błędzie w przeciwnym wypadku.
        /// </returns>
        public static string ValidateIsFirstLetterUpper(string text)
        {
            try
            {
                // Sprawdzenie, czy tekst jest null lub pusty
                if (string.IsNullOrEmpty(text))
                {
                    return "Nazwa jest polem wymaganym.";
                }

                // Sprawdzenie, czy pierwszy znak tekstu jest dużą literą
                // Metoda char.IsUpper sprawdza, czy znak na danej pozycji jest wielką literą
                return char.IsUpper(text, 0) ? string.Empty : "Rozpocznij dużą literą.";
            }
            catch (Exception ex)
            {
                // W przypadku wystąpienia wyjątku, zwróć jego komunikat
                return ex.Message;
            }
        }

        /// <summary>
        /// Waliduje, czy przekazany tekst nie jest pusty ani null.
        /// Jeśli tekst jest pusty lub null, zwraca komunikat "Pole jest wymagane.".
        /// W przeciwnym razie zwraca pusty string, co oznacza, że walidacja zakończyła się sukcesem.
        /// </summary>
        /// <param name="text">Tekst do sprawdzenia.</param>
        /// <returns>
        /// Pusty string, jeśli tekst nie jest pusty, lub komunikat o błędzie, jeśli jest pusty.
        /// </returns>
        public static string ValidateIsNotEmpty(string text)
        {
            // Sprawdzenie, czy tekst jest null lub pusty
            if (string.IsNullOrEmpty(text))
            {
                return "Pole jest wymagane.";
            }

            // Tekst został podany, więc walidacja zakończyła się pomyślnie
            return string.Empty;
        }
    }
}
