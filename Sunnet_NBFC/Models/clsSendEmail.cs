
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using Org.BouncyCastle.Asn1.Crmf;

namespace Sunnet_NBFC.Models
{
    public class clsMail : IDisposable
    {
        public string Subject { get; set; }
        public string ToEmail { get; set; }
        public string Ccmail { get; set; }
        public string Body { get; set; }
        public string BodyHtml { get; set; }
        public string AttachFile { get; set; }
        
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



    //public class clsMailTemplate : IDisposable
    //{
    //    public string FilePath { get; set; } = "";
    //    public string ToMail { get; set; } = "";
    //    public string CCMail { get; set; } = "";
    //    public string Body { get; set; } = "";
    //    public string Subject { get; set; } = "";
    //    public string Attachment { get; set; } = "";

    //}
    public class clsWhatsup
    {
        public string Message { get; set; } = "";
        public string URL { get; set; } = "";
        public string FileName { get; set; } = "";
    }

}