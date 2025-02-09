using LogistiQ.Models.BusinessLogic.BaseWorkspace; // Import klasy bazowej dla logiki biznesowej, która zawiera m.in. klasę DatabaseClass.
using LogistiQ.Models.Entities; // Import encji bazy danych, takich jak Orders, Customers, OrderDetails, Products, Payments.
using LogistiQ.Models.EntitiesForView; // Import encji widokowych (DTO) używanych do prezentacji danych, np. OrderRecordForAllView.
using LogistiQ.Models.EntitiesForView.BaseWorkspace; // Import encji bazowej dla widoków, np. KeyAndValue.
using System; // Import podstawowych funkcji systemowych i typów danych.
using System.Collections.Generic; // Import kolekcji generycznych, np. List<T>.
using System.Linq; // Import LINQ umożliwiający wykonywanie zapytań na kolekcjach.

namespace LogistiQ.Models.BusinessLogic.SharedLogic
{
    /// <summary>
    /// Klasa `OrderRecordB` zawiera logikę biznesową dotyczącą zamówień.
    /// Dziedziczy po `DatabaseClass`, co umożliwia dostęp do kontekstu bazy danych.
    /// </summary>
    public class OrderRecordB : DatabaseClass
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor przekazuje kontekst bazy danych do klasy nadrzędnej `DatabaseClass`.
        /// Dzięki temu instancja klasy OrderRecordB ma dostęp do danych bazy poprzez wspólny kontekst.
        /// </summary>
        /// <param name="db">Instancja kontekstu bazy danych `LogistiQ_Entities`.</param>
        public OrderRecordB(LogistiQ_Entities db)
            : base(db) { } // Przekazuje kontekst bazy danych do klasy bazowej, co zapobiega wielokrotnemu tworzeniu instancji kontekstu.

        #endregion

        #region Funkcje biznesowe

        /// <summary>
        /// Pobiera listę klientów w formacie klucz-wartość.
        /// </summary>
        /// <returns>
        /// `IQueryable<KeyAndValue>` zawierający ID klienta (Key) oraz dane klienta w formacie "Imię Nazwisko | NIP" (Value).
        /// </returns>
        public IQueryable<KeyAndValue> GetCustomerKeyAndValueItems()
        {
            // Zapytanie LINQ, które:
            // 1. Iteruje po wszystkich klientach w tabeli Customers.
            // 2. Tworzy dla każdego obiekt KeyAndValue, gdzie:
            //    - Key jest ustawiony na CustomerID,
            //    - Value jest złożonym stringiem łączącym imię, nazwisko oraz NIP.
            // 3. Konwertuje wynik do listy, a następnie do IQueryable, co umożliwia dalsze operacje zapytań.
            return (from customer in db.Customers
                    select new KeyAndValue
                    {
                        Key = customer.CustomerID, // Ustawienie klucza jako ID klienta.
                        Value = customer.FirstName + " " + customer.LastName + " | " + customer.NIP // Łączenie imienia, nazwiska i NIP w jeden string.
                    }).ToList().AsQueryable(); // Konwersja wyniku na listę, a następnie na IQueryable.
        }

        /// <summary>
        /// Pobiera listę zamówień dla danego klienta, uwzględniając szczegóły zamówienia, produkty oraz płatności.
        /// </summary>
        /// <param name="customerId">ID klienta.</param>
        /// <returns>Lista `OrderRecordForAllView` zawierająca szczegóły zamówień.</returns>
        public List<OrderRecordForAllView> GetOrdersByCustomer(int customerId)
        {
            // Zapytanie LINQ łączące dane z wielu tabel:
            // - Orders: główna tabela zamówień,
            // - Customers: tabela klientów,
            // - OrderDetails: szczegóły zamówień (może nie istnieć dla każdego zamówienia - left join),
            // - Products: produkty przypisane do zamówień (left join),
            // - Payments: płatności powiązane z zamówieniami (left join).
            var query = (from order in db.Orders
                         join customer in db.Customers on order.CustomerID equals customer.CustomerID
                         join orderDetail in db.OrderDetails on order.OrderID equals orderDetail.OrderID into orderDetails
                         from orderDetail in orderDetails.DefaultIfEmpty() // Left join: umożliwia uwzględnienie zamówień bez szczegółów.
                         join product in db.Products on orderDetail.ProductID equals product.ProductID into products
                         from product in products.DefaultIfEmpty() // Left join: uwzględnia sytuacje, gdy produkt nie jest przypisany.
                         join payment in db.Payments on order.OrderID equals payment.OrderID into payments
                         from payment in payments.DefaultIfEmpty() // Left join: uwzględnia brak płatności dla zamówienia.
                         where order.CustomerID == customerId // Filtrowanie zamówień tylko dla określonego klienta.
                         select new { order, customer, orderDetail, product, payment }
                        ).ToList(); // Konwersja wyniku zapytania na listę anonimowych obiektów.

            // Grupowanie wyników zapytania według ID zamówienia, aby zebrać wszystkie powiązane dane.
            // Następnie mapowanie każdej grupy na obiekt OrderRecordForAllView.
            return query.GroupBy(x => x.order.OrderID)
                        .Select(grouped => new OrderRecordForAllView
                        {
                            OrderID = grouped.Key, // Ustawienie ID zamówienia.

                            // Pobranie imienia i nazwiska klienta z pierwszego elementu grupy.
                            // Użycie operatora null-conditional (?.) zapewnia, że brak danych nie spowoduje wyjątku.
                            // Jeśli dane są niedostępne, wynikowy string będzie "Unknown".
                            CustomerName = $"{grouped.FirstOrDefault()?.customer?.FirstName} {grouped.FirstOrDefault()?.customer?.LastName}" ?? "Unknown",

                            // Pobranie daty zamówienia z pierwszego elementu grupy, lub ustawienie na DateTime.MinValue, gdy brak danych.
                            OrderDate = grouped.FirstOrDefault()?.order?.OrderDate ?? DateTime.MinValue,

                            // Pobranie statusu zamówienia lub ustawienie na "Unknown", gdy brak danych.
                            Status = grouped.FirstOrDefault()?.order?.Status ?? "Unknown",

                            // Obliczenie całkowitej wartości zamówienia poprzez sumowanie iloczynu UnitPrice i Quantity dla każdego szczegółu.
                            TotalOrderValue = grouped.Sum(x => (x.orderDetail?.UnitPrice ?? 0m) * (x.orderDetail?.Quantity ?? 0)),

                            // Obliczenie sumy ilości produktów we wszystkich szczegółach zamówienia.
                            Quantity = grouped.Sum(x => x.orderDetail?.Quantity ?? 0),

                            // Obliczenie średniej ceny jednostkowej:
                            // Jeśli łączna ilość produktów jest większa od 0, obliczamy średnią jako stosunek całkowitej wartości do łącznej ilości.
                            // W przeciwnym wypadku zwracamy 0m.
                            UnitPrice = grouped.Sum(x => (x.orderDetail?.Quantity ?? 0)) > 0
                                        ? grouped.Sum(x => (x.orderDetail?.UnitPrice ?? 0m) * (x.orderDetail?.Quantity ?? 0)) / grouped.Sum(x => x.orderDetail?.Quantity ?? 0)
                                        : 0m,

                            // Określenie statusu płatności:
                            // Jeśli suma wszystkich płatności pokrywa lub przekracza wartość zamówienia, ustawiamy status na "Paid",
                            // w przeciwnym razie na "Unpaid".
                            PaymentStatus = grouped.Sum(x => x.payment?.Amount ?? 0) >= grouped.Sum(x => (x.orderDetail?.UnitPrice ?? 0m) * (x.orderDetail?.Quantity ?? 0))
                                            ? "Paid" : "Unpaid",

                            // Ustalenie nazwy produktu:
                            // Jeśli zamówienie zawiera wiele unikalnych nazw produktów, zwracamy "Multiple Products".
                            // W przeciwnym razie, jeśli jest jeden produkt, zwracamy jego nazwę, lub "No Product" gdy brak danych.
                            ProductName = grouped.Select(x => x.product?.Name).Distinct().Count() > 1
                                          ? "Multiple Products"
                                          : grouped.FirstOrDefault(x => x.product != null)?.product?.Name ?? "No Product"
                        }).ToList(); // Konwersja wyników na listę obiektów OrderRecordForAllView.
        }

        /// <summary>
        /// Oblicza łączną wartość zamówień dla danego klienta.
        /// </summary>
        /// <param name="customerId">ID klienta.</param>
        /// <returns>Łączna wartość zamówień jako `decimal`.</returns>
        public decimal GetTotalOrderValue(int customerId)
        {
            // Pobiera wszystkie zamówienia dla klienta i sumuje wartość TotalOrderValue każdego zamówienia.
            return GetOrdersByCustomer(customerId).Sum(x => x.TotalOrderValue);
        }

        /// <summary>
        /// Oblicza średnią wartość zamówienia dla danego klienta.
        /// </summary>
        /// <param name="customerId">ID klienta.</param>
        /// <returns>Średnia wartość zamówienia jako `decimal`.</returns>
        public decimal GetAverageOrderValue(int customerId)
        {
            // Pobiera listę zamówień dla klienta.
            var orders = GetOrdersByCustomer(customerId);
            // Oblicza łączną ilość produktów we wszystkich zamówieniach.
            var totalQuantity = orders.Sum(x => x.Quantity);
            // Jeśli łączna ilość produktów jest większa niż 0, oblicza średnią wartość jako stosunek sumy TotalOrderValue do łącznej ilości produktów.
            // W przeciwnym razie zwraca 0m.
            return totalQuantity > 0 ? orders.Sum(x => x.TotalOrderValue) / totalQuantity : 0m;
        }

        #endregion
    }
}
