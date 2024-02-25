using Sunnet_NBFC.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Sunnet_NBFC.Models
{

    public class clsFilesLead : IDisposable
    {
        public string ReqType { get; set; }
        public int leadid { get; set; }

        public string SanctionLetter { get; set; }


        public string WelcomeLetter { get; set; }

        public string RepyamentLetter { get; set; }
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

        ~clsFilesLead()
        {
            Dispose(false);
        }

    }

    public class clsCity : IDisposable
    {
        public string ReqType { get; set; }
        public int Cityid { get; set; }

        public string CityName { get; set; }


        public int Stateid { get; set; }

        public string StateName { get; set; }
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

        ~clsCity()
        {
            Dispose(false);
        }

    }

    public class clsProduct : IDisposable
    {


        public string ReqType { get; set; }
        [Required(ErrorMessage = "Main Product is required.")]
        [DisplayName("Main Product")]
        public int MainProdId { get; set; } = 0;
        public int ProdId { get; set; } = 0;


        [Required(ErrorMessage = "Product Name is required.")]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        public int IsDelete { get; set; }
        public string CustTypeRequried { get; set; }
        public int CompanyId { get; set; }

        [DisplayName("Main Product")]
        public string MainProduct { get; set; }
        public string SearchMainProdId { get; set; }
        public string SerarchProdId { get; set; }

        public string CustTypeName { get; set; }
        public string ReportProductName { get; set; }
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

        ~clsProduct()
        {
            Dispose(false);
        }


        public static SelectList GetProductDDL()
        {
            clsProduct cls = new clsProduct();
            cls.ReqType = "View";
            cls.IsDelete = 0;
            return ClsCommon.ToSelectList(DataInterface1.GetProduct(cls), "ProdId", "ProductName");
        }

    }




    public class clsEmployeeDetails
    {
        public string ReqType { get; set; }
        public int EmpDtlID { get; set; }

        [Required(ErrorMessage = "Employee is required.")]
        [DisplayName("Employee")]
        public int EmpID { get; set; }
        [Required(ErrorMessage = "Desgination is required.")]
        [DisplayName("Designation")]
        public int DesignationID { get; set; }

        [DisplayName("Department")]
        public int DepartmentId { get; set; }

        [DisplayName("Date Of Joining")]
        public string DOJ { get; set; }

        [DisplayName("Last ESIC No.")]
        public string LastESICNo { get; set; }

        [DisplayName("Last PF No.")]
        public string LastPFNo { get; set; }

        [DisplayName("Last Acadmic Degree")]
        [Required(ErrorMessage = "Last Acadmic Degree is required.")]
        public string LastAcadmicDegree { get; set; }

        [DisplayName("Last Professional Degree")]
        public string LastProfDegree { get; set; }

        [DisplayName("Last Company")]
        public string LastCompany { get; set; }

        [DisplayName("Last Experience Detail")]
        public string LastExperienceDtls { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        public double Salary { get; set; }
        public bool IsLeave { get; set; }

        [DisplayName("Date Of Leave")]
        public string DOL { get; set; }

        [DisplayName("Login Type")]
        public string LoginType { get; set; }
        public int Companyid { get; set; }
        public int IsActive { get; set; }
        public int IsDelete { get; set; }
        public string Longtitute { get; set; }
        public string Langtiute { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

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

        ~clsEmployeeDetails()
        {
            Dispose(false);
        }


    }


    public class clsMenuMaster : IDisposable
    {
        public string ReqType { get; set; }
        public int MenuId { get; set; }

        [Required(ErrorMessage = "Menu Name is required.")]
        [DisplayName("Menu Name")]
        public string MenuName { get; set; }

        [Required(ErrorMessage = "Menu Url is required.")]
        [DisplayName("Menu Url")]
        public string MenuUrl { get; set; }
        public int MenuActive { get; set; }
        public string ActiveStr { get; set; }
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

        ~clsMenuMaster()
        {
            Dispose(false);
        }
    }

    public class clsSubMenu : IDisposable
    {
        public string ReqType { get; set; }
        public int SubMenuId { get; set; }

        [Required(ErrorMessage = "Menu Name is required.")]
        [DisplayName("Menu Name")]
        public int MenuId { get; set; }

        [Required(ErrorMessage = "Title Name is required.")]
        [DisplayName("Title Name")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Controller is required.")]
        [DisplayName("Controller Name")]
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

        ~clsSubMenu()
        {
            Dispose(false);
        }
    }

    public class clsStageRole:IDisposable
    {
        public string ReqType { get; set; }
        public int StageRoleId { get; set; }

        [Required(ErrorMessage = "Employee is required.")]
        [DisplayName("Employee")]
        public int StageRoleEmpId { get; set; }

        [DisplayName("Employee Code")]
        public string StageRoleEmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string StageRoleEmpName { get; set; }

        [DisplayName("Role Name")]
        [Required(ErrorMessage = "Role Name is required.")]
        public string StageRoleName { get; set; }

        [DisplayName("CreatedBy")]
        public int CreatedBy { get; set; }
        public string SearchEmpId { get; set; }
        public string SerachRoleName { get; set; }
        public int CompanyID { get; set; }
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

        ~clsStageRole()
        {
            Dispose(false);
        }
    }
    public class clsLeadmaind : clsLeadDocument
    {
        //public string ReqType { get; set; }
        //public int LeadId { get; set; }
        public string LeadNo { get; set; }
        public string MainProdName { get; set; }
        public string ProdName { get; set; }
        public string CustomerName {get; set; }
        public string ContactNo { get;set; }
        public List<clsLeadDocument> clsLeadDocument { get; set; }


        bool disposed = false;

        ~clsLeadmaind()
        {
            Dispose(false);
        }

    }

    public class clsLeadDocSign : clsLeadDetail
    {
        //public string ReqType { get; set; }
        public int DocSignId { get; set; }
        public int MainProdId { get; set; }
        public string LeadNo { get; set; }

        //[Required(ErrorMessage = "Employee is required.")]
        [DisplayName("Documents")]
        public string Documents { get; set; }

        [DisplayName("Sanction Letter")]
        public string SanctionLetter { get; set; }

        [DisplayName("Loan Agreement Kit")]
        public string LoanAgrmentKit { get; set; }

        [DisplayName("PDC")]
        //[Required(ErrorMessage = "Role Name is required.")]
        public string PDC { get; set; }

        [DisplayName("NACH")]
        public string NACH { get; set; }

        [DisplayName("Disbursment Kit")]
        public string DisbursmentKit { get; set; }

        [DisplayName("Insurance With HP")]
        public string InsuranceWithHP { get; set; }

        [DisplayName("NOC")]
        public string NOC { get; set; }

        [DisplayName("RTO Slip")]
        public string RTOSlip { get; set; }

        [DisplayName("Orignal Property Paper")]
        public string OrignalPropertyPaper { get; set; }

        [DisplayName("Registered Mortgage Deed")]
        public string RegisteredMortgageDeed { get; set; }

        [DisplayName("Equitable Mortage Deed")]
        public string EquitableMortageDeed { get; set; }

        [DisplayName("Affidavit")]
        public string Affidavit { get; set; }

        [DisplayName("Remark")]
        public string DsRemark { get; set; }

        [DisplayName("CreatedBy")]
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int IsDelete { get; set; }

        [DisplayName("Borrower Kyc")]
        public string BorrowerKyc { get; set; }

        [DisplayName("CoBorrower Kyc")]
        public string CoBorrowerKyc { get; set; }

        [DisplayName("Guarantor Kyc")]
        public string GuarantorKyc { get; set; }

        [DisplayName("Borrower Photo")]
        public string BorrowerPhoto { get; set; }

        [DisplayName("CoBorrower Photo")]
        public string CoBorrowerPhoto { get; set; }

        [DisplayName("Guarantor Photo")]
        public string GuarantorPhoto { get; set; }

        [DisplayName("Disbursement Request Letter")]
        public string DisbursementRequestLetter { get; set; }

        [DisplayName("Signature Verification")]
        public string SignatureVerification { get; set; }

        [DisplayName("Kyc Self Attested")]
        public string KycSelfAttested { get; set; }

        public string MainProdName { get; set; }
        public string ProdName { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public string ReqLoanAmt { get; set; }


        //bool disposed = false;

        //// Public implementation of Dispose pattern callable by consumers.
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //// Protected implementation of Dispose pattern.
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposed)
        //        return;

        //    if (disposing)
        //    {
        //        // Free any other managed objects here.
        //        //
        //    }

        //    // Free any unmanaged objects here.
        //    //
        //    disposed = true;
        //}

        //~clsLeadDocSign()
        //{
        //    Dispose(false);
        //}
    }


    public class clsLeadFinalApprove : clsLeadDetail
    {
        //public string ReqType { get; set; }
        public string LeadNo { get; set; }
        public int FinalApproveId { get; set; }
        //public int LeadId { get; set; }

        //[Required(ErrorMessage = "Employee is required.")]
        [DisplayName("Particulers")]
        public string Particulers { get; set; }

        [DisplayName("Procces fees")]
        public decimal Proccesfees { get; set; }

        [DisplayName("Advance EMI")]
        public int AdvanceEMI { get; set; }

        [DisplayName("GST")]
        public decimal GST { get; set; }

        [DisplayName("NetDisbAmt")]
        public decimal NetDisbAmt { get; set; }

        [DisplayName("TrnchsNo")]
        public decimal TrnchsNo { get; set; }

        [DisplayName("CersaiCharges")]
        public decimal CersaiCharges { get; set; }

        [DisplayName("StamppingCharges")]
        public decimal StamppingCharges { get; set; }

        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [DisplayName("CreatedBy")]
        public int CreatedBy { get; set; }
        //public string ReqType { get; set; }  //already exists in child class




        [DisplayName("UpdatedBy")]
        public int UpdatedBy { get; set; }

        [DisplayName("IsDelete")]
        public int IsDelete { get; set; }

        [DisplayName("CompanyId")]
        public int CompanyId { get; set; }

        [DisplayName("BorrowerKyc")]
        public string BorrowerKyc { get; set; }

        [DisplayName("GuarantorKyc")]
        public string GuarantorKyc { get; set; }

        [DisplayName("PDC")]
        public string PDC { get; set; }

        [DisplayName("BorrowerPhoto")]
        public string BorrowerPhoto { get; set; }

        [DisplayName("CoBorrowerPhoto")]
        public string CoBorrowerPhoto { get; set; }

        [DisplayName("GuarantorPhoto")]
        public string GuarantorPhoto { get; set; }

        [DisplayName("SanctionLetter")]
        public string SanctionLetter { get; set; }

        [DisplayName("LoanAgreementkit")]
        public string LoanAgreementkit { get; set; }

        [DisplayName("DisbursementRequestLetter")]
        public string DisbursementRequestLetter { get; set; }

        [DisplayName("NocPreviousFinanced")]
        public string NocPreviousFinanced { get; set; }

        [DisplayName("Rtoslip")]
        public string Rtoslip { get; set; }

        public string MainProdName { get; set; }
        public string ProdName { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public string ReqLoanAmt { get; set; }


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

        ~clsLeadFinalApprove()
        {
            Dispose(false);
        }
    }


    public class clsLeadFinalApproveMain : clsLeadFinalApprove
    {
        public string LeadNo { get; set; }
        public List<clsLeadFinalApprove> clsLeadFinalApprove { get; set; }


        bool disposed = false;

        ~clsLeadFinalApproveMain()
        {
            Dispose(false);
        }


    }

    public class clsDisbursement : clsLeadDetail
    {
        //public string ReqType { get; set; }
        public string LeadNo { get; set; }
        public int DisbursementId { get; set; }

        [DisplayName("Net Disbursement Amount")]
        public decimal NetDisbursementAmount { get; set; }

        [DisplayName("IFSC Code")]
        public string IfscCode { get; set; }

        [DisplayName("Account No.")]

        public string BeneficiaryAccountNo { get; set; }

        [DisplayName("Account Holder Name")]
        public string BeneficiaryName { get; set; }

        [DisplayName("Remark")]
        public string DRemark { get; set; }

        [DisplayName("CreatedBy")]
        public int CreatedBy { get; set; }

        [DisplayName("UpdatedBy")]
        public int UpdatedBy { get; set; }

        [DisplayName("IsDelete")]
        public int IsDelete { get; set; }

        [DisplayName("Payment Mode")]
        public string PaymentMode { get; set; }
        [DisplayName("Rate Of Interest")]
        public decimal ROI { get; set; }
        [DisplayName("Tenure (In Months)")]
        public int Tenure { get; set; }
        [DisplayName("Utr No.")]
        public string UtrNo { get; set; }
        [DisplayName("Loan Start Date")]
        public string LoanStartDate { get; set; }
        public string MainProdName { get; set; }
        public string ProdName { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public decimal ReqLoanAmt { get; set; } = 0;
        public int MiscId { get; set; } // for payment mode by misc master
        public string SanctionLetter_fileName { get; set; }
        public string WelcomeLetter_fileName { get; set; }
        public string RepyamentLetter_fileName { get; set; }

    }
    public class clsEmi
    {
        [DisplayName("Sr. No.")]
        public int Id { get; set; }
        public int PERIOD { get; set; }
        [DisplayName("Emi Date")]
        public string PAYDATE { get; set; }
        [DisplayName("Emi Amount")]
        public decimal PAYMENT { get; set; }
        [DisplayName("Current Balance")]
        public decimal CURRENT_BALANCE { get; set; }
        [DisplayName("Interest")]
        public decimal INTEREST { get; set; }
        [DisplayName("Principal")]
        public decimal PRINCIPAL { get; set; }
        [DisplayName("StartPaymentDate")]
        public string StartPaymentDate { get; set; } = "01/01/1900";
    }

    public class clsDisbursementMain : clsDisbursement
    {
        public string LeadNo { get; set; }
        public List<clsDisbursement> clsDisbursement { get; set; }

        bool disposed = false;

        ~clsDisbursementMain()
        {
            Dispose(false);
        }


    }

    public class clsMiscDDL
    {
        public static SelectList GetMiscDDL(string mtype)
        {
            return ClsCommon.ToSelectList(DataInterface1.GetMiseddl(mtype), "MiscId", "MiscName");
        }
    }
    public class clsBranchDDL
    {
        public static SelectList GetBranchDDL()
        {
            return ClsCommon.ToSelectList(DataInterface1.dbBranchddl(), "BranchId", "BranchName");
        }
    }

    public class clsEmoloyeeDDL
    {
        public static SelectList GetEmoloyeeDDL()
        {
            clsEmployee cls = new clsEmployee();
            cls.ReqType = "view";
            cls.CompId = ClsSession.CompanyID;
            cls.IsDelete = 0;
            return ClsCommon.ToSelectList(DataInterface1.dbEmployee(cls), "EmpID", "EmployeeName");

        }
    }

    public class clsRoleDDL
    {
        public static SelectList GetRoleDDL()
        {
            return ClsCommon.ToSelectList(DataInterface1.dbStageMasterddl(), "ShortStage_Name", "Stage_Name");

        }
    }


    public class clsChargesMaster : IDisposable
    {

        public string ReqType { get; set; }
        public int ChargeID { get; set; }
        public int CompanyID { get; set; }

        [Required(ErrorMessage = "Charge Name is required.")]
        public string ChargeName { get; set; }
        public int ChargeTypeID { get; set; }
        public string ChargeType { get; set; }
        public bool IsChargePer { get; set; }
        public decimal ChargePer { get; set; }
        public decimal ChargeAmount { get; set; }
        public DateTime? EffectiveFromDate { get; set; }
        public DateTime? EffectiveToDate { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
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

        ~clsChargesMaster()
        {
            Dispose(false);
        }

    }

    public class clsBankMaster : IDisposable
    {

        public string ReqType { get; set; }
        public int BankId { get; set; }

        [Required(ErrorMessage = "BankName is required.")]
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string BankIFSCCode { get; set; }
        public string BankMICRCode { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
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

        ~clsBankMaster()
        {
            Dispose(false);
        }

    }

    public class clsReceipt : IDisposable
    {

        public string ReqType { get; set; }
        public long ReceiptID { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public int LeadID { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int ReceiptNoSeqNo { get; set; }
        public decimal EMIAmt { get; set; }
        public decimal PenaltyAmt { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal OtherAmt { get; set; }
        public decimal TotalAmt { get; set; }
        public int PaymentModeID { get; set; }
        public int BankID { get; set; }
        public string ChequeBankName { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        public string Remarks { get; set; }
        public string EntryFrom { get; set; }
        public int StatusID { get; set; }
        public bool IsDelete { get; set; }
        public string DeletedReason { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public virtual ICollection<clsReceiptChargesDetails> clsReceiptChargesDetails { get; set; }

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

        ~clsReceipt()
        {
            Dispose(false);
        }

    }

    public class clsReceiptChargesDetails : IDisposable
    {

        public long ReceiptDtlID { get; set; }
        public int ReceiptID { get; set; }
        public int ChargeTypeID { get; set; }
        public int InstallmentID { get; set; }
        public string InstallmentNo { get; set; }
        public double Amount { get; set; }
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

        ~clsReceiptChargesDetails()
        {
            Dispose(false);
        }

    }

    public class clsReceiptChargesDetailsFill : IDisposable
    {

        public long ReceiptDtlID { get; set; }
        public int ReceiptID { get; set; }
        public int ChargeTypeID { get; set; }
        public string ChargeType { get; set; }
        public string KeyType { get; set; }
        public int InstallmentID { get; set; }
        public string Installment { get; set; }
        public double Amount { get; set; }
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

        ~clsReceiptChargesDetailsFill()
        {
            Dispose(false);
        }

    }


    

    public class clsLedgerMaster
    {
        public string ReqType { get; set; }
        public int LedgerID { get; set; }
        public int CompanyID { get; set; }

        [DisplayName("Ledger Name")]
        [Required(ErrorMessage = "Ledger Name is required.")]
        public string LedgerName { get; set; }
        [DisplayName("Ledger Code")]
        [Required(ErrorMessage = "Ledger Code is required.")]
        public string LedgerCode { get; set; }
        public int LedgerGroupID { get; set; }
        public int StatusID { get; set; }
        public int IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }

    public class clsChargesType
    {
        public string ReqType { get; set; }
        public int ChargeTypeID { get; set; }

        [DisplayName("Charge Type Name")]
        [Required(ErrorMessage = "Charge Type Name is required.")]
        public string ChargeTypeName { get; set; }
        [DisplayName("Charge Type Code")]
        [Required(ErrorMessage = "Charge Type Code is required.")]
        public string ChargeTypeCode { get; set; }

        [DisplayName("Key Name")]
        //[Required(ErrorMessage = "Key Name is required.")]
        public int KeyID { get; set; }

        [DisplayName("Ledger Name")]
        [Required(ErrorMessage = "Ledger Name is required.")]
        public int LedgerID { get; set; }
        public int IsDelete { get; set; }
        public int CompanyID { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        [DisplayName("Key Name")]
        public string KeyName { get; set; }
        [DisplayName("Key Code")]
        public string KeyCode { get; set; }
        [DisplayName("Ledger Name")]
        public string LedgerName { get; set; }
        [DisplayName("Ledger Code")]
        public string LedgerCode { get; set; }

    }

    public class clsKeyDDL
    {
        public static SelectList GetKeyDDL()
        {
            clsChargesType cls = new clsChargesType();
            cls.ReqType = "ddlkey";
            cls.CompanyID = ClsSession.CompanyID;
            cls.IsDelete = 0;
            return ClsCommon.ToSelectList(DataInterface1.dbChargesType(cls), "KeyID", "KeyName");
        }
    }

    public class clsLedgerDDL
    {
        public static SelectList GetLedgerDDL()
        {
            clsLedgerMaster cls = new clsLedgerMaster();
            cls.ReqType = "view";
            cls.CompanyID = ClsSession.CompanyID;
            cls.IsDelete = 0;
            return ClsCommon.ToSelectList(DataInterface1.dbLedgerMaster(cls), "LedgerID", "LedgerName");
        }
    }

    public class clsForecloseEntry
    {
        public string ReqType { get; set; }
        public int LeadId { get; set; }
        //[Required]
        //[MaxLength(12)]
        //[MinLength(1)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "LeadNo must be numeric")]
        public string SearchLeadNo { get; set; }

        [DisplayName("LeadNo")]
        public string LeadNo { get; set; }

        [DisplayName("Main Product")]
        public string MainProduct { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [DisplayName("Branch Name")]
        public string BranchName { get; set; }

        [DisplayName("Loan Amount")]
        public decimal NetDisbursementAmount { get; set; }
        public int CompanyID { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        [DisplayName("ROI")]
        public decimal ROI { get; set; }

        [DisplayName("Tenure")]
        public int Tenure { get; set; }

        [DisplayName("Loan Start Date")]
        public string LoanStartDate { get; set; }

        [DisplayName("Loan End Date")]
        public string LoanEndDate { get; set; }

        [DisplayName("Emi Amount")]
        public decimal EmiAmount { get; set; }

        [DisplayName("TotalInst")]
        public decimal TotalInst { get; set; }

        [DisplayName("DSA Name")]
        public string DSAName { get; set; }

        [DisplayName("Customer Name")]
        public string CustName { get; set; }

        [DisplayName("Father Name")]
        public string FatherName { get; set; }

        [DisplayName("Mobile No I")]
        public string MobileNo1 { get; set; }

        [DisplayName("Mobile No II")]
        public string MobileNo2 { get; set; }

        [DisplayName("Principal Outstanding")]
        public decimal pos { get; set; }

        [DisplayName("Current Month Interest")]
        public decimal CurrentMonthInterest { get; set; }

        [DisplayName("Installment Overdue")]
        public decimal InstalmentOverdue { get; set; }

        [DisplayName("Foreclosure Charges")]
        public decimal ForeclosureCharges { get; set; }

        [DisplayName("GST on foreclosure Charges")]
        public decimal GstOnForclose { get; set; }

        [DisplayName("Excess Amount")]
        public decimal ExcessAmount { get; set; }

        [DisplayName("Bouncing Charges")]
        public decimal BouncingCharges { get; set; }

        [DisplayName("Penal Charges")]
        public decimal PenalCharges { get; set; }

        [DisplayName("Other Charges")]
        public decimal OtherCharges { get; set; }

        [DisplayName("Final Foreclosure Amount")]
        public decimal FinalForeclosureAmount { get; set; }

    }


    public class clsLeadPostDisburse : IDisposable
    {
        public string ReqType { get; set; }
        public int Id { get; set; } = 0;
        public int LeadId { get; set; } = 0;
        public string LeadNo { get; set; } = "";

        [DisplayName("Original Documents No")]
        public string OrgDocNo { get; set; } = "";

        [DisplayName("Document Type")]
        public string DocType { get; set; } = "";

        [DisplayName("Document Date")]
        public string DocDate { get; set; } = "";

        [DisplayName("Page No From")]
        public int PagesFrom { get; set; } = 0;

        [DisplayName("Page No To")]
        public int PagesTo { get; set; } = 0;

        [DisplayName("Any Other Document")]
        public string AnyOther { get; set; } = "";

        [DisplayName("Registration Certificate")]
        public string RegistrationCertificate { get; set; }

        [DisplayName("Insured HP Endorse")]
        public string InsuredHPEndorse { get; set; }

        [DisplayName("Invoice HP Endorse")]
        public string InvoiceHPEndorse { get; set; }

        [DisplayName("Margin Money Endorse")]
        public string MarginMoneyEndorse { get; set; }

        [DisplayName("NOC")]
        public string NOC { get; set; }

        [DisplayName("RTO Slip")]
        public string RTOSlip { get; set; }

        [DisplayName("Endorsed RC")]
        public string EndorsedRC { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int isDelete { get; set; }

        public string MainProdName { get; set; }
        public string ProdName { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        [DisplayName("Document Type")]
        public string DocTypeName { get; set; }
        public decimal ReqLoanAmt { get; set; } = 0;
        public int MainProdId { get; set; }
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

        ~clsLeadPostDisburse()
        {
            Dispose(false);
        }
    }



    //===
}