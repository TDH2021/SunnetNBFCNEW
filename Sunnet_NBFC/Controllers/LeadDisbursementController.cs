using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunnet_NBFC.Models;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using Sunnet_NBFC.App_Code;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using iText.Layout.Properties;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using System.Web.Services;
using System.Runtime.InteropServices.ComTypes;
using WebGrease.Activities;
using System.Web.Script.Serialization;
using System.Web.ModelBinding;
using static System.Net.WebRequestMethods;
using Ionic.Zip;
//using System.IO.Compression;

namespace Sunnet_NBFC.Controllers
{
    public class LeadDisbursementController : Controller
    {
        [Sunnet_NBFC.App_Code.SessionAttribute]
        public ActionResult LeadView(clsLeadGenerationMaster clss)
        {
            if (Session["UserID"] != null)
            {
                if (String.IsNullOrEmpty(Session["UserID"].ToString()) == true)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();


                    List<clsLeadGenerationMaster> lst = new List<clsLeadGenerationMaster>();
                    try
                    {
                        ViewBag.MainProductList = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("View"), "MainProdId", "ProductName");

                        using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
                        {
                            cls.ReqType = "GetLeadAllData";
                            cls.CompanyId = ClsSession.CompanyID;
                            cls.BranchID = ClsSession.BranchId;
                            cls.MainProductId = clss.MainProductId;
                            cls.ProductId = clss.ProductId;
                            cls.LeadNo = clss.LeadNo;
                            cls.CustomerName = clss.CustomerName;
                            cls.MobileNo1 = clss.MobileNo1;
                            cls.PanNo = clss.PanNo;
                            cls.AadharNo = clss.AadharNo;

                            cls.ShortStage_Name = "Disburse";
                            cls.LeadNo = "";
                            cls.LeadId = 0;
                            cls.Empid = int.Parse(Session["EmpId"].ToString());
                            if (Request.QueryString["ShortStage_Name"] != null)
                            {
                                cls.ShortStage_Name = Request.QueryString["ShortStage_Name"].ToString();
                                cls.StageEmpId = int.Parse(Session["EmpId"].ToString());
                                cls.Empid = 0;
                            }

                            using (DataTable dt = DataInterface.GetLeadGenerationData(cls))
                            {
                                if (dt != null)
                                {
                                    List<clsLeadGenerationMaster> list = new List<clsLeadGenerationMaster>();
                                    list = (from DataRow row in dt.Rows

                                            select new clsLeadGenerationMaster()
                                            {
                                                LeadId = int.Parse(row["LeadId"].ToString()),
                                                LeadNo = row["LeadNo"].ToString(),
                                                MainProductName = row["MainProductName"].ToString(),
                                                ProductName = row["ProductName"].ToString(),
                                                CustomerName = row["CustomerName"].ToString(),
                                                MobileNo1 = row["MobileNo1"].ToString(),
                                                PanNo = row["PanNo"].ToString(),
                                                AadharNo = row["AadharNo"].ToString(),
                                                ReuestedLoanAmount = row["ReuestedLoanAmount"].ToString(),
                                                ReuestedLoanTenure = row["ReuestedLoanTenure"].ToString(),
                                                ShortStage_Name = row["ShortStage_Name"].ToString(),
                                                StatusDesc = row["StatusDesc"].ToString(),

                                            }).ToList();


                                    ViewBag.lst = list;// DataInterface.ConvertDataTable<clsLeadGenerationMaster>(dt);
                                }
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
                            clsE.FunctionName = "Status View";
                            clsE.Link = "Status/ViewStatus";
                            clsE.PageName = "Status Controller";
                            clsE.UserId = "1";
                            DataInterface.PostError(clsE);
                        }
                        throw e1;
                    }

                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }


        [Sunnet_NBFC.App_Code.SessionAttribute]
        public ActionResult ExportToExcel(clsLeadGenerationMaster clss)

        {

            var gv = new GridView();

            using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
            {
                cls.ReqType = "GetLeadAllData";
                cls.CompanyId = ClsSession.CompanyID;
                cls.BranchID = ClsSession.BranchId;
                cls.MainProductId = clss.MainProductId;
                cls.ProductId = clss.ProductId;
                cls.LeadNo = clss.LeadNo;
                cls.CustomerName = clss.CustomerName;
                cls.MobileNo1 = clss.MobileNo1;
                cls.PanNo = clss.PanNo;
                cls.AadharNo = clss.AadharNo;
                //cls.ReqType = "ViewLead";

                cls.ShortStage_Name = "Disburse";
                cls.LeadNo = "";
                cls.LeadId = 0;
                cls.Empid = Session["UserType"].ToString().ToUpper() != "A" ? int.Parse(Session["EmpId"].ToString()) : 0;
                if (Request.QueryString["ShortStage_Name"] != null)
                {
                    cls.ShortStage_Name = Request.QueryString["ShortStage_Name"].ToString();
                    cls.StageEmpId = int.Parse(Session["EmpId"].ToString());
                    cls.Empid = 0;
                }
                using (DataTable dt = DataInterface.GetLeadGenerationData(cls))
                {
                    if (dt != null)
                    {
                        gv.DataSource = dt;

                        gv.DataBind();
                        Response.ClearContent();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment; filename=ReportView.xls");
                        Response.ContentType = "application/ms-excel";
                        Response.Charset = "";

                        StringWriter objStringWriter = new StringWriter();

                        HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

                        gv.RenderControl(objHtmlTextWriter);



                        Response.Output.Write(objStringWriter.ToString());

                        Response.Flush();

                        Response.End();

                    }


                    return RedirectToAction("LeadView");

                }

            }
        }


        //public ActionResult LeadView()
        //{


        //    if (Session["UserID"] != null)
        //    {
        //        if (String.IsNullOrEmpty(Session["UserID"].ToString()) == true)
        //        {
        //            return RedirectToAction("Index", "Login");
        //        }
        //        else
        //        {
        //            List<clsLeadGenerationMaster> lst = new List<clsLeadGenerationMaster>();
        //            try
        //            {
        //                using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
        //                {
        //                    cls.ReqType = "GetLeadAllData";
        //                    cls.CompanyId = ClsSession.CompanyID;
        //                    cls.LeadNo = "";
        //                    cls.LeadId = 0;
        //                    cls.Empid = ClsSession.EmpId;
        //                    cls.ShortStage_Name = "Disburse";
        //                    using (DataTable dt = DataInterface.GetLeadGenerationData(cls))
        //                    {
        //                        if (dt != null)
        //                        {
        //                            ViewBag.lst = DataInterface.ConvertDataTable<clsLeadGenerationMaster>(dt);
        //                        }
        //                    }
        //                }

        //            }
        //            catch (Exception e1)
        //            {
        //                using (clsError clsE = new clsError())
        //                {
        //                    clsE.ReqType = "Get";
        //                    clsE.Mode = "WEB";
        //                    clsE.ErrorDescrption = e1.Message;
        //                    clsE.FunctionName = "LeadView";
        //                    clsE.Link = "LeadDisbursement/LeadView";
        //                    clsE.PageName = "LeadDisbursement Controller";
        //                    clsE.UserId = "1";
        //                    DataInterface.PostError(clsE);
        //                }
        //            }

        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }

        //    return View();
        //}

        [Sunnet_NBFC.App_Code.SessionAttribute]
        public ActionResult LeadDisbursement(int? leadid, string ComeFrom = "Disburse")
        {
            clsDisbursement M = new clsDisbursement();
            try
            {
                DataSet ds = new DataSet();
                DataTable dtLeadDoc = new DataTable();
                if (leadid > 0)
                {
                    M.ReqType = "Get";

                    M.ShortStage_Name = "Disburse";
                    M.LeadId = Convert.ToInt32("0" + leadid.ToString());
                    M.CompanyId = ClsSession.CompanyID;
                    ds = DataInterface1.dbDisbursement(M);
                    if (ds != null && ds.Tables.Count > 0)
                        dtLeadDoc = ds.Tables[0];
                }

                if (dtLeadDoc != null && dtLeadDoc.Rows.Count > 0)
                    M = DataInterface.GetItem<clsDisbursement>(dtLeadDoc.Rows[0]);

                List<clsEmi> Lead = new List<clsEmi>();
                if (ds.Tables.Count > 1)
                {
                    Lead = DataInterface.ConvertDataTable<clsEmi>(ds.Tables[1]);
                }
                ViewBag.EmiList = Lead;
                ViewBag.StatusListDDL = ClsCommon.StatusDDL("Disburse");
                M.Status = M.Status ?? "P";
                ViewBag.ComeFrom = ComeFrom;
                return View(M);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Sunnet_NBFC.App_Code.SessionAttribute]
        [HttpPost]
        public ActionResult LeadDisbursement(clsDisbursement M, FormCollection frm)
        {
            ClsReturnData clsRtn = new ClsReturnData();
            clsRtn.MsgType = (int)MessageType.Fail;
            bool IsSave = false;

            try
            {
                TempData.Clear();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Invalid Model";
                    return View(M);
                }
                if (M.DisbursementId <= 0)
                {
                    M.ReqType = "Insert";
                }
                else
                {
                    M.ReqType = "Update";
                }

                ds = DataInterface1.dbDisbursement(M);
                dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"])) > 0)
                    {
                        IsSave = true;
                    }
                }


                if (IsSave)
                {
                    string slname = DownloadSanctionLetter(M.LeadId, M.LeadNo);
                    string wlname = DownloadWelcomeLetter(M.LeadId, M.LeadNo);
                    string rlname = DownloadRepyamentLetter(M.LeadId, M.LeadNo);
                    try
                    {
                        using (clsFilesLead clsfl = new clsFilesLead())
                        {
                            clsfl.leadid = M.LeadId;
                            clsfl.SanctionLetter = slname;
                            clsfl.WelcomeLetter = wlname;
                            clsfl.RepyamentLetter = rlname;
                            clsfl.ReqType = "Update";
                            DataTable dtupdate = DataInterface1.UpdateLetterFilesInLead(clsfl);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    downloadZipFile(M.LeadNo, slname, wlname, rlname);
                    M.ReqType = "UpdateStatus";
                    M.Status = "A";
                    dt = DataInterface1.UpdateLeadStatusDisburse(M);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        clsRtn.ID = Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"]));
                        clsRtn.Message = Convert.ToString(dt.Rows[0]["ReturnMessage"]);
                        clsRtn.MessageDesc = clsRtn.Message;
                        if (clsRtn.ID > 0)
                        {
                            clsRtn.MsgType = (int)MessageType.Success;
                        }
                        else
                        {
                            clsRtn.MsgType = (int)MessageType.Fail;
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "InsertUpdate";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "LeadDisbursement";
                    clse.Link = "Lead/Lead Disbursement";
                    clse.PageName = "LeadDisbursement Controller";
                    clse.UserId = ClsSession.UserID.ToString();
                    DataInterface.PostError(clse);
                }
            }
            if (IsSave)
            {
                TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Saved/Updated";
                return RedirectToAction("LeadView", "LeadDisbursement");
            }
            else
            {
                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved/Updated";
                return RedirectToAction("LeadDisbursement", new { leadid = M.LeadId });
            }
        }

        [Sunnet_NBFC.App_Code.SessionAttribute]
        public ActionResult LeadDisbursementView(clsDisbursement M)
        {
            List<clsDisbursement> lst = new List<clsDisbursement>();
            try
            {
                DataSet ds = new DataSet();
                DataTable dtLeadDoc = new DataTable();
                M.ReqType = "View";
                M.CompanyId = ClsSession.CompanyID;
                ds = DataInterface1.dbDisbursement(M);
                dtLeadDoc = ds.Tables[0];
                lst = DataInterface.ConvertDataTable<clsDisbursement>(dtLeadDoc);

            }
            catch (Exception ex)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "View";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = ex.Message;
                    clse.FunctionName = "LeadDisbursementView";
                    clse.Link = "LeadDisbursement/LeadDisbursementView";
                    clse.PageName = "LeadDisbursement Controller";
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }

            return PartialView("LeadDisbursementView", lst);
        }


        [Sunnet_NBFC.App_Code.SessionAttribute]
        public JsonResult GetEmijson(decimal _loan, decimal _roi, int _tenure, string _startdate)
        {
            JsonResult result = new JsonResult();
            try
            {
                clsEmi cls = new clsEmi();
                cls.PRINCIPAL = _loan;
                cls.INTEREST = _roi;
                cls.PERIOD = _tenure;
                cls.StartPaymentDate = _startdate;// DateTime.ParseExact(_startdate, "yyyy-MM-dd", null);
                //using (DataSet ds = DataInterface1.dbDisbursement(cls))
                using (DataSet ds = DataInterface1.dbGetEMI(cls))
                {
                    DataTable dt = new DataTable();
                    if (ds != null && ds.Tables.Count > 0)
                        dt = ds.Tables[0];
                    result = this.Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception e1)
            {
                using (clsError cls = new clsError())
                {
                    cls.ReqType = "GetProduct";
                    cls.Mode = "WEB";
                    cls.ErrorDescrption = e1.Message + "-" + e1.InnerException.Message;
                    cls.FunctionName = "GetProduct";
                    cls.Link = "Company/GetProduct";
                    cls.PageName = "Product Controller";
                    cls.UserId = "1";
                    DataInterface.PostError(cls);
                }
                throw e1;
            }

            return result;

        }


        public string DownloadSanctionLetter(int? leadid, string leadno)
        {
            string FileName = "";
            if (leadid > 0)
            {
                FileName = "SancLetter_" + leadno.ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";
                string Filepath = Server.MapPath("~/" + ConfigurationManager.AppSettings["GenLetterPath"].ToString() + "/" + FileName);
                using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
                {
                    cls.ReqType = "GenSanction";
                    cls.LeadId = Convert.ToInt32("0" + leadid.ToString());
                    //cls.LeadNo = "Lead032023000001";
                    using (DataSet ds = DataInterface.DBLetter(cls))
                    {
                        if (ds.Tables.Count == 2)
                        {
                            ds.Tables[0].TableName = "Company";
                            ds.Tables[1].TableName = "Lead";
                            using (clsSanctionLetter cls1 = new clsSanctionLetter())
                            {
                                cls1.GenSanctionLetter(Filepath, "SanctionLetter", ds);
                            }
                        }
                    }
                }

                //if (FileName != "")
                //{
                //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Filepath));
                //    Response.WriteFile(Filepath);
                //    Response.End();
                //}

                return FileName;
            }
            else
            {
                return FileName;
            }

        }


        public string DownloadWelcomeLetter(int? leadid, string leadno)
        {
            string FileName = "";
            if (leadid > 0)
            {
                FileName = "Welcome_" + leadno.ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";
                string Filepath = Server.MapPath("~/" + ConfigurationManager.AppSettings["GenLetterPath"].ToString() + "/" + FileName);
                using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
                {
                    cls.ReqType = "GenWelcome";
                    cls.LeadId = Convert.ToInt32("0" + leadid.ToString());
                    //cls.LeadNo = "Lead032023000001";
                    using (DataSet ds = DataInterface.DBLetter(cls))
                    {
                        if (ds.Tables.Count >= 2)
                        {
                            ds.Tables[0].TableName = "Company";
                            ds.Tables[1].TableName = "Lead";
                            ds.Tables[2].TableName = "Disburse";
                            using (clsWelcomeLetter cls1 = new clsWelcomeLetter())
                            {
                                cls1.GenWelcomeLetter(Filepath, "WelcomeLetter", ds);
                            }
                        }
                    }
                }
                //string path = Filepath;
                //byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);
                //if (FileName != "")
                //{
                //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Filepath));
                //    Response.WriteFile(Filepath);
                //    Response.End();
                //}
                return FileName;
            }
            else
            {
                return FileName;
                //return View();
            }

        }

        public string DownloadRepyamentLetter(int? leadid, string leadno)
        {
            string FileName = "";
            if (leadid > 0)
            {
                FileName = "Repyament_" + leadno.ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";
                string Filepath = Server.MapPath("~/" + ConfigurationManager.AppSettings["GenLetterPath"].ToString() + "/" + FileName);
                using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
                {
                    cls.ReqType = "GenRepyament";
                    cls.LeadId = Convert.ToInt32("0" + leadid.ToString());
                    //cls.LeadNo = "Lead032023000001";
                    using (DataSet ds = DataInterface.DBLetter(cls))
                    {
                        if (ds.Tables.Count > 2)
                        {
                            ds.Tables[0].TableName = "Company";
                            ds.Tables[1].TableName = "Lead";
                            ds.Tables[2].TableName = "Repayment";
                            using (clsRepyament cls1 = new clsRepyament())
                            {
                                cls1.GenRepyament(Filepath, "Repyament", ds);
                            }
                        }
                    }
                }
                //string path = Filepath;
                //byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);

                //if (FileName != "")
                //{
                //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Filepath));
                //    Response.WriteFile(Filepath);
                //    Response.End();
                //}
                return FileName;
            }
            else
            {
                return FileName;
            }
        }

        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
            string path = fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(fileName, "application/pdf");
            //Send the File to Download.
            //return File(bytes, "application/octet-stream", fileName);
        }


        //
        public string downloadZipFile(string leadno, string sl, string wl, string rl)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                string tmpFolderPath = Server.MapPath("~/" + ConfigurationManager.AppSettings["GenLetterPath"].ToString() + "/");
                // string folderpath = ConfigurationManager.AppSettings["SMFFilePDF"].ToString() + lblBCCode.Text.Trim().Replace("'", "") + "\\SMFPDF\\";
                if (sl != "")
                {
                    zip.AddFile(tmpFolderPath + sl, leadno);
                }

                if (wl != "")
                {
                    zip.AddFile(tmpFolderPath + wl, leadno);
                }

                if (rl != "")
                {
                    zip.AddFile(tmpFolderPath + rl, leadno);
                }


                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("{0}.zip", leadno.Trim() + "_" + DateTime.Now.ToString("dd-MMM-yyyy-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);

                Response.End();
                //Mulv.ActiveViewIndex = 0;


                //FillGv();
            }
            return "";
        }
    }
}