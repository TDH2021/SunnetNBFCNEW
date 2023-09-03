using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunnet_NBFC.Controllers
{
    public class SideMenuBarController : Controller
    {
        // GET: SideMenuBar

        [ChildActionOnly]
        public ActionResult SetSideBarMenuesRights()
        {
            return PartialView("~/Views/shared/_MenuBarPartial.cshtml", DataInterface1.GetSideBarMenuRights());
            
        }
    }
}