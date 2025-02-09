using System;  // Import podstawowych typów systemowych, takich jak DateTime, które są używane w tej klasie.

namespace LogistiQ.Models.EntitiesForView  // Definicja przestrzeni nazw dla modeli widokowych (view models).
{
    /// <summary>
    /// Klasa WarehouseOverviewForAllView reprezentuje model widoku, który agreguje i prezentuje przegląd stanu magazynowego.
    /// Zawiera informacje o produkcie, magazynie, poziomach zapasów oraz danych dotyczących ostatnich dostaw.
    /// </summary>
    public class WarehouseOverviewForAllView
    {
        // Nazwa produktu, którego stan magazynowy jest monitorowany.
        public string ProductName { get; set; }

        // Nazwa magazynu, w którym produkt jest przechowywany.
        public string WarehouseName { get; set; }

        // Ilość produktu dostępna w magazynie.
        public int Quantity { get; set; }

        // Całkowita wartość zapasów danego produktu (np. Quantity * UnitPrice).
        public decimal TotalStockValue { get; set; }

        // Data ostatniej dostawy tego produktu do magazynu.
        public DateTime LastDeliveryDate { get; set; }

        // Średnia cena zakupu produktu na podstawie poprzednich dostaw.
        public decimal AveragePurchasePrice { get; set; }

        // Aktualna cena jednostkowa produktu.
        public decimal UnitPrice { get; set; }

        // Wartość ostatniej dostawy (np. ilość dostarczona * cena jednostkowa w tej dostawie).
        public decimal LastDeliveryValue { get; set; }

        // Średnia cena dostawy produktu na podstawie historycznych danych.
        public decimal AverageDeliveryPrice { get; set; }

        // Minimalny poziom zapasów, poniżej którego powinno się podjąć działania (domyślnie 0).
        public int MinStock { get; set; } = 0;

        // Maksymalny poziom zapasów, powyżej którego magazyn może być uznany za nadmiarowy (domyślnie 100).
        public int MaxStock { get; set; } = 100;
    }
}
