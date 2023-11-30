using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunnet_NBFC.Models;
using Sunnet_NBFC.App_Code;
using System.Web.Script.Serialization;
using System.Data;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;
using CaptchaMvc.HtmlHelpers;
namespace Sunnet_NBFC.Controllers
{
    public class LoginController : Controller
    {
        public string JSONresult { get; private set; }
        // GET: Login
        public ActionResult Index()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(clsLogin cls)
        {

            if (string.IsNullOrEmpty(cls.UserName))
            {
                ViewBag.Error = "Please Enter User Name";
                return View("Index");
            }
            else if (string.IsNullOrEmpty(cls.UserPassword))
            {

                ViewBag.Error = "Please Enter Password.";

                return View("Index");

            }

            if (this.IsCaptchaValid("Validate your captcha"))
            {
                try
                {
                    cls.ReqType = "Check";
                    using (DataTable dt = DataInterface.DBLogin(cls))
                    {
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                Session["UserID"] = dt.Rows[0]["UserID"].ToString();

                                if (dt.Rows[0]["ChangePasswordYN"].ToString() == "0")
                                {
                                    ViewBag.Userid = Session["UserID"].ToString();
                                    return View("ChangePassword");
                                }
                                else
                                {
                                    cls.ReqType = "Update";
                                    cls.UserID = int.Parse(dt.Rows[0]["UserID"].ToString());
                                    cls.RefID = int.Parse(dt.Rows[0]["RefID"].ToString());
                                    cls.Type = dt.Rows[0]["Type"].ToString();
                                    if (UpdateLogin(cls) == 1)
                                    {
                                        if (Session["CompanyId"] != null&& Session["EmpId"]!=null && Session["EmpCode"]!=null && Session["UserName"]!=null && Session["RoleId"]!=null)
                                        {
                                            if (bool.Parse(dt.Rows[0]["IsLogged"].ToString()) == true)
                                            {
                                                return RedirectToAction("Index", "Home");
                                            }
                                            else
                                            {
                                                cls.tmpUserName = cls.UserName.ToString();

                                                return PartialView("LoginPopup", cls);

                                            }
                                        }
                                        else
                                        {
                                            ViewBag.Error = "Employee not exists or deleted.";
                                            return View("Index");
                                        }
                                        
                                    }

                                }
                            }
                            else
                            {
                                ViewBag.Error = "Please Enter Coorect User Name or Password";
                                return View("Index");
                            }
                        }
                        else
                        {
                            ViewBag.Error = "Please Enter Coorect User Name or Password";
                            return View("Index");
                        }
                    }

                }
                catch (Exception e1)
                {

                    using (clsError clsE = new clsError())
                    {
                        clsE.ReqType = "CheckLogin";
                        clsE.Mode = "WEB";
                        clsE.ErrorDescrption = e1.Message;
                        clsE.FunctionName = "CheckLogin";
                        clsE.Link = "Login/CheckLogin";
                        clsE.PageName = "Login Controller";
                        clsE.UserId = "0";
                        DataInterface.PostError(clsE);
                    }
                    
                }


            }
            else
            {
                ViewBag.Message = "Invalid Captcha";
                return View("Index");
            }
            return View("Index");
        }
        [HttpPost]

        //public ActionResult LoginPopup(clsLogin cls, string Command)

        //{

        //    if (Command == "No")

        //    {

        //        return RedirectToAction("Login");

        //    }

        //    else

        //    {

        //        cls.EmpCode = EncryptDecryptQueryString.Decryptnew(cls.tmpEmpCode);

        //        DataTable dt = new DataTable();

        //        dt = clsDBContext.ViewLogin(cls);

        //        cls.LoginSession = dt.Rows[0]["SessionId"].ToString();

        //        cls.userlogged = int.Parse(dt.Rows[0]["LOGGED"].ToString());

        //        //if (cls.userlogged == 1)

        //        //{

        //        //    LoginPopup();

        //        //    return View();

        //        //}

        //        cls.LoginId = int.Parse(dt.Rows[0]["LOGINID"].ToString());

        //        cls.UserType = dt.Rows[0]["UserType"].ToString();

        //        cls.cnt = int.Parse(dt.Rows[0]["count1"].ToString());

        //        Session["UserType"] = dt.Rows[0]["UserType"].ToString();

        //        cls.useractive = int.Parse(dt.Rows[0]["Active"].ToString());

        //        Session["EmpCode"] = cls.EmpCode;

        //        Session["EmpMail"] = dt.Rows[0]["Email"].ToString();

        //        Session["EmpName"] = dt.Rows[0]["EmpName"].ToString();

        //        Session["Scale"] = dt.Rows[0]["Scale"].ToString();

        //        cls.ReqType = "L";

        //        cls.locktype = 0;

        //        cls.userlogged = 1;

        //        cls.LoginSession = Session.SessionID;

        //        Session["SessionId"] = cls.LoginSession;

        //        clsDBContext.UpdateLogin(cls);

        //        dt = clsDBContext.ViewLogin(cls);

        //        Session["LoginSession"] = dt.Rows[0]["Sessionid"].ToString();



        //        if (EncryptDecryptQueryString.Decryptnew(cls.tmpDept) == "LOA")

        //        {

        //            return RedirectToAction("Index", "Home");

        //        }
        //        else
        //        {

        //            return RedirectToAction("Index", "Payroll/Index");
        //        }
        //    }
        //}
        
        public int UpdateLogin(clsLogin cls)
        {
            int resut = 0;
            cls.SessionID = Session.SessionID;
            cls.IsLogged = true;
            string hostName = Dns.GetHostName();
            cls.DeviceID = "";// Dns.GetHostEntry(hostName).AddressList[3].ToString();
            cls.DeviceType = "Web";
            try
            {

                using (DataTable dt = DataInterface.DBLogin(cls))
                {
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["Message"].ToString().ToUpper() == "RECORD UPDATE")
                            {
                                resut = 1;
                                Session["UserID"] = cls.UserID;
                                Session["UserType"] = cls.Type;
                                Session["SessionId"] = cls.SessionID;
                                
                                if (cls.Type == "E" || cls.Type.ToUpper() == "ADMIN" || cls.Type.ToUpper()=="SUPERADMIN" )
                                {
                                    clsEmployee cls1 = new clsEmployee();
                                    cls1.EmpID = cls.RefID;
                                    cls1.ReqType = "View";
                                    cls1.IsDelete = 0;
                                    using (DataTable dt1 = DataInterface1.dbEmployee(cls1))
                                    {
                                        if (dt1 != null)
                                        {
                                            if (dt1.Rows.Count > 0)
                                            {
                                                Session["EmpId"] = dt1.Rows[0]["EmpId"].ToString();
                                                Session["EmpCode"] = dt1.Rows[0]["EmpCode"].ToString();
                                                Session["UserName"] = dt1.Rows[0]["EmpName"].ToString();
                                                Session["CompanyId"] = dt1.Rows[0]["CompId"].ToString();
                                                Session["BranchId"] = dt1.Rows[0]["Branchid"].ToString();
                                                Session["UserImg"] = dt1.Rows[0]["ImageName"].ToString();
                                                Session["RoleId"] = dt1.Rows[0]["RoleId"].ToString();
                                                
                                            }
                                        }


                                    }

                                }
                            }
                        }

                    }
                }

                //var data = new
                //{
                //    Msg = "Success"
                //};
                //JSONresult = JsonConvert.SerializeObject(data);
                //var jsonresult = Json(JSONresult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e1)
            {

                using (clsError clsE = new clsError())
                {
                    clsE.ReqType = "CheckLogin";
                    clsE.Mode = "WEB";
                    clsE.ErrorDescrption = e1.Message;
                    clsE.FunctionName = "CheckLogin";
                    clsE.Link = "Login/CheckLogin";
                    clsE.PageName = "Login Controller";
                    clsE.UserId = "0";
                    DataInterface.PostError(clsE);
                }

                var data = new
                {
                    Msg = "Error"
                };


            }

            return resut;
        }
        public ActionResult ChangePassword()
        {
            using (clsLogin cls = new clsLogin())
            {
                cls.UserID = int.Parse(Session["UserID"].ToString());
                return View(cls);
            }


        }
        [HttpPost]
        public JsonResult UpdatePassword()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            using (clsLogin cls = jss.Deserialize<clsLogin>(Request.Form["AllDataArray"]))
            {

                try
                {

                    using (DataTable dt = DataInterface.DBLogin(cls))
                    {
                        return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
                    }

                }
                catch (Exception e1)
                {

                    using (clsError clsE = new clsError())
                    {
                        clsE.ReqType = "CheckLogin";
                        clsE.Mode = "WEB";
                        clsE.ErrorDescrption = e1.Message;
                        clsE.FunctionName = "CheckLogin";
                        clsE.Link = "Login/UpdatePasword";
                        clsE.PageName = "Login Controller";
                        clsE.UserId = "0";
                        DataInterface.PostError(clsE);
                    }

                    var data = new
                    {
                        Msg = "Error"
                    };
                    JSONresult = JsonConvert.SerializeObject(data);
                    var jsonresult = Json(JSONresult, JsonRequestBehavior.AllowGet);
                    return Json(jsonresult);
                }


            }

        }

        public ActionResult ForgetPass()
        {


            return View();
        }

        [HttpPost]
        public JsonResult IsAlreadyExistsUser(string UserName)
        {
            bool status = false;
            using (clsLogin cls = new clsLogin())
            {
                cls.ReqType = "CheckUserId";
                cls.UserName = UserName;
                using (DataTable dt = DataInterface.DBLogin(cls))
                {
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            status = true;
                        }
                        else
                        {
                            status = false;
                        }

                    }
                    else
                    {
                        status = false;
                    }
                }


                return Json(status, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.AddHeader("Cache-control", "no-store, must-revalidate, private, no-cache");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            Response.AppendToLog("window.location.reload();");

            return RedirectToAction("Index");

        }
    }


}
