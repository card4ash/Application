using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppProj.Web.Models
{
    public class StandingDataModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string StringValue { get; set; }
        public bool IsActive { get; set; }
    }
}