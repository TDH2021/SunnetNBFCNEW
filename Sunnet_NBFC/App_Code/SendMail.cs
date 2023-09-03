using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Sunnet_NBFC.Models;
using System.IO;
namespace Sunnet_NBFC.App_Code
{
    public class clsSendMail:IDisposable
    {
        public int SendMail(clsMail cls)
        {
            int a = 0;
            string emailSender = ConfigurationManager.AppSettings["username"].ToString();
            string emailSenderPassword = ConfigurationManager.AppSettings["password"].ToString();
            string emailSenderHost = ConfigurationManager.AppSettings["Host"].ToString();
            int emailSenderPort = Convert.ToInt16(ConfigurationManager.AppSettings["portnumber"]);
            Boolean emailIsSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);


            //Base class for sending email  
            MailMessage _mailmsg = new MailMessage();

            //Make TRUE because our body text is html  
            _mailmsg.IsBodyHtml = true;

          
            //Set From Email ID  
            _mailmsg.From = new MailAddress(emailSender);

            //Set To Email ID  
            _mailmsg.To.Add(cls.ToEmail.ToString());

            //if(cls.Ccmail.Trim()!="")
            //{
            //    _mailmsg.CC.Add(cls.Ccmail.ToString().Trim());

            //}
                


            //Set Subject  
            _mailmsg.Subject = cls.Subject;

            //Set Body Text of Email   
            _mailmsg.Body = cls.Body;
            if (cls.AttachFile!= "")
            {
                _mailmsg.Attachments.Add(new Attachment(cls.AttachFile));
            }
            //Now set your SMTP   
            SmtpClient _smtp = new SmtpClient();

            //Set HOST server SMTP detail  
            _smtp.Host = emailSenderHost;

            //Set PORT number of SMTP  
            _smtp.Port = emailSenderPort;

            //Set SSL --> True / False  
            _smtp.EnableSsl = emailIsSSL;
            _smtp.UseDefaultCredentials = true;

            //Set Sender UserEmailID, Password  
            NetworkCredential _network = new NetworkCredential(emailSender, emailSenderPassword);
            _smtp.Credentials = _network;

            //Send Method will send your MailMessage create above.  
            _smtp.Send(_mailmsg);
            return a;


        }

        public string TicketMail(string UserName,string TicketNo,string FilePath)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(FilePath))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Client Name}",UserName);
            body = body.Replace("{TicketNo}", TicketNo);
            return body;
        }


        public string SanctionLetter(string CustName, string ProductName,string Amount, string FilePath)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(FilePath))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{CustName}", CustName);
            body = body.Replace("{ProdName}", ProductName);
            body = body.Replace("{Amount}", Amount);
            return body;
        }




        bool disposed = false;
        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;

        }
    }



}