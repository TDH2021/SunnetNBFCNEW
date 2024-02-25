using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using Sunnet_NBFC.App_Code;
using Sunnet_NBFC.Models;
namespace Sunnet_NBFC.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Client
        [SessionAttribute]
        public ActionResult AddQuestion()
        {

            ViewBag.MainProdList = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("View"), "MainProdId", "ProductName");
            return View();
        }
        [HttpPost]
        [SessionAttribute]
        public ActionResult AddQuestion(clsQuestion cls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cls.CompanyId = 1;
                    cls.CreatedBy = 1;
                    if (cls.QuestionId == 0)
                    {
                        cls.ReqType = "Insert";

                    }
                    else
                    {
                        cls.ReqType = "Update";
                    }
                    using (DataTable dt = DataInterface.DBQuestionMaster(cls))
                    {
                        ViewBag.Message = dt.Rows[0]["ReturnMessage"].ToString();
                    }


                }
                ModelState.Clear();
                ViewBag.MainProdList = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("View"), "MainProdId", "ProductName");
            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "Question Add";
                    clse.Link = "Question/AddQuestion";
                    clse.PageName = "Question Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
            }

            return View("AddQuestion");
        }
        [SessionAttribute]
        public ActionResult ViewQuestion(clsQuestion clss)
        {
            try
            {
                ViewBag.MainProductList = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("View"), "MainProdId", "ProductName");

                List<clsQuestion> list = new List<clsQuestion>();
                using (clsQuestion cls = new clsQuestion())
                {
                    cls.ReqType = "View";
                    cls.CompanyId = ClsSession.CompanyID;
                    cls.IsDelete = 0;
                    if(clss.SearchMainProdId!=null && clss.SearchMainProdId != "")
                    {
                        cls.MainProdId =int.Parse(clss.SearchMainProdId);
                    }
                    if (clss.SerarchProdId != null && clss.SerarchProdId != "")
                    {
                        cls.ProdId = int.Parse(clss.SerarchProdId);
                    }
                    using (DataTable dt = DataInterface.DBQuestionMaster(cls))
                    {
                        if (dt != null)
                        {
                            list = (from DataRow row in dt.Rows

                                    select new clsQuestion()
                                    {
                                        QuestionId = int.Parse(row["QuestionId"].ToString()),
                                        MainProduct = row["MainProduct"].ToString(),
                                        Product = row["Product"].ToString(),
                                        Question = row["Question"].ToString(),
                                        QuestionAnsType = row["QuestionAnsType"].ToString()
                                    }).ToList();
                            ViewBag.lst = DataInterface.ConvertDataTable<clsQuestion>(dt);


                        }
                    }
                }

            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "View";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "Question View";
                    clse.Link = "Question/ViewQuestion";
                    clse.PageName = "Question Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
            }
            return View();
        }
        [HttpGet]
        [SessionAttribute]
        public ActionResult DeleteQ(int Id)
        {
            try
            {

                if (Id <= 0)
                {
                    TempData["Error"] = "Question not exists";
                    return RedirectToAction("ViewQuestion");
                }

                TempData.Clear();
                using (clsQuestion cls = new clsQuestion())
                {
                    cls.ReqType = "Delete";
                    cls.QuestionId = Id;
                    cls.CompanyId = 1;
                    using (DataTable dt = DataInterface.DBQuestionMaster(cls))
                    {
                        ViewBag.Message = dt.Rows[0]["ReturnMessage"].ToString();
                    }

                }

            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Delete Question";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "Question Delete";
                    clse.Link = "Question/DeleteQ";
                    clse.PageName = "Question Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
            }
            return RedirectToAction("ViewQuestion", "Question");
        }



        // GET: Client
        [SessionAttribute]
        public ActionResult EditQuestion(string Id)
        {
            clsQuestion cls = new clsQuestion();
            cls.QuestionId = Convert.ToInt32(Id);
            cls.IsDelete = 0;
            cls.CompanyId = 1;
            cls.ReqType = "View";


            using (DataTable dt = DataInterface.DBQuestionMaster(cls))
                {
                    if (dt != null)
                    {
                    foreach (DataRow row in dt.Rows)
                    {
                        cls = new clsQuestion();
                        cls.QuestionId = Convert.ToInt32(row["QuestionId"].ToString());
                        cls.Question = row["Question"].ToString();
                        cls.QuestionAnsType = row["QuestionAnsType"].ToString();
                        cls.MainProdId = Convert.ToInt32(row["MainProdId"].ToString());
                        cls.ProdId = Convert.ToInt32(row["ProdId"].ToString());
                    }


                    }
                }

            using (clsProduct clsProducy = new clsProduct())
            {
                clsProducy.ReqType = "View";
                clsProducy.MainProdId = cls.MainProdId;

                ViewBag.ProdList = ClsCommon.ToSelectList(DataInterface1.GetProduct(clsProducy), "ProdId", "ProductName");
                
            }

            ViewBag.MainProdList = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("View"), "MainProdId", "ProductName");
            return View(cls);
        }
        public ActionResult ExportToExcel(clsQuestion clss)
        {
            clss.ReqType = "View";
            clss.IsDelete = 0;
            if (clss.SerarchProdId != null)
            {

                clss.ProdId = int.Parse(clss.SerarchProdId);
            }
            if (clss.SearchMainProdId != null)
            {

                clss.MainProdId = int.Parse(clss.SearchMainProdId);
            }
            using (DataTable dt = DataInterface.DBQuestionMaster(clss))
            {
                if (dt != null)
                {

                    var workbook = new XLWorkbook();

                    // Add a worksheet
                    var worksheet = workbook.Worksheets.Add("Question Report");

                    // Add data from DataTable to the worksheet
                    worksheet.Cell(1, 1).InsertTable(dt.AsEnumerable(), "LeadDatatable", true);
                    worksheet.Columns().AdjustToContents();
                    // Save the workbook to a MemoryStream
                    var stream = new MemoryStream();
                    workbook.SaveAs(stream);

                    // Set the position of the stream back to the beginning
                    stream.Seek(0, SeekOrigin.Begin);

                    // Return the Excel file for download
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProductReport.xlsx");
                }


                return RedirectToAction("ViewQuestion");

            }
        }
    }
}