using Sunnet_NBFC.App_Code;
using Sunnet_NBFC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Sunnet_NBFC.Controllers
{
    public class HomeController : Controller
    {
        [SessionAttribute]
        public ActionResult Index()
        {

            if (Session["UserID"] != null)
            {
                if (String.IsNullOrEmpty(Session["UserID"].ToString()) == true)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ClsSession.CompanyID = int.Parse(Session["CompanyId"].ToString());
                    ClsSession.UserID = int.Parse(Session["UserID"].ToString());
                    ClsSession.EmpId = int.Parse(Session["EmpId"].ToString());
                    ClsSession.BranchId = int.Parse(Session["BranchId"].ToString());
                    ClsSession.RoleID = int.Parse(Session["RoleId"].ToString());
                    ClsSession.UserType = Session["UserType"].ToString();
                    List<clsDashboard> lst = new List<clsDashboard>();
                    using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
                    {
                        cls.ReqType = "Dashboard";
                        if (Session["UserType"].ToString().ToUpper() == "ADMIN" || Session["UserType"].ToString().ToUpper() == "SUPERADMIN")
                        {
                            using (DataTable dt = DataInterface.DBDashBoard(cls))
                            {
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        lst = (from DataRow row in dt.Rows

                                                select new clsDashboard ()
                                                {
                                                    cnt = row["cnt"].ToString(),
                                                    ShortStage_Name = row["ShortStage_Name"].ToString(),
                                                    Stage_Name = row["Stage_Name"].ToString(),

                                                }).ToList();

                                        ViewBag.lst = lst;
                                    }
                                }
                            }
                        }
                        else
                        {
                            cls.Empid = ClsSession.EmpId;
                            cls.BranchID = ClsSession.BranchId;
                            cls.CompanyId = ClsSession.CompanyID;
                            using (DataTable dt = DataInterface.DBDashBoard(cls))
                            {
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {

                                        lst = (from DataRow row in dt.Rows

                                               select new clsDashboard()
                                               {
                                                   cnt = row["cnt"].ToString(),
                                                   ShortStage_Name = row["ShortStage_Name"].ToString(),
                                                   Stage_Name = row["Stage_Name"].ToString(),

                                               }).ToList();

                                        ViewBag.lst = lst;
                                    }
                                }
                            }
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}