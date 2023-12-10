using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunnet_NBFC.App_Code;


namespace Sunnet_NBFC.Models
{
    public class clsTDH2
    {
    }
    public class clsBranch : IDisposable
    {
        public string ReqType { get; set; }

        public int BranchId { get; set; }

        [Required(ErrorMessage = "Branch Code is required.")]
        [DisplayName("Branch Code")]

        public string BranchCode { get; set; }

        [Required(ErrorMessage = "Branch Name is required.")]
        [DisplayName("Branch Name")]
        public string BranchName { get; set; }

        public int CompanyID { get; set; }

        [DisplayName("Branch Address")]
        public string BranchAddres { get; set; }

        [DisplayName("Contact No")]
        [Required(ErrorMessage = "Contact No is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Contact Number.")]
        public string BranchContactNo { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string BranchManger { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public string RentAgrementStartDate { get; set; }
        public string RentAgrimentEndDate { get; set; }
        public decimal BranchRent { get; set; }
        public string OwnerName { get; set; }
        public int IsDelete { get; set; }
        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~clsBranch()
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


    public class clsMisc : IDisposable
    {
        public string ReqType { get; set; }

        public int MiscId { get; set; }
        public int CompanyId { get; set; }


        [Required(ErrorMessage = "Misc Name is required.")]
        [DisplayName("Misc Name")]
        public string MiscName { get; set; }


        [Required(ErrorMessage = "Misc Type is required.")]
        [DisplayName("Misc Type")]
        public string MiscType { get; set; }
        public string tmpMiscType { get; set; }

        public int IsDelete { get; set; }

        //public DateTime? CreateDate { get; set; }
        //public int? CreateBy { get; set; }
        //public DateTime? UpdateDate { get; set; }
        //public int? UpdateBy { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        ~clsMisc()
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




    public class clsDocument : IDisposable
    {
        public string ReqType { get; set; }

        public int DocID { get; set; }

        [Required(ErrorMessage = "Document Name is required.")]
        [DisplayName("Document Name")]
        public string DocumentName { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [DisplayName("Product Name")]
        public int ProdID { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }


        [DisplayName("Is Requried")]
        public bool IsRequried { get; set; }
        public int IsDelete { get; set; }
        public int CompanyID { get; set; }

        //public DateTime UpdateDate { get; set; }
        //public int UpdatedBy { get; set; }
        //public int CreatedBy { get; set; }
        //public DateTime CreateDate { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        ~clsDocument()
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

    public class clsEmployee
    {
        public string ReqType { get; set; }
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [Required(ErrorMessage = "Employee Name is required.")]
        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Father Name")]
        public string FatherName { get; set; }

        [DisplayName("Mother Name")]
        public string MotherName { get; set; }

        public string Address { get; set; }

        [DisplayName("State")]
        public int? StateID { get; set; }

        [DisplayName("City")]
        public int? CityID { get; set; }


        [DisplayName("Pin Code")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "{0} is Required")]
        [DisplayName("Mobile No")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid {0}")]
        public string ContactNo1 { get; set; }

        [DisplayName("Mobile No2")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid {0}")]
        public string ContactNo2 { get; set; }

        [DisplayName("WhatsAppNo")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid {0}")]
        public string WhatsAppNo { get; set; }

        [DisplayName("Email")]
        //[DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail address")]
        [StringLength(50, ErrorMessage = "{0} max limit only 50 char")]
        public string Email { get; set; }

        [DisplayName("Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }
        [DisplayName("PAN")]
        public string PAN { get; set; }
        [DisplayName("Aadhaar No")]
        public string AadhaarNo { get; set; }
        [DisplayName("Marital Status")]
        public string MaritalStatus { get; set; }

        [DisplayName("Photo")]
        public HttpPostedFile Imagefile { get; set; }
        public string ImageName { get; set; }
        public int IsDelete { get; set; }
        public int CompId { get; set; }
        public string Longtitute { get; set; }
        public string Langtiute { get; set; }
        public int? EmployeeAttnStatus { get; set; }
        public DateTime? AttnDate { get; set; }

        [Required(ErrorMessage = "Branch is required.")]
        [DisplayName("Branch")]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [DisplayName("Role")]
        public int RoleId { get; set; }


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

        ~clsEmployee()
        {
            Dispose(false);
        }

    }


    public class clsStateDDL
    {
        public static SelectList GetStateDDL()
        {
            return ClsCommon.ToSelectList(DataInterface1.GetState(), "ID", "StateName");
        }
    }

    public class clsCityDDL
    {
        public static SelectList GetCityDDL()
        {
            clsCity cls = new clsCity();
            cls.ReqType = "View";
            return ClsCommon.ToSelectList(DataInterface1.GetCity(cls), "CityId", "CityName");
        }
    }

    public class clsLeadDetail
    {
        public string ReqType { get; set; }
        public int LeadDtlId { get; set; }
        public int LeadId { get; set; }

        [Required(ErrorMessage = "Stage is required.")]
        [DisplayName("StageId")]
        public int StageId { get; set; }

        [DisplayName("StageName")]
        public string StageName { get; set; }

        public int IsActive { get; set; }
        public int IsCurrent { get; set; }

        [DisplayName("ShortStage_Name")]
        public string ShortStage_Name { get; set; }


        public int Dependancy { get; set; }
        public int Sequence { get; set; }
        public int CompanyId { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }

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

        ~clsLeadDetail()
        {
            Dispose(false);
        }

    }


    public class clsLeadMain : clsLeadDetail
    {
        //public string ReqType { get; set; }
        //public int LeadId { get; set; }
        public string LeadNo { get; set; }
        public int MainProdId { get; set; }
        public string MainProdName { get; set; }
        public string MainProdType { get; set; }
        public int ProdId { get; set; }
        public string ProdName { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public List<clsLeadCalling> clsLeadCalling { get; set; }
        public clsLeadCredit clsLeadCredit { get; set; }
        //public List<clsLeadCredit> clsLeadCred { get; set; }

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

        ~clsLeadMain()
        {
            Dispose(false);
        }

    }

    public class clsLeadCalling
    {
        public string ReqType { get; set; }
        public int TcId { get; set; }
        public int LeadId { get; set; }

        [DisplayName("QuestionId")]
        public int QuestionId { get; set; }
        public string LeadNo { get; set; }
        [DisplayName("Question")]
        public string Question { get; set; }
        public string QuestionAnsType { get; set; }
        //[Required(ErrorMessage = "Answer is required.")]

        [DisplayName("Answer")]
        public string Answer { get; set; }

        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        public int IsDelete { get; set; }

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

        ~clsLeadCalling()
        {
            Dispose(false);
        }

    }

    public class clsLeadDocument : clsLeadDetail
    {
        //public string ReqType { get; set; }
        public int DcId { get; set; }
        //public int LeadId { get; set; }
        public string LeadNo { get; set; }

        [Required(ErrorMessage = "Document is required.")]
        [DisplayName("Document")]
        public int DocID { get; set; }

        [DisplayName("CustomerType")]
        public string CustomerType { get; set; }

        [DisplayName("Is Received")]
        public bool IsReceived { get; set; }

        [DisplayName("Remarks")]
        //public string Remarks { get; set; }
        public bool IsDelete { get; set; }
        public bool? IsRequried { get; set; }

        //[DisplayName("Document Name")]
        public string DocumentName { get; set; }
        public int LeadCustId { get; set; }
        public string CustName { get; set; }
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

        ~clsLeadDocument()
        {
            Dispose(false);
        }

    }

    public class clsLeadCredit : IDisposable
    {
        public string ReqType { get; set; }
        public int CrId { get; set; }
        public int LeadId { get; set; }

        public string CIBILCode { get { return "CIBIL"; } }
        [DisplayName("CIBIL Verification")]
        public string CIBILVerification { get; set; }
        public string CibilDoc { get; set; }
        public string CibilRemarks { get; set; }

        public string CAMCODE { get { return "CAMCODE"; } }
        [DisplayName("CAM Generation")]
        

        public string FICode { get { return "FI"; } }
        [DisplayName("FI Verification")]
        public string FIVerification { get; set; }
        public string FIDoc { get; set; }
        public string FIRemarks { get; set; }


        public string TVRCode { get { return "TVR"; } }
        [DisplayName("TVR Verification")]
        public string TVRVerification { get; set; }
        public string TVRDoc { get; set; }
        public string TVRRemarks { get; set; }


        public string CashFlowCode { get { return "CASHFLOW"; } }
        [DisplayName("CashFlow Verification")]
        public string CashFlowVerification { get; set; }
        public string CashFlowDoc { get; set; }
        public string CashFlowRemarks { get; set; }


        public string DependentFamilyAssessmentVerificationCode { get { return "FAMILY"; } }
        [DisplayName("Dependent Family Assessment")]
        public string DependentFamilyAssessmentVerification { get; set; }
        public string DependentFamilyAssessmentDoc { get; set; }
        public string DependentFamilyAssessmentRemarks { get; set; }


        public string ViechleCode { get { return "VIECHLE"; } }
        [DisplayName("Viechle Verification")]
        public string ViechleValVerfication { get; set; }
        public string ViechleDoc { get; set; }
        public string ViechleRemarks { get; set; }

        public string BankStmtCode { get { return "BANKSTATEMENT"; } }
        [DisplayName("Bank Statement Verification")]
        public string BankStmtVerification { get; set; }
        public string BankStmtDoc { get; set; }
        public string BankStmtRemarks { get; set; }


        public string IncomeStmtCode { get { return "INCOMESTATEMENT"; } }
        [DisplayName("Income Statement Verification")]
        public string IncomeStmtVerification { get; set; }
        public string IncomeStmtDoc { get; set; }
        public string IncomeStmtRemarks { get; set; }


        public string PersonalDiscussCode { get { return "PERSONALDISCUSS"; } }
        [DisplayName("Personal Discuss Verification")]
        public string PersonalDiscussVerification { get; set; }
        public string PersonalDiscussDoc { get; set; }
        public string PersonalDiscusssRemarks { get; set; }


        public string EligiblityCode { get { return "ELIGIBLITY"; } }
        [DisplayName("Eligibilty")]
        public string Eligiblity { get; set; }
        public string EligiblityDoc { get; set; }
        public string EligiblityRemarks { get; set; }
        public string Occupation { get; set; }


        public string PropertyCode { get { return "PROPERTY"; } }
        [DisplayName("Property Doc Verification")]
        public string PropertyDocVerification { get; set; }
        public string PropertyDoc { get; set; }
        public string PropertyDocRemarks { get; set; }

        public string ColletralType { get; set; }
        public string PropertyAddress { get; set; }
        public string PropertySize { get; set; }
        public string LandArea { get; set; }
        public string Dimension { get; set; }
        public string SecurityValue { get; set; }
        public string MarketValue { get; set; }
        public string LandValue { get; set; }
        public string ConstrutionValue { get; set; }
        public string TotalMarketValue { get; set; }
        public string LTV { get; set; }
        public string RelizableValue { get; set; }
        public string PropertyVal { get; set; }
        public string PropertyDocuments { get; set; }
        public string PropertyChain { get; set; }
        public string LegalOpinionBy { get; set; }
        public string LegalReportDate { get; set; }
        public string ValuerName { get; set; }
        public string Valuation { get; set; }
        public string ValueDate { get; set; }
        public string BussinessName { get; set; }
        public string BussinessVinatage { get; set; }
        public string BussinessAddress { get; set; }
        public string BussinessProve { get; set; }
        public string CAMGeneration { get; set; }
        public string CAMDoc { get; set; }
        public string CAMRemarks { get; set; }
        public string CreditRemarks { get; set; }
        public string NegativeRemarks { get; set; }


        public string VehicleType { get; set; }
        public string ChassisNo { get; set; }
        public string EngineNo { get; set; }
        public string Insurername { get; set; }
        public string InsurancePolicyNo { get; set; }
        public string PolicyValidity { get; set; }
        public string RCDate { get; set; }
        public string Fitness { get; set; }
        public int? DealerId { get; set; }
        public string RTO { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
        public string CarType { get; set; }
        public string ProposedLoanAmountAndCommercial { get; set; }
        public string Occupancy { get; set; }
        public string CreditCheckListCode { get { return "CREDITCHECKLIST"; } }
        [DisplayName("Credit Check List")]
        public string CreditCheckList { get; set; }
        public int CreditCheckListID { get; set; }
        public string CreditCheckListRemarks { get; set; }
        public int? CompanyId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [DisplayName("CAM Verification")]
        public string CamVerification { get; set; }
        
        public HttpPostedFileBase CibilDocPostedFile { get; set; }

        public HttpPostedFileBase FIDocPostedFile { get; set; }
        public HttpPostedFileBase TVRDocPostedFile { get; set; }
        public HttpPostedFileBase CashFlowDocPostedFile { get; set; }
        public HttpPostedFileBase ViechleDocPostedFile { get; set; }
        public HttpPostedFileBase BankStmtDocPostedFile { get; set; }
        public HttpPostedFileBase IncomeStmtDocPostedFile { get; set; }
        public HttpPostedFileBase PersonalDiscussDocPostedFile { get; set; }
        public HttpPostedFileBase EligiblityDocPostedFile { get; set; }
        public HttpPostedFileBase PropertyDocPostedFile { get; set; }
        public HttpPostedFileBase CAMDocPostedFile { get; set; }
        public HttpPostedFileBase DependentFamilyAssessmentDocPostedFile { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        ~clsLeadCredit()
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


    public class clsUploadDownload
    {


    }

    public class clsCommonData
    {
        public static SelectList GetEmoloyeeDDL()
        {
            clsEmployee cls = new clsEmployee();
            cls.ReqType = "view";
            cls.CompId = ClsSession.CompanyID;
            cls.IsDelete = 0;
            return ClsCommon.ToSelectList(DataInterface1.dbEmployee(cls), "EmpID", "EmpName");

        }

    }

    public class clsCenter : IDisposable
    {
        public string ReqType { get; set; }

        public int CenterId { get; set; }


        [Required(ErrorMessage = "Center Name is required.")]
        [DisplayName("Center Name")]
        public string CenterName { get; set; }

        public int CompanyID { get; set; }
        public int MaxNo { get; set; }
        [DisplayName("Center Head")]
        public string CenterHead { get; set; }

        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public int IsDelete { get; set; }
        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~clsCenter()
        {
            Dispose(false);
        }
        private void ReleaseNativeResource()
        {

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

    }
}