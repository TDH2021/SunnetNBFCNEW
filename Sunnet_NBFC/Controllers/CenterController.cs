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
    public class CenterController : Controller
    {


        // GET: Branch
        [SessionAttribute]
        public ActionResult Center(int? Id)
        {
            try
            {
                clsCenter M = new clsCenter();

                using (clsBranch cls = new clsBranch())
                {
                    cls.ReqType = "View";
                    cls.CompanyID = ClsSession.CompanyID;
                    ViewBag.BranchList = ClsCommon.ToSelectList(DataInterface2.ViewBranch(cls), "BranchId", "BranchName");
                }
                if (Id != null && Id > 0)
                {
                    using (clsCenter cls = new clsCenter())
                    {
                        List<clsCenter> lst = new List<clsCenter>();
                        cls.ReqType = "View";
                        cls.CenterId = int.Parse("0" + Id.ToString());
                        cls.CompanyID = ClsSession.CompanyID;
                        using (DataTable dt = DataInterface2.DBCenter(cls))
                        {
                            Id = 0;
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                 
                                    M.CenterId = int.Parse(dt.Rows[0]["CenterId"].ToString());
                                    M.CenterName = dt.Rows[0]["CenterName"].ToString();
                                    M.CenterHead = dt.Rows[0]["CenterHead"].ToString();
                                    M.CompanyID = int.Parse(dt.Rows[0]["CompanyId"].ToString());
                                    M.MaxNo = int.Parse(dt.Rows[0]["MaxNo"].ToString());
                                    M.IsDelete = int.Parse(dt.Rows[0]["IsDELETE"].ToString());
                                    M.BranchId= int.Parse(dt.Rows[0]["BranchId"].ToString());
                                    M.BranchName = dt.Rows[0]["BranchName"].ToString();
                                    if (dt.Rows[0]["CreatedBy"].ToString() != "")
                                    {
                                        M.CreatedBy = int.Parse(dt.Rows[0]["CreatedBy"].ToString());
                                    }
                                    else
                                    {
                                        M.CreatedBy = 0;
                                    }
                                   
                                }
                            }
                        }
                    }
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
        public ActionResult Center(clsCenter M)
        {
            TempData.Clear();

            
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Invalid Model";
                return View(M);
            }

            if (M.CenterId <= 0)
            {
                M.ReqType = "Insert";
                M.CreatedBy = ClsSession.EmpId;
            }
            else
            {
                M.ReqType = "Update";
                M.UpdateBy = ClsSession.EmpId;
            }
            using(DataTable dt= DataInterface2.DBCenter(M))
            {
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        TempData["Success"] = dt.Rows[0]["ReturnMessage"].ToString();
                        return RedirectToAction("CenterView", "Center");
                    }
                }
                else
                {
                    ViewBag.Error = "Error: Data Not Saved/Updated";
                }
            }

            return View(M);


        }


        [HttpGet]
        [SessionAttribute]
        public ActionResult CenterView()
        {
            try
            {
                if (TempData["Error"] != null)
                    ViewBag.Error = TempData["Error"];
                if (TempData["Success"] != null)
                    ViewBag.Success = TempData["Success"];
                TempData.Clear();
                List<clsCenter> lst = new List<clsCenter>();
                using (clsCenter cls = new clsCenter())
                {
                    cls.ReqType = "View";
                    cls.CompanyID = ClsSession.CompanyID;
                    using(DataTable dt = DataInterface2.DBCenter(cls))
                    {
                        if (dt != null)
                        {
                          
                            lst=(from DataRow row in dt.Rows
                                 select new clsCenter()
                                 {
                                     CenterId= int.Parse(row["CenterId"].ToString()),
                                     CenterName = row["CenterName"].ToString(),
                                     CompanyID = int.Parse(row["CompanyId"].ToString()),
                                     MaxNo = int.Parse(row["MaxNo"].ToString()),
                                     CenterHead = row["CenterHead"].ToString(),
                                     BranchId = int.Parse(row["BranchId"].ToString()),
                                     BranchName = row["BranchName"].ToString(),
                                 }).ToList();
                        }
                    }
                   
                }

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
        public ActionResult DeleteCenter(int Id)
        {
            try
            {

                if (Id <= 0)
                {
                    TempData["Error"] = "Center not exists";
                    return RedirectToAction("CenterView");
                }

                TempData.Clear();
                using(clsCenter cls=new clsCenter())
                {
                    cls.CenterId = Id;
                    cls.ReqType = "Delete";
                    cls.IsDelete = 1;

                    using (DataTable dt = DataInterface2.DBCenter(cls))
                    {
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                TempData["Success"] = dt.Rows[0]["ReturnMessage"].ToString();
                                return RedirectToAction("CenterView", "Center");
                            }
                        }
                        else
                        {
                            ViewBag.Error = "Error: Data Not Saved/Updated";
                            return View(cls);
                        }
                    }
                }              
                return RedirectToAction("CenterView", "Center");

            }
            catch (Exception ex)
            {
                throw ex;
                //return RedirectToAction("Index");
            }
        }

        public JsonResult GetCenter(string BranchId)
        {
            JsonResult result = new JsonResult();

            try
            {

                using (clsCenter cls = new clsCenter())
                {
                    cls.ReqType = "View";
                    cls.BranchId = int.Parse(BranchId);
                    cls.IsDelete = 0;
                    using (DataTable dt = DataInterface2.DBCenter(cls))
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
                    cls.FunctionName = "GetCenter";
                    cls.Link = "Company/GetCenter";
                    cls.PageName = "Center Controller";
                    cls.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(cls);
                }
            }

            return result;

        }
    }
}