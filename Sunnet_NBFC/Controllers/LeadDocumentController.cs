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
using System.Web.Script.Serialization;
using System.Web.ModelBinding;

namespace Sunnet_NBFC.Controllers
{
    public class LeadDocumentController : Controller
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

                            cls.ShortStage_Name = "DocCol";
                            
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

                cls.ShortStage_Name = "DocCol";
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
        //                    cls.ShortStage_Name = "DocCol";
        //                    cls.StageEmpId = int.Parse(Session["EmpId"].ToString());
        //                    cls.Empid = 0;


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
        //                    clsE.ReqType = "Insert";
        //                    clsE.Mode = "WEB";
        //                    clsE.ErrorDescrption = e1.Message;
        //                    clsE.FunctionName = "Status View";
        //                    clsE.Link = "Status/ViewStatus";
        //                    clsE.PageName = "Status Controller";
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
        public ActionResult LeadDocument(clsLeadmaind M, string ComeFrom = "DocCol")
        {
            try
            {
                DataTable dtLeadDetail = new DataTable();
                DataTable dtLeadDoc = new DataTable();
                if (M.LeadId > 0)
                {
                    M.ReqType = "Get";
                    M.ShortStage_Name = "DocCol";
                    dtLeadDetail = DataInterface2.GetLeadDetail(M);
                    dtLeadDoc = DataInterface1.dbLeadDocument(M);
                }
                if (dtLeadDetail != null && dtLeadDetail.Rows.Count > 0)
                {

                    M = (from DataRow row in dtLeadDetail.Rows
                         select new clsLeadmaind()
                         {
                             LeadId = Convert.ToInt32("0" + Convert.ToString(row["LeadId"])),
                             LeadNo = Convert.ToString(row["LeadNo"]),
                             //MainProdId = Convert.ToInt32("0" + Convert.ToString(row["MainProdId"])),
                             MainProdName = Convert.ToString(row["MainProdName"]),
                             //MainProdType = Convert.ToString(row["MainProdType"]),
                             //ProdId = Convert.ToInt32("0" + Convert.ToString(row["ProdId"])),
                             ProdName = Convert.ToString(row["ProdName"]),
                             CustomerName = Convert.ToString(row["CustomerName"]),
                             ContactNo = Convert.ToString(row["ContactNo"]),
                         }).FirstOrDefault();
                    if (dtLeadDoc != null && dtLeadDoc.Rows.Count > 0)
                    {
                        M.clsLeadDocument = DataInterface.ConvertDataTable<clsLeadDocument>(dtLeadDoc);
                    }
                }



                //M = DataInterface1.GetItem<clsLeadCalling>(dt.Rows[0]); //for single row

                //ViewBag.AnswerListDDL = ClsCommon.AnswerDDL();
                ViewBag.StatusListDDL = ClsCommon.StatusDDL("DocCol");
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
        public ActionResult LeadDocument(clsLeadmaind M, FormCollection frm)
        {
            ClsReturnData clsRtn = new ClsReturnData();
            clsRtn.MsgType = (int)MessageType.Fail;
            bool IsSave = false;

            try
            {
                TempData.Clear();
                DataTable dt = new DataTable();


                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Invalid Model";
                    return View(M);
                }

                clsLeadDocument DocumentModel = new clsLeadDocument();
                int Total = Convert.ToInt32(frm["hdnCount"]);
                string DocID = "";
                string DcId = "";
                string CustomerType = "";
                string LeadCustId = "";
                string IsReceived = "";
                string Remarks = "";
                clsLeadDocument cls = new clsLeadDocument();
                cls.LeadId = M.LeadId;
                cls.ReqType = "DeleteAll";
                DataInterface1.dbLeadDocument(cls);
                cls = null;
                for (int i = 1; i <= Total; i++)
                {
                    DocID = "";
                    DcId = "";
                    CustomerType = "";
                    LeadCustId = "";
                    IsReceived = "";
                    Remarks = "";
                    DocID = frm[i + "_DocID"];
                    DcId = frm[i + "_DcId"];
                    CustomerType = frm[i + "_CustomerType"];
                    LeadCustId = frm[i + "_LeadCustId"];
                    IsReceived = frm[i + "_IsReceived"];
                    Remarks = frm[i + "_Remarks"];

                    string[] arr = IsReceived.Split(',');

                    DocumentModel = new clsLeadDocument();

                    DocumentModel.DcId = Convert.ToInt32(DcId);
                    DocumentModel.LeadId = M.LeadId;
                    DocumentModel.DocID = Convert.ToInt32(DocID);
                    DocumentModel.CustomerType = CustomerType.ToString().ToUpper()=="CUSTOMER"?"C":CustomerType;
                    DocumentModel.IsReceived = Convert.ToBoolean(arr[0]);
                    DocumentModel.Remarks = Remarks;
                    DocumentModel.LeadCustId = Convert.ToInt32(LeadCustId);
                    DocumentModel.CreatedBy = ClsSession.EmpId;
                    DocumentModel.UpdatedBy = ClsSession.EmpId;
                    DocumentModel.CompanyId = ClsSession.CompanyID;
                    if (DocumentModel.IsReceived == true)
                    {
                        DocumentModel.ReqType = "Insert";
                        //if (DocumentModel.DcId <= 0)
                        //{
                        //    DocumentModel.ReqType = "Insert";
                        //}
                        //else
                        //{
                        //    DocumentModel.ReqType = "Update";
                        //}

                        dt = DataInterface1.dbLeadDocument(DocumentModel);

                        //ClsCommon.GETClassFromDt(dt, ref clsRtn);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"])) > 0)
                            {
                                IsSave = true;
                            }
                        }
                    }

                }
                if (M.Status == "A")
                {
                    if (IsSave)
                    {
                        M.ReqType = "UpdateStatus";
                        dt = DataInterface1.UpdateLeadStatus(M);
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
                else
                {
                    M.ReqType = "UpdateStatus";
                    dt = DataInterface1.UpdateLeadStatus(M);
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
                    clse.FunctionName = "LeadCalling";
                    clse.Link = "Lead/LeadCalling";
                    clse.PageName = "Lead Calling Controller";
                    clse.UserId = ClsSession.UserID.ToString();
                    DataInterface.PostError(clse);
                }
            }
            if (IsSave)
            {

                TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Saved/Updated";
                return RedirectToAction("LeadView", "LeadDocument");
            }
            else
            {
                DataTable dtLeadDoc2 = new DataTable();
                if (M.LeadId > 0)
                {
                    M.ReqType = "Get";
                    M.StageId = 2;
                    dtLeadDoc2 = DataInterface1.dbLeadDocument(M);
                }

                if (dtLeadDoc2 != null && dtLeadDoc2.Rows.Count > 0)
                    M.clsLeadDocument = DataInterface.ConvertDataTable<clsLeadDocument>(dtLeadDoc2);

                //M = DataInterface1.GetItem<clsLeadCalling>(dt.Rows[0]); //for single row

                ViewBag.AnswerListDDL = ClsCommon.AnswerDDL();
                ViewBag.StatusListDDL = ClsCommon.StatusDDL("PrimyTel");
                M.Status = M.Status ?? "P";

                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved/Updated";
                return View(M);
            }
        }

        [Sunnet_NBFC.App_Code.SessionAttribute]
        public ActionResult LeadDocumentView(clsLeadDocument M)
        {
            List<clsLeadDocument> lst = new List<clsLeadDocument>();
            try
            {

                DataTable dtLeadDoc = new DataTable();
                M.ReqType = "View";
                M.CompanyId = ClsSession.CompanyID;
                dtLeadDoc = DataInterface1.dbLeadDocument(M);
                lst = DataInterface.ConvertDataTable<clsLeadDocument>(dtLeadDoc);

            }
            catch (Exception ex)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "View";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = ex.Message;
                    clse.FunctionName = "LeadDocumentView";
                    clse.Link = "LeadDocument/LeadDocumentView";
                    clse.PageName = "LeadDocument Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
            }
            return PartialView("LeadDocumentView", lst);
            //return View(lst);
        }

        
    }
}