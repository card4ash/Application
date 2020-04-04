using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppProj.Web.ViewModels
{
    public class PosModel
    {
        public int Serial { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }

    public class PosSelectModel
    {       
        [Required]
        public int PointId { get; set; }
    }

    public class PosOrderModel
    {
        public string PosName { get; set; }
        public int Id { get; set; }
        public Nullable<int> FolioId { get; set; }
        public string FolioCode { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderEndDate { get; set; }
        public int CurrencyId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> ContactPersonId { get; set; }
        public Nullable<int> BillingAdderssId { get; set; }
        public Nullable<int> ServiceStuffId { get; set; }
        public Nullable<int> OperationTypeId { get; set; }
        public Nullable<int> OperationVariableId { get; set; }
        public string Remark { get; set; }
    }

    public class PosItemModel
    {
        public Guid? Serial { get; set; }
        public int? Id { get; set; }

        public Nullable<DateTime> RentalDate { get; set; }
        public int? DisbursePointId { get; set; }

        [Required]
        public int ProductId { get; set; }
        
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public bool PriceEditable { get; set; }

        [Required]
        public decimal ServiceCharge { get; set; }

        [Required]
        public decimal VatTax { get; set; }

        [Required]
        public decimal Quantity { get; set; }
        
        [Required]
        public int ProductPropertyId { get; set; }

        public string ProductPropertyName { get; set; }

        [Required]
        public int UomId { get; set; }

        public string Remark { get; set; }

        public DateTime OrderDate { get; set; }
        public int CurrencyId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> OperationTypeId { get; set; }
        public Nullable<int> OperationVariableId { get; set; }        
    }

    public class PosItemChargeModel
    {
        public Guid? Serial { get; set; }
    }

    public class PosImageGridParam
    {
        public int? CategoryId { get; set; }
        public string SearchStr { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }    
}