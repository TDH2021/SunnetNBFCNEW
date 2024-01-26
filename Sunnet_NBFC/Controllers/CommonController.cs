using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using Sunnet_NBFC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using Sunnet_NBFC.App_Code;

namespace Sunnet_NBFC.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetSubProduct(string MainProductId, string ProductId)
        {
            JsonResult result = new JsonResult();

            try
            {

                using (clsProduct cls = new clsProduct())
                {
                    cls.ReqType = "ViewProduct";
                    cls.MainProdId = int.Parse(MainProductId);
                    cls.ProdId = int.Parse(ProductId);
                    using (DataTable dt = DataInterface1.GetProduct(cls))
                    {
                        result = this.Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);

                    }


                }

            }
            catch (Exception e1)
            {
                using (clsError cls = new clsError())
                {
                    cls.ReqType = "GetProduct";
                    cls.Mode = "WEB";
                    cls.ErrorDescrption = e1.Message.ToString();
                    cls.FunctionName = "GetProduct";
                    cls.Link = "Company/GetProduct";
                    cls.PageName = "Product Controller";
                    cls.UserId = "1";
                    DataInterface.PostError(cls);
                }
            }

            return result;

        }

        public JsonResult GetPinCode(string PinCode)
        {
            JsonResult result = new JsonResult();

            try
            {



                Uri objURI = new Uri(WebConfigurationManager.AppSettings["RapidPostalAPI"].ToString());
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                WebRequest objWebRequest = WebRequest.Create(objURI);

                objWebRequest.Headers.Add("X-RapidAPI-Key", WebConfigurationManager.AppSettings["RapidKey"].ToString());
                objWebRequest.Headers.Add("X-RapidAPI-Host", "pincode.p.rapidapi.com");
                objWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(objWebRequest.GetRequestStream()))
                {
                    string json = "{\"searchBy\":\"pincode\"," +
                                  "\"value\":\"" + PinCode + "\"}";

                    streamWriter.Write(json);
                }

                WebResponse objWebResponse = objWebRequest.GetResponse();
                Stream objStream = objWebResponse.GetResponseStream();
                StreamReader objStreamReader = new StreamReader(objStream);
                var httpResponse = (HttpWebResponse)objWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result1 = streamReader.ReadToEnd();
                    using (DataTable dt = ClsCommon.ConvertJsonToDataTable(result1))
                    {
                        if(dt.Rows.Count>0)
                        {
                            result = this.Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);

                        }
                        else
                        {
                            result = this.Json("", JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                
              
            }
            catch (Exception e1)
            {
                using (clsError cls = new clsError())
                {
                    cls.ReqType = "GetProduct";
                    cls.Mode = "WEB";
                    cls.ErrorDescrption = e1.Message.ToString();
                    cls.FunctionName = "GetProduct";
                    cls.Link = "Company/GetProduct";
                    cls.PageName = "Product Controller";
                    cls.UserId = "1";
                    DataInterface.PostError(cls);
                }
            }

            return result;

        }
    }

}
