using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using EmailApi.Models;
using System.Net.Mail;
using System.Net.Configuration;
using EmailApi.Handler;
namespace EmailApi.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmailController : ApiController
    {
        Service action = new Service();
        [HttpGet]
        public string index()
        {
            return "Email Controller";
        }
        [HttpPost]
        public EmailDatas pimsEmail(EmailSender mailcontent)
        {
            EmailDatas info = new EmailDatas();

            SmtpSection smtp = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            info.UserName = smtp.Network.UserName;
            info.password = smtp.Network.Password;
            info.smtp = smtp.Network.Host;
            info.port = smtp.Network.Port;
            info.EnableSsl = smtp.Network.EnableSsl;
            MailMessage mm = new MailMessage(info.UserName, mailcontent.toAddress);
            //mm.CC.Add("");
            mm.Subject = "RE : PIMS Idea "+mailcontent.subTitle+" - "+mailcontent.ideaID; // mailcontent.subject;
            mm.Body =  "<html><body><style>.button { background - color: #4CAF50; /* Green */ border: none; color: white;padding: 15px 32px; text - align: center;text - decoration: none;display: inline - block;font - size: 16px;}</style>"
            +"<div style = 'background-color:#E9EBEC'><div style ='padding:40px'><div><div style = 'padding:10px;background-color:#00ABEA'><center><h1 style ='color:white'> PIMS Idea "+mailcontent.subTitle+ " - "+mailcontent.ideaID+" </h1></center>"
             + "</div><div style ='padding:20px;background-color:white'><p style = 'text-align:justify'> Hi  <b>"+ mailcontent .resci+ "</b>, <br><br>" + mailcontent.content + "<br/></p>"
              +" <br/> Regards,<br/> "+ mailcontent .sender+ " <hr/> <center><a href ='http:10.14.0.150/PIMS/Form/Login.aspx' class='button'>Login into PIMS</a></center> </div>"
			+"<div style = 'padding:10px;background-color:#00ABEA'><center><h5 style='color:white'>Poclain Idea Management System</h5></center></div></div></div></div></body></html>";

               mm.IsBodyHtml = true;
 
            try
            {
                SmtpClient smtpServer = new SmtpClient();
                smtpServer.Host = info.smtp;
                smtpServer.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(info.UserName, info.password);
                smtpServer.UseDefaultCredentials = true;
                smtpServer.Credentials = NetworkCred;
                smtpServer.Port = info.port;
                smtpServer.Send(mm);
                info.status = "Mail Sent";
                action.WriteEntry("email sent to "+ mailcontent.toAddress+ " for "+mailcontent.subTitle, "Good", "EmailController");
            }
            catch (Exception ex)
            {
                info.status = "Something went wrong while sending email !";
                info.errorInfo = ex.ToString();
                action.WriteEntry(ex.ToString(), "Error", "EmailController");
            }



            return info;
        }
    }
}
