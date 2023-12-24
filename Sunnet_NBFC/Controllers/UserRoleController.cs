using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunnet_NBFC.Models;
using System.Data;
using Sunnet_NBFC.App_Code;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Sunnet_NBFC.Controllers
{
    public class UserRoleController : Controller
    {

        public string JSONresult { get; private set; }
        // GET: UserRole
        [SessionAttribute]
        public ActionResult RoleMaster(clsRoleMaster clsR)
        {
            using (clsRoleMaster M = new clsRoleMaster())
            {
                try
                {
                    ViewBag.RoleList = ClsCommon.ToSelectList(DataInterface1.GetMiseddl("Role"), "MiscId", "MiscName");
                   
                    TempData["CompanyId"] = ClsSession.CompanyID;
                    M.ReqType = "View";
                    if (clsR.RoleId != null)
                    {
                        M.RoleId = clsR.RoleId.ToString() == "" ? "0" : clsR.RoleId;
                    }
                    else
                    {
                        M.RoleId = "0";
                    }

                    M.EmpId = "0";
                 
                    M.CompanyId = ClsSession.CompanyID;
                    M.IsDelete = 0;
                    if (int.Parse(M.RoleId) == 0)
                    {
                        using (clsSubMenu cls = new clsSubMenu())
                        {

                            cls.ReqType = "View";
                            cls.IsActive = 1;
                            M.ReqType = "Insert";
                            M.CompanyId = ClsSession.CompanyID;
                            M.IsDelete = 0;
                            using (DataTable dtddl = DataInterface1.GetSubMenu(cls))
                            {
                                M.clsSubMenulst = DataInterface.ConvertDataTable<clsRoleMaster>(dtddl);
                            }

                        }
                    }
                    else
                    {
                        using (DataTable dt = DataInterface.DBRole(M))
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                List<clsRoleMaster> list = new List<clsRoleMaster>();
                                list = (from DataRow row in dt.Rows

                                        select new clsRoleMaster()
                                        {
                                            RoleId = row["RoleId"].ToString(),
                                            RoleName = row["RoleName"].ToString(),
                                            MenuName = row["MenuName"].ToString(),
                                            Title = row["Title"].ToString(),
                                            MenuId = int.Parse(row["MenuId"].ToString()),
                                            SubMenuId = int.Parse(row["SubMenuId"].ToString()),
                                            IsSelected = row["IsSelected"].ToString() == "1" ? true : false,
                                        }).ToList();


                                M.clsSubMenulst = list;
                            }
                            else
                            {

                                using (clsSubMenu cls = new clsSubMenu())
                                {

                                    cls.ReqType = "View";
                                    cls.IsActive = 1;
                                    M.ReqType = "Insert";
                                    M.CompanyId = ClsSession.CompanyID;
                                    M.IsDelete = 0;
                                    using (DataTable dtddl = DataInterface1.GetSubMenu(cls))
                                    {
                                        M.clsSubMenulst = DataInterface.ConvertDataTable<clsRoleMaster>(dtddl);
                                    }

                                }
                            }
                        }

                    }


                }
                catch (Exception ex)
                {
                    using (clsError cls = new clsError())
                    {
                        cls.ReqType = "Insert";
                        cls.Mode = "WEB";
                        cls.ErrorDescrption = ex.Message;
                        cls.FunctionName = "RoleMaster";
                        cls.Link = "Role/Role Master";
                        cls.PageName = "Role Controller";
                        cls.UserId = ClsSession.EmpId.ToString();
                        DataInterface.PostError(cls);
                    }
                }
                return View(M);
            }


        }

        [SessionAttribute]
        public ActionResult RoleMaster_EmpWise(clsRoleMaster clsR)
        {
            using (clsRoleMaster M = new clsRoleMaster())
            {
                try
                {
                    clsEmployee clsE = new clsEmployee();
                    clsE.ReqType = "view";
                    clsE.CompId = ClsSession.CompanyID;
                    clsE.IsDelete = 0;
                    ViewBag.EmpList = ClsCommon.ToSelectList(DataInterface1.dbEmployee(clsE), "EmpID", "EmployeeName");
                    clsE = null;

                    TempData["CompanyId"] = ClsSession.CompanyID;
                    M.ReqType = "View";
                    if (clsR.EmpId != null)
                    {
                        M.EmpId = clsR.EmpId.ToString() == "" ? "0" : clsR.EmpId;
                    }
                    else
                    {
                        M.EmpId = "0";
                    }
                    M.RoleId = "0";

                    M.CompanyId = ClsSession.CompanyID;
                    M.IsDelete = 0;
                    if (int.Parse(M.RoleId) == 0 && int.Parse(M.EmpId) == 0)
                    {
                        using (clsSubMenu cls = new clsSubMenu())
                        {

                            cls.ReqType = "View";
                            cls.IsActive = 1;
                            M.ReqType = "Insert";
                            M.CompanyId = ClsSession.CompanyID;
                            M.IsDelete = 0;
                            using (DataTable dtddl = DataInterface1.GetSubMenu(cls))
                            {
                                M.clsSubMenulst = DataInterface.ConvertDataTable<clsRoleMaster>(dtddl);
                            }

                        }
                    }
                    else
                    {
                        using (DataTable dt = DataInterface.DBRole(M))
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                List<clsRoleMaster> list = new List<clsRoleMaster>();
                                list = (from DataRow row in dt.Rows

                                        select new clsRoleMaster()
                                        {
                                            RoleId = row["RoleId"].ToString(),
                                            RoleName = row["RoleName"].ToString(),
                                            MenuName = row["MenuName"].ToString(),
                                            Title = row["Title"].ToString(),
                                            MenuId = int.Parse(row["MenuId"].ToString()),
                                            SubMenuId = int.Parse(row["SubMenuId"].ToString()),
                                            IsSelected = row["IsSelected"].ToString() == "1" ? true : false,
                                        }).ToList();


                                M.clsSubMenulst = list;
                            }
                            else
                            {

                                using (clsSubMenu cls = new clsSubMenu())
                                {

                                    cls.ReqType = "View";
                                    cls.IsActive = 1;
                                    M.ReqType = "Insert";
                                    M.CompanyId = ClsSession.CompanyID;
                                    M.IsDelete = 0;
                                    using (DataTable dtddl = DataInterface1.GetSubMenu(cls))
                                    {
                                        M.clsSubMenulst = DataInterface.ConvertDataTable<clsRoleMaster>(dtddl);
                                    }

                                }
                            }
                        }

                    }


                }
                catch (Exception ex)
                {
                    using (clsError cls = new clsError())
                    {
                        cls.ReqType = "Insert";
                        cls.Mode = "WEB";
                        cls.ErrorDescrption = ex.Message;
                        cls.FunctionName = "RoleMaster_EmpWise";
                        cls.Link = "Role/Role Master";
                        cls.PageName = "Role Controller";
                        cls.UserId = ClsSession.EmpId.ToString();
                        DataInterface.PostError(cls);
                    }
                }
                return View(M);
            }


        }
        [HttpPost]
        [SessionAttribute]
        public JsonResult Role()
        {
            string JSONresult = "";
            JavaScriptSerializer jss = new JavaScriptSerializer();

            try
            {
                List<clsRoleMaster> clsRoles = jss.Deserialize<List<clsRoleMaster>>(Request.Form["MenuRoleS"]);
                RoleMenuMaster roleMenuMasters = jss.Deserialize<RoleMenuMaster>(Request.Form["AllDataArray"]);
                using(clsRoleMaster roleMenu = new clsRoleMaster())
                {
                    roleMenu.ReqType = "Delete";
                    roleMenu.CompanyId = 0;
                    roleMenu.EmpName = roleMenuMasters.EmpName;
                    roleMenu.RoleId = roleMenuMasters.RoleId;
                    roleMenu.RoleName = roleMenuMasters.RoleName;
                    roleMenu.EmpId = roleMenuMasters.EmpID;
                    roleMenu.EmpCode = roleMenuMasters.EmpCode;
                    roleMenu.CompanyId = ClsSession.CompanyID;
                    roleMenu.CreatedBy = ClsSession.UserID;

                    using (DataTable dt = DataInterface.DBRole(roleMenu))
                    {
                        JSONresult = JsonConvert.SerializeObject(dt);

                    }
                }
                
                for (int i = 0; i < clsRoles.Count; i++)
                {

                    
                    clsRoles[i].ReqType = "Insert";
                    clsRoles[i].EmpId = roleMenuMasters.EmpID;
                    clsRoles[i].EmpName = roleMenuMasters.EmpName;
                    clsRoles[i].RoleId = roleMenuMasters.RoleId;
                    clsRoles[i].RoleName = roleMenuMasters.RoleName;
                    clsRoles[i].EmpCode = roleMenuMasters.EmpCode;
                    clsRoles[i].CompanyId = ClsSession.CompanyID;
                    clsRoles[i].CreatedBy = ClsSession.UserID;

                    using (DataTable dt = DataInterface.DBRole(clsRoles[i]))
                    {
                        JSONresult = JsonConvert.SerializeObject(dt);

                    }

                }
                return Json(JSONresult, JsonRequestBehavior.AllowGet);
            }



            catch (Exception e1)
            {

                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "RoleMaster";
                    clse.Link = "RoleMaster/RoleMaster";
                    clse.PageName = "Role Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
                return Json(JSONresult, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult UpdateMenu()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            List<UpdateMenuMaster> updateMenuMasters = jss.Deserialize<List<UpdateMenuMaster>>(Request.Form["MenuRoleS"]);
            RoleMenuMaster roleMenuMasters = jss.Deserialize<RoleMenuMaster>(Request.Form["AllDataArray"]);


            try
            {
                using (clsRoleMaster cls = new clsRoleMaster())
                {
                    cls.ReqType = "Update";
                    cls.RoleName = roleMenuMasters.RoleName;
                    cls.EmpId = roleMenuMasters.EmpID;
                    DataTable dtddl = DataInterface.DBRole(cls);
                }

                DataTable dt = null;
                for (int i = 0; i < updateMenuMasters.Count; i++)
                {

                    using (clsRoleMaster cls = new clsRoleMaster())
                    {
                        cls.ReqType = "Update";
                        cls.RoleId = roleMenuMasters.RoleId;
                        cls.RoleName = roleMenuMasters.RoleName;
                        cls.EmpId = roleMenuMasters.EmpID;
                        cls.EmpCode = roleMenuMasters.EmpCode;
                        cls.MenuId = int.Parse(updateMenuMasters[i].MenuId);
                        cls.SubMenuId = int.Parse(updateMenuMasters[i].SubMenuId);
                        DataTable dtddl = DataInterface.DBRole(cls);
                    }
                    //dt = DataInterface1.SaveMenu(roleMenuMasters.RoleId, roleMenuMasters.RoleName, roleMenuMasters.EmpID, 
                    //    roleMenuMasters.EmpCode, updateMenuMasters[i]);

                }

                JSONresult = JsonConvert.SerializeObject(dt);

            }
            catch (Exception ex)
            {
                using (clsError clsE = new clsError())
                {
                    clsE.ReqType = "Insert";
                    clsE.Mode = "WEB";
                    clsE.ErrorDescrption = ex.Message;
                    clsE.FunctionName = "UpdateMenu";
                    clsE.Link = "UserRole/UpdateMenu";
                    clsE.PageName = "UserRole Controller";
                    clsE.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clsE);
                }

            }



            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }
        [SessionAttribute]

        public JsonResult GetRoleData(string RoleId, string EmpId)
        {
            JsonResult result = new JsonResult();

            try
            {
                using (clsRoleMaster cls = new clsRoleMaster())
                {
                    cls.ReqType = "ViewRole";
                    cls.RoleId = RoleId;
                    cls.EmpId = EmpId;
                    cls.CompanyId = ClsSession.CompanyID;
                    using (DataSet dt = DataInterface.ViewDBRole(cls))
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
                    cls.FunctionName = "GetEmpdtl";
                    cls.Link = "Employee/GetEmpDtl";
                    cls.PageName = "Employee Controller";
                    cls.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(cls);
                }
            }

            return result;

        }
    }
}