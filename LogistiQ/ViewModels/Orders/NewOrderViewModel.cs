using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Views.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.ViewModels.Orders
{   
    public class NewOrderViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Orders>
    {

        #region Konstruktor
        public NewOrderViewModel()
            : base("Orders")
        {
            item = new Models.Entities.Orders();
        }

        #endregion

        #region Properties

        //dla każdego pola na interfejsie tworzymy properties
        public int OrderID
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
        public int? CustomerID
        {
            get
            {
                return item.CustomerID;
            }
            set
            {
                item.CustomerID = value;
                OnPropertyChanged(() => CustomerID);
            }
        }
        public DateTime? OrderDate
        {
            get
            {
                return item.OrderDate;
            }
            set
            {
                item.OrderDate = value;
                OnPropertyChanged(() => OrderDate);
            }
        }
        public string Status
        {
            get
            {
                return item.Status;
            }
            set
            {
                item.Status = value;
                OnPropertyChanged(() => Status);
            }
        }
        public decimal? Total
        {
            get
            {
                return item.Total;
            }
            set
            {
                item.Total = value;
                OnPropertyChanged(() => Total);
            }
        }

        #endregion

        #region PropertiesForCombobox

        public IQueryable<KeyAndValue> CustomerKeyAndValueItems
        {
            get
            {
                return new LogistiQ.Models.BusinessLogic.
                    CustomerB(logistiQ_Entities).GetCustomerKeyAndValueItems();
            }
        }

        #endregion
        #region Helpers
        public override void Save()
        {
            logistiQ_Entities.Orders.Add(item);//dodaje towar do lokalnej kolekcji
            logistiQ_Entities.SaveChanges();//zapisuje zmiany dokonane w bazie danych
        }

        #endregion
    }
}
