using LogistiQ.Models.BusinessLogic.BaseWorkspace;
using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var query = (from order in db.Orders
                         join customer in db.Customers on order.CustomerID equals customer.CustomerID
                         join orderDetail in db.OrderDetails on order.OrderID equals orderDetail.OrderID into orderDetails
                         from orderDetail in orderDetails.DefaultIfEmpty()
                         join product in db.Products on orderDetail.ProductID equals product.ProductID into products
                         from product in products.DefaultIfEmpty()
                         join payment in db.Payments on order.OrderID equals payment.OrderID into payments
                         from payment in payments.DefaultIfEmpty()
                         where order.CustomerID == customerId
                         select new { order, customer, orderDetail, product, payment }).ToList();

            return query.GroupBy(x => x.order.OrderID)
                        .Select(grouped => new OrderRecordForAllView
                        {
                            OrderID = grouped.Key,
                            CustomerName = grouped.FirstOrDefault()?.customer?.FirstName + " " + grouped.FirstOrDefault()?.customer?.LastName ?? "Unknown",
                            OrderDate = grouped.FirstOrDefault()?.order?.OrderDate ?? DateTime.MinValue,
                            Status = grouped.FirstOrDefault()?.order?.Status ?? "Unknown",
                            UnitPrice = grouped.FirstOrDefault()?.orderDetail?.UnitPrice ?? 0m, // Nowe pole
                            Quantity = grouped.Sum(x => x.orderDetail?.Quantity ?? 0),
                            TotalOrderValue = grouped.Sum(x => (x.orderDetail?.UnitPrice ?? 0m) * (decimal)(x.orderDetail?.Quantity ?? 0)),
                            PaymentStatus = grouped.Sum(x => x.payment?.Amount ?? 0) >= grouped.Sum(x => (x.orderDetail?.UnitPrice ?? 0m) * (decimal)(x.orderDetail?.Quantity ?? 0))
                                            ? "Paid" : "Unpaid",
                            ProductName = grouped.FirstOrDefault(x => x.product != null)?.product?.Name ?? "No Product"
                        }).ToList();
        }

        #endregion
    }
}
