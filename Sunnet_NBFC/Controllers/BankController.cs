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
    public class BankController : Controller
    {
        // GET: Bank
        [SessionAttribute]
        public ActionResult Bank(int? Id)
        {
            try
            {
                clsBankMaster M = new clsBankMaster();
                DataTable dt = new DataTable();
                if (Id != null && Id > 0)
                {
                    dt = DataInterface1.GetBankData(Id);
                }

                if (dt != null && dt.Rows.Count > 0)
                    M = DataInterface1.GetItem<clsBankMaster>(dt.Rows[0]);

                return View(M);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [SessionAttribute]
        public ActionResult Bank(clsBankMaster M)
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
                    return View(M);
                }

                if (M.BankId <= 0)
                {
                    M.ReqType = "Insert";
                }
                else
                {
                    M.ReqType = "Update";
                }

                dt = DataInterface1.dbBank(M);

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
                    clse.FunctionName = "Bank";
                    clse.Link = "Bank/Bank";
                    clse.PageName = "Bank Controller";
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }
            if (clsRtn.ID > 0)
            {

                TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Saved/Updated";
                return RedirectToAction("BankView", "Bank");
            }
            else
            {
                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved/Updated";
                return View(M);
            }
        }

        [HttpGet]
        [SessionAttribute]
        public ActionResult BankView()
        {
            List<clsBankMaster> lst = new List<clsBankMaster>();
            try
            {

                //if (TempData["Error"] != null)
                //    ViewBag.Error = TempData["Error"];
                //if (TempData["Success"] != null)
                //    ViewBag.Success = TempData["Success"];
                TempData.Clear();

                DataTable dt = new DataTable();
                

                dt = DataInterface1.GetBankData(0);
                lst = DataInterface.ConvertDataTable<clsBankMaster>(dt);

            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "Bank";
                    clse.Link = "Bank/Bank";
                    clse.PageName = "Bank Controller";
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
        public ActionResult DeleteBank(int Id)
        {
            try
            {

                if (Id <= 0)
                {
                    TempData["Error"] = "BankMaster not exists";
                    return RedirectToAction("BankView");
                }

                TempData.Clear();

                ClsReturnData clsRetData = new ClsReturnData();

                clsRetData = DataInterface2.DeleteBank(Convert.ToInt32(Id));

                if (clsRetData.ID > 0)
                {
                    TempData["Success"] = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Deleted";
                }
                else
                {
                    TempData["Error"] = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Error: Data Not Deleted";
                }

                return RedirectToAction("BankView", "Bank");

            }
            catch (Exception ex)
            {
                throw ex;
                //return RedirectToAction("Index");
            }

        }
    }
}