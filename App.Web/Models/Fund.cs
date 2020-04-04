using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppProj.Web.Models
{
    public class Fund
    {
        public string Program { get; set; }
        public string Project { get; set; }
        public string RunYr { get; set; }
        public string PrjLn { get; set; }

        public decimal? Proposed { get; set; }

        public string Date1 { get; set; }
        public decimal? Amount1 { get; set; }
        public string Date2 { get; set; }
        public decimal? Amount2 { get; set; }

        public decimal? Amount { get; set; }
        public string Amnt { get; set; }
    }
}