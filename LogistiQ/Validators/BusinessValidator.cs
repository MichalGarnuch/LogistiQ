using System;                        // Import podstawowych typów systemowych.
using System.Collections.Generic;    // Import kolekcji generycznych (choć w tej klasie nie są wykorzystywane).
using System.Linq;                   // Import LINQ (nie jest bezpośrednio używany w tym kodzie).
using System.Text;                   // Import operacji na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla operacji asynchronicznych (nie jest używany w tej klasie).

namespace LogistiQ.Validators
{
    /// <summary>
    /// Klasa BusinessValidator zawiera metody walidacyjne, które sprawdzają poprawność danych zgodnie z logiką biznesową.
    /// Jest zadeklarowana jako statyczna, więc jej metody można wywoływać bez tworzenia instancji klasy.
    /// </summary>
    public static class BusinessValidator
    {
        /// <summary>
        /// Waliduje, czy przekazana cena jest dodatnia.
        /// Metoda sprawdza, czy cena została podana oraz czy jest większa od zera.
        /// Jeśli cena nie została podana, zwraca komunikat "Cena jest polem wymaganym.".
        /// Jeśli cena jest mniejsza lub równa 0, zwraca komunikat "Cena powinna być większa od 0.".
        /// W przeciwnym razie zwraca pusty string, co oznacza, że cena jest poprawna.
        /// </summary>
        /// <param name="price">Cena do walidacji jako wartość nullable typu decimal.</param>
        /// <returns>
        /// Komunikat błędu walidacji, jeśli wystąpił, lub pusty string, jeśli walidacja zakończyła się sukcesem.
        /// </returns>
        public static string ValidateIsPricePositive(decimal? price)
        {
            // Sprawdzenie, czy cena ma wartość.
            if (!price.HasValue)
            {
                // Jeśli cena nie została podana, zwróć komunikat o wymaganej cenie.
                return "Cena jest polem wymaganym.";
            }

            // Sprawdzenie, czy cena jest większa od 0.
            if (price <= 0)
            {
                // Jeśli cena jest mniejsza lub równa 0, zwróć komunikat o nieprawidłowej cenie.
                return "Cena powinna być większa od 0.";
            }

            // Jeśli walidacja przebiegła pomyślnie, zwróć pusty string, co oznacza brak błędów.
            return string.Empty;
        }
    }
}
