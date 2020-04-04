using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppProj.Web.ViewModels
{    
    public class GridButtonModel
    {
        /// <summary>
        /// url
        /// </summary>
        public string U { get; set; }
        /// <summary>
        /// text or value of button
        /// </summary>
        public string T { get; set; }
        /// <summary>
        /// Css Class
        /// </summary>       
        public string D { get; set; }
        /// <summary>
        /// Dialog header
        /// </summary>
        public string H { get; set; }
        /// <summary>
        /// Is it an Ajax Button
        /// </summary>
        public bool? A { get; set; }
        /// <summary>
        /// button icon class
        /// </summary>
        public string I { get; set; }
        /// <summary>
        /// button html attribute
        /// </summary>
        public string M { get; set; }
        /// <summary>
        /// button visible
        /// </summary>
        public bool? V { get; set; }

    }

    public static class GridButtonDialog
    {
        public static string mainBody { get { return "mb"; } }
        public static string dialig1 { get { return "d1"; } }
        public static string dialig2 { get { return "d2"; } }
    }
}