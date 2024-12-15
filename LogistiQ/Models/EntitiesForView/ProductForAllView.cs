﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogistiQ.Models.EntitiesForView
{
  public class ProductForAllView
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public decimal VAT { get; set; }
        public string CategoryName { get; set; }
    }
}
