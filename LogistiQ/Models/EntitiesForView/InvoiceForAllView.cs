using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.EntitiesForView
{
    public class InvoiceForAllView
    {
        public int InvoiceID { get; set; }
        // klucz obcy poczatek
        public string DocumentType { get; set; } 
        public string DocumentDocumentNumber { get; set; }
        // klucz obcy koniec
        // klucz obcy poczatek
        public string CustomerFirstName { get; set; } 
        public string CustomerLastName { get; set; } 
        public string CustomerNIP { get; set; }
        // klucz obcy koniec
        public DateTime? IssueDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        // klucz obcy poczatek
        public string PaymentStatus { get; set; }
        // klucz obcy koniec

    }
}
