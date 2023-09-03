using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Sunnet_NBFC.Models;
using Sunnet_NBFC.App_Code;
using Newtonsoft.Json;

namespace Sunnet_NBFC.Controllers
{
    public class ReceiptController : Controller
    {
        // GET: Receipt
        [SessionAttribute]
        public ActionResult ReceiptView()
        {
            List<clsReceipt> lst = new List<clsReceipt>();
            try
            {
                TempData.Clear();
                DataSet DS = new DataSet();
                DS = DataInterface1.GetReceiptView();
                DataTable dt = new DataTable();
                dt = DS.Tables[0].Copy();
                lst = DataInterface.ConvertDataTable<clsReceipt>(dt);

            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "View";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "GetReceiptView";
                    clse.Link = "Receipt/ReceiptView";
                    clse.PageName = "Receipt Controller";
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }
            finally
            {

            }
            return View(lst);
        }
        [SessionAttribute]
        public ActionResult AddReceipt(int? Id)
        {
            try
            {
                clsReceipt M = new clsReceipt();
                ViewBag.ReceiptNo = DataInterface1.GetNextReceiptNo();
                ViewBag.BankList = ClsCommon.ToSelectList(DataInterface1.dbBankddl(), "BankId", "BankName");
                ViewBag.BranchList = ClsCommon.ToSelectList(DataInterface1.dbBranchddl(), "BranchId", "BranchName");
                ViewBag.PaymentModeList = ClsCommon.ToSelectList(DataInterface1.GetPaymentModeddl(), "MiscId", "MiscName");
                ViewBag.LeadList = ClsCommon.ToSelectList(DataInterface1.dbLeadddl(), "LeadId", "LeadNo");
                ViewBag.InstallmentList = ClsCommon.ToSelectList(DataInterface1.dbGetInstallment(0), "Period", "Period");
                ViewBag.ChargeType = ClsCommon.ToSelectList(DataInterface1.GetChargeType(), "ChargeTypeID", "ChargeTypeName");
                DataTable dt = new DataTable();
                DataSet DS = new DataSet();
                if (Id != null && Id > 0)
                {
                    DS = DataInterface1.GetReceipt(Id);
                    dt = DS.Tables[0].Copy();
                    ViewBag.ReceiptNo = Convert.ToString(dt.Rows[0]["ReceiptNo"]);
                    
                    if (DS.Tables.Count > 1)
                    {
                        TempData["ReceiptDetail"] = DataInterface1.GetReceiptDetail(Id);
                    }
                    else
                        TempData["ReceiptDetail"] = DataInterface1.GetReceiptDetail(0);
                }

                if (dt != null && dt.Rows.Count > 0)
                    M = DataInterface1.GetItem<clsReceipt>(dt.Rows[0]);

                return View(M);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult PostData(clsReceipt receipt)
        {
            ClsReturnData clsRtn = new ClsReturnData();
            clsRtn.MsgType = (int)MessageType.Fail;
            try
            {
                if (receipt == null)
                {
                    return Json(new { Status = 0, Message = "Data Empty !" });
                }
                TempData.Clear();
                DataTable dt = new DataTable();
                if (receipt.ReceiptID <= 0)
                {
                    receipt.ReqType = "Insert";
                }
                else
                {
                    receipt.ReqType = "Update";
                }

                /// Save-Update----------------//
                dt = DataInterface1.dbReceipt(receipt);

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
                    clse.FunctionName = "Receipt";
                    clse.Link = "Receipt/AddReceipt";
                    clse.PageName = "Receipt Controller";
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }
            if (clsRtn.ID > 0)
            {

                TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Saved/Updated";
                //return RedirectToAction("ReceiptView", "Receipt");
                return Json(new { Status = 1, Message = "Save Successfully !" });
            }
            else
            {
                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved/Updated";
                return Json(new { Status = 0, Message = "Failed Process !" });
            }

        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetChargeTypeKey(int ChargeTypeID)
        {
            string KeyCode = string.Empty;
            try
            {
                if (ChargeTypeID <=0)
                {
                    return Json(new { Status = 0, KeyCode = "Error" });
                }


                /// Save-Update----------------//
                KeyCode = DataInterface1.GetChargeTypeKeyCode(ChargeTypeID);
                if (!string.IsNullOrEmpty(KeyCode))
                    return Json(new { Status = 1, KeyCode = KeyCode });
                else
                    return Json(new { Status = 0, KeyCode = KeyCode });

            }
            catch (Exception e1)
            {
                return Json(new { Status = 0, KeyCode = KeyCode });
            }
        }


    }
}