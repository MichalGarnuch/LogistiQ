using System;                      // Import przestrzeni nazw, kt�ra zawiera podstawowe typy systemowe.
using System.Windows.Input;        // Import przestrzeni nazw zawieraj�cej interfejs ICommand potrzebny do implementacji komend w WPF.

namespace LogistiQ.Helper        // Deklaracja przestrzeni nazw, w kt�rej znajduje si� klasa BaseCommand.
{
    /// <summary>
    /// Klasa `BaseCommand` implementuje interfejs `ICommand`, co umo�liwia jej u�ycie
    /// do obs�ugi komend w WPF w architekturze MVVM.
    /// 
    /// Klasa ta pozwala powi�za� logik� biznesow� (czyli akcje, kt�re maj� zosta� wykonane)
    /// z elementami interfejsu u�ytkownika (np. przyciskami). Dodatkowo umo�liwia okre�lenie
    /// warunku, kiedy dana komenda mo�e by� wykonana, co wp�ywa na aktywacj� lub dezaktywacj�
    /// powi�zanych element�w UI.
    /// </summary>
    internal class BaseCommand : ICommand
    {
        // Pole przechowuj�ce delegata (metod�) do wykonania. 
        // U�ywamy typu Action, co oznacza, �e metoda nie przyjmuje parametr�w i nie zwraca warto�ci.
        private readonly Action _command;

        // Pole przechowuj�ce delegata okre�laj�cego, czy komenda mo�e zosta� wykonana.
        // Typ Func<bool> reprezentuje metod�, kt�ra zwraca warto�� bool (true/false).
        // To pole jest opcjonalne i je�li nie zostanie przypisane, komenda b�dzie zawsze wykonywalna.
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Konstruktor `BaseCommand` przyjmuje delegata reprezentuj�cego metod� do wykonania
        /// oraz opcjonalnie delegata okre�laj�cego warunek wykonania komendy.
        /// </summary>
        /// <param name="command">
        /// Delegat (metoda) do wykonania po wywo�aniu komendy. 
        /// Jest to wymagana funkcjonalno�� komendy.
        /// </param>
        /// <param name="canExecute">
        /// Opcjonalny delegat, kt�ry zwraca warto�� true, je�li komenda mo�e by� wykonana,
        /// lub false w przeciwnym przypadku. Je�li nie zostanie podany, komenda b�dzie zawsze wykonywalna.
        /// </param>
        public BaseCommand(Action command, Func<bool> canExecute = null)
        {
            // Sprawdzenie, czy przekazany delegat 'command' nie jest null.
            // Je�li jest null, rzucany jest wyj�tek, aby unikn�� p�niejszych b��d�w wykonania.
            if (command == null)
                throw new ArgumentNullException("command");

            // Przypisanie opcjonalnego delegata sprawdzaj�cego mo�liwo�� wykonania komendy.
            _canExecute = canExecute;
            // Przypisanie delegata reprezentuj�cego metod�, kt�ra zostanie wykonana.
            _command = command;
        }

        /// <summary>
        /// Metoda wykonuj�ca akcj� przypisan� do komendy.
        /// 
        /// Jest wywo�ywana, gdy u�ytkownik aktywuje komend� (np. klikaj�c przycisk powi�zany z t� komend�).
        /// Metoda ta wywo�uje delegata `_command`, kt�ry zosta� przekazany przy tworzeniu obiektu.
        /// </summary>
        /// <param name="parameter">
        /// Parametr przekazywany do metody. W tej implementacji parametr nie jest wykorzystywany,
        /// ale jest wymagany przez interfejs ICommand.
        /// </param>
        public void Execute(object parameter)
        {
            // Wywo�anie metody przekazanej jako delegat, realizuj�cej logik� komendy.
            _command();
        }

        /// <summary>
        /// Metoda sprawdzaj�ca, czy komenda mo�e zosta� wykonana.
        /// 
        /// Je�li nie zosta� podany delegat `_canExecute`, metoda domy�lnie zwraca true,
        /// co oznacza, �e komenda jest zawsze dost�pna do wykonania.
        /// Je�li delegat zosta� podany, metoda wywo�uje go, aby okre�li� aktualny stan komendy.
        /// </summary>
        /// <param name="parameter">
        /// Parametr przekazywany do metody. W tej implementacji nie jest wykorzystywany,
        /// ale jest wymagany przez interfejs ICommand.
        /// </param>
        /// <returns>
        /// Zwraca true, je�li komenda mo�e by� wykonana, lub false, je�li nie mo�e.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            // Je�li delegat sprawdzaj�cy mo�liwo�� wykonania nie zosta� okre�lony,
            // zak�adamy, �e komenda zawsze mo�e zosta� wykonana.
            if (_canExecute == null)
                return true;

            // W przeciwnym wypadku wywo�ujemy delegata, kt�ry zwraca informacj� o tym, czy komenda mo�e by� wykonana.
            return _canExecute();
        }

        /// <summary>
        /// Zdarzenie, kt�re powinno by� wywo�ane, gdy zmienia si� mo�liwo�� wykonania komendy.
        /// 
        /// W WPF, interfejs u�ytkownika subskrybuje to zdarzenie, aby automatycznie od�wie�a�
        /// stan przycisk�w lub innych element�w steruj�cych zwi�zanych z komend�.
        /// 
        /// UWAGA: W tej implementacji zdarzenie nie jest wywo�ywane automatycznie.
        /// Je�li chcemy dynamicznie aktualizowa� stan element�w UI, musimy r�cznie wywo�a�:
        /// CommandManager.InvalidateRequerySuggested();
        /// </summary>
        public event EventHandler CanExecuteChanged;
    }
}
