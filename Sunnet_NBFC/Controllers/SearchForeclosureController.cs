using iText.Layout.Properties;
using Sunnet_NBFC.App_Code;
using Sunnet_NBFC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using SessionAttribute = Sunnet_NBFC.App_Code.SessionAttribute;

namespace Sunnet_NBFC.Controllers
{
    public class SearchForeclosureController : Controller
    {
        // GET: SearchForeclosure
        [SessionAttribute]
        public ActionResult SearchForeclosure()
        {
            return View();
        }


        // GET: SearchForeclosure/Details/5
        [SessionAttribute]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SearchForeclosure/Create
        [SessionAttribute]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchForeclosure/Create
        [SessionAttribute]
        [HttpPost]
        public ActionResult SearchForeclosure(clsForecloseEntry cls)
        {
            //clsForecloseEntry cls = new clsForecloseEntry();
            try
            {
                // TODO: Add insert logic here
                DataSet ds = new DataSet();
                DataTable dtLeaddtl = new DataTable();
                List<clsForecloseEntry> lst = new List<clsForecloseEntry>();
                //if (cls.SearchLeadNo > 0)
                //{
                cls.ReqType = "Get";
                //cls.SearchLeadNo = Convert.ToInt32("0" + LeadNo.ToString());
                //ds = DataInterface1.dbForeClose(cls);
                if (ds != null && ds.Tables.Count > 0)
                    dtLeaddtl = ds.Tables[0];



                //}

                if (dtLeaddtl != null && dtLeaddtl.Rows.Count > 0)
                {
                    // cls = DataInterface.GetItem<clsForecloseEntry>(dtLeaddtl.Rows[0]);
                    // list = DataInterface.ConvertDataTable<clsForecloseEntry>(dtLeaddtl);
                    List<clsForecloseEntry> list = new List<clsForecloseEntry>();
                    list = (from DataRow row in dtLeaddtl.Rows

                            select new clsForecloseEntry()
                            {
                                LeadId= Convert.ToInt32(row["Leadid"].ToString()),
                                LeadNo = row["LeadNo"].ToString(),
                                MainProduct = row["MainProduct"].ToString(),
                                ProductName = row["ProductName"].ToString(),
                                BranchName = row["BranchName"].ToString(),
                                NetDisbursementAmount = Convert.ToDecimal(row["NetDisbursementAmount"].ToString()),
                                CompanyID = Convert.ToInt32(row["CompanyID"].ToString()),
                                ROI = Convert.ToDecimal(row["ROI"].ToString()),
                                Tenure = Convert.ToInt32(row["Tenure"].ToString()),
                                LoanStartDate = row["LoanStartDate"].ToString(),
                                LoanEndDate = row["LoanEndDate"].ToString(),
                                EmiAmount = Convert.ToDecimal(row["EmiAmount"].ToString()),
                                TotalInst = Convert.ToDecimal(row["TotalInst"].ToString()),
                                DSAName = row["DSAName"].ToString(),
                                CustName = row["CustName"].ToString(),
                                FatherName = row["FatherName"].ToString(),
                                MobileNo1 = row["MobileNo1"].ToString(),
                                MobileNo2 = row["MobileNo2"].ToString(),
                                pos = Convert.ToDecimal(row["pos"].ToString()),
                                CurrentMonthInterest = Convert.ToDecimal(row["CurrentMonthInterest"].ToString()),
                                InstalmentOverdue = Convert.ToDecimal(row["InstalmentOverdue"].ToString()),
                                ForeclosureCharges = Convert.ToDecimal(row["ForeclosureCharges"].ToString()),
                                GstOnForclose = Convert.ToDecimal(row["GstOnForclose"].ToString()),
                                ExcessAmount = Convert.ToDecimal(row["ExcessAmount"].ToString()),
                                BouncingCharges = Convert.ToDecimal(row["BouncingCharges"].ToString()),
                                PenalCharges = Convert.ToDecimal(row["PenalCharges"].ToString()),
                                OtherCharges = Convert.ToDecimal(row["OtherCharges"].ToString()),
                                FinalForeclosureAmount = Convert.ToDecimal(row["FinalForeclosureAmount"].ToString()),

                            }).ToList();
                    ViewBag.SearchList = list;
                }


                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: SearchForeclosure/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SearchForeclosure/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SearchForeclosure/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SearchForeclosure/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
