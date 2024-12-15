using LogistiQ.Models.Entities;
using LogistiQ.Views.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.ViewModels.Documents
{
    public class NewDocumentViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Documents>
    {

        #region Konstruktor
        public NewDocumentViewModel()
            : base("Document")
        {
            item = new Models.Entities.Documents();
        }

        #endregion

        #region Properties

        public int DocumentID
        {
            get
            {
                return item.DocumentID;
            }
            set
            {
                item.DocumentID = value;
                OnPropertyChanged(() => DocumentID);
            }
        }
        public string Type
        {
            get
            {
                return item.Type;
            }
            set
            {
                item.Type = value;
                OnPropertyChanged(() => Type);
            }
        }
        public string DocumentNumber
        {
            get
            {
                return item.DocumentNumber;
            }
            set
            {
                item.DocumentNumber = value;
                OnPropertyChanged(() => DocumentNumber);
            }
        }
        public DateTime Date
        {
            get
            {
                return item.Date;
            }
            set
            {
                item.Date = value;
                OnPropertyChanged(() => Date);
            }
        }
        public int? WarehouseID
        {
            get
            {
                return item.WarehouseID;
            }
            set
            {
                item.WarehouseID = value;
                OnPropertyChanged(() => WarehouseID);
            }
        }
        public decimal TotalValue
        {
            get
            {
                return item.TotalValue;
            }
            set
            {
                item.TotalValue = value;
                OnPropertyChanged(() => TotalValue);
            }
        }
        public string Notes
        {
            get
            {
                return item.Notes;
            }
            set
            {
                item.Notes = value;
                OnPropertyChanged(() => Notes);
            }
        }

        #endregion

        #region Helpers
        public override void Save()
        {
            logistiQ_Entities.Documents.Add(item);//dodaje towar do lokalnej kolekcji
            logistiQ_Entities.SaveChanges();//zapisuje zmiany dokonane w bazie danych
        }

        #endregion
    }
}
