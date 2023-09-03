using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Sunnet_NBFC.Models;
using Sunnet_NBFC.App_Code;
using Newtonsoft.Json;
using System.Web.UI.WebControls;

namespace Sunnet_NBFC.Controllers
{
    public class CityController : Controller
    {


        //[HttpGet]
        [SessionAttribute]
        public ActionResult AddCity()
        {
            ViewBag.Service = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");
            return View();
        }
        [SessionAttribute]
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult PostCity(clsCity cls)
        {

            if (cls.CityName == "")
            {
                ViewBag.Error = "Please Enter City";
            }
            else if (cls.Stateid == 0)
            {
                ViewBag.Error = "Please Enter State";
            }
            else
            {

                DataInterface dB = new DataInterface();
                cls.ReqType = "Insert";
                using (DataTable dt = DataInterface1.GetCity(cls))
                {

                    if (dt.Rows[0]["ReturnMessage"].ToString().ToLower() == "city saved successfully")
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
            ViewBag.Service = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");

            return RedirectToAction("AddCity");
        }

        [SessionAttribute]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCity(clsCity cls)
        {
            if (ModelState.IsValid)
            {
                DataInterface dB = new DataInterface();
                cls.ReqType = "Update";
                using (DataTable dt = DataInterface1.GetCity(cls))
                {
                    ViewBag.Success = dt.Rows[0]["ReturnMessage"].ToString();
                    if (dt.Rows[0]["ReturnMessage"].ToString().ToLower() == "record updated successfully")
                    {
                        ModelState.Clear();
                        return RedirectToAction("CityView", "City");
                    }
                    else
                    {
                        ViewBag.Error = dt.Rows[0]["ReturnMessage"].ToString();
                    }
                }
            }
            cls = null;
            ViewBag.Service = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");
            return View("EditCity");

        }
        public ActionResult CityView(clsCity cs)
        {

            if (Session["UserID"] != null)
            {
                if (String.IsNullOrEmpty(Session["UserID"].ToString()) == true)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ViewBag.StateList = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");
                    try
                    {

                        using (clsCity cls = new clsCity())
                        {
                            cls.ReqType = "view";
                            cls.Stateid = cs.Stateid;
                            cls.CityName = cls.CityName;

                            using (DataTable dt = DataInterface1.GetCity(cls))
                            {
                                if (dt != null)
                                {
                                    List<clsCity> list = new List<clsCity>();
                                    list = (from DataRow row in dt.Rows

                                            select new clsCity()
                                            {
                                                StateName = row["StateName"].ToString(),
                                                CityName = row["CityName"].ToString(),
                                            }).ToList();


                                    ViewBag.CityDetails = list;// DataInterface.ConvertDataTable<clsLeadGenerationMaster>(dt);
                                }
                            }
                        }

                    }
                    catch (Exception e1)
                    {
                        using (clsError cls = new clsError())
                        {
                            cls.ReqType = "Insert";
                            cls.Mode = "WEB";
                            cls.ErrorDescrption = e1.Message;
                            cls.FunctionName = "City View";
                            cls.Link = "City/CityView";
                            cls.PageName = "City Controller";
                            cls.UserId = "1";
                            DataInterface.PostError(cls);
                        }
                    }
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");

            }
        }
        [SessionAttribute]

        public ActionResult EditCity(int cityid)
        {
            DataTable dt = new DataTable();
            clsCity clsdt = new clsCity();
            try
            {

                using (clsCity cls = new clsCity())
                {
                    cls.ReqType = "view";
                    cls.Cityid = cityid;

                    dt = DataInterface1.GetCity(cls);
                }


                if (dt.Rows.Count == 1)
                {
                    clsdt.Cityid = int.Parse(dt.Rows[0]["CityId"].ToString());
                    clsdt.Stateid = int.Parse(dt.Rows[0]["Stateid"].ToString());
                    clsdt.CityName = dt.Rows[0]["CityName"].ToString();


                }
                ViewBag.Service = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");
                return View(clsdt);
            }
            catch (Exception e1)
            {
                return null;
            }

        }

        public JsonResult GetCity(string StateId)
        {
            JsonResult result = new JsonResult();

            try
            {

                using (clsCity cls = new clsCity())
                {
                    cls.ReqType = "View";
                    cls.Stateid = int.Parse(StateId);
                    using (DataTable dt = DataInterface1.GetCity(cls))
                    {
                        result = this.Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);

                    }


                }

            }
            catch (Exception e1)
            {
                using (clsError cls = new clsError())
                {
                    cls.ReqType = "Insert";
                    cls.Mode = "WEB";
                    cls.ErrorDescrption = e1.Message + "-" + e1.InnerException.Message;
                    cls.FunctionName = "City View";
                    cls.Link = "Company/CompanyView";
                    cls.PageName = "Company Controller";
                    cls.UserId = "1";
                    DataInterface.PostError(cls);
                }
            }

            return result;

        }
        [SessionAttribute]

        [HttpPost]

        public ActionResult ExportToExcel(clsCity clss)

        {
            if (Session["UserID"] != null)
            {
                if (String.IsNullOrEmpty(Session["UserID"].ToString()) == true)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    var gv = new GridView();

                    using (clsCity cls = new clsCity())
                    {
                        cls.ReqType = "View";
                        cls.CityName = clss.CityName;
                        cls.Stateid = clss.Stateid;

                        using (DataTable dt = DataInterface1.GetCity(cls))
                        {
                            if (dt != null)
                            {
                                gv.DataSource = dt;

                                gv.DataBind();
                                Response.ClearContent();
                                Response.Buffer = true;
                                Response.AddHeader("content-disposition", "attachment; filename=CityExport.xls");
                                Response.ContentType = "application/ms-excel";
                                Response.Charset = "";

                                StringWriter objStringWriter = new StringWriter();

                                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

                                gv.RenderControl(objHtmlTextWriter);



                                Response.Output.Write(objStringWriter.ToString());

                                Response.Flush();

                                Response.End();

                            }


                            return RedirectToAction("LeadView");

                        }

                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public JsonResult ExportCity()
        {

            List<clsCity> Lead = new List<clsCity>();

            try
            {
                using (clsCity cls = new clsCity())
                {
                    cls.ReqType = "view";
                    DataTable dt = new DataTable();
                    dt = DataInterface1.GetCity(cls);
                    Lead = DataInterface.ConvertDataTable<clsCity>(dt);

                }

                var gv = new GridView();
                gv.DataSource = Lead;
                gv.DataBind();
                ClsCommon.ExportToExcel(gv, "City");

            }
            catch (Exception e1)
            {
                using (clsError cls = new clsError())
                {
                    cls.ReqType = "Insert";
                    cls.Mode = "WEB";
                    cls.ErrorDescrption = e1.Message;
                    cls.FunctionName = "City View";
                    cls.Link = "City/CityView";
                    cls.PageName = "City Controller";
                    cls.UserId = "1";
                    DataInterface.PostError(cls);
                }
            }
            return Json(Lead, JsonRequestBehavior.AllowGet);

        }
    }
}