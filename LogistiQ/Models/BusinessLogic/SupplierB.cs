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
    public class SupplierB : DatabaseClass
    {
        #region Konstruktor
        public SupplierB(LogistiQ_Entities db)
            : base(db) { }
        #endregion
        #region Funkcje biznesowe
        //dodamy funkcje która będzie zwracała id supplier oraz ich nazwy w KeyAndValue
        public IQueryable<KeyAndValue> GetSupplierKeyAndValueItems()
        {
            return
                (
                    from supplier in db.Suppliers
                    select new KeyAndValue
                    {
                        Key = supplier.SupplierID,
                        Value = supplier.Name + " " + supplier.Address
                    }
                ).ToList().AsQueryable();
        }

        #endregion
    }
}
