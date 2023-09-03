using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunnet_NBFC.Models;
using System.Data;
namespace Sunnet_NBFC.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(clsUser cls, HttpPostedFileBase postedFile)
        {
            //Use Namespace called :  System.IO
            if (postedFile != null)
            {

                string FileName = Path.GetFileNameWithoutExtension(postedFile.FileName);

                //To Get File Extension
                string FileExtension = Path.GetExtension(postedFile.FileName);

                //Add Current Date To Attached File Name
                FileName = DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim() + FileExtension;

                //Get Upload path from Web.Config file AppSettings.
                string UploadPath = ConfigurationManager.AppSettings["ImgPath"].ToString();

                //Its Create complete path to store in server.
                postedFile.SaveAs(UploadPath + FileName);
                cls.Userpic = FileName;
                //byte[] bytes;
                //using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                //{
                //    bytes = br.ReadBytes(postedFile.ContentLength);
                //}
                //cls.UserPic = Convert.ToBase64String(bytes);
                cls.UserPicExt = Path.GetExtension(postedFile.FileName);
                cls.UserPicName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                cls.IsActive = 1;    
            }
            else
            {
                cls.Userpic = "";
                cls.UserPicExt = "";
                cls.UserPicName = "";
                cls.IsActive = 1;
            }
            //To save Club Member Contact Form Detail to database table.
            if (cls.UserId == 0)
            {
                cls.OptType = 1;
            }
            else
            {
                cls.OptType = 2;
            }


            if (DataInterface.User(cls) > 0)
            {
                ModelState.Clear();
                ViewBag.Message = "User Detail Save successfully";
            }


            return View();
        }
        [HttpPost]
        public ActionResult Login(clsUser cls)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = new DataTable();
                DataInterface db = new DataInterface();
                cls.OptType = 5;
                dt = db.GetUser(cls);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() != "")
                    {
                        Session["UserName"] = dt.Rows[0]["UserName"].ToString();
                        Session["UserPicName"] = dt.Rows[0]["UserPic"].ToString();
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        ViewBag.Message = "User Name Or Password Wrong";
                    }
                }
                else
                {
                    ViewBag.Message = "User Name Or Password Wrong";
                }
            }
            return View("Login");
        }

        [HttpGet]
        public ActionResult ViewUser()
        {
            DataTable dt = new DataTable();
            List<clsUser> User = new List<clsUser>();
            DataInterface db = new DataInterface();
            clsUser cls = new clsUser();
            cls.OptType = 4;
            dt = db.GetUser(cls);

            User = DataInterface.ConvertDataTable<clsUser>(dt);
            ViewBag.UserDetails = User;
            cls = null;
            db.Dispose();
            return View();
        }
      
    }
}