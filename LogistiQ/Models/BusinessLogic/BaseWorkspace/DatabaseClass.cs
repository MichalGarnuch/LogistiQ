using LogistiQ.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.BusinessLogic.BaseWorkspace
{
    public class DatabaseClass
    {
        #region Context
        public LogistiQ_Entities db { get; set; }
        #endregion
        #region Konstruktor
        public DatabaseClass(LogistiQ_Entities db)
        {
            this.db = db;
        }
        #endregion
    }
}
