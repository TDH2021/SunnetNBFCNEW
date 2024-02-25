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
using ClosedXML.Excel;

namespace Sunnet_NBFC.Controllers
{
    public class ProductController : Controller
    {

        [SessionAttribute]
        public ActionResult ProductView(clsProduct clss)
        {

            try
            {
                ViewBag.MainProductList = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("View"), "MainProdId", "ProductName");
                clss.ReqType = "view";
                List<clsProduct> list = new List<clsProduct>();
                if (clss.SerarchProdId != null)
                {

                    clss.ProdId = int.Parse(clss.SerarchProdId);
                }
                if (clss.SearchMainProdId != null)
                {

                    clss.MainProdId = int.Parse(clss.SearchMainProdId);
                }
                using (DataTable dt = DataInterface1.GetProduct(clss))
                {
                    if (dt != null)
                    {
                        list = (from DataRow row in dt.Rows

                                select new clsProduct()
                                {
                                    ProdId = int.Parse(row["ProdId"].ToString()),
                                    ProductName = row["ProductName"].ToString(),
                                    MainProduct = row["MainProduct"].ToString(),
                                    CustTypeName = row["CustTypeName"].ToString()
                                }).ToList();
                    }
                }
                ViewBag.ProductDetails = list;
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
        public ActionResult ExportToExcel(clsProduct clss)
        {
            if (clss.SerarchProdId != null)
            {

                clss.ProdId = int.Parse(clss.SerarchProdId);
            }
            if (clss.SearchMainProdId != null)
            {

                clss.MainProdId = int.Parse(clss.SearchMainProdId);
            }
            using (DataTable dt = DataInterface1.GetProduct(clss))
            {
                if (dt != null)
                {

                    var workbook = new XLWorkbook();

                    // Add a worksheet
                    var worksheet = workbook.Worksheets.Add("Product Report");

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


                return RedirectToAction("ProductView");

            }
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
                    cls.IsDelete = 0;
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
                    cls.ReqType = "GetCenter";
                    cls.Mode = "WEB";
                    cls.ErrorDescrption = e1.Message + "-" + e1.InnerException.Message;
                    cls.FunctionName = "GetCenter";
                    cls.Link = "Company/GetCenter";
                    cls.PageName = "Product Controller";
                    cls.UserId = ClsSession.EmpId.ToString();
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
                    cls.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(cls);
                }
            }

            return result;

        }
    }

}