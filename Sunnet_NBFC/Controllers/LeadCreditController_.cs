using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunnet_NBFC.Models;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using Sunnet_NBFC.App_Code;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;

namespace Sunnet_NBFC.Controllers
{
    public class LeadCreditController_ : Controller
    {

        [SessionAttribute]
        public ActionResult LeadView()
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
                        using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
                        {
                            cls.ReqType = "GetLeadAllData";
                            cls.CompanyId = ClsSession.CompanyID;
                            cls.LeadNo = "";
                            cls.LeadId = 0;
                            cls.ShortStage_Name = "CreditApprove";
                            cls.StageEmpId = int.Parse(Session["EmpId"].ToString());
                            cls.Empid = 0;


                            using (DataTable dt = DataInterface.GetLeadGenerationData(cls))
                            {
                                if (dt != null)
                                {
                                    ViewBag.lst = DataInterface.ConvertDataTable<clsLeadGenerationMaster>(dt);


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
                            clsE.FunctionName = "Status View";
                            clsE.Link = "Status/ViewStatus";
                            clsE.PageName = "Status Controller";
                            clsE.UserId = "1";
                            DataInterface.PostError(clsE);
                        }
                    }

                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // GET: LeadCredit
        [SessionAttribute]
        public ActionResult LeadCredit(clsLeadMain M, string ComeFrom = "CreditApprove")
        {
            try
            {
                DataTable dtLeadDetail = new DataTable();

                if (M.LeadId > 0)
                {
                    M.ReqType = "Get";
                    M.StageId = 4;
                    M.ShortStage_Name = "CreditApprove";
                    dtLeadDetail = DataInterface2.GetLeadDetail(M);

                    if (dtLeadDetail != null && dtLeadDetail.Rows.Count > 0)
                        M = DataInterface.GetItem<clsLeadMain>(dtLeadDetail.Rows[0]);

                    M.ReqType = "Get";
                    M.StageId = 4;
                    M.ShortStage_Name = "CreditApprove";

                    M.clsLeadCredit = new clsLeadCredit();//assigned a blank value handled nullable error

                    using (DataTable dtLeadCredit = DataInterface2.GetLeadCredit(M))
                    {
                        if (dtLeadCredit != null)
                        {
                            //List<clsLeadCredit> list = new List<clsLeadCredit>();
                            clsLeadCredit Singlelist = new clsLeadCredit();
                            Singlelist = (from DataRow row in dtLeadCredit.Rows

                                    select new clsLeadCredit()
                                    {
                                        CrId = int.Parse(row["CrId"].ToString()),
                                        LeadId = int.Parse(row["LeadId"].ToString()),
                                        CIBILVerification = row["CIBILVerification"].ToString(),
                                        CibilDoc = row["CibilDoc"].ToString(),
                                        FIVerification = row["FIVerification"].ToString(),
                                        CibilRemarks = row["CibilRemarks"].ToString(),
                                        FIDoc = row["FIDoc"].ToString(),
                                        FIRemarks = row["FIRemarks"].ToString(),
                                        TVRVerification = row["TVRVerification"].ToString(),
                                        TVRDoc = row["TVRDoc"].ToString(),
                                        TVRRemarks = row["TVRRemarks"].ToString(),
                                        CashFlowVerification = row["CashFlowVerification"].ToString(),
                                        CashFlowDoc = row["CashFlowDoc"].ToString(),
                                        CashFlowRemarks = row["CashFlowRemarks"].ToString(),
                                        DependentFamilyAssessmentVerification = row["DependentFamilyAssessmentVerification"].ToString(),
                                        DependentFamilyAssessmentDoc = row["DependentFamilyAssessmentDoc"].ToString(),
                                        DependentFamilyAssessmentRemarks = row["DependentFamilyAssessmentRemarks"].ToString(),

                                        ViechleValVerfication = row["ViechleValVerfication"].ToString(),
                                        ViechleDoc = row["ViechleDoc"].ToString(),
                                        ViechleRemarks = row["ViechleRemarks"].ToString(),
                                        BankStmtDoc = row["BankStmtDoc"].ToString(),
                                        BankStmtRemarks = row["BankStmtRemarks"].ToString(),

                                        IncomeStmtVerification = row["IncomeStmtVerification"].ToString(),
                                        IncomeStmtDoc = row["IncomeStmtDoc"].ToString(),
                                        IncomeStmtRemarks = row["IncomeStmtRemarks"].ToString(),
                                        PersonalDiscussVerification = row["PersonalDiscussVerification"].ToString(),
                                        PersonalDiscussDoc = row["PersonalDiscussDoc"].ToString(),

                                        PersonalDiscusssRemarks = row["PersonalDiscusssRemarks"].ToString(),
                                        Eligiblity = row["Eligiblity"].ToString(),
                                        EligiblityDoc = row["EligiblityDoc"].ToString(),
                                        EligiblityRemarks = row["EligiblityRemarks"].ToString(),
                                        Occupation = row["Occupation"].ToString(),

                                        PropertyDocVerification = row["PropertyDocVerification"].ToString(),
                                        PropertyDoc = row["PropertyDoc"].ToString(),
                                        PropertyDocRemarks = row["PropertyDocRemarks"].ToString(),
                                        ColletralType = row["ColletralType"].ToString(),
                                        PropertyAddress = row["PropertyAddress"].ToString(),
                                        PropertySize = row["PropertySize"].ToString(),
                                        LandArea = row["LandArea"].ToString(),

                                        Dimension = row["Dimension"].ToString(),
                                        SecurityValue = row["SecurityValue"].ToString(),
                                        MarketValue = row["MarketValue"].ToString(),
                                        LandValue = row["LandValue"].ToString(),
                                        ConstrutionValue = row["ConstrutionValue"].ToString(),
                                        TotalMarketValue = row["TotalMarketValue"].ToString(),
                                        LTV = row["LTV"].ToString(),

                                        RelizableValue = row["RelizableValue"].ToString(),
                                        PropertyVal = row["PropertyVal"].ToString(),
                                        PropertyDocuments = row["PropertyDocuments"].ToString(),
                                        PropertyChain = row["PropertyChain"].ToString(),
                                        LegalOpinionBy = row["LegalOpinionBy"].ToString(),
                                        LegalReportDate = row["LegalReportDate"].ToString(),
                                        ValuerName = row["ValuerName"].ToString(),

                                        Valuation = row["Valuation"].ToString(),
                                        ValueDate = row["ValueDate"].ToString(),
                                        BussinessName = row["BussinessName"].ToString(),
                                        BussinessVinatage = row["BussinessVinatage"].ToString(),
                                        BussinessAddress = row["BussinessAddress"].ToString(),
                                        BussinessProve = row["BussinessProve"].ToString(),
                                        CamVerification = row["CamVerification"].ToString(),

                                        CAMDoc = row["CAMDoc"].ToString(),
                                        CAMRemarks = row["CAMRemarks"].ToString(),
                                        CreditRemarks = row["CreditRemarks"].ToString(),
                                        NegativeRemarks = row["NegativeRemarks"].ToString(),
                                        VehicleType = row["VehicleType"].ToString(),
                                        ChassisNo = row["ChassisNo"].ToString(),
                                        EngineNo = row["EngineNo"].ToString(),

                                        Insurername = row["Insurername"].ToString(),
                                        InsurancePolicyNo = row["InsurancePolicyNo"].ToString(),
                                        PolicyValidity = row["PolicyValidity"].ToString(),
                                        RCDate = row["RCDate"].ToString(),
                                        Fitness = row["Fitness"].ToString(),
                                        
                                        DealerId = (row["DealerId"].ToString()==""?0:int.Parse(row["DealerId"].ToString())),
                                        RTO = row["RTO"].ToString(),
                                        Transmission = row["Transmission"].ToString(),
                                        FuelType = row["FuelType"].ToString(),
                                        CarType = row["CarType"].ToString(),
                                        ProposedLoanAmountAndCommercial = row["ProposedLoanAmountAndCommercial"].ToString(),
                                        Occupancy = row["Occupancy"].ToString(),
                                        CreditCheckList = row["CreditCheckList"].ToString(),
                                        CreditCheckListID = int.Parse(row["CreditCheckListID"].ToString()),
                                        CreditCheckListRemarks = row["CreditCheckListRemarks"].ToString(),

                                    }).ToList().FirstOrDefault();

                            if(Singlelist==null)
                                M.clsLeadCredit = new clsLeadCredit();
                            else
                                M.clsLeadCredit = Singlelist;
                        }
                        
                    }

                }
                else
                {
                    return RedirectToAction("LeadView", "Lead", new { ShortStage_Name = "CreditApprove" });
                }


                //if (dtLeadCredit != null && dtLeadCredit.Rows.Count > 0)
                //    M.clsLeadCredit = DataInterface.GetItem<clsLeadCredit>(dtLeadCredit.Rows[0]);
                //else
                //    M.clsLeadCredit = new clsLeadCredit();

                ViewBag.StatusListDDL = ClsCommon.StatusDDL("CreditApprove");
                ViewBag.CrditCheckListDDL = ClsCommon.ToSelectList(DataInterface2.GetMiscForDDL("CreditCheckList"), "MiscId", "MiscName");

                //M = DataInterface1.GetItem<clsLeadCalling>(dt.Rows[0]); //for single row
                //ViewBag.AnswerListDDL = ClsCommon.AnswerDDL();
                //ViewBag.StatusListDDL = ClsCommon.StatusDDL("PrimyTel");
                M.Status = M.Status ?? "P";
                ViewBag.ComeFrom = ComeFrom;
                return View(M);

                //clsLeadCredit M = new clsLeadCredit();
                //DataTable dt = new DataTable();
                //if (Id != null && Id > 0)
                //{
                //    clsEmployee cls = new clsEmployee();
                //    cls.ReqType = "View";
                //    cls.EmpID = Convert.ToInt32("0" + Id.ToString());
                //    dt = DataInterface1.dbEmployee(cls);
                //}
                //if (dt != null && dt.Rows.Count > 0)
                //    M = DataInterface1.GetItem<clsEmployee>(dt.Rows[0]);
                //return View(M);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [SessionAttribute]
        public ActionResult LeadCredit(clsLeadMain M, FormCollection frm)
        {
            //HttpPostedFileBase CibilDocPostedFile, HttpPostedFileBase FIDocPostedFile

            ClsReturnData clsRtn = new ClsReturnData();
            clsRtn.MsgType = (int)MessageType.Fail;
            bool IsSave = false;
            ViewBag.StatusListDDL = ClsCommon.StatusDDL("CreditApprove");


            try
            {
                TempData.Clear();
                DataTable dt = new DataTable();


                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Invalid Model";
                    //return View(M);
                    return RedirectToAction("LeadCredit", "LeadCredit", new { LeadId = M.LeadId, ComeFrom = "CreditApprove" });
                }

                #region Attachments

                //------------------------------CIBIL-------------------------------------------------------------------
                if (string.IsNullOrEmpty(M.clsLeadCredit.CibilDoc) && (M.clsLeadCredit.CibilDocPostedFile == null || M.clsLeadCredit.CibilDocPostedFile.ToString() == ""))
                {
                    ViewBag.Error = " Please Upload Cibil Document";
                    //return View(M);
                    return RedirectToAction("LeadCredit", "LeadCredit", new { LeadId = M.LeadId, ComeFrom = "CreditApprove" });
                }
                
                if (M.clsLeadCredit.CibilDocPostedFile != null && M.clsLeadCredit.CibilDocPostedFile.ToString() != "")
                {
                    if (ClsCommon.CheckFileType(M.clsLeadCredit.CibilDocPostedFile.FileName.ToString()) == true)
                    {
                        M.clsLeadCredit.CibilDoc = UploadImage("CIBIL", M.clsLeadCredit.CibilDocPostedFile, M.LeadId);
                        if (!string.IsNullOrEmpty(M.clsLeadCredit.CibilDoc))
                            M.clsLeadCredit.CIBILVerification = "Yes";
                        else
                            M.clsLeadCredit.CIBILVerification = null;
                    }
                    else
                    {
                        ViewBag.Error = " Please Upload Photo in jpg,jpeg,png format.";
                        //return View(M);
                        return RedirectToAction("LeadCredit", "LeadCredit", new { LeadId = M.LeadId, ComeFrom = "CreditApprove" });
                    }
                }
                else if (!string.IsNullOrEmpty(M.clsLeadCredit.CibilDoc) && frm["chkCIBILVerification"].Split(',')[0] == "true")
                {
                    M.clsLeadCredit.CIBILVerification = "Yes";
                }
                //------------------------------CIBIL-------------------------------------------------------------------
                //------------------------------FI-------------------------------------------------------------------
                if (M.clsLeadCredit.FIDocPostedFile != null && M.clsLeadCredit.FIDocPostedFile.ToString() != "")
                {
                    if (ClsCommon.CheckFileType(M.clsLeadCredit.FIDocPostedFile.FileName.ToString()) == true)
                    {
                        M.clsLeadCredit.FIDoc = UploadImage("FI", M.clsLeadCredit.FIDocPostedFile, M.LeadId);
                        if (!string.IsNullOrEmpty(M.clsLeadCredit.FIDoc))
                            M.clsLeadCredit.FIVerification = "Yes";
                        else
                            M.clsLeadCredit.FIVerification = null;
                    }
                    else
                    {
                        ViewBag.Error = " Please Upload Photo in jpg,jpeg,png format.";
                        //return View(M);
                        return RedirectToAction("LeadCredit", "LeadCredit", new { LeadId = M.LeadId, ComeFrom = "CreditApprove" });
                    }
                }
                else if (!string.IsNullOrEmpty(M.clsLeadCredit.FIDoc) && frm["chkFIVerification"].Split(',')[0] == "true")
                {
                    M.clsLeadCredit.FIVerification = "Yes";
                }
                //------------------------------FI-------------------------------------------------------------------
                //------------------------------TVR-------------------------------------------------------------------
                if (M.clsLeadCredit.TVRDocPostedFile != null && M.clsLeadCredit.TVRDocPostedFile.ToString() != "")
                {
                    if (ClsCommon.CheckFileType(M.clsLeadCredit.TVRDocPostedFile.FileName.ToString()) == true)
                    {
                        M.clsLeadCredit.TVRDoc = UploadImage("TVR", M.clsLeadCredit.TVRDocPostedFile, M.LeadId);
                        if (!string.IsNullOrEmpty(M.clsLeadCredit.TVRDoc))
                            M.clsLeadCredit.TVRVerification = "Yes";
                        else
                            M.clsLeadCredit.TVRVerification = null;
                    }
                    else
                    {
                        ViewBag.Error = " Please Upload Photo in jpg,jpeg,png format.";
                        //return View(M);
                        return RedirectToAction("LeadCredit", "LeadCredit", new { LeadId = M.LeadId, ComeFrom = "CreditApprove" });
                    }
                }
                else if (!string.IsNullOrEmpty(M.clsLeadCredit.TVRDoc) && frm["chkTVRVerification"].Split(',')[0] == "true")
                {
                    M.clsLeadCredit.TVRVerification = "Yes";
                }
                //------------------------------TVR-------------------------------------------------------------------

                //------------------------------DependentFamilyAssessmentVerification-------------------------------------------------------------------
                if (M.clsLeadCredit.DependentFamilyAssessmentDocPostedFile != null && M.clsLeadCredit.DependentFamilyAssessmentDocPostedFile.ToString() != "")
                {
                    if (ClsCommon.CheckFileType(M.clsLeadCredit.DependentFamilyAssessmentDocPostedFile.FileName.ToString()) == true)
                    {
                        M.clsLeadCredit.DependentFamilyAssessmentDoc = UploadImage(M.clsLeadCredit.DependentFamilyAssessmentVerificationCode, M.clsLeadCredit.DependentFamilyAssessmentDocPostedFile, M.LeadId);
                        if (!string.IsNullOrEmpty(M.clsLeadCredit.DependentFamilyAssessmentDoc))
                            M.clsLeadCredit.DependentFamilyAssessmentVerification = "Yes";
                        else
                            M.clsLeadCredit.DependentFamilyAssessmentVerification = null;
                    }
                    else
                    {
                        ViewBag.Error = " Please Upload Photo in jpg,jpeg,png format.";
                        //return View(M);
                        return RedirectToAction("LeadCredit", "LeadCredit", new { LeadId = M.LeadId, ComeFrom = "CreditApprove" });
                    }
                }
                else if (!string.IsNullOrEmpty(M.clsLeadCredit.DependentFamilyAssessmentDoc) && frm["chkDependentFamilyAssessmentVerification"].Split(',')[0] == "true")
                {
                    M.clsLeadCredit.DependentFamilyAssessmentVerification = "Yes";
                }
                else if (!string.IsNullOrEmpty(M.clsLeadCredit.DependentFamilyAssessmentDoc) && frm["chkDependentFamilyAssessmentVerification"].Split(',')[0] != "true")
                {
                    M.clsLeadCredit.DependentFamilyAssessmentDoc = null;
                }

                //------------------------------DependentFamilyAssessmentVerification-------------------------------------------------------------------

                //------------------------------CashFlow-------------------------------------------------------------------
                if (M.clsLeadCredit.CashFlowDocPostedFile != null && M.clsLeadCredit.CashFlowDocPostedFile.ToString() != "")
                {
                    if (ClsCommon.CheckFileType(M.clsLeadCredit.CashFlowDocPostedFile.FileName.ToString()) == true)
                    {
                        M.clsLeadCredit.CashFlowDoc = UploadImage(M.clsLeadCredit.CashFlowCode, M.clsLeadCredit.CashFlowDocPostedFile, M.LeadId);
                        if (!string.IsNullOrEmpty(M.clsLeadCredit.CashFlowDoc))
                            M.clsLeadCredit.CashFlowVerification = "Yes";
                        else
                            M.clsLeadCredit.CashFlowVerification = null;
                    }
                    else
                    {
                        ViewBag.Error = " Please Upload Photo in jpg,jpeg,png format.";
                        //return View(M);
                        return RedirectToAction("LeadCredit", "LeadCredit", new { LeadId = M.LeadId, ComeFrom = "CreditApprove" });
                    }
                }
                //------------------------------CashFlow-------------------------------------------------------------------
                //------------------------------Viechle-------------------------------------------------------------------
                if (M.clsLeadCredit.ViechleDocPostedFile != null && M.clsLeadCredit.ViechleDocPostedFile.ToString() != "")
                {
                    if (ClsCommon.CheckFileType(M.clsLeadCredit.ViechleDocPostedFile.FileName.ToString()) == true)
                    {
                        M.clsLeadCredit.ViechleDoc = UploadImage(M.clsLeadCredit.ViechleCode, M.clsLeadCredit.ViechleDocPostedFile, M.LeadId);
                        if (!string.IsNullOrEmpty(M.clsLeadCredit.ViechleDoc))
                            M.clsLeadCredit.ViechleValVerfication = "Yes";
                        else
                            M.clsLeadCredit.ViechleValVerfication = null;
                    }
                    else
                    {
                        ViewBag.Error = " Please Upload Photo in jpg,jpeg,png format.";
                        //return View(M);
                        return RedirectToAction("LeadCredit", "LeadCredit", new { LeadId = M.LeadId, ComeFrom = "CreditApprove" });
                    }
                }
                //------------------------------Viechle-------------------------------------------------------------------

                //------------------------------BankStmt-------------------------------------------------------------------
                if (M.clsLeadCredit.BankStmtDocPostedFile != null && M.clsLeadCredit.BankStmtDocPostedFile.ToString() != "")
                {
                    if (ClsCommon.CheckFileType(M.clsLeadCredit.BankStmtDocPostedFile.FileName.ToString()) == true)
                    {
                        M.clsLeadCredit.BankStmtDoc = UploadImage(M.clsLeadCredit.BankStmtCode, M.clsLeadCredit.BankStmtDocPostedFile, M.LeadId);
                        if (!string.IsNullOrEmpty(M.clsLeadCredit.BankStmtDoc))
                            M.clsLeadCredit.BankStmtVerification = "Yes";
                        else
                            M.clsLeadCredit.BankStmtVerification = null;
                    }
                    else
                    {
                        ViewBag.Error = " Please Upload Photo in jpg,jpeg,png format.";
                        //return View(M);
                        return RedirectToAction("LeadCredit", "LeadCredit", new { LeadId = M.LeadId, ComeFrom = "CreditApprove" });
                    }
                }
                //------------------------------BankStmt-------------------------------------------------------------------\

                //------------------------------IncomeStmt-------------------------------------------------------------------
                if (M.clsLeadCredit.IncomeStmtDocPostedFile != null && M.clsLeadCredit.IncomeStmtDocPostedFile.ToString() != "")
                {
                    if (ClsCommon.CheckFileType(M.clsLeadCredit.IncomeStmtDocPostedFile.FileName.ToString()) == true)
                    {
                        M.clsLeadCredit.IncomeStmtDoc = UploadImage(M.clsLeadCredit.IncomeStmtCode, M.clsLeadCredit.IncomeStmtDocPostedFile, M.LeadId);
                        if (!string.IsNullOrEmpty(M.clsLeadCredit.IncomeStmtDoc))
                            M.clsLeadCredit.IncomeStmtVerification = "Yes";
                        else
                            M.clsLeadCredit.IncomeStmtVerification = null;
                    }
                    else
                    {
                        ViewBag.Error = " Please Upload Photo in jpg,jpeg,png format.";
                        //return View(M);
                        return RedirectToAction("LeadCredit", "LeadCredit", new { LeadId = M.LeadId, ComeFrom = "CreditApprove" });
                    }
                }
                //------------------------------IncomeStmt-------------------------------------------------------------------

                //------------------------------PersonalDiscuss-------------------------------------------------------------------
                if (M.clsLeadCredit.PersonalDiscussDocPostedFile != null && M.clsLeadCredit.PersonalDiscussDocPostedFile.ToString() != "")
                {
                    if (ClsCommon.CheckFileType(M.clsLeadCredit.PersonalDiscussDocPostedFile.FileName.ToString()) == true)
                    {
                        M.clsLeadCredit.PersonalDiscussDoc = UploadImage(M.clsLeadCredit.PersonalDiscussCode, M.clsLeadCredit.PersonalDiscussDocPostedFile, M.LeadId);
                        if (!string.IsNullOrEmpty(M.clsLeadCredit.PersonalDiscussDoc))
                            M.clsLeadCredit.PersonalDiscussVerification = "Yes";
                        else
                            M.clsLeadCredit.PersonalDiscussVerification = null;
                    }
                    else
                    {
                        ViewBag.Error = " Please Upload Photo in jpg,jpeg,png format.";
                        //return View(M);
                        return RedirectToAction("LeadCredit", "LeadCredit", new { LeadId = M.LeadId, ComeFrom = "CreditApprove" });
                    }
                }
                //------------------------------PersonalDiscuss-------------------------------------------------------------------


                //------------------------------Eligiblity-------------------------------------------------------------------
                if (M.clsLeadCredit.EligiblityDocPostedFile != null && M.clsLeadCredit.EligiblityDocPostedFile.ToString() != "")
                {
                    if (ClsCommon.CheckFileType(M.clsLeadCredit.EligiblityDocPostedFile.FileName.ToString()) == true)
                    {
                        M.clsLeadCredit.EligiblityDoc = UploadImage(M.clsLeadCredit.EligiblityCode, M.clsLeadCredit.EligiblityDocPostedFile, M.LeadId);
                        if (!string.IsNullOrEmpty(M.clsLeadCredit.EligiblityDoc))
                            M.clsLeadCredit.Eligiblity = "Yes";
                        else
                            M.clsLeadCredit.Eligiblity = null;
                    }
                    else
                    {
                        ViewBag.Error = " Please Upload Photo in jpg,jpeg,png format.";
                        //return View(M);
                        return RedirectToAction("LeadCredit", "LeadCredit", new { LeadId = M.LeadId, ComeFrom = "CreditApprove" });
                    }
                }
                //------------------------------Eligiblity-------------------------------------------------------------------

                //------------------------------Property-------------------------------------------------------------------
                if (M.clsLeadCredit.PropertyDocPostedFile != null && M.clsLeadCredit.PropertyDocPostedFile.ToString() != "")
                {
                    if (ClsCommon.CheckFileType(M.clsLeadCredit.PropertyDocPostedFile.FileName.ToString()) == true)
                    {
                        M.clsLeadCredit.PropertyDoc = UploadImage(M.clsLeadCredit.PropertyCode, M.clsLeadCredit.PropertyDocPostedFile, M.LeadId);
                        if (!string.IsNullOrEmpty(M.clsLeadCredit.PropertyDoc))
                            M.clsLeadCredit.PropertyDocVerification = "Yes";
                        else
                            M.clsLeadCredit.PropertyDocVerification = null;
                    }
                    else
                    {
                        ViewBag.Error = " Please Upload Photo in jpg,jpeg,png format.";
                        //return View(M);
                        return RedirectToAction("LeadCredit", "LeadCredit", new { LeadId = M.LeadId, ComeFrom = "CreditApprove" });
                    }
                }

                if (M.clsLeadCredit.CAMDocPostedFile != null && M.clsLeadCredit.CAMDocPostedFile.ToString() != "")
                {
                    if (ClsCommon.CheckFileType(M.clsLeadCredit.CAMDocPostedFile.FileName.ToString()) == true)
                    {
                        M.clsLeadCredit.CAMDoc = UploadImage(M.clsLeadCredit.CAMCODE, M.clsLeadCredit.CAMDocPostedFile, M.LeadId);
                        if (!string.IsNullOrEmpty(M.clsLeadCredit.CAMDoc))
                            M.clsLeadCredit.CamVerification = "Yes";
                        else
                            M.clsLeadCredit.CamVerification = null;
                    }
                    else
                    {
                        ViewBag.Error = " Please Upload Photo in jpg,jpeg,png format.";
                        //return View(M);
                        return RedirectToAction("LeadCredit", "LeadCredit", new { LeadId = M.LeadId, ComeFrom = "CreditApprove" });
                    }
                }
                //------------------------------Property-------------------------------------------------------------------


                #endregion


                if (M.clsLeadCredit.CrId <= 0)
                {
                    M.ReqType = "Insert";
                    M.clsLeadCredit.ReqType = "Insert";
                    M.clsLeadCredit.LeadId = M.LeadId;
                }
                else
                {
                    M.ReqType = "Update";
                    M.clsLeadCredit.ReqType = "Update";
                }

                dt = DataInterface2.SaveLeadCredit(M.clsLeadCredit);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"])) > 0)
                    {
                        IsSave = true;
                    }
                }
                if (IsSave)
                {
                    M.ReqType = "UpdateStatus";
                    dt = DataInterface2.UpdateLeadStatus(M);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        clsRtn.ID = Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"]));
                        clsRtn.Message = Convert.ToString(dt.Rows[0]["ReturnMessage"]);
                        clsRtn.MessageDesc = clsRtn.Message;
                        if (clsRtn.ID > 0)
                        {
                            clsRtn.MsgType = (int)MessageType.Success;
                        }
                        else
                        {
                            clsRtn.MsgType = (int)MessageType.Fail;
                        }
                    }
                }




            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "InsertUpdate";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "LeadCalling";
                    clse.Link = "Lead/LeadCalling";
                    clse.PageName = "Lead Calling Controller";
                    clse.UserId = ClsSession.UserID.ToString();
                    DataInterface.PostError(clse);
                }
            }
            if (IsSave)
            {

                TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Saved/Updated";
                return RedirectToAction("LeadView", "LeadCredit");
                //return RedirectToAction("LeadView", "Lead", new { ShortStage_Name = "CreditApprove" });
            }
            else
            {
                DataTable dtLeadCredit = new DataTable();
                if (M.LeadId > 0)
                {
                    M.ReqType = "Get";
                    M.StageId = 4;
                    dtLeadCredit = DataInterface2.GetLeadCredit(M);
                }

                if (dtLeadCredit != null && dtLeadCredit.Rows.Count > 0)
                    M.clsLeadCredit = DataInterface.GetItem<clsLeadCredit>(dtLeadCredit.Rows[0]);

                //M = DataInterface1.GetItem<clsLeadCalling>(dt.Rows[0]); //for single row

                M.Status = M.Status ?? "P";

                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved/Updated";
                //return View(M);
                return RedirectToAction("LeadCredit", "LeadCredit", new { LeadId = M.LeadId, ComeFrom = "CreditApprove" });
            }
        }

        // GET: LeadCredit
        [SessionAttribute]
        public ActionResult LeadCreditView(int LeadId = 0)
        {
            try
            {
                clsLeadMain M = new clsLeadMain();
                M.LeadId = LeadId;

                DataTable dtLeadDetail = new DataTable();
                DataTable dtLeadCredit = new DataTable();
                if (M.LeadId > 0)
                {
                    M.ReqType = "Get";
                    M.StageId = 4;
                    dtLeadDetail = DataInterface2.GetLeadDetail(M);
                    dtLeadCredit = DataInterface2.GetLeadCredit(M);
                }
                else
                {
                    return RedirectToAction("LeadView", "Lead", new { ShortStage_Name = "CreditApprove" });
                }

                if (dtLeadDetail != null && dtLeadDetail.Rows.Count > 0)
                    M = DataInterface.GetItem<clsLeadMain>(dtLeadDetail.Rows[0]);

                if (dtLeadCredit != null && dtLeadCredit.Rows.Count > 0)
                    M.clsLeadCredit = DataInterface.GetItem<clsLeadCredit>(dtLeadCredit.Rows[0]);
                else
                    M.clsLeadCredit = new clsLeadCredit();

                ViewBag.StatusListDDL = ClsCommon.StatusDDL("CreditApprove");

                M.Status = M.Status ?? "P";
                return PartialView("_LeadCreditView", M);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [SessionAttribute]
        public ActionResult LeadCredit_em(clsEmployee M, HttpPostedFileBase postedFile)
        {
            ClsReturnData clsRtn = new ClsReturnData();
            clsRtn.MsgType = (int)MessageType.Fail;
            string up = "";
            try
            {
                TempData.Clear();
                DataTable dt = new DataTable();


                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Invalid Model";
                    return View(M);
                }

                if (postedFile != null && postedFile.ToString() != "")
                {
                    if (ClsCommon.CheckFileType(postedFile.FileName.ToString()) == true)
                    {
                        //UploadImage(string FileType, HttpPostedFileBase file, int ? id = 0)
                        //up = UploadEmpPhoto(postedFile);
                    }
                    else
                    {
                        ViewBag.Error = " Please Upload Photo in jpg,jpeg,png format.";
                        return View(M);
                    }
                }


                if (M.EmpID <= 0)
                {
                    M.ReqType = "Insert";
                }
                else
                {
                    M.ReqType = "Update";
                }

                if (up != "")
                {
                    M.ImageName = up;
                }

                //clsRetData = DataInterface2.SaveEmployee(M);
                dt = DataInterface1.dbEmployee(M);

                if (dt != null && dt.Rows.Count > 0)
                {
                    clsRtn.ID = Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"]));
                    clsRtn.Message = Convert.ToString(dt.Rows[0]["ReturnMessage"]);
                    clsRtn.MessageDesc = clsRtn.Message;
                    if (clsRtn.ID > 0)
                        clsRtn.MsgType = (int)MessageType.Success;
                    else
                        clsRtn.MsgType = (int)MessageType.Fail;
                }
            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Insert";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "Employee";
                    clse.Link = "Employee/Employee";
                    clse.PageName = "Employee Controller";
                    clse.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clse);
                }
            }
            if (clsRtn.ID > 0)
            {

                TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Saved/Updated";
                return RedirectToAction("EmployeeView", "Employee");
            }
            else
            {
                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved/Updated";
                return View(M);
            }
        }


        public string UploadImage(string FileType, HttpPostedFileBase file, int? id = 0)
        {
            clsLeadCredit M = new clsLeadCredit();
            string upfile = "";
            try
            {
                if (file.ContentLength > 0)
                {
                    string FilePrefix = "";
                    string FolderPath = "";

                    if (FileType == "CIBIL")
                    {
                        FilePrefix = "CIBIL_";
                        FolderPath = "/UploadedFiles/LeadCredit/";
                    }
                    else if (FileType == "FI")
                    {
                        FilePrefix = "FI_";
                        FolderPath = "/UploadedFiles/LeadCredit/";
                    }
                    else if (FileType == "TVR")
                    {
                        FilePrefix = "TVR_";
                        FolderPath = "/UploadedFiles/LeadCredit/";
                    }
                    else if (FileType == M.DependentFamilyAssessmentVerificationCode)
                    {
                        FilePrefix = M.DependentFamilyAssessmentVerificationCode + "_";
                        FolderPath = "/UploadedFiles/LeadCredit/";
                    }
                    else if (FileType == M.CashFlowCode)
                    {
                        FilePrefix = M.CashFlowCode + "_";
                        FolderPath = "/UploadedFiles/LeadCredit/";
                    }
                    else if (FileType == M.ViechleCode)
                    {
                        FilePrefix = M.ViechleCode + "_";
                        FolderPath = "/UploadedFiles/LeadCredit/";
                    }
                    else if (FileType == M.BankStmtCode)
                    {
                        FilePrefix = M.BankStmtCode + "_";
                        FolderPath = "/UploadedFiles/LeadCredit/";
                    }
                    else if (FileType == M.IncomeStmtCode)
                    {
                        FilePrefix = M.IncomeStmtCode + "_";
                        FolderPath = "/UploadedFiles/LeadCredit/";
                    }
                    else if (FileType == M.PersonalDiscussCode)
                    {
                        FilePrefix = M.PersonalDiscussCode + "_";
                        FolderPath = "/UploadedFiles/LeadCredit/";
                    }
                    else if (FileType == M.EligiblityCode)
                    {
                        FilePrefix = M.EligiblityCode + "_";
                        FolderPath = "/UploadedFiles/LeadCredit/";
                    }
                    else if (FileType == M.PropertyCode)
                    {
                        FilePrefix = M.PropertyCode + "_";
                        FolderPath = "/UploadedFiles/LeadCredit/";
                    }
                    else if (FileType == M.CAMCODE)
                    {
                        FilePrefix = M.CAMCODE + "_";
                        FolderPath = "/UploadedFiles/LeadCredit/";
                    }




                    //string _FileName = Path.GetFileName(p.ImageName.FileName);
                    Guid newid = Guid.NewGuid();
                    string FileExtension = Path.GetExtension(file.FileName);
                    string _FileName = FilePrefix + newid.ToString() + FileExtension;
                    string _path = Path.Combine(Server.MapPath("~" + FolderPath), _FileName);
                    file.SaveAs(_path);
                    upfile = _FileName;
                }
                //ViewBag.Message = "File Uploaded Successfully!!";

                return upfile;
            }
            catch
            {
                //ViewBag.Message = "File upload failed!!";
                return upfile;
            }
        }

        public FileResult Download(string FileType, string ImageName)
        {
            string FilePrefix = "";
            string FolderPath = "";
            if (FileType == "CIBIL" || FileType != "CIBIL")
            {
                FilePrefix = "CIBIL_";
                FolderPath = "/UploadedFiles/LeadCredit/";
            }
            //var FileVirtualPath = "~/UploadedFiles/LeadCredit/" + ImageName;

            var FileVirtualPath = "~" + FolderPath + ImageName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));

            ////------------other ref code--------------------
            //var filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
            //var contentType = MimeMapping.GetMimeMapping(fileName);
            //return File(filePath, contentType, fileName);

        }


        [HttpPost]
        [SessionAttribute]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            string FileName = "";
            string FilePath = "";
            string ContentType = "";

            if (file != null && file.ContentLength > 0)
            {
                // Validate file type, size, etc.

                // Save file to server
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                file.SaveAs(filePath);

                //// Update model with new image info
                //var model = new ImageModel
                //{
                //    FileName = fileName,
                //    FilePath = filePath,
                //    ContentType = file.ContentType
                //};

                // Return view with updated model
                return View("Index");
            }

            // Handle validation errors, etc.
            return View("Index");
        }




    }
}