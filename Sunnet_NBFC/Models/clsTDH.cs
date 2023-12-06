using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sunnet_NBFC.Models
{

    public class clsLead
    {
        public int OptType { get; set; }
        public int LeadId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string CustName { get; set; } = "";

        [Required(ErrorMessage = "Contact No is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Contact Number.")]
        public string CustContNo { get; set; } = "";
        public string CustContNo2 { get; set; } = "";
        public string CustMail { get; set; } = "";
        public string CustAdress { get; set; } = "";
        public string Country { get; set; } = "";
        public string Pincode { get; set; }
        public Gender CustGender { get; set; }
        [Required(ErrorMessage = "Gender is required.")]
        public int ServiceId { get; set; } = 0;
        public string Website { get; set; } = "";
        public string IndustryType { get; set; } = "";
        public string Remarks { get; set; } = "";
        public string CreatedBy { get; set; } = "";
        public string LeadStatus { get; set; } = "";
        public string ServiceName { get; set; } = "";
        public string GenderName { get; set; } = "";


    }
    public class ViewCatgModel
    {
        public clsLead ClsProp { get; set; }
        public List<clsLead> ViewLead { get; set; }
    }
    public enum Gender
    {
        Select,
        Male,
        Female
    }



    public class clsUser
    {
        public int OptType { get; set; }
        public int UserId { get; set; }
        public int LoginId { get; set; }

        [Required(ErrorMessage = "User Name is Requried")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "User Name is Requried")]
        public string Password { get; set; }
        public Int64 ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string Userpic { get; set; }
        public string UserPicExt { get; set; }
        public string UserPicName { get; set; }
        public int IsActive { get; set; }
        public string SessionKey { get; set; }
    }
    public class ImageModel
    {
        List<string> _images = new List<string>();

        public ImageModel()
        {
            _images = new List<string>();
        }

        public List<string> Images
        {
            get { return _images; }
            set { _images = value; }
        }
    }

    public class clsTicket
    {
        public int OptType { get; set; }
        public int TicketId { get; set; }
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "Description is Requried")]
        public string Description { get; set; }
        public string SoftwareName { get; set; }
        [Required(ErrorMessage = "Application Name is Requried")]
        public int Prodid { get; set; }
        public string TicketType { get; set; }
        public int TicketTypeId { get; set; }
        public string ClientName { get; set; } = "";
        [Required(ErrorMessage = "Email is Requried")]
        public string ClientEmail { get; set; } = "";
        public Int64 ClientContactNo { get; set; } = 0;
        public string TicketStatus { get; set; } = "";
        public string TicketResolution { get; set; } = "";
        public string TicketDoc { get; set; } = "";
        public string ClientRemarks { get; set; } = "";
        public string FromDate { get; set; } = "";
        public string ToDate { get; set; } = "";
        public string OutputResult { get; set; } = "";

    }



    public class clsClient
    {
        public int OptType { get; set; }
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Client Name is Requried")]
        public string ClientName { get; set; }
        [Required(ErrorMessage = "PhoneNo is Requried")]
        public string PhoneNo { get; set; }
        public string WhatsUpNo { get; set; }
        [Required(ErrorMessage = "Address is Requried")]
        public string Address { get; set; }
        public string City { get; set; }
        [Required(ErrorMessage = "City is Requried")]
        public string State { get; set; }
        public string PinCode { get; set; }
        public string GSTNo { get; set; }
        public string Email { get; set; }
    }

    public class clsStatusMaster : IDisposable
    {
        public string ReqType { get; set; }
        public int StatusID { get; set; }
        public int CompanyID { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Status Description is required.")]
        public string StatusDesc { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~clsStatusMaster()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose any disposable fields here-
                GC.SuppressFinalize(this);
            }
            ReleaseNativeResource();
        }
        private void ReleaseNativeResource()
        {

        }


    }

    public class clsCustomerMaster : IDisposable
    {
        public string ReqType { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SpouseName { get; set; }
        public string Gender { get; set; }
        public string Dob { get; set; }
        public string MaterialStatus { get; set; }
        public string PresentAddress { get; set; }
        public string PresentPinCode { get; set; }
        public string PermanentAddress { get; set; }
        public string PermanentPincode { get; set; }
        public string CibilScore { get; set; }
        public int PresentStateId { get; set; }
        public int PresentCityId { get; set; }
        public int PermanentStateId { get; set; }
        public int PermanentCityId { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string FatherMobileNo { get; set; }
        public string MotherMobileNo { get; set; }
        public string SpouseMobileNo { get; set; }
        public string AadharNo { get; set; }
        public string PanNo { get; set; }
        public int OccupationID { get; set; }
        public int QualificationId { get; set; }
        public string CustRelation { get; set; }
        public string DateofBirth { get; set; }
        public int CompanyId { get; set; }
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~clsCustomerMaster()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose any disposable fields here-
                GC.SuppressFinalize(this);
            }
            ReleaseNativeResource();
        }
        private void ReleaseNativeResource()
        {

        }


    }
    public class clsCompanyMaster : IDisposable
    {

        public string ReqType { get; set; }
        public int CompanyId { get; set; }
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string PANNo { get; set; }
        public string GSTNo { get; set; }
        public string CompanyType { get; set; }
        public string CompanyDesc { get; set; }
        public string CompanyOthDesc { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string LOGO { get; set; }
        public string CINNo { get; set; }
        public string DateofIncorporation { get; set; }

        public string tmpDateofIncorporation { get; set; }
        public string RBIRegd { get; set; }
        public string EmailId { get; set; }
        public string Website { get; set; }
        public string MobileNo { get; set; }
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        ~clsCompanyMaster()
        {
            Dispose(false);
        }

    }

    public class clsError : IDisposable
    {
        public string ReqType { get; set; }
        public int Id { get; set; }
        public string Mode { get; set; }
        public string PageName { get; set; }
        public string Link { get; set; }
        public string FunctionName { get; set; }
        public string ErrorDescrption { get; set; }
        public string UserId { get; set; }
        public DateTime ErrorDate { get; set; }
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        ~clsError()
        {
            Dispose(false);
        }
    }

    public class clsDSAMaster : IDisposable
    {
        public string ReqType { get; set; }
        public int DSAId { get; set; }
        public string DSACode { get; set; }
        [Required(ErrorMessage = "DSA Name is required.")]
        public string DSAName { get; set; }
        public string DSAAddress { get; set; }
        public int DSACityId { get; set; }
        public int DSAStateId { get; set; }
        [Required(ErrorMessage = "DSA Contact No is required.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Invalid Contact Number.")]
        public string DSAContactNo { get; set; }
        [Required(ErrorMessage = "DSA PinCode is required.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Invalid Pin Code.")]
        public Int64 DSAPincode { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Invalid What's Number.")]
        public string DSAWhatsUpNo { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Please enter correct email")]
        public string DSAEmail { get; set; }
        [MaxLength(15)]

        [RegularExpression("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$", ErrorMessage = "Invalid GST Number")]
        public string DSAGSTNo { get; set; }
        public decimal DSACommision { get; set; }
        public string DSARemarks { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public string DSAccountNo { get; set; }
        public string DSABankName { get; set; }
        public string DSABranch { get; set; }
        public string DSAIFSCCode { get; set; }
        public int ISDELETE { get; set; }
        public int COMPANYID { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        [RegularExpression("^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Invalid PAN Number")]
        public string PAN { get; set; } = "";
        [RegularExpression("^[2-9]{1}[0-9]{3}[0-9]{4}[0-9]{4}$", ErrorMessage = "Invalid Aadhar Number")]
        public string AAdharNo { get; set; } = "";
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        ~clsDSAMaster()
        {
            Dispose(false);
        }
    }


    public class clsQuestion : IDisposable
    {
        public string ReqType { get; set; }
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "Main Product is required.")]
        public int MainProdId { get; set; }
        [Required(ErrorMessage = "Product is required.")]
        public int ProdId { get; set; }
        [Required(ErrorMessage = "Question Answer Type is required.")]
        public string QuestionAnsType { get; set; }

        [Required(ErrorMessage = "Question is required.")]
        public string Question { get; set; }
        public string MainProduct { get; set; }
        public string Product { get; set; }
        public int CreatedBy { get; set; }
        public int IsDelete { get; set; }
        public int CompanyId { get; set; }

        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        ~clsQuestion()
        {
            Dispose(false);
        }
    }

    public class clsLogin : IDisposable
    {
        public string ReqType { get; set; }

        public int UserID { get; set; }
        [Remote("IsAlreadyExistsUser", "Login", HttpMethod = "POST", ErrorMessage = "User ID Not Exists.")]
        public string UserName { get; set; }
        public string tmpUserName { get; set; }
        public string UserPassword { get; set; }
        public string Type { get; set; }
        public int RefID { get; set; }
        public bool IsActive { get; set; }
        public string SessionID { get; set; }
        public string DeviceID { get; set; }
        public string DeviceType { get; set; }
        public bool IsLogged { get; set; }
        public int Compid { get; set; }
        public string DeviceToken { get; set; }
        public int ChangePasswordYN { get; set; }
        public string ConfirmPassword { get; set; }
        public string Message { get; set; }
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~clsLogin()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose any disposable fields here-
                GC.SuppressFinalize(this);
            }
            ReleaseNativeResource();
        }
        private void ReleaseNativeResource()
        {

        }

    }

    public class clsLeadGenerationMaster : IDisposable
    {
        public string ReqType { get; set; }
        public string MainProductId { get; set; }
        public string MainProductName { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Empid { get; set; }
        public string Prefix { get; set; }
        public string CustomerName { get; set; }
        public string CIFID { get; set; }
        public string EmailId { get; set; }
        public string Status { get; set; }
        public int LeadId { get; set; }
        public string LeadNo { get; set; }
        public string LeadNo1 { get; set; }
        public string PresentStateName { get; set; }
        public string PresentCityName { get; set; }
        public string PremenentStateName { get; set; }
        public string PremenentCityName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CustType { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SpouseName { get; set; }
        public string Gender { get; set; }
        public string DateofBirth { get; set; }
        public string MartialStatus { get; set; }
        public string PresentAddress { get; set; }
        public string PresentPincode { get; set; }
        public string PresentVillage { get; set; }
        public string PresentDistrict { get; set; }
        public string Hdn_type { get; set; }
        public string PermanentAddress { get; set; }
        public string PermanentPincode { get; set; }
        public string CibilScore { get; set; }
        public string PresentStateId { get; set; }
        public string PresentCityId { get; set; }
        public string PermanentStateId { get; set; }
        public string PermanentCityId { get; set; }
        public string PermanentVillage { get; set; }
        public string PermanentDistrict { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string FatherMobileNo { get; set; }
        public string MotherMobileNo { get; set; }
        public string SpouseMobileNo { get; set; }
        public string IsLoanDisbursed { get; set; }
        public string AadharNo { get; set; }
        public string AAdharverfiy { get; set; }
        public string PanNo { get; set; }
        public string PanVerify { get; set; }
        public string OccupationID { get; set; }
        public string QualificationId { get; set; }
        public string CustRelation { get; set; }
        public string EstValueofscurity { get; set; } = "0";
        public string CreateDate { get; set; }
        public int CompanyId { get; set; }
        public string CustImage { get; set; } = "";
        public string ElectricBill { get; set; } = "";

        public string Co_CIF { get; set; }
        public string CO_Prefix { get; set; }
        public string CO_FirstName { get; set; }
        public string CO_MiddleName { get; set; }
        public string CO_LastName { get; set; }
        public string CO_Adress { get; set; }
        public string CO_DOB { get; set; }
        public string CO_Occupation { get; set; }
        public string CO_Marital_Status { get; set; }
        public string CO_Gender { get; set; }
        public string CO_PAN { get; set; }
        public string CO_Adhaar { get; set; }
        public string CO_AAdharverfiy { get; set; }
        public string CO_Panverfiy { get; set; }
        public string G_AAdharverfiy { get; set; }
        public string G_Panverfiy { get; set; }
        public string G_FilePath { get; set; }
        public string CO_PresentAddress { get; set; }
        public string CO_PresentPinCode { get; set; }
        public string CO_PresentStateId { get; set; }
        public string CO_PresentStateName { get; set; }
        public string CO_PresentCityId { get; set; }
        public string CO_PresentCityName { get; set; }
        public string CO_PresentVillage { get; set; }
        public string CO_PresentDistrict { get; set; }

        public string CO_PermanentAddress { get; set; }
        public string CO_PermanentPincode { get; set; }
        public string CO_PermanentStateId { get; set; }
        public string CO_PermanentStateName { get; set; }
        public string CO_PermanentCityId { get; set; }
        public string CO_PermanentCityName { get; set; }
        public string CO_PermanentVillage { get; set; }
        public string CO_PermanentDistrict { get; set; }
        public string AddharClientId { get; set; }
        public string AddharClientScreate { get; set; }
        public string AddharISAPICHeck { get; set; }
        public string PanClientId { get; set; }
        public string PanClientScreate { get; set; }
        public string PanISAPICHeck { get; set; }
        public string DealerName { get; set; }


        public string CO_Mobile_No { get; set; }
        public string CO_Email_Id { get; set; }

        public string CO_CIBIL { get; set; }
        public string CO_image { get; set; } = "";

        public string G_CIF { get; set; } = "";
        public string CO_ElectricBill { get; set; } = "";
        public string G_Prefix { get; set; }
        public string G_FirstName { get; set; }
        public string G_MiddleName { get; set; }
        public string G_LastName { get; set; }
        public string G_Adress { get; set; }
        public string G_DOB { get; set; }
        public string G_Occupation { get; set; }
        public string G_Marital_Status { get; set; }
        public string G_Gender { get; set; }

        public string G_PresentAddress { get; set; }
        public string G_PresentPinCode { get; set; }
        public int G_PresentStateId { get; set; }
        public int G_PresentCityId { get; set; }
        public int G_PresentVillage { get; set; }
        public int G_PresentDistrict { get; set; }
        public string G_PermanentAddress { get; set; }
        public string G_PermanentPincode { get; set; }
        public string G_PermanentStateId { get; set; }
        public string G_PermanentCityId { get; set; }
        public string G_PermanentVillage { get; set; }
        public string G_PermanentDistrict { get; set; }

        public string G_Mobile_No { get; set; }
        public string G_Email_Id { get; set; }
        public string G_PAN { get; set; }
        public string G_Adhaar { get; set; }
        public string G_CIBIL { get; set; }

        public string ReuestedLoanAmount { get; set; }
        public string EstValueViechle { get; set; }
        public string ReuestedLoanTenure { get; set; }
        public string EstMonthIncome { get; set; }
        public string EstFamilyIncome { get; set; }
        public string EstMonthExpense { get; set; }
        public string CurMonthObligation { get; set; }
        public string FORecomedAmt { get; set; }

        public string NoofDependent { get; set; }
        public string ViechleNo { get; set; }
        public string ViechleRegYear { get; set; }
        public string MFGYear { get; set; }

        public string ViechleModel { get; set; }
        public string ViechleColor { get; set; }
        public string ViechleCompany { get; set; }

        public string ViechleOwner { get; set; }
        public string RefernceName { get; set; }
        public string RefenceMobileNo { get; set; }
        public string RefenceRelation { get; set; }
        public string RefernceName1 { get; set; }
        public string RefenceMobileNo1 { get; set; }
        public string RefenceRelation1 { get; set; }
        public string LoanPurpose { get; set; }
        public string ColletralSecurityType { get; set; }

        public string GeoTagging { get; set; }
        public string FORemarks { get; set; }
        public int BranchID { get; set; }
        public string ShortStage_Name { get; set; }
        public int StageEmpId { get; set; }
        public string StatusDesc { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string DSAId { get; set; }
        public int CenterId { get; set; } = 0;
        public int PLLoanBranch { get; set; } = 0;/// for Personal Loan Branch
        public string CenterName { get; set; } = "";
        public string PLBranchName { get; set; } = "";
        public string UserRemarks { get; set; } = "";
        public string FuelType { get; set; } = "";
        public string RegistrationDate { get; set; } = "";
        public string Insurer { get; set; } = "";
        public string PolicyNo { get; set; } = "";
        public string Owner { get; set; } = "";
        public string InsuranceStatus { get; set; } = "";
        public string InsuranceType { get; set; } = "";
        public string ValidityDate { get; set; } = "";
        public string ExShowRoomPrice { get; set; } = "";
        public string OnRoadPrice { get; set; } = "";
        public string Propertyarea { get; set; } = "";
        public string PropertyAddress { get; set; } = "";
        public string PropertyType { get; set; } = "";
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }


    }


    public class Gurantor
    {
        public string G_CIF { get; set; }
        public string G_Prefix { get; set; }
        public string G_FirstName { get; set; }
        public string G_MiddleName { get; set; }
        public string G_LastName { get; set; }
        public string G_Adress { get; set; }
        public string G_DOB { get; set; }
        public string G_Occupation { get; set; }
        public string G_Marital_Status { get; set; }
        public string G_Gender { get; set; }

        public string G_PresentAddress { get; set; }
        public string G_PresentPinCode { get; set; }
        public string G_PresentStateId { get; set; }
        public string G_PresentCityId { get; set; }

        public string G_PermanentStateId { get; set; }
        public string G_PermanentCityId { get; set; }

        public string G_PresentStateName { get; set; }
        public string G_PresentCityName { get; set; }

        public string G_PermanentStateName { get; set; }
        public string G_PermanentCityName { get; set; }

        public string G_PermanentAddress { get; set; }
        public string G_PermanentPincode { get; set; }
        public string G_PresentVillage { get; set; }
        public string G_PresentDistrict { get; set; }
        public string G_PermanentVillage { get; set; }
        public string G_PermanentDistrict { get; set; }
        public string G_P_State { get; set; }
        public string G_P_City { get; set; }
        public string G_Mobile_No { get; set; }
        public string G_EmailId { get; set; }
        public string G_PanNo { get; set; }
        public string G_AadharNo { get; set; }
        public string G_CibilScore { get; set; }
        public string G_FilePath { get; set; }

        public int G_CompanyId { get; set; }
        public int G_BranchID { get; set; }
        public string G_LeadNo { get; set; }
        public int G_LeadId { get; set; }
        public int G_PanVerify { get; set; }
        public int G_AadharVerify { get; set; }
        public int G_CreatedBy { get; set; }

    }
    public class clsRoleMaster : clsRoleSubMenu
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int EmpId { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public int CreatedBy { get; set; }
        public int CompanyId { get; set; }
        public int IsDelete { get; set; }
        public List<clsRoleMaster> clsSubMenulst { get; set; }

        // Public implementation of Dispose pattern callable by consumers.

    }

    public class UpdateMenuMaster
    {

        public string SNo { get; set; }
        public string MenuName { get; set; }
        public string SubMenuName { get; set; }
        public string MenuId { get; set; }
        public string SubMenuId { get; set; }


    }
    public class RoleSideMenuBar
    {
        public int MenuDisplaySeqNo { get; set; }
        public int ParentMenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public int SubMenuDisplaySeqNo { get; set; }
        public int ChildMenuID { get; set; }
        public string Title { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

    }
    public class RoleMenuMaster
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
    }
    public class clsRoleSubMenu : IDisposable
    {
        public string ReqType { get; set; }
        public int SubMenuId { get; set; }
        public int MenuId { get; set; }
        public string Title { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int IsActive { get; set; }
        public string MenuName { get; set; }
        public string ActiveStr { get; set; }
        public bool IsSelected { get; set; }

        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        ~clsRoleSubMenu()
        {
            Dispose(false);
        }
    }

}

public class clsDashboard : IDisposable
{
    public string cnt { get; set; }
    public string ShortStage_Name { get; set; }
    public string Stage_Name { get; set; }
    bool disposed = false;

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
            return;

        if (disposing)
        {
            // Free any other managed objects here.
            //
        }

        // Free any unmanaged objects here.
        //
        disposed = true;
    }

    ~clsDashboard()
    {
        Dispose(false);
    }
}


