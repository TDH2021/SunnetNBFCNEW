using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunnet_NBFC.App_Code;
using Sunnet_NBFC.Models;
namespace Sunnet_NBFC.Controllers
{
    public class DSAController : Controller
    {
        // GET: Client
        [SessionAttribute]
        public ActionResult AddDSA()
        {
            ViewBag.StateList = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");
            //using(clsCity cls=new clsCity())
            //{
            //    cls.ReqType = "View";
            //    ViewBag.CityList = ClsCommon.ToSelectList(DataInterface1.GetCity(cls), "Cityid", "CityName");
            //}
            
            return View();
        }
        [HttpPost]
        [SessionAttribute]
        public ActionResult AddDSA(clsDSAMaster cls)
        {
            if (ModelState.IsValid)
            {
                cls.COMPANYID = ClsSession.CompanyID;
                cls.CreatedBy = 1;
                if (cls.DSAId == 0)
                {
                    cls.ReqType = "Insert";

                }
                else
                {
                    cls.ReqType = "Update";
                }
                using(DataTable dt= DataInterface.DBDSAMaster(cls))
                {
                    if (dt != null)
                    {
                        ViewBag.Message = dt.Rows[0]["ReturnMessage"].ToString();
                    }
                        
                    
                }


            }
            ViewBag.StateList = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");
            ModelState.Clear();
            if (cls.DSAId == 0)
            {
                return View("AddDSA");

            }
            else
            {
                return RedirectToAction("ViewDSA");
            }   
        }
        [SessionAttribute]
        public ActionResult ViewDSA()
        {
            try
            {
                using (clsDSAMaster cls = new clsDSAMaster())
                {
                    cls.ReqType = "View";
                    using (DataTable dt = DataInterface.DBDSAMaster(cls))
                    {
                        if (dt != null)
                        {
                            ViewBag.lst = DataInterface.ConvertDataTable<clsDSAMaster>(dt);


                        }
                    }
                }
                
            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "DSA View";
                    clse.Link = "DSA/DSAView";
                    clse.PageName = "DSA Controller";
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }
            return View();
        }
        [HttpGet]
        [SessionAttribute]
        public ActionResult DSAView()
        {
            DataTable dt = new DataTable();
            List<clsLead> Lead = new List<clsLead>();
            //dt = GetLead();
            Lead = DataInterface.ConvertDataTable<clsLead>(dt);
            ViewBag.LeadDetails = Lead;

            return View();
        }

        // GET: Client
        public ActionResult EditDSA(string Id)
        {
            clsDSAMaster cls = new clsDSAMaster();
            cls.DSAId = Convert.ToInt32(Id);
            cls.ISDELETE = Convert.ToInt32('0');
            cls.COMPANYID = 1;
            cls.ReqType = "View";

            using (DataTable dt = DataInterface.DBDSAMaster(cls))
            {
                foreach (DataRow row in dt.Rows)
                {
                    cls = new clsDSAMaster();

                    cls.DSAName = row["DSAName"].ToString();
                    cls.DSAAddress = row["DSAAddress"].ToString();
                    cls.DSAContactNo = row["DSAContactNo"].ToString();
                    cls.DSAWhatsUpNo = row["DSAWhatsUpNo"].ToString();
                    cls.DSACommision = Convert.ToDecimal(row["DSACommision"].ToString());
                    cls.DSARemarks = row["DSARemarks"].ToString();
                    cls.DSAPincode = Convert.ToInt64(row["DSAPincode"].ToString());
                    cls.DSAEmail = row["DSAEmail"].ToString();
                    cls.DSAGSTNo = row["DSAGSTNo"].ToString();
                    cls.DSAId = Convert.ToInt32(row["DSAId"].ToString());
                    cls.DSACode = row["DSACode"].ToString();
                    cls.DSAStateId = Convert.ToInt32(row["DSAStateId"].ToString());
                    cls.DSACityId = Convert.ToInt32(row["DSACityId"].ToString());
                    cls.DSAIFSCCode = row["DSAIFSCCode"].ToString();
                    cls.DSABranch = row["DSABranch"].ToString();
                    cls.DSABankName = row["DSABankName"].ToString();
                    cls.DSAccountNo = row["DSAccountNo"].ToString();
                    cls.AAdharNo = row["AAdharNo"].ToString();
                    cls.PAN = row["PAN"].ToString();
                }

            }
            using (clsCity clsCity = new clsCity())
            {
                clsCity.ReqType = "View";
                clsCity.Stateid = cls.DSAStateId;
                ViewBag.CityList = ClsCommon.ToSelectList(DataInterface1.GetCity(clsCity), "CityId", "CityName");
                
            }

            
            ViewBag.StateList = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");
            return View(cls);
        }

        // GET: Client
        public ActionResult DeleteDSA(string Id)
        {
            clsDSAMaster cls = new clsDSAMaster();
            cls.DSAId = Convert.ToInt32(Id);
            cls.ISDELETE = 1;
            cls.COMPANYID = 1;
            cls.ReqType = "Delete";

            using (DataTable dt = DataInterface.DBDSAMaster(cls))
            {
               
            }
            


            return RedirectToAction("ViewDSA");
        }
    }
}