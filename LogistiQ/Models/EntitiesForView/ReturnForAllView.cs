using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.EntitiesForView
{
    public class ReturnForAllView
    {
        public int ReturnID { get; set; }
        // klucz obcy poczatek
        public string OrderCustomerIDFirstName { get; set; }
        public string OrderCustomerIDLastName { get; set; }
        public string OrderCustomerIDNIP { get; set; }
        public string OrderCustomerIDAddress { get; set; }
        public string OrderCustomerIDPhone { get; set; }
        public string OrderCustomerIDEmail { get; set; }
        // klucz obcy koniec
        // klucz obcy poczatek
        public string ProductName { get; set; }
        // klucz obcy koniec
        public int Quantity { get; set; }
        public string Reason { get; set; }
        public DateTime? Date { get; set; }


    }
}
