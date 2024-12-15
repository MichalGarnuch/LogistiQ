using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.EntitiesForView
{
    public class StockLevelForAllView
    {
        public int StockLevelID { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseLocation { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
