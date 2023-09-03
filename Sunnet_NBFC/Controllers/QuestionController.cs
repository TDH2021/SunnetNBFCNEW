using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }

            return View("AddQuestion");
        }
        [SessionAttribute]
        public ActionResult ViewQuestion()
        {
            try
            {
                using (clsQuestion cls = new clsQuestion())
                {
                    cls.ReqType = "View";
                    cls.CompanyId = 1;
                    cls.IsDelete = 0;
                    using (DataTable dt = DataInterface.DBQuestionMaster(cls))
                    {
                        if (dt != null)
                        {
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
                    clse.UserId = "1";
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
                    clse.UserId = "1";
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
    }
}