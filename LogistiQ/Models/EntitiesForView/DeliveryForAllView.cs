using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.EntitiesForView
{
    public class DeliveryForAllView
    {
        public int DeliveryID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal Cost { get; set; }
        public string Status { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseLocation { get; set; }
    }
}
