using LogistiQ.Models.BusinessLogic.BaseWorkspace;
using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
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
        public IQueryable<KeyAndValue> GetCustomerKeyAndValueItems()
        {
            return (from customer in db.Customers
                    select new KeyAndValue
                    {
                        Key = customer.CustomerID,
                        Value = customer.FirstName + " " + customer.LastName + " | " + customer.NIP
                    }).ToList().AsQueryable();
        }

        public List<OrderRecordForAllView> GetOrdersByCustomer(int customerId)
        {
            return (from order in db.Orders
                    where order.CustomerID == customerId
                    select new OrderRecordForAllView
                    {
                        OrderID = order.OrderID,
                        CustomerName = order.Customers.FirstName + " " + order.Customers.LastName,
                        OrderDate = order.OrderDate ?? DateTime.MinValue,
                        Status = order.Status,
                        ProductName = order.OrderDetails.FirstOrDefault().Products.Name,
                        Quantity = order.OrderDetails.FirstOrDefault().Quantity
                    }).ToList();
        }
        #endregion
    }
}
