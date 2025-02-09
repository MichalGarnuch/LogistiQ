using System;                      // Import przestrzeni nazw, która zawiera podstawowe typy systemowe.
using System.Windows.Input;        // Import przestrzeni nazw zawieraj¹cej interfejs ICommand potrzebny do implementacji komend w WPF.

namespace LogistiQ.Helper        // Deklaracja przestrzeni nazw, w której znajduje siê klasa BaseCommand.
{
    /// <summary>
    /// Klasa `BaseCommand` implementuje interfejs `ICommand`, co umo¿liwia jej u¿ycie
    /// do obs³ugi komend w WPF w architekturze MVVM.
    /// 
    /// Klasa ta pozwala powi¹zaæ logikê biznesow¹ (czyli akcje, które maj¹ zostaæ wykonane)
    /// z elementami interfejsu u¿ytkownika (np. przyciskami). Dodatkowo umo¿liwia okreœlenie
    /// warunku, kiedy dana komenda mo¿e byæ wykonana, co wp³ywa na aktywacjê lub dezaktywacjê
    /// powi¹zanych elementów UI.
    /// </summary>
    internal class BaseCommand : ICommand
    {
        // Pole przechowuj¹ce delegata (metodê) do wykonania. 
        // U¿ywamy typu Action, co oznacza, ¿e metoda nie przyjmuje parametrów i nie zwraca wartoœci.
        private readonly Action _command;

        // Pole przechowuj¹ce delegata okreœlaj¹cego, czy komenda mo¿e zostaæ wykonana.
        // Typ Func<bool> reprezentuje metodê, która zwraca wartoœæ bool (true/false).
        // To pole jest opcjonalne i jeœli nie zostanie przypisane, komenda bêdzie zawsze wykonywalna.
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Konstruktor `BaseCommand` przyjmuje delegata reprezentuj¹cego metodê do wykonania
        /// oraz opcjonalnie delegata okreœlaj¹cego warunek wykonania komendy.
        /// </summary>
        /// <param name="command">
        /// Delegat (metoda) do wykonania po wywo³aniu komendy. 
        /// Jest to wymagana funkcjonalnoœæ komendy.
        /// </param>
        /// <param name="canExecute">
        /// Opcjonalny delegat, który zwraca wartoœæ true, jeœli komenda mo¿e byæ wykonana,
        /// lub false w przeciwnym przypadku. Jeœli nie zostanie podany, komenda bêdzie zawsze wykonywalna.
        /// </param>
        public BaseCommand(Action command, Func<bool> canExecute = null)
        {
            // Sprawdzenie, czy przekazany delegat 'command' nie jest null.
            // Jeœli jest null, rzucany jest wyj¹tek, aby unikn¹æ póŸniejszych b³êdów wykonania.
            if (command == null)
                throw new ArgumentNullException("command");

            // Przypisanie opcjonalnego delegata sprawdzaj¹cego mo¿liwoœæ wykonania komendy.
            _canExecute = canExecute;
            // Przypisanie delegata reprezentuj¹cego metodê, która zostanie wykonana.
            _command = command;
        }

        /// <summary>
        /// Metoda wykonuj¹ca akcjê przypisan¹ do komendy.
        /// 
        /// Jest wywo³ywana, gdy u¿ytkownik aktywuje komendê (np. klikaj¹c przycisk powi¹zany z t¹ komend¹).
        /// Metoda ta wywo³uje delegata `_command`, który zosta³ przekazany przy tworzeniu obiektu.
        /// </summary>
        /// <param name="parameter">
        /// Parametr przekazywany do metody. W tej implementacji parametr nie jest wykorzystywany,
        /// ale jest wymagany przez interfejs ICommand.
        /// </param>
        public void Execute(object parameter)
        {
            // Wywo³anie metody przekazanej jako delegat, realizuj¹cej logikê komendy.
            _command();
        }

        /// <summary>
        /// Metoda sprawdzaj¹ca, czy komenda mo¿e zostaæ wykonana.
        /// 
        /// Jeœli nie zosta³ podany delegat `_canExecute`, metoda domyœlnie zwraca true,
        /// co oznacza, ¿e komenda jest zawsze dostêpna do wykonania.
        /// Jeœli delegat zosta³ podany, metoda wywo³uje go, aby okreœliæ aktualny stan komendy.
        /// </summary>
        /// <param name="parameter">
        /// Parametr przekazywany do metody. W tej implementacji nie jest wykorzystywany,
        /// ale jest wymagany przez interfejs ICommand.
        /// </param>
        /// <returns>
        /// Zwraca true, jeœli komenda mo¿e byæ wykonana, lub false, jeœli nie mo¿e.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            // Jeœli delegat sprawdzaj¹cy mo¿liwoœæ wykonania nie zosta³ okreœlony,
            // zak³adamy, ¿e komenda zawsze mo¿e zostaæ wykonana.
            if (_canExecute == null)
                return true;

            // W przeciwnym wypadku wywo³ujemy delegata, który zwraca informacjê o tym, czy komenda mo¿e byæ wykonana.
            return _canExecute();
        }

        /// <summary>
        /// Zdarzenie, które powinno byæ wywo³ane, gdy zmienia siê mo¿liwoœæ wykonania komendy.
        /// 
        /// W WPF, interfejs u¿ytkownika subskrybuje to zdarzenie, aby automatycznie odœwie¿aæ
        /// stan przycisków lub innych elementów steruj¹cych zwi¹zanych z komend¹.
        /// 
        /// UWAGA: W tej implementacji zdarzenie nie jest wywo³ywane automatycznie.
        /// Jeœli chcemy dynamicznie aktualizowaæ stan elementów UI, musimy rêcznie wywo³aæ:
        /// CommandManager.InvalidateRequerySuggested();
        /// </summary>
        public event EventHandler CanExecuteChanged;
    }
}
