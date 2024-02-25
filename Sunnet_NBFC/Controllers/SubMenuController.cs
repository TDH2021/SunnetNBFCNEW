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
    public class SubMenuController : Controller
    {

        [HttpGet]
        public ActionResult SubMenuView()
        {
            try
            {
                clsSubMenu cls = new clsSubMenu();
                cls.ReqType = "View";
                cls.IsActive = -1;
                DataTable dt = new DataTable();
                List<clsSubMenu> Lead = new List<clsSubMenu>();
                dt = DataInterface1.GetSubMenu(cls);
                Lead = DataInterface.ConvertDataTable<clsSubMenu>(dt);
                ViewBag.ProductDetails = Lead;
            }
            catch (Exception e1)
            {
                return null;
            }
            return View();

        }


        public ActionResult AddSubMenu()
        {
            clsMenuMaster cls = new clsMenuMaster();
            cls.ReqType = "View";
            cls.MenuActive = -1;
            DataTable dt = new DataTable();
            List<clsMenuMaster> Lead = new List<clsMenuMaster>();
            dt = DataInterface1.GetMenuMaster(cls);
            ViewBag.ddlm = ClsCommon.ToSelectList(dt, "MenuId", "MenuName");
            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult PostSubMenu(clsSubMenu cls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cls.ReqType = "Insert";
                    using (DataTable dt = DataInterface1.GetSubMenu(cls))
                    {

                        ViewBag.Message = dt.Rows[0]["ReturnMessage"].ToString();
                        if (dt.Rows[0]["ReturnMessage"].ToString().ToLower() == "submenu saved successfully")
                            
                        {
                            ModelState.Clear();
                            return RedirectToAction("SubMenuView", "SubMenu");
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
                    clse.FunctionName = "Add SubMenu";
                    clse.Link = "SubMenu/AddSubMenu";
                    clse.PageName = "SubMenu Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
            }
            clsMenuMaster clsddl = new clsMenuMaster();
            clsddl.ReqType = "View";
            DataTable dtddl = new DataTable();
            List<clsMenuMaster> Leadddl = new List<clsMenuMaster>();
            dtddl = DataInterface1.GetMenuMaster(clsddl);
            ViewBag.ddlm = ClsCommon.ToSelectList(dtddl, "MenuId", "MenuName");

            return View("AddSubMenu");
        }

        public ActionResult EditSubMenu(int submenuid)
        {
            DataTable dt = new DataTable();
            clsSubMenu  clsdt = new clsSubMenu();
            try
            {
                using (clsSubMenu cls = new clsSubMenu())
                {
                    cls.ReqType = "view";
                    cls.SubMenuId = submenuid;
                    cls.IsActive = -1;
                    dt = DataInterface1.GetSubMenu(cls);
                }

                if (dt.Rows.Count == 1)
                {
                    clsdt.SubMenuId = int.Parse(dt.Rows[0]["SubMenuId"].ToString());
                    clsdt.MenuId = int.Parse(dt.Rows[0]["MenuId"].ToString());
                    clsdt.Title = dt.Rows[0]["Title"].ToString();
                    clsdt.Controller = dt.Rows[0]["Controller"].ToString();
                    clsdt.Action = dt.Rows[0]["Action"].ToString();
                    clsdt.IsActive = int.Parse(dt.Rows[0]["IsActive"].ToString());
                }
                
                clsMenuMaster clsddl = new clsMenuMaster();
                clsddl.ReqType = "View";
                clsddl.MenuActive = -1;
                DataTable dtddl = new DataTable();
                List<clsMenuMaster> Leadddl = new List<clsMenuMaster>();
                dtddl = DataInterface1.GetMenuMaster(clsddl);
                ViewBag.ddlm = ClsCommon.ToSelectList(dtddl, "MenuId", "MenuName");
                
                return View(clsdt);
            }
            catch (Exception e1)
            {
                return null;
            }

        }

        public ActionResult UpdateSubMenu(clsSubMenu cls)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    cls.ReqType = "Update";
                    using (DataTable dt = DataInterface1.GetSubMenu(cls))
                    {
                        ViewBag.Message = dt.Rows[0]["ReturnMessage"].ToString();
                        if (dt.Rows[0]["ReturnMessage"].ToString().ToLower() == "submenu updated successfully")
                        {
                            ModelState.Clear();
                            return RedirectToAction("SubMenuView", "SubMenu");
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
                    clse.FunctionName = "Edit SubMenu";
                    clse.Link = "SubMenu/EditSubMenu";
                    clse.PageName = "SubMenu Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
            }
            clsMenuMaster clsddl = new clsMenuMaster();
            clsddl.ReqType = "View";
            DataTable dtddl = new DataTable();
            List<clsMenuMaster> Leadddl = new List<clsMenuMaster>();
            dtddl = DataInterface1.GetMenuMaster(clsddl);
            ViewBag.ddlm = ClsCommon.ToSelectList(dtddl, "MenuId", "MenuName");
            return View("EditSubMenu");
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