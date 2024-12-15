using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.EntitiesForView
{
    public class OrderDetailForAllView
    {
        public int OrderDetailID { get; set; }
        public string OrderCustomerIDFirstName { get; set; }
        public string OrderCustomerIDLastName { get; set; }
        public string OrderCustomerIDNIP { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal VAT { get; set; }
    }
}
