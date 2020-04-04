using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppProj.Web.Models
{
    public class ApiModel
    {
        public string Key { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PIN { get; set; }
        public bool HasFever { get; set; }        
        public bool HasBreathingProblem { get; set; }
        public bool HasDryCough { get; set; }
        public bool HasAnyContactWithSuspected { get; set; }
    }
}