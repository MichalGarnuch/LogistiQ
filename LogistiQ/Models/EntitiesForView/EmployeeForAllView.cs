using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.EntitiesForView
{
    public class EmployeeForAllView
    {
        public int EmployeeID { get; set; }
        // klucz obcy poczatek
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseLocation { get; set; }
        // klucz obcy koniec
        public string Remarks { get; set; }
    }
}
