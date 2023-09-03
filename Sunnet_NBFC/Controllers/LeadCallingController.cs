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

namespace Sunnet_NBFC.Controllers
{
    public class LeadCallingController : Controller
    {
        [SessionAttribute]
        public ActionResult LeadCalling(clsLeadMain M, string ComeFrom = "PrimyTel")
        {
            try
            {

                //clsLeadMain M = new clsLeadMain();
                DataTable dtLeadDetail = new DataTable();
                DataTable dtLeadCalling = new DataTable();
                if (M.LeadId > 0)
                {
                    M.ReqType = "Get";
                    M.ShortStage_Name = "PrimyTel";
                    dtLeadDetail = DataInterface2.GetLeadDetail(M);
                    dtLeadCalling = DataInterface2.GetLeadCalling(M);
                }
                if (dtLeadDetail != null && dtLeadDetail.Rows.Count > 0)
                {
                    //M = DataInterface.GetItem<clsLeadMain>(dtLeadDetail.Rows[0]);
                    M = (from DataRow row in dtLeadDetail.Rows
                         select new clsLeadMain()
                         {
                             LeadId = Convert.ToInt32("0" + Convert.ToString(row["LeadId"])),
                             LeadNo = Convert.ToString(row["LeadNo"]),
                             //MainProdId = Convert.ToInt32("0" + Convert.ToString(row["MainProdId"])),
                             //MainProdName = Convert.ToString(row["MainProdName"]),
                             //MainProdType = Convert.ToString(row["MainProdType"]),
                             //ProdId = Convert.ToInt32("0" + Convert.ToString(row["ProdId"])),
                             //ProdName = Convert.ToString(row["ProdName"]),

                         }).FirstOrDefault();

                    if (dtLeadCalling != null && dtLeadCalling.Rows.Count > 0)
                    {
                        //M.clsLeadCalling = DataInterface.ConvertDataTable<clsLeadCalling>(dtLeadCalling);
                        M.clsLeadCalling = (from DataRow row in dtLeadCalling.Rows
                                            select new clsLeadCalling()
                                            {
                                                TcId = int.Parse(row["TcId"].ToString()),
                                                LeadId = int.Parse(row["LeadId"].ToString()),
                                                QuestionId = Convert.ToInt32("0" + Convert.ToString(row["QuestionId"])),
                                                Question = row["Question"].ToString(),
                                                QuestionAnsType = row["QuestionAnsType"].ToString(),
                                                Answer = row["Answer"].ToString(),
                                                Remarks = row["Remarks"].ToString(),
                                                //IsDelete = Convert.ToInt32("0" + Convert.ToString(row["IsDelete"])),
                                            }).ToList();
                    }
                }



                //M = DataInterface1.GetItem<clsLeadCalling>(dt.Rows[0]); //for single row

                ViewBag.AnswerListDDL = ClsCommon.AnswerDDL();
                ViewBag.StatusListDDL = ClsCommon.StatusDDL("PrimyTel");
                ViewBag.ComeFrom = ComeFrom;
                M.Status = M.Status ?? "P";
                return View(M);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [SessionAttribute]
        public ActionResult LeadCalling(clsLeadMain M, FormCollection frm)
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

                var QnsAnsKeyValue = frm.AllKeys
                 .Where(k => k.StartsWith("A_"))
                 .ToDictionary(k => k, k => frm[k]);


                clsLeadCalling CallingModel = new clsLeadCalling();

                foreach (var item in QnsAnsKeyValue)
                {
                    string Key = item.Key;
                    string Value = item.Value;
                    string[] valueArray = Key.Split('_');
                    CallingModel = new clsLeadCalling();

                    CallingModel.TcId = Convert.ToInt32(valueArray[1]);
                    CallingModel.LeadId = M.LeadId;
                    CallingModel.QuestionId = Convert.ToInt32(valueArray[2]);
                    CallingModel.Answer = Value;


                    if (CallingModel.TcId <= 0)
                    {
                        CallingModel.ReqType = "Insert";
                    }
                    else
                    {
                        CallingModel.ReqType = "Update";
                    }

                    dt = DataInterface2.SaveLeadCalling(CallingModel);

                    //ClsCommon.GETClassFromDt(dt, ref clsRtn);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"])) > 0)
                        {
                            IsSave = true;
                        }
                    }
                }


                if (IsSave)
                {
                    M.ReqType = "UpdateStatus";
                    dt = DataInterface2.UpdateLeadStatus(M);
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
                return RedirectToAction("LeadView", "Leadcalling");
            }
            else
            {
                DataTable dtLeadCalling = new DataTable();
                if (M.LeadId > 0)
                {
                    M.ReqType = "Get";
                    M.StageId = 2;
                    dtLeadCalling = DataInterface2.GetLeadCalling(M);
                }

                if (dtLeadCalling != null && dtLeadCalling.Rows.Count > 0)
                    M.clsLeadCalling = DataInterface.ConvertDataTable<clsLeadCalling>(dtLeadCalling);

                //M = DataInterface1.GetItem<clsLeadCalling>(dt.Rows[0]); //for single row

                ViewBag.AnswerListDDL = ClsCommon.AnswerDDL();
                ViewBag.StatusListDDL = ClsCommon.StatusDDL("PrimyTel");
                M.Status = M.Status ?? "P";

                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved/Updated";
                return View(M);
            }
        }
        [SessionAttribute]
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
                    ViewBag.MainProductList = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("View"), "MainProdId", "ProductName");

                    List<clsLeadGenerationMaster> lst = new List<clsLeadGenerationMaster>();
                    try
                    {
                        using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
                        {
                            cls.ReqType = "GetLeadAllData";
                            cls.CompanyId = ClsSession.CompanyID;
                            cls.LeadNo = "";
                            cls.LeadId = 0;
                            cls.ShortStage_Name = "PRIMYTEL";
                            cls.StageEmpId = int.Parse(Session["EmpId"].ToString());
                            cls.Empid = 0;
                            cls.MainProductId = clss.MainProductId;
                            cls.ProductId = clss.ProductId;
                            cls.LeadNo = clss.LeadNo;
                            cls.CustomerName = clss.CustomerName;
                            cls.MobileNo1 = clss.MobileNo1;
                            cls.PanNo = clss.PanNo;
                            cls.AadharNo = clss.AadharNo;

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
                            clsE.ReqType = "View";
                            clsE.Mode = "WEB";
                            clsE.ErrorDescrption = e1.Message;
                            clsE.FunctionName = "Status View";
                            clsE.Link = "Status/ViewStatus";
                            clsE.PageName = "Status Controller";
                            clsE.UserId = "1";
                            DataInterface.PostError(clsE);
                        }
                    }

                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [SessionAttribute]
        public ActionResult LeadCallingView(clsLeadMain M)
        {
            try
            {
                //clsLeadMain M = new clsLeadMain();
                DataTable dtLeadDetail = new DataTable();
                DataTable dtLeadCalling = new DataTable();
                if (M.LeadId > 0)
                {
                    M.ReqType = "Get";
                    M.ShortStage_Name = "PrimyTel";
                    dtLeadDetail = DataInterface2.GetLeadDetail(M);
                    dtLeadCalling = DataInterface2.GetLeadCalling(M);
                }
                if (dtLeadDetail != null && dtLeadDetail.Rows.Count > 0)
                    M = DataInterface.GetItem<clsLeadMain>(dtLeadDetail.Rows[0]);

                if (dtLeadCalling != null && dtLeadCalling.Rows.Count > 0)
                    M.clsLeadCalling = DataInterface.ConvertDataTable<clsLeadCalling>(dtLeadCalling);

                //M = DataInterface1.GetItem<clsLeadCalling>(dt.Rows[0]); //for single row

                ViewBag.AnswerListDDL = ClsCommon.AnswerDDL();
                ViewBag.StatusListDDL = ClsCommon.StatusDDL("PrimyTel");
                M.Status = M.Status ?? "P";
                return PartialView("_LeadCallingView", M);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [SessionAttribute]
        public ActionResult ExportToExcel(clsLeadGenerationMaster clss)

        {

            var gv = new GridView();

            using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
            {
                cls.ReqType = "ViewLead";
                cls.CompanyId = ClsSession.CompanyID;
                cls.BranchID = ClsSession.BranchId;
                cls.MainProductId = clss.MainProductId;
                cls.ProductId = clss.ProductId;
                cls.LeadNo = clss.LeadNo;
                cls.CustomerName = clss.CustomerName;
                cls.MobileNo1 = clss.MobileNo1;
                cls.PanNo = clss.PanNo;
                cls.AadharNo = clss.AadharNo;
                cls.ReqType = "ViewLead";

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


    }
}