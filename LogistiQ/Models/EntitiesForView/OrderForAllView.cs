using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.EntitiesForView
{
    public class OrderForAllView
    {
        public int OrderID { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerNIP { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Status { get; set; }
        public decimal? Total { get; set; }

    }
}
