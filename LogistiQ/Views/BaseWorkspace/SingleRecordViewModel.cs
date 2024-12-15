using LogistiQ.Helper;
using LogistiQ.Models.Entities;
using LogistiQ.ViewModels.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogistiQ.Views.BaseWorkspace
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:LogistiQ.Views.BaseWorkspace"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:LogistiQ.Views.BaseWorkspace;assembly=LogistiQ.Views.BaseWorkspace"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:SingleRecordViewModel/>
    ///
    /// </summary>
    public abstract class SingleRecordViewModel<T> : WorkspaceViewModel
    {
        #region DB
        protected LogistiQ_Entities logistiQ_Entities;
        #endregion

        #region Item
        protected T item;
        #endregion

        #region Command
        private BaseCommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                    _SaveCommand = new BaseCommand(() => SaveAndClose());
                return _SaveCommand;
            }
        }
        #endregion

        #region Konstruktor
        public SingleRecordViewModel(string displayName)
        {
            base.DisplayName = displayName;
            logistiQ_Entities = new LogistiQ_Entities();
        }
        #endregion

        #region Helpers
        public abstract void Save();
        public void SaveAndClose()
        {
            Save();
            base.OnRequestClose();//zamknięcie zakładki
        }
        #endregion
    }
}
