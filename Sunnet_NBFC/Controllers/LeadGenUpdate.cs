using Newtonsoft.Json;
using Sunnet_NBFC.App_Code;
using Sunnet_NBFC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DocumentFormat.OpenXml.EMMA;
using iText.Layout.Properties;
using System.Security.AccessControl;

namespace Sunnet_NBFC.Controllers
{
    public class LeadGenUpdateController : Controller
    {
        // GET: LeadGeneration
        public string JSONresult { get; private set; }
        public ActionResult LeadGenUpdate(string LeadId)
        {
            clsLeadGenerationMaster clslead = new clsLeadGenerationMaster();

            try
            {
                ViewBag.CompanyId = ClsSession.CompanyID;
                using (clsLeadGenerationMaster clsStatus = new clsLeadGenerationMaster())
                {
                    clsStatus.CompanyId = ClsSession.CompanyID;
                }


                //ViewBag.StateList = ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");




                ViewBag.MainProductList = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("View"), "MainProdId", "ProductName");
                ViewBag.MaterialStatusList = ClsCommon.ToSelectList(DataInterface1.GetMiseddl("Martial Status"), "MiscName", "MiscName");
                ViewBag.ProductId = "";
                using (clsCenter cls = new clsCenter())
                {
                    cls.ReqType = "View";
                    cls.CompanyID = ClsSession.CompanyID;
                    ViewBag.CenterList = ClsCommon.ToSelectList(DataInterface2.DBCenter(cls), "CenterId", "CenterName");
                }

                using (clsBranch cls = new clsBranch())
                {
                    cls.ReqType = "View";
                    cls.CompanyID = ClsSession.CompanyID;
                    cls.IsDelete = 0;
                    ViewBag.BranchList = ClsCommon.ToSelectList(DataInterface2.ViewBranch(cls), "BranchId", "BranchName");
                }
                ViewBag.RelationList = ClsCommon.ToSelectList(DataInterface2.GetMiscForDDL("Relation"), "MiscId", "MiscName");
                ViewBag.Colletral = ClsCommon.ToSelectList(DataInterface2.GetMiscForDDL("Colletral"), "MiscId", "MiscName");
                using (clsDSAMaster cls = new clsDSAMaster())
                {
                    cls.ReqType = "View";
                    using (DataTable dt1 = DataInterface.DBDSAMaster(cls))
                    {
                        if (dt1 != null)
                        {
                            ViewBag.Dealer = ClsCommon.ToSelectList(dt1, "DSAId", "DSAName");


                        }
                    }
                }
                List<clsLeadGenerationMaster> LeadGenerationdataModels = new List<clsLeadGenerationMaster>();


                ViewBag.LeadGenerationdataModels = LeadGenerationdataModels;
                //ViewBag.PostalApi = ConfigurationManager.AppSettings["PostalAPI"].ToString();
                ViewBag.PostalApi = ConfigurationManager.AppSettings["RapidPostalAPI"].ToString();


                clslead.ReqType = "ViewLead";
                clslead.CompanyId = ClsSession.CompanyID;
                if (ClsSession.UserType.ToUpper() == "SUPERADMIN" || ClsSession.UserType.ToUpper() == "ADMIN")
                {
                    clslead.BranchID = 0;

                }
                else
                {
                    clslead.BranchID = ClsSession.BranchId;

                }
                clslead.LeadId = int.Parse(LeadId);

                using (DataTable dt = DataInterface.GetLeadGenerationData(clslead))
                {
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {

                            clslead.LeadId = int.Parse(row["LeadId"].ToString());
                            clslead.LeadNo = row["LeadNo"].ToString();
                            clslead.MainProductId = row["MainProdId"].ToString();
                            clslead.MainProductName = row["MainProductName"].ToString();
                            clslead.ProductId = row["ProdId"].ToString();
                            clslead.CustTypeRequried = row["CustTypeRequried"].ToString();
                            clslead.ProductName = row["ProductName"].ToString();
                            clslead.BranchID = row["BranchID"].ToString() == "" ? 0 : int.Parse(row["BranchID"].ToString());
                            clslead.CenterId = row["CenterId"].ToString() == "" ? 0 : int.Parse(row["CenterId"].ToString());
                            clslead.PLLoanBranch = row["PLLoanBranch"].ToString() == "" ? 0 : int.Parse(row["PLLoanBranch"].ToString());

                            clslead.ReuestedLoanAmount = row["ReuestedLoanAmount"].ToString();
                            clslead.ReuestedLoanTenure = row["ReuestedLoanTenure"].ToString();
                            clslead.EstMonthIncome = row["EstMonthIncome"].ToString();
                            clslead.EstFamilyIncome = row["EstFamilyIncome"].ToString();
                            clslead.EstMonthExpense = row["EstMonthExpense"].ToString();
                            clslead.CurMonthObligation = row["CurMonthObligation"].ToString();
                            clslead.NoofDependent = row["NoofDependent"].ToString();
                            clslead.RefernceName = row["RefernceName"].ToString();
                            clslead.RefenceMobileNo = row["RefenceMobileNo"].ToString();
                            clslead.ShortStage_Name = row["ShortStage_Name"].ToString();
                            clslead.RefenceRelation = row["RefenceRelation"].ToString();
                            clslead.RefernceName1 = row["RefernceName1"].ToString();
                            clslead.RefenceRelation1 = row["RefenceRelation1"].ToString();
                            clslead.RefenceMobileNo1 = row["RefenceMobileNo1"].ToString();
                            clslead.LoanPurpose = row["LoanPurpose"].ToString();
                            clslead.RepaymentType = row["RepaymentType"].ToString();
                            clslead.UserRemarks = row["UserRemarks"].ToString();
                            clslead.ViechleModel = row["ViechleModel"].ToString();
                            clslead.ViechleColor = row["ViechleColor"].ToString();
                            clslead.ViechleCompany = row["ViechleCompany"].ToString();
                            clslead.MFGYear = row["MFGYear"].ToString();
                            clslead.ViechleOwner = row["ViechleOwner"].ToString();
                            clslead.FuelType = row["FuelType"].ToString();
                            clslead.InsuranceType = row["InsuranceType"].ToString();
                            clslead.ViechleRegYear = row["ViechleRegYear"].ToString();
                            clslead.ViechleRegYear = row["ViechleRegYear"].ToString();
                            clslead.RegistrationDate = row["RegistrationDate"].ToString();
                            clslead.Owner = row["Owner"].ToString();
                            clslead.EstValueViechle = row["EstValueViechle"].ToString();
                            clslead.InsuranceStatus = row["InsuranceStatus"].ToString();
                            clslead.Insurer = row["Insurer"].ToString();
                            clslead.PolicyNo = row["PolicyNo"].ToString();
                            clslead.ValidityDate = row["ValidityDate"].ToString();
                            clslead.InsEndValidityDate = row["InsEndValidityDate"].ToString();
                            clslead.OnRoadPrice = row["OnRoadPrice"].ToString();
                            clslead.ExShowRoomPrice = row["ExShowRoomPrice"].ToString();
                            clslead.FORecomedAmt = row["FORecomedAmt"].ToString();
                            clslead.StatusDesc = row["StatusDesc"].ToString();
                            clslead.DSAId = row["DSAId"].ToString();
                            clslead.PerformaInvoice = row["PerformaInvoice"].ToString();
                            clslead.NoofDependent = row["NoofDependent"].ToString();
                            clslead.ColletralSecurityType = row["ColletralSecurityType"].ToString();
                            clslead.FORecomedAmt = row["FORecomedAmt"].ToString();
                            clslead.EstValueofscurity = row["EstValueofscurity"].ToString();
                            clslead.PropertyType = row["PropertyType"].ToString();
                            clslead.Propertyarea = row["Propertyarea"].ToString();
                            clslead.PropertyAddress = row["PropertyAddress"].ToString();
                        }


                    }
                }
                clslead.ReqType = "View";
                using (DataTable dtcust = DataInterface.GetLeadGenerationCustomer(clslead))
                {
                    if (dtcust != null)
                    {
                        if (dtcust.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dtcust.Rows.Count; i++)
                            {
                                if (dtcust.Rows[i]["CustType"].ToString() == "C")
                                {
                                    clslead.LeadCustomerId = dtcust.Rows[i]["LeadCustomerId"].ToString();
                                    clslead.PanNo = dtcust.Rows[i]["PanNo"].ToString();
                                    clslead.AadharNo = dtcust.Rows[i]["AadharNo"].ToString();
                                    clslead.PrefixName = dtcust.Rows[i]["PrefixName"].ToString();
                                    clslead.FirstName = dtcust.Rows[i]["FirstName"].ToString();
                                    clslead.MiddleName = dtcust.Rows[i]["MiddleName"].ToString();
                                    clslead.LastName = dtcust.Rows[i]["LastName"].ToString();
                                    clslead.FatherName = dtcust.Rows[i]["FatherName"].ToString();
                                    clslead.MotherName = dtcust.Rows[i]["MotherName"].ToString();
                                    clslead.SpouseName = dtcust.Rows[i]["SpouseName"].ToString();
                                    clslead.Gender = dtcust.Rows[i]["Gender"].ToString();
                                    clslead.DateofBirth = dtcust.Rows[i]["DateofBirth"].ToString();
                                    clslead.MartialStatus = dtcust.Rows[i]["MartialStatus"].ToString();
                                    clslead.PresentAddress = dtcust.Rows[i]["PresentAddress"].ToString();
                                    clslead.PresentPincode = dtcust.Rows[i]["PresentPincode"].ToString();
                                    clslead.PermanentAddress = dtcust.Rows[i]["PermanentAddress"].ToString();
                                    clslead.PermanentPincode = dtcust.Rows[i]["PermanentPincode"].ToString();
                                    clslead.CibilScore = dtcust.Rows[i]["CibilScore"].ToString();
                                    clslead.PresentStateId = dtcust.Rows[i]["PresentStateId"].ToString();
                                    clslead.PresentCityId = dtcust.Rows[i]["PresentCityId"].ToString();
                                    clslead.PermanentStateId = dtcust.Rows[i]["PermanentStateId"].ToString();
                                    clslead.PermanentCityId = dtcust.Rows[i]["PermanentCityId"].ToString();
                                    clslead.MobileNo1 = dtcust.Rows[i]["MobileNo1"].ToString();
                                    clslead.MobileNo2 = dtcust.Rows[i]["MobileNo2"].ToString();
                                    clslead.FatherMobileNo = dtcust.Rows[i]["FatherMobileNo"].ToString();
                                    clslead.MotherMobileNo = dtcust.Rows[i]["MotherMobileNo"].ToString();
                                    clslead.SpouseMobileNo = dtcust.Rows[i]["SpouseMobileNo"].ToString();
                                    clslead.AadharNo = dtcust.Rows[i]["AadharNo"].ToString();
                                    clslead.AAdharverfiy = dtcust.Rows[i]["AAdharverfiy"].ToString();
                                    clslead.PanNo = dtcust.Rows[i]["PanNo"].ToString();
                                    clslead.PanVerify = dtcust.Rows[i]["PanVerify"].ToString();
                                    clslead.OccupationID = dtcust.Rows[i]["OccupationID"].ToString();
                                    clslead.PresentDistrict = dtcust.Rows[i]["PresentDistrict"].ToString();
                                    clslead.PresentVillage = dtcust.Rows[i]["PresentVillage"].ToString();
                                    clslead.PermanentDistrict = dtcust.Rows[i]["PermanentDistrict"].ToString();
                                    clslead.PermanentVillage = dtcust.Rows[i]["PermanentVillage"].ToString();
                                    clslead.ElectricBill = dtcust.Rows[i]["ElectricBill"].ToString();
                                    clslead.OwnerShip = dtcust.Rows[i]["OwnerShip"].ToString();
                                    clslead.CustLandMark = dtcust.Rows[i]["LandMark"].ToString();
                                    clslead.EmailId = dtcust.Rows[i]["EmailID"].ToString();
                                    clslead.CIFID = dtcust.Rows[i]["CIF"].ToString();
                                    clslead.QualificationId = dtcust.Rows[i]["QualificationId"].ToString();
                                    clslead.CustImage = dtcust.Rows[i]["CustImage"].ToString();
                                    clslead.ElectricBill = dtcust.Rows[i]["ElectricBill"].ToString();
                                    clslead.Cust_IsSameCurrentperadd = int.Parse(dtcust.Rows[0]["IsSameCurrentperadd"].ToString());
                                    clslead.bCust_IsSameCurrentperadd = clslead.Cust_IsSameCurrentperadd == 1 ? true : false;
                                    if (System.IO.File.Exists(Server.MapPath("~/Img/ApplicantImgs/" + Convert.ToString(clslead.CustImage))))
                                    {
                                        clslead.CustImage = Server.MapPath("~/Img/ApplicantImgs/") + Convert.ToString(clslead.CustImage);
                                    }
                                    if (System.IO.File.Exists(Server.MapPath("~/Img/ApplicantImgs/" + Convert.ToString(clslead.ElectricBill))))
                                    {
                                        clslead.ElectricBill = Server.MapPath("~/Img/ApplicantImgs/") + Convert.ToString(clslead.ElectricBill);
                                    }
                                }
                                else if (dtcust.Rows[i]["CustType"].ToString() == "CO_Applicant")
                                {
                                    clslead.CO_LeadCustomerId = dtcust.Rows[i]["LeadCustomerId"].ToString();
                                    clslead.Co_PrefixName = dtcust.Rows[i]["PrefixName"].ToString();
                                    clslead.CO_FirstName = dtcust.Rows[i]["FirstName"].ToString();
                                    clslead.CO_MiddleName = dtcust.Rows[i]["MiddleName"].ToString();
                                    clslead.CO_LastName = dtcust.Rows[i]["LastName"].ToString();
                                    clslead.CO_FatherName = dtcust.Rows[i]["FatherName"].ToString();
                                    clslead.CO_MotherName = dtcust.Rows[i]["MotherName"].ToString();
                                    clslead.CO_DOB = dtcust.Rows[i]["DateofBirth"].ToString();
                                    clslead.CO_Gender = dtcust.Rows[i]["Gender"].ToString();
                                    clslead.CO_Marital_Status = dtcust.Rows[i]["MartialStatus"].ToString();
                                    clslead.CO_PresentAddress = dtcust.Rows[i]["PresentAddress"].ToString();
                                    clslead.CO_PresentPinCode = dtcust.Rows[i]["PresentPincode"].ToString();
                                    clslead.CO_PermanentAddress = dtcust.Rows[i]["PermanentAddress"].ToString();
                                    clslead.CO_PermanentPincode = dtcust.Rows[i]["PermanentPincode"].ToString();
                                    clslead.CO_CIBIL = dtcust.Rows[i]["CibilScore"].ToString();
                                    clslead.CO_PresentStateId = dtcust.Rows[i]["PresentStateId"].ToString();
                                    clslead.CO_PresentCityId = dtcust.Rows[i]["PresentCityId"].ToString();
                                    clslead.CO_PermanentStateId = dtcust.Rows[i]["PermanentStateId"].ToString();
                                    clslead.CO_PermanentCityId = dtcust.Rows[i]["PermanentCityId"].ToString();
                                    clslead.CO_Mobile_No = dtcust.Rows[i]["MobileNo1"].ToString();
                                    clslead.CO_Adhaar = dtcust.Rows[i]["AadharNo"].ToString();
                                    clslead.CO_AAdharverfiy = dtcust.Rows[i]["AAdharverfiy"].ToString();
                                    clslead.CO_PAN = dtcust.Rows[i]["PanNo"].ToString();
                                    clslead.CO_Panverfiy = dtcust.Rows[i]["PanVerify"].ToString();
                                    clslead.CO_Occupation = dtcust.Rows[i]["OccupationID"].ToString();
                                    clslead.CO_PresentDistrict = dtcust.Rows[i]["PresentDistrict"].ToString();
                                    clslead.CO_PresentVillage = dtcust.Rows[i]["PresentVillage"].ToString();
                                    clslead.CO_PermanentDistrict = dtcust.Rows[i]["PermanentDistrict"].ToString();
                                    clslead.CO_PermanentVillage = dtcust.Rows[i]["PermanentVillage"].ToString();
                                   
                                    clslead.CO_ElectricBill = dtcust.Rows[i]["ElectricBill"].ToString();
                                    clslead.Co_OwnerShip = dtcust.Rows[i]["OwnerShip"].ToString();
                                    clslead.Co_LandMark = dtcust.Rows[i]["LandMark"].ToString();
                                    clslead.CO_Email_Id = dtcust.Rows[i]["EmailID"].ToString();
                                    clslead.Co_CIF = dtcust.Rows[i]["CIF"].ToString();
                                    clslead.CO_image = dtcust.Rows[i]["CustImage"].ToString();
                                    clslead.CO_ElectricBill = dtcust.Rows[i]["ElectricBill"].ToString();
                                    clslead.CO_IsSameCurrentperadd = int.Parse(dtcust.Rows[0]["IsSameCurrentperadd"].ToString());
                                    clslead.bCO_IsSameCurrentperadd = clslead.CO_IsSameCurrentperadd == 1 ? true : false;
                                    if (System.IO.File.Exists(Server.MapPath("~/Img/COApplicantImgs/" + Convert.ToString(clslead.CO_image))))
                                    {
                                        clslead.CO_image = Server.MapPath("~/Img/COApplicantImgs/") + Convert.ToString(clslead.CO_image);
                                    }
                                    if (System.IO.File.Exists(Server.MapPath("~/Img/COApplicantImgs/" + Convert.ToString(clslead.CO_ElectricBill))))
                                    {
                                        clslead.CO_ElectricBill = Server.MapPath("~/Img/COApplicantImgs/") + Convert.ToString(clslead.CO_ElectricBill);
                                    }
                                }
                                else if (dtcust.Rows[i]["CustType"].ToString() == "Gurantor")
                                {
                                    Gurantor gurantor = new Gurantor();
                                    gurantor.G_LeadCustomerId = dtcust.Rows[i]["LeadCustomerId"].ToString();
                                    gurantor.G_PrefixName = dtcust.Rows[i]["PrefixName"].ToString();
                                    gurantor.G_FirstName = dtcust.Rows[i]["FirstName"].ToString();
                                    gurantor.G_MiddleName = dtcust.Rows[i]["MiddleName"].ToString();
                                    gurantor.G_LastName = dtcust.Rows[i]["LastName"].ToString();
                                    gurantor.G_FatherName = dtcust.Rows[i]["FatherName"].ToString();
                                    gurantor.G_SpouseName = dtcust.Rows[i]["SpouseName"].ToString();
                                    gurantor.G_DOB = dtcust.Rows[i]["DateofBirth"].ToString();
                                    gurantor.G_Gender = dtcust.Rows[i]["Gender"].ToString();
                                    gurantor.G_Marital_Status = dtcust.Rows[i]["MartialStatus"].ToString();
                                    gurantor.G_PresentAddress = dtcust.Rows[i]["PresentAddress"].ToString();
                                    gurantor.G_PresentPinCode = dtcust.Rows[i]["PresentPincode"].ToString();
                                    gurantor.G_PermanentAddress = dtcust.Rows[i]["PermanentAddress"].ToString();
                                    gurantor.G_PermanentPincode = dtcust.Rows[i]["PermanentPincode"].ToString();
                                    gurantor.G_CibilScore = dtcust.Rows[i]["CibilScore"].ToString();
                                    gurantor.G_PresentStateId = dtcust.Rows[i]["PresentStateId"].ToString();
                                    gurantor.G_PresentCityId = dtcust.Rows[i]["PresentCityId"].ToString();
                                    gurantor.G_PermanentStateId = dtcust.Rows[i]["PermanentStateId"].ToString();
                                    gurantor.G_PermanentCityId = dtcust.Rows[i]["PermanentCityId"].ToString();
                                    gurantor.G_Mobile_No = dtcust.Rows[i]["MobileNo1"].ToString();
                                    gurantor.G_AadharNo = dtcust.Rows[i]["AadharNo"].ToString();
                                    gurantor.G_AadharVerify = int.Parse(dtcust.Rows[i]["AAdharverfiy"].ToString());
                                    gurantor.G_PanNo = dtcust.Rows[i]["PanNo"].ToString();
                                    gurantor.G_PanVerify = int.Parse(dtcust.Rows[i]["PanVerify"].ToString());
                                    gurantor.G_Occupation = dtcust.Rows[i]["OccupationID"].ToString();
                                    gurantor.G_PresentDistrict = dtcust.Rows[i]["PresentDistrict"].ToString();
                                    gurantor.G_PresentVillage = dtcust.Rows[i]["PresentVillage"].ToString();
                                    gurantor.G_PermanentDistrict = dtcust.Rows[i]["PermanentDistrict"].ToString();
                                    gurantor.G_PermanentVillage = dtcust.Rows[i]["PermanentVillage"].ToString();
                                    gurantor.G_OwnerShip = dtcust.Rows[i]["OwnerShip"].ToString();
                                    gurantor.G_LandMark = dtcust.Rows[i]["LandMark"].ToString();
                                    gurantor.G_EmailId = dtcust.Rows[i]["EmailID"].ToString();
                                    gurantor.G_CIF = dtcust.Rows[i]["CIF"].ToString();
                                    gurantor.G_LeadNo = clslead.LeadNo;
                                    gurantor.G_IsSameCurrentperadd = int.Parse(dtcust.Rows[0]["IsSameCurrentperadd"].ToString());
                                    gurantor.bG_IsSameCurrentperadd = gurantor.G_IsSameCurrentperadd == 1 ? true : false;
                                    clslead.GuList.Add(gurantor);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                using (clsError clsE = new clsError())
                {
                    // Get stack trace for the exception with source file information
                    var st = new StackTrace(e1, true);
                    // Get the top stack frame
                    var frame = st.GetFrame(0);
                    // Get the line number from the stack frame
                    var line = frame.GetFileLineNumber();
                    clsE.ReqType = "Update";
                    clsE.Mode = "WEB";
                    clsE.ErrorDescrption = e1.Message + "Line " + line + "Frame " + frame;
                    clsE.FunctionName = "AddRequestLead";
                    clsE.Link = "Status/AddRequestLead";
                    clsE.PageName = "Status Controller";
                    clsE.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clsE);
                }
            }
            return View("LeadGenUpdate", clslead);
        }




        [HttpPost]
        public JsonResult AddRequestLeadGeneration(clsLeadGenerationMaster cls)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            clsLeadGenerationMaster master = jss.Deserialize<clsLeadGenerationMaster>(Request.Form["AllDataArray"]);

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    try
                    {
                        master.CompanyId = ClsSession.CompanyID;
                        master.BranchID = ClsSession.BranchId;
                        master.CreatedBy = ClsSession.EmpId;
                        string message = "";

                        if (master.MainProductId == "1")
                        {
                            master.ViechleRegYear = "0";
                            master.MFGYear = "0";
                            master.EstValueViechle = "0";
                        }
                        if (master.MainProductId == "1")
                        {
                            master.NoofDependent = "0";
                            master.FORecomedAmt = "0";
                        }
                        if (master.MainProductId == "4")
                        {
                            master.EstValueViechle = "0";
                            master.DSAId = "0";
                        }
                        using (DataTable dt = DataInterface.GetLeadGeneration(master))
                        {
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    message = dt.Rows[0]["ReturnMessage"].ToString();

                                }
                            }

                            JSONresult = JsonConvert.SerializeObject(dt);
                        }
                        scope.Complete();
                        return Json(JSONresult, JsonRequestBehavior.AllowGet);

                    }
                    catch (Exception e1)
                    {
                        scope.Dispose();
                        using (clsError clsE = new clsError())
                        {
                            clsE.ReqType = "Insert";
                            clsE.Mode = "WEB";
                            clsE.ErrorDescrption = e1.Message;
                            clsE.FunctionName = "AddRequestLead";
                            clsE.Link = "Status/AddRequestLead";
                            clsE.PageName = "Status Controller";
                            clsE.UserId = ClsSession.EmpId.ToString();
                            DataInterface.PostError(clsE);
                        }
                    }

                    return Json(JSONresult, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e1)
            {
                DataTable dt = new DataTable();
                using (clsError clsE = new clsError())
                {
                    clsE.ReqType = "Insert";
                    clsE.Mode = "WEB";
                    clsE.ErrorDescrption = e1.Message;
                    clsE.FunctionName = "AddRequestStatus";
                    clsE.Link = "Status/AddStatus";
                    clsE.PageName = "Status Controller";
                    clsE.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clsE);
                }


            }

            return Json("", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateGurrenter()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            Gurantor master = jss.Deserialize<Gurantor>(Request.Form["AllDataArray"]);


            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    try
                    {
                        master.G_Reqtype = "Update";
                        master.G_CompanyId = ClsSession.CompanyID;
                        master.G_BranchID = ClsSession.BranchId;
                        master.G_UpdatedBy = ClsSession.EmpId;
                        string message = "";
                        using (DataTable dt = DataInterface.UpdateGurrenter(master))
                        {

                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    message = dt.Rows[0]["ReturnMessage"].ToString();
                                }
                            }


                            JSONresult = JsonConvert.SerializeObject(dt);
                        }
                        scope.Complete();
                        return Json(JSONresult, JsonRequestBehavior.AllowGet);

                    }
                    catch (Exception e1)
                    {
                        scope.Dispose();
                        using (clsError clsE = new clsError())
                        {
                            clsE.ReqType = "Insert";
                            clsE.Mode = "WEB";
                            clsE.ErrorDescrption = e1.Message;
                            clsE.FunctionName = "UpdateGurrenter";
                            clsE.Link = "LeadGenUpdate/UpdateGurrenter";
                            clsE.PageName = "Lead GenUpdate Controller";
                            clsE.UserId = ClsSession.EmpId.ToString();
                            DataInterface.PostError(clsE);
                        }
                    }

                    return Json(JSONresult, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e1)
            {
                DataTable dt = new DataTable();
                using (clsError clsE = new clsError())
                {
                    clsE.ReqType = "Insert";
                    clsE.Mode = "WEB";
                    clsE.ErrorDescrption = e1.Message;
                    clsE.FunctionName = "UpdateGurrenter";
                    clsE.Link = "LeadGenUpdate/UpdateGurrenter";
                    clsE.PageName = "Lead GenUpdate Controller";
                    clsE.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clsE);
                }


            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateCustomer()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            clsLeadGenerationMaster master = jss.Deserialize<clsLeadGenerationMaster>(Request.Form["AllDataArray"]);


            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    try
                    {
                        master.ReqType = "Update";
                        master.CompanyId = ClsSession.CompanyID;
                        master.BranchID = ClsSession.BranchId;
                        master.UpdatedBy = ClsSession.EmpId;
                        string message = "";

                        HttpPostedFileBase file = null;
                        if (Request.Files.Count > 0)
                        {
                            if (Request.Files[0] != null)
                            {
                                file = Request.Files["ApplicantImg"];
                                //Extract Image File Name.
                                string fileName = System.IO.Path.GetFileName(file.FileName);

                                fileName = master.LeadNo + "_Applicant_" + fileName;

                                //Set the Image File Path.
                                string filePath = Server.MapPath("~/Img/ApplicantImgs");

                                //Save the Image File in Folder.
                                file.SaveAs(filePath + "\\" + fileName);
                                master.CustImage = fileName;

                            }

                            if (Request.Files[1] != null)
                            {
                                file = Request.Files["AppElectricBill"];
                                //Extract Image File Name.
                                string fileName = System.IO.Path.GetFileName(file.FileName);

                                fileName = master.LeadNo + "_ElectricBill_" + fileName;

                                //Set the Image File Path.
                                string filePath = Server.MapPath("~/Img/ApplicantImgs");

                                //Save the Image File in Folder.
                                file.SaveAs(filePath + "\\" + fileName);
                                master.ElectricBill = fileName;

                            }
                        }
                       
                        using (DataTable dt = DataInterface.UpdateCustomer(master))
                        {

                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    message = dt.Rows[0]["ReturnMessage"].ToString();
                                }
                            }


                            JSONresult = JsonConvert.SerializeObject(dt);
                        }
                        scope.Complete();
                        return Json(JSONresult, JsonRequestBehavior.AllowGet);

                    }
                    catch (Exception e1)
                    {
                        scope.Dispose();
                        using (clsError clsE = new clsError())
                        {
                            clsE.ReqType = "Insert";
                            clsE.Mode = "WEB";
                            clsE.ErrorDescrption = e1.Message;
                            clsE.FunctionName = "UpdateGurrenter";
                            clsE.Link = "LeadGenUpdate/UpdateCustomer";
                            clsE.PageName = "Lead GenUpdate Controller";
                            clsE.UserId = ClsSession.EmpId.ToString();
                            DataInterface.PostError(clsE);
                        }
                    }

                    return Json(JSONresult, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e1)
            {
                using (clsError clsE = new clsError())
                {
                    clsE.ReqType = "Insert";
                    clsE.Mode = "WEB";
                    clsE.ErrorDescrption = e1.Message;
                    clsE.FunctionName = "UpdateGurrenter";
                    clsE.Link = "LeadGenUpdate/UpdateCustomer";
                    clsE.PageName = "Lead GenUpdate Controller";
                    clsE.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clsE);
                }


            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateCoBorrower()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            clsLeadGenerationMaster master = jss.Deserialize<clsLeadGenerationMaster>(Request.Form["AllDataArray"]);


            try
            {
                HttpPostedFileBase file = null;
                if (Request.Files.Count > 0)
                {

                    if (Request.Files[0] != null)
                    {
                        file = Request.Files["COApplicantImg"];
                        //Extract Image File Name.
                        string fileName = System.IO.Path.GetFileName(file.FileName);

                        fileName = master.LeadNo + "_CO_Applicant_" + fileName;

                        //Set the Image File Path.
                        string filePath = Server.MapPath("~/Img/COApplicantImgs");

                        //Save the Image File in Folder.
                        file.SaveAs(filePath + "\\" + fileName);
                        master.CustImage = fileName;
                    }

                    if (Request.Files[1] != null)
                    {
                        file = Request.Files["COElectricBill"];
                        //Extract Image File Name.
                        string fileName = System.IO.Path.GetFileName(file.FileName);

                        fileName = master.LeadNo + "_CO_ElectricBill_" + fileName;

                        //Set the Image File Path.
                        string filePath = Server.MapPath("~/Img/COApplicantImgs");

                        //Save the Image File in Folder.
                        file.SaveAs(filePath + "\\" + fileName);
                        master.ElectricBill = fileName;
                    }
                }
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    try
                    {
                        master.ReqType = "Update";
                        master.CompanyId = ClsSession.CompanyID;
                        master.BranchID = ClsSession.BranchId;
                        master.UpdatedBy = ClsSession.EmpId;
                        string message = "";
                        using (DataTable dt = DataInterface.UpdateCoBorrower(master))
                        {

                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    message = dt.Rows[0]["ReturnMessage"].ToString();
                                }
                            }


                            JSONresult = JsonConvert.SerializeObject(dt);
                        }
                        scope.Complete();
                        return Json(JSONresult, JsonRequestBehavior.AllowGet);

                    }
                    catch (Exception e1)
                    {
                        scope.Dispose();
                        using (clsError clsE = new clsError())
                        {
                            clsE.ReqType = "Insert";
                            clsE.Mode = "WEB";
                            clsE.ErrorDescrption = e1.Message;
                            clsE.FunctionName = "UpdateCoBorrower";
                            clsE.Link = "LeadGenUpdate/UpdateCoBorrower";
                            clsE.PageName = "Lead GenUpdate Controller";
                            clsE.UserId = ClsSession.EmpId.ToString();
                            DataInterface.PostError(clsE);
                        }
                    }

                    return Json(JSONresult, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e1)
            {

                using (clsError clsE = new clsError())
                {
                    clsE.ReqType = "Insert";
                    clsE.Mode = "WEB";
                    clsE.ErrorDescrption = e1.Message;
                    clsE.FunctionName = "UpdateCoBorrower";
                    clsE.Link = "LeadGenUpdate/UpdateCoBorrower";
                    clsE.PageName = "Lead GenUpdate Controller";
                    clsE.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clsE);
                }


            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetApiResponse(string stage, string OTP, string referenceId, string AadharNo, string email, string urlvariable, string responseCode, string responseMessage, string response)
        {
            JsonResult result = new JsonResult();

            string request = "";
            string apiname = "";

            if (stage == "1")
            {
                request = "{aadhaarNumber:" + AadharNo + ", email:" + email + ", url:" + urlvariable + "}";
                apiname = "Aadhar Verify";

            }
            else if (stage == "2")
            {
                request = "{otp:" + OTP + ", refrenceid:" + referenceId + ", url:" + urlvariable + "}";
                apiname = "Aadhar OTP Verify";

            }


            DataTable dt = DataInterface1.SetApiResponse(request, responseCode, responseMessage, response, apiname);

            result = this.Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);

            return result;

        }


        public JsonResult SetPANApiResponse(string stage, string ApiName, string OTP, string referenceId, string Panno, string email, string urlvariable,
                             string responseCode, string responseMessage, string response)
        {
            JsonResult result = new JsonResult();

            string request = "";

            if (stage == "1")
            {
                request = Panno;


            }
            else if (stage == "2")
            {
                request = Panno;


            }


            DataTable dt = DataInterface1.SetApiResponse(request, responseCode, responseMessage, response, ApiName);

            result = this.Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);

            return result;

        }

        public ActionResult GurrenterDetails(string LeadNo, string LeadCustomerId)
        {
            Gurantor gurantor = new Gurantor();

            try
            {
                using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
                {
                    cls.ReqType = "View";
                    cls.CompanyId = ClsSession.CompanyID;
                    cls.LeadNo = LeadNo;
                    cls.LeadCustomerId = LeadCustomerId;
                    using (DataTable dtcust = DataInterface.GetLeadGenerationCustomer(cls))
                    {
                        using (DataTable dt = DataInterface.GetLeadGenerationCustomer(cls))
                        {

                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    gurantor.G_LeadCustomerId = dtcust.Rows[0]["LeadCustomerId"].ToString();
                                    gurantor.G_PrefixName = dtcust.Rows[0]["PrefixName"].ToString();
                                    gurantor.G_FirstName = dtcust.Rows[0]["FirstName"].ToString();
                                    gurantor.G_MiddleName = dtcust.Rows[0]["MiddleName"].ToString();
                                    gurantor.G_LastName = dtcust.Rows[0]["LastName"].ToString();
                                    gurantor.G_FatherName = dtcust.Rows[0]["FatherName"].ToString();
                                    gurantor.G_SpouseName = dtcust.Rows[0]["SpouseName"].ToString();
                                    gurantor.G_DOB = dtcust.Rows[0]["DateofBirth"].ToString();
                                    gurantor.G_Gender = dtcust.Rows[0]["Gender"].ToString();
                                    gurantor.G_Marital_Status = dtcust.Rows[0]["MartialStatus"].ToString();
                                    gurantor.G_PresentAddress = dtcust.Rows[0]["PresentAddress"].ToString();
                                    gurantor.G_PresentPinCode = dtcust.Rows[0]["PresentPincode"].ToString();
                                    gurantor.G_PermanentAddress = dtcust.Rows[0]["PermanentAddress"].ToString();
                                    gurantor.G_PermanentPincode = dtcust.Rows[0]["PermanentPincode"].ToString();
                                    gurantor.G_CibilScore = dtcust.Rows[0]["CibilScore"].ToString();
                                    gurantor.G_PresentStateId = dtcust.Rows[0]["PresentStateId"].ToString();
                                    gurantor.G_PresentCityId = dtcust.Rows[0]["PresentCityId"].ToString();
                                    gurantor.G_PermanentStateId = dtcust.Rows[0]["PermanentStateId"].ToString();
                                    gurantor.G_PermanentCityId = dtcust.Rows[0]["PermanentCityId"].ToString();
                                    gurantor.G_Mobile_No = dtcust.Rows[0]["MobileNo1"].ToString();
                                    gurantor.G_AadharNo = dtcust.Rows[0]["AadharNo"].ToString();
                                    gurantor.G_AadharVerify = int.Parse(dtcust.Rows[0]["AAdharverfiy"].ToString());
                                    gurantor.G_PanNo = dtcust.Rows[0]["PanNo"].ToString();
                                    gurantor.G_PanVerify = int.Parse(dtcust.Rows[0]["PanVerify"].ToString());
                                    gurantor.G_Occupation = dtcust.Rows[0]["OccupationID"].ToString();
                                    gurantor.G_PresentDistrict = dtcust.Rows[0]["PresentDistrict"].ToString();
                                    gurantor.G_PresentVillage = dtcust.Rows[0]["PresentVillage"].ToString();
                                    gurantor.G_PermanentDistrict = dtcust.Rows[0]["PermanentDistrict"].ToString();
                                    gurantor.G_PermanentVillage = dtcust.Rows[0]["PermanentVillage"].ToString();
                                    gurantor.G_OwnerShip = dtcust.Rows[0]["OwnerShip"].ToString();
                                    gurantor.G_LandMark = dtcust.Rows[0]["LandMark"].ToString();
                                    gurantor.G_EmailId = dtcust.Rows[0]["EmailID"].ToString();
                                    gurantor.G_CIF = dtcust.Rows[0]["CIF"].ToString();
                                    gurantor.G_IsSameCurrentperadd = int.Parse(dtcust.Rows[0]["IsSameCurrentperadd"].ToString());

                                }

                            }

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
                    clsE.FunctionName = "Gurrenter sDetails";
                    clsE.Link = "LeadGenUpdate/GurrenterDetails";
                    clsE.PageName = "Lead Update Controller";
                    clsE.UserId = Session["UserID"].ToString();
                    DataInterface.PostError(clsE);
                }
            }

            return PartialView("_AddGuranterDetailsUpdate", gurantor);
        }
    }
}