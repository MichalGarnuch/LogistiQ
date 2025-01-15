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
    public class DocumentB : DatabaseClass
    {
        #region Konstruktor
        public DocumentB(LogistiQ_Entities db)
            : base(db) { }
        #endregion
        #region Funkcje biznesowe
        //dodamy funkcje która będzie zwracała id document oraz ich nazwy w KeyAndValue
        public IQueryable<KeyAndValue> GetDocumentKeyAndValueItems()
        {
            return
                (
                    from document in db.Documents
                    select new KeyAndValue
                    {
                        Key = document.DocumentID,
                        Value = document.Type + " " + document.DocumentNumber
                    }
                ).ToList().AsQueryable();
        }

        #endregion
    }
}
