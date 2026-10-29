using System;                      // Import przestrzeni nazw, która zawiera podstawowe typy systemowe.
using System.Windows.Input;        // Import przestrzeni nazw zawierającej interfejs ICommand potrzebny do implementacji komend w WPF.

namespace LogistiQ.Helper        // Deklaracja przestrzeni nazw, w której znajduje się klasa BaseCommand.
{
    /// <summary>
    /// Klasa `BaseCommand` implementuje interfejs `ICommand`, co umożliwia jej użycie
    /// do obsługi komend w WPF w architekturze MVVM.
    ///
    /// Klasa ta pozwala powiązać logikę biznesową (czyli akcje, które mają zostać wykonane)
    /// z elementami interfejsu użytkownika (np. przyciskami). Dodatkowo umożliwia określenie
    /// warunku, kiedy dana komenda może być wykonana, co wpływa na aktywację lub dezaktywację
    /// powiązanych elementów UI.
    /// </summary>
    internal class BaseCommand : ICommand
    {
        // Pole przechowujące delegata (metodę) do wykonania.
        // Używamy typu Action, co oznacza, że metoda nie przyjmuje parametrów i nie zwraca wartości.
        private readonly Action _command;

        // Pole przechowujące delegata określającego, czy komenda może zostać wykonana.
        // Typ Func<bool> reprezentuje metodę, która zwraca wartość bool (true/false).
        // To pole jest opcjonalne i jeśli nie zostanie przypisane, komenda będzie zawsze wykonywalna.
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Konstruktor `BaseCommand` przyjmuje delegata reprezentującego metodę do wykonania
        /// oraz opcjonalnie delegata określającego warunek wykonania komendy.
        /// </summary>
        /// <param name="command">
        /// Delegat (metoda) do wykonania po wywołaniu komendy.
        /// Jest to wymagana funkcjonalność komendy.
        /// </param>
        /// <param name="canExecute">
        /// Opcjonalny delegat, który zwraca wartość true, jeśli komenda może być wykonana,
        /// lub false w przeciwnym przypadku. Jeśli nie zostanie podany, komenda będzie zawsze wykonywalna.
        /// </param>
        public BaseCommand(Action command, Func<bool> canExecute = null)
        {
            // Sprawdzenie, czy przekazany delegat 'command' nie jest null.
            // Jeśli jest null, rzucany jest wyjątek, aby uniknąć późniejszych błędów wykonania.
            if (command == null)
                throw new ArgumentNullException("command");

            // Przypisanie opcjonalnego delegata sprawdzającego możliwość wykonania komendy.
            _canExecute = canExecute;
            // Przypisanie delegata reprezentującego metodę, która zostanie wykonana.
            _command = command;
        }

        /// <summary>
        /// Metoda wykonująca akcję przypisaną do komendy.
        ///
        /// Jest wywoływana, gdy użytkownik aktywuje komendę (np. klikając przycisk powiązany z tą komendą).
        /// Metoda ta wywołuje delegata `_command`, który został przekazany przy tworzeniu obiektu.
        /// </summary>
        /// <param name="parameter">
        /// Parametr przekazywany do metody. W tej implementacji parametr nie jest wykorzystywany,
        /// ale jest wymagany przez interfejs ICommand.
        /// </param>
        public void Execute(object parameter)
        {
            // Wywołanie metody przekazanej jako delegat, realizującej logikę komendy.
            if (CanExecute(parameter)) _command();
        }

        /// <summary>
        /// Metoda sprawdzająca, czy komenda może zostać wykonana.
        ///
        /// Jeśli nie został podany delegat `_canExecute`, metoda domyślnie zwraca true,
        /// co oznacza, że komenda jest zawsze dostępna do wykonania.
        /// Jeśli delegat został podany, metoda wywołuje go, aby określić aktualny stan komendy.
        /// </summary>
        /// <param name="parameter">
        /// Parametr przekazywany do metody. W tej implementacji nie jest wykorzystywany,
        /// ale jest wymagany przez interfejs ICommand.
        /// </param>
        /// <returns>
        /// Zwraca true, jeśli komenda może być wykonana, lub false, jeśli nie może.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            // Jeśli delegat sprawdzający możliwość wykonania nie został określony,
            // zakładamy, że komenda zawsze może zostać wykonana.
            if (_canExecute == null)
                return true;

            // W przeciwnym wypadku wywołujemy delegata, który zwraca informację o tym, czy komenda może być wykonana.
            return _canExecute();
        }

        /// <summary>
        /// Zdarzenie, które powinno być wywołane, gdy zmienia się możliwość wykonania komendy.
        ///
        /// W WPF, interfejs użytkownika subskrybuje to zdarzenie, aby automatycznie odświeżać
        /// stan przycisków lub innych elementów sterujących związanych z komendą.
        ///
        /// Zdarzenie jest przekazywane z CommandManager.RequerySuggested.
        /// Po zmianie stanu poza cyklem wejścia WPF można poprosić o ponowną ocenę:
        /// CommandManager.InvalidateRequerySuggested();
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
