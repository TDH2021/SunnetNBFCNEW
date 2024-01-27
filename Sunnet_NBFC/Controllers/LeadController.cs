using Sunnet_NBFC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sunnet_NBFC.App_Code;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Web;
using Org.BouncyCastle.Crypto.IO;
using System.Diagnostics.Metrics;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using System.Configuration;
using System.Diagnostics;

namespace Sunnet_NBFC.Controllers
{
    public class LeadController : Controller
    {
        [SessionAttribute]
        public ActionResult LeadView(clsLeadGenerationMaster clss)
        {
            if (Session["UserID"] != null)
            {
                if (String.IsNullOrEmpty(Session["UserID"].ToString()) == true)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();


                    List<clsLeadGenerationMaster> lst = new List<clsLeadGenerationMaster>();
                    try
                    {
                        ViewBag.MainProductList = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("View"), "MainProdId", "ProductName");

                        using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
                        {
                            cls.ReqType = "ViewLead";
                            cls.CompanyId = ClsSession.CompanyID;
                            cls.BranchID = ClsSession.BranchId;
                            cls.MainProductId = clss.MainProductId;
                            cls.ProductId = clss.ProductId;
                            cls.LeadNo = clss.LeadNo;
                            cls.CustomerName = clss.CustomerName;
                            cls.MobileNo1 = clss.MobileNo1;
                            cls.PanNo = clss.PanNo;
                            cls.AadharNo = clss.AadharNo;
                            cls.ReqType = "ViewLead";
                            cls.isdelete = 0;
                            cls.LeadNo = clss.LeadNo;
                            cls.LeadId = 0;
                            if (ClsSession.UserType.ToUpper() == "E")
                            {
                                cls.Empid = int.Parse(Session["EmpId"].ToString());
                            }
                            else
                            {
                                cls.Empid = 0;
                            }
                            if (Request.QueryString["ShortStage_Name"] != null)
                            {
                                cls.ShortStage_Name = Request.QueryString["ShortStage_Name"].ToString();
                                cls.StageEmpId = int.Parse(Session["EmpId"].ToString());
                                cls.Empid = 0;
                            }

                            using (DataTable dt = DataInterface.GetLeadGenerationData(cls))
                            {
                                if (dt != null)
                                {
                                    List<clsLeadGenerationMaster> list = new List<clsLeadGenerationMaster>();
                                    list = (from DataRow row in dt.Rows

                                            select new clsLeadGenerationMaster()
                                            {
                                                LeadId = int.Parse(row["LeadId"].ToString()),
                                                LeadNo = row["LeadNo"].ToString(),
                                                MainProductName = row["MainProductName"].ToString(),
                                                ProductName = row["ProductName"].ToString(),
                                                CustomerName = row["CustomerName"].ToString(),
                                                MobileNo1 = row["MobileNo1"].ToString(),
                                                PanNo = row["PanNo"].ToString(),
                                                AadharNo = row["AadharNo"].ToString(),
                                                ReuestedLoanAmount = row["ReuestedLoanAmount"].ToString(),
                                                ReuestedLoanTenure = row["ReuestedLoanTenure"].ToString(),
                                                ShortStage_Name = row["ShortStage_Name"].ToString(),
                                                StatusDesc = row["StatusDesc"].ToString(),
                                                IsLoanDisbursed = row["IsLoanDisbursed"].ToString(),
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
                            clsE.FunctionName = "LeadView";
                            clsE.Link = "Lead/LeadView";
                            clsE.PageName = "Lead View Controller";
                            clsE.UserId = Session["UserID"].ToString();
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

        public ActionResult GoToStage(string StageName = "")
        {
            if (StageName.ToUpper() == "DISBURSE")
                return RedirectToAction("LeadView", "LeadDisbursement");

            else if (StageName.ToUpper() == "FINALAPPROVE")
                return RedirectToAction("LeadView", "LeadFinalApprove");

            else if (StageName.ToUpper() == "DOCSIGN")
                return RedirectToAction("LeadView", "LeadDocumentSigning");

            else if (StageName.ToUpper() == "CREDITAPPROVE")
                return RedirectToAction("LeadCreditView", "LeadCredit");

            else if (StageName.ToUpper() == "DOCCOL")
                return RedirectToAction("LeadView", "LeadDocument");

            else if (StageName.ToUpper() == "PRIMYTEL")
                return RedirectToAction("LeadView", "LeadCalling");

            else
                return RedirectToAction("LeadView", "Lead");
        }
        [SessionAttribute]
        public ActionResult LeadDetails(string LeadNo, string LeadId)
        {
            clsLeadGenerationMaster model = new clsLeadGenerationMaster();

            try
            {
                List<Gurantor> gurantorslist = new List<Gurantor>();
                using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
                {
                    cls.ReqType = "ViewLead";
                    cls.CompanyId = ClsSession.CompanyID;
                    cls.LeadNo = LeadNo;
                    cls.LeadId = Convert.ToInt32(LeadId);
                    using (DataSet ds = DataInterface.GetLeadGenerationDataSingle(cls))
                    {

                        if (ds != null)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt != null)
                            {

                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {

                                    model.CIFID = ds.Tables[0].Rows[i]["CIF"].ToString();
                                    model.LeadId = int.Parse(ds.Tables[0].Rows[i]["LeadId"].ToString());
                                    model.LeadNo = Convert.ToString(ds.Tables[0].Rows[i]["LeadNo"]);
                                    model.CenterName = ds.Tables[0].Rows[i]["CenterName"].ToString();
                                    model.PLBranchName = ds.Tables[0].Rows[i]["PersonalLoanBranch"].ToString();
                                    model.ReuestedLoanAmount = Convert.ToString(ds.Tables[0].Rows[i]["ReqLoanAmt"]);
                                    model.ReuestedLoanTenure = Convert.ToString(ds.Tables[0].Rows[i]["ReqLoanTenure"]);
                                    model.MainProductId = Convert.ToString(ds.Tables[0].Rows[i]["MainProdId"]);
                                    model.ProductId = Convert.ToString(ds.Tables[0].Rows[i]["ProdId"]);
                                    model.MainProductName = Convert.ToString(ds.Tables[0].Rows[i]["MainProductName"]);
                                    model.ProductName = Convert.ToString(ds.Tables[0].Rows[i]["ProductName"]);
                                    model.RefernceName = Convert.ToString(ds.Tables[0].Rows[i]["RefernceName"]);
                                    model.RefenceRelation = Convert.ToString(ds.Tables[0].Rows[i]["RefenceRelation"]);
                                    model.RefenceMobileNo = Convert.ToString(ds.Tables[0].Rows[i]["RefenceMobileNo"]);

                                    model.RefernceName1 = Convert.ToString(ds.Tables[0].Rows[i]["RefernceName1"]);
                                    model.RefenceRelation1 = Convert.ToString(ds.Tables[0].Rows[i]["RefenceRelation1"]);
                                    model.RefenceMobileNo1 = Convert.ToString(ds.Tables[0].Rows[i]["RefenceMobileNo1"]);

                                    model.NoofDependent = Convert.ToString(ds.Tables[0].Rows[i]["NoofDependent"]);
                                    model.EstMonthIncome = Convert.ToString(ds.Tables[0].Rows[i]["EstMonthIncome"]);
                                    model.EstMonthExpense = Convert.ToString(ds.Tables[0].Rows[i]["EstMonthExpense"]);
                                    model.ColletralSecurityType = Convert.ToString(ds.Tables[0].Rows[i]["ColletralSecurityType"]);
                                    model.CurMonthObligation = Convert.ToString(ds.Tables[0].Rows[i]["CurMonthObligation"]);
                                    model.FORecomedAmt = Convert.ToString(ds.Tables[0].Rows[i]["FORecomedAmt"]);
                                    model.LoanPurpose = Convert.ToString(ds.Tables[0].Rows[i]["LoanPurpose"]);
                                    model.NoofDependent = Convert.ToString(ds.Tables[0].Rows[i]["NoofDependent"]);
                                    model.ColletralSecurityType = Convert.ToString(ds.Tables[0].Rows[i]["ColletralSecurityType"]);
                                    model.ViechleNo = Convert.ToString(ds.Tables[0].Rows[i]["ViechleNo"]);
                                    model.ViechleRegYear = Convert.ToString(ds.Tables[0].Rows[i]["ViechleRegYear"]);
                                    model.MFGYear = Convert.ToString(ds.Tables[0].Rows[i]["MFGYear"]);
                                    model.ViechleModel = Convert.ToString(ds.Tables[0].Rows[i]["ViechleModel"]);
                                    model.ViechleColor = Convert.ToString(ds.Tables[0].Rows[i]["ViechleColor"]);
                                    model.ViechleCompany = Convert.ToString(ds.Tables[0].Rows[i]["ViechleCompany"]);
                                    model.ViechleOwner = Convert.ToString(ds.Tables[0].Rows[i]["ViechleOwner"]);
                                    model.EstValueViechle = Convert.ToString(ds.Tables[0].Rows[i]["EstValueViechle"]);
                                    model.UserRemarks = Convert.ToString(ds.Tables[0].Rows[i]["UserRemarks"]);
                                    model.FuelType = Convert.ToString(ds.Tables[0].Rows[i]["FuelType"]);
                                    model.RegistrationDate = Convert.ToString(ds.Tables[0].Rows[i]["RegistrationDate"]);
                                    model.Insurer = Convert.ToString(ds.Tables[0].Rows[i]["Insurer"]);
                                    model.PolicyNo = Convert.ToString(ds.Tables[0].Rows[i]["PolicyNo"]);
                                    model.Owner = Convert.ToString(ds.Tables[0].Rows[i]["Owner"]);
                                    model.InsuranceStatus = Convert.ToString(ds.Tables[0].Rows[i]["InsuranceStatus"]);
                                    model.InsuranceType = Convert.ToString(ds.Tables[0].Rows[i]["InsuranceType"]);
                                    model.ValidityDate = Convert.ToString(ds.Tables[0].Rows[i]["ValidityDate"]);
                                    model.ExShowRoomPrice = Convert.ToString(ds.Tables[0].Rows[i]["ExShowRoomPrice"]);
                                    model.OnRoadPrice = Convert.ToString(ds.Tables[0].Rows[i]["OnRoadPrice"]);
                                    model.EstValueofscurity = Convert.ToString(ds.Tables[0].Rows[i]["EstValueofscurity"]);
                                    model.PropertyAddress = Convert.ToString(ds.Tables[0].Rows[i]["PropertyAddress"]);
                                    model.PropertyType = Convert.ToString(ds.Tables[0].Rows[i]["PropertyType"]);
                                    model.Propertyarea = Convert.ToString(ds.Tables[0].Rows[i]["Propertyarea"]);
                                    model.FORemarks = Convert.ToString(ds.Tables[0].Rows[i]["Remarks"]);
                                    model.DealerName= Convert.ToString(ds.Tables[0].Rows[i]["DealerName"]);
                                    model.ERikshawMaker= Convert.ToString(ds.Tables[0].Rows[i]["ERikshawMaker"]);
                                    model.EstFamilyIncome = Convert.ToString(ds.Tables[0].Rows[i]["EstFamilyIncome"]);
                                    model.PerformaInvoice = Convert.ToString(ds.Tables[0].Rows[i]["PerformaInvoice"]);
                                    model.RepaymentType = Convert.ToString(ds.Tables[0].Rows[i]["RepaymentType"]);
                                    model.InsEndValidityDate = Convert.ToString(ds.Tables[0].Rows[i]["InsEndValidityDate"]);
                                    //model.EstValueofscurity = Convert.ToString(ds.Tables[0].Rows[i]["EstValueofscurity"]);
                                    ViewBag.CUSTYPEREUIRED = Convert.ToString(ds.Tables[0].Rows[i]["CustTypeRequried"]);
                                    ViewBag.Status1 = Convert.ToString(ds.Tables[0].Rows[i]["Status1"]);
                                    ViewBag.Short_StageName = Convert.ToString(ds.Tables[0].Rows[i]["ShortStage_Name"]);
                                    ViewBag.MainProductName= Convert.ToString(ds.Tables[0].Rows[i]["MainProductName"]).ToUpper();
                                }

                            }
                            HttpPostedFileBase file = null;

                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {

                                if (Convert.ToString(ds.Tables[1].Rows[i]["CustType"]) == "C")
                                {
                                    model.Prefix = Convert.ToString(ds.Tables[1].Rows[i]["PrefixName"]);
                                    model.FirstName = Convert.ToString(ds.Tables[1].Rows[i]["FirstName"]);
                                    model.MiddleName = Convert.ToString(ds.Tables[1].Rows[i]["MiddleName"]);
                                    model.LastName = Convert.ToString(ds.Tables[1].Rows[i]["LastName"]);
                                    model.FatherName = Convert.ToString(ds.Tables[1].Rows[i]["FatherName"]);
                                    model.MotherName = Convert.ToString(ds.Tables[1].Rows[i]["MotherName"]);
                                    model.SpouseName = Convert.ToString(ds.Tables[1].Rows[i]["SpouseName"]);
                                    model.Gender = Convert.ToString(ds.Tables[1].Rows[i]["Gender"]);
                                    model.DateofBirth = Convert.ToString(ds.Tables[1].Rows[i]["DateofBirth"]);
                                    model.MartialStatus = Convert.ToString(ds.Tables[1].Rows[i]["MartialStatus"]);
                                    model.PresentAddress = Convert.ToString(ds.Tables[1].Rows[i]["PresentAddress"]);
                                    model.PresentPincode = Convert.ToString(ds.Tables[1].Rows[i]["PresentPincode"]);
                                    model.PresentStateId = Convert.ToString(ds.Tables[1].Rows[i]["PresentStateId"]);
                                    model.PresentCityId = Convert.ToString(ds.Tables[1].Rows[i]["PresentCityId"]);

                                    model.PermanentAddress = Convert.ToString(ds.Tables[1].Rows[i]["PermanentAddress"]);
                                    model.PermanentPincode = Convert.ToString(ds.Tables[1].Rows[i]["PermanentPincode"]);
                                    model.PermanentStateId = Convert.ToString(ds.Tables[1].Rows[i]["PermanentStateId"]);
                                    model.PermanentCityId = Convert.ToString(ds.Tables[1].Rows[i]["PermanentCityId"]);

                                    model.PresentStateName = Convert.ToString(ds.Tables[1].Rows[i]["PermanentStateId"]);
                                    model.PresentCityName = Convert.ToString(ds.Tables[1].Rows[i]["PermanentStateId"]);

                                    model.PremenentStateName = Convert.ToString(ds.Tables[1].Rows[i]["PermanentStateId"]);
                                    model.PremenentCityName = Convert.ToString(ds.Tables[1].Rows[i]["PermanentCityId"]);

                                    model.PresentVillage = Convert.ToString(ds.Tables[1].Rows[i]["PresentVillage"]);
                                    model.PresentDistrict = Convert.ToString(ds.Tables[1].Rows[i]["PresentDistrict"]);
                                    model.PermanentVillage = Convert.ToString(ds.Tables[1].Rows[i]["PermanentVillage"]);
                                    model.PermanentDistrict = Convert.ToString(ds.Tables[1].Rows[i]["PermanentDistrict"]);


                                    model.CibilScore = Convert.ToString(ds.Tables[1].Rows[i]["CibilScore"]);
                                    model.MobileNo1 = Convert.ToString(ds.Tables[1].Rows[i]["MobileNo1"]);
                                    model.FatherMobileNo = Convert.ToString(ds.Tables[1].Rows[i]["FatherMobileNo"]);
                                    model.MotherMobileNo = Convert.ToString(ds.Tables[1].Rows[i]["MotherMobileNo"]);
                                    model.SpouseMobileNo = Convert.ToString(ds.Tables[1].Rows[i]["SpouseMobileNo"]);
                                    model.AadharNo = Convert.ToString(ds.Tables[1].Rows[i]["AadharNo"]);
                                    model.PanNo = Convert.ToString(ds.Tables[1].Rows[i]["PanNo"]);
                                    model.EmailId = Convert.ToString(ds.Tables[1].Rows[i]["EmailId"]);
                                    model.OwnerShip = Convert.ToString(ds.Tables[1].Rows[i]["OwnerShip"]);
                                    model.CIFID = Convert.ToString(ds.Tables[1].Rows[i]["CIF"]);
                                    model.CustLandMark = Convert.ToString(ds.Tables[1].Rows[i]["LandMark"]);
                                    if (System.IO.File.Exists(Server.MapPath("~/Img/ApplicantImgs/" + Convert.ToString(ds.Tables[1].Rows[i]["CustImage"]))))
                                    {
                                        model.CustImage = Server.MapPath("~/Img/ApplicantImgs/") + Convert.ToString(ds.Tables[1].Rows[i]["CustImage"]);
                                    }
                                    if (System.IO.File.Exists(Server.MapPath("~/Img/ApplicantImgs/" + Convert.ToString(ds.Tables[1].Rows[i]["ElectricBill"]))))
                                    {
                                        model.ElectricBill = Server.MapPath("~/Img/ApplicantImgs/") + Convert.ToString(ds.Tables[1].Rows[i]["ElectricBill"]);
                                    }

                                }

                                else if (Convert.ToString(ds.Tables[1].Rows[i]["CustType"]) == "Gurantor")
                                {

                                    Gurantor gurantor = new Gurantor();
                                    gurantor.G_CIF = Convert.ToString(ds.Tables[1].Rows[i]["CIF"]);
                                    gurantor.G_Prefix = Convert.ToString(ds.Tables[1].Rows[i]["PrefixName"]);
                                    gurantor.G_FirstName = Convert.ToString(ds.Tables[1].Rows[i]["FirstName"]);
                                    gurantor.G_MiddleName = Convert.ToString(ds.Tables[1].Rows[i]["MiddleName"]);
                                    gurantor.G_LastName = Convert.ToString(ds.Tables[1].Rows[i]["LastName"]);
                                    gurantor.G_Gender = Convert.ToString(ds.Tables[1].Rows[i]["Gender"]);
                                    gurantor.G_DOB = Convert.ToString(ds.Tables[1].Rows[i]["DateofBirth"]);
                                    gurantor.G_Marital_Status = Convert.ToString(ds.Tables[1].Rows[i]["MartialStatus"]);
                                    gurantor.G_PresentAddress = Convert.ToString(ds.Tables[1].Rows[i]["PresentAddress"]);
                                    gurantor.G_PresentPinCode = Convert.ToString(ds.Tables[1].Rows[i]["PresentPincode"]);
                                    gurantor.G_PresentStateId = ds.Tables[1].Rows[i]["PresentStateId"].ToString();
                                    gurantor.G_PresentCityId = ds.Tables[1].Rows[i]["PresentCityId"].ToString();

                                    gurantor.G_PermanentAddress = Convert.ToString(ds.Tables[1].Rows[i]["PermanentAddress"]);
                                    gurantor.G_PermanentPincode = Convert.ToString(ds.Tables[1].Rows[i]["PermanentPincode"]);
                                    gurantor.G_PermanentStateId = ds.Tables[1].Rows[i]["PermanentStateId"].ToString();
                                    gurantor.G_PermanentCityId = ds.Tables[1].Rows[i]["PermanentCityId"].ToString();

                                    gurantor.G_PresentStateName = Convert.ToString(ds.Tables[1].Rows[i]["PresentStateId"]);
                                    gurantor.G_PresentCityName = Convert.ToString(ds.Tables[1].Rows[i]["PresentCityId"]);

                                    gurantor.G_PermanentStateName = Convert.ToString(ds.Tables[1].Rows[i]["PermanentStateId"]);
                                    gurantor.G_PermanentCityName = Convert.ToString(ds.Tables[1].Rows[i]["PermanentCityId"]);

                                    gurantor.G_PresentVillage = Convert.ToString(ds.Tables[1].Rows[i]["PresentVillage"]);
                                    gurantor.G_PresentDistrict = Convert.ToString(ds.Tables[1].Rows[i]["PresentDistrict"]);
                                    gurantor.G_PermanentVillage = Convert.ToString(ds.Tables[1].Rows[i]["PermanentVillage"]);
                                    gurantor.G_PermanentDistrict = Convert.ToString(ds.Tables[1].Rows[i]["PermanentDistrict"]);
                                    gurantor.G_FatherName = Convert.ToString(ds.Tables[1].Rows[i]["FatherName"]);
                                    gurantor.G_SpouseName = Convert.ToString(ds.Tables[1].Rows[i]["SpouseName"]);
                                    gurantor.G_Mobile_No = Convert.ToString(ds.Tables[1].Rows[i]["MobileNo1"]);
                                    gurantor.G_AadharNo = Convert.ToString(ds.Tables[1].Rows[i]["AadharNo"]);
                                    gurantor.G_PanNo = Convert.ToString(ds.Tables[1].Rows[i]["PanNo"]);
                                    gurantor.G_CibilScore = Convert.ToString(ds.Tables[1].Rows[i]["CibilScore"]);
                                    gurantor.G_EmailId = Convert.ToString(ds.Tables[1].Rows[i]["EmailId"]);
                                    gurantor.G_OwnerShip = Convert.ToString(ds.Tables[1].Rows[i]["OwnerShip"]);
                                    gurantor.G_LandMark = Convert.ToString(ds.Tables[1].Rows[i]["LandMark"]);
                                    gurantorslist.Add(gurantor);

                                }
                                else if (Convert.ToString(ds.Tables[1].Rows[i]["CustType"]) == "CO_Applicant")
                                {

                                    model.Co_CIF = Convert.ToString(ds.Tables[1].Rows[i]["CIF"]);
                                    model.CO_Prefix = Convert.ToString(ds.Tables[1].Rows[i]["PrefixName"]);
                                    model.CO_FirstName = Convert.ToString(ds.Tables[1].Rows[i]["FirstName"]);
                                    model.CO_MiddleName = Convert.ToString(ds.Tables[1].Rows[i]["MiddleName"]);
                                    model.CO_LastName = Convert.ToString(ds.Tables[1].Rows[i]["LastName"]);
                                    model.CO_Gender = Convert.ToString(ds.Tables[1].Rows[i]["Gender"]);
                                    model.CO_DOB = Convert.ToString(ds.Tables[1].Rows[i]["DateofBirth"]);
                                    model.CO_Marital_Status = Convert.ToString(ds.Tables[1].Rows[i]["MartialStatus"]);
                                    model.CO_PresentAddress = Convert.ToString(ds.Tables[1].Rows[i]["PresentAddress"]);
                                    model.CO_PresentPinCode = Convert.ToString(ds.Tables[1].Rows[i]["PresentPincode"]);
                                    model.CO_PresentStateId = Convert.ToString(ds.Tables[1].Rows[i]["PresentStateId"]);
                                    model.CO_PresentCityId = Convert.ToString(ds.Tables[1].Rows[i]["PresentCityId"]);

                                    model.CO_PermanentAddress = Convert.ToString(ds.Tables[1].Rows[i]["PermanentAddress"]);
                                    model.CO_PermanentPincode = Convert.ToString(ds.Tables[1].Rows[i]["PermanentPincode"]);
                                    model.CO_PermanentStateId = Convert.ToString(ds.Tables[1].Rows[i]["PermanentStateId"]);
                                    model.CO_PermanentCityId = Convert.ToString(ds.Tables[1].Rows[i]["PermanentCityId"]);

                                    model.CO_PresentStateName = Convert.ToString(ds.Tables[1].Rows[i]["PresentStateId"]);
                                    model.CO_PresentCityName = Convert.ToString(ds.Tables[1].Rows[i]["PresentCityId"]);

                                    model.CO_PermanentStateName = Convert.ToString(ds.Tables[1].Rows[i]["PermanentStateId"]);
                                    model.CO_PermanentCityName = Convert.ToString(ds.Tables[1].Rows[i]["PermanentCityId"]);

                                    model.CO_PresentVillage = Convert.ToString(ds.Tables[1].Rows[i]["PresentVillage"]);
                                    model.CO_PresentDistrict = Convert.ToString(ds.Tables[1].Rows[i]["PresentDistrict"]);
                                    model.CO_PermanentVillage = Convert.ToString(ds.Tables[1].Rows[i]["PermanentVillage"]);
                                    model.CO_PermanentDistrict = Convert.ToString(ds.Tables[1].Rows[i]["PermanentDistrict"]);


                                    model.CO_Mobile_No = Convert.ToString(ds.Tables[1].Rows[i]["MobileNo1"]);
                                    model.CO_Adhaar = Convert.ToString(ds.Tables[1].Rows[i]["AadharNo"]);
                                    model.CO_PAN = Convert.ToString(ds.Tables[1].Rows[i]["PanNo"]);
                                    model.CO_CIBIL = Convert.ToString(ds.Tables[1].Rows[i]["CibilScore"]);
                                    model.CO_Email_Id = Convert.ToString(ds.Tables[1].Rows[i]["EmailId"]);
                                    model.Co_OwnerShip = Convert.ToString(ds.Tables[1].Rows[i]["OwnerShip"]);
                                    model.CO_FatherName = Convert.ToString(ds.Tables[1].Rows[i]["FatherName"]);
                                    model.Co_LandMark = Convert.ToString(ds.Tables[1].Rows[i]["LandMark"]);
                                    
                                    if (System.IO.File.Exists(Server.MapPath("~/Img/COApplicantImgs/" + Convert.ToString(ds.Tables[1].Rows[i]["CustImage"]))))
                                    {
                                        model.CO_image = Server.MapPath("~/Img/COApplicantImgs/") + Convert.ToString(ds.Tables[1].Rows[i]["CustImage"]);
                                    }
                                    if (System.IO.File.Exists(Server.MapPath("~/Img/COApplicantImgs/" + Convert.ToString(ds.Tables[1].Rows[i]["ElectricBill"]))))
                                    {
                                        model.CO_ElectricBill = Server.MapPath("~/Img/COApplicantImgs/") + Convert.ToString(ds.Tables[1].Rows[i]["ElectricBill"]);
                                    }

                                }
                            }


                            ViewBag.gurantorslist = gurantorslist;
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
                    clsE.Link = "Lead/LeadDetails";
                    clsE.PageName = "Lead Controller";
                    clsE.UserId = Session["UserID"].ToString();
                    DataInterface.PostError(clsE);
                }
            }

            return PartialView("_LeadDetailsView", model);
        }

        [HttpPost]
        public JsonResult UpdateLeadGeneration()
        {
            string JSONresult = "";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            clsLeadMain master = jss.Deserialize<clsLeadMain>(Request.Form["AllDataArray"]);


            DataTable dt = DataInterface2.UpdateLeadStatus(master);
            JSONresult = JsonConvert.SerializeObject(dt);
            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }
        [SessionAttribute]
        public ActionResult ExportToExcel(clsLeadGenerationMaster clss)

        {

            var gv = new GridView();

            using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
            {
                cls.ReqType = "ViewLead";
                cls.CompanyId = ClsSession.CompanyID;
                cls.BranchID = ClsSession.BranchId;
                cls.MainProductId = clss.MainProductId;
                cls.ProductId = clss.ProductId;
                cls.LeadNo = clss.LeadNo;
                cls.CustomerName = clss.CustomerName;
                cls.MobileNo1 = clss.MobileNo1;
                cls.PanNo = clss.PanNo;
                cls.AadharNo = clss.AadharNo;
                cls.ReqType = "ViewLead";

                cls.LeadNo = "";
                cls.LeadId = 0;
                cls.Empid = Session["UserType"].ToString().ToUpper() != "A" ? int.Parse(Session["EmpId"].ToString()) : 0;
                if (Request.QueryString["ShortStage_Name"] != null)
                {
                    cls.ShortStage_Name = Request.QueryString["ShortStage_Name"].ToString();
                    cls.StageEmpId = int.Parse(Session["EmpId"].ToString());
                    cls.Empid = 0;
                }
                using (DataTable dt = DataInterface.GetLeadGenerationData(cls))
                {
                    if (dt != null)
                    {
                        gv.DataSource = dt;

                        gv.DataBind();
                        Response.ClearContent();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment; filename=ReportView.xls");
                        Response.ContentType = "application/ms-excel";
                        Response.Charset = "";

                        StringWriter objStringWriter = new StringWriter();

                        HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

                        gv.RenderControl(objHtmlTextWriter);

                        Response.Output.Write(objStringWriter.ToString());

                        Response.Flush();

                        Response.End();

                    }


                    return RedirectToAction("LeadView");

                }

            }
        }

        public JsonResult LeadDelete()
        {
            string JSONresult = "";
            try
            {
              
                JavaScriptSerializer jss = new JavaScriptSerializer();
                ViewBag.CompanyId = ClsSession.CompanyID;
                clsLeadGenerationMaster cls = jss.Deserialize<clsLeadGenerationMaster>(Request.Form["AllDataArray"]);

                cls.LeadId = cls.LeadId;
                cls.ReqType = "Delete";
                using (DataTable dt = DataInterface.GetLeadGeneration(cls))
                {
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            JSONresult = JsonConvert.SerializeObject(dt);
                            return Json(JSONresult, JsonRequestBehavior.AllowGet);
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
                    clsE.FunctionName = "LeadDelete";
                    clsE.Link = "Lead/LeadDelete";
                    clsE.PageName = "Status Controller";
                    clsE.UserId = ClsSession.EmpId.ToString();
                    DataInterface.PostError(clsE);
                }
            }
            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }
    }
}