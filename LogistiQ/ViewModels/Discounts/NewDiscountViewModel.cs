using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Views.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.ViewModels.Discounts
{
    public class NewDiscountViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Discounts>
    {

        #region Konstruktor
        public NewDiscountViewModel()
            : base("Discounts")
        {
            item = new Models.Entities.Discounts();
        }

        #endregion

        #region Properties

        //dla każdego pola na interfejsie tworzymy properties
        public int DiscountID
        {
            get
            {
                return item.DiscountID;
            }
            set
            {
                item.DiscountID = value;
                OnPropertyChanged(() => DiscountID);
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
        public DateTime StartDate
        {
            get
            {
                return item.StartDate;
            }
            set
            {
                item.StartDate = value;
                OnPropertyChanged(() => StartDate);
            }
        }
        public DateTime EndDate
        {
            get
            {
                return item.EndDate;
            }
            set
            {
                item.EndDate = value;
                OnPropertyChanged(() => EndDate);
            }
        }
        public decimal DiscountPercent
        {
            get
            {
                return item.DiscountPercent;
            }
            set
            {
                item.DiscountPercent = value;
                OnPropertyChanged(() => DiscountPercent);
            }
        }

        #endregion

        #region PropertiesForCombobox

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
            logistiQ_Entities.Discounts.Add(item);//dodaje towar do lokalnej kolekcji
            logistiQ_Entities.SaveChanges();//zapisuje zmiany dokonane w bazie danych
        }

        #endregion
    }

}
