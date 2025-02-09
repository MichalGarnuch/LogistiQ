using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogistiQ.Helper;
using System.Windows.Input;

namespace LogistiQ.ViewModels.BaseWorkspace
{
    /// <summary>
    /// WorkspaceViewModel stanowi abstrakcyjną bazę dla wszystkich widoków roboczych (workspace).
    /// Dziedziczy po BaseViewModel, co umożliwia implementację mechanizmu powiadamiania o zmianie właściwości.
    /// Dodatkowo, udostępnia mechanizm zamykania widoku poprzez komendę CloseCommand i zdarzenie RequestClose.
    /// </summary>
    public abstract class WorkspaceViewModel : BaseViewModel
    {
        #region Fields
        // Prywatne pole przechowujące instancję komendy zamknięcia.
        private BaseCommand _CloseCommand;
        #endregion 

        #region Constructor
        /// <summary>
        /// Konstruktor domyślny WorkspaceViewModel.
        /// Może być rozszerzony przez klasy pochodne do inicjalizacji specyficznych zasobów.
        /// </summary>
        public WorkspaceViewModel()
        {
            // Konstruktor nie zawiera dodatkowej logiki.
        }
        #endregion 

        #region CloseCommand
        /// <summary>
        /// Komenda CloseCommand umożliwia zamknięcie bieżącego widoku roboczego.
        /// Przy pierwszym wywołaniu, instancja komendy jest tworzona i powiązana z metodą OnRequestClose.
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                    _CloseCommand = new BaseCommand(() => this.OnRequestClose());
                return _CloseCommand;
            }
        }
        #endregion 

        #region RequestClose [event]
        /// <summary>
        /// Zdarzenie RequestClose jest wywoływane, gdy widok roboczy powinien zostać zamknięty.
        /// Subskrybenci tego zdarzenia mogą podjąć odpowiednie działania, np. usunąć widok z kolekcji.
        /// </summary>
        public event EventHandler RequestClose;

        /// <summary>
        /// Metoda OnRequestClose wywołuje zdarzenie RequestClose, przekazując bieżący obiekt jako nadawcę oraz pusty EventArgs.
        /// </summary>
        protected void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
        #endregion 
    }
}
