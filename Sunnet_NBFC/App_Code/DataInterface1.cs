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
using Sunnet_NBFC.App_Code;
using System.ComponentModel.Design;

public class DataInterface1 : IDisposable
{

    public static DataTable GetChargeType()
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", "View");
                sqlCommand.Parameters.AddWithValue("@CompanyId", ClsSession.CompanyID);
                dt = db.FillTableProc(sqlCommand, "USP_ChargeType");
            }


        }
        return dt;
    }

    public static string GetChargeTypeKeyCode(int ChargeTypeID)
    {
        string RetStr = string.Empty;
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", "GetChargeTypeKey");
                sqlCommand.Parameters.AddWithValue("@CompanyId", ClsSession.CompanyID);
                sqlCommand.Parameters.AddWithValue("@ChargeTypeID", ChargeTypeID);
                dt = db.FillTableProc(sqlCommand, "USP_ChargeType");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("KeyCode"))
                        RetStr = Convert.ToString(dt.Rows[0]["KeyCode"]);
                }
            }


        }
        return RetStr;
    }
    public static DataTable GetCity(clsCity cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@CityId", cls.Cityid);
                sqlCommand.Parameters.AddWithValue("@StateId", cls.Stateid);
                sqlCommand.Parameters.AddWithValue("@CityName", cls.CityName);
                dt = db.FillTableProc(sqlCommand, "USP_City");
            }


        }
        return dt;
    }

    public static DataTable GetState()
    {
        DataTable dt = new DataTable();
        try
        {

            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {

                    dt = db.FillTableProc(sqlCommand, "USP_STATE");
                }


            }

        }
        catch (Exception e)
        {

        }
        return dt;
    }



    public static DataTable GetCustomerData(string cifid, string Aadharno, string panno)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", "View");
                sqlCommand.Parameters.AddWithValue("@AadharNo", Aadharno);
                sqlCommand.Parameters.AddWithValue("@CIF", cifid);
                sqlCommand.Parameters.AddWithValue("@PanNo", panno);

                dt = db.FillTableProc(sqlCommand, "USP_LeadCustomer");
            }


        }
        return dt;
    }



    public static DataTable SetApiResponse(string request, string responseCode, string responseMessage, string response, string ApiName)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", "Insert");
                sqlCommand.Parameters.AddWithValue("@Request", request);
                sqlCommand.Parameters.AddWithValue("@Response", response);
                sqlCommand.Parameters.AddWithValue("@ResponseCode", responseCode);
                sqlCommand.Parameters.AddWithValue("@ResponseMsg", responseMessage);
                sqlCommand.Parameters.AddWithValue("@APINAME", ApiName);


                dt = db.FillTableProc(sqlCommand, "USP_APIResponse");
            }


        }
        return dt;
    }

    public static DataTable GetProduct(clsProduct cls)
    {

        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@MainProdId", cls.MainProdId);
                sqlCommand.Parameters.AddWithValue("@ProdId", cls.ProdId);
                sqlCommand.Parameters.AddWithValue("@ProductName", cls.ProductName);
                sqlCommand.Parameters.AddWithValue("@CustTypeRequried", cls.CustTypeRequried);
                sqlCommand.Parameters.AddWithValue("@CompanyId", cls.CompanyId);
                dt = db.FillTableProc(sqlCommand, "USP_Product");
            }
        }
        return dt;
    }

    public static DataTable GetMainProductddl(string ReqType)
    {

        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", ReqType);
                dt = db.FillTableProc(sqlCommand, "USP_MainProduct");
            }
        }
        return dt;
    }
    public static DataTable GetMiseddl(string MiscType)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", "View");
                sqlCommand.Parameters.AddWithValue("@MiscType", MiscType);
                sqlCommand.Parameters.AddWithValue("@CompanyId", ClsSession.CompanyID);
                sqlCommand.Parameters.AddWithValue("@isDelete", 0);
                dt = db.FillTableProc(sqlCommand, "USP_MiscMaster");
            }
        }
        return dt;
    }

    public static DataTable GetPaymentModeddl()
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", "View");
                sqlCommand.Parameters.AddWithValue("@MiscType", "PaymentMode");
                sqlCommand.Parameters.AddWithValue("@CompanyId", ClsSession.CompanyID);
                sqlCommand.Parameters.AddWithValue("@isDelete", 0);
                dt = db.FillTableProc(sqlCommand, "USP_MiscMaster");
            }
        }
        return dt;
    }


    public static DataTable dbBranchddl()
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "View";
                cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                cmd.Parameters.Add("@IsDelete", SqlDbType.VarChar).Value = 0;
                dt = db.FillTableProc(cmd, "USP_Branch");
            }
        }
        return dt;
    }

    public static DataTable dbBankddl()
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "View";
                //cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                cmd.Parameters.Add("@IsDelete", SqlDbType.VarChar).Value = 0;
                dt = db.FillTableProc(cmd, "USP_Bank");
            }
        }
        return dt;
    }

    public static DataTable dbLeadddl()
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "View";
                cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                cmd.Parameters.Add("@IsDelete", SqlDbType.VarChar).Value = 0;
                dt = db.FillTableProc(cmd, "USP_LEAD");
            }
        }
        return dt;
    }

    public static clsEmployeeDetails GetEmployeedtlbyid(int EmpID)
    {
        clsEmployeeDetails lst = new clsEmployeeDetails();


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
            cmd.Parameters.Add("@Companyid", SqlDbType.Int).Value = ClsSession.CompanyID;
            cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = 0;
            dt = db.FillTableProc(cmd, "USP_EmployeeDetails");
            //    }
            //}

            if (dt != null && dt.Rows.Count > 0)
                lst = GetItem<clsEmployeeDetails>(dt.Rows[0]);

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

    public static DataTable GetKeyMaster()
    {

        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", "Select");
                dt = db.FillTableProc(sqlCommand, "USP_KeyMaster");
            }
        }
        return dt;
    }
    public static ClsReturnData GetEmployeeDetails(clsEmployeeDetails cls)
    {
        ClsReturnData clsRtn = new ClsReturnData();
        clsRtn.MsgType = (int)MessageType.Fail;
        DataTable dt = new DataTable();
        try
        {
            DBOperation db = new DBOperation();
            SqlCommand sqlCommand = new SqlCommand();

            //using (DBOperation db = new DBOperation())
            //{
            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
            sqlCommand.Parameters.AddWithValue("@EmpDtlID", cls.EmpDtlID);
            sqlCommand.Parameters.AddWithValue("@EmpID", cls.EmpID);
            sqlCommand.Parameters.AddWithValue("@DesignationID", cls.DesignationID);
            sqlCommand.Parameters.AddWithValue("@DepartmentId", cls.DepartmentId);
            sqlCommand.Parameters.AddWithValue("@DOJ", cls.DOJ);
            sqlCommand.Parameters.AddWithValue("@LastESICNo", cls.LastESICNo);
            sqlCommand.Parameters.AddWithValue("@LastPFNo", cls.LastPFNo);
            sqlCommand.Parameters.AddWithValue("@LastAcadmicDegree", cls.LastAcadmicDegree);
            sqlCommand.Parameters.AddWithValue("@LastProfDegree", cls.LastProfDegree);
            sqlCommand.Parameters.AddWithValue("@LastCompany", cls.LastCompany);
            sqlCommand.Parameters.AddWithValue("@LastExperienceDtls", cls.LastExperienceDtls);
            sqlCommand.Parameters.AddWithValue("@Salary", cls.Salary);
            sqlCommand.Parameters.AddWithValue("@IsLeave", cls.IsLeave);
            sqlCommand.Parameters.AddWithValue("@DOL", cls.DOL);
            sqlCommand.Parameters.AddWithValue("@LoginType", cls.LoginType);
            sqlCommand.Parameters.AddWithValue("@Companyid", cls.Companyid);
            sqlCommand.Parameters.AddWithValue("@IsActive", cls.IsActive);
            sqlCommand.Parameters.AddWithValue("@IsDelete", cls.IsDelete);
            sqlCommand.Parameters.AddWithValue("@Longtitute", cls.Longtitute);
            sqlCommand.Parameters.AddWithValue("@Langtiute", cls.Langtiute);
            dt = db.FillTableProc(sqlCommand, "USP_EmployeeDetails");

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

    public static List<RoleSideMenuBar> GetSideBarMenuRights()
    {
        DataTable Dt = new DataTable();
        List<RoleSideMenuBar> subMenuRights = new List<RoleSideMenuBar>();
        int iRoleID = ClsSession.RoleID;
        int iEMPID = ClsSession.EmpId;
        try
        {


            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ReqType", "SideMenuBarRights");
                    sqlCommand.Parameters.AddWithValue("@EmpId", iEMPID);
                    Dt = db.FillTableProc(sqlCommand, "USP_Role");
                }

                if (Dt == null || Dt.Rows.Count == 0)
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@ReqType", "SideMenuBarRights");
                        sqlCommand.Parameters.AddWithValue("@RoleId", iRoleID);
                        Dt = db.FillTableProc(sqlCommand, "USP_Role");
                    }
                }

            }


            if (Dt != null)
            {
                foreach (DataRow dr in Dt.Rows)
                {
                    subMenuRights.Add(new RoleSideMenuBar
                    {
                        MenuDisplaySeqNo = Convert.ToInt32("0" + Convert.ToString(dr["MenuDisplaySeqNo"])),
                        ParentMenuID = Convert.ToInt32("0" + Convert.ToString(dr["ParentMenuID"])),
                        MenuName = Convert.ToString(dr["MenuName"]),
                        MenuUrl = Convert.ToString(dr["MenuUrl"]),
                        SubMenuDisplaySeqNo = Convert.ToInt32("0" + Convert.ToString(dr["SubMenuDisplaySeqNo"])),
                        ChildMenuID = Convert.ToInt32("0" + Convert.ToString(dr["ChildMenuID"])),
                        Title = Convert.ToString(dr["Title"]),
                        Controller = Convert.ToString(dr["Controller"]),
                        Action = Convert.ToString(dr["Action"])
                    });
                }


            }



        }
        catch (Exception ex)
        {
        }
        finally
        {
            if (Dt != null)
                Dt.Dispose();
        }
        return subMenuRights;
    }

    public static DataTable GetMenuMaster(clsMenuMaster cls)
    {

        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@MenuId", cls.MenuId);
                sqlCommand.Parameters.AddWithValue("@MenuName", cls.MenuName);
                sqlCommand.Parameters.AddWithValue("@MenuUrl", cls.MenuUrl);
                sqlCommand.Parameters.AddWithValue("@MenuActive", cls.MenuActive);
                dt = db.FillTableProc(sqlCommand, "USP_Menu");
            }
        }
        return dt;
    }

    public static DataTable GetSubMenu(clsSubMenu cls)
    {

        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@SubMenuId", cls.SubMenuId);
                sqlCommand.Parameters.AddWithValue("@MenuId", cls.MenuId);
                sqlCommand.Parameters.AddWithValue("@Title", cls.Title);
                sqlCommand.Parameters.AddWithValue("@Controller", cls.Controller);
                sqlCommand.Parameters.AddWithValue("@Action", cls.Action);
                sqlCommand.Parameters.AddWithValue("@IsActive", cls.IsActive);
                dt = db.FillTableProc(sqlCommand, "USP_SubMenu");
            }
        }
        return dt;
    }

    public static DataTable GetBankData(int? BankId)
    {
        DataTable dt = new DataTable();
        try
        {
            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ReqType", "View");
                    sqlCommand.Parameters.Add("@BankId", SqlDbType.Int).Value = BankId;
                    sqlCommand.Parameters.Add("@IsDelete", SqlDbType.Int).Value = 0;

                    dt = db.FillTableProc(sqlCommand, "USP_Bank");
                }
            }

        }
        catch
        {
            return dt;
        }
        finally
        {
        }
        return dt;

    }

    public static DataTable dbBank(clsBankMaster cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.Add("@BankId", SqlDbType.Int).Value = cls.BankId;
                sqlCommand.Parameters.Add("@BankName", SqlDbType.VarChar).Value = cls.BankName;
                sqlCommand.Parameters.Add("@BankBranch", SqlDbType.VarChar).Value = cls.BankBranch;
                sqlCommand.Parameters.Add("@BankIFSCCode", SqlDbType.VarChar).Value = cls.BankIFSCCode;
                sqlCommand.Parameters.Add("@BankMICRCode", SqlDbType.VarChar).Value = cls.BankMICRCode;
                sqlCommand.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = cls.CreatedBy;
                sqlCommand.Parameters.Add("@IsDelete", SqlDbType.Int).Value = cls.IsDelete;
                dt = db.FillTableProc(sqlCommand, "USP_Bank");

            }
        }
        return dt;
    }
    public static DataTable GetChargeMasterById(int? ChargeId)
    {
        DataTable dt = new DataTable();
        try
        {
            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ReqType", "Get");
                    sqlCommand.Parameters.Add("@ChargeId", SqlDbType.Int).Value = ChargeId;

                    dt = db.FillTableProc(sqlCommand, "USP_ChargeMaster");
                }
            }

        }
        catch
        {
            return dt;
        }
        finally
        {
        }
        return dt;

    }

    public static DataTable GetChargeMaster()
    {
        DataTable dt = new DataTable();
        try
        {
            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ReqType", "View");
                    sqlCommand.Parameters.Add("@IsDelete", SqlDbType.Int).Value = 0;
                    dt = db.FillTableProc(sqlCommand, "USP_ChargeMaster");
                }
            }

        }
        catch
        {
            return dt;
        }
        finally
        {
        }
        return dt;

    }

    public static DataTable dbChargesMaster(clsChargesMaster cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.Add("@ChargeId", SqlDbType.Int).Value = cls.ChargeID;
                sqlCommand.Parameters.Add("@ChargeName", SqlDbType.VarChar).Value = cls.ChargeName;
                sqlCommand.Parameters.Add("@ChargeTypeID", SqlDbType.Int).Value = cls.ChargeTypeID;
                sqlCommand.Parameters.Add("@IsChargePer", SqlDbType.Bit).Value = cls.IsChargePer;
                sqlCommand.Parameters.Add("@ChargePer", SqlDbType.Float).Value = cls.ChargePer;
                sqlCommand.Parameters.Add("@ChargeAmount", SqlDbType.Float).Value = cls.ChargeAmount;
                sqlCommand.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = cls.CreatedBy;
                sqlCommand.Parameters.Add("@UpdateBy", SqlDbType.Int).Value = cls.UpdatedBy;
                sqlCommand.Parameters.Add("@IsDelete", SqlDbType.Int).Value = cls.IsDelete;
                sqlCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                sqlCommand.Parameters.Add("@EffectiveFromDate", SqlDbType.DateTime).Value = cls.EffectiveFromDate;
                sqlCommand.Parameters.Add("@EffectiveToDate", SqlDbType.DateTime).Value = cls.EffectiveToDate;
                sqlCommand.Parameters.Add("@ChargeType", SqlDbType.VarChar).Value = cls.ChargeType;
                dt = db.FillTableProc(sqlCommand, "USP_ChargeMaster");

                /*
                DateTime DtpEffectiveFromDate;
                DateTime DtpEffectiveToDate;

                if (!string.IsNullOrEmpty(Convert.ToString(cls.EffectiveFromDate)))
                {
                    DtpEffectiveFromDate = (DateTime)cls.EffectiveFromDate;
                    sqlCommand.Parameters.Add("@EffectiveFromDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DtpEffectiveFromDate.ToString("dd/MM/yyyy"));
                }
                else
                    sqlCommand.Parameters.Add("@EffectiveFromDate", SqlDbType.DateTime).Value = DBNull.Value;

                if (!string.IsNullOrEmpty(Convert.ToString(cls.EffectiveToDate)))
                {
                    DtpEffectiveToDate = (DateTime)cls.EffectiveToDate;
                    sqlCommand.Parameters.Add("@EffectiveToDate", SqlDbType.DateTime).Value = Convert.ToDateTime(DtpEffectiveToDate.ToString("dd/MM/yyyy"));
                }
                else
                    sqlCommand.Parameters.Add("@EffectiveToDate", SqlDbType.DateTime).Value = DBNull.Value;
                */



            }
        }
        return dt;
    }

    public static string GetNextReceiptNo()
    {
        string StrVal = string.Empty;
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = "NextReceiptNo";
                cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                dt = db.FillTableProc(cmd, "USP_Receipt");
                if (dt.Rows.Count > 0)
                    StrVal = Convert.ToString(dt.Rows[0][0]);
            }
        }
        return StrVal;
    }
    public static IList<clsReceiptChargesDetailsFill> GetReceiptDetail(int? ReceiptId)
    {

        List<clsReceiptChargesDetailsFill> receiptDetail = new List<clsReceiptChargesDetailsFill>();

        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", "Get");
                sqlCommand.Parameters.Add("@ReceiptID", SqlDbType.Int).Value = ReceiptId;
                DataSet ds = new DataSet();
                ds = db.FillDsProc(sqlCommand, "USP_Receipt");

                if (ds.Tables.Count > 1)
                {
                    /*Get Only Detail Part*/
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        receiptDetail.Add(new clsReceiptChargesDetailsFill
                        {
                            ReceiptDtlID = Convert.ToInt64("0" + dr["ReceiptDtlID"]),
                            ReceiptID = Convert.ToInt32("0" + dr["ReceiptID"]),
                            ChargeTypeID = Convert.ToInt32("0" + dr["ChargeTypeID"]),
                            ChargeType = dr["ChargeType"].ToString(),
                            KeyType = dr["KeyType"].ToString(),
                            InstallmentID = Convert.ToInt32("0" + dr["InstallmentID"]),
                            Installment = dr["InstallmentNo"].ToString(),
                            Amount = Convert.ToDouble("0" + dr["Amount"]),
                            Remarks = dr["Remarks"].ToString()
                        });
                    }
                }

            }
        }




        return (receiptDetail);

    }

    public static DataSet GetReceipt(int? ReceiptId)
    {
        DataSet DS = new DataSet();
        try
        {
            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ReqType", "Get");
                    sqlCommand.Parameters.Add("@ReceiptID", SqlDbType.Int).Value = ReceiptId;

                    DS = db.FillDsProc(sqlCommand, "USP_Receipt");
                }
            }

        }
        catch
        {
            return DS;
        }
        finally
        {
        }
        return DS;

    }
    public static DataSet GetReceiptView()
    {
        DataSet DS = new DataSet();
        try
        {
            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ReqType", "View");
                    sqlCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                    DS = db.FillDsProc(sqlCommand, "USP_Receipt");
                }
            }

        }
        catch
        {
            return DS;
        }
        finally
        {
        }
        return DS;

    }
    public static DataTable dbReceipt(clsReceipt receipt)
    {
        DataTable dt = new DataTable();
        DataTable dtReceiptChargesDetails = new DataTable();
        dynamic json = null;
        json = Newtonsoft.Json.JsonConvert.SerializeObject(receipt.clsReceiptChargesDetails.ToList());
        dtReceiptChargesDetails = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", receipt.ReqType);
                sqlCommand.Parameters.Add("@ReceiptID", SqlDbType.BigInt).Value = receipt.ReceiptID;
                sqlCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = ClsSession.CompanyID;
                sqlCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = receipt.BranchID;
                sqlCommand.Parameters.Add("@LeadID", SqlDbType.Int).Value = receipt.LeadID;
                sqlCommand.Parameters.Add("@ReceiptNo", SqlDbType.NVarChar).Value = receipt.ReceiptNo;
                sqlCommand.Parameters.Add("@ReceiptDate", SqlDbType.DateTime).Value = receipt.ReceiptDate;
                sqlCommand.Parameters.Add("@ReceiptNoSeqNo", SqlDbType.Int).Value = receipt.ReceiptNoSeqNo;
                sqlCommand.Parameters.Add("@EMIAmt", SqlDbType.Decimal).Value = receipt.EMIAmt;
                sqlCommand.Parameters.Add("@PenaltyAmt", SqlDbType.Decimal).Value = receipt.PenaltyAmt;
                sqlCommand.Parameters.Add("@OtherAmt", SqlDbType.Decimal).Value = receipt.OtherAmt;
                sqlCommand.Parameters.Add("@TotalAmt", SqlDbType.Decimal).Value = receipt.TotalAmt;
                sqlCommand.Parameters.Add("@EntryFrom", SqlDbType.NVarChar).Value = "";
                sqlCommand.Parameters.Add("@PaymentModeID", SqlDbType.Int).Value = receipt.PaymentModeID;
                sqlCommand.Parameters.Add("@BankID", SqlDbType.Int).Value = receipt.BankID;
                sqlCommand.Parameters.Add("@ChequeBankName", SqlDbType.NVarChar).Value = receipt.ChequeBankName;
                sqlCommand.Parameters.Add("@ChequeNo", SqlDbType.NVarChar).Value = receipt.ChequeNo;
                sqlCommand.Parameters.Add("@ChequeDate", SqlDbType.DateTime).Value = receipt.ChequeDate;
                sqlCommand.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = receipt.Remarks;
                sqlCommand.Parameters.Add("@StatusID", SqlDbType.Int).Value = receipt.StatusID;
                sqlCommand.Parameters.Add("@IsDelete", SqlDbType.Bit).Value = receipt.IsDelete;
                sqlCommand.Parameters.Add("@DeletedReason", SqlDbType.NVarChar).Value = receipt.DeletedReason;
                sqlCommand.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = ClsSession.UserID;
                sqlCommand.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = ClsSession.UserID;
                sqlCommand.Parameters.AddWithValue("@ReceiptDetails", dtReceiptChargesDetails);
                dt = db.FillTableProc(sqlCommand, "USP_Receipt");

            }
        }
        return dt;
    }


    public static DataTable dbEmployee(clsEmployee cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.Add("@EmpID", SqlDbType.Int).Value = cls.EmpID;
                sqlCommand.Parameters.Add("@EmpName", SqlDbType.VarChar).Value = cls.EmpName;
                sqlCommand.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = cls.FatherName;
                sqlCommand.Parameters.Add("@MotherName", SqlDbType.VarChar).Value = cls.MotherName;
                sqlCommand.Parameters.Add("@Address", SqlDbType.VarChar).Value = cls.Address;
                sqlCommand.Parameters.Add("@StateID", SqlDbType.Int).Value = cls.StateID;
                sqlCommand.Parameters.Add("@CityID", SqlDbType.Int).Value = cls.CityID;
                sqlCommand.Parameters.Add("@ZipCode", SqlDbType.VarChar).Value = cls.ZipCode;
                sqlCommand.Parameters.Add("@ContactNo1", SqlDbType.VarChar).Value = cls.ContactNo1;
                sqlCommand.Parameters.Add("@ContactNo2", SqlDbType.VarChar).Value = cls.ContactNo2;
                sqlCommand.Parameters.Add("@WhatsAppNo", SqlDbType.VarChar).Value = cls.WhatsAppNo;
                sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = cls.Email;
                sqlCommand.Parameters.Add("@DOB", SqlDbType.Date).Value = cls.DOB == null ? (object)DBNull.Value : cls.DOB;
                sqlCommand.Parameters.Add("@PAN", SqlDbType.VarChar).Value = cls.PAN;
                sqlCommand.Parameters.Add("@AadhaarNo", SqlDbType.VarChar).Value = cls.AadhaarNo;
                sqlCommand.Parameters.Add("@MaritalStatus", SqlDbType.VarChar).Value = cls.MaritalStatus;
                sqlCommand.Parameters.Add("@ImageName", SqlDbType.VarChar).Value = cls.ImageName;
                sqlCommand.Parameters.Add("@IsDelete", SqlDbType.Int).Value = cls.IsDelete;
                sqlCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = ClsSession.CompanyID;
                sqlCommand.Parameters.Add("@Longtitute", SqlDbType.VarChar).Value = cls.Longtitute;
                sqlCommand.Parameters.Add("@Langtiute", SqlDbType.VarChar).Value = cls.Langtiute;
                sqlCommand.Parameters.Add("@EmployeeAttnStatus", SqlDbType.Int).Value = cls.EmployeeAttnStatus;
                sqlCommand.Parameters.Add("@AttnDate", SqlDbType.DateTime).Value = cls.AttnDate == null ? (object)DBNull.Value : cls.AttnDate;
                sqlCommand.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = ClsSession.UserID;
                sqlCommand.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = ClsSession.UserID;
                sqlCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = cls.BranchId;
                sqlCommand.Parameters.Add("@RoleId", SqlDbType.Int).Value = cls.RoleId;
                dt = db.FillTableProc(sqlCommand, "USP_Employee");
            }
        }
        return dt;
    }

    public static DataTable dbEmployeeDetails(clsEmployeeDetails cls)
    {
        DataTable dt = new DataTable();
        try
        {
            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                    sqlCommand.Parameters.AddWithValue("@EmpDtlID", cls.EmpDtlID);
                    sqlCommand.Parameters.AddWithValue("@EmpID", cls.EmpID);
                    sqlCommand.Parameters.AddWithValue("@DesignationID", cls.DesignationID);
                    sqlCommand.Parameters.AddWithValue("@DepartmentId", cls.DepartmentId);
                    sqlCommand.Parameters.AddWithValue("@DOJ", cls.DOJ);
                    sqlCommand.Parameters.AddWithValue("@LastESICNo", cls.LastESICNo);
                    sqlCommand.Parameters.AddWithValue("@LastPFNo", cls.LastPFNo);
                    sqlCommand.Parameters.AddWithValue("@LastAcadmicDegree", cls.LastAcadmicDegree);
                    sqlCommand.Parameters.AddWithValue("@LastProfDegree", cls.LastProfDegree);
                    sqlCommand.Parameters.AddWithValue("@LastCompany", cls.LastCompany);
                    sqlCommand.Parameters.AddWithValue("@LastExperienceDtls", cls.LastExperienceDtls);
                    sqlCommand.Parameters.AddWithValue("@Salary", cls.Salary);
                    sqlCommand.Parameters.AddWithValue("@IsLeave", cls.IsLeave);
                    sqlCommand.Parameters.AddWithValue("@DOL", cls.DOL);
                    sqlCommand.Parameters.AddWithValue("@LoginType", cls.LoginType);
                    sqlCommand.Parameters.AddWithValue("@Companyid", cls.Companyid);
                    sqlCommand.Parameters.AddWithValue("@IsActive", cls.IsActive);
                    sqlCommand.Parameters.AddWithValue("@IsDelete", cls.IsDelete);
                    sqlCommand.Parameters.AddWithValue("@Longtitute", cls.Longtitute);
                    sqlCommand.Parameters.AddWithValue("@Langtiute", cls.Langtiute);
                    dt = db.FillTableProc(sqlCommand, "USP_EmployeeDetails");

                }
            }
        }
        catch (Exception ex)
        { }
        return dt;
    }
    public static DataTable dbStageRole(clsStageRole cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@StageRoleId", cls.StageRoleId);
                sqlCommand.Parameters.AddWithValue("@StageRoleEmpId", cls.StageRoleEmpId);
                sqlCommand.Parameters.AddWithValue("@StageRoleEmpCode", cls.StageRoleEmpCode);
                sqlCommand.Parameters.AddWithValue("@StageRoleEmpName", cls.StageRoleEmpName);
                sqlCommand.Parameters.AddWithValue("@StageRoleName", cls.StageRoleName);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", cls.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@ISDELETE", cls.IsDelete);
                sqlCommand.Parameters.AddWithValue("@COMPANYID", cls.CompanyID);
                dt = db.FillTableProc(sqlCommand, "USP_StageRole");
            }
        }
        return dt;
    }

    public static DataTable dbStageMasterddl()
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", "ViewStage");
                sqlCommand.Parameters.AddWithValue("@IsActive", "1");
                dt = db.FillTableProc(sqlCommand, "USP_StageMaster");
            }
        }
        return dt;
    }


    public static DataTable dbLeadDocument(clsLeadDocument cls)
    {
        DataTable dt = new DataTable();
        try
        {
            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;
                    cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = cls.LeadId;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;

                    cmd.Parameters.Add("@DcId", SqlDbType.Int).Value = cls.DcId;
                    cmd.Parameters.Add("@DocID", SqlDbType.Int).Value = cls.DocID;
                    cmd.Parameters.Add("@CustomerType", SqlDbType.VarChar).Value = cls.CustomerType;
                    cmd.Parameters.Add("@IsReceived", SqlDbType.Bit).Value = cls.IsReceived;
                    cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = cls.Remarks;
                    cmd.Parameters.Add("@IsDelete", SqlDbType.Bit).Value = cls.IsDelete;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = cls.CreatedBy;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = cls.UpdatedBy;
                    cmd.Parameters.Add("@LeadCustId", SqlDbType.Int).Value = cls.LeadCustId;
                    dt = db.FillTableProc(cmd, "USP_LeadDocument");
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
    public static DataTable dbLeadDocSign(clsLeadDocSign cls)
    {
        DataTable dt = new DataTable();
        try
        {
            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;

                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                    cmd.Parameters.Add("@DocSignId", SqlDbType.Int).Value = cls.DocSignId;
                    cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = cls.LeadId;
                    //cmd.Parameters.Add("@LeadNo", SqlDbType.Int).Value = cls.LeadNo;

                    cmd.Parameters.Add("@Documents", SqlDbType.VarChar).Value = cls.Documents;
                    cmd.Parameters.Add("@SanctionLetter", SqlDbType.VarChar).Value = cls.SanctionLetter;
                    cmd.Parameters.Add("@LoanAgrmentKit", SqlDbType.VarChar).Value = cls.LoanAgrmentKit;
                    cmd.Parameters.Add("@PDC", SqlDbType.VarChar).Value = cls.PDC;
                    cmd.Parameters.Add("@NACH", SqlDbType.VarChar).Value = cls.NACH;
                    cmd.Parameters.Add("@DisbursmentKit", SqlDbType.VarChar).Value = cls.DisbursmentKit;
                    cmd.Parameters.Add("@InsuranceWithHP", SqlDbType.VarChar).Value = cls.InsuranceWithHP;
                    cmd.Parameters.Add("@NOC", SqlDbType.VarChar).Value = cls.NOC;
                    cmd.Parameters.Add("@RTOSlip", SqlDbType.VarChar).Value = cls.RTOSlip;
                    cmd.Parameters.Add("@OrignalPropertyPaper", SqlDbType.VarChar).Value = cls.OrignalPropertyPaper;
                    cmd.Parameters.Add("@RegisteredMortgageDeed", SqlDbType.VarChar).Value = cls.RegisteredMortgageDeed;
                    cmd.Parameters.Add("@EquitableMortageDeed", SqlDbType.VarChar).Value = cls.EquitableMortageDeed;
                    cmd.Parameters.Add("@Affidavit", SqlDbType.VarChar).Value = cls.Affidavit;

                    cmd.Parameters.Add("@DsRemark", SqlDbType.VarChar).Value = cls.DsRemark;
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = ClsSession.UserID;
                    cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = ClsSession.UserID;
                    cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = cls.IsDelete;
                    cmd.Parameters.Add("@StageId", SqlDbType.Int).Value = cls.StageId;
                    cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = cls.Remarks;
                    cmd.Parameters.Add("@BorrowerKyc", SqlDbType.VarChar).Value = cls.BorrowerKyc;
                    cmd.Parameters.Add("@CoBorrowerKyc", SqlDbType.VarChar).Value = cls.CoBorrowerKyc;
                    cmd.Parameters.Add("@GuarantorKyc", SqlDbType.VarChar).Value = cls.GuarantorKyc;
                    cmd.Parameters.Add("@BorrowerPhoto", SqlDbType.VarChar).Value = cls.BorrowerPhoto;
                    cmd.Parameters.Add("@CoBorrowerPhoto", SqlDbType.VarChar).Value = cls.CoBorrowerPhoto;
                    cmd.Parameters.Add("@GuarantorPhoto", SqlDbType.VarChar).Value = cls.GuarantorPhoto;
                    cmd.Parameters.Add("@DisbursementRequestLetter", SqlDbType.VarChar).Value = cls.DisbursementRequestLetter;
                    cmd.Parameters.Add("@SignatureVerification", SqlDbType.VarChar).Value = cls.SignatureVerification;
                    cmd.Parameters.Add("@KycSelfAttested", SqlDbType.VarChar).Value = cls.KycSelfAttested;

                    dt = db.FillTableProc(cmd, "USP_LeadDocSign");
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

    //public static DataTable dbLeadDocumentCollection(clsLeadDocSign cls)
    //{
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        using (DBOperation db = new DBOperation())
    //        {
    //            using (SqlCommand cmd = new SqlCommand())
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;

    //                cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
    //                cmd.Parameters.Add("@DocSignId", SqlDbType.Int).Value = cls.DocSignId;
    //                cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = cls.LeadId;
    //                //cmd.Parameters.Add("@LeadNo", SqlDbType.Int).Value = cls.LeadNo;

    //                cmd.Parameters.Add("@Documents", SqlDbType.Int).Value = cls.Documents;
    //                cmd.Parameters.Add("@SanctionLetter", SqlDbType.Int).Value = cls.SanctionLetter;
    //                cmd.Parameters.Add("@LoanAgrmentKit", SqlDbType.VarChar).Value = cls.LoanAgrmentKit;
    //                cmd.Parameters.Add("@PDC", SqlDbType.Bit).Value = cls.PDC;
    //                cmd.Parameters.Add("@NACH", SqlDbType.VarChar).Value = cls.NACH;
    //                cmd.Parameters.Add("@DisbursmentKit", SqlDbType.Bit).Value = cls.DisbursmentKit;
    //                cmd.Parameters.Add("@InsuranceWithHP", SqlDbType.Int).Value = cls.InsuranceWithHP;
    //                cmd.Parameters.Add("@NOC", SqlDbType.VarChar).Value = cls.NOC;
    //                cmd.Parameters.Add("@RTOSlip", SqlDbType.Bit).Value = cls.RTOSlip;
    //                cmd.Parameters.Add("@OrignalPropertyPaper", SqlDbType.VarChar).Value = cls.OrignalPropertyPaper;
    //                cmd.Parameters.Add("@RegisterdMortageDeed", SqlDbType.Bit).Value = cls.RegisterdMortageDeed;
    //                cmd.Parameters.Add("@EquitableMortageDeed", SqlDbType.Int).Value = cls.EquitableMortageDeed;
    //                cmd.Parameters.Add("@Affidavit", SqlDbType.Bit).Value = cls.Affidavit;
    //                cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = cls.Remarks;
    //                cmd.Parameters.Add("@CreatedBy", SqlDbType.Bit).Value = cls.CreatedBy;
    //                cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = cls.UpdatedBy;
    //                cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = cls.IsDelete;
    //                dt = db.FillTableProc(cmd, "USP_LeadDocSign");
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        dt.Dispose();
    //    }
    //    return dt;
    //}
    public static DataTable dbLeadFinalApprove(clsLeadFinalApprove cls)
    {
        DataTable dt = new DataTable();
        try
        {
            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;
                    //cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                    cmd.Parameters.Add("@FinalApproveId", SqlDbType.Int).Value = cls.FinalApproveId;
                    cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = cls.LeadId;
                    //cmd.Parameters.Add("@LeadNo", SqlDbType.Int).Value = cls.LeadNo;

                    cmd.Parameters.AddWithValue("@Particulers", cls.Particulers);
                    cmd.Parameters.AddWithValue("@Proccesfees", cls.Proccesfees);
                    cmd.Parameters.AddWithValue("@AdvanceEMI", cls.AdvanceEMI);
                    cmd.Parameters.AddWithValue("@GST", cls.GST);
                    cmd.Parameters.AddWithValue("@NetDisbAmt", cls.NetDisbAmt);
                    cmd.Parameters.AddWithValue("@TrnchsNo", cls.TrnchsNo);
                    cmd.Parameters.AddWithValue("@CersaiCharges", cls.CersaiCharges);
                    cmd.Parameters.AddWithValue("@StamppingCharges", cls.StamppingCharges);
                    cmd.Parameters.AddWithValue("@Remarks", cls.Remarks);
                    cmd.Parameters.AddWithValue("@CreatedBy", cls.CreatedBy);
                    cmd.Parameters.AddWithValue("@UpdatedBy", cls.UpdatedBy);
                    cmd.Parameters.AddWithValue("@IsDelete", cls.IsDelete);
                    cmd.Parameters.AddWithValue("@CompanyId", ClsSession.CompanyID);
                    cmd.Parameters.AddWithValue("@BorrowerKyc", cls.BorrowerKyc);
                    cmd.Parameters.AddWithValue("@GuarantorKyc", cls.GuarantorKyc);
                    cmd.Parameters.AddWithValue("@PDC", cls.PDC);
                    cmd.Parameters.AddWithValue("@BorrowerPhoto", cls.BorrowerPhoto);
                    cmd.Parameters.AddWithValue("@CoBorrowerPhoto", cls.CoBorrowerPhoto);
                    cmd.Parameters.AddWithValue("@GuarantorPhoto", cls.GuarantorPhoto);
                    cmd.Parameters.AddWithValue("@SanctionLetter", cls.SanctionLetter);
                    cmd.Parameters.AddWithValue("@LoanAgreementkit", cls.LoanAgreementkit);
                    cmd.Parameters.AddWithValue("@DisbursementRequestLetter", cls.DisbursementRequestLetter);
                    cmd.Parameters.AddWithValue("@NocPreviousFinanced", cls.NocPreviousFinanced);
                    cmd.Parameters.AddWithValue("@Rtoslip", cls.Rtoslip);

                    dt = db.FillTableProc(cmd, "USP_LeadFinalApprove");
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


    public static DataSet dbDisbursement(clsDisbursement cls)
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReqType", SqlDbType.VarChar).Value = cls.ReqType;
                    cmd.Parameters.Add("@DisbursementId", SqlDbType.Int).Value = cls.DisbursementId;
                    cmd.Parameters.Add("@LeadId", SqlDbType.Int).Value = cls.LeadId;
                    cmd.Parameters.AddWithValue("@NetDisbursementAmount", cls.NetDisbursementAmount);
                    cmd.Parameters.AddWithValue("@IfscCode", cls.IfscCode);
                    cmd.Parameters.AddWithValue("@BeneficiaryAccountNo", cls.BeneficiaryAccountNo);
                    cmd.Parameters.AddWithValue("@BeneficiaryName", cls.BeneficiaryName);
                    cmd.Parameters.AddWithValue("@ROI", cls.ROI);
                    cmd.Parameters.AddWithValue("@Tenure", cls.Tenure);
                    cmd.Parameters.AddWithValue("@PaymentMode", cls.MiscId);
                    cmd.Parameters.AddWithValue("@UtrNo", cls.UtrNo);
                    cmd.Parameters.AddWithValue("@LoanStartDate", cls.LoanStartDate);
                    cmd.Parameters.AddWithValue("@DRemark", cls.DRemark);
                    cmd.Parameters.AddWithValue("@CreatedBy", cls.CreatedBy);
                    cmd.Parameters.AddWithValue("@UpdatedBy", cls.UpdatedBy);
                    cmd.Parameters.AddWithValue("@IsDelete", cls.IsDelete);
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = ClsSession.CompanyID;
                    cmd.Parameters.AddWithValue("@LeadNo", cls.LeadNo);
                    cmd.Parameters.AddWithValue("@StageId", cls.StageId);
                    cmd.Parameters.AddWithValue("@Remarks", cls.Remarks);
                    ds = db.FillDsProc(cmd, "USP_Disbursement");
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ds.Dispose();
        }
        return ds;
    }


    public static DataSet dbGetEMI(clsEmi cls)
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LoanAmount", cls.PRINCIPAL);
                    cmd.Parameters.AddWithValue("@InterestRate", cls.INTEREST);
                    cmd.Parameters.AddWithValue("@LoanPeriod", cls.PERIOD);
                    cmd.Parameters.AddWithValue("@StartDate", cls.StartPaymentDate ?? (object)DBNull.Value);
                    ds = db.FillDsProc(cmd, "USP_GetEMI");
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ds.Dispose();
        }
        return ds;
    }
    public static DataTable dbGetInstallment(int LeadId)
    {
        DataTable ds = new DataTable();
        try
        {
            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ReqType", "GetInstallment");
                    cmd.Parameters.AddWithValue("@LeadId", LeadId);
                    ds = db.FillTableProc(cmd, "USP_Lead");
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ds.Dispose();
        }
        return ds;
    }


    public static DataTable dbLedgerMaster(clsLedgerMaster cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@LedgerID", cls.LedgerID);
                sqlCommand.Parameters.AddWithValue("@CompanyID", ClsSession.CompanyID);
                sqlCommand.Parameters.AddWithValue("@LedgerName", cls.LedgerName);
                sqlCommand.Parameters.AddWithValue("@LedgerCode", cls.LedgerCode);
                sqlCommand.Parameters.AddWithValue("@LedgerGroupID", cls.LedgerGroupID);
                sqlCommand.Parameters.AddWithValue("@StatusID", cls.StatusID);
                sqlCommand.Parameters.AddWithValue("@IsDelete", cls.IsDelete);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", cls.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", cls.UpdatedBy);
                dt = db.FillTableProc(sqlCommand, "USP_Ledgermaster");
            }
        }
        return dt;
    }

    public static DataTable dbChargesType(clsChargesType cls)
    {
        DataTable dt = new DataTable();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@ChargeTypeID", cls.ChargeTypeID);
                sqlCommand.Parameters.AddWithValue("@ChargeTypeName", cls.ChargeTypeName);
                sqlCommand.Parameters.AddWithValue("@ChargeTypeCode", cls.ChargeTypeCode);
                sqlCommand.Parameters.AddWithValue("@KeyID", cls.KeyID);
                sqlCommand.Parameters.AddWithValue("@LedgerID", cls.LedgerID);
                sqlCommand.Parameters.AddWithValue("@IsDelete", cls.IsDelete);
                sqlCommand.Parameters.AddWithValue("@CompanyID", ClsSession.CompanyID);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", cls.CreatedBy);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", cls.UpdatedBy);
                dt = db.FillTableProc(sqlCommand, "USP_ChargesType");
            }
        }
        return dt;
    }


    public static DataSet dbForeClose(clsForecloseEntry cls)
    {
        DataSet ds = new DataSet();
        using (DBOperation db = new DBOperation())
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReqType", cls.ReqType);
                sqlCommand.Parameters.AddWithValue("@CompanyId", ClsSession.CompanyID);
                sqlCommand.Parameters.AddWithValue("@LeadNo", cls.SearchLeadNo);
                sqlCommand.Parameters.AddWithValue("@LeadId", cls.LeadId);
                sqlCommand.Parameters.AddWithValue("@PrincipalOutstanding", cls.pos);
                sqlCommand.Parameters.AddWithValue("@CurrentMonthInterest", cls.CurrentMonthInterest);
                sqlCommand.Parameters.AddWithValue("@InstallmentOverdue", cls.InstalmentOverdue);
                sqlCommand.Parameters.AddWithValue("@BouncingCharges", cls.BouncingCharges);
                sqlCommand.Parameters.AddWithValue("@ExcessAmount", cls.ExcessAmount);
                sqlCommand.Parameters.AddWithValue("@PenalCharges", cls.PenalCharges);
                sqlCommand.Parameters.AddWithValue("@OtherCharges", cls.OtherCharges);
                sqlCommand.Parameters.AddWithValue("@ForeclosureCharges", cls.ForeclosureCharges);
                sqlCommand.Parameters.AddWithValue("@GstOnForclose", cls.GstOnForclose);
                sqlCommand.Parameters.AddWithValue("@FinalForeclosureAmount", cls.FinalForeclosureAmount);
                ds = db.FillDsProc(sqlCommand, "Usp_ForeClose");
            }
        }
        return ds;
    }

    public static DataSet dbSMSMaster(clsSMSMaster cls)
    {
        DataSet ds = new DataSet();
        try
        {
            using (DBOperation db = new DBOperation())
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@SMSType", cls.SMSType);
                    sqlCommand.Parameters.AddWithValue("@LeadId", cls.LeadId);
                    ds = db.FillDsProc(sqlCommand, "USP_SMSMaster");
                }
            }

        }
        catch
        {
            return ds;
        }
        finally
        {
        }
        return ds;

    }



    #region Update Lead Status
    public static DataTable UpdateLeadStatus(clsLeadmaind cls)
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

    public static DataTable UpdateLeadStatusDc(clsLeadDocSign cls)
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

    public static DataTable UpdateLeadStatusFa(clsLeadFinalApproveMain cls)
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




    public static DataTable UpdateLeadStatusDisburse(clsDisbursement cls)
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

    //commom fun
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

    //==== start
    // Flag: Has Dispose already been called?
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

    ~DataInterface1()
    {
        Dispose(false);
    }





    //==== end
}