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
    
    public partial class SprRptAssetsPurchase_Result
    {
        public string SourceOfFundName { get; set; }
        public string ProjectName { get; set; }
        public string PointName { get; set; }
        public string LocationName { get; set; }
        public string AreaName { get; set; }
        public string ProductName { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductGroupName { get; set; }
        public string BarCode { get; set; }
        public string MfgSlNo { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public Nullable<decimal> PurchaseValue { get; set; }
        public Nullable<decimal> BookValue { get; set; }
        public Nullable<decimal> AccumulatedDepreciation { get; set; }
        public string Description { get; set; }
        public bool IsHold { get; set; }
        public Nullable<System.DateTime> BeginDate { get; set; }
        public Nullable<System.DateTime> ExpiredDate { get; set; }
    }
}
