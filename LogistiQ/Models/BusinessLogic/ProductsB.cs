using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using System;
using LogistiQ.Models.BusinessLogic.BaseWorkspace;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.BusinessLogic
{
    public class ProductB : DatabaseClass
    {
        #region Konstruktor
        public ProductB(LogistiQ_Entities db)
            : base(db) { }
        #endregion
        #region Funkcje biznesowe
        //dodamy funkcje która będzie zwracała id products oraz ich nazwy w KeyAndValue
        public IQueryable<KeyAndValue> GetProductKeyAndValueItems()
        {
            return
                (
                    from product in db.Products
                    select new KeyAndValue
                    {
                        Key = product.ProductID,
                        Value = product.Name + " " + product.Type
                    }
                ).ToList().AsQueryable();
        }

        #endregion
    }
}
