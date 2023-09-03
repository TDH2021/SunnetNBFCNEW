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
    public class ChargesMasterController : Controller
    {
        // GET: ChargesMaster
        [SessionAttribute]
        public ActionResult AddChargesMaster()
        {
            clsChargesMaster clsChargesMaster = new clsChargesMaster();
            ViewBag.ChargesTypeList = ClsCommon.ToSelectList(DataInterface1.GetChargeType(), "ChargeTypeID", "ChargeType");
            return View(clsChargesMaster);
        }
        [SessionAttribute]
        public ActionResult ChargesMaster(int? Id)
        {
            try
            {
                clsChargesMaster M = new clsChargesMaster();
                ViewBag.ChargesTypeList = ClsCommon.ToSelectList(DataInterface1.GetChargeType(), "ChargeTypeID", "ChargeType");
                DataTable dt = new DataTable();
                if (Id != null && Id > 0)
                {
                    dt = DataInterface1.GetChargeMasterById(Id);
                }

                if (dt != null && dt.Rows.Count > 0)
                    M = DataInterface1.GetItem<clsChargesMaster>(dt.Rows[0]);

                return View(M);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [SessionAttribute]
        public ActionResult ChargesMaster(clsChargesMaster M)
        {
            ClsReturnData clsRtn = new ClsReturnData();
            clsRtn.MsgType = (int)MessageType.Fail;
            string up = "";
            try
            {
                ViewBag.ChargesTypeList = ClsCommon.ToSelectList(DataInterface1.GetChargeType(), "ChargeTypeID", "ChargeType");
                TempData.Clear();
                DataTable dt = new DataTable();

                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Invalid Model";
                    return View(M);
                }

                if (M.ChargeID <= 0)
                {
                    M.ReqType = "Insert";
                }
                else
                {
                    M.ReqType = "Update";
                }
                
                dt = DataInterface1.dbChargesMaster(M);

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
                    clse.FunctionName = "ChargesMaster";
                    clse.Link = "ChargesMaster/ChargesMaster";
                    clse.PageName = "ChargesMaster Controller";
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }
            if (clsRtn.ID > 0)
            {

                TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Saved/Updated";
                return RedirectToAction("ChargesView", "ChargesMaster");
            }
            else
            {
                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved/Updated";
                return View(M);
            }
        }


        [HttpGet]
        [SessionAttribute]
        public ActionResult ChargesView()
        {
            List<clsChargesMaster> lst = new List<clsChargesMaster>();
            try
            {

                //if (TempData["Error"] != null)
                //    ViewBag.Error = TempData["Error"];
                //if (TempData["Success"] != null)
                //    ViewBag.Success = TempData["Success"];
                TempData.Clear();

                DataTable dt = new DataTable();

                //lst = DataInterface2.GetEmployeeNew();

               
                dt = DataInterface1.GetChargeMaster();
                lst = DataInterface.ConvertDataTable<clsChargesMaster>(dt);

            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "ChargesMaster";
                    clse.Link = "ChargesMaster/ChargesMaster";
                    clse.PageName = "ChargesMaster Controller";
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }
            finally
            {

            }
            return View(lst);
        }

        public ActionResult DeleteCharges(int Id)
        {
            try
            {

                if (Id <= 0)
                {
                    TempData["Error"] = "ChargesMaster not exists";
                    return RedirectToAction("ChargesView");
                }

                TempData.Clear();

                ClsReturnData clsRetData = new ClsReturnData();

                clsRetData = DataInterface2.DeleteChargesMaster(Convert.ToInt32(Id));

                if (clsRetData.ID > 0)
                {
                    TempData["Success"] = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Deleted";
                }
                else
                {
                    TempData["Error"] = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Error: Data Not Deleted";
                }

                return RedirectToAction("ChargesView", "ChargesMaster");

            }
            catch (Exception ex)
            {
                throw ex;
                //return RedirectToAction("Index");
            }

        }


    }
}