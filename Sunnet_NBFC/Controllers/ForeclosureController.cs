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
using System.Web.ModelBinding;

namespace Sunnet_NBFC.Controllers
{
    public class ForeclosureController : Controller
    {
        // GET: Employee
        [Sunnet_NBFC.App_Code.SessionAttribute]
        public ActionResult Foreclosure(int? Id)
        {
            try
            {
                List<clsForecloseEntry> list = new List<clsForecloseEntry>();
                clsForecloseEntry M = new clsForecloseEntry();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                if (Id != null && Id > 0)
                {
                    clsForecloseEntry cls = new clsForecloseEntry();
                    cls.ReqType = "Get";
                    cls.LeadId = Convert.ToInt32("0" + Id.ToString());
                    cls.CompanyID = ClsSession.CompanyID;
                    cls.SearchLeadNo = "0";
                    ds = DataInterface1.dbForeClose(cls);
                    dt = ds.Tables[0];
                }
                //if (dt != null && dt.Rows.Count > 0)
                //{

                //    list = (from DataRow row in dt.Rows

                //            select new clsForecloseEntry()
                //            {
                //                LeadId = Convert.ToInt32(row["Leadid"].ToString()),
                //                LeadNo = row["LeadNo"].ToString(),
                //                MainProduct = row["MainProduct"].ToString(),
                //                ProductName = row["ProductName"].ToString(),
                //                BranchName = row["BranchName"].ToString(),
                //                NetDisbursementAmount = Convert.ToDecimal(row["NetDisbursementAmount"].ToString()),
                //                CompanyID = Convert.ToInt32(row["CompanyID"].ToString()),
                //                ROI = Convert.ToDecimal(row["ROI"].ToString()),
                //                Tenure = Convert.ToInt32(row["Tenure"].ToString()),
                //                LoanStartDate = row["LoanStartDate"].ToString(),
                //                LoanEndDate = row["LoanEndDate"].ToString(),
                //                EmiAmount = Convert.ToDecimal(row["EmiAmount"].ToString()),
                //                TotalInst = Convert.ToDecimal(row["TotalInst"].ToString()),
                //                DSAName = row["DSAName"].ToString(),
                //                CustName = row["CustName"].ToString(),
                //                FatherName = row["FatherName"].ToString(),
                //                MobileNo1 = row["MobileNo1"].ToString(),
                //                MobileNo2 = row["MobileNo2"].ToString(),
                //                pos = Convert.ToDecimal(row["pos"].ToString()),
                //                CurrentMonthInterest = Convert.ToDecimal(row["CurrentMonthInterest"].ToString()),
                //                InstalmentOverdue = Convert.ToDecimal(row["InstalmentOverdue"].ToString()),
                //                ForeclosureCharges = Convert.ToDecimal(row["ForeclosureCharges"].ToString()),
                //                GstOnForclose = Convert.ToDecimal(row["GstOnForclose"].ToString()),
                //                ExcessAmount = Convert.ToDecimal(row["ExcessAmount"].ToString()),
                //                BouncingCharges = Convert.ToDecimal(row["BouncingCharges"].ToString()),
                //                PenalCharges = Convert.ToDecimal(row["PenalCharges"].ToString()),
                //                OtherCharges = Convert.ToDecimal(row["OtherCharges"].ToString()),
                //                FinalForeclosureAmount = Convert.ToDecimal(row["FinalForeclosureAmount"].ToString()),

                //            }).;
                //    //ViewBag.SearchList = list;

                //}
                M = DataInterface1.GetItem<clsForecloseEntry>(dt.Rows[0]);
                return View(M);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Sunnet_NBFC.App_Code.SessionAttribute]
        [HttpPost]
        public ActionResult Foreclosure(clsForecloseEntry cls)
        {
            ClsReturnData clsRtn = new ClsReturnData();
            clsRtn.MsgType = (int)MessageType.Fail;
            string up = "";
            try
            {
                TempData.Clear();
                DataTable dt = new DataTable();
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Invalid Model";
                    return View(cls);
                }

                //if (cls.LedgerID <= 0)
                //{
                cls.ReqType = "Insert";
                //}
                //else
                //{
                //    M.ReqType = "Update";
                //}

                DataSet ds = DataInterface1.dbForeClose(cls);
                dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    clsRtn.ID = Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"]));
                    clsRtn.Message = Convert.ToString(dt.Rows[0]["ReturnMessage"]);
                    clsRtn.MessageDesc = clsRtn.Message;
                    if (clsRtn.ID > 0)
                        clsRtn.MsgType = (int)MessageType.Success;
                    else
                        clsRtn.MsgType = (int)MessageType.Fail;
                }
            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "Foreclosure";
                    clse.Link = "Foreclosure/Foreclosure";
                    clse.PageName = "Foreclosure Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
            }
            if (clsRtn.ID > 0)
            {

                TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Saved/Updated";
                return RedirectToAction("Foreclosure", "Foreclosure");
            }
            else
            {
                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved/Updated";
                // return View(M);
                return View();
            }
        }

        [Sunnet_NBFC.App_Code.SessionAttribute]
        [HttpGet]
        public ActionResult ForeclosureView()
        {
            List<clsForecloseEntry> lst = new List<clsForecloseEntry>();
            try
            {
                //if (TempData["Error"] != null)
                //    ViewBag.Error = TempData["Error"];
                //if (TempData["Success"] != null)
                //    ViewBag.Success = TempData["Success"];
                TempData.Clear();
                DataTable dt = new DataTable();
                //lst = DataInterface2.GetEmployeeNew();
                clsForecloseEntry cls = new clsForecloseEntry();
                cls.ReqType = "view";
                cls.CompanyID = ClsSession.CompanyID;
                //cls.IsDelete = -1;
                DataSet ds = DataInterface1.dbForeClose(cls);
                dt = ds.Tables[0];
                lst = DataInterface.ConvertDataTable<clsForecloseEntry>(dt);

            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "ForeclosureView";
                    clse.Link = "Foreclosure/ForeclosureView";
                    clse.PageName = "Foreclosure Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
            }
            finally
            {

            }
            return View(lst);
        }


        [Sunnet_NBFC.App_Code.SessionAttribute]
        public ActionResult ForeclosureMaster(int Id)
        {
            try
            {

                if (Id <= 0)
                {
                    TempData["Error"] = "LedgerMaster not exists";
                    return RedirectToAction("LedgerMasterView");
                }

                TempData.Clear();
                DataTable dt = new DataTable();
                ClsReturnData clsRtn = new ClsReturnData();

                clsLedgerMaster cls = new clsLedgerMaster();
                cls.ReqType = "Delete";
                cls.LedgerID = Convert.ToInt32("0" + Id.ToString());
                dt = DataInterface1.dbLedgerMaster(cls);

                if (dt != null && dt.Rows.Count > 0)
                {
                    clsRtn.ID = Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"]));
                    clsRtn.Message = Convert.ToString(dt.Rows[0]["ReturnMessage"]);
                    clsRtn.MessageDesc = clsRtn.Message;
                    if (clsRtn.ID > 0)
                        clsRtn.MsgType = (int)MessageType.Success;
                    else
                        clsRtn.MsgType = (int)MessageType.Fail;
                }

                if (clsRtn.ID > 0)
                {
                    TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Deleted";
                }
                else
                {
                    TempData["Error"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Deleted";
                }
                return RedirectToAction("LedgerMasterView", "LedgerMaster");
            }
            catch (Exception ex)
            {
                throw ex;
                //return RedirectToAction("Index");
            }

        }

    }
}