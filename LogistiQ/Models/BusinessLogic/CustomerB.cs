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
    public class CustomerB : DatabaseClass
    {
        #region Konstruktor
        public CustomerB(LogistiQ_Entities db)
            : base(db) { }
        #endregion
        #region Funkcje biznesowe
        //dodamy funkcje która będzie zwracała id customer oraz ich nazwy w KeyAndValue
        public IQueryable<KeyAndValue> GetCustomerKeyAndValueItems()
        {
            return
                (
                    from customer in db.Customers
                    select new KeyAndValue
                    {
                        Key = customer.CustomerID,
                        Value = customer.FirstName + " " + customer.LastName + " " + customer.NIP
                    }
                ).ToList().AsQueryable();
        }

        #endregion
    }
}
