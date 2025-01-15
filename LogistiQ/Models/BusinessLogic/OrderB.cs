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
    public class OrderB : DatabaseClass
    {
        #region Konstruktor
        public OrderB(LogistiQ_Entities db)
            : base(db) { }
        #endregion
        #region Funkcje biznesowe
        //dodamy funkcje która będzie zwracała id order oraz ich nazwy w KeyAndValue
        public IQueryable<KeyAndValue> GetCustomerKeyAndValueItems()
        {
            return
                (
                    from order in db.Orders
                    select new KeyAndValue
                    {
                        Key = order.OrderID,
                        Value = order.Customers.FirstName + " " + order.Customers.LastName + " " + order.Customers.NIP 
                        + " " + order.Customers.Address + " " + order.Customers.Phone + " " + order.Customers.Email
                    }
                ).ToList().AsQueryable();
        }

        #endregion
    }
}
