using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.EntitiesForView
{
    public class InventoryForAllView
    {
        public int InventoryID { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseLocation { get; set; }
        public DateTime? Date { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int? RecordedQuantity { get; set; }

    }
}
