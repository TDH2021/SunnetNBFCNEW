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
            catch (Exception ex)
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

        [SessionAttribute]
        public ActionResult DocumentView(clsDocument cls)
        {
            try
            {

                if (TempData["Error"] != null)
                    ViewBag.Error = TempData["Error"];
                if (TempData["Success"] != null)
                    ViewBag.Success = TempData["Success"];
                TempData.Clear();
                cls.ReqType = "View";
                cls.IsDelete = 0;
                if (cls.SerachProdId != null && cls.SerachProdId != "")
                {
                    cls.ProdID = int.Parse(cls.SerachProdId);
                }
                using (DataTable dt = DataInterface2.ViewDocument(cls))
                {

                    List<clsDocument> list = new List<clsDocument>();
                    list = (from DataRow row in dt.Rows

                            select new clsDocument()
                            {
                                DocID = int.Parse(row["DocID"].ToString()),
                                ProductName = row["ProductName"].ToString(),
                                DocumentName = row["DocumentName"].ToString(),
                            }).ToList();
                    ViewBag.lst = list;

                }
                return View();
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
                    TempData["Error"] = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Error: Data Not Deleted";
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