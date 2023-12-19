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
using System.Diagnostics;

namespace Sunnet_NBFC.Controllers
{
    public class BranchController : Controller
    {


        // GET: Branch
        [SessionAttribute]
        public ActionResult Branch()
        {
            try
            {

                using (clsBranch M=new clsBranch())
                {
                    using (clsBranch cls = new clsBranch())
                    {
                        List<clsBranch> lst = new List<clsBranch>();
                        cls.ReqType = "GetAuto";
                        cls.CompanyID = ClsSession.CompanyID;
                        using (DataTable dt = DataInterface2.ViewBranch(cls))
                        {
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    M.BranchCode = dt.Rows[0]["BranchCode"].ToString();
                                }
                            }
                        }
                    }
                    ViewBag.StateList = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");
                    return View(M);
                }
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [SessionAttribute]
        public ActionResult Branch(clsBranch M)
        {
            TempData.Clear();

            ClsReturnData clsRetData = new ClsReturnData();

            ViewBag.StateList = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Invalid Model";
                return View(M);
            }

            if (M.BranchId <= 0)
            {
                M.ReqType = "Insert";
                M.CreatedBy = ClsSession.EmpId;
            }
            else
            {
                M.ReqType = "Update";
                M.UpdateBy = ClsSession.EmpId;
            }

            clsRetData = DataInterface2.SaveBranch(M);


            if (clsRetData.ID > 0)
            {
                TempData["Success"] = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Saved/Updated";
                return RedirectToAction("BranchView", "Branch");
            }
            else
            {
                ViewBag.Error = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Error: Data Not Saved/Updated";
                return View(M);
            }
        }

        [HttpGet]
        [SessionAttribute]
        public ActionResult BranchView()
        {
            try
            {
                if (TempData["Error"] != null)
                    ViewBag.Error = TempData["Error"];
                if (TempData["Success"] != null)
                    ViewBag.Success = TempData["Success"];
                TempData.Clear();
                List<clsBranch> lst = new List<clsBranch>();
                using (clsBranch cls = new clsBranch())
                {
                    cls.ReqType = "View";
                    cls.CompanyID = ClsSession.CompanyID;
                    using (DataTable dt = DataInterface2.ViewBranch(cls))
                    {
                        if (dt != null)
                        {

                            lst = (from DataRow row in dt.Rows
                                   select new clsBranch()
                                   {
                                       BranchId = int.Parse(row["BranchId"].ToString()),
                                       BranchCode = row["BranchCode"].ToString(),
                                       BranchName = row["BranchName"].ToString(),
                                       CompanyID = int.Parse(row["CompanyId"].ToString()),
                                       BranchAddres = row["BranchAddres"].ToString(),
                                       BranchContactNo = row["BranchContactNo"].ToString(),
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

        public ActionResult DeleteBranch(int Id)
        {
            try
            {

                if (Id <= 0)
                {
                    TempData["Error"] = "Branch not exists";
                    return RedirectToAction("BranchView");
                }

                TempData.Clear();

                using (ClsReturnData clsRetData = DataInterface2.DeleteBranch(Convert.ToInt32(Id)))
                {

                    if (clsRetData.ID > 0)
                    {
                        TempData["Success"] = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Deleted";
                    }
                    else
                    {
                        TempData["Error"] = !string.IsNullOrEmpty(clsRetData.Message) ? clsRetData.Message : "Error: Data Not Deleted";
                    }

                }


                return RedirectToAction("BranchView", "Branch");

            }
            catch (Exception ex)
            {
                throw ex;
                //return RedirectToAction("Index");
            }

        }

        public ActionResult EditBranch(string Id)
        {
            using (clsBranch M = new clsBranch())
            {
                try
                {


                    if (Id != null && int.Parse(Id) > 0)
                    {
                        using (clsBranch cls = new clsBranch())
                        {
                            List<clsBranch> lst = new List<clsBranch>();
                            cls.ReqType = "View";
                            cls.BranchId = int.Parse("0" + Id.ToString());
                            cls.CompanyID = ClsSession.CompanyID;
                            using (DataTable dt = DataInterface2.ViewBranch(cls))
                            {
                                
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {

                                        M.BranchId = int.Parse(dt.Rows[0]["BranchId"].ToString());
                                        M.BranchCode = dt.Rows[0]["BranchCode"].ToString();
                                        M.BranchName = dt.Rows[0]["BranchName"].ToString();
                                        M.CompanyID = int.Parse(dt.Rows[0]["CompanyId"].ToString());
                                        M.BranchAddres = dt.Rows[0]["BranchAddres"].ToString();
                                        M.BranchContactNo = dt.Rows[0]["BranchContactNo"].ToString();
                                        M.IsDelete = int.Parse(dt.Rows[0]["IsDELETE"].ToString());
                                        if (dt.Rows[0]["CreatedBy"].ToString() != "")
                                        {
                                            M.CreatedBy = int.Parse(dt.Rows[0]["CreatedBy"].ToString());
                                        }
                                        else
                                        {
                                            M.CreatedBy = 0;
                                        }

                                        M.StateId = int.Parse(dt.Rows[0]["StateId"].ToString());
                                        M.BranchManger = dt.Rows[0]["BranchManger"].ToString();
                                        M.CityId = int.Parse(dt.Rows[0]["CityId"].ToString());
                                        M.RentAgrementStartDate = dt.Rows[0]["RentAgrementStartDate1"].ToString();
                                        M.RentAgrimentEndDate = dt.Rows[0]["RentAgrimentEndDate1"].ToString();
                                        if (dt.Rows[0]["BranchRent"].ToString() != "")
                                        {
                                            M.BranchRent = decimal.Parse(dt.Rows[0]["BranchRent"].ToString());
                                        }
                                        else
                                        {
                                            M.BranchRent = 0;
                                        }

                                        M.OwnerName = dt.Rows[0]["OwnerName"].ToString();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        using (clsBranch cls = new clsBranch())
                        {
                            List<clsBranch> lst = new List<clsBranch>();
                            cls.ReqType = "GetAuto";
                            cls.CompanyID = ClsSession.CompanyID;
                            using (DataTable dt = DataInterface2.ViewBranch(cls))
                            {
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        M.BranchCode = dt.Rows[0]["BranchCode"].ToString();
                                    }
                                }
                            }
                        }
                    }


                }

                catch (Exception e1)
                {
                    using (clsError clsE = new clsError())
                    {
                        // Get stack trace for the exception with source file information
                        var st = new StackTrace(e1, true);
                        // Get the top stack frame
                        var frame = st.GetFrame(0);
                        // Get the line number from the stack frame
                        var line = frame.GetFileLineNumber();
                        clsE.ReqType = "Edit";
                        clsE.Mode = "WEB";
                        clsE.ErrorDescrption = e1.Message + "Line " + line + "Frame " + frame;
                        clsE.FunctionName = "Edit Branch";
                        clsE.Link = "Edit Branch";
                        clsE.PageName = "EditBranch";
                        clsE.UserId = ClsSession.EmpId.ToString();
                        DataInterface.PostError(clsE);
                    }



                }
                ViewBag.StateList = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");
                return View(M);
            }


        }

    }
}