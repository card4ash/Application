using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AppProj.Domain;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppProj.Web.Models
{
    public class ConfigModel
    {
        [Key]
        public int UnitId { get; set; }
        public Nullable<int> LastUniqueCode { get; set; }
    }
}
