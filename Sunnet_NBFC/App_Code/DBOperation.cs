using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;
public class DBOperation:IDisposable
{
    private SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);

    public SqlConnection GetConnection()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        catch (Exception e1)
        {
            Console.Write("SqlConnection" + e1.Message);
        }
        return con;
    }

    public void CloseConnection()
    {
        try
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception e1)
        {
            Console.Write("CloseConnection" + e1.Message);
        }

    }

    public int ExecuteNonQuery(string ssql)
    {

        int updateRows = 0;
        try
        {
            SqlCommand command = new SqlCommand
            {
                Connection = GetConnection(),
                CommandType = CommandType.Text,
                CommandText = ssql
            };
            updateRows = command.ExecuteNonQuery();
            CloseConnection();
        }
        catch (Exception e1)
        {
            Console.Write("ExecuteNonQuery" + e1.Message);
        }
        return updateRows;
    }

    public int ExecuteNonQueryProcedure(SqlCommand cmd, string ProcedureName)
    {

        int updateRows = 0;
        try
        {
            cmd.CommandText = ProcedureName;
            cmd.Connection = GetConnection();
            cmd.CommandType = CommandType.StoredProcedure;

            updateRows = cmd.ExecuteNonQuery();
            CloseConnection();
        }
        catch (Exception e1)
        {
            HttpContext.Current.Session["error"] = e1.Message.ToString();
            throw e1;
            //HttpContext.Current.Response.Redirect("~/Error.aspx", true);
            // Console.Write("ExecuteNonQuery" + e1.Message);
        }
        return updateRows;
    }

    public DataTable FillTableProc(SqlCommand cmd, string ProcedureName)
    {
        DataTable dt = new DataTable();
       
        try
        {
            cmd.CommandText = ProcedureName;
            cmd.CommandTimeout = 0;
            cmd.Connection = GetConnection();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter ad = new SqlDataAdapter
            {
                SelectCommand = cmd
            };
            ad.Fill(dt);
            CloseConnection();
        }
        catch (Exception e1)
        {
            Console.WriteLine(e1.Message);
            throw e1;
            //HttpContext.Current.Response.Redirect("~/Error.aspx", true);
            // Console.Write("ExecuteNonQuery" + e1.Message);
        }
        return dt;
    }
    public DataSet FillDsProc(SqlCommand cmd, string ProcedureName)
    {
        DataSet dt = new DataSet();

        try
        {
            cmd.CommandText = ProcedureName;
            cmd.Connection = GetConnection();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter ad = new SqlDataAdapter
            {
                SelectCommand = cmd
            };
            ad.Fill(dt);
            CloseConnection();
        }
        catch (Exception e1)
        {
            HttpContext.Current.Session["error"] = e1.Message.ToString();
            throw e1;

            //HttpContext.Current.Response.Redirect("~/Error.aspx", true);
            // Console.Write("ExecuteNonQuery" + e1.Message);
        }
        return dt;
    }
    public int ExecuteNonStoreProc(string ProcName, string ParameterList, string ParameterValue)
    {

        int updateRows = 0;
        try
        {

            SqlCommand command = new SqlCommand
            {
                Connection = GetConnection(),
                CommandType = CommandType.StoredProcedure,
                CommandText = ProcName
            };

            for (int i = 0; i <= ParameterList.Split(',').Length - 1; i++)
            {
                command.Parameters.AddWithValue(ParameterList.Split(',')[i], ParameterValue.Split('-')[i]);
            }
            updateRows = command.ExecuteNonQuery();
            CloseConnection();
        }
        catch (Exception e1)
        {
            Console.Write("ExecuteNonQuery" + e1.Message);
        }
        return updateRows;
    }

    public DataSet FillDatasetStoreProc(string ProcName, string ParameterList, string ParameterValue)
    {

        DataSet ds = new DataSet();
        try
        {

            SqlCommand command = new SqlCommand
            {
                Connection = GetConnection(),
                CommandType = CommandType.StoredProcedure,
                CommandText = ProcName
            };

            for (int i = 0; i <= ParameterList.Split(',').Length - 1; i++)
            {
                command.Parameters.AddWithValue(ParameterList.Split(',')[i], ParameterValue.Split('-')[i]);
            }
            SqlDataAdapter ad = new SqlDataAdapter
            {
                SelectCommand = command
            };
            ad.Fill(ds);
            CloseConnection();
        }
        catch (Exception e1)
        {
            Console.Write("ExecuteNonQuery" + e1.Message);
        }
        return ds;
    }
    public DataTable FillDatatableStoreProc(string ProcName, string ParameterList, string ParameterValue)
    {

        DataTable dt = new DataTable();
        try
        {

            SqlCommand command = new SqlCommand
            {
                Connection = GetConnection(),
                CommandType = CommandType.StoredProcedure,
                CommandText = ProcName
            };

            for (int i = 0; i <= ParameterList.Split(',').Length - 1; i++)
            {
                command.Parameters.AddWithValue(ParameterList.Split(',')[i], ParameterValue.Split('-')[i]);
            }
            SqlDataAdapter ad = new SqlDataAdapter
            {
                SelectCommand = command
            };
            ad.Fill(dt);
            CloseConnection();
        }
        catch (Exception e1)
        {
            Console.Write("ExecuteNonQuery" + e1.Message);
        }
        return dt;
    }

    public DataTable ExecuteTable(string ssql)
    {

        DataTable dt = new DataTable();
        try
        {
            SqlCommand command = new SqlCommand
            {
                Connection = GetConnection(),
                CommandType = CommandType.Text,
                CommandText = ssql
            };
            SqlDataAdapter ad = new SqlDataAdapter
            {
                SelectCommand = command
            };
            ad.Fill(dt);

            CloseConnection();
        }
        catch (Exception e1)
        {
            Console.Write("ExecuteTable" + e1.Message);
        }
        return dt;
    }

    public DataSet ExecuteDataset(string ssql)
    {

        DataSet ds = new DataSet();
        try
        {
            SqlCommand command = new SqlCommand
            {
                Connection = GetConnection(),
                CommandType = CommandType.Text,
                CommandText = ssql
            };
            SqlDataAdapter ad = new SqlDataAdapter
            {
                SelectCommand = command
            };
            ad.Fill(ds);

            CloseConnection();
        }
        catch (Exception e1)
        {
            Console.Write("ExecuteTable" + e1.Message);
        }
        return ds;
    }


    public string ExecuteScalerValue(SqlCommand cmd, string ProcedureName)
    {

        string RetVal = "";
        try
        {
            cmd.CommandText = ProcedureName;
            cmd.Connection = GetConnection();
            cmd.CommandType = CommandType.StoredProcedure;

            RetVal = Convert.ToString(cmd.ExecuteScalar());
            CloseConnection();
        }
        catch (Exception e1)
        {
            HttpContext.Current.Session["error"] = e1.Message.ToString();
            HttpContext.Current.Response.Redirect("~/Error.aspx", true);
            // Console.Write("ExecuteNonQuery" + e1.Message);
        }
        return RetVal;
    }

    public string ExecuteScalerValue(string ssql)
    {
        string value = "";
        try
        {
            SqlCommand command = new SqlCommand
            {
                Connection = GetConnection(),
                CommandType = CommandType.Text,
                CommandText = ssql
            };
            SqlDataAdapter ad = new SqlDataAdapter
            {
                SelectCommand = command
            };
            value = Convert.ToString(command.ExecuteScalar());

            CloseConnection();
        }
        catch (Exception e1)
        {
            Console.Write("ExecuteScalerValue" + e1.Message);
        }
        return value;
    }

    public void FillDropDownlist(DataTable dt, DropDownList ddl, string valuemember, string displaymember)
    {
        ddl.Items.Clear();
        //ddl.Items.Add(new ListItem("Select", "0"));
        ddl.DataSource = dt;
        ddl.DataValueField = valuemember;
        ddl.DataTextField = displaymember;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        //ddl.Items.Insert(0, "Select");

    }

    public void FiiCheckBoxList(DataTable dt, CheckBoxList chl, string valuemember, string displaymember)
    {
        chl.Items.Clear();
        //chl.Items.Add(new ListItem("Select", "0"));
        chl.DataSource = dt;
        chl.DataValueField = valuemember;
        chl.DataTextField = displaymember;
        chl.DataBind();
        // chl.Items.Insert(0, new ListItem("Select", "0"));
        //ddl.Items.Insert(0, "Select");

    }

    bool disposed = false;

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    ~DBOperation()
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
