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
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
