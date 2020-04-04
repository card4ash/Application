using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppProj.Web.ViewModels
{
    public class ImageGroupDataModel
    {
        /// <summary>
        /// id of group
        /// </summary>
        public int I { get; set; }
        /// <summary>
        /// name of the group
        /// </summary>
        public string N { get; set; }
        /// <summary>
        /// text class
        /// </summary>
        public string C { get; set; }
        /// <summary>
        /// list of image data
        /// </summary>
        public List<ImageDataModel> D { get; set; }
    }

    public class ImageDataModel
    {
        /// <summary>
        /// url
        /// </summary>
        public string U { get; set; }
        /// <summary>
        /// css class
        /// </summary>
        public string C { get; set; }
        /// <summary>
        /// base64 string of image
        /// </summary>
        public string B { get; set; }
        /// <summary>
        /// alter text
        /// </summary>
        public string T { get; set; }      
        /// <summary>
        /// Dialog header
        /// </summary>
        public string H { get; set; }
        /// <summary>
        /// Primary Key
        /// </summary>
        public int I { get; set; }
    }




    public class ImageNoLinkGroupDataModel
    {
        /// <summary>
        /// id of group
        /// </summary>
        public int I { get; set; }
        /// <summary>
        /// name of the group
        /// </summary>
        public string N { get; set; }
        /// <summary>
        /// text class
        /// </summary>
        public string C { get; set; }
        /// <summary>
        /// list of image data
        /// </summary>
        public List<ImageNoLinkDataModel> D { get; set; }
    }

    public class ImageNoLinkDataModel
    {        
        /// <summary>
        /// base64 string of image
        /// </summary>
        public string B { get; set; }
        /// <summary>
        /// text
        /// </summary>
        public string T { get; set; }
        /// <summary>
        /// main button class
        /// </summary>
        public string C { get; set; }
        /// <summary>
        /// url
        /// </summary>
        public string U { get; set; }
        /// <summary>
        /// Dialog header
        /// </summary>
        public string H { get; set; }
        /// <summary>
        /// list of links of image
        /// </summary>
        public List<LinksOfImageDataModel> L { get; set; }
    }

    public class LinksOfImageDataModel
    {
        /// <summary>
        /// product id
        /// </summary>
        public int I { get; set; }
        /// <summary>
        /// product name
        /// </summary>
        public string N { get; set; }     
        /// <summary>
        /// css class
        /// </summary>
        public string C { get; set; }        
        /// <summary>
        /// alter text
        /// </summary>
        public string T { get; set; }
        
        /// <summary>
        /// Date of order
        /// </summary>
        public string D { get; set; }
        /// <summary>
        /// Uom Id
        /// </summary>
        public int U { get; set; }
        /// <summary>
        /// Property Id
        /// </summary>
        public int P { get; set; }
        /// <summary>
        /// Property Name
        /// </summary>
        public string M { get; set; }
    }
}