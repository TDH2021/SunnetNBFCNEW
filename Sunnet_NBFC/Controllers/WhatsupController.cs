using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunnet_NBFC.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Threading.Tasks;
namespace Sunnet_NBFC.Controllers
{
    public class WhatsupController : Controller
    {
        public ActionResult TestAPI()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TestAPI(clsWhatsup cls, HttpPostedFileBase postedFile)
        {

            if (postedFile != null)
            {
                string FileName = Path.GetFileNameWithoutExtension(postedFile.FileName);

                //To Get File Extension
                string FileExtension = Path.GetExtension(postedFile.FileName);

                //Add Current Date To Attached File Name
                FileName = DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim() + FileExtension;

                //Get Upload path from Web.Config file AppSettings.
                string UploadPath = ConfigurationManager.AppSettings["ImgPath"].ToString();

                //Its Create complete path to store in server.
                cls.URL = UploadPath + FileName;
                postedFile.SaveAs(cls.URL);
                cls.FileName = FileName;
                CallAPi(cls);
            }

            return View();

        }

        public string CallAPi(clsWhatsup cls)
        {
            string Baseurl = "http://wapi.tagdigitalsolutions.com/whatsapp/api/send?apikey=&mobile=&msg=";
            string Url = "";
            
            if (cls.URL != "")
            {
                Url = "http://wapi.tagdigitalsolutions.com/whatsapp/api/send?mobile={MobileNo}&msg={Message}&apikey={Key}&pdf={pdf}";
                Url = Url.Replace("{Key}", "c894bc3f7cbf40fca37920d7ae0bfc0f").Replace("{MobileNo}", "9929141894").Replace("{Message}", cls.Message);
                Url =Url.Replace("{pdf}", ConfigurationManager.AppSettings["URL"].ToString()+cls.FileName);
            }
            else
            {
                Url = "http://wapi.tagdigitalsolutions.com/whatsapp/api/send?mobile={MobileNo}&msg={Message}&apikey={Key}";
                Url = Url.Replace("{Key}", "c894bc3f7cbf40fca37920d7ae0bfc0f").Replace("{MobileNo}", "9929141894").Replace("{Message}", cls.Message);
            }

            WebRequest objWebRequest = WebRequest.Create(Url);
            string Response = "";
            WebResponse objWebResponse = objWebRequest.GetResponse();
            Stream objStream = objWebResponse.GetResponseStream();
            StreamReader objStreamReader = new StreamReader(objStream);
            string strHTML = objStreamReader.ReadToEnd();
            Response = strHTML;
            //List <clsWhatsup> EmpInfo = new List<clsWhatsup>();
            //WebClient wb = new WebClient();
            //wb.Headers.Add("user-agent", "Mozilla/4.0(compatible;MSIE6.0;WindowsNT 5.2;.NETCLR1.0.3705;");
            //wb.QueryString.Add("apikey", "c894bc3f7cbf40fca37920d7ae0bfc0f");
            //wb.QueryString.Add("number", "8209721308");
            //wb.QueryString.Add("msg", cls.Message);
            //if (cls.URL != "")
            //{
            //    wb.QueryString.Add("pdf", cls.URL);
            //}
            //Stream data = wb.OpenRead(Baseurl);
            //StreamReader sr = new StreamReader(data);
            //string result = sr.ReadToEnd();

            return Response;
        }
    }
}