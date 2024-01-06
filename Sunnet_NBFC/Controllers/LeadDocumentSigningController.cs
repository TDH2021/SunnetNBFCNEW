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
    public class LeadDocumentSigningController : Controller
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

                            cls.ShortStage_Name = "DocSign";
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

                cls.ShortStage_Name = "DocSign";
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
        //                    cls.CompanyId = 1;
        //                    cls.LeadNo = "";
        //                    cls.LeadId = 0;
        //                    cls.Empid = int.Parse(Session["EmpId"].ToString());
        //                   cls.ShortStage_Name = "DocSign";
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
        //                    clsE.FunctionName = "ToBeLeadDocSignView";
        //                    clsE.Link = "LeadDocumentSigning/ToBeLeadDocSignView";
        //                    clsE.PageName = "LeadDocumentSigning Controller";
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
        public ActionResult LeadDocumentSigning(int leadid, string ComeFrom = "DocSign")
        {
            clsLeadDocSign model = new clsLeadDocSign();
            clsLeadDocSign M = new clsLeadDocSign();
            try
            {
                if (leadid > 0)
                {
                    M.ReqType = "Get";
                    M.ShortStage_Name = "DocSign";
                    M.LeadId = leadid;
                    using (DataTable dt = DataInterface1.dbLeadDocSign(M))
                    {
                        if (dt != null)
                        {
                            DataRow dr = dt.Rows[0];
                            model.MainProdId = int.Parse(dr["MainProdId"].ToString());
                            model.DocSignId = int.Parse(dr["DocSignId"].ToString());
                            model.LeadNo = dr["LeadNo"].ToString();
                            model.Documents = dr["Documents"].ToString();
                            model.SanctionLetter = dr["SanctionLetter"].ToString();
                            model.LoanAgrmentKit = dr["LoanAgrmentKit"].ToString();
                            model.PDC = dr["PDC"].ToString();
                            model.NACH = dr["NACH"].ToString();
                            model.DisbursmentKit = dr["DisbursmentKit"].ToString();
                            model.InsuranceWithHP = dr["InsuranceWithHP"].ToString();
                            model.NOC = dr["NOC"].ToString();
                            model.RTOSlip = dr["RTOSlip"].ToString();
                            model.OrignalPropertyPaper = dr["OrignalPropertyPaper"].ToString();
                            model.RegisteredMortgageDeed = dr["RegisteredMortgageDeed"].ToString();
                            model.EquitableMortageDeed = dr["EquitableMortageDeed"].ToString();
                            model.Affidavit = dr["Affidavit"].ToString();
                            model.DsRemark = dr["DsRemark"].ToString();
                            model.CreatedBy = int.Parse(dr["CreatedBy"].ToString());
                            model.UpdatedBy = int.Parse(dr["UpdatedBy"].ToString());
                            model.IsDelete = int.Parse(dr["IsDelete"].ToString());
                            model.DsRemark = dr["DsRemark"].ToString();
                            model.BorrowerKyc = dr["BorrowerKyc"].ToString();
                            model.CoBorrowerKyc = dr["CoBorrowerKyc"].ToString();
                            model.GuarantorKyc = dr["GuarantorKyc"].ToString();
                            model.BorrowerPhoto = dr["BorrowerPhoto"].ToString();
                            model.CoBorrowerPhoto = dr["CoBorrowerPhoto"].ToString();
                            model.GuarantorPhoto = dr["GuarantorPhoto"].ToString();
                            model.DisbursementRequestLetter = dr["DisbursementRequestLetter"].ToString();
                            model.SignatureVerification = dr["SignatureVerification"].ToString();
                            model.KycSelfAttested = dr["KycSelfAttested"].ToString();
                            model.ReqLoanAmt = dr["ReqLoanAmt"].ToString();

                        }
                    }
                }

                using (DataTable dt = DataInterface2.GetLeadDetail(M))
                {
                    if (dt != null)
                    {
                        DataRow dr = dt.Rows[0];
                        model.LeadId = int.Parse(dr["LeadId"].ToString());
                        model.ShortStage_Name = dr["ShortStage_Name"].ToString();
                        model.Status = dr["Status"].ToString();
                        model.Remarks = dr["Remarks"].ToString();
                        model.MainProdName = Convert.ToString(dr["MainProdName"]);
                        model.ProdName = Convert.ToString(dr["ProdName"]);
                        model.CustomerName = Convert.ToString(dr["CustomerName"]);
                        model.ContactNo = Convert.ToString(dr["ContactNo"]);
                    }
                }

                ViewBag.StatusListDDL = ClsCommon.StatusDDL("DocSign");
                model.Status = model.Status ?? "P";
                ViewBag.ComeFrom = ComeFrom;
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Sunnet_NBFC.App_Code.SessionAttribute]
        [HttpPost]
        public ActionResult LeadDocumentSigning(clsLeadDocSign M)
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
                if (M.DocSignId <= 0)
                {
                    M.ReqType = "Insert";
                }
                else
                {
                    M.ReqType = "Update";
                }
                M.DsRemark = M.Remarks;
                dt = DataInterface1.dbLeadDocSign(M);

                //ClsCommon.GETClassFromDt(dt, ref clsRtn);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"])) > 0)
                    {
                        IsSave = true;
                    }
                }

                if (M.Status == "A")
                {
                    if (IsSave)
                    {
                        M.ReqType = "UpdateStatus";
                        dt = DataInterface1.UpdateLeadStatusDc(M);
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
                    dt = DataInterface1.UpdateLeadStatusDc(M);
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
                    clse.FunctionName = "LeadDocumentSigning";
                    clse.Link = "LeadDocumentSigning/LeadDocumentSigning";
                    clse.PageName = "LeadDocumentSigning Controller";
                    clse.UserId = ClsSession.UserID.ToString();
                    DataInterface.PostError(clse);
                }
            }
            if (IsSave)
            {

                TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Saved/Updated";
                return RedirectToAction("LeadView", "LeadDocumentSigning");
            }
            else
            {
                clsLeadDocSign model = new clsLeadDocSign();
                //DataTable dtLeadDoc2 = new DataTable();
                if (M.LeadId > 0)
                {
                    M.ReqType = "Get";
                    M.ShortStage_Name = "DocSign";
                    using (DataTable dt = DataInterface1.dbLeadDocSign(M))
                    {
                        if (dt != null)
                        {
                            DataRow dr = dt.Rows[0];
                            model.DocSignId = int.Parse(dr["DocSignId"].ToString());
                            model.LeadNo = dr["LeadNo"].ToString();
                            model.Documents = dr["Documents"].ToString();
                            model.SanctionLetter = dr["SanctionLetter"].ToString();
                            model.LoanAgrmentKit = dr["LoanAgrmentKit"].ToString();
                            model.PDC = dr["PDC"].ToString();
                            model.NACH = dr["NACH"].ToString();
                            model.DisbursmentKit = dr["DisbursmentKit"].ToString();
                            model.InsuranceWithHP = dr["InsuranceWithHP"].ToString();
                            model.NOC = dr["NOC"].ToString();
                            model.RTOSlip = dr["RTOSlip"].ToString();
                            model.OrignalPropertyPaper = dr["OrignalPropertyPaper"].ToString();
                            model.RegisteredMortgageDeed = dr["RegisteredMortgageDeed"].ToString();
                            model.EquitableMortageDeed = dr["EquitableMortageDeed"].ToString();
                            model.Affidavit = dr["Affidavit"].ToString();
                            model.DsRemark = dr["DsRemark"].ToString();
                            model.CreatedBy = int.Parse(dr["CreatedBy"].ToString());
                            model.UpdatedBy = int.Parse(dr["UpdatedBy"].ToString());
                            model.IsDelete = int.Parse(dr["IsDelete"].ToString());
                            model.DsRemark = dr["DsRemark"].ToString();
                            model.BorrowerKyc = dr["BorrowerKyc"].ToString();
                            model.CoBorrowerKyc = dr["CoBorrowerKyc"].ToString();
                            model.GuarantorKyc = dr["GuarantorKyc"].ToString();
                            model.BorrowerPhoto = dr["BorrowerPhoto"].ToString();
                            model.CoBorrowerPhoto = dr["CoBorrowerPhoto"].ToString();
                            model.GuarantorPhoto = dr["GuarantorPhoto"].ToString();
                            model.DisbursementRequestLetter = dr["DisbursementRequestLetter"].ToString();
                            model.SignatureVerification = dr["SignatureVerification"].ToString();
                            model.KycSelfAttested = dr["KycSelfAttested"].ToString();

                        }
                    }
                }

                //if (dtLeadDoc2 != null && dtLeadDoc2.Rows.Count > 0)
                //    M = DataInterface.GetItem<clsLeadDocSign>(dtLeadDoc2.Rows[0]);
                ViewBag.StatusListDDL = ClsCommon.StatusDDL("DocSign");
                model.Status = model.Status ?? "P";
                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved/Updated";
                return View(model);
            }
        }

        [Sunnet_NBFC.App_Code.SessionAttribute]
        public ActionResult LeadDocumentSigningView(clsLeadDocSign M)
        {
            //List<clsLeadDocSign> lst = new List<clsLeadDocSign>();
            clsLeadDocSign list = new clsLeadDocSign();
            try
            {
                DataTable dtLeadDoc = new DataTable();
                M.ReqType = "View";
                M.CompanyId = ClsSession.CompanyID;

                //dtLeadDoc = DataInterface1.dbLeadDocSign(M);
                //lst = DataInterface.ConvertDataTable<clsLeadDocSign>(dtLeadDoc);
                using (DataTable dt = DataInterface1.dbLeadDocSign(M))
                {
                    if (dt != null)
                    {

                        //List<clsLeadDocSign> list = new List<clsLeadDocSign>();
                        //list = (from DataRow dr in dt.Rows

                        //        select new clsLeadDocSign()
                        //        {

                        DataRow dr = dt.Rows[0];

                        //MainProdId = int.Parse(dr["MainProdId"].ToString()),
                        list.DocSignId = int.Parse(dr["DocSignId"].ToString());
                        list.LeadNo = dr["LeadNo"].ToString();
                        list.Documents = dr["Documents"].ToString();
                        list.SanctionLetter = dr["SanctionLetter"].ToString();
                        list.LoanAgrmentKit = dr["LoanAgrmentKit"].ToString();
                        list.PDC = dr["PDC"].ToString();
                        list.NACH = dr["NACH"].ToString();
                        list.DisbursmentKit = dr["DisbursmentKit"].ToString();
                        list.InsuranceWithHP = dr["InsuranceWithHP"].ToString();
                        list.NOC = dr["NOC"].ToString();
                        list.RTOSlip = dr["RTOSlip"].ToString();
                        list.OrignalPropertyPaper = dr["OrignalPropertyPaper"].ToString();
                        list.RegisteredMortgageDeed = dr["RegisteredMortgageDeed"].ToString();
                        list.EquitableMortageDeed = dr["EquitableMortageDeed"].ToString();
                        list.Affidavit = dr["Affidavit"].ToString();
                        list.DsRemark = dr["DsRemark"].ToString();
                        //CreatedBy = int.Parse(dr["CreatedBy"].ToString()),
                        //UpdatedBy = int.Parse(dr["UpdatedBy"].ToString()),
                        //IsDelete = int.Parse(dr["IsDelete"].ToString()),
                        list.BorrowerKyc = dr["BorrowerKyc"].ToString();
                        list.CoBorrowerKyc = dr["CoBorrowerKyc"].ToString();
                        list.GuarantorKyc = dr["GuarantorKyc"].ToString();
                        list.BorrowerPhoto = dr["BorrowerPhoto"].ToString();
                        list.CoBorrowerPhoto = dr["CoBorrowerPhoto"].ToString();
                        list.GuarantorPhoto = dr["GuarantorPhoto"].ToString();
                        list.DisbursementRequestLetter = dr["DisbursementRequestLetter"].ToString();
                        list.SignatureVerification = dr["SignatureVerification"].ToString();
                        list.KycSelfAttested = dr["KycSelfAttested"].ToString();
                        //ReqLoanAmt = dr["ReqLoanAmt"].ToString(),
                        //}).ToList();


                        //lst = list;
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
                    clse.FunctionName = "LeadDocumentSigningView";
                    clse.Link = "LeadDocumentSigning/LeadDocumentSigningView";
                    clse.PageName = "LeadDocumentSigning Controller";
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }
            return PartialView("LeadDocumentSigningView", list);
            //return View(lst);
        }



    }
}