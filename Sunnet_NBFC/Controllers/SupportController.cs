using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Sunnet_NBFC.App_Code;
using Sunnet_NBFC.Models;
using System.IO;
using System.Configuration;

namespace Sunnet_NBFC.Controllers
{
    public class SupportController : Controller
    {
        // GET: Support
        public ActionResult CreateTicket()
        {
            ProductController P = new ProductController();
            ViewBag.Product = ClsCommon.ToSelectList(P.GetProduct(0), "ProdId", "ProdName");
            P.Dispose();
            return View();
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTicket(clsTicket cls, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {

                string FileName = "";
                if (postedFile != null)
                {

                    FileName = Path.GetFileNameWithoutExtension(postedFile.FileName);

                    //To Get File Extension
                    string FileExtension = Path.GetExtension(postedFile.FileName);

                    //Add Current Date To Attached File Name
                    FileName = DateTime.Now.ToString("yyyyMMdd") + "_" + FileName.Trim() + FileExtension;

                    //Get Upload path from Web.Config file AppSettings.
                    string UploadPath = ConfigurationManager.AppSettings["ImgPath"].ToString();

                    //Its Create complete path to store in server.
                    postedFile.SaveAs(UploadPath + FileName);
                    cls.TicketDoc = FileName;
                }
                else
                {
                    //cls.TicketNo = "";
                    //cls.SoftwareName = "";
                    //cls.TicketDoc = "";
                    //cls.ClientName = "";
                    //cls.ClientRemarks = "";
                    //cls.Description = "";
                    //cls.Prodid = "0";
                    //cls.TicketStatus = "";
                }
                //To save Club Member Contact Form Detail to database table.
                if (cls.TicketId == 0)
                {
                    cls.OptType = 1;
                }
                else
                {
                    cls.OptType = 2;
                }


                if (DataInterface.PostTicket(cls) > 0)
                {
                    ModelState.Clear();
                    clsSendMail Mail = new clsSendMail();
                    clsMailTemplate Temp = new clsMailTemplate();
                    Temp.Body = Mail.TicketMail(cls.ClientName, cls.TicketNo, Server.MapPath("~/EmailTemplates/SendTicket.html"));
                    Temp.ToMail = cls.ClientEmail;
                    Temp.Subject = "[Ticket ID:" + cls.TicketNo + "]";
                    Temp.Attachment = ConfigurationManager.AppSettings["ImgPath"].ToString() + FileName;
                    Mail.SendMail(Temp);
                    ViewBag.Message = "Your Ticket Send successfully";
                }

                
            }

            ProductController P = new ProductController();
            ViewBag.Product = ClsCommon.ToSelectList(P.GetProduct(0), "ProdId", "ProdName");
            P.Dispose();
            return View("CreateTicket");
        }
        [HttpGet]
        public ActionResult ViewTicket()
        {
            DataTable dt = new DataTable();
            List<clsTicket> Lead = new List<clsTicket>();
            dt = GetTicket();

            Lead = DataInterface.ConvertDataTable<clsTicket>(dt);
            ViewBag.LeadDetails = Lead;

            return View();
            
        }

        public DataTable GetTicket(int TicketId=0,string TicketNo="",string Frmdate="",string Todate="")
        {
            clsTicket cls = new clsTicket();
            cls.OptType = 4;
            cls.TicketId = TicketId;
            cls.TicketNo = TicketNo;
            cls.FromDate = Frmdate;
            cls.ToDate = Todate;
            DataInterface db = new DataInterface();
            DataTable dt = new DataTable();
            dt = db.GetTicket(cls);
            db = null;
            cls = null;
            return dt;
        }
    }
}