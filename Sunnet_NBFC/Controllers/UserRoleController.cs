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
        public ActionResult RoleMaster()
        {
            using (clsRoleMaster M = new clsRoleMaster())
            {
                try
                {

                    TempData["CompanyId"] = ClsSession.CompanyID;
                    using (clsSubMenu cls = new clsSubMenu())
                    {
                        cls.ReqType = "View";
                        cls.IsActive = 1;

                        using (DataTable dtddl = DataInterface1.GetSubMenu(cls))
                        {
                            M.clsSubMenulst = DataInterface.ConvertDataTable<clsRoleMaster>(dtddl);
                        }
                        M.ReqType = "Insert";
                        M.CompanyId = ClsSession.CompanyID;
                        M.IsDelete = 0;
                    }



                }
                catch (Exception ex)
                {

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
                List<clsRoleMaster> clsRoles = jss.Deserialize<List<clsRoleMaster>>(Request.Form["Role"]);
                for (int i = 0; i < clsRoles.Count; i++)
                {
                    clsRoles[i].ReqType = "Insert";
                    using (DataTable dt = DataInterface.DBRole(clsRoles[i]))
                    {
                        JSONresult= JsonConvert.SerializeObject(dt);
                        
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
                using(clsRoleMaster cls=new clsRoleMaster())
                {
                    cls.ReqType = "Update";
                    cls.RoleName = roleMenuMasters.RoleName;
                    cls.EmpId = int.Parse(roleMenuMasters.EmpID);
                    DataTable dtddl = DataInterface.DBRole(cls);
                }
                
                DataTable dt = null;
                for (int i = 0; i < updateMenuMasters.Count; i++)
                {

                    using (clsRoleMaster cls = new clsRoleMaster())
                    {
                        cls.ReqType = "Update";
                        cls.RoleId = int.Parse(roleMenuMasters.RoleId);
                        cls.RoleName = roleMenuMasters.RoleName;
                        cls.EmpId = int.Parse(roleMenuMasters.EmpID);
                        cls.EmpCode = roleMenuMasters.EmpCode;
                        cls.MenuId = int.Parse(updateMenuMasters[i].MenuId);
                        cls.SubMenuId= int.Parse(updateMenuMasters[i].SubMenuId);
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
                    clsE.FunctionName = "Deleter or Save Menu";
                    clsE.Link = "UserRole/UpdateMenu";
                    clsE.PageName = "UserRole Controller";
                    clsE.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clsE);
                }

            }

          
            
            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }

    }
}