using LogistiQ.Models.Entities;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using LogistiQ.ViewModels.Products;

namespace LogistiQ.ViewModels.Documents
{
    public class AllDocumentsViewModel : AllViewModel<DocumentForAllView>
    {
        #region Constructor

        public AllDocumentsViewModel()
            : base()
        {
            base.DisplayName = "Documents";

        }
        public override WorkspaceViewModel CreateNewViewModel()
        {
            return new NewDocumentViewModel(); // Zwracamy odpowiedni ViewModel
        }
        #endregion

        #region Sort And Find
        //tu decydujemy po czym sortować do comboboxa
        public override List<string> GetComboboxSortList()
        {
            throw new System.NotImplementedException();
        }
        //tu decydujemy jak sortować
        public override void Sort()
        {
            throw new System.NotImplementedException();
        }
        //tu decydujemy po czym wyszukiwać
        public override List<string> GetComboboxFindList()
        {
            throw new System.NotImplementedException();
        }
        //tu decydujemy jak wyszukiwać
        public override void Find()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<DocumentForAllView>
                (
                    from Documents in logistiq_Entities.Documents
                    select new DocumentForAllView
                    {
                        DocumentID = Documents.DocumentID,
                        Type = Documents.Type,
                        DocumentNumber = Documents.DocumentNumber,
                        Date = Documents.Date,
                        WarehouseName = Documents.Warehouses.Name,
                        WarehouseLocation = Documents.Warehouses.Location,
                        TotalValue = Documents.TotalValue,
                        Notes = Documents.Notes
                    }
                );
        }
        #endregion
    }
}
