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
using ClosedXML.Excel;

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
            catch (Exception ex)
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

        [SessionAttribute]
        public ActionResult MiscView(clsMisc cls)
        {
            List<clsMisc> list = new List<clsMisc>();
            try
            {

                if (TempData["Error"] != null)
                    ViewBag.Error = TempData["Error"];
                if (TempData["Success"] != null)
                    ViewBag.Success = TempData["Success"];
                TempData.Clear();
                cls.ReqType = "View";
                using (DataTable dt = DataInterface2.ViewMisc(cls))
                {
                    if (dt != null)
                    {
                        list = (from DataRow row in dt.Rows

                                select new clsMisc()
                                {
                                    MiscId = int.Parse(row["MiscId"].ToString()),
                                    MiscName = row["MiscName"].ToString(),
                                    MiscType = row["MiscType"].ToString(),
                                }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                using (clsError clsE = new clsError())
                {
                    clsE.ReqType = "Insert";
                    clsE.Mode = "WEB";
                    clsE.ErrorDescrption = ex.Message;
                    clsE.FunctionName = "LeadReport";
                    clsE.Link = "Misc/MiscView";
                    clsE.PageName = "Misc Controller";
                    clsE.UserId = ClsSession.UserID.ToString();
                    DataInterface.PostError(clsE);
                }
            }
            finally
            {

            }
            ViewBag.lst = list;
            return View();
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
                    TempData["Error"] = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Error: Data Not Deleted";
                }

                return RedirectToAction("MiscView", "Misc");

            }
            catch (Exception ex)
            {
                throw ex;
                //return RedirectToAction("Index");
            }

        }
        [SessionAttribute]
        public ActionResult ExportToExcel(clsMisc clss)
        {

            clss.ReqType = "View";

            using (DataTable dt = DataInterface2.ViewMisc(clss))
            {
                if (dt != null)
                {

                    var workbook = new XLWorkbook();

                    // Add a worksheet
                    var worksheet = workbook.Worksheets.Add("MiscReport");

                    // Add data from DataTable to the worksheet
                    worksheet.Cell(1, 1).InsertTable(dt.AsEnumerable(), "MiscDatatable", true);
                    worksheet.Columns().AdjustToContents();
                    // Save the workbook to a MemoryStream
                    var stream = new MemoryStream();
                    workbook.SaveAs(stream);

                    // Set the position of the stream back to the beginning
                    stream.Seek(0, SeekOrigin.Begin);

                    // Return the Excel file for download
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LeadReport.xlsx");
                }


                return RedirectToAction("MiscView");

            }


        }



    }
}