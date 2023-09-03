using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Sunnet_NBFC.Models;
using Sunnet_NBFC.App_Code;

public class DataInterface2 : DataInterface, IDisposable
{
    public SqlConnection _cnSQL = null;
    public ConnectionStatus _StateValue = ConnectionStatus.Unknown;

    //private static errormessage="Error Occurred";

    /// <summary>
    /// Enumeration representing the state of the SQL connection.
    /// </summary>

    /// <summary>
    /// State of the SQL connection.
    /// </summary>

    /// <summary>
    /// A description of the state of the connection or command.
    /// </summary>

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

    //--------------------------Branch Master-----------------------

    #region Save Branch
    public static ClsReturnData SaveBranch(clsBranch cls)
    {
        ClsReturnData clsRtn = new ClsReturnData();
        clsRtn.MsgType = (int)MessageType.Fail;
        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;

            cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = cls.BranchId;
            cmd.Parameters.Add("@BranchCode", SqlDbType.VarChar).Value = cls.BranchCode;
            cmd.Parameters.Add("@BranchName", SqlDbType.VarChar).Value = cls.BranchName;
            cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
            cmd.Parameters.Add("@BranchAddres", SqlDbType.VarChar).Value = cls.BranchAddres;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = cls.CreatedBy;
            cmd.Parameters.Add("@UpdateBy", SqlDbType.Int).Value = cls.UpdateBy;
            cmd.Parameters.Add("@BranchContactNo", SqlDbType.VarChar).Value = cls.BranchContactNo;
            cmd.Parameters.Add("@IsDelete", SqlDbType.VarChar).Value = cls.IsDelete;
            cmd.Parameters.Add("@CityId", SqlDbType.Int).Value = cls.CityId;
            cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = cls.StateId;
            cmd.Parameters.Add("@BranchManger", SqlDbType.VarChar).Value = cls.BranchManger;

            cmd.Parameters.AddWithValue("@RentAgrementStartDate", cls.RentAgrementStartDate);
            cmd.Parameters.AddWithValue("@RentAgrimentEndDate", cls.RentAgrimentEndDate);
            cmd.Parameters.AddWithValue("@BranchRent", cls.BranchRent);
            cmd.Parameters.AddWithValue("@OwnerName", cls.OwnerName);
            dt = db.FillTableProc(cmd, "USP_Branch");

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
            //    }
            //}

        }
        catch (Exception ex)
        {
            clsRtn.MsgType = (int)MessageType.Error;
            clsRtn.ID = 0;
            clsRtn.Message = ex.Message;
            clsRtn.MessageDesc = ex.Message;
        }
        finally
        {
            dt.Dispose();
        }

        return clsRtn;
    }

    #endregion


    #region View Branch
    public static DataTable ViewBranch(clsBranch cls)
    {
        //DBHelper dBHlper = new DBHelper();
        List<clsBranch> lst = new List<clsBranch>();
        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;
            cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = cls.BranchId;
            cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
            cmd.Parameters.Add("@IsDelete", SqlDbType.VarChar).Value = 0;
            dt = db.FillTableProc(cmd, "USP_Branch");

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt.Dispose();
        }

        return dt;
    }
    #endregion

    #region Delete Branch
    public static ClsReturnData DeleteBranch(int Id)
    {
        ClsReturnData clsRtn = new ClsReturnData();
        clsRtn.MsgType = (int)MessageType.Fail;
        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "Delete";
            cmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = Id;
            cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
            dt = db.FillTableProc(cmd, "USP_Branch");

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
            //    }
            //}

        }
        catch (Exception ex)
        {
            clsRtn.MsgType = (int)MessageType.Error;
            clsRtn.ID = 0;
            clsRtn.Message = ex.Message;
            clsRtn.MessageDesc = ex.Message;
        }
        finally
        {
            dt.Dispose();
        }

        return clsRtn;
    }
    #endregion

    //--------------------------Branch Master-----------------------


    //--------------------------Misc Master-----------------------

    #region Save Misc
    public static ClsReturnData SaveMisc(clsMisc cls)
    {
        ClsReturnData clsRtn = new ClsReturnData();
        clsRtn.MsgType = (int)MessageType.Fail;
        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;
            cmd.Parameters.Add("@MiscId", SqlDbType.Int).Value = cls.MiscId;
            cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
            cmd.Parameters.Add("@MiscName", SqlDbType.VarChar).Value = cls.MiscName;
            cmd.Parameters.Add("@MiscType", SqlDbType.VarChar).Value = cls.MiscType;
            cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = cls.IsDelete;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = ClsSession.UserID;// cls.CreatedBy ?? 0;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = ClsSession.UserID;// cls.UpdatedBy ?? 0;
            dt = db.FillTableProc(cmd, "USP_MiscMaster");

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
            //    }
            //}

        }
        catch (Exception ex)
        {
            clsRtn.MsgType = (int)MessageType.Error;
            clsRtn.ID = 0;
            clsRtn.Message = ex.Message;
            clsRtn.MessageDesc = ex.Message;
        }
        finally
        {
            dt.Dispose();
        }

        return clsRtn;
    }

    #endregion

    #region GET Misc
    public static clsMisc GetMisc(int MiscId)
    {
        clsMisc lst = new clsMisc();


        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "View";
            cmd.Parameters.Add("@MiscId", SqlDbType.Int).Value = MiscId;
            cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
            cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = 0;
            dt = db.FillTableProc(cmd, "USP_MiscMaster");
            //    }
            //}

            if (dt != null && dt.Rows.Count > 0)
                lst = GetItem<clsMisc>(dt.Rows[0]);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt.Dispose();
        }

        return lst;

    }

    #endregion

    #region View Misc
    public static List<clsMisc> ViewMisc()
    {
        //DBHelper dBHlper = new DBHelper();
        List<clsMisc> lst = new List<clsMisc>();
        DataTable dt = new DataTable();
        try
        {
            //DBOperation db = new DBOperation();
            //SqlCommand cmd = new SqlCommand();

            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "View";
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                    cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = 0;
                    dt = db.FillTableProc(cmd, "USP_MiscMaster");

                }
            }

            lst = DataInterface.ConvertDataTable<clsMisc>(dt);


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt.Dispose();
        }

        return lst;
    }
    #endregion

    #region Delete Misc
    public static ClsReturnData DeleteMisc(int Id)
    {
        ClsReturnData clsRtn = new ClsReturnData();
        clsRtn.MsgType = (int)MessageType.Fail;
        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "Delete";
            cmd.Parameters.Add("@MiscId", SqlDbType.Int).Value = Id;
            cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
            dt = db.FillTableProc(cmd, "USP_MiscMaster");

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
            //    }
            //}

        }
        catch (Exception ex)
        {
            clsRtn.MsgType = (int)MessageType.Error;
            clsRtn.ID = 0;
            clsRtn.Message = ex.Message;
            clsRtn.MessageDesc = ex.Message;
        }
        finally
        {
            dt.Dispose();
        }

        return clsRtn;
    }
    #endregion

    #region GEt Misc DDL
    public static DataTable GetMiscForDDL(string MiscType)
    {

        DataTable dt = new DataTable();
        try
        {
            //DBOperation db = new DBOperation();
            //SqlCommand cmd = new SqlCommand();

            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "View";
                    cmd.Parameters.Add("@MiscType", SqlDbType.VarChar).Value = MiscType;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                    cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = 0;
                    dt = db.FillTableProc(cmd, "USP_MiscMaster");

                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt.Dispose();
        }

        return dt;
    }
    #endregion


    //--------------------------Misc Master-----------------------


    //--------------------------Document Master-----------------------

    #region Save Document
    public static ClsReturnData SaveDocument(clsDocument cls)
    {
        ClsReturnData clsRtn = new ClsReturnData();
        clsRtn.MsgType = (int)MessageType.Fail;
        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;
            cmd.Parameters.Add("@DocId", SqlDbType.Int).Value = cls.DocID;
            cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
            cmd.Parameters.Add("@DocumentName", SqlDbType.VarChar).Value = cls.DocumentName;
            cmd.Parameters.Add("@ProdId", SqlDbType.Int).Value = cls.ProdID;
            cmd.Parameters.Add("@IsRequried", SqlDbType.Int).Value = (cls.IsRequried ? 1 : 0);
            cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = cls.IsDelete;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = ClsSession.UserID;// cls.CreatedBy ?? 0;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = ClsSession.UserID;// cls.UpdatedBy ?? 0;
            dt = db.FillTableProc(cmd, "USP_Document");

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
            //    }
            //}

        }
        catch (Exception ex)
        {
            clsRtn.MsgType = (int)MessageType.Error;
            clsRtn.ID = 0;
            clsRtn.Message = ex.Message;
            clsRtn.MessageDesc = ex.Message;
        }
        finally
        {
            dt.Dispose();
        }

        return clsRtn;
    }

    #endregion

    #region GET Document
    public static clsDocument GetDocument(int DocumentId)
    {
        clsDocument lst = new clsDocument();


        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "View";
            cmd.Parameters.Add("@DocId", SqlDbType.Int).Value = DocumentId;
            cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
            cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = 0;
            dt = db.FillTableProc(cmd, "USP_Document");
            //    }
            //}

            if (dt != null && dt.Rows.Count > 0)
                lst = GetItem<clsDocument>(dt.Rows[0]);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt.Dispose();
        }

        return lst;

    }

    #endregion

    #region View Document
    public static List<clsDocument> ViewDocument()
    {
        //DBHelper dBHlper = new DBHelper();
        List<clsDocument> lst = new List<clsDocument>();
        DataTable dt = new DataTable();
        try
        {
            //DBOperation db = new DBOperation();
            //SqlCommand cmd = new SqlCommand();

            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "View";
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                    cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = 0;
                    dt = db.FillTableProc(cmd, "USP_Document");

                }
            }

            lst = DataInterface.ConvertDataTable<clsDocument>(dt);


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt.Dispose();
        }

        return lst;
    }
    #endregion

    #region Delete clsDocument
    public static ClsReturnData DeleteDocument(int Id)
    {
        ClsReturnData clsRtn = new ClsReturnData();
        clsRtn.MsgType = (int)MessageType.Fail;
        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "Delete";
            cmd.Parameters.Add("@DocId", SqlDbType.Int).Value = Id;
            cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
            dt = db.FillTableProc(cmd, "USP_Document");

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
            //    }
            //}

        }
        catch (Exception ex)
        {
            clsRtn.MsgType = (int)MessageType.Error;
            clsRtn.ID = 0;
            clsRtn.Message = ex.Message;
            clsRtn.MessageDesc = ex.Message;
        }
        finally
        {
            dt.Dispose();
        }

        return clsRtn;
    }
    #endregion

    //--------------------------Document Master-----------------------

    //public static DataTable GetProduct()
    //{

    //    DataTable dt = new DataTable();
    //    using (DBOperation db = new DBOperation())
    //    {
    //        using (SqlCommand sqlCommand = new SqlCommand())
    //        {
    //            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
    //            sqlCommand.Parameters.AddWithValue("@ReqType", "View");
    //            sqlCommand.Parameters.AddWithValue("@CompanyId", ClsSession.CompanyID);
    //            dt = db.FillTableProc(sqlCommand, "USP_Product");
    //        }


    //    }
    //    return dt;
    //}


    //--------------------------Document Master-----------------------


    //--------------------------------------------------------------
    #region Delete clsChargesMaster
    public static ClsReturnData DeleteChargesMaster(int Id)
    {
        ClsReturnData clsRtn = new ClsReturnData();
        clsRtn.MsgType = (int)MessageType.Fail;
        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "Delete";
            cmd.Parameters.Add("@ChargeId", SqlDbType.Int).Value = Id;
            cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
            dt = db.FillTableProc(cmd, "USP_ChargeMaster");

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
            //    }
            //}

        }
        catch (Exception ex)
        {
            clsRtn.MsgType = (int)MessageType.Error;
            clsRtn.ID = 0;
            clsRtn.Message = ex.Message;
            clsRtn.MessageDesc = ex.Message;
        }
        finally
        {
            dt.Dispose();
        }

        return clsRtn;
    }
    #endregion
    //-------------------------------------------------------------

    //--------------------------------------------------------------
    #region Delete clsBankMaster
    public static ClsReturnData DeleteBank(int Id)
    {
        ClsReturnData clsRtn = new ClsReturnData();
        clsRtn.MsgType = (int)MessageType.Fail;
        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "Delete";
            cmd.Parameters.Add("@BankId", SqlDbType.Int).Value = Id;
            cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = 1;
            dt = db.FillTableProc(cmd, "USP_Bank");

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
            //    }
            //}

        }
        catch (Exception ex)
        {
            clsRtn.MsgType = (int)MessageType.Error;
            clsRtn.ID = 0;
            clsRtn.Message = ex.Message;
            clsRtn.MessageDesc = ex.Message;
        }
        finally
        {
            dt.Dispose();
        }

        return clsRtn;
    }
    #endregion
    //-------------------------------------------------------------


    //--------------------------Employee Master-----------------------
    // by chetan as himnshu change




    #region Save Employee
    public static ClsReturnData SaveEmployee(clsEmployee cls)
    {
        ClsReturnData clsRtn = new ClsReturnData();
        clsRtn.MsgType = (int)MessageType.Fail;
        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;
            cmd.Parameters.Add("@EmpID", SqlDbType.Int).Value = cls.EmpID;
            //cmd.Parameters.Add("@EmpCode", SqlDbType.VarChar).Value = cls.EmpCode;
            cmd.Parameters.Add("@EmpName", SqlDbType.VarChar).Value = cls.EmpName;
            cmd.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = cls.FatherName;
            cmd.Parameters.Add("@MotherName", SqlDbType.VarChar).Value = cls.MotherName;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = cls.Address;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = cls.StateID;
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = cls.CityID;
            cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar).Value = cls.ZipCode;
            cmd.Parameters.Add("@ContactNo1", SqlDbType.VarChar).Value = cls.ContactNo1;
            cmd.Parameters.Add("@ContactNo2", SqlDbType.VarChar).Value = cls.ContactNo2;
            cmd.Parameters.Add("@WhatsAppNo", SqlDbType.VarChar).Value = cls.WhatsAppNo;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = cls.Email;
            cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = cls.DOB == null ? (object)DBNull.Value : cls.DOB;
            cmd.Parameters.Add("@PAN", SqlDbType.VarChar).Value = cls.PAN;
            cmd.Parameters.Add("@AadhaarNo", SqlDbType.VarChar).Value = cls.AadhaarNo;
            cmd.Parameters.Add("@MaritalStatus", SqlDbType.VarChar).Value = cls.MaritalStatus;
            cmd.Parameters.Add("@ImageName", SqlDbType.VarChar).Value = cls.ImageName;
            cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = cls.IsDelete;
            cmd.Parameters.Add("@CompId", SqlDbType.Int).Value = ClsSession.CompanyID;
            cmd.Parameters.Add("@Longtitute", SqlDbType.VarChar).Value = cls.Longtitute;
            cmd.Parameters.Add("@Langtiute", SqlDbType.VarChar).Value = cls.Langtiute;
            cmd.Parameters.Add("@EmployeeAttnStatus", SqlDbType.Int).Value = cls.EmployeeAttnStatus;
            cmd.Parameters.Add("@AttnDate", SqlDbType.DateTime).Value = cls.AttnDate == null ? (object)DBNull.Value : cls.AttnDate;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = ClsSession.UserID;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = ClsSession.UserID;
            dt = db.FillTableProc(cmd, "USP_Employee");

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
            //    }
            //}

        }
        catch (Exception ex)
        {
            clsRtn.MsgType = (int)MessageType.Error;
            clsRtn.ID = 0;
            clsRtn.Message = ex.Message;
            clsRtn.MessageDesc = ex.Message;
        }
        finally
        {
            dt.Dispose();
        }

        return clsRtn;
    }

    #endregion

    #region GET Employee
    public static DataTable GetEmployee(int EmpID)
    {
        clsEmployee lst = new clsEmployee();

        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "View";
            cmd.Parameters.Add("@EmpID", SqlDbType.Int).Value = EmpID;
            cmd.Parameters.Add("@CompId", SqlDbType.Int).Value = ClsSession.CompanyID;
            cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = 0;
            dt = db.FillTableProc(cmd, "USP_Employee");
            //    }
            //}

            if (dt != null && dt.Rows.Count > 0)
                lst = GetItem<clsEmployee>(dt.Rows[0]);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt.Dispose();
        }

        return dt;

    }

    #endregion

    #region View Employee
    public static List<clsEmployee> ViewEmployee()
    {
        //DBHelper dBHlper = new DBHelper();
        List<clsEmployee> lst = new List<clsEmployee>();
        DataTable dt = new DataTable();
        try
        {
            //DBOperation db = new DBOperation();
            //SqlCommand cmd = new SqlCommand();

            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "View";
                    cmd.Parameters.Add("@CompId", SqlDbType.Int).Value = ClsSession.CompanyID;
                    cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = 0;
                    dt = db.FillTableProc(cmd, "USP_Employee");

                }
            }

            lst = DataInterface.ConvertDataTable<clsEmployee>(dt);
            //lst = DataInterface.ConvertDataTable<clsCustomerMaster>(dt);


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt.Dispose();
        }

        return lst;
    }
    #endregion

    #region Delete clsEmployee
    public static ClsReturnData DeleteEmployee(int Id)
    {
        ClsReturnData clsRtn = new ClsReturnData();
        clsRtn.MsgType = (int)MessageType.Fail;
        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "Delete";
            cmd.Parameters.Add("@EmpID", SqlDbType.Int).Value = Id;
            cmd.Parameters.Add("@CompId", SqlDbType.Int).Value = ClsSession.CompanyID;
            dt = db.FillTableProc(cmd, "USP_Employee");

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
            //    }
            //}

        }
        catch (Exception ex)
        {
            clsRtn.MsgType = (int)MessageType.Error;
            clsRtn.ID = 0;
            clsRtn.Message = ex.Message;
            clsRtn.MessageDesc = ex.Message;
        }
        finally
        {
            dt.Dispose();
        }

        return clsRtn;
    }
    #endregion

    //--------------------------Employee Master-----------------------

    //--------------------------Lead Detail Data-----------------------

    #region Lead Calling
    public static DataTable GetLeadDetail(clsLeadDetail cls)
    {
        DataTable dt = new DataTable();
        try
        {
            //DBOperation db = new DBOperation();
            //SqlCommand cmd = new SqlCommand();

            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;
                    cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = cls.LeadId;
                    cmd.Parameters.Add("@StageId", SqlDbType.Int).Value = cls.StageId;
                    cmd.Parameters.Add("@ShortStage_Name", SqlDbType.VarChar).Value = cls.ShortStage_Name;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                    dt = db.FillTableProc(cmd, "USP_LeadDetail");
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt.Dispose();
        }

        return dt;

    }

    #endregion
    //--------------------------Lead Detail Data-----------------------


    //--------------------------Lead Calling-----------------------

    #region Lead Calling
    public static DataTable GetLeadCalling(clsLeadMain cls)
    {
        DataTable dt = new DataTable();
        try
        {
            //DBOperation db = new DBOperation();
            //SqlCommand cmd = new SqlCommand();

            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;
                    cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = cls.LeadId;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                    cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = 0;
                    dt = db.FillTableProc(cmd, "USP_LeadCalling");
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt.Dispose();
        }

        return dt;

    }

    #endregion

    #region Save LeadCalling
    public static DataTable SaveLeadCalling(clsLeadCalling cls)
    {
        //ClsReturnData clsRtn = new ClsReturnData();
        //clsRtn.MsgType = (int)MessageType.Fail;
        DataTable dt = new DataTable();
        try
        {
            //DBOperation db = new DBOperation();
            //SqlCommand cmd = new SqlCommand();

            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;
                    cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = cls.LeadId;

                    cmd.Parameters.Add("@TcId", SqlDbType.Int).Value = cls.TcId;
                    cmd.Parameters.Add("@QuestionId", SqlDbType.Int).Value = cls.QuestionId;
                    cmd.Parameters.Add("@Answer", SqlDbType.VarChar).Value = cls.Answer;
                    cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = cls.Remarks;
                    cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = cls.IsDelete;

                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = ClsSession.UserID;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = ClsSession.UserID;
                    dt = db.FillTableProc(cmd, "USP_LeadCalling");

                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    clsRtn.ID = Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"]));
                    //    clsRtn.Message = Convert.ToString(dt.Rows[0]["ReturnMessage"]);
                    //    clsRtn.MessageDesc = clsRtn.Message;
                    //    if (clsRtn.ID > 0)
                    //        clsRtn.MsgType = (int)MessageType.Success;
                    //    else
                    //        clsRtn.MsgType = (int)MessageType.Fail;
                    //}
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
            //clsRtn.MsgType = (int)MessageType.Error;
            //clsRtn.ID = 0;
            //clsRtn.Message = ex.Message;
            //clsRtn.MessageDesc = ex.Message;
        }
        finally
        {
            dt.Dispose();
        }

        return dt;
    }

    #endregion


    #region Update Lead Status
    public static DataTable UpdateLeadStatus(clsLeadMain cls)
    {
        //ClsReturnData clsRtn = new ClsReturnData();
        //clsRtn.MsgType = (int)MessageType.Fail;
        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand cmd = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;
            cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = cls.LeadId;
            cmd.Parameters.Add("@StageId", SqlDbType.Int).Value = cls.StageId;
            cmd.Parameters.Add("@StageName", SqlDbType.VarChar).Value = cls.StageName;
            cmd.Parameters.Add("@IsActive", SqlDbType.Int).Value = cls.IsActive;
            cmd.Parameters.Add("@IsCurrent", SqlDbType.Int).Value = cls.IsCurrent;
            cmd.Parameters.Add("@ShortStage_Name", SqlDbType.VarChar).Value = cls.ShortStage_Name;
            cmd.Parameters.Add("@Dependancy", SqlDbType.Int).Value = cls.Dependancy;
            cmd.Parameters.Add("@Sequence", SqlDbType.Int).Value = cls.Sequence;
            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = cls.Status;
            cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = cls.Remarks;
            cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
            dt = db.FillTableProc(cmd, "USP_LeadDetail");

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    clsRtn.ID = Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"]));
            //    clsRtn.Message = Convert.ToString(dt.Rows[0]["ReturnMessage"]);
            //    clsRtn.MessageDesc = clsRtn.Message;
            //    if (clsRtn.ID > 0)
            //        clsRtn.MsgType = (int)MessageType.Success;
            //    else
            //        clsRtn.MsgType = (int)MessageType.Fail;
            //}
            //    }
            //}

        }
        catch (Exception ex)
        {
            throw ex;
            //clsRtn.MsgType = (int)MessageType.Error;
            //clsRtn.ID = 0;
            //clsRtn.Message = ex.Message;
            //clsRtn.MessageDesc = ex.Message;
        }
        finally
        {
            dt.Dispose();
        }

        return dt;
    }

    #endregion


    //--------------------------Lead Calling-----------------------

    //--------------------------Lead Credit-----------------------

    #region Lead Credit
    public static DataTable GetLeadCredit(clsLeadMain cls)
    {
        DataTable dt = new DataTable();
        try
        {
            //DBOperation db = new DBOperation();
            //SqlCommand cmd = new SqlCommand();

            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;
                    cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = cls.LeadId;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                    dt = db.FillTableProc(cmd, "USP_LeadCredit");
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt.Dispose();
        }

        return dt;

    }

    #endregion

    #region Save LeadCredit
    public static DataTable SaveLeadCredit(clsLeadCredit cls)
    {
        //ClsReturnData clsRtn = new ClsReturnData();
        //clsRtn.MsgType = (int)MessageType.Fail;
        DataTable dt = new DataTable();
        try
        {
            //DBOperation db = new DBOperation();
            //SqlCommand cmd = new SqlCommand();

            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;
                    cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = cls.LeadId;
                    cmd.Parameters.Add("@CrId", SqlDbType.Int).Value = cls.CrId;

                    cmd.Parameters.Add("@CIBILVerification", SqlDbType.VarChar).Value = cls.CIBILVerification;
                    cmd.Parameters.Add("@CibilDoc", SqlDbType.VarChar).Value = cls.CibilDoc;
                    cmd.Parameters.Add("@CibilRemarks", SqlDbType.VarChar).Value = cls.CibilRemarks;
                    cmd.Parameters.Add("@FIVerification", SqlDbType.VarChar).Value = cls.FIVerification;
                    cmd.Parameters.Add("@FIDoc", SqlDbType.VarChar).Value = cls.FIDoc;
                    cmd.Parameters.Add("@FIRemarks", SqlDbType.VarChar).Value = cls.FIRemarks;
                    cmd.Parameters.Add("@TVRVerification", SqlDbType.VarChar).Value = cls.TVRVerification;
                    cmd.Parameters.Add("@TVRDoc", SqlDbType.VarChar).Value = cls.TVRDoc;
                    cmd.Parameters.Add("@TVRRemarks", SqlDbType.VarChar).Value = cls.TVRRemarks;
                    cmd.Parameters.Add("@CashFlowVerification", SqlDbType.VarChar).Value = cls.CashFlowVerification;
                    cmd.Parameters.Add("@CashFlowDoc", SqlDbType.VarChar).Value = cls.CashFlowDoc;
                    cmd.Parameters.Add("@CashFlowRemarks", SqlDbType.VarChar).Value = cls.CashFlowRemarks;
                    cmd.Parameters.Add("@ViechleValVerfication", SqlDbType.VarChar).Value = cls.ViechleValVerfication;
                    cmd.Parameters.Add("@ViechleDoc", SqlDbType.VarChar).Value = cls.ViechleDoc;
                    cmd.Parameters.Add("@ViechleRemarks", SqlDbType.VarChar).Value = cls.ViechleRemarks;
                    cmd.Parameters.Add("@BankStmtVerification", SqlDbType.VarChar).Value = cls.BankStmtVerification;
                    cmd.Parameters.Add("@BankStmtDoc", SqlDbType.VarChar).Value = cls.BankStmtDoc;
                    cmd.Parameters.Add("@BankStmtRemarks", SqlDbType.VarChar).Value = cls.BankStmtRemarks;
                    cmd.Parameters.Add("@IncomeStmtVerification", SqlDbType.VarChar).Value = cls.IncomeStmtVerification;
                    cmd.Parameters.Add("@IncomeStmtDoc", SqlDbType.VarChar).Value = cls.IncomeStmtDoc;
                    cmd.Parameters.Add("@IncomeStmtRemarks", SqlDbType.VarChar).Value = cls.IncomeStmtRemarks;
                    cmd.Parameters.Add("@PersonalDiscussVerification", SqlDbType.VarChar).Value = cls.PersonalDiscussVerification;
                    cmd.Parameters.Add("@PersonalDiscussDoc", SqlDbType.VarChar).Value = cls.PersonalDiscussDoc;
                    cmd.Parameters.Add("@PersonalDiscusssRemarks", SqlDbType.VarChar).Value = cls.PersonalDiscusssRemarks;
                    cmd.Parameters.Add("@Eligiblity", SqlDbType.VarChar).Value = cls.Eligiblity;
                    cmd.Parameters.Add("@EligiblityDoc", SqlDbType.VarChar).Value = cls.EligiblityDoc;
                    cmd.Parameters.Add("@EligiblityRemarks", SqlDbType.VarChar).Value = cls.EligiblityRemarks;
                    cmd.Parameters.Add("@Occupation", SqlDbType.VarChar).Value = cls.Occupation;
                    cmd.Parameters.Add("@PropertyDocVerification", SqlDbType.VarChar).Value = cls.PropertyDocVerification;
                    cmd.Parameters.Add("@PropertyDoc", SqlDbType.VarChar).Value = cls.PropertyDoc;
                    cmd.Parameters.Add("@PropertyDocRemarks", SqlDbType.VarChar).Value = cls.PropertyDocRemarks;
                    cmd.Parameters.Add("@ColletralType", SqlDbType.VarChar).Value = cls.ColletralType;
                    cmd.Parameters.Add("@PropertyAddress", SqlDbType.VarChar).Value = cls.PropertyAddress;
                    cmd.Parameters.Add("@PropertySize", SqlDbType.VarChar).Value = cls.PropertySize;
                    cmd.Parameters.Add("@LandArea", SqlDbType.VarChar).Value = cls.LandArea;
                    cmd.Parameters.Add("@Dimension", SqlDbType.VarChar).Value = cls.Dimension;
                    cmd.Parameters.Add("@SecurityValue", SqlDbType.VarChar).Value = cls.SecurityValue;
                    cmd.Parameters.Add("@MarketValue", SqlDbType.VarChar).Value = cls.MarketValue;
                    cmd.Parameters.Add("@LandValue", SqlDbType.VarChar).Value = cls.LandValue;
                    cmd.Parameters.Add("@ConstrutionValue", SqlDbType.VarChar).Value = cls.ConstrutionValue;
                    cmd.Parameters.Add("@TotalMarketValue", SqlDbType.VarChar).Value = cls.TotalMarketValue;
                    cmd.Parameters.Add("@LTV", SqlDbType.VarChar).Value = cls.LTV;
                    cmd.Parameters.Add("@RelizableValue", SqlDbType.VarChar).Value = cls.RelizableValue;
                    cmd.Parameters.Add("@PropertyVal", SqlDbType.VarChar).Value = cls.PropertyVal;
                    cmd.Parameters.Add("@PropertyDocuments", SqlDbType.VarChar).Value = cls.PropertyDocuments;
                    cmd.Parameters.Add("@PropertyChain", SqlDbType.VarChar).Value = cls.PropertyChain;
                    cmd.Parameters.Add("@LegalOpinionBy", SqlDbType.VarChar).Value = cls.LegalOpinionBy;
                    //cmd.Parameters.Add("@LegalReportDate", SqlDbType.VarChar).Value = cls.LegalReportDate != null ? DateTime.Parse(cls.LegalReportDate.ToString()).ToString("dd-MM-yyyy") : (object)DBNull.Value;
                    cmd.Parameters.Add("@LegalReportDate", SqlDbType.VarChar).Value = cls.LegalReportDate == null ? (object)DBNull.Value : cls.LegalReportDate;
                    cmd.Parameters.Add("@ValuerName", SqlDbType.VarChar).Value = cls.ValuerName;
                    cmd.Parameters.Add("@Valuation", SqlDbType.VarChar).Value = cls.Valuation;
                    //cmd.Parameters.Add("@ValueDate", SqlDbType.VarChar).Value = cls.ValueDate != null ? DateTime.Parse(cls.ValueDate.ToString()).ToString("dd-MM-yyyy") : (object)DBNull.Value;
                    cmd.Parameters.Add("@ValueDate", SqlDbType.VarChar).Value = cls.ValueDate == null ? (object)DBNull.Value : cls.ValueDate;
                    cmd.Parameters.Add("@BussinessName", SqlDbType.VarChar).Value = cls.BussinessName;
                    cmd.Parameters.Add("@BussinessVinatage", SqlDbType.VarChar).Value = cls.BussinessVinatage;
                    cmd.Parameters.Add("@BussinessAddress", SqlDbType.VarChar).Value = cls.BussinessAddress;
                    cmd.Parameters.Add("@BussinessProve", SqlDbType.VarChar).Value = cls.BussinessProve;
                    cmd.Parameters.Add("@CAMGeneration", SqlDbType.VarChar).Value = cls.CAMGeneration;
                    cmd.Parameters.Add("@CAMDoc", SqlDbType.VarChar).Value = cls.CAMDoc;
                    cmd.Parameters.Add("@CAMRemarks", SqlDbType.VarChar).Value = cls.CAMRemarks;
                    cmd.Parameters.Add("@CreditRemarks", SqlDbType.VarChar).Value = cls.CreditRemarks;
                    cmd.Parameters.Add("@NegativeRemarks", SqlDbType.VarChar).Value = cls.NegativeRemarks;

                    cmd.Parameters.Add("@VehicleType", SqlDbType.VarChar).Value = cls.VehicleType;
                    cmd.Parameters.Add("@ChassisNo", SqlDbType.VarChar).Value = cls.ChassisNo;
                    cmd.Parameters.Add("@EngineNo", SqlDbType.VarChar).Value = cls.EngineNo;
                    cmd.Parameters.Add("@Insurername", SqlDbType.VarChar).Value = cls.Insurername;
                    cmd.Parameters.Add("@InsurancePolicyNo", SqlDbType.VarChar).Value = cls.InsurancePolicyNo;
                    //cmd.Parameters.Add("@PolicyValidity", SqlDbType.DateTime).Value = cls.PolicyValidity;
                    //cmd.Parameters.Add("@RCDate", SqlDbType.DateTime).Value = cls.RCDate;
                    cmd.Parameters.Add("@PolicyValidity", SqlDbType.VarChar).Value = cls.PolicyValidity == null ? (object)DBNull.Value : cls.PolicyValidity;
                    cmd.Parameters.Add("@RCDate", SqlDbType.VarChar).Value = cls.RCDate == null ? (object)DBNull.Value : cls.RCDate;
                    cmd.Parameters.Add("@Fitness", SqlDbType.VarChar).Value = cls.Fitness;
                    cmd.Parameters.Add("@DealerId", SqlDbType.Int).Value = cls.DealerId;
                    cmd.Parameters.Add("@RTO", SqlDbType.VarChar).Value = cls.RTO;
                    cmd.Parameters.Add("@Transmission", SqlDbType.VarChar).Value = cls.Transmission;
                    cmd.Parameters.Add("@FuelType", SqlDbType.VarChar).Value = cls.FuelType;
                    cmd.Parameters.Add("@CarType", SqlDbType.VarChar).Value = cls.CarType;
                    cmd.Parameters.Add("@DependentFamilyAssessmentVerification", SqlDbType.VarChar).Value = cls.DependentFamilyAssessmentVerification;
                    cmd.Parameters.Add("@DependentFamilyAssessmentDoc", SqlDbType.VarChar).Value = cls.DependentFamilyAssessmentDoc;
                    cmd.Parameters.Add("@DependentFamilyAssessmentRemarks", SqlDbType.VarChar).Value = cls.DependentFamilyAssessmentRemarks;
                    cmd.Parameters.Add("@ProposedLoanAmountAndCommercial", SqlDbType.VarChar).Value = cls.ProposedLoanAmountAndCommercial;
                    cmd.Parameters.Add("@Occupancy", SqlDbType.VarChar).Value = cls.Occupancy;
                    cmd.Parameters.Add("@CreditCheckList", SqlDbType.VarChar).Value = cls.CreditCheckList;
                    cmd.Parameters.Add("@CreditCheckListID", SqlDbType.Int).Value = cls.CreditCheckListID;
                    cmd.Parameters.Add("@CreditCheckListRemarks", SqlDbType.VarChar).Value = cls.CreditCheckListRemarks;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = ClsSession.UserID;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = ClsSession.UserID;
                    dt = db.FillTableProc(cmd, "USP_LeadCredit");

                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    clsRtn.ID = Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"]));
                    //    clsRtn.Message = Convert.ToString(dt.Rows[0]["ReturnMessage"]);
                    //    clsRtn.MessageDesc = clsRtn.Message;
                    //    if (clsRtn.ID > 0)
                    //        clsRtn.MsgType = (int)MessageType.Success;
                    //    else
                    //        clsRtn.MsgType = (int)MessageType.Fail;
                    //}
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
            //clsRtn.MsgType = (int)MessageType.Error;
            //clsRtn.ID = 0;
            //clsRtn.Message = ex.Message;
            //clsRtn.MessageDesc = ex.Message;
        }
        finally
        {
            dt.Dispose();
        }

        return dt;
    }

    #endregion

    //--------------------------Lead Credit-----------------------

    //--------------------------GET Status for DDL-----------------------

    #region GET Status for DDL
    public static DataTable GetStatusForDDL(string StatusType)
    {
        //DBHelper dBHlper = new DBHelper();
        List<clsMisc> lst = new List<clsMisc>();
        DataTable dt = new DataTable();
        try
        {
            //DBOperation db = new DBOperation();
            //SqlCommand cmd = new SqlCommand();

            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "DDLByStatusType";
                    cmd.Parameters.Add("@StatusType", SqlDbType.VarChar).Value = StatusType;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                    cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = 0;
                    dt = db.FillTableProc(cmd, "USP_Status");

                }
            }


            //lst = DataInterface.ConvertDataTable<clsMisc>(dt);


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt.Dispose();
        }

        return dt;
    }
    #endregion

    //--------------------------GET Status for DDL-----------------------
    #region Center
    public static DataTable DBCenter(clsCenter cls)
    {
        DataTable dt = new DataTable();
        try
        {
            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ReqType", cls.ReqType);
                    cmd.Parameters.AddWithValue("@CenterId", cls.CenterId);
                    cmd.Parameters.AddWithValue("@CompanyID", ClsSession.CompanyID);
                    cmd.Parameters.AddWithValue("@IsDelete", cls.IsDelete);
                    cmd.Parameters.AddWithValue("@CenterName", cls.CenterName);
                    cmd.Parameters.AddWithValue("@CenterHead", cls.CenterHead);
                    cmd.Parameters.AddWithValue("@MaxNo", cls.MaxNo);
                    cmd.Parameters.AddWithValue("@CreatedBy", cls.CreatedBy);
                    cmd.Parameters.AddWithValue("@UpdatedBy", cls.UpdateBy);

                    dt = db.FillTableProc(cmd, "USP_Center");

                }

            }

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt.Dispose();
        }

        return dt;
    }
    #endregion


}
