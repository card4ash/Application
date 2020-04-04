using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Models
{
    public class ReportModel
    {
        public Nullable<int> ContentTypeId1 { get; set; }
        public IEnumerable<SelectListItem> ContentTypes1 { get; set; }

        public Nullable<int> ContentTypeId2 { get; set; }
        public IEnumerable<SelectListItem> ContentTypes2 { get; set; }

        public Nullable<int> ContentTypeId3 { get; set; }
        public IEnumerable<SelectListItem> ContentTypes3 { get; set; }

        public Nullable<int> ContentTypeId4 { get; set; }
        public IEnumerable<SelectListItem> ContentTypes4 { get; set; }

        public Nullable<int> ContentTypeId5 { get; set; }
        public IEnumerable<SelectListItem> ContentTypes5 { get; set; }

        public Nullable<int> ContentTypeId6 { get; set; }
        public IEnumerable<SelectListItem> ContentTypes6 { get; set; }

        public Nullable<int> ContentTypeId7 { get; set; }
        public IEnumerable<SelectListItem> ContentTypes7 { get; set; }

        [Required(ErrorMessage = "select")]
        public Nullable<int> RadioButtonId { get; set; }
        public List<RadioButtonHelper> RadioButton { get; set; }

        [Display(Name = "From Date")]
        public Nullable<DateTime> FromDate { get; set; }

        [Display(Name = "To Date")]
        public Nullable<DateTime> ToDate { get; set; }
        
        public bool IsPostBack { get; set; }

        public int userId { get; set; }
    }
}