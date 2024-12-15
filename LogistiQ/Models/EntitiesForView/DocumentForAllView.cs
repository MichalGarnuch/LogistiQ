using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.EntitiesForView
{
    public class DocumentForAllView
    {
        public int DocumentID { get; set; }
        public string Type { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime Date { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseLocation { get; set; }
        public decimal TotalValue { get; set; }
        public string Notes { get; set; }

    }
}
