using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.EntitiesForView
{
    public class DeliveryDetailForAllView
    {
        public int DeliveryDetailID { get; set; }
        public string DeliverySupplierIDName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
