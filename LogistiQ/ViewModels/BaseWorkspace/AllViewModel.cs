using LogistiQ.Helper;                             // Import pomocniczych klas, m.in. BaseCommand, wykorzystywanych do obsługi komend.
using LogistiQ.Models.Entities;                     // Import encji bazy danych, np. LogistiQ_Entities.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;            // Import kolekcji ObservableCollection, używanej do dynamicznego powiadamiania widoku o zmianach w kolekcji.
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;                        // Import interfejsu ICommand, kluczowego dla implementacji MVVM.
using System.Windows;                              // Import przestrzeni nazw System.Windows, umożliwiający dostęp do Application.Current.

namespace LogistiQ.ViewModels.BaseWorkspace
{
    /// <summary>
    /// Klasa AllViewModel jest abstrakcyjną klasą bazową dla widoków prezentujących listę rekordów.
    /// Typ generyczny T reprezentuje typ elementów wyświetlanych w liście.
    /// Klasa dziedziczy po WorkspaceViewModel i implementuje funkcjonalności takie jak ładowanie danych, sortowanie, filtrowanie oraz dodawanie nowych rekordów.
    /// </summary>
    public abstract class AllViewModel<T> : WorkspaceViewModel
    {
        #region DB
        // Właściwość przechowująca kontekst bazy danych, udostępniany klasom dziedziczącym.
        protected readonly LogistiQ_Entities logistiq_Entities;
        #endregion

        #region LoadCommand
        // Prywatne pole przechowujące instancję komendy LoadCommand.
        private BaseCommand _LoadCommand;

        /// <summary>
        /// Komenda LoadCommand inicjuje proces ładowania danych.
        /// Jeśli komenda nie została jeszcze utworzona, zostaje zainicjowana przy użyciu delegata wywołującego metodę Load().
        /// </summary>
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                    _LoadCommand = new BaseCommand(() => Load());
                return _LoadCommand;
            }
        }

        // Prywatne pole przechowujące instancję komendy AddCommand.
        private BaseCommand _AddCommand;

        /// <summary>
        /// Komenda AddCommand wywołuje metodę AddNew(), która inicjuje proces dodawania nowego rekordu.
        /// </summary>
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                    _AddCommand = new BaseCommand(() => AddNew());
                return _AddCommand;
            }
        }
        #endregion  

        #region List
        // Prywatne pole przechowujące kolekcję rekordów typu T.
        private ObservableCollection<T> _List;

        /// <summary>
        /// Właściwość List przechowuje kolekcję rekordów wyświetlanych w widoku.
        /// Jeśli lista nie została jeszcze załadowana, wywoływana jest metoda Load().
        /// Przy ustawieniu nowej wartości wywoływana jest metoda OnPropertyChanged(), aby powiadomić widok o zmianie.
        /// </summary>
        public ObservableCollection<T> List
        {
            get
            {
                if (_List == null)
                    Load();
                return _List;
            }
            set
            {
                _List = value;
                OnPropertyChanged(() => List);
            }
        }

        // Prywatne pole określające widoczność przycisku dodawania nowych rekordów.
        private bool _isAddVisible = true;

        /// <summary>
        /// Właściwość IsAddVisible kontroluje, czy przycisk dodawania nowego rekordu jest widoczny w widoku.
        /// </summary>
        public bool IsAddVisible
        {
            get => _isAddVisible;
            set
            {
                _isAddVisible = value;
                OnPropertyChanged(() => IsAddVisible);
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Konstruktor klasy AllViewModel inicjalizuje kontekst bazy danych.
        /// Dzięki temu wszystkie klasy dziedziczące mają dostęp do tej samej instancji LogistiQ_Entities.
        /// </summary>
        public AllViewModel()
        {
            logistiq_Entities = new LogistiQ_Entities();
        }
        #endregion

        #region Sort And Find
        // Właściwość przechowująca nazwę pola, według którego ma być wykonane sortowanie.
        public string SortField { get; set; }

        /// <summary>
        /// Właściwość SortComboboxItems zwraca listę elementów do wyboru kryterium sortowania.
        /// Lista ta jest pobierana przez wywołanie abstrakcyjnej metody GetComboboxSortList(), którą musi zaimplementować klasa dziedzicząca.
        /// </summary>
        public List<string> SortComboboxItems
        {
            get
            {
                return GetComboboxSortList();
            }
        }

        /// <summary>
        /// Abstrakcyjna metoda GetComboboxSortList musi zwracać listę kryteriów sortowania jako List<string>.
        /// Implementacja tej metody jest wymagana w klasach dziedziczących.
        /// </summary>
        public abstract List<string> GetComboboxSortList();

        // Prywatne pole przechowujące instancję komendy SortCommand.
        private BaseCommand _SortCommand; // Po naciśnięciu przycisku "Sortuj" (zdefiniowanym w Generic.xaml) zostanie wywołana metoda Sort().

        /// <summary>
        /// Komenda SortCommand wywołuje metodę Sort(), która sortuje dane według wybranego kryterium.
        /// </summary>
        public ICommand SortCommand
        {
            get
            {
                if (_SortCommand == null)
                    _SortCommand = new BaseCommand(() => Sort());
                return _SortCommand;
            }
        }

        /// <summary>
        /// Abstrakcyjna metoda Sort musi zostać zaimplementowana w klasach dziedziczących.
        /// Metoda ta definiuje logikę sortowania danych w widoku.
        /// </summary>
        public abstract void Sort();

        // Właściwość przechowująca nazwę pola, według którego ma być wykonywane filtrowanie (wyszukiwanie).
        public string FindField { get; set; }

        /// <summary>
        /// Właściwość FindComboboxItems zwraca listę elementów do wyboru kryterium wyszukiwania.
        /// Lista ta jest pobierana przez wywołanie abstrakcyjnej metody GetComboboxFindList(), którą musi zaimplementować klasa dziedzicząca.
        /// </summary>
        public List<string> FindComboboxItems
        {
            get
            {
                return GetComboboxFindList();
            }
        }

        /// <summary>
        /// Abstrakcyjna metoda GetComboboxFindList musi zwracać listę kryteriów wyszukiwania jako List<string>.
        /// Implementacja tej metody jest wymagana w klasach dziedziczących.
        /// </summary>
        public abstract List<string> GetComboboxFindList();

        // Właściwość przechowująca tekst wpisany w polu wyszukiwania.
        public string FindTextBox { get; set; }

        // Prywatne pole przechowujące instancję komendy FindCommand.
        private BaseCommand _FindCommand; // Po naciśnięciu przycisku "Szukaj" (zdefiniowanym w Generic.xaml) zostanie wywołana metoda Find().

        /// <summary>
        /// Komenda FindCommand wywołuje metodę Find(), która filtruje dane według wprowadzonego kryterium.
        /// </summary>
        public ICommand FindCommand
        {
            get
            {
                if (_FindCommand == null)
                    _FindCommand = new BaseCommand(() => Find());
                return _FindCommand;
            }
        }

        /// <summary>
        /// Abstrakcyjna metoda Find musi zostać zaimplementowana w klasach dziedziczących.
        /// Metoda ta definiuje logikę wyszukiwania (filtrowania) danych w widoku.
        /// </summary>
        public abstract void Find();
        #endregion

        #region Helpers
        /// <summary>
        /// Abstrakcyjna metoda Load musi być zaimplementowana w klasach dziedziczących.
        /// Metoda ta ładuje dane z bazy do kolekcji List.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Abstrakcyjna metoda CreateNewViewModel musi być zaimplementowana w klasach dziedziczących.
        /// Metoda ta tworzy i zwraca nową instancję widoku (ViewModel) przeznaczonego do dodawania nowych rekordów.
        /// </summary>
        public abstract WorkspaceViewModel CreateNewViewModel();

        /// <summary>
        /// Metoda AddNew inicjuje proces dodawania nowego rekordu.
        /// Tworzy nowy ViewModel przy użyciu metody CreateNewViewModel,
        /// a następnie przekazuje go do głównego widoku (MainWindowViewModel), aby wyświetlić odpowiedni widok.
        /// </summary>
        public void AddNew()
        {
            var newViewModel = CreateNewViewModel();
            var mainWindowViewModel = Application.Current.MainWindow.DataContext as MainWindowViewModel;
            mainWindowViewModel?.CreateView(newViewModel);
        }
        #endregion
    }
}
