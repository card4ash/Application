using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AppProj.Web.Models
{
    public class RoleFeatureModel
    {
       
            [Required]
            [Display(Name = "Role")]
            public int RoleId { get; set; }

            [Required]
            [Display(Name = "Feature")]
            public int FeatureId { get; set; }
    }
}