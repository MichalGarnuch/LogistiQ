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
    
    public partial class Shifts
    {
        public int ShiftID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public System.DateTime Date { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public Nullable<int> WarehouseID { get; set; }
    
        public virtual Employees Employees { get; set; }
        public virtual Warehouses Warehouses { get; set; }
    }
}