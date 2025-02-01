using LogistiQ.Models.BusinessLogic.BaseWorkspace;
using LogistiQ.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.BusinessLogic.SharedLogic
{
    public class OrderRecordB : DatabaseClass
    {
        #region Konstruktor
        public OrderRecordB(LogistiQ_Entities db)
            : base(db) { }
        #endregion

        #region Funkcje biznesowe
        // Funkcja zwraca łączną wartość zamówień danego klienta w podanym okresie
        public decimal GetTotalOrderValue(int customerId, DateTime dateFrom, DateTime dateTo)
        {
            return
                (
                    from order in db.Orders
                    where order.CustomerID == customerId &&
                          order.OrderDate >= dateFrom &&
                          order.OrderDate <= dateTo
                    select order.Total ?? 0 // Poprawka: jeśli Total jest NULL, zwróci 0
                ).Sum();
        }
        #endregion
    }
}
