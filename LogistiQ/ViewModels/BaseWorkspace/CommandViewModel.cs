using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace LogistiQ.ViewModels.BaseWorkspace
{
    /// <summary>
    /// CommandViewModel encapsuluje komendę wraz z jej nazwą wyświetlaną.
    /// Klasa ta dziedziczy po <see cref="BaseViewModel"/>, dzięki czemu wspiera powiadamianie widoku o zmianach właściwości.
    /// Jest używana głównie do wiązania poleceń (np. przycisków, pozycji menu) z logiką biznesową.
    /// </summary>
    public class CommandViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// Pobiera komendę, która zostanie wykonana po aktywacji przez widok.
        /// </summary>
        public ICommand Command { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="CommandViewModel"/> z określoną nazwą wyświetlaną oraz komendą.
        /// </summary>
        /// <param name="displayName">Nazwa wyświetlana w interfejsie użytkownika, opisująca akcję.</param>
        /// <param name="command">Komenda, która zostanie wykonana przy aktywacji. Nie może być nullem.</param>
        /// <exception cref="ArgumentNullException">Wyrzuca wyjątek, gdy parametr <paramref name="command"/> jest nullem.</exception>
        public CommandViewModel(string displayName, ICommand command)
        {
            // Sprawdzenie, czy przekazana komenda nie jest nullem.
            if (command == null)
                throw new ArgumentNullException("command");

            // Ustawienie nazwy wyświetlanej (DisplayName) oraz przypisanie komendy.
            this.DisplayName = displayName;
            this.Command = command;
        }

        #endregion
    }
}
