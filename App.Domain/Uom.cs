//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppProj.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Uom
    {
        public Uom()
        {
            this.AssetEntries = new HashSet<AssetEntry>();
            this.UomOfProducts = new HashSet<UomOfProduct>();
        }
    
        public int ID { get; set; }
        public int UnitId { get; set; }
        public string Name { get; set; }
        public string PublicName { get; set; }
        public bool IsActive { get; set; }
        public int LastActionBy { get; set; }
        public System.DateTime LastActionTime { get; set; }
    
        public virtual ICollection<AssetEntry> AssetEntries { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<UomOfProduct> UomOfProducts { get; set; }
    }
}
