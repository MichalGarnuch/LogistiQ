//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LogistiQ.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Discounts
    {
        public int DiscountID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public decimal DiscountPercent { get; set; }
    
        public virtual Products Products { get; set; }
    }
}
