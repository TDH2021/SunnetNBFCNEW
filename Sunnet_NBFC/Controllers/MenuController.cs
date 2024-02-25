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
using Newtonsoft.Json;

namespace Sunnet_NBFC.Controllers
{
    public class MenuController : Controller
    {

        [HttpGet]
        public ActionResult MenuView()
        {

            try
            {
                clsMenuMaster cls = new clsMenuMaster();
                cls.ReqType = "View";
                cls.MenuActive = -1;
                DataTable dt = new DataTable();
                List<clsMenuMaster> Lead = new List<clsMenuMaster>();
                dt = DataInterface1.GetMenuMaster(cls);
                Lead = DataInterface.ConvertDataTable<clsMenuMaster>(dt);
                ViewBag.ProductDetails = Lead;
            }
            catch (Exception e1)
            {
                return null;
            }
            return View();

        }


        public ActionResult AddMenu()
        {
            //ViewBag.ddlmp = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("view"), "MainProdId", "ProductName");
            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult PostMenu(clsMenuMaster cls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cls.ReqType = "Insert";
                    using (DataTable dt = DataInterface1.GetMenuMaster(cls))
                    {

                        ViewBag.Message = dt.Rows[0]["ReturnMessage"].ToString();
                        if (dt.Rows[0]["ReturnMessage"].ToString().ToLower() == "menu saved successfully")

                        {
                            ModelState.Clear();
                            return RedirectToAction("MenuView", "Menu");
                        }

                    }
                }

                cls = null;


            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "Add Menu";
                    clse.Link = "Menu/AddMenu";
                    clse.PageName = "Menu Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
            }
            return View("AddMenu");
        }

        public ActionResult EditMenu(int menuid)
        {
            DataTable dt = new DataTable();
            clsMenuMaster clsdt = new clsMenuMaster();
            try
            {

                using (clsMenuMaster cls = new clsMenuMaster())
                {
                    cls.ReqType = "view";
                    cls.MenuId = menuid;
                    cls.MenuActive = -1;
                    dt = DataInterface1.GetMenuMaster(cls);
                }

                if (dt.Rows.Count == 1)
                {
                    clsdt.MenuId = int.Parse(dt.Rows[0]["MenuId"].ToString());
                    clsdt.MenuName = dt.Rows[0]["MenuName"].ToString();
                    clsdt.MenuUrl = dt.Rows[0]["MenuUrl"].ToString();
                    clsdt.MenuActive = int.Parse(dt.Rows[0]["MenuActive"].ToString());
                }
                //ViewBag.ddlmp = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("view"), "MainProdId", "ProductName");
                return View(clsdt);
            }
            catch (Exception e1)
            {
                return null;
            }

        }

        public ActionResult UpdateMenu(clsMenuMaster cls)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    cls.ReqType = "Update";
                    using (DataTable dt = DataInterface1.GetMenuMaster(cls))
                    {
                        ViewBag.Message = dt.Rows[0]["ReturnMessage"].ToString();
                        if (dt.Rows[0]["ReturnMessage"].ToString().ToLower() == "menu updated successfully")
                        {
                            ModelState.Clear();
                            return RedirectToAction("MenuView", "Menu");
                        }

                    }
                }

                cls = null;


            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Update";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "Edit Menu";
                    clse.Link = "Menu/EditMenu";
                    clse.PageName = "Menu Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
            }
            //ViewBag.ddlmp = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("view"), "MainProdId", "ProductName");
            return View("EditMenu");
        }

        

        public ActionResult DisplayProduct()
        {
            return View();
        }


        //public DataTable GetProduct(int prodid)
        //{
        //    clsProduct cls = new clsProduct();
        //    cls.ReqType = "";
        //    cls.ProdId = 0;
        //    DataInterface1 db = new DataInterface1();
        //    DataTable dt = new DataTable();
        //    dt = db.GetProduct(cls);
        //    db = null;
        //    cls = null;
        //    return dt;
        //}
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
        //public JsonResult IsAlreadyExistsProduct(string ProdName)
        //{
        //    clsProduct cls = new clsProduct();

        //    cls.OptType = 5;
        //    cls.ProdName = ProdName;

        //    DataTable dt = new DataTable();
        //    DataInterface DB = new DataInterface();
        //    dt = DB.GetProduct(cls);

        //    bool status;
        //    if (Convert.ToInt32(dt.Rows[0][0].ToString()) > 0)
        //    {
        //        //Already registered  
        //        status = false;
        //        cls.ProdName = "";
        //    }
        //    else
        //    {
        //        //Available to use  
        //        status = true;
        //    }
        //    dt.Dispose();
        //    DB.Dispose();
        //    cls = null;
        //    return Json(status, JsonRequestBehavior.AllowGet);

        //}
        public ActionResult ExportToExcel()
        {
            var gv = new GridView();
            //gv.DataSource = this.GetProduct(0);
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=ProductView.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("ProductView");
        }
        public JsonResult GetProduct(string MainProductId)
        {
            JsonResult result = new JsonResult();

            try
            {

                using (clsProduct cls = new clsProduct())
                {
                    cls.ReqType = "View";
                    cls.MainProdId = int.Parse(MainProductId);
                    using (DataTable dt = DataInterface1.GetProduct(cls))
                    {
                        result = this.Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);

                    }


                }

            }
            catch (Exception e1)
            {
                using (clsError cls = new clsError())
                {
                    cls.ReqType = "GetProduct";
                    cls.Mode = "WEB";
                    cls.ErrorDescrption = e1.Message + "-" + e1.InnerException.Message;
                    cls.FunctionName = "GetProduct";
                    cls.Link = "Company/GetProduct";
                    cls.PageName = "Product Controller";
                    cls.UserId = "1";
                    DataInterface.PostError(cls);
                }
            }

            return result;

        }
    }

   
}