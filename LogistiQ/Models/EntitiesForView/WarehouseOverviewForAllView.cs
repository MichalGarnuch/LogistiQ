using System;

namespace LogistiQ.Models.EntitiesForView
{
    public class WarehouseOverviewForAllView
    {
        public string ProductName { get; set; }
        public string WarehouseName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalStockValue { get; set; } // ✅ Łączna wartość stanu magazynowego
        public DateTime LastDeliveryDate { get; set; } // ✅ Data ostatniej dostawy
        public decimal AveragePurchasePrice { get; set; } // ✅ Średnia cena zakupu
        public decimal UnitPrice { get; set; } // ✅ Cena jednostkowa produktu
        public decimal LastDeliveryValue { get; set; } // ✅ Wartość ostatniej dostawy
        public decimal AverageDeliveryPrice { get; set; } // ✅ Brakująca właściwość!

        public int MinStock { get; set; } = 0; // ✅ Minimalna ilość dla progress bara
        public int MaxStock { get; set; } = 100; // ✅ Maksymalna ilość dla progress bara
    }
}
