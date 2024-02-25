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
using System.Web.Services;
using System.Runtime.InteropServices.ComTypes;
using WebGrease.Activities;
using System.Web.Script.Serialization;
using System.Web.ModelBinding;

//using System.IO.Compression;

namespace Sunnet_NBFC.Controllers
{
    public class LeadPostDisbursementController : Controller
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
                            cls.ReqType = "GetLeadAllDataPostDisburse";
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
                            clsE.Link = "LeadPostDisburse/LeadView";
                            clsE.PageName = "Lead Post Disburse Controller";
                            clsE.UserId = Session["EmpId"].ToString();
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
                cls.ReqType = "GetLeadAllDataPostDisburse";
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



        [Sunnet_NBFC.App_Code.SessionAttribute]
        public ActionResult LeadPostDisbursement(int? leadid)
        {
            clsLeadPostDisburse cls = new clsLeadPostDisburse();
            try
            {
                DataSet ds = new DataSet();
                DataTable dtLeadDoc = new DataTable();
                if (leadid > 0)
                {
                    cls.ReqType = "Get";
                    //cls.ShortStage_Name = "Disburse";
                    cls.LeadId = Convert.ToInt32("0" + leadid.ToString());
                    cls.CompanyId = ClsSession.CompanyID;
                    ds = DataInterface1.dbPostDisbursement(cls);
                    if (ds != null && ds.Tables.Count > 0)
                        dtLeadDoc = ds.Tables[0];
                }

                if (dtLeadDoc != null && dtLeadDoc.Rows.Count > 0)
                    cls = DataInterface.GetItem<clsLeadPostDisburse>(dtLeadDoc.Rows[0]);

                //List<clsEmi> Lead = new List<clsEmi>();
                //if (ds.Tables.Count > 1)
                //{
                //    Lead = DataInterface.ConvertDataTable<clsEmi>(ds.Tables[1]);
                //}
                return View(cls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Sunnet_NBFC.App_Code.SessionAttribute]
        [HttpPost]
        public ActionResult LeadPostDisbursement(clsLeadPostDisburse M, FormCollection frm)
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
                if (M.Id <= 0)
                {
                    M.ReqType = "Insert";
                }
                else
                {
                    M.ReqType = "Update";
                }

                ds = DataInterface1.dbPostDisbursement(M);
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

                    //M.ReqType = "UpdateStatus";
                    //M.Status = "A";
                    //dt = DataInterface1.UpdateLeadStatusDisburse(M);
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
                    clse.FunctionName = "LeadPostDisbursement";
                    clse.Link = "Lead/Lead Post Disbursement";
                    clse.PageName = "LeadPostDisbursement Controller";
                    clse.UserId = ClsSession.UserID.ToString();
                    DataInterface.PostError(clse);
                }
            }
            if (IsSave)
            {
                TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Saved / Updated";
                return RedirectToAction("LeadView", "LeadPostDisbursement");
            }
            else
            {
                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved / Updated";
                return RedirectToAction("LeadPostDisbursement", new { leadid = M.LeadId });
            }
        }

        [Sunnet_NBFC.App_Code.SessionAttribute]
        public ActionResult LeadPostDisbursementView(clsLeadPostDisburse M)
        {
            clsLeadPostDisburse model = new clsLeadPostDisburse();
            try
            {
                DataSet ds = new DataSet();
                DataTable dtLeadDoc = new DataTable();
                M.ReqType = "View";
                M.CompanyId = ClsSession.CompanyID;
                ds = DataInterface1.dbPostDisbursement(M);
                dtLeadDoc = ds.Tables[0];

                using (DataTable dt = dtLeadDoc)
                {
                    if (dt != null)
                    {

                        model.MainProdId = int.Parse(dt.Rows[0]["MainProdId"].ToString());
                        model.MainProdName = dt.Rows[0]["ProductName"].ToString();
                        model.OrgDocNo = dt.Rows[0]["OrgDocNo"].ToString();
                        model.DocType = dt.Rows[0]["DocType"].ToString();
                        model.DocDate = dt.Rows[0]["DocDate"].ToString();
                        model.PagesFrom = int.Parse(dt.Rows[0]["PagesFrom"].ToString());
                        model.PagesTo = int.Parse(dt.Rows[0]["PagesTo"].ToString());
                        model.AnyOther = dt.Rows[0]["AnyOther"].ToString();
                        model.RegistrationCertificate = dt.Rows[0]["RegistrationCertificate"].ToString();
                        model.LeadId = int.Parse(dt.Rows[0]["LeadId"].ToString());
                        model.InsuredHPEndorse = dt.Rows[0]["InsuredHPEndorse"].ToString();
                        model.MarginMoneyEndorse = dt.Rows[0]["MarginMoneyEndorse"].ToString();
                        model.NOC = dt.Rows[0]["NOC"].ToString();
                        model.RTOSlip = dt.Rows[0]["RTOSlip"].ToString();
                        model.EndorsedRC = dt.Rows[0]["EndorsedRC"].ToString();
                        model.DocTypeName = dt.Rows[0]["DocTypeName"].ToString();

                    }
                }



            }
            catch (Exception ex)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "View";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = ex.Message;
                    clse.FunctionName = "LeadPostDisbursementView";
                    clse.Link = "LeadPostDisbursement/LeadPostDisbursementView";
                    clse.PageName = "LeadPostDisbursement Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
            }

            return PartialView("LeadPostDisbursementView", model);
        }






    }
}