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
    public class CategoryB : DatabaseClass
    {
        #region Konstruktor
        public CategoryB(LogistiQ_Entities db)
            : base(db) { }
        #endregion
        #region Funkcje biznesowe
        //dodamy funkcje która będzie zwracała id products oraz ich nazwy w KeyAndValue
        public IQueryable<KeyAndValue> GetCategoryKeyAndValueItems()
        {
            return
                (
                    from category in db.Categories
                    select new KeyAndValue
                    {
                        Key = category.CategoryID,
                        Value = category.Name
                    }
                ).ToList().AsQueryable();
        }

        #endregion
    }
}
