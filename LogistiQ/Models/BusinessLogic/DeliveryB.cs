using LogistiQ.Models.BusinessLogic.BaseWorkspace;
using LogistiQ.Models.Entities;
using LogistiQ.Models.EntitiesForView.BaseWorkspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.BusinessLogic
{
    public class DeliveryB : DatabaseClass
        {
            #region Konstruktor
            public DeliveryB(LogistiQ_Entities db)
                : base(db) { }
            #endregion
            #region Funkcje biznesowe
            //dodamy funkcje która będzie zwracała id customer oraz ich nazwy w KeyAndValue
            public IQueryable<KeyAndValue> GetDeliveryKeyAndValueItems()
            {
                return
                    (
                        from delivery in db.Deliveries
                        select new KeyAndValue
                        {
                            Key = delivery.DeliveryID,
                            Value = delivery.Suppliers.Name
                        }
                    ).ToList().AsQueryable();
            }

            #endregion
        }
}
