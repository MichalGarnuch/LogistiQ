using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Views.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.ViewModels.Returns
{

    public class NewReturnViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Returns>
    {

        #region Konstruktor
        public NewReturnViewModel()
            : base("Returns")
        {
            item = new Models.Entities.Returns();
        }

        #endregion

        #region Properties

        //dla każdego pola na interfejsie tworzymy properties
        public int ReturnID
        {
            get
            {
                return item.ReturnID;
            }
            set
            {
                item.ReturnID = value;
                OnPropertyChanged(() => ReturnID);
            }
        }
        public int? OrderID
        {
            get
            {
                return item.OrderID;
            }
            set
            {
                item.OrderID = value;
                OnPropertyChanged(() => OrderID);
            }
        }
        public int? ProductID
        {
            get
            {
                return item.ProductID;
            }
            set
            {
                item.ProductID = value;
                OnPropertyChanged(() => ProductID);
            }
        }
        public int Quantity
        {
            get
            {
                return item.Quantity;
            }
            set
            {
                item.Quantity = value;
                OnPropertyChanged(() => Quantity);
            }
        }
        public string Reason
        {
            get
            {
                return item.Reason;
            }
            set
            {
                item.Reason = value;
                OnPropertyChanged(() => Reason);
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

        #endregion

        #region PropertiesForCombobox

        public IQueryable<KeyAndValue> OrderKeyAndValueItems
        {
            get
            {
                return new LogistiQ.Models.BusinessLogic.
                    OrderB(logistiQ_Entities).GetOrderKeyAndValueItems();
            }
        }
        public IQueryable<KeyAndValue> ProductKeyAndValueItems
        {
            get
            {
                return new LogistiQ.Models.BusinessLogic.
                    ProductB(logistiQ_Entities).GetProductKeyAndValueItems();
            }
        }
        #endregion

        #region Helpers
        public override void Save()
        {
            logistiQ_Entities.Returns.Add(item);//dodaje towar do lokalnej kolekcji
            logistiQ_Entities.SaveChanges();//zapisuje zmiany dokonane w bazie danych
        }

        #endregion
    }
}
