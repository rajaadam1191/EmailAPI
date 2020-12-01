using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Configuration;
using System.Web.Http;
using EmailApi.Models;
using EmailApi.Handler;

using System.Net.Mail;
using System.Net.Configuration;
using System.IO;
using System.Diagnostics;
using System.Web.Mail;

namespace EmailApi.Controllers
{
 
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : ApiController
    {
        Service action = new Service();
        [HttpGet]
 
        public string Hellow()
        {
            action.WriteEntry("Home controller loaded", "Good", "HomeController");
            return "Hellow Mani";
        }
        [HttpPost]
 
        public UserInfo posted(UserInfo info)
        {
            action.WriteEntry(info.ToString(), "Good", "HomeController");
            return info;
        }

        [HttpGet]
         public EmailDatas Smtpinfo()
        {
            EmailDatas info = new EmailDatas();
            
            SmtpSection smtp = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            info.UserName = smtp.Network.UserName;
            info.password = smtp.Network.Password;
            info.smtp = smtp.Network.Host;
            info.port = smtp.Network.Port;
            info.EnableSsl = smtp.Network.EnableSsl;
            System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(info.UserName, "manikandan.r@binary2quantum.com");
            //mm.CC.Add("manikandan.r@binary2quantum.com,manikandan.ramsaro@gmail.com"); -- example for CC
            mm.Subject = "Test Email using Web APi";
            mm.Body = string.Format("Dear {0},<br /><br />Test email from Dot NEt web api", "Mani");
            mm.IsBodyHtml = true;
            //if (fuAttachment.HasFile)
            //{
            //    string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
            //    mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
            //}
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
                action.WriteEntry("Test email sent to manikandan.r@binary2quantum.com", "Good", "HomeController");
            }
            catch (Exception ex)
            {
                info.status = "Something went wrong while sending email !";
                info.errorInfo = ex.ToString();
                action.WriteEntry(ex.ToString(), "Error", "HomeController");
            }
          
                
             
            return info;
        }
        [HttpGet]
        public EmailDatas sendUsingIP()
        {
            EmailDatas info = new EmailDatas();

            SmtpSection smtp = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            info.UserName = smtp.Network.UserName;
            info.password = smtp.Network.Password;
            info.smtp = smtp.Network.Host;
            info.port = smtp.Network.Port;
            info.EnableSsl = smtp.Network.EnableSsl;
            try
            {
                System.Web.Mail.MailMessage Msg = new System.Web.Mail.MailMessage();
                Msg.From = info.UserName;
                // Recipient e-mail address.
                Msg.To = "manikandan.r@binary2quantum.com";
                Msg.Subject = "Test email from Poclain";
                Msg.Body = "Test email sent without Password";
                // your remote SMTP server IP.
                SmtpMail.SmtpServer = info.smtp;//your ip address
                SmtpMail.Send(Msg);
                Msg = null;
                action.WriteEntry("Test email sent to manikandan.r@binary2quantum.com", "Good", "HomeController");
            }
            catch (Exception ex)
            {
                info.status = "Something went wrong while sending email !";
                info.errorInfo = ex.ToString();
                action.WriteEntry(ex.ToString(), "Error", "HomeController");
            }



            return info;
        }
        [HttpPost]
        public UserInfo SendEmailFromWebfrom(UserInfo infos)
        {
            EmailDatas info = new EmailDatas();

            SmtpSection smtp = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            info.UserName = smtp.Network.UserName;
            info.password = smtp.Network.Password;
            info.smtp = smtp.Network.Host;
            info.port = smtp.Network.Port;
            info.EnableSsl = smtp.Network.EnableSsl;
            System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(info.UserName, infos.email);
            //mm.cc.add("manikandan.r@binary2quantum.com,manikandan.ramsaro"); -- example for CC
           // mm.Subject = "Test Email using Web APi from webform";
           // mm.Body = ("Dear {0},<br /><br />Test email from Dot NEt web api OTP=" +infos.password);

            mm.Subject = "RE : PIMS  " + infos.UserId + " - " + infos.UserName; // mailcontent.subject;
            mm.Body = "<html><body><style>.button { background - color: #4CAF50; /* Green */ border: none; color: white;padding: 15px 32px; text - align: center;text - decoration: none;display: inline - block;font - size: 16px;}</style>"
            + "<div style = 'background-color:#E9EBEC'><div style ='padding:40px'><div><div style = 'padding:10px;background-color:#00ABEA'><center><h1 style ='color:white'> PIMS" + infos.process + "For" + infos.UserId + " - " + infos.UserName + " </h1></center>"
             + "</div><div style ='padding:20px;background-color:white'><p style = 'text-align:justify'> Hi  <b>" + infos.UserName + "</b>, <br><br>" + infos.email + "<br/></p>"
              + " <br/> OTP= " + infos.password + " <hr/> <center><a href ='http:10.14.0.150/PIMS/Form/Login.aspx' class='button'>Login into PIMS</a></center> </div>"
            + "<div style = 'padding:10px;background-color:#00ABEA'><center><h5 style='color:white'>Poclain Idea Management System</h5></center></div></div></div></div></body></html>";


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
                smtpServer.Send(mm)
;
                info.status = "Mail Sent";
                action.WriteEntry("Test email sent to manikandan.r@binary2quantum.com", "Good", "HomeController");
            }
            catch (Exception ex)
            {
                infos.password = "Something went wrong while sending email !";
                info.errorInfo = ex.ToString();
                action.WriteEntry(ex.ToString(), "Error", "HomeController");
            }

            infos.password = "Mail Sent to " + infos.UserName;

            return infos;
        }
    }
}
