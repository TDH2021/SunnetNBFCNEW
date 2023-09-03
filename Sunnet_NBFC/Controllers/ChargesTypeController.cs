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
    public class ChargesTypeController : Controller
    {
        // GET: Employee
        [SessionAttribute]
        public ActionResult ChargesType(int? Id)
        {
            try
            {
                clsChargesType M = new clsChargesType();
                DataTable dt = new DataTable();
                if (Id != null && Id > 0)
                {
                    clsChargesType cls = new clsChargesType();
                    cls.ReqType = "View";
                    cls.ChargeTypeID = Convert.ToInt32("0" + Id.ToString());
                    dt = DataInterface1.dbChargesType(cls);
                }
                if (dt != null && dt.Rows.Count > 0)
                    M = DataInterface1.GetItem<clsChargesType>(dt.Rows[0]);
                return View(M);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [SessionAttribute]
        public ActionResult ChargesType(clsChargesType M)
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

                if (M.ChargeTypeID<= 0)
                {
                    M.ReqType = "Insert";
                }
                else
                {
                    M.ReqType = "Update";
                }

                dt = DataInterface1.dbChargesType(M);

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
                    clse.FunctionName = "ChargesType";
                    clse.Link = "ChargesType/ChargesType";
                    clse.PageName = "ChargesType Controller";
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }
            if (clsRtn.ID > 0)
            {
                TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Saved/Updated";
                return RedirectToAction("ChargesTypeView", "ChargesType");
            }
            else
            {
                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved/Updated";
                return View(M);
            }
        }

        [HttpGet]
        [SessionAttribute]
        public ActionResult ChargesTypeView()
        {
            List<clsChargesType> lst = new List<clsChargesType>();
            try
            {

                //if (TempData["Error"] != null)
                //    ViewBag.Error = TempData["Error"];
                //if (TempData["Success"] != null)
                //    ViewBag.Success = TempData["Success"];
                TempData.Clear();

                DataTable dt = new DataTable();

                //lst = DataInterface2.GetEmployeeNew();

                clsChargesType cls = new clsChargesType();
                cls.ReqType = "view";
                cls.IsDelete = 0;
                dt = DataInterface1.dbChargesType(cls);
                lst = DataInterface.ConvertDataTable<clsChargesType>(dt);

            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "ChargesTypeView";
                    clse.Link = "ChargesType/ChargesType";
                    clse.PageName = "ChargesType Controller";
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
        public ActionResult DeleteChargesType(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    TempData["Error"] = "ChargesType not exists";
                    return RedirectToAction("ChargesTypeView");
                }

                TempData.Clear();
                DataTable dt = new DataTable();
                ClsReturnData clsRtn = new ClsReturnData();

                clsChargesType cls = new clsChargesType();
                cls.ReqType = "Delete";
                cls.ChargeTypeID = Convert.ToInt32("0" + Id.ToString());
                dt = DataInterface1.dbChargesType(cls);

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
                return RedirectToAction("ChargesTypeView", "ChargesType");
            }
            catch (Exception ex)
            {
                throw ex;
                //return RedirectToAction("Index");
            }

        }

    }
}