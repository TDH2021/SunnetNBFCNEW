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
    public class DocumentController : Controller
    {

        [SessionAttribute]
        // GET: Document
        public ActionResult Document(int? Id)
        {
            try
            {
                clsDocument M = new clsDocument();
                DataTable dt = new DataTable();

                if (Id != null && Id > 0)
                    M = DataInterface2.GetDocument(Convert.ToInt32("0" + Id.ToString()));
                return View(M);
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [SessionAttribute]
        public ActionResult Document(clsDocument M)
        {
            TempData.Clear();

            ClsReturnData clsRetData = new ClsReturnData();

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Invalid Model";
                return View(M);
            }

            if (M.DocID <= 0)
            {
                M.ReqType = "Insert";
            }
            else
            {
                M.ReqType = "Update";
            }
            clsRetData = DataInterface2.SaveDocument(M);


            if (clsRetData.ID > 0)
            {
                ModelState.Clear();
                ViewBag.Success = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Saved/Updated";
               // return RedirectToAction("DocumentView", "Document");
                return View(M);
                
            }
            else
            {
                ViewBag.Error = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Error: Data Not Saved/Updated";
                return View(M);
            }
            //return RedirectToAction("Document");
        }

        [HttpGet]
        [SessionAttribute]
        public ActionResult DocumentView()
        {
            try
            {

                if (TempData["Error"] != null)
                    ViewBag.Error = TempData["Error"];
                if (TempData["Success"] != null)
                    ViewBag.Success = TempData["Success"];
                TempData.Clear();

                DataTable dt = new DataTable();
                List<clsDocument> lst = new List<clsDocument>();
                lst = DataInterface2.ViewDocument();
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
        public ActionResult DeleteDocument(int Id)
        {
            try
            {
                
                if (Id <= 0)
                {
                    TempData["Error"] = "Document not exists";
                    return RedirectToAction("DocumentView");
                }

                TempData.Clear();

                ClsReturnData clsRetData = new ClsReturnData();

                clsRetData = DataInterface2.DeleteDocument(Convert.ToInt32(Id));

                if (clsRetData.ID > 0)
                {
                    TempData["Success"] = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Deleted";
                }
                else
                {
                    TempData["Error"]  = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Error: Data Not Deleted";
                }

                return RedirectToAction("DocumentView", "Document");

            }
            catch (Exception ex)
            {
                throw ex;
                //return RedirectToAction("Index");
            }

        }
        
      
     
    }
}