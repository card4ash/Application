using AppProj.Service;
using AppProj.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Api
{
    public class SendMailController : Controller
    {
        //
        // GET: /SendMail/
        [HttpGet]
        public JsonResult Index()
        {
            var fromAddress = new MailAddress("imailservice@brac.net", "BRAC Project");            
            const string fromPassword = "Br@c$mtp";
            

            SqlExecutor exec = new SqlExecutor();
            SqlParameter[] par;

            par = new SqlParameter[0];

            DataTable dt = exec.ExecStoredProcedureWithReturn("[dbo].[SprMailSend]", par);

            var data = (from rw in dt.AsEnumerable()
                                 select new MailModel()
                                 {
                                     EmailAddress = Convert.ToString(rw["addr"]),
                                     Body = Convert.ToString(rw["mail"]),
                                     Subject = Convert.ToString(rw["sub"])
                                 }).ToList();


            var smtp = new SmtpClient
            {
                Host = "smtp-relay.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)                 
            };

            foreach (var r in data)
            {
                var toAddress = new MailAddress(r.EmailAddress);
                
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = r.Subject,
                    Body = r.Body,
                    IsBodyHtml=true
                })
                {
                    message.ReplyToList.Add(new MailAddress("hossain.sm@brac.net", "Hossain Shahid Mufti"));
                    smtp.Send(message);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }



    }
}
