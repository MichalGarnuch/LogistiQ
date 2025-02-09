using System;                        // Import podstawowych typów systemowych, takich jak DateTime, int, string.
using System.Collections.Generic;    // Import kolekcji generycznych, przydatnych przy pracy z kolekcjami (np. List<T>).
using System.Linq;                   // Import LINQ, umożliwiający wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw do operacji na ciągach znakowych.
using System.Threading.Tasks;        // Import przestrzeni nazw dla obsługi operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Deklaracja przestrzeni nazw, w której znajdują się encje widokowe.
{
    /// <summary>
    /// Klasa InvoiceForAllView reprezentuje model widoku faktury.
    /// Zawiera właściwości, które opisują szczegóły faktury, takie jak:
    /// - InvoiceID: unikalny identyfikator faktury,
    /// - DocumentType i DocumentDocumentNumber: dane dokumentu powiązanego z fakturą,
    /// - CustomerFirstName, CustomerLastName oraz CustomerNIP: dane klienta powiązanego z fakturą,
    /// - IssueDate: data wystawienia faktury (wartość opcjonalna),
    /// - DueDate: data płatności (wartość opcjonalna),
    /// - TotalAmount: łączna kwota faktury,
    /// - PaymentStatus: status płatności.
    /// Komentarze "klucz obcy początek" oraz "klucz obcy koniec" wskazują na grupy właściwości,
    /// które pochodzą z powiązanych tabel (relacji) w bazie danych.
    /// </summary>
    public class InvoiceForAllView
    {
        // Unikalny identyfikator faktury.
        public int InvoiceID { get; set; }

        // klucz obcy początek
        // Właściwości te reprezentują dane dokumentu powiązanego z fakturą.
        public string DocumentType { get; set; }          // Typ dokumentu (np. "Faktura", "Paragon").
        public string DocumentDocumentNumber { get; set; }  // Numer dokumentu, służący do identyfikacji.
        // klucz obcy koniec

        // klucz obcy początek
        // Właściwości te reprezentują dane klienta powiązanego z fakturą.
        public string CustomerFirstName { get; set; }       // Imię klienta.
        public string CustomerLastName { get; set; }        // Nazwisko klienta.
        public string CustomerNIP { get; set; }             // Numer identyfikacji podatkowej klienta.
        // klucz obcy koniec

        // Data wystawienia faktury; wartość opcjonalna, stąd typ nullable (DateTime?).
        public DateTime? IssueDate { get; set; }

        // Data, do której faktura powinna zostać opłacona; typ nullable (DateTime?).
        public DateTime? DueDate { get; set; }

        // Łączna kwota faktury.
        public decimal TotalAmount { get; set; }

        // klucz obcy początek
        // Właściwość reprezentująca status płatności faktury (np. "Paid", "Unpaid").
        public string PaymentStatus { get; set; }
        // klucz obcy koniec
    }
}
