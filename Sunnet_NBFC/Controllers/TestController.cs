using iText.Layout.Properties;
using Sunnet_NBFC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunnet_NBFC.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            string FileName = "SancLetter_01_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";
            string Filepath = Server.MapPath("~/" + ConfigurationManager.AppSettings["GenLetterPath"].ToString() + "/" + FileName);
            using (clsTestLetter cls = new clsTestLetter())
            {
                cls.GenSanctionLetter(Filepath, "TestLetter");
            }
            if (FileName != "")
            {

                
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Filepath));
                Response.WriteFile(Filepath);
                Response.End();
            }
            //string path = Filepath;
            //byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);
            return View();
        }
    }
}