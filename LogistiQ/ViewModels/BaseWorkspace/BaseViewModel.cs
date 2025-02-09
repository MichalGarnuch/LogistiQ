using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Input;
using LogistiQ.Helper;

namespace LogistiQ.ViewModels.BaseWorkspace
{
    /// <summary>
    /// BaseViewModel stanowi podstawow¹ klasê dla wszystkich ViewModeli.
    /// Implementuje interfejs <see cref="INotifyPropertyChanged"/>, dziêki czemu zmiany w³aœciwoœci s¹ komunikowane do widoku.
    /// Dodatkowo, udostêpnia wspólne funkcje dotycz¹ce obs³ugi okna (np. zamykanie, minimalizowanie, maksymalizowanie, przeci¹ganie).
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region DisplayName

        /// <summary>
        /// W³aœciwoœæ <c>DisplayName</c> s³u¿y do przechowywania nazwy wyœwietlanej w interfejsie (np. tytu³u okna).
        /// W³aœciwoœæ jest wirtualna, aby klasy pochodne mog³y j¹ nadpisaæ.
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        #endregion

        #region WindowPropertys

        /// <summary>
        /// Metoda wyœwietla okno komunikatu z podan¹ wiadomoœci¹ b³êdu.
        /// </summary>
        /// <param name="message">Treœæ komunikatu do wyœwietlenia.</param>
        public void ShowMessageBox(string message)
        {
            MessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Komenda zamyka aplikacjê.
        /// </summary>
        public ICommand Close
        {
            get { return new BaseCommand(CloseApplication); }
        }

        /// <summary>
        /// Komenda zmienia stan okna miêdzy maksymalizacj¹ a normalnym rozmiarem.
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
        /// Komenda umo¿liwia przesuwanie okna (przeci¹ganie okna).
        /// </summary>
        public ICommand DragMove
        {
            get { return new BaseCommand(DragMoveCommand); }
        }

        /// <summary>
        /// Komenda restartuje aplikacjê (w tej implementacji wywo³uje jedynie zamkniêcie aplikacji).
        /// </summary>
        public ICommand Restart
        {
            get { return new BaseCommand(RestartCommand); }
        }

        /// <summary>
        /// Zamykamy aplikacjê.
        /// </summary>
        private static void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Restartuje aplikacjê poprzez jej zamkniêcie.
        /// (W rzeczywistej implementacji restartu mo¿na dodaæ logikê ponownego uruchomienia aplikacji.)
        /// </summary>
        private static void RestartCommand()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Rozpoczyna operacjê przeci¹gania g³ównego okna.
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
        /// Minimalizuje okno lub przywraca je, zmieniaj¹c równie¿ jego przezroczystoœæ.
        /// </summary>
        private static void MinimiceApplication()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
            {
                // Przywracamy okno: ustawiamy przezroczystoœæ na 1 i stan na normalny.
                Application.Current.MainWindow.Opacity = 1;
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            else
            {
                // Minimalizujemy okno: ustawiamy przezroczystoœæ na 0 i stan na zminimalizowany.
                Application.Current.MainWindow.Opacity = 0;
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            }
        }

        #endregion

        #region Propertychanged

        /// <summary>
        /// Metoda <c>OnPropertyChanged</c> wywo³uje zdarzenie PropertyChanged, informuj¹c widok o zmianie w³aœciwoœci.
        /// U¿ywa wyra¿enia lambda, aby pobraæ nazwê w³aœciwoœci.
        /// </summary>
        /// <typeparam name="T">Typ w³aœciwoœci.</typeparam>
        /// <param name="action">Wyra¿enie lambda wskazuj¹ce w³aœciwoœæ.</param>
        protected void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Metoda pomocnicza pobiera nazwê w³aœciwoœci z wyra¿enia lambda.
        /// </summary>
        /// <typeparam name="T">Typ w³aœciwoœci.</typeparam>
        /// <param name="action">Wyra¿enie lambda reprezentuj¹ce w³aœciwoœæ.</param>
        /// <returns>Nazwa w³aœciwoœci jako string.</returns>
        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        /// <summary>
        /// Metoda <c>OnPropertyChanged</c> wywo³uje zdarzenie PropertyChanged z podan¹ nazw¹ w³aœciwoœci.
        /// </summary>
        /// <param name="propertyName">Nazwa zmienionej w³aœciwoœci.</param>
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
        /// Zdarzenie wywo³ywane przy zmianie w³aœciwoœci, zgodnie z interfejsem INotifyPropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
