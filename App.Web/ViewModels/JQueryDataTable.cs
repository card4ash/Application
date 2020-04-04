using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppProj.Web.ViewModels
{
    public class JQueryDataTable
    {
        public int sEcho { get; set; }
        public string iTotalRecords { get; set; }
        public string iTotalDisplayRecords { get; set; }
        public object[][] aaData { get; set; }
    }
}