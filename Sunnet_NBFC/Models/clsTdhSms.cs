using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;

namespace Sunnet_NBFC.Models
{
    public class clsTdhSms : IDisposable
    {
        public string SendSms(string url)
        {
            string returnstr = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //response.ContentType = "application/json; charset=utf-8";
                StreamReader reader = new StreamReader(response.GetResponseStream());
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    returnstr = sr.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                returnstr = ex.ToString();
            }
            return returnstr;
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



    public class clsSMSMaster : IDisposable
    {
        public string ReqType { get; set; }
        public int SmsId { get; set; }
        public string ClientId { get; set; }
        public string TemplateId { get; set; }
        public string URL { get; set; }
        public string SMSType { get; set; }
        public int SendSMS { get; set; }
        public string Method { get; set; }
        public string SMS { get; set; }
        
        public int LeadId { get; set; }

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