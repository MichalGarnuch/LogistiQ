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
    
    public partial class Inventory
    {
        public int InventoryID { get; set; }
        public Nullable<int> WarehouseID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> RecordedQuantity { get; set; }
    
        public virtual Warehouses Warehouses { get; set; }
        public virtual Products Products { get; set; }
    }
}
