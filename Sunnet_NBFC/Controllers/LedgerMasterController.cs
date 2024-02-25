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
    public class LedgerMasterController : Controller
    {
        // GET:
        // 
        [SessionAttribute]
        public ActionResult LedgerMaster(int? Id)
        {
            try
            {
                clsLedgerMaster M = new clsLedgerMaster();
                DataTable dt = new DataTable();
                if (Id != null && Id > 0)
                {
                    clsLedgerMaster cls = new clsLedgerMaster();
                    cls.ReqType = "View";
                    cls.LedgerID = Convert.ToInt32("0" + Id.ToString());
                    dt = DataInterface1.dbLedgerMaster(cls);
                }
                if (dt != null && dt.Rows.Count > 0)
                    M = DataInterface1.GetItem<clsLedgerMaster>(dt.Rows[0]);
                return View(M);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [SessionAttribute]
        public ActionResult LedgerMaster(clsLedgerMaster M)
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

                if (M.LedgerID <= 0)
                {
                    M.ReqType = "Insert";
                }
                else
                {
                    M.ReqType = "Update";
                }

                dt = DataInterface1.dbLedgerMaster(M);

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
                    clse.FunctionName = "LedgerMaster";
                    clse.Link = "LedgerMaster/LedgerMaster";
                    clse.PageName = "LedgerMaster Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
            }
            if (clsRtn.ID > 0)
            {

                TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Saved/Updated";
                return RedirectToAction("LedgerMasterView", "LedgerMaster");
            }
            else
            {
                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved/Updated";
                return View(M);
            }
        }

        [HttpGet]
        [SessionAttribute]
        public ActionResult LedgerMasterView()
        {
            List<clsLedgerMaster> lst = new List<clsLedgerMaster>();
            try
            {

                //if (TempData["Error"] != null)
                //    ViewBag.Error = TempData["Error"];
                //if (TempData["Success"] != null)
                //    ViewBag.Success = TempData["Success"];
                TempData.Clear();

                DataTable dt = new DataTable();

                //lst = DataInterface2.GetEmployeeNew();

                clsLedgerMaster cls = new clsLedgerMaster();
                cls.ReqType = "view";
                cls.IsDelete = -1;
                dt = DataInterface1.dbLedgerMaster(cls);
                lst = DataInterface.ConvertDataTable<clsLedgerMaster>(dt);

            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "LedgerMasterView";
                    clse.Link = "LedgerMaster/LedgerMaster";
                    clse.PageName = "LedgerMaster Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
            }
            finally
            {

            }
            return View(lst);
        }

        [SessionAttribute]
        public ActionResult DeleteLedgerMaster(int Id)
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