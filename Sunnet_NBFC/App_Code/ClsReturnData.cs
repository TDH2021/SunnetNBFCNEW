using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunnet_NBFC.App_Code
{
    public class ClsReturnData:IDisposable
    {
        public int MsgType { get; set; }
        public long ID { get; set; }
        public string Message { get; set; }
        public string MessageDesc { get; set; }
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

        ~ClsReturnData()
        {
            Dispose(false);
        }

    }

    public enum MessageType
    {
        Unknown,
        Success,
        Fail,
        Error
        
    }

    
}

