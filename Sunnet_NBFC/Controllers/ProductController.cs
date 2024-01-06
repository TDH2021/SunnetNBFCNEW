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
    public class ProductController : Controller
    {

        [HttpGet]
        [SessionAttribute]
        public ActionResult ProductView()
        {

            try
            {
                clsProduct cls = new clsProduct();
                cls.ReqType = "view";
                DataTable dt = new DataTable();
                List<clsProduct> Lead = new List<clsProduct>();
                dt = DataInterface1.GetProduct(cls);
                Lead = DataInterface.ConvertDataTable<clsProduct>(dt);
                ViewBag.ProductDetails = Lead;
            }
            catch (Exception e1)
            {
                return null;
            }
            return View();

        }

        [SessionAttribute]
        public ActionResult AddProduct()
        {
            ViewBag.ddlmp = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("view"), "MainProdId", "ProductName");
            return View();
        }

        [HttpPost]
        [SessionAttribute]
        [ValidateAntiForgeryToken]

        public ActionResult PostProduct(clsProduct cls)
        {
            try
            {
                if (ModelState.IsValid)

                {

                    cls.ReqType = "Insert";
                    cls.CompanyId = ClsSession.CompanyID;
                    
                    cls.IsDelete = 0;
                    using (DataTable dt = DataInterface1.GetProduct(cls))
                    {
                        if (dt.Rows[0]["ReturnMessage"].ToString().ToLower() == "product saved successfully")
                        {
                            ViewBag.Success = dt.Rows[0]["ReturnMessage"].ToString();
                            ModelState.Clear();
                            //return RedirectToAction("ProductView", "Product");
                        }
                        else
                        {
                            ViewBag.Error = dt.Rows[0]["ReturnMessage"].ToString();
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
                    clse.FunctionName = "Add Product";
                    clse.Link = "Product/AddProduct";
                    clse.PageName = "Product Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
            }
            ViewBag.ddlmp = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("view"), "MainProdId", "ProductName");
            return View("AddProduct");
        }
        [SessionAttribute]
        public ActionResult EditProduct(int prodid)
        {
            DataTable dt = new DataTable();
            clsProduct clsdt = new clsProduct();
            try
            {

                using (clsProduct cls = new clsProduct())
                {
                    cls.ReqType = "view";
                    cls.ProdId = prodid;

                    dt = DataInterface1.GetProduct(cls);
                }

                if (dt.Rows.Count == 1)
                {
                    clsdt.ProdId = int.Parse(dt.Rows[0]["ProdId"].ToString());
                    clsdt.MainProdId = int.Parse(dt.Rows[0]["MainProdId"].ToString());
                    clsdt.ProductName = dt.Rows[0]["ProductName"].ToString();
                    clsdt.CustTypeRequried = dt.Rows[0]["CustTypeRequried"].ToString();
                    clsdt.ReportProductName = dt.Rows[0]["ReportProductName"].ToString();
                }
                ViewBag.ddlmp = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("view"), "MainProdId", "ProductName");
                return View(clsdt);
            }
            catch (Exception e1)
            {
                return null;
            }

        }
        [SessionAttribute]
        public ActionResult UpdateProduct(clsProduct cls)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    cls.ReqType = "Update";
                    cls.CompanyId = ClsSession.CompanyID;
                    cls.IsDelete = 0;
                    using (DataTable dt = DataInterface1.GetProduct(cls))
                    {
                        // ViewBag.Message = dt.Rows[0]["ReturnMessage"].ToString();
                        if (dt.Rows[0]["ReturnMessage"].ToString().ToLower() == "product updated successfully")
                        {
                            ViewBag.Success = dt.Rows[0]["ReturnMessage"].ToString();
                            ModelState.Clear();
                            return RedirectToAction("ProductView", "Product");
                        }
                        else
                        {
                            ViewBag.Error = dt.Rows[0]["ReturnMessage"].ToString();

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
                    clse.FunctionName = "Edit Product";
                    clse.Link = "Product/EditProduct";
                    clse.PageName = "Product Controller";
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }
            ViewBag.ddlmp = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("view"), "MainProdId", "ProductName");
            return View("AddProduct");
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

        //    DataTable dt = new DataTable();
        //    dt = DataInterface1.GetProduct(cls);
           
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
        [SessionAttribute]
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

        public JsonResult GetSubProduct(string MainProductId, string ProductId)
        {
            JsonResult result = new JsonResult();

            try
            {

                using (clsProduct cls = new clsProduct())
                {
                    cls.ReqType = "ViewProduct";
                    cls.MainProdId = int.Parse(MainProductId);
                    cls.ProdId = int.Parse(ProductId);
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
                    cls.ErrorDescrption = e1.Message.ToString();
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