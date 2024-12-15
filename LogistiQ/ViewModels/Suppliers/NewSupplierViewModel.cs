using LogistiQ.Models.Entities;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Views.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.ViewModels.Suppliers
{
    public class NewSupplierViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Suppliers>
    {

        #region Konstruktor
        public NewSupplierViewModel()
            : base("Suppliers")
        {
            item = new Models.Entities.Suppliers();
        }

        #endregion

        #region Properties

        //dla każdego pola na interfejsie tworzymy properties
        public int SupplierID
        {
            get
            {
                return item.SupplierID;
            }
            set
            {
                item.SupplierID = value;
                OnPropertyChanged(() => SupplierID);
            }
        }
        public string Name
        {
            get
            {
                return item.Name;
            }
            set
            {
                item.Name = value;
                OnPropertyChanged(() => Name);
            }
        }
        public string Address
        {
            get
            {
                return item.Address;
            }
            set
            {
                item.Address = value;
                OnPropertyChanged(() => Address);
            }
        }
        public string Phone
        {
            get
            {
                return item.Phone;
            }
            set
            {
                item.Phone = value;
                OnPropertyChanged(() => Phone);
            }
        }
        public string Email
        {
            get
            {
                return item.Email;
            }
            set
            {
                item.Email = value;
                OnPropertyChanged(() => Email);
            }
        }
        public string Remarks
        {
            get
            {
                return item.Remarks;
            }
            set
            {
                item.Remarks = value;
                OnPropertyChanged(() => Remarks);
            }
        }

        #endregion

        #region Helpers
        public override void Save()
        {
            logistiQ_Entities.Suppliers.Add(item);//dodaje towar do lokalnej kolekcji
            logistiQ_Entities.SaveChanges();//zapisuje zmiany dokonane w bazie danych
        }

        #endregion
    }
}
