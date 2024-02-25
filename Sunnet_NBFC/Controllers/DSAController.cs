using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI;
using Sunnet_NBFC.App_Code;
using Sunnet_NBFC.Models;
using ClosedXML.Excel;

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
        public ActionResult ViewDSA(clsDSAMaster clss)
        {
            try
            {
                ViewBag.StateList = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");
                using (clsDSAMaster cls = new clsDSAMaster())
                {
                    cls.ReqType = "View";
                    cls.ISDELETE = 0;
                    cls.DSACode = clss.DSACode;
                    if(clss.SearchPinCode!=null && clss.SearchPinCode != "")
                    {
                        cls.DSAPincode = long.Parse(clss.SearchPinCode);
                    }
                    cls.PAN=clss.SearchPAN;
                
                    cls.AAdharNo = clss.SearchAadhar;
                    cls.DSAContactNo = clss.SearchContNo;
                    if (clss.SearchCityId != null && clss.SearchCityId != "")
                    {
                        cls.DSACityId=int.Parse(clss.SearchCityId);
                    }
                    if (clss.SearchStateId != null && clss.SearchStateId != "")
                    {
                        cls.DSAStateId = int.Parse(clss.SearchStateId);
                    }
                    using (DataTable dt = DataInterface.DBDSAMaster(cls))
                    {
                        if (dt != null)
                        {
                            List<clsDSAMaster> list = new List<clsDSAMaster>();
                            list = (from DataRow row in dt.Rows

                                    select new clsDSAMaster()
                                    {
                                        DSAId =int.Parse(row["DSAId"].ToString()),
                                        DSACode = row["DSACode"].ToString(),
                                        DSAName = row["DSAName"].ToString(),
                                        DSAContactNo = row["DSAContactNo"].ToString(),
                                        DSAAddress = row["DSAAddress"].ToString(),
                                        CityName = row["CityName"].ToString(),
                                        StateName = row["StateName"].ToString(),
                                        DSAPincode =long.Parse(row["DSAPincode"].ToString()),
                                        DSAEmail = row["DSAEmail"].ToString(),
                                        PAN = row["PAN"].ToString(),
                                        AAdharNo = row["AAdharNo"].ToString(),
                                        DSAWhatsUpNo = row["DSAWhatsUpNo"].ToString(),
                                        DSAGSTNo = row["DSAGSTNo"].ToString(),
                                        DSAccountNo = row["DSAccountNo"].ToString(),
                                        DSABankName = row["DSABankName"].ToString(),
                                        DSABranch = row["DSABranch"].ToString(),
                                        DSAIFSCCode = row["DSAIFSCCode"].ToString(),
                                        DSACommision =decimal.Parse(row["DSACommision"].ToString()),
                                        DSARemarks = row["DSARemarks"].ToString(),
                                    }).ToList();
                            ViewBag.lst=list;

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
                    clse.UserId = ClsSession.EmpId.ToString();
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
        [SessionAttribute]
        public ActionResult ExportToExcel(clsDSAMaster clss)
        {
            using (clsDSAMaster cls = new clsDSAMaster())
            {
                cls.ReqType = "View";
                cls.ISDELETE = 0;
                cls.DSACode = clss.DSACode;
                if (clss.SearchPinCode != null && clss.SearchPinCode != "")
                {
                    cls.DSAPincode = long.Parse(clss.SearchPinCode);
                }
                cls.PAN = clss.SearchPAN;

                cls.AAdharNo = clss.SearchAadhar;
                cls.DSAContactNo = clss.SearchContNo;
                if (clss.SearchCityId != null && clss.SearchCityId != "")
                {
                    cls.DSACityId = int.Parse(clss.SearchCityId);
                }
                if (clss.SearchStateId != null && clss.SearchStateId != "")
                {
                    cls.DSAStateId = int.Parse(clss.SearchStateId);
                }
                using (DataTable dt = DataInterface.DBDSAMaster(cls))
                {
                    if (dt != null)
                    {

                        var workbook = new XLWorkbook();

                        // Add a worksheet
                        var worksheet = workbook.Worksheets.Add("DSA Report");

                        // Add data from DataTable to the worksheet
                        worksheet.Cell(1, 1).InsertTable(dt.AsEnumerable(), "LeadDatatable", true);
                        worksheet.Columns().AdjustToContents();
                        // Save the workbook to a MemoryStream
                        var stream = new MemoryStream();
                        workbook.SaveAs(stream);

                        // Set the position of the stream back to the beginning
                        stream.Seek(0, SeekOrigin.Begin);

                        // Return the Excel file for download
                        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DSAReport.xlsx");
                    }


                    return RedirectToAction("DSAView");

                }

            }
        }
    }
}