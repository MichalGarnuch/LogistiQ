using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.EntitiesForView
{
    public class OrderRecordForAllView
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal UnitPrice { get; set; } // Nowe pole
        public int Quantity { get; set; }
        public decimal TotalOrderValue { get; set; }
        public string PaymentStatus { get; set; }
        public string ProductName { get; set; }
    }

}
