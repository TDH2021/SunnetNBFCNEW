using Newtonsoft.Json;
using Sunnet_NBFC.App_Code;
using Sunnet_NBFC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Configuration;
namespace Sunnet_NBFC.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Status
        [SessionAttribute]
        public ActionResult AddCompany()
        {
            using (clsCompanyMaster clsStatus = new clsCompanyMaster())
            {
                clsStatus.CompanyId = ClsSession.CompanyID;
            }

            ViewBag.StateList = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");

            return View();
        }

        public string JSONresult { get; private set; }


        [HttpPost]
        [SessionAttribute]
        public JsonResult AddRequestCompany(clsCompanyMaster cls)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            clsCompanyMaster master = jss.Deserialize<clsCompanyMaster>(Request.Form["AllDataArray"]);
           

            try
            {

                using (DataTable dt = DataInterface.DBCompany(master))
                {
                    JSONresult = JsonConvert.SerializeObject(dt);
                    HttpPostedFileBase file = null;
                    if (Request.Files[0] != null)
                    {
                        file = Request.Files["LOGO"];
                        //Extract Image File Name.
                        string fileName = System.IO.Path.GetFileName(file.FileName);

                        //Set the Image File Path.
                        string filePath = ConfigurationManager.AppSettings["ImgPath"];

                        //Save the Image File in Folder.
                        file.SaveAs(filePath);
                        file.SaveAs(Server.MapPath(filePath + fileName));

                    }
                }
                return Json(JSONresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e1)
            {
               
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "AddRequestCompany";
                    clse.Link = "Status/AddRequestCompany";
                    clse.PageName = "Company Controller";
                    clse.UserId =ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
                return Json(JSONresult, JsonRequestBehavior.AllowGet);
            }
        }

        [SessionAttribute]
        public ActionResult CompanyView(clsCompanyMaster cls)
        {

            List<clsCompanyMaster> lst = new List<clsCompanyMaster>();
            try
            {
                cls.ReqType = "View";
                using (DataTable dt = DataInterface.DBCompany(cls))
                {
                    if (dt != null)
                    {

                        List<clsCompanyMaster> list = new List<clsCompanyMaster>();
                        list = (from DataRow row in dt.Rows

                                select new clsCompanyMaster()
                                {
                                    CompanyId = int.Parse(row["CompanyId"].ToString()),
                                    CompanyName = row["CompanyName"].ToString(),
                                    Address= row["Address"].ToString(),
                                    CityName = row["CityName"].ToString(),
                                    StateName = row["StateName"].ToString(),
                                    Country = row["Country"].ToString(),
                                    PinCode = row["PinCode"].ToString(),
                                    PANNo = row["PANNo"].ToString(),
                                    GSTNo = row["GSTNo"].ToString(),
                                    CompanyType = row["CompanyType"].ToString(),
                                    LOGO = row["Logo"].ToString(),
                                    CINNo = row["CINNo"].ToString(),
                                    DateofIncorporation = row["DateofIncorporation"].ToString(),
                                    CompanyDesc = row["CompanyDesc"].ToString(),


                                }).ToList();
                        ViewBag.lst = list;
                        //ViewBag.lst = DataInterface.ConvertDataTable<clsCompanyMaster>(dt);


                    }
                }
            }
            catch (Exception e1)
            {
                using (clsError clsError = new clsError())
                {
                    clsError.ReqType = "I";
                    clsError.Mode = "WEB";
                    clsError.ErrorDescrption = e1.Message;
                    clsError.FunctionName = "Comoany View";
                    clsError.Link = "Company/CompanyView";
                    clsError.PageName = "Company Controller";
                    clsError.UserId = "1";
                    DataInterface.PostError(clsError);
                }
            }
            return View();
        }
        [HttpGet]
        [SessionAttribute]
        public ActionResult Companydelete(string Id)
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
                        ViewBag.Message = dt.Rows[0]["ReturnMessage"].ToString();
                    }
                }
                return RedirectToAction("CompanyView");

            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "Company Status";
                    clse.Link = "Company/Companydelete";
                    clse.PageName = "Compnay Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
                return Json(JSONresult, JsonRequestBehavior.AllowGet);
            }
          

        }


    }
}