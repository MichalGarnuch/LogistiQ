using System;                        // Import podstawowych typów systemowych, np. int, string, decimal.
using System.Collections.Generic;    // Import kolekcji generycznych, np. List<T>.
using System.Linq;                   // Import LINQ, umożliwia wykonywanie zapytań na kolekcjach.
using System.Text;                   // Import przestrzeni nazw do operacji na tekście.
using System.Threading.Tasks;        // Import przestrzeni nazw do obsługi operacji asynchronicznych.

namespace LogistiQ.Models.EntitiesForView  // Deklaracja przestrzeni nazw, w której znajdują się encje widokowe.
{
    /// <summary>
    /// Klasa DeliveryDetailForAllView reprezentuje szczegóły dostawy, które będą prezentowane w widoku.
    /// Zawiera właściwości, które opisują poszczególne dane dotyczące szczegółów dostawy.
    /// </summary>
    public class DeliveryDetailForAllView
    {
        // Unikalny identyfikator szczegółu dostawy.
        public int DeliveryDetailID { get; set; }

        // Łączona informacja o dostawcy, która może zawierać zarówno identyfikator dostawcy, jak i jego nazwę.
        public string DeliverySupplierIDName { get; set; }

        // Nazwa produktu, który został dostarczony.
        public string ProductName { get; set; }

        // Ilość dostarczonego produktu.
        public int Quantity { get; set; }

        // Cena jednostkowa produktu w dostawie.
        public decimal UnitPrice { get; set; }
    }
}
