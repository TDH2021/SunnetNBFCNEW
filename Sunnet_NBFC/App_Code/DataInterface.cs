using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Globalization;
using Newtonsoft.Json;
using System.Threading;
using Sunnet_NBFC.Models;
using System.Reflection;

public class DataInterface : IDisposable
{
    public SqlConnection _cnSQL = null;
    public ConnectionStatus _StateValue = ConnectionStatus.Unknown;
    public string _StateDescription = string.Empty;
    //private static errormessage="Error Occurred";

    /// <summary>
    /// Enumeration representing the state of the SQL connection.
    /// </summary>
    public enum ConnectionStatus
    {
        Open = 0,
        Closed = 1,
        Failed = 2,
        Unknown = 99

    }
    /// <summary>
    /// State of the SQL connection.
    /// </summary>
    public ConnectionStatus StateValue
    {
        get
        {
            return _StateValue;
        }
    }
    /// <summary>
    /// A description of the state of the connection or command.
    /// </summary>
    public string StateDescription
    {
        get
        {
            return _StateDescription;
        }
    }
    public void Dispose()
    {
        try
        {
            if (_cnSQL != null)
            {
                try { _cnSQL.Close(); }
                catch { }
                try { _cnSQL.Dispose(); }
                catch { }
                _cnSQL = null;
            }
        }
        catch { }
        _StateValue = ConnectionStatus.Closed;
        _StateDescription = "Disconnected OK";
    }


    public DataInterface()
    {
        try
        {
            _cnSQL = new SqlConnection();

            // _cnOLEDB.ConnectionString = "Provider=HP3KProvider;Server=10.10.25.101;ServerPort=30009;ServerType=0;User=mgr;UserPassword=c;Account=ccstest;AccountPassword=c;ImageDatabase0=dsc." + paramagencyloc + ".ccstest,CHIEF,0,1,0";
            // _cnOLEDB.ConnectionString = "Provider=HP3KProvider;Server=" + Servername + ";ServerPort=30009;ServerType=0;User=mgr;UserPassword=quantum;Account=ccs;AccountPassword=;ImageDatabase0=dsc." + paramagencyloc + ".ccs,CHIEF,0,1,0";

            _cnSQL.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            _cnSQL.Open();
            _StateValue = ConnectionStatus.Open;
            string Textline = string.Empty;
            string Textbody = string.Empty;

            if (_cnSQL.State == ConnectionState.Open)
            {
                _StateValue = ConnectionStatus.Open;
                _StateDescription = "Connection OK";
                SqlCommand Cmd = new SqlCommand();
            }
            else
            {
                Dispose();
                _StateValue = ConnectionStatus.Failed;
                _StateDescription = "invalid Connection: " + _cnSQL.State.ToString();
            }


        }
        catch (Exception se)
        {
            Dispose();
            _StateValue = ConnectionStatus.Failed;
            //_StateDescription = se.Message;
            _StateDescription = "Database Connection failed " + se.Message;
        }
    }


    public static int PostLead(clsLead cls)
    {
        int a = 0;
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OptType", SqlDbType.Int).Value = cls.OptType;
            cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = cls.LeadId;
            cmd.Parameters.Add("@CustName", SqlDbType.VarChar).Value = cls.CustName;
            cmd.Parameters.Add("@CustContNo", SqlDbType.BigInt).Value = cls.CustContNo;
            cmd.Parameters.Add("@CustContNo2", SqlDbType.VarChar).Value = cls.CustContNo2;
            cmd.Parameters.Add("@CustMail", SqlDbType.VarChar).Value = cls.CustMail;
            cmd.Parameters.Add("@CustAdress", SqlDbType.VarChar).Value = cls.CustAdress;
            cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = cls.Country;
            cmd.Parameters.Add("@Pincode", SqlDbType.VarChar).Value = cls.Pincode;
            cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = cls.CustGender;
            cmd.Parameters.Add("@ServiceId", SqlDbType.Int).Value = cls.ServiceId;
            cmd.Parameters.Add("@Website", SqlDbType.VarChar).Value = cls.Website;
            cmd.Parameters.Add("@LeadStatus", SqlDbType.VarChar).Value = cls.LeadStatus;
            cmd.Parameters.Add("@IndustryType", SqlDbType.VarChar).Value = cls.IndustryType;
            cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = cls.Remarks;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = cls.CustAdress;

            if (db.ExecuteNonQueryProcedure(cmd, "USP_LEAD") > 0)
            {
                a = 1;
            }
            else
            {
                a = 0;
            }

        }
        catch (Exception e)
        {

        }
        return a;
    }


    public static int User(clsUser cls)
    {
        int a = 0;
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OptType", SqlDbType.Int).Value = cls.OptType;
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = cls.UserId;
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = cls.UserName;
            cmd.Parameters.Add("@ContactNo", SqlDbType.BigInt).Value = cls.ContactNo;
            cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = cls.EmailAddress;
            cmd.Parameters.Add("@Userpic", SqlDbType.VarChar).Value = cls.Userpic;
            cmd.Parameters.Add("@UserPicName", SqlDbType.VarChar).Value = cls.UserPicName;
            cmd.Parameters.Add("@UserPicExt", SqlDbType.VarChar).Value = cls.UserPicExt;
            cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = cls.IsActive;
            if (db.ExecuteNonQueryProcedure(cmd, "USP_USER") > 0)
            {
                a = 1;
            }
            else
            {
                a = 0;
            }

            a = cmd.ExecuteNonQuery();

        }
        catch (Exception e)
        {

        }
        return a;
    }

    public static int PostTicket(clsTicket cls)
    {
        int a = 0;
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OptType", SqlDbType.Int).Value = cls.OptType;
            cmd.Parameters.Add("@TICKETID", SqlDbType.Int).Value = cls.TicketId;
            cmd.Parameters.Add("@TICKETNO", SqlDbType.VarChar).Value = cls.TicketNo;
            cmd.Parameters.Add("@Prodid", SqlDbType.VarChar).Value = cls.Prodid;
            cmd.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = cls.Description;
            cmd.Parameters.Add("@SOFTWARENAME", SqlDbType.VarChar).Value = cls.SoftwareName;
            cmd.Parameters.Add("@TICKETTYPE", SqlDbType.VarChar).Value = cls.TicketType;
            cmd.Parameters.Add("@CLIENTNAME", SqlDbType.VarChar).Value = cls.ClientName;
            cmd.Parameters.Add("@CLIENTCONTACTNO", SqlDbType.BigInt).Value = cls.ClientContactNo;
            cmd.Parameters.Add("@TICKETSTATUS", SqlDbType.VarChar).Value = cls.TicketStatus;
            cmd.Parameters.Add("@TICKETRESOLUTION", SqlDbType.VarChar).Value = cls.TicketResolution;
            cmd.Parameters.Add("@CLIENTREMARKS", SqlDbType.VarChar).Value = cls.ClientRemarks;
            cmd.Parameters.Add("@TicketDoc", SqlDbType.VarChar).Value = cls.TicketDoc;
            cmd.Parameters.Add("@ClientEmail", SqlDbType.VarChar).Value = cls.ClientEmail;
            cmd.Parameters.Add("@OUTPUTREsult", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
            if (db.ExecuteNonQueryProcedure(cmd, "USP_TICKET") > 0)
            {
                cls.TicketNo = cmd.Parameters["@OUTPUTREsult"].Value.ToString();
                a = 1;
            }
            else
            {
                a = 0;
            }

        }
        catch (Exception e)
        {

        }
        return a;
    }

    public static int PostClient(clsClient cls)
    {
        int a = 0;
        try
        {

            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OptType", SqlDbType.Int).Value = cls.OptType;
                    cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = cls.ClientId;
                    cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = cls.ClientName;
                    cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar).Value = cls.PhoneNo;
                    cmd.Parameters.Add("@WhatsUpNo", SqlDbType.VarChar).Value = cls.WhatsUpNo;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = cls.Address;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = cls.City;
                    cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = cls.State;
                    cmd.Parameters.Add("@PinCode", SqlDbType.BigInt).Value = cls.PinCode;
                    cmd.Parameters.Add("@GSTNo", SqlDbType.VarChar).Value = cls.GSTNo;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = cls.Email;
                    if (db.ExecuteNonQueryProcedure(cmd, "USP_Client") > 0)
                    {
                        a = 1;
                    }
                    else
                    {
                        a = 0;
                    }
                }



            }


        }
        catch (Exception e)
        {

        }
        return a;
    }

    public DataTable GetClient(clsClient cls)
    {
        DataTable dt = new DataTable();
        try
        {
            if (_StateValue == ConnectionStatus.Open)
            {
                DBOperation db = new DBOperation();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OptType", SqlDbType.Int).Value = cls.OptType;
                cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = cls.ClientId;
                dt = db.FillTableProc(cmd, "USP_Client");
            }
        }
        catch (Exception ex)
        {
            dt = null;
            return null;
        }
        finally
        {
            if (_cnSQL.State == ConnectionState.Open)
                _cnSQL.Close();

        }

        return dt;
    }

    public DataTable GetService()
    {
        DataTable dt = new DataTable();
        try
        {
            if (_StateValue == ConnectionStatus.Open)
            {
                DBOperation db = new DBOperation();
                SqlCommand cmd = new SqlCommand();

                dt = db.FillTableProc(cmd, "USP_Service");
            }
        }
        catch (Exception ex)
        {
            dt = null;
            return null;
        }
        finally
        {
            if (_cnSQL.State == ConnectionState.Open)
                _cnSQL.Close();

        }

        return dt;
    }

    public static List<T> ConvertDataTable<T>(DataTable dt)
    {
        List<T> data = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            T item = GetItem<T>(row);
            data.Add(item);
        }
        return data;
    }
    public static T GetItem<T>(DataRow dr)
    {
        Type temp = typeof(T);
        T obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dr.Table.Columns)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (pro.Name == column.ColumnName)
                    if (obj == null)
                    {
                        pro.SetValue("", dr[column.ColumnName] == DBNull.Value ? null : dr[column.ColumnName], null);
                    }
                    else
                    {
                        pro.SetValue(obj, dr[column.ColumnName] == DBNull.Value ? null : dr[column.ColumnName], null);
                    }
                else
                    continue;
            }
        }
        return obj;
    }
    public DataTable GetLead(clsLead cls)
    {
        DataTable dt = new DataTable();
        try
        {
            if (_StateValue == ConnectionStatus.Open)
            {
                DBOperation db = new DBOperation();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OptType", SqlDbType.Int).Value = cls.OptType;
                cmd.Parameters.Add("@Leadid", SqlDbType.Int).Value = cls.LeadId;
                dt = db.FillTableProc(cmd, "USP_Lead");
            }
        }
        catch (Exception ex)
        {
            dt = null;
            return null;
        }
        finally
        {
            if (_cnSQL.State == ConnectionState.Open)
                _cnSQL.Close();

        }

        return dt;
    }



    public DataTable GetUser(clsUser cls)
    {
        DataTable dt = new DataTable();
        try
        {
            if (_StateValue == ConnectionStatus.Open)
            {
                DBOperation db = new DBOperation();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OptType", SqlDbType.Int).Value = cls.OptType;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = cls.UserId;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = cls.UserName;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = cls.Password;
                cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = cls.ContactNo;
                cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = cls.EmailAddress;
                cmd.Parameters.Add("@Userpic", SqlDbType.VarChar).Value = cls.Userpic;
                cmd.Parameters.Add("@UserPicExt", SqlDbType.VarChar).Value = cls.UserPicExt;
                cmd.Parameters.Add("@UserPicName", SqlDbType.VarChar).Value = cls.UserPicName;
                cmd.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = cls.IsActive;
                dt = db.FillTableProc(cmd, "USP_USER");
            }
        }
        catch (Exception ex)
        {
            dt = null;
            return null;
        }
        finally
        {
            if (_cnSQL.State == ConnectionState.Open)
                _cnSQL.Close();

        }

        return dt;
    }
    public static DataTable GetStatus(clsStatusMaster cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@StatusId", cls.StatusID);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", cls.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", cls.UpdatedBy);
                sqlCommand.Parameters.AddWithValue("@CompanyID", cls.CompanyID);
                sqlCommand.Parameters.AddWithValue("@Status", cls.Status);
                sqlCommand.Parameters.AddWithValue("@StatusDesc", cls.StatusDesc);
                sqlCommand.Parameters.AddWithValue("@IsDelete", cls.IsDelete);
                dt = db.FillTableProc(sqlCommand, "USP_Status");
            }


        }
        return dt;
    }


    public static DataTable GetLeadGeneration(clsLeadGenerationMaster cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@LeadId", cls.LeadId);
                sqlCommand.Parameters.AddWithValue("@MainProdId", cls.MainProductId);
                sqlCommand.Parameters.AddWithValue("@ProdId", cls.ProductId);
                sqlCommand.Parameters.AddWithValue("@ReqLoanAmt", cls.ReuestedLoanAmount);
                sqlCommand.Parameters.AddWithValue("@EstValueViechle", cls.EstValueViechle);
                sqlCommand.Parameters.AddWithValue("@ReqLoanTenure", cls.ReuestedLoanTenure);

                sqlCommand.Parameters.AddWithValue("@EstMonthIncome", cls.EstMonthIncome);
                sqlCommand.Parameters.AddWithValue("@EstFamilyIncome", cls.EstFamilyIncome);
                sqlCommand.Parameters.AddWithValue("@EstMonthExpense", cls.EstMonthExpense);
                sqlCommand.Parameters.AddWithValue("@CurMonthObligation", cls.CurMonthObligation);
                sqlCommand.Parameters.AddWithValue("@FORecomedAmt", cls.FORecomedAmt);
                sqlCommand.Parameters.AddWithValue("@NoofDependent", cls.NoofDependent);

                sqlCommand.Parameters.AddWithValue("@ViechleNo", cls.ViechleNo);
                sqlCommand.Parameters.AddWithValue("@ViechleRegYear", cls.ViechleRegYear);
                sqlCommand.Parameters.AddWithValue("@MFGYear", cls.MFGYear);
                sqlCommand.Parameters.AddWithValue("@ViechleModel", cls.ViechleModel);
                sqlCommand.Parameters.AddWithValue("@ViechleColor", cls.ViechleColor);

                sqlCommand.Parameters.AddWithValue("@ViechleCompany", cls.ViechleCompany);
                sqlCommand.Parameters.AddWithValue("@ViechleOwner", cls.ViechleOwner);
                sqlCommand.Parameters.AddWithValue("@RefernceName", cls.RefernceName);
                sqlCommand.Parameters.AddWithValue("@RefenceMobileNo", cls.RefenceMobileNo);
                sqlCommand.Parameters.AddWithValue("@RefenceRelation", cls.RefenceRelation);
                sqlCommand.Parameters.AddWithValue("@RefernceName1", cls.RefernceName1);
                sqlCommand.Parameters.AddWithValue("@RefenceMobileNo1", cls.RefenceMobileNo1);
                sqlCommand.Parameters.AddWithValue("@RefenceRelation1", cls.RefenceRelation1);

                sqlCommand.Parameters.AddWithValue("@DSAId", cls.DSAId);

                sqlCommand.Parameters.AddWithValue("@LoanPurpose", cls.LoanPurpose);
                sqlCommand.Parameters.AddWithValue("@ColletralSecurityType", cls.ColletralSecurityType);
                sqlCommand.Parameters.AddWithValue("@GeoTagging", "");
                sqlCommand.Parameters.AddWithValue("@FORemarks", "");
                sqlCommand.Parameters.AddWithValue("@BranchId", cls.BranchID);
                sqlCommand.Parameters.AddWithValue("@CompanyID", cls.CompanyId);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", cls.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", cls.UpdatedBy);
                sqlCommand.Parameters.AddWithValue("@CenterID", cls.CenterId);
                sqlCommand.Parameters.AddWithValue("@PLLoanBranch", cls.PLLoanBranch);
                sqlCommand.Parameters.AddWithValue("@UserRemarks", cls.UserRemarks);
                sqlCommand.Parameters.AddWithValue("@FuelType", cls.FuelType);
                sqlCommand.Parameters.AddWithValue("@RegistrationDate", cls.RegistrationDate);
                sqlCommand.Parameters.AddWithValue("@Insurer", cls.Insurer);
                sqlCommand.Parameters.AddWithValue("@PolicyNo", cls.PolicyNo);
                sqlCommand.Parameters.AddWithValue("@Owner", cls.Owner);
                sqlCommand.Parameters.AddWithValue("@InsuranceStatus", cls.InsuranceStatus);
                sqlCommand.Parameters.AddWithValue("@InsuranceType", cls.InsuranceType);
                sqlCommand.Parameters.AddWithValue("@ValidityDate", cls.ValidityDate);
                sqlCommand.Parameters.AddWithValue("@ExShowRoomPrice", cls.ExShowRoomPrice);
                sqlCommand.Parameters.AddWithValue("@OnRoadPrice", cls.OnRoadPrice);
                sqlCommand.Parameters.AddWithValue("@EstValueofscurity", cls.EstValueofscurity);
                sqlCommand.Parameters.AddWithValue("@Propertyarea", cls.Propertyarea);
                sqlCommand.Parameters.AddWithValue("@PropertyType", cls.PropertyType);
                sqlCommand.Parameters.AddWithValue("@PropertyAddress", cls.PropertyAddress);
                sqlCommand.Parameters.AddWithValue("@ERikshawMaker", cls.ERikshawMaker);
                sqlCommand.Parameters.AddWithValue("@PerformaInvoice", cls.PerformaInvoice);
                sqlCommand.Parameters.AddWithValue("@RepaymentType", cls.RepaymentType);
                sqlCommand.Parameters.AddWithValue("@InsEndValidityDate", cls.InsEndValidityDate);
                dt = db.FillTableProc(sqlCommand, "USP_Lead");
            }


        }
        return dt;
    }




    public static DataTable GetLeadGenerationCustomer(clsLeadGenerationMaster cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@Cif", cls.CIFID);
                sqlCommand.Parameters.AddWithValue("@FirstName", cls.FirstName);
                sqlCommand.Parameters.AddWithValue("@MiddleName", cls.MiddleName);
                sqlCommand.Parameters.AddWithValue("@LastName", cls.LastName);
                sqlCommand.Parameters.AddWithValue("@Custtype", "C");

                sqlCommand.Parameters.AddWithValue("@FatherName", cls.FatherName);
                sqlCommand.Parameters.AddWithValue("@MotherName", cls.MotherName);
                sqlCommand.Parameters.AddWithValue("@SpouseName", cls.SpouseName);
                sqlCommand.Parameters.AddWithValue("@Gender", cls.Gender);


                
                sqlCommand.Parameters.AddWithValue("@DateofBirth", cls.DateofBirth);
                sqlCommand.Parameters.AddWithValue("@MartialStatus", cls.MartialStatus);
                sqlCommand.Parameters.AddWithValue("@PresentAddress", cls.PresentAddress);
                sqlCommand.Parameters.AddWithValue("@PresentPincode", cls.PresentPincode);

                sqlCommand.Parameters.AddWithValue("@PermanentAddress", cls.PermanentAddress);
                sqlCommand.Parameters.AddWithValue("@PermanentPincode", cls.PermanentPincode);

                sqlCommand.Parameters.AddWithValue("@CibilScore", cls.CibilScore);
                sqlCommand.Parameters.AddWithValue("@PresentStateId", cls.PresentStateId);
                sqlCommand.Parameters.AddWithValue("@PresentCityId",cls.PresentCityId);
                sqlCommand.Parameters.AddWithValue("@PermanentStateId", cls.PermanentStateId);
                sqlCommand.Parameters.AddWithValue("@PermanentCityId",cls.PermanentCityId);

                sqlCommand.Parameters.AddWithValue("@MobileNo1", cls.MobileNo1);
                sqlCommand.Parameters.AddWithValue("@MobileNo2", cls.MobileNo2);
                sqlCommand.Parameters.AddWithValue("@FatherMobileNo", cls.FatherMobileNo);
                sqlCommand.Parameters.AddWithValue("@MotherMobileNo", cls.MotherMobileNo);
                sqlCommand.Parameters.AddWithValue("@SpouseMobileNo", cls.SpouseMobileNo);
                sqlCommand.Parameters.AddWithValue("@IsLoanDisbursed", Convert.ToInt32(0));


                sqlCommand.Parameters.AddWithValue("@AadharNo", cls.AadharNo);
                sqlCommand.Parameters.AddWithValue("@AAdharverfiy", cls.AAdharverfiy);
                sqlCommand.Parameters.AddWithValue("@PanNo", cls.PanNo);
                sqlCommand.Parameters.AddWithValue("@PanVerify", cls.PanVerify);
                sqlCommand.Parameters.AddWithValue("@CustRelation", cls.CustRelation);
                sqlCommand.Parameters.AddWithValue("@BranchID", cls.BranchID);

                sqlCommand.Parameters.AddWithValue("@LeadID", cls.LeadId);
                sqlCommand.Parameters.AddWithValue("@CompanyId", cls.CompanyId);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", cls.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@CustImage", cls.CustImage);
                sqlCommand.Parameters.AddWithValue("@ElectricBill", cls.ElectricBill);
                sqlCommand.Parameters.AddWithValue("@OwnerShip", cls.OwnerShip);
                sqlCommand.Parameters.AddWithValue("@PrefixName", cls.Prefix);
                sqlCommand.Parameters.AddWithValue("@PresentDistrict", cls.PresentDistrict);
                sqlCommand.Parameters.AddWithValue("@PresentVillage", cls.PresentVillage);
                sqlCommand.Parameters.AddWithValue("@PermanentDistrict", cls.PermanentDistrict);
                sqlCommand.Parameters.AddWithValue("@PermanentVillage", cls.PermanentVillage);
                sqlCommand.Parameters.AddWithValue("@EmailId", cls.EmailId);
                sqlCommand.Parameters.AddWithValue("@LandMark", cls.CustLandMark);

                dt = db.FillTableProc(sqlCommand, "USP_LeadCustomer");
            }


        }
        return dt;
    }



    public static DataTable GetLeadGenerationCO_ApplicantCustomer(clsLeadGenerationMaster cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@Cif", cls.Co_CIF);
                sqlCommand.Parameters.AddWithValue("@FirstName", cls.CO_FirstName);
                sqlCommand.Parameters.AddWithValue("@MiddleName", cls.CO_MiddleName);
                sqlCommand.Parameters.AddWithValue("@LastName", cls.CO_LastName);
                sqlCommand.Parameters.AddWithValue("@Custtype", "CO_Applicant");

                sqlCommand.Parameters.AddWithValue("@FatherName", cls.CO_FatherName);
                sqlCommand.Parameters.AddWithValue("@MotherName", cls.CO_MotherName);
                sqlCommand.Parameters.AddWithValue("@SpouseName", "");
                sqlCommand.Parameters.AddWithValue("@Gender", cls.CO_Gender);


                sqlCommand.Parameters.AddWithValue("@DateofBirth", cls.CO_DOB);
                sqlCommand.Parameters.AddWithValue("@MartialStatus", cls.CO_Marital_Status);
                sqlCommand.Parameters.AddWithValue("@PresentAddress", cls.CO_PresentAddress);
                sqlCommand.Parameters.AddWithValue("@PresentPincode", cls.CO_PresentPinCode);

                sqlCommand.Parameters.AddWithValue("@PermanentAddress", cls.CO_PermanentAddress);
                sqlCommand.Parameters.AddWithValue("@PermanentPincode", cls.CO_PermanentPincode);

                sqlCommand.Parameters.AddWithValue("@CibilScore", cls.CO_CIBIL);
                sqlCommand.Parameters.AddWithValue("@PresentStateId", cls.CO_PresentStateId);
                sqlCommand.Parameters.AddWithValue("@PresentCityId",cls.CO_PresentCityId);
                sqlCommand.Parameters.AddWithValue("@PermanentStateId", cls.CO_PresentStateId);
                sqlCommand.Parameters.AddWithValue("@PermanentCityId", cls.CO_PermanentCityId);

                sqlCommand.Parameters.AddWithValue("@MobileNo1", cls.CO_Mobile_No);
                sqlCommand.Parameters.AddWithValue("@MobileNo2", "");
                sqlCommand.Parameters.AddWithValue("@FatherMobileNo", "");
                sqlCommand.Parameters.AddWithValue("@MotherMobileNo", "");
                sqlCommand.Parameters.AddWithValue("@SpouseMobileNo", "");
                sqlCommand.Parameters.AddWithValue("@IsLoanDisbursed", Convert.ToInt32(0));


                sqlCommand.Parameters.AddWithValue("@AadharNo", cls.CO_Adhaar);
                sqlCommand.Parameters.AddWithValue("@AAdharverfiy", Convert.ToInt32(cls.CO_AAdharverfiy));
                sqlCommand.Parameters.AddWithValue("@PanNo", cls.CO_PAN);
                sqlCommand.Parameters.AddWithValue("@PanVerify", Convert.ToInt32(cls.CO_Panverfiy));
                sqlCommand.Parameters.AddWithValue("@CustRelation", "");
                sqlCommand.Parameters.AddWithValue("@BranchID", cls.BranchID);

                sqlCommand.Parameters.AddWithValue("@LeadID", cls.LeadId);
                sqlCommand.Parameters.AddWithValue("@CompanyId", cls.CompanyId);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", cls.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@CustImage", cls.CustImage);
                sqlCommand.Parameters.AddWithValue("@ElectricBill", cls.ElectricBill);
                sqlCommand.Parameters.AddWithValue("@OwnerShip", cls.Co_OwnerShip);
                sqlCommand.Parameters.AddWithValue("@PrefixName", cls.CO_Prefix);
                sqlCommand.Parameters.AddWithValue("@PresentDistrict", cls.CO_PresentDistrict);
                sqlCommand.Parameters.AddWithValue("@PresentVillage", cls.CO_PresentVillage);
                sqlCommand.Parameters.AddWithValue("@PermanentDistrict", cls.CO_PermanentDistrict);
                sqlCommand.Parameters.AddWithValue("@PermanentVillage", cls.CO_PermanentVillage);
                sqlCommand.Parameters.AddWithValue("@EmailId", cls.CO_Email_Id);
                sqlCommand.Parameters.AddWithValue("@LandMark", cls.Co_LandMark);

                dt = db.FillTableProc(sqlCommand, "USP_LeadCustomer");
            }


        }
        return dt;
    }


    public static DataTable GetLeadGenerationGurantorCustomer(Gurantor cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", "Insert");
                sqlCommand.Parameters.AddWithValue("@Cif", cls.G_CIF);
                sqlCommand.Parameters.AddWithValue("@FirstName", cls.G_FirstName);
                sqlCommand.Parameters.AddWithValue("@MiddleName", cls.G_MiddleName);
                sqlCommand.Parameters.AddWithValue("@LastName", cls.G_LastName);
                sqlCommand.Parameters.AddWithValue("@Custtype", "Gurantor");

                sqlCommand.Parameters.AddWithValue("@FatherName", cls.G_FatherName);
                sqlCommand.Parameters.AddWithValue("@MotherName", "");
                sqlCommand.Parameters.AddWithValue("@SpouseName", cls.G_SpouseName);
                sqlCommand.Parameters.AddWithValue("@Gender", cls.G_Gender);


                sqlCommand.Parameters.AddWithValue("@DateofBirth", cls.G_DOB);
                sqlCommand.Parameters.AddWithValue("@MartialStatus", cls.G_Marital_Status);
                sqlCommand.Parameters.AddWithValue("@PresentAddress", cls.G_PresentAddress);
                sqlCommand.Parameters.AddWithValue("@PresentPincode", cls.G_PresentPinCode);

                sqlCommand.Parameters.AddWithValue("@PermanentAddress", cls.G_PermanentAddress);
                sqlCommand.Parameters.AddWithValue("@PermanentPincode", cls.G_PermanentPincode);

                sqlCommand.Parameters.AddWithValue("@CibilScore", cls.G_CibilScore);
                sqlCommand.Parameters.AddWithValue("@PresentStateId", cls.G_PresentStateId);
                sqlCommand.Parameters.AddWithValue("@PresentCityId", cls.G_PresentCityId);
                sqlCommand.Parameters.AddWithValue("@PermanentStateId", cls.G_P_State);
                sqlCommand.Parameters.AddWithValue("@PermanentCityId",cls.G_P_City);

                sqlCommand.Parameters.AddWithValue("@MobileNo1", cls.G_Mobile_No);
                sqlCommand.Parameters.AddWithValue("@MobileNo2", "");
                sqlCommand.Parameters.AddWithValue("@FatherMobileNo", "");
                sqlCommand.Parameters.AddWithValue("@MotherMobileNo", "");
                sqlCommand.Parameters.AddWithValue("@SpouseMobileNo", "");
                sqlCommand.Parameters.AddWithValue("@EmailId", cls.G_EmailId);


                sqlCommand.Parameters.AddWithValue("@AadharNo", cls.G_AadharNo);
                sqlCommand.Parameters.AddWithValue("@AAdharverfiy", Convert.ToInt32(cls.G_AadharVerify));
                sqlCommand.Parameters.AddWithValue("@PanNo", cls.G_PanNo);
                sqlCommand.Parameters.AddWithValue("@PanVerify", Convert.ToInt32(cls.G_PanVerify));
                sqlCommand.Parameters.AddWithValue("@CustRelation", "");
                sqlCommand.Parameters.AddWithValue("@BranchID", cls.G_BranchID);

                sqlCommand.Parameters.AddWithValue("@LeadID", cls.G_LeadId);
                sqlCommand.Parameters.AddWithValue("@CompanyId", cls.G_CompanyId);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", cls.G_CreatedBy);
                sqlCommand.Parameters.AddWithValue("@OwnerShip", cls.G_OwnerShip);
                sqlCommand.Parameters.AddWithValue("@PrefixName", cls.G_PrefixName);
                sqlCommand.Parameters.AddWithValue("@PresentDistrict", cls.G_PresentDistrict);
                sqlCommand.Parameters.AddWithValue("@PresentVillage", cls.G_PresentVillage);
                sqlCommand.Parameters.AddWithValue("@PermanentDistrict", cls.G_PermanentDistrict);
                sqlCommand.Parameters.AddWithValue("@PermanentVillage", cls.G_PermanentVillage);
                sqlCommand.Parameters.AddWithValue("@LandMark", cls.G_LandMark);
                dt = db.FillTableProc(sqlCommand, "USP_LeadCustomer");
            }


        }
        return dt;
    }


    public DataTable GetTicket(clsTicket cls)
    {
        DataTable dt = new DataTable();
        try
        {
            if (_StateValue == ConnectionStatus.Open)
            {
                DBOperation db = new DBOperation();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@OptType", SqlDbType.Int).Value = cls.OptType;
                cmd.Parameters.Add("@TicketId", SqlDbType.Int).Value = cls.TicketId;
                cmd.Parameters.Add("@TICKETNO", SqlDbType.VarChar).Value = cls.TicketNo;
                cmd.Parameters.Add("@fromdate", SqlDbType.VarChar).Value = cls.FromDate;
                cmd.Parameters.Add("@Todate", SqlDbType.VarChar).Value = cls.ToDate;
                dt = db.FillTableProc(cmd, "USP_GETTICKET");
                db = null;
                cmd.Dispose();
            }
        }
        catch (Exception ex)
        {
            dt = null;
            return null;
        }
        finally
        {
            if (_cnSQL.State == ConnectionState.Open)
                _cnSQL.Close();

        }

        return dt;
    }
    ~DataInterface()
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

    public static void PostError(clsError cls)
    {

        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@PageName", cls.PageName);
                sqlCommand.Parameters.AddWithValue("@Link", cls.Link);
                sqlCommand.Parameters.AddWithValue("@Mode", cls.Mode);
                sqlCommand.Parameters.AddWithValue("@FunctionName", cls.FunctionName);
                sqlCommand.Parameters.AddWithValue("@ErrorDescrption", cls.ErrorDescrption);
                sqlCommand.Parameters.AddWithValue("@UserId", cls.UserId);
                DataTable dt = new DataTable();
                dt = db.FillTableProc(sqlCommand, "USP_error");
            }


        }
    }

    public static DataTable DBCompany(clsCompanyMaster cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@CompanyID", cls.CompanyId);
                sqlCommand.Parameters.AddWithValue("@CompanyName", cls.CompanyName);
                sqlCommand.Parameters.AddWithValue("@Address", cls.Address);
                sqlCommand.Parameters.AddWithValue("@City", cls.City);
                sqlCommand.Parameters.AddWithValue("@State", cls.State);
                sqlCommand.Parameters.AddWithValue("@Country", cls.Country);
                sqlCommand.Parameters.AddWithValue("@Pincode", cls.PinCode);
                sqlCommand.Parameters.AddWithValue("@PANNo", cls.PANNo);
                sqlCommand.Parameters.AddWithValue("@GSTNo", cls.GSTNo);
                sqlCommand.Parameters.AddWithValue("@CompanyType", cls.CompanyType);
                sqlCommand.Parameters.AddWithValue("@CompanyDesc", cls.CompanyDesc);
                sqlCommand.Parameters.AddWithValue("@CompanyOthDesc", cls.CompanyOthDesc);
                sqlCommand.Parameters.AddWithValue("@Logo", cls.LOGO);
                sqlCommand.Parameters.AddWithValue("@CINNo", cls.CINNo);
                sqlCommand.Parameters.AddWithValue("@DateofIncorporation", cls.DateofIncorporation);
                sqlCommand.Parameters.AddWithValue("@RBIRegd", cls.RBIRegd);
                sqlCommand.Parameters.AddWithValue("@Emailid", cls.EmailId);
                sqlCommand.Parameters.AddWithValue("@Website", cls.Website);
                sqlCommand.Parameters.AddWithValue("@MobileNo", cls.MobileNo);


                dt = db.FillTableProc(sqlCommand, "USP_Company");

            }


        }
        return dt;
    }

    public static DataTable DBCustomer(clsCustomerMaster cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@CompanyID", cls.CompanyId);
                sqlCommand.Parameters.AddWithValue("@FirstName", cls.FirstName);
                sqlCommand.Parameters.AddWithValue("@MiddleName", cls.MiddleName);
                sqlCommand.Parameters.AddWithValue("@LastName", cls.LastName);
                sqlCommand.Parameters.AddWithValue("@FatherName", cls.FatherName);
                sqlCommand.Parameters.AddWithValue("@MotherName", cls.MotherName);
                sqlCommand.Parameters.AddWithValue("@SpouseName", cls.SpouseName);
                sqlCommand.Parameters.AddWithValue("@Gender", cls.Gender);
                sqlCommand.Parameters.AddWithValue("@DateofBirth", cls.Dob);
                sqlCommand.Parameters.AddWithValue("@MartialStatus", cls.MaterialStatus);
                sqlCommand.Parameters.AddWithValue("@PresentAddress", cls.PresentAddress);
                sqlCommand.Parameters.AddWithValue("@PresentPincode", cls.PresentPinCode);
                sqlCommand.Parameters.AddWithValue("@PermanentAddress", cls.PermanentAddress);
                sqlCommand.Parameters.AddWithValue("@PermanentPincode", cls.PermanentPincode);
                sqlCommand.Parameters.AddWithValue("@CibilScore", cls.CibilScore);
                sqlCommand.Parameters.AddWithValue("@PresentStateId", cls.PresentStateId);
                sqlCommand.Parameters.AddWithValue("@PresentCityId", cls.PresentCityId);
                sqlCommand.Parameters.AddWithValue("@PermanentStateId", cls.PermanentStateId);
                sqlCommand.Parameters.AddWithValue("@PermanentCityId", cls.PermanentCityId);
                sqlCommand.Parameters.AddWithValue("@MobileNo1", cls.MobileNo1);
                sqlCommand.Parameters.AddWithValue("@MobileNo2", cls.MobileNo2);
                sqlCommand.Parameters.AddWithValue("@FatherMobileNo", cls.FatherMobileNo);
                sqlCommand.Parameters.AddWithValue("@MotherMobileNo", cls.MotherMobileNo);
                sqlCommand.Parameters.AddWithValue("@SpouseMobileNo", cls.SpouseMobileNo);
                sqlCommand.Parameters.AddWithValue("@AadharNo", cls.AadharNo);
                sqlCommand.Parameters.AddWithValue("@PanNo", cls.PanNo);
                dt = db.FillTableProc(sqlCommand, "USP_CUSTOMER");

            }


        }
        return dt;
    }
    public static DataTable DBDSAMaster(clsDSAMaster cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@DSAId", cls.DSAId);
                sqlCommand.Parameters.AddWithValue("@DSACode", cls.DSACode);
                sqlCommand.Parameters.AddWithValue("@DSAName", cls.DSAName);
                sqlCommand.Parameters.AddWithValue("@DSAAddress", cls.DSAAddress);
                sqlCommand.Parameters.AddWithValue("@DSACityId", cls.DSACityId);
                sqlCommand.Parameters.AddWithValue("@DSAStateId", cls.DSAStateId);
                sqlCommand.Parameters.AddWithValue("@DSAPincode", cls.DSAPincode);
                sqlCommand.Parameters.AddWithValue("@DSAContactNo", cls.DSAContactNo);
                sqlCommand.Parameters.AddWithValue("@DSAWhatsUpNo", cls.DSAWhatsUpNo);
                sqlCommand.Parameters.AddWithValue("@DSACommision", cls.DSACommision);
                sqlCommand.Parameters.AddWithValue("@DSAEmail", cls.DSAEmail);
                sqlCommand.Parameters.AddWithValue("@DSAGSTNo", cls.DSAGSTNo);
                sqlCommand.Parameters.AddWithValue("@DSARemarks", cls.DSARemarks);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", cls.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", cls.UpdatedBy);
                sqlCommand.Parameters.AddWithValue("@ISDELETE", cls.ISDELETE);
                sqlCommand.Parameters.AddWithValue("@COMPANYID", cls.COMPANYID);
                sqlCommand.Parameters.AddWithValue("@DSABranch", cls.DSABranch);
                sqlCommand.Parameters.AddWithValue("@DSAccountNo", cls.DSAccountNo);
                sqlCommand.Parameters.AddWithValue("@DSABankName", cls.DSABankName);
                sqlCommand.Parameters.AddWithValue("@DSAIFSCCode", cls.DSAIFSCCode);
                sqlCommand.Parameters.AddWithValue("@PAN", cls.PAN);
                sqlCommand.Parameters.AddWithValue("@AadharNo", cls.AAdharNo);
                dt = db.FillTableProc(sqlCommand, "USP_DSAMaster");

            }


        }
        return dt;
    }

    public static DataTable DBQuestionMaster(clsQuestion cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@QuestionId", cls.QuestionId);
                sqlCommand.Parameters.AddWithValue("@MainProdId", cls.MainProdId);
                sqlCommand.Parameters.AddWithValue("@ProdId", cls.ProdId);
                sqlCommand.Parameters.AddWithValue("@QuestionAnsType", cls.QuestionAnsType);
                sqlCommand.Parameters.AddWithValue("@Question", cls.Question);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", cls.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@ISDELETE", cls.IsDelete);
                sqlCommand.Parameters.AddWithValue("@CompanyId", cls.CompanyId);
                dt = db.FillTableProc(sqlCommand, "USP_Question");

            }


        }
        return dt;
    }
    public static DataTable DBLogin(clsLogin cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@UserID", cls.UserID);
                sqlCommand.Parameters.AddWithValue("@UserName", cls.UserName);
                sqlCommand.Parameters.AddWithValue("@Password", cls.UserPassword);
                sqlCommand.Parameters.AddWithValue("@CompId", cls.Compid);
                sqlCommand.Parameters.AddWithValue("@SessionID", cls.SessionID);
                sqlCommand.Parameters.AddWithValue("@DeviceId", cls.DeviceID);
                sqlCommand.Parameters.AddWithValue("@DeviceType", cls.DeviceType);
                sqlCommand.Parameters.AddWithValue("@IsLogged", cls.IsLogged);
                sqlCommand.Parameters.AddWithValue("@DeviceToken", cls.DeviceToken);
                dt = db.FillTableProc(sqlCommand, "USP_Login");
            }


        }
        return dt;
    }






    public static DataTable GetLeadGenerationData(clsLeadGenerationMaster cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@leadid", cls.LeadId);
                sqlCommand.Parameters.AddWithValue("@LeadNo", cls.LeadNo);
                sqlCommand.Parameters.AddWithValue("@CompanyID", cls.CompanyId);
                sqlCommand.Parameters.AddWithValue("@EMPId", cls.Empid);
                sqlCommand.Parameters.AddWithValue("@ShortStage_Name", cls.ShortStage_Name);
                sqlCommand.Parameters.AddWithValue("@StageEmpId", cls.StageEmpId);
                sqlCommand.Parameters.AddWithValue("@MainProdId", cls.MainProductId);
                sqlCommand.Parameters.AddWithValue("@ProdId", cls.ProductId);
                sqlCommand.Parameters.AddWithValue("@CustomerName", cls.CustomerName);
                sqlCommand.Parameters.AddWithValue("@PanNo", cls.PanNo);
                sqlCommand.Parameters.AddWithValue("@AadharNo", cls.AadharNo);
                sqlCommand.Parameters.AddWithValue("@MobileNo", cls.MobileNo1);
                sqlCommand.Parameters.AddWithValue("@isdelete", cls.isdelete);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", cls.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@BranchId", cls.BranchID);

                dt = db.FillTableProc(sqlCommand, "USP_Lead");
            }


        }
        return dt;
    }




    public static DataSet GetLeadGenerationDataSingle(clsLeadGenerationMaster cls)
    {
        DataSet dt = new DataSet();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@leadid", cls.LeadId);
                sqlCommand.Parameters.AddWithValue("@LeadNo", cls.LeadNo);
                sqlCommand.Parameters.AddWithValue("@CompanyID", cls.CompanyId);

                dt = db.FillDsProc(sqlCommand, "USP_Lead");
            }


        }
        return dt;
    }
    public static DataTable DBRole(clsRoleMaster cls)
    {


        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@RoleId", cls.RoleId);
                sqlCommand.Parameters.AddWithValue("@RoleName", cls.RoleName);
                sqlCommand.Parameters.AddWithValue("@EmpId", cls.EmpId);
                sqlCommand.Parameters.AddWithValue("@EmpCode", cls.EmpCode);
                sqlCommand.Parameters.AddWithValue("@MenuId", cls.MenuId);
                sqlCommand.Parameters.AddWithValue("@SubMenuId", cls.SubMenuId);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", cls.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@CompanyId", cls.CompanyId);
                sqlCommand.Parameters.AddWithValue("@IsDelete", cls.IsDelete);
                dt = db.FillTableProc(sqlCommand, "USP_Role");
            }


        }
        return dt;
    }

    public static DataSet ViewDBRole(clsRoleMaster cls)
    {


        DataSet dt = new DataSet();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@RoleId", cls.RoleId);
                sqlCommand.Parameters.AddWithValue("@RoleName", cls.RoleName);
                sqlCommand.Parameters.AddWithValue("@EmpId", cls.EmpId);
                sqlCommand.Parameters.AddWithValue("@EmpCode", cls.EmpCode);
                sqlCommand.Parameters.AddWithValue("@MenuId", cls.MenuId);
                sqlCommand.Parameters.AddWithValue("@SubMenuId", cls.SubMenuId);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", cls.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@CompanyId", cls.CompanyId);
                sqlCommand.Parameters.AddWithValue("@IsDelete", cls.IsDelete);
                dt = db.FillDsProc(sqlCommand, "USP_Role");
            }


        }
        return dt;
    }
    public static DataSet DBLetter(clsLeadGenerationMaster cls)
    {


        DataSet ds = new DataSet();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@LeadId", cls.LeadId);
                sqlCommand.Parameters.AddWithValue("@LeadNo", cls.LeadNo);
                ds = db.FillDsProc(sqlCommand, "USP_CustomerLetter");
            }


        }
        return ds;
    }

    public static DataTable DBDashBoard(clsLeadGenerationMaster cls)
    {


        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@EMPID", cls.Empid);
                sqlCommand.Parameters.AddWithValue("@Branchid", cls.BranchID);
                sqlCommand.Parameters.AddWithValue("@CompanyId", cls.CompanyId);
                sqlCommand.Parameters.AddWithValue("@Fromdate", cls.FromDate);
                sqlCommand.Parameters.AddWithValue("@ToDate", cls.ToDate);
                dt = db.FillTableProc(sqlCommand, "USP_Dashboard");
            }


        }
        return dt;
    }

}
