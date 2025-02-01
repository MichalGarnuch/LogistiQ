using LogistiQ.Models.BusinessLogic.BaseWorkspace;
using LogistiQ.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.BusinessLogic.SharedLogic
{
    public class DeliveryRecordB : DatabaseClass
    {
        #region Konstruktor
        public DeliveryRecordB(LogistiQ_Entities db)
            : base(db) { }
        #endregion

        #region Funkcje biznesowe
        // Funkcja zwraca ilość dostaw w zadanym przedziale czasu dla magazynu
        public int GetDeliveriesCount(int warehouseId, DateTime dateFrom, DateTime dateTo)
        {
            return db.Deliveries.Count(delivery =>
                delivery.WarehouseID == warehouseId &&
                delivery.DeliveryDate >= dateFrom &&
                delivery.DeliveryDate <= dateTo);
        }
        #endregion
    }
}