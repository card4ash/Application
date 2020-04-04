using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProj.Service
{
    public static class Email
    {

        readonly static string css = @"<style type=""text/css"">
            <!--
            body
            {
                color: #373737;
                font-family: Tahoma,Verdana, sans-serif,Arial, Helvetica;
                background-color: #FFFFFF;
                font-size: 13px;
            }

            .title
            {
                font-size: 22px;
                color: #443A8F;
                padding-left: 9px;
                background-repeat: repeat-x;
                line-height: 40px;
                height: 40px;
            }
            .caption
            {
                font-size: 15px;
                color: #443A8F; 
                font-weight:bold;                                               
            }
            .header
            {
                margin-bottom: 10px;
                text-align: center;
                line-height: 70px;
                
                -moz-border-bottom-colors: none;
                -moz-border-left-colors: none;
                -moz-border-right-colors: none;
                -moz-border-top-colors: none;
                background-color: #ffffff;
                background-image: -moz-linear-gradient(center top, #ffffff, #fbfbfb);
                background-repeat: repeat-x;
                border-color: #f5f5f5 #f5f5f5 #f5f5f5;
                border-image: none;
                border-radius: 5px;
                border-style: solid;
                border-width: 1px;
                box-shadow: 0 1px 0 rgba(255, 255, 255, 0.2) inset, 0 1px 2px rgba(0, 0, 0, 0.05);
            }

            table, .table-bordered
            {               
                font-size: 12px;
            }
            .table-bordered
            {
                 width: 100%;
            }

                .table-bordered th
                {
                    background-color: #6b64a4;
                    color: #fff;
                    line-height: 18px;
                    padding: 3px;
                    text-align: left;
                    vertical-align: top;
                }

                .table-bordered td
                {
                    line-height: 18px;
                    padding: 3px;
                    text-align: left;
                    vertical-align: top;
                    background-color: #e0e0e0;
                }

            .btn-primary, .btn-primary:hover, .btn-warning, .btn-warning:hover, .btn-danger, .btn-danger:hover, .btn-success, .btn-success:hover, .btn-info, .btn-info:hover, .btn-inverse, .btn-inverse:hover
            {
                color: #ffffff;
                text-shadow: 0 -1px 0 rgba(0, 0, 0, 0.25);
            }

            .btn
            {
                -moz-border-bottom-colors: none;
                -moz-border-left-colors: none;
                -moz-border-right-colors: none;
                -moz-border-top-colors: none;
                background-color: #f5f5f5;
                background-image: -moz-linear-gradient(center top, #ffffff, #e6e6e6);
                background-repeat: repeat-x;
                border-color: #cccccc #cccccc #b3b3b3;
                border-image: none;
                border-radius: 4px;
                border-style: solid;
                border-width: 1px;
                box-shadow: 0 1px 0 rgba(255, 255, 255, 0.2) inset, 0 1px 2px rgba(0, 0, 0, 0.05);
                color: #333333;
                cursor: pointer;
                display: inline-block;
                font-size: 13px;
                line-height: 18px;
                margin-bottom: 0;
                padding: 4px 10px;
                text-align: center;
                text-shadow: 0 1px 1px rgba(255, 255, 255, 0.75);
                vertical-align: middle;
            }

            .btn-success
            {
                background-color: #5bb75b;
                background-image: -moz-linear-gradient(center top, #62c462, #51a351);
                background-repeat: repeat-x;
                border-color: rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.25);
            }

            .btn-warning
            {
                background-color: #faa732;
                background-image: -moz-linear-gradient(center top, #fbb450, #f89406);
                background-repeat: repeat-x;
                border-color: rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.25);
            }

            .btn-danger
            {
                background-color: #da4f49;
                background-image: -moz-linear-gradient(center top, #ee5f5b, #bd362f);
                background-repeat: repeat-x;
                border-color: rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.1) rgba(0, 0, 0, 0.25);
            }

            a
            {
                color: #0088cc;
                text-decoration: none;
                font-size: 12px;
            }

            .muted
            {
                color: #999999;
            }

            .footer
            {
                font-size: 11px;
            }

            .pull-right
            {
                float: right;
            }

            .row
            {
                margin-top: 10px;
                width: 100%;
                display: table;
            }
            ---->
        </style>";
               
        public static bool Send(string header, string body, string subject, List<string> receiverEmail)
        {
            try
            {
                string mailBody = @"<body><div style=""max-width: 600px; margin-left: 10px; margin-top: 10px;"">"
                   + css
                   + @"<div class=""header"">
                        <span class=""title"">" + header + @"</span>
                      </div>"
                   + body
                   + @"<p class=""muted footer pull-right"">Powered by BRAC ICT </p>"
                   + @"</div>
        </body>";

                var mail = new EmailServiceReferance.EmailWSClient();

                var jobRecipients = new List<EmailServiceReferance.jobRecipients>();

                receiverEmail.ForEach(c => jobRecipients.Add(new EmailServiceReferance.jobRecipients { recipientEmail = c }));

                mail.sendEmail(new EmailServiceReferance.jobs
                {
                    body = mailBody
                    ,
                    caption = subject
                    ,
                    jobRecipients = jobRecipients.ToArray()
                    ,
                    jobContentType = "html"
                    ,
                    subject = subject
                    ,
                    requester = "bcMis"

                });
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
