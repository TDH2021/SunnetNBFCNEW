using Newtonsoft.Json;
using Sunnet_NBFC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Sunnet_NBFC.App_Code;
namespace Sunnet_NBFC.Controllers
{
    public class CustomerController : Controller
    {

        // GET: Status
        [SessionAttribute]
        public ActionResult AddCustomer()
        {

            try
            {

                using (clsCustomerMaster clsStatus = new clsCustomerMaster())
                {
                    clsStatus.CompanyId = 1;
                }


                    ViewBag.StateList = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");
                
                
            }
            catch (Exception e1)
            {

            }
            return View();
        }

        public string JSONresult { get; private set; }

        [HttpPost]
        [SessionAttribute]
        public JsonResult AddRequestCustomer(clsCustomerMaster cls)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            clsCustomerMaster master = jss.Deserialize<clsCustomerMaster>(Request.Form["AllDataArray"]);

            try
            {

                using (DataTable dt = DataInterface.DBCustomer(master))
                {
                    JSONresult = JsonConvert.SerializeObject(dt);
                }
                return Json(JSONresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e1)
            {
               
                using (clsError clsE = new clsError())
                {
                    clsE.ReqType = "Insert";
                    clsE.Mode = "WEB";
                    clsE.ErrorDescrption = e1.Message;
                    clsE.FunctionName = "AddRequestCustomer";
                    clsE.Link = "Customer/AddRequestCustomer";
                    clsE.PageName = "Customer Controller";
                    clsE.UserId = "1";
                    DataInterface.PostError(clsE);
                }
                return Json(JSONresult, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionAttribute]
        public ActionResult CustomerView()
        {


            List<clsCompanyMaster> lst = new List<clsCompanyMaster>();
            try
            {
                using (clsCustomerMaster cls = new clsCustomerMaster())
                {
                    cls.ReqType = "View";
                    using (DataTable dt = DataInterface.DBCustomer(cls))
                    {
                        if (dt != null)
                        {
                            ViewBag.lst = DataInterface.ConvertDataTable<clsCustomerMaster>(dt);

                        }
                    }
                }
            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "Customer View";
                    clse.Link = "Customer/CustomerView";
                    clse.PageName = "Customer Controller";
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }
            return View();
        }

        [HttpGet]
        [SessionAttribute]
        public ActionResult CustomerDlete(string Id)
        {

            try
            {
                using (clsCompanyMaster cls = new clsCompanyMaster())
                {
                    cls.CompanyId = int.Parse(Id);
                    cls.ReqType = "Delete";
                    using (DataTable dt = DataInterface.DBCompany(cls))
                    {
                        JSONresult = JsonConvert.SerializeObject(dt);
                    }
                }
                
            }
            catch (Exception e1)
            {
                using (clsError clsE = new clsError())
                {
                    clsE.ReqType = "Insert";
                    clsE.Mode = "WEB";
                    clsE.ErrorDescrption = e1.Message;
                    clsE.FunctionName = "Company Status";
                    clsE.Link = "Company/Companydelete";
                    clsE.PageName = "Compnay Controller";
                    clsE.UserId = "1";
                    DataInterface.PostError(clsE);
                }
                return Json(JSONresult, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("CustomerView");


        }
    }
}