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
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace Sunnet_NBFC.Controllers
{
    public class StageRoleController : Controller
    {
        // GET: Employee
        [SessionAttribute]
        public ActionResult StageRole(int? Id)
        {
            try
            {
                clsStageRole M = new clsStageRole();
                DataTable dt = new DataTable();
                if (Id != null && Id > 0)
                {
                    clsStageRole cls = new clsStageRole();
                    cls.ReqType = "View";
                    cls.StageRoleId = Convert.ToInt32("0" + Id.ToString());
                    dt = DataInterface1.dbStageRole(cls);
                }
                if (dt != null && dt.Rows.Count > 0)
                    M = DataInterface1.GetItem<clsStageRole>(dt.Rows[0]);
                return View(M);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult StageRole(clsStageRole M)
        {
            ClsReturnData clsRtn = new ClsReturnData();
            clsRtn.MsgType = (int)MessageType.Fail;
            string up = "";
            try
            {
                TempData.Clear();
                DataTable dt = new DataTable();


                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Invalid Model";
                    return View(M);
                }

              


                if (M.StageRoleId <= 0)
                {
                    M.ReqType = "Insert";
                }
                else
                {
                    M.ReqType = "Update";
                }

                dt = DataInterface1.dbStageRole(M);

                if (dt != null && dt.Rows.Count > 0)
                {
                    clsRtn.ID = Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"]));
                    clsRtn.Message = Convert.ToString(dt.Rows[0]["ReturnMessage"]);
                    clsRtn.MessageDesc = clsRtn.Message;
                    if (clsRtn.ID > 0)
                        clsRtn.MsgType = (int)MessageType.Success;
                    else
                        clsRtn.MsgType = (int)MessageType.Fail;
                }
            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "StageRole";
                    clse.Link = "StageRole/StageRole";
                    clse.PageName = "StageRole Controller";
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }
            if (clsRtn.ID > 0)
            {

                TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Saved/Updated";
                return RedirectToAction("StageRoleView", "StageRole");
            }
            else
            {
                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved/Updated";
                return View(M);
            }
        }

        [HttpGet]
        [SessionAttribute]
        public ActionResult StageRoleView()
        {
            List<clsStageRole> lst = new List<clsStageRole>();
            try
            {

                //if (TempData["Error"] != null)
                //    ViewBag.Error = TempData["Error"];
                //if (TempData["Success"] != null)
                //    ViewBag.Success = TempData["Success"];
                TempData.Clear();

                DataTable dt = new DataTable();

                //lst = DataInterface2.GetEmployeeNew();

                clsStageRole cls = new clsStageRole();
                cls.ReqType = "view";
                cls.IsDelete = 0;
                dt = DataInterface1.dbStageRole(cls);
                lst = DataInterface.ConvertDataTable<clsStageRole>(dt);

            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "StageRoleView";
                    clse.Link = "StageRole/StageRole";
                    clse.PageName = "StageRole Controller";
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }
            finally
            {

            }
            return View(lst);
        }


        public ActionResult DeleteStageRole(int Id)
        {
            try
            {

                if (Id <= 0)
                {
                    TempData["Error"] = "StageRole not exists";
                    return RedirectToAction("StageRoleView");
                }

                TempData.Clear();
                DataTable dt = new DataTable();
                ClsReturnData clsRtn = new ClsReturnData();

                clsStageRole cls = new clsStageRole();
                cls.ReqType = "Delete";
                cls.StageRoleId = Convert.ToInt32("0" + Id.ToString());
                cls.IsDelete = 1;
                dt = DataInterface1.dbStageRole(cls);

                if (dt != null && dt.Rows.Count > 0)
                {
                    clsRtn.ID = Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"]));
                    clsRtn.Message = Convert.ToString(dt.Rows[0]["ReturnMessage"]);
                    clsRtn.MessageDesc = clsRtn.Message;
                    if (clsRtn.ID > 0)
                        clsRtn.MsgType = (int)MessageType.Success;
                    else
                        clsRtn.MsgType = (int)MessageType.Fail;
                }

                if (clsRtn.ID > 0)
                {
                    TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Deleted";
                }
                else
                {
                    TempData["Error"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Deleted";
                }

                return RedirectToAction("StageRoleView", "StageRole");

            }
            catch (Exception ex)
            {
                throw ex;
                //return RedirectToAction("Index");
            }

        }

        public ActionResult EmployeeDetail(int? Id)
        {
            try
            {
                clsEmployeeDetails M = new clsEmployeeDetails();
                DataTable dt = new DataTable();
                if (Id != null && Id > 0)
                {
                    clsEmployeeDetails cls = new clsEmployeeDetails();
                    cls.ReqType = "View";
                    cls.EmpID = Convert.ToInt32("0" + Id.ToString());
                    dt = DataInterface1.dbEmployeeDetails(cls);
                }
                if (dt != null && dt.Rows.Count > 0)
                    M = DataInterface1.GetItem<clsEmployeeDetails>(dt.Rows[0]);

                ViewBag.ddlDep = ClsCommon.ToSelectList(DataInterface1.GetMiseddl("Department"), "MiscId", "MiscName");
                ViewBag.ddlDesig = ClsCommon.ToSelectList(DataInterface1.GetMiseddl("Designation"), "MiscId", "MiscName");
                ViewBag.ddlmari = ClsCommon.ToSelectList(DataInterface1.GetMiseddl("Martial Status"), "MiscId", "MiscName");

                return View(M);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult EmployeeDetail(clsEmployeeDetails M)
        {
            ClsReturnData clsRtn = new ClsReturnData();
            clsRtn.MsgType = (int)MessageType.Fail;
            try
            {
                TempData.Clear();
                DataTable dt = new DataTable();
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Invalid Model";
                    return View(M);
                }

                M.ReqType = "Insert";
                dt = DataInterface1.dbEmployeeDetails(M);
                if (dt != null && dt.Rows.Count > 0)
                {
                    clsRtn.ID = Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"]));
                    clsRtn.Message = Convert.ToString(dt.Rows[0]["ReturnMessage"]);
                    clsRtn.MessageDesc = clsRtn.Message;
                    if (clsRtn.ID > 0)
                        clsRtn.MsgType = (int)MessageType.Success;
                    else
                        clsRtn.MsgType = (int)MessageType.Fail;
                }
            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "Employee";
                    clse.Link = "Employee/Employee";
                    clse.PageName = "Employee Controller";
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }
            if (clsRtn.ID > 0)
            {
                TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Saved/Updated";
                return RedirectToAction("EmployeeView", "Employee");
            }
            else
            {
                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved/Updated";
                return View(M);
            }
        }

        public string UploadEmpPhoto(HttpPostedFileBase file)
        {
            string upfile = "";
            try
            {
                if (file.ContentLength > 0)
                {
                    //string _FileName = Path.GetFileName(p.ImageName.FileName);
                    Guid id = Guid.NewGuid();
                    string FileExtension = Path.GetExtension(file.FileName);
                    string _FileName = "Emp" + id.ToString() + FileExtension;
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles/EmployeePhoto"), _FileName);
                    file.SaveAs(_path);
                    upfile = _FileName;
                    //clsEmployee clsphoto = new clsEmployee();
                    //clsphoto.ReqType = "UpdatePhoto";
                    //clsphoto.EmpID = EmpId;
                    //clsphoto.CompId = ClsSession.CompanyID;
                    //clsphoto.ImageName = _FileName;
                    //DataTable dt = DataInterface1.dbEmployee(clsphoto);
                }
                //ViewBag.Message = "File Uploaded Successfully!!";

                return upfile;
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return upfile;
            }
        }
    }
}