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

namespace Sunnet_NBFC.Controllers
{
    public class MiscController : Controller
    {


        // GET: Misc
        [SessionAttribute]
        public ActionResult Misc(int? Id)
        {
            try
            {
                clsMisc M = new clsMisc();
                DataTable dt = new DataTable();

                if (Id != null && Id > 0)
                    M = DataInterface2.GetMisc(Convert.ToInt32("0" + Id.ToString()));
               if (M != null)
                {
                    M.tmpMiscType = M.MiscType;
                }
                return View(M);
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [SessionAttribute]
        public ActionResult Misc(clsMisc M)
        {
            TempData.Clear();

            ClsReturnData clsRetData = new ClsReturnData();

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Invalid Model";
                return View(M);
            }

            if (M.MiscId <= 0)
            {
                M.ReqType = "Insert";
            }
            else
            {
                M.ReqType = "Update";
            }
            clsRetData = DataInterface2.SaveMisc(M);


            if (clsRetData.ID > 0)
            {
                TempData["Success"] = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Saved/Updated";
                return RedirectToAction("MiscView", "Misc");
            }
            else
            {
                ViewBag.Error = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Error: Data Not Saved/Updated";
                return View(M);
            }
        }

        [HttpGet]
        [SessionAttribute]
        public ActionResult MiscView()
        {
            try
            {

                if (TempData["Error"] != null)
                    ViewBag.Error = TempData["Error"];
                if (TempData["Success"] != null)
                    ViewBag.Success = TempData["Success"];
                TempData.Clear();

                DataTable dt = new DataTable();
                List<clsMisc> lst = new List<clsMisc>();
                lst = DataInterface2.ViewMisc();
                //ViewBag.LeadDetails = lst;
                return View(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        [SessionAttribute]
        public ActionResult DeleteMisc(int Id)
        {
            try
            {
                
                if (Id <= 0)
                {
                    TempData["Error"] = "Misc not exists";
                    return RedirectToAction("MiscView");
                }

                TempData.Clear();

                ClsReturnData clsRetData = new ClsReturnData();

                clsRetData = DataInterface2.DeleteMisc(Convert.ToInt32(Id));

                if (clsRetData.ID > 0)
                {
                    TempData["Success"] = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Deleted";
                }
                else
                {
                    TempData["Error"]  = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Error: Data Not Deleted";
                }

                return RedirectToAction("MiscView", "Misc");

            }
            catch (Exception ex)
            {
                throw ex;
                //return RedirectToAction("Index");
            }

        }
        
      
     
    }
}