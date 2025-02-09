using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Input;
using LogistiQ.Helper;

namespace LogistiQ.ViewModels.BaseWorkspace
{
    /// <summary>
    /// BaseViewModel stanowi podstawow� klas� dla wszystkich ViewModeli.
    /// Implementuje interfejs <see cref="INotifyPropertyChanged"/>, dzi�ki czemu zmiany w�a�ciwo�ci s� komunikowane do widoku.
    /// Dodatkowo, udost�pnia wsp�lne funkcje dotycz�ce obs�ugi okna (np. zamykanie, minimalizowanie, maksymalizowanie, przeci�ganie).
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region DisplayName

        /// <summary>
        /// W�a�ciwo�� <c>DisplayName</c> s�u�y do przechowywania nazwy wy�wietlanej w interfejsie (np. tytu�u okna).
        /// W�a�ciwo�� jest wirtualna, aby klasy pochodne mog�y j� nadpisa�.
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        #endregion

        #region WindowPropertys

        /// <summary>
        /// Metoda wy�wietla okno komunikatu z podan� wiadomo�ci� b��du.
        /// </summary>
        /// <param name="message">Tre�� komunikatu do wy�wietlenia.</param>
        public void ShowMessageBox(string message)
        {
            MessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Komenda zamyka aplikacj�.
        /// </summary>
        public ICommand Close
        {
            get { return new BaseCommand(CloseApplication); }
        }

        /// <summary>
        /// Komenda zmienia stan okna mi�dzy maksymalizacj� a normalnym rozmiarem.
        /// </summary>
        public ICommand Maximice
        {
            get { return new BaseCommand(MaximiceApplication); }
        }

        /// <summary>
        /// Komenda minimalizuje lub przywraca okno.
        /// </summary>
        public ICommand Minimice
        {
            get { return new BaseCommand(MinimiceApplication); }
        }

        /// <summary>
        /// Komenda umo�liwia przesuwanie okna (przeci�ganie okna).
        /// </summary>
        public ICommand DragMove
        {
            get { return new BaseCommand(DragMoveCommand); }
        }

        /// <summary>
        /// Komenda restartuje aplikacj� (w tej implementacji wywo�uje jedynie zamkni�cie aplikacji).
        /// </summary>
        public ICommand Restart
        {
            get { return new BaseCommand(RestartCommand); }
        }

        /// <summary>
        /// Zamykamy aplikacj�.
        /// </summary>
        private static void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Restartuje aplikacj� poprzez jej zamkni�cie.
        /// (W rzeczywistej implementacji restartu mo�na doda� logik� ponownego uruchomienia aplikacji.)
        /// </summary>
        private static void RestartCommand()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Rozpoczyna operacj� przeci�gania g��wnego okna.
        /// </summary>
        private static void DragMoveCommand()
        {
            Application.Current.MainWindow.DragMove();
        }

        /// <summary>
        /// Maksymalizuje lub przywraca okno do normalnego rozmiaru.
        /// </summary>
        private static void MaximiceApplication()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            else
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// Minimalizuje okno lub przywraca je, zmieniaj�c r�wnie� jego przezroczysto��.
        /// </summary>
        private static void MinimiceApplication()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
            {
                // Przywracamy okno: ustawiamy przezroczysto�� na 1 i stan na normalny.
                Application.Current.MainWindow.Opacity = 1;
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            else
            {
                // Minimalizujemy okno: ustawiamy przezroczysto�� na 0 i stan na zminimalizowany.
                Application.Current.MainWindow.Opacity = 0;
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            }
        }

        #endregion

        #region Propertychanged

        /// <summary>
        /// Metoda <c>OnPropertyChanged</c> wywo�uje zdarzenie PropertyChanged, informuj�c widok o zmianie w�a�ciwo�ci.
        /// U�ywa wyra�enia lambda, aby pobra� nazw� w�a�ciwo�ci.
        /// </summary>
        /// <typeparam name="T">Typ w�a�ciwo�ci.</typeparam>
        /// <param name="action">Wyra�enie lambda wskazuj�ce w�a�ciwo��.</param>
        protected void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Metoda pomocnicza pobiera nazw� w�a�ciwo�ci z wyra�enia lambda.
        /// </summary>
        /// <typeparam name="T">Typ w�a�ciwo�ci.</typeparam>
        /// <param name="action">Wyra�enie lambda reprezentuj�ce w�a�ciwo��.</param>
        /// <returns>Nazwa w�a�ciwo�ci jako string.</returns>
        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        /// <summary>
        /// Metoda <c>OnPropertyChanged</c> wywo�uje zdarzenie PropertyChanged z podan� nazw� w�a�ciwo�ci.
        /// </summary>
        /// <param name="propertyName">Nazwa zmienionej w�a�ciwo�ci.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        /// <summary>
        /// Zdarzenie wywo�ywane przy zmianie w�a�ciwo�ci, zgodnie z interfejsem INotifyPropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
