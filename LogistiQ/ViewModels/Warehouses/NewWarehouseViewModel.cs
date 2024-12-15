using LogistiQ.Models.Entities;
using LogistiQ.ViewModels.BaseWorkspace;
using LogistiQ.Views.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.ViewModels.Warehouses
{
    public class NewWarehouseViewModel : SingleRecordViewModel<LogistiQ.Models.Entities.Warehouses>
    {

        #region Konstruktor
        public NewWarehouseViewModel()
            : base("Warehouses")
        {
            item = new Models.Entities.Warehouses();
        }

        #endregion

        #region Properties

        //dla każdego pola na interfejsie tworzymy properties
        public int WarehouseID
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
        public string Location
        {
            get
            {
                return item.Location;
            }
            set
            {
                item.Location = value;
                OnPropertyChanged(() => Location);
            }
        }
        public int Capacity
        {
            get
            {
                return item.Capacity;
            }
            set
            {
                item.Capacity = value;
                OnPropertyChanged(() => Capacity);
            }
        }

        #endregion

        #region Helpers
        public override void Save()
        {
            logistiQ_Entities.Warehouses.Add(item);//dodaje towar do lokalnej kolekcji
            logistiQ_Entities.SaveChanges();//zapisuje zmiany dokonane w bazie danych
        }

        #endregion
    }
}
