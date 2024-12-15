using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.EntitiesForView
{
    public class PaymentForAllView
    {
        public int PaymentID { get; set; }
        // klucz obcy poczatek
        public string OrderCustomerIDFirstName { get; set; }
        public string OrderCustomerIDLastName { get; set; }
        public string OrderCustomerIDNIP { get; set; }
        public string OrderCustomerIDAddress { get; set; }
        public string OrderCustomerIDPhone { get; set; }
        public string OrderCustomerIDEmail { get; set; }
        // klucz obcy koniec
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
