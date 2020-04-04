using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppProj.Web.Models
{
    public class MailModel
    {
        public string EmailAddress { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}