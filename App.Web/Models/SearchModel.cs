using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using AppProj.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppProj.Web.Models
{
    public class SearchModel
    {
        public int ContentTypeId1 { get; set; }
        public IEnumerable<SelectListItem> ContentTypes1 { get; set; }

        public int ContentTypeId2 { get; set; }
        public IEnumerable<SelectListItem> ContentTypes2 { get; set; }

        public int ContentTypeId3 { get; set; }
        public IEnumerable<SelectListItem> ContentTypes3 { get; set; }

        public int ContentTypeId4 { get; set; }
        public IEnumerable<SelectListItem> ContentTypes4 { get; set; }

        public int ContentTypeId5 { get; set; }
        public IEnumerable<SelectListItem> ContentTypes5 { get; set; }

        [Required(ErrorMessage = "select")]
        public int RadioButtonId { get; set; }
        public List<RadioButtonHelper> RadioButton { get; set; }

        [Display(Name = "From Date")]
        public Nullable<DateTime> FromDate { get; set; }

        [Display(Name = "To Date")]
        public Nullable<DateTime> ToDate { get; set; }

        public string Code { get; set; }

        public bool IsPostBack { get; set; }
    }

    public class RadioButtonHelper
    {
        public string Id { get; set; }
        public bool Selected { get; set; }
        public string Label { get; set; }
    }
}