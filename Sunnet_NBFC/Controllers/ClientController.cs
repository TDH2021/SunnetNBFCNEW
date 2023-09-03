using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunnet_NBFC.Models;
namespace Sunnet_NBFC.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult AddClient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddClient(clsClient cls)
        {
            if (ModelState.IsValid)
            {
                if (cls.ClientId == 0)
                {
                    cls.OptType = 1;
                }
                else
                {
                    cls.OptType = 2;
                }


                if (DataInterface.PostClient(cls) > 0)
                {
                    ModelState.Clear();
                    ViewBag.Message = "Client Detail Save successfully";
                }

            }
            return View();
        }

        [HttpGet]
        public ActionResult ClientView()
        {
            DataTable dt = new DataTable();
            List<clsLead> Lead = new List<clsLead>();
            //dt = GetLead();
            Lead = DataInterface.ConvertDataTable<clsLead>(dt);
            ViewBag.LeadDetails = Lead;

            return View();
        }
    }
}