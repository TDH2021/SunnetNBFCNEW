﻿using System;
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

        public JsonResult Role()
        {
            string JSONresult = "";
            JavaScriptSerializer jss = new JavaScriptSerializer();

           


            try
            {
                List<clsRoleMaster> clsRoles = jss.Deserialize<List<clsRoleMaster>>(Request.Form["Role"]);
                for (int i = 0; i < clsRoles.Count; i++)
                {
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

                DataTable dtddl = DataInterface1.DeleteMenu(roleMenuMasters.RoleName, roleMenuMasters.EmpID);
                DataTable dt = null;
                for (int i = 0; i < updateMenuMasters.Count; i++)
                {
                    dt = DataInterface1.SaveMenu(roleMenuMasters.RoleId, roleMenuMasters.RoleName, roleMenuMasters.EmpID, 
                        roleMenuMasters.EmpCode, updateMenuMasters[i]);

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