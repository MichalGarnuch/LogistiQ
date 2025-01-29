using LogistiQ.Helper;
using LogistiQ.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;


namespace LogistiQ.ViewModels.BaseWorkspace
{
    public abstract class AllViewModel<T>:WorkspaceViewModel
    {
        #region DB
        protected readonly LogistiQ_Entities logistiq_Entities;
        #endregion
        #region LoadCommand

        private BaseCommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                    _LoadCommand = new BaseCommand(() => Load());
                return _LoadCommand;
            }
        }

        private BaseCommand _AddCommand;

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
        private ObservableCollection<T> _List;
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
        private bool _isAddVisible = true;
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

        public AllViewModel()
        {
            logistiq_Entities = new LogistiQ_Entities();
        }
        #endregion

        #region Sort And Find
        // do sortowania
        //wynik wyboru poczym sortować zostanie zapisany w SortField
        public string SortField { get; set; }
        public List<string> SortComboboxItems
            { 
                get
                {
                    return GetComboboxSortList();
                }
        }
        public abstract List<string> GetComboboxSortList();

        private BaseCommand _SortCommand; //po naciśnieciu na przysk ortuj po sortowaniu(generic.xaml)
        public ICommand SortCommand
        {
            get
            {
                if (_SortCommand == null)
                    _SortCommand = new BaseCommand(() => Sort());
                return _SortCommand;
            }
        }
        public abstract void Sort();
        //do filtrowania
        public string FindField { get; set; }
        public List<string> FindComboboxItems
        {
            get
            {
                return GetComboboxFindList();
            }
        }
        public abstract List<string> GetComboboxFindList();

        public string FindTextBox { get; set; }

        private BaseCommand _FindCommand; //po naciśnieciu na przysk ortuj szukaj w wyszukiwaniu(generic.xaml)
        public ICommand FindCommand
        {
            get
            {
                if (_FindCommand == null)
                    _FindCommand = new BaseCommand(() => Find());
                return _FindCommand;
            }
        }
        public abstract void Find();
        #endregion

        #region Helpers
        public abstract void Load();

        // Metoda abstrakcyjna - każda klasa pochodna musi zaimplementować
        public abstract WorkspaceViewModel CreateNewViewModel();

        // Ogólna metoda AddNew
        public void AddNew()
        {
            var newViewModel = CreateNewViewModel();
            var mainWindowViewModel = Application.Current.MainWindow.DataContext as MainWindowViewModel;
            mainWindowViewModel?.CreateView(newViewModel);
        }
        #endregion
    }
}
