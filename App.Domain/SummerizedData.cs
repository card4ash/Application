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
    
    public partial class SummerizedData
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public int SourceId { get; set; }
        public int DistrictId { get; set; }
        public int UpazillaId { get; set; }
        public int ReachCount { get; set; }
        public string Description { get; set; }
        public string CollectedBy { get; set; }
        public int InsertedById { get; set; }
    
        public virtual StandingData StandingData { get; set; }
        public virtual StandingData StandingData1 { get; set; }
        public virtual StandingData StandingData2 { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
