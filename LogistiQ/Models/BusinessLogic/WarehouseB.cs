using LogistiQ.Models.BusinessLogic.BaseWorkspace;
using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.BusinessLogic
{
    public class WarehouseB : DatabaseClass
    {
        #region Konstruktor
        public WarehouseB(LogistiQ_Entities db)
            : base(db) { }
        #endregion
        #region Funkcje biznesowe
        //dodamy funkcje która będzie zwracała id warehouse oraz ich nazwy w KeyAndValue
        public IQueryable<KeyAndValue> GetWarehouseKeyAndValueItems()
        {
            return
                (
                    from warehouse in db.Warehouses
                    select new KeyAndValue
                    {
                        Key = warehouse.WarehouseID,
                        Value = warehouse.Name + " " + warehouse.Location
                    }
                ).ToList().AsQueryable();
        }

        #endregion
    }
}
