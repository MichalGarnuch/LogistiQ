using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.EntitiesForView
{
    public class DeliveryRecordForAllView
    {
        public int DeliveryID { get; set; }
        public string SupplierName { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
        public string WarehouseName { get; set; }
    }
}

