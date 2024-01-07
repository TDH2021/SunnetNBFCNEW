using Newtonsoft.Json;
using Sunnet_NBFC.App_Code;
using Sunnet_NBFC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Configuration;
using System.IO;
using System.Diagnostics;

namespace Sunnet_NBFC.Controllers
{
    public class LeadGenerationController : Controller
    {
        // GET: LeadGeneration

        public ActionResult LeadGeneration()
        {

            try
            {
                ViewBag.CompanyId = ClsSession.CompanyID;
                using (clsLeadGenerationMaster clsStatus = new clsLeadGenerationMaster())
                {
                    clsStatus.CompanyId = ClsSession.CompanyID;
                }


                ViewBag.StateList = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");




                ViewBag.MainProductList = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("View"), "MainProdId", "ProductName");
                ViewBag.MaterialStatusList = ClsCommon.ToSelectList(DataInterface1.GetMiseddl("Martial Status"), "MiscName", "MiscName");
                ViewBag.ProductId = "";
                using (clsCenter cls = new clsCenter())
                {
                    cls.ReqType = "View";
                    cls.CompanyID = ClsSession.CompanyID;
                    ViewBag.CenterList = ClsCommon.ToSelectList(DataInterface2.DBCenter(cls), "CenterId", "CenterName");
                }

                using (clsBranch cls = new clsBranch())
                {
                    cls.ReqType = "View";
                    cls.CompanyID = ClsSession.CompanyID;
                    cls.IsDelete = 0;
                    ViewBag.BranchList = ClsCommon.ToSelectList(DataInterface2.ViewBranch(cls), "BranchId", "BranchName");
                }
                ViewBag.RelationList = ClsCommon.ToSelectList(DataInterface2.GetMiscForDDL("Relation"), "MiscId", "MiscName");
                ViewBag.Colletral = ClsCommon.ToSelectList(DataInterface2.GetMiscForDDL("Colletral"), "MiscId", "MiscName");
                using (clsDSAMaster cls = new clsDSAMaster())
                {
                    cls.ReqType = "View";
                    using (DataTable dt1 = DataInterface.DBDSAMaster(cls))
                    {
                        if (dt1 != null)
                        {
                            ViewBag.Dealer = ClsCommon.ToSelectList(dt1, "DSAId", "DSAName");


                        }
                    }
                }
                List<clsLeadGenerationMaster> LeadGenerationdataModels = new List<clsLeadGenerationMaster>();


                ViewBag.LeadGenerationdataModels = LeadGenerationdataModels;
                ViewBag.PostalApi = ConfigurationManager.AppSettings["PostalAPI"].ToString();
                DataTable dt = DataInterface1.GetKeyMaster();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Key"].ToString() == "Aadhar")
                    {
                        ViewBag.AAdharClientID = dt.Rows[i]["ClientID"].ToString();
                        ViewBag.AAdharClientScreate = dt.Rows[i]["ClientSecretKey"].ToString();
                        ViewBag.AAdharIsAPICheck = dt.Rows[i]["IsAPICheck"].ToString();
                        ViewBag.APIName = dt.Rows[i]["APIName"].ToString();
                        if (i == 0)
                        {
                            ViewBag.AadharAPi = dt.Rows[i]["APIURL"].ToString();
                        }
                        if (i == 1)
                        {
                            ViewBag.AadharAPIOTP = dt.Rows[i]["APIURL"].ToString();
                        }
                    }
                    if (dt.Rows[i]["Key"].ToString() == "PAN")
                    {
                        ViewBag.PANClientID = dt.Rows[i]["ClientID"].ToString();
                        ViewBag.PANClientScreate = dt.Rows[i]["ClientSecretKey"].ToString();
                        ViewBag.PANIsAPICheck = dt.Rows[i]["IsAPICheck"].ToString();
                        ViewBag.PANAPIName = dt.Rows[i]["APIName"].ToString();
                        if (i == 2)
                        {
                            ViewBag.PanAPi = dt.Rows[i]["APIURL"].ToString();
                        }
                        if (i == 3)
                        {
                            ViewBag.PanAPIOTP = dt.Rows[i]["APIURL"].ToString();
                        }
                    }
                }

            }
            catch (Exception e1)
            {
                using (clsError clsE = new clsError())
                {
                    // Get stack trace for the exception with source file information
                    var st = new StackTrace(e1, true);
                    // Get the top stack frame
                    var frame = st.GetFrame(0);
                    // Get the line number from the stack frame
                    var line = frame.GetFileLineNumber();
                    clsE.ReqType = "Insert";
                    clsE.Mode = "WEB";
                    clsE.ErrorDescrption = e1.Message + "Line " + line + "Frame " + frame;
                    clsE.FunctionName = "AddRequestLead";
                    clsE.Link = "Status/AddRequestLead";
                    clsE.PageName = "Status Controller";
                    clsE.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clsE);
                }
            }
            return View();
        }

        public string JSONresult { get; private set; }


        [HttpPost]
        public JsonResult AddRequestLeadGeneration(clsLeadGenerationMaster cls)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            clsLeadGenerationMaster master = jss.Deserialize<clsLeadGenerationMaster>(Request.Form["AllDataArray"]);
            List<Gurantor> Gurantor_Details = jss.Deserialize<List<Gurantor>>(Request.Form["Gurantor_Details"]);


            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    try
                    {
                        master.CompanyId = ClsSession.CompanyID;
                        master.BranchID = ClsSession.BranchId;
                        master.CreatedBy = ClsSession.EmpId;
                        string message = "";

                        if (master.MainProductId == "1")
                        {
                            master.ViechleRegYear = "0";
                            master.MFGYear = "0";
                            master.EstValueViechle = "0";
                        }
                        if (master.MainProductId == "1")
                        {
                            master.NoofDependent = "0";
                            master.FORecomedAmt = "0";
                        }
                        if (master.MainProductId == "4")
                        {
                            master.EstValueViechle = "0";
                            master.DSAId = "0";
                        }
                        using (DataTable dt = DataInterface.GetLeadGeneration(master))
                        {

                            foreach (DataRow row in dt.Rows)
                            {

                                message = row["ReturnMessage"].ToString();
                                master.LeadId = int.Parse(row["ReturnID"].ToString());
                                master.LeadNo = row["LeadNo"].ToString();

                            }
                            if (message == "Lead saved succussfully")
                            {

                                HttpPostedFileBase file = null;
                                if (Request.Files[0] != null)
                                {
                                    file = Request.Files["ApplicantImg"];
                                    //Extract Image File Name.
                                    string fileName = System.IO.Path.GetFileName(file.FileName);

                                    fileName = master.LeadNo + "_Applicant_" + fileName;

                                    //Set the Image File Path.
                                    string filePath = Server.MapPath("~/Img/ApplicantImgs");

                                    //Save the Image File in Folder.
                                    file.SaveAs(filePath + "\\" + fileName);
                                    master.CustImage = fileName;

                                }

                                if (Request.Files[1] != null)
                                {
                                    file = Request.Files["AppElectricBill"];
                                    //Extract Image File Name.
                                    string fileName = System.IO.Path.GetFileName(file.FileName);

                                    fileName = master.LeadNo + "_ElectricBill_" + fileName;

                                    //Set the Image File Path.
                                    string filePath = Server.MapPath("~/Img/ApplicantImgs");

                                    //Save the Image File in Folder.
                                    file.SaveAs(filePath + "\\" + fileName);
                                    master.ElectricBill = fileName;

                                }
                                DataTable dt1 = DataInterface.GetLeadGenerationCustomer(master);

                                if (master.Hdn_type.ToUpper() == "B" || master.Hdn_type.ToUpper() == "C")
                                {
                                    if (Request.Files[2] != null)
                                    {
                                        file = Request.Files["COApplicantImg"];
                                        //Extract Image File Name.
                                        string fileName = System.IO.Path.GetFileName(file.FileName);

                                        fileName = master.LeadNo + "_CO_Applicant_" + fileName;

                                        //Set the Image File Path.
                                        string filePath = Server.MapPath("~/Img/COApplicantImgs");

                                        //Save the Image File in Folder.
                                        file.SaveAs(filePath + "\\" + fileName);
                                        master.CustImage = fileName;
                                    }

                                    if (Request.Files[3] != null)
                                    {
                                        file = Request.Files["COElectricBill"];
                                        //Extract Image File Name.
                                        string fileName = System.IO.Path.GetFileName(file.FileName);

                                        fileName = master.LeadNo + "_CO_ElectricBill_" + fileName;

                                        //Set the Image File Path.
                                        string filePath = Server.MapPath("~/Img/COApplicantImgs");

                                        //Save the Image File in Folder.
                                        file.SaveAs(filePath + "\\" + fileName);
                                        master.ElectricBill = fileName;
                                    }
                                    dt1 = DataInterface.GetLeadGenerationCO_ApplicantCustomer(master);

                                }


                                for (int i = 0; i < Gurantor_Details.Count; i++)
                                {
                                    Gurantor_Details[i].G_CompanyId = ClsSession.CompanyID;
                                    Gurantor_Details[i].G_BranchID = ClsSession.BranchId;
                                    Gurantor_Details[i].G_LeadId = master.LeadId;
                                    Gurantor_Details[i].G_CreatedBy = ClsSession.EmpId;
                                    if (master.Hdn_type.ToString().ToUpper() == "B" || master.Hdn_type.ToUpper() == "G")
                                    {
                                        dt1 = DataInterface.GetLeadGenerationGurantorCustomer(Gurantor_Details[i]);
                                    }

                                }
                            }

                            JSONresult = JsonConvert.SerializeObject(dt);
                        }
                        scope.Complete();
                        return Json(JSONresult, JsonRequestBehavior.AllowGet);

                    }
                    catch (Exception e1)
                    {
                        scope.Dispose();
                        using (clsError clsE = new clsError())
                        {
                            clsE.ReqType = "Insert";
                            clsE.Mode = "WEB";
                            clsE.ErrorDescrption = e1.Message;
                            clsE.FunctionName = "AddRequestLead";
                            clsE.Link = "Status/AddRequestLead";
                            clsE.PageName = "Status Controller";
                            clsE.UserId = ClsSession.EmpId.ToString();
                            DataInterface.PostError(clsE);
                        }
                    }

                    return Json(JSONresult, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e1)
            {
                DataTable dt = new DataTable();
                using (clsError clsE = new clsError())
                {
                    clsE.ReqType = "Insert";
                    clsE.Mode = "WEB";
                    clsE.ErrorDescrption = e1.Message;
                    clsE.FunctionName = "AddRequestStatus";
                    clsE.Link = "Status/AddStatus";
                    clsE.PageName = "Status Controller";
                    clsE.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clsE);
                }


            }

            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCustomerData(string cfid, string Aadharno, string PanNo)
        {
            JsonResult result = new JsonResult();
            if (Aadharno.Trim() != "" || PanNo.Trim() != "")
            {

                DataTable dt = DataInterface1.GetCustomerData(cfid, Aadharno, PanNo);

                result = this.Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            }
            else
            {
                result = this.Json("", JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        public JsonResult SetApiResponse(string stage, string OTP, string referenceId, string AadharNo, string email, string urlvariable, string responseCode, string responseMessage, string response)
        {
            JsonResult result = new JsonResult();

            string request = "";
            string apiname = "";

            if (stage == "1")
            {
                request = "{aadhaarNumber:" + AadharNo + ", email:" + email + ", url:" + urlvariable + "}";
                apiname = "Aadhar Verify";

            }
            else if (stage == "2")
            {
                request = "{otp:" + OTP + ", refrenceid:" + referenceId + ", url:" + urlvariable + "}";
                apiname = "Aadhar OTP Verify";

            }


            DataTable dt = DataInterface1.SetApiResponse(request, responseCode, responseMessage, response, apiname);

            result = this.Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);

            return result;

        }


        public JsonResult SetPANApiResponse(string stage, string ApiName, string OTP, string referenceId, string Panno, string email, string urlvariable,
                             string responseCode, string responseMessage, string response)
        {
            JsonResult result = new JsonResult();

            string request = "";

            if (stage == "1")
            {
                request = Panno;


            }
            else if (stage == "2")
            {
                request = Panno;


            }


            DataTable dt = DataInterface1.SetApiResponse(request, responseCode, responseMessage, response, ApiName);

            result = this.Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);

            return result;

        }
    }
}