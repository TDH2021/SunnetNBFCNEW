using Sunnet_NBFC.App_Code;
using Sunnet_NBFC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Sunnet_NBFC.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report

        [SessionAttribute]
        public ActionResult LeadReport(clsLeadGenerationMaster clss)
        {

            if (Session["UserID"] != null)
            {
                if (String.IsNullOrEmpty(Session["UserID"].ToString()) == true)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    List<clsLeadGenerationMaster> lst = new List<clsLeadGenerationMaster>();
                    try
                    {
                        ViewBag.MainProductList = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("View"), "MainProdId", "ProductName");

                        using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
                        {
                            cls.ReqType = "LeadReport";
                            cls.CompanyId = ClsSession.CompanyID;
                            if (ClsSession.UserType.ToUpper() != "ADMIN" && ClsSession.UserType.ToUpper() != "SUPERADMIN")
                            {

                                cls.BranchID = ClsSession.BranchId;
                            }
                            cls.MainProductId = clss.MainProductId;
                            cls.ProductId = clss.ProductId;
                            cls.LeadNo = clss.LeadNo;
                            cls.CustomerName = clss.CustomerName;
                            cls.MobileNo1 = clss.MobileNo1;
                            cls.PanNo = clss.PanNo;
                            cls.AadharNo = clss.AadharNo;
                            cls.isdelete = 0;
                            cls.LeadId = 0;
                            cls.FromDate = clss.FromDate;
                            cls.ToDate = clss.ToDate;
                            if (ClsSession.UserType.ToUpper() == "E")
                            {
                                cls.Empid = int.Parse(Session["EmpId"].ToString());
                            }
                            else
                            {
                                cls.Empid = 0;
                            }


                            using (DataTable dt = DataInterface.LeadReport(cls))
                            {
                                if (dt != null)
                                {
                                    List<clsLeadGenerationMaster> list = new List<clsLeadGenerationMaster>();
                                    list = (from DataRow row in dt.Rows

                                            select new clsLeadGenerationMaster()
                                            {
                                                LeadNo = row["LeadNo"].ToString(),
                                                MainProductName = row["MainProduct"].ToString(),
                                                ProductName = row["ProductName"].ToString(),
                                                CustomerName = row["CustomerName"].ToString(),
                                                MobileNo1 = row["Mobile No"].ToString(),
                                                CreateDate = row["Date of Submission"].ToString(),
                                                Updatedate = row["Last Update date"].ToString(),
                                                LeadStatus = row["LeadStatus"].ToString(),
                                                DisburseDate = row["Disbures date"].ToString(),
                                            }).ToList();


                                    ViewBag.lst = list;
                                }
                            }
                        }

                    }
                    catch (Exception e1)
                    {
                        using (clsError clsE = new clsError())
                        {
                            clsE.ReqType = "Insert";
                            clsE.Mode = "WEB";
                            clsE.ErrorDescrption = e1.Message;
                            clsE.FunctionName = "LeadReport";
                            clsE.Link = "Report/LeadView";
                            clsE.PageName = "Report Controller";
                            clsE.UserId = ClsSession.UserID.ToString();
                            DataInterface.PostError(clsE);
                        }
                        throw e1;
                    }

                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
        public ActionResult ExportToExcel(clsLeadGenerationMaster clss,string ReqType)
        {
            using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
            {
                cls.ReqType = ReqType;
                cls.CompanyId = ClsSession.CompanyID;
                cls.MainProductId = clss.MainProductId;
                if (ClsSession.UserType.ToUpper() != "ADMIN" && ClsSession.UserType.ToUpper() != "SUPERADMIN")
                {
                    cls.BranchID = ClsSession.BranchId;
                }
                cls.LeadNo = clss.LeadNo;
                cls.MobileNo1 = clss.MobileNo1;
                cls.FromDate = clss.FromDate;
                cls.ToDate = clss.ToDate;
                cls.LeadStatus = clss.LeadStatus;
                if (ClsSession.UserType.ToUpper() != "ADMIN" && ClsSession.UserType.ToUpper() != "SUPERADMIN")
                {
                    cls.Empid = int.Parse(Session["EmpId"].ToString());
                }

                using (DataTable dt = DataInterface.LeadReport(cls))
                {
                    if (dt != null)
                    {

                        var workbook = new XLWorkbook();

                        // Add a worksheet
                        var worksheet = workbook.Worksheets.Add("Report");

                        // Add data from DataTable to the worksheet
                        worksheet.Cell(1, 1).InsertTable(dt.AsEnumerable(), "LeadDatatable", true);
                        worksheet.Columns().AdjustToContents();
                        // Save the workbook to a MemoryStream
                        var stream = new MemoryStream();
                        workbook.SaveAs(stream);

                        // Set the position of the stream back to the beginning
                        stream.Seek(0, SeekOrigin.Begin);

                        // Return the Excel file for download
                        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ReqType + ".xlsx");
                    }


                    return RedirectToAction("LeadReport");

                }

            }
        }
        public ActionResult DisburseReport(clsLeadGenerationMaster clss)
        {

            if (Session["UserID"] != null)
            {
                if (String.IsNullOrEmpty(Session["UserID"].ToString()) == true)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    List<clsLeadGenerationMaster> lst = new List<clsLeadGenerationMaster>();
                    try
                    {
                        ViewBag.MainProductList = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("View"), "MainProdId", "ProductName");

                        using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
                        {
                            cls.ReqType = "DisburseReport";
                            cls.CompanyId = ClsSession.CompanyID;
                            if (ClsSession.UserType.ToUpper() != "ADMIN" && ClsSession.UserType.ToUpper() != "SUPERADMIN")
                            {

                                cls.BranchID = ClsSession.BranchId;
                            }
                            cls.MainProductId = clss.MainProductId;
                            cls.ProductId = clss.ProductId;
                            cls.LeadNo = clss.LeadNo;
                            cls.CustomerName = clss.CustomerName;
                            cls.MobileNo1 = clss.MobileNo1;
                            cls.PanNo = clss.PanNo;
                            cls.AadharNo = clss.AadharNo;
                            cls.isdelete = 0;
                            cls.LeadId = 0;
                            cls.FromDate = clss.FromDate;
                            cls.ToDate = clss.ToDate;
                            if (ClsSession.UserType.ToUpper() == "E")
                            {
                                cls.Empid = int.Parse(Session["EmpId"].ToString());
                            }
                            else
                            {
                                cls.Empid = 0;
                            }


                            using (DataTable dt = DataInterface.LeadReport(cls))
                            {
                                if (dt != null)
                                {
                                    List<clsLeadGenerationMaster> list = new List<clsLeadGenerationMaster>();
                                    list = (from DataRow row in dt.Rows

                                            select new clsLeadGenerationMaster()
                                            {
                                                LeadNo = row["LeadNo"].ToString(),
                                                MainProductName = row["MainProduct"].ToString(),
                                                ProductName = row["ProductName"].ToString(),
                                                CustomerName = row["CustomerName"].ToString(),
                                                CreateDate = row["Case date"].ToString(),
                                                DisburseDate = row["Disbures date"].ToString(),
                                            }).ToList();


                                    ViewBag.lst = list;
                                }
                            }
                        }

                    }
                    catch (Exception e1)
                    {
                        using (clsError clsE = new clsError())
                        {
                            clsE.ReqType = "Insert";
                            clsE.Mode = "WEB";
                            clsE.ErrorDescrption = e1.Message;
                            clsE.FunctionName = "LeadReport";
                            clsE.Link = "Report/LeadView";
                            clsE.PageName = "Report Controller";
                            clsE.UserId = ClsSession.UserID.ToString();
                            DataInterface.PostError(clsE);
                        }
                        throw e1;
                    }

                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}