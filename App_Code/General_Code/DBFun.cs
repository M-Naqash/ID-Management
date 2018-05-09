using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Collections;

public class DBFun
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static SqlDataAdapter da = new SqlDataAdapter();
    static SqlConnection con;
    static DataTable dt;
    static string ConName = "constring";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public void OpenCon()
    {
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings[ConName].ConnectionString);
            if (con.State != ConnectionState.Open) { con.Open(); }
        }
        catch (Exception ex) { throw ex; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public void CloseCon()
    {
        try
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
        }
        catch (Exception ex) { throw ex; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	static public DataTable DaFetchDataQuery(string pQuery) //
    {
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings[ConName].ConnectionString);
            OpenCon();
            da.SelectCommand = new SqlCommand(pQuery, con);
            da.SelectCommand.CommandType = CommandType.Text;

            SqlDataReader dr = da.SelectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            CloseCon();
            return dt;
        }
        catch (Exception ex) { throw ex; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public DataTable FetchData(string pQuery)
    {
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings[ConName].ConnectionString);
            OpenCon();
            da.SelectCommand = new SqlCommand("FetchData", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@Query", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pQuery);
            da.SelectCommand.Parameters.Add(param);
            
            SqlDataReader dr = da.SelectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            CloseCon();
            return dt;
        }
        catch (Exception ex) { throw ex; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static DataTable FetchData(string Query, string[] Param)
    {
        try
        {
            if (string.IsNullOrEmpty(Query) || Param.Length <= 0) { return new DataTable(); }

            SqlCommand cmd = new SqlCommand(Query);     
            for (int i = 0; i < Param.Length; i++) { cmd.Parameters.AddWithValue("@P" + (i+1).ToString(), Param[i]); }
            
            OpenCon();
            cmd.CommandType = CommandType.Text;
            cmd.Connection  = con;
            
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DataTable FDT = new DataTable();
            FDT.Load(dr);
            dr.Close();
            return FDT;
        }
        catch (Exception ex) { throw ex; }
        finally 
        { 
            CloseCon(); 
            con.Dispose();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public int InsertData(string pQuery)
    {
        try
        {
            if (string.IsNullOrEmpty(pQuery)) { return 0; }
            con = new SqlConnection(ConfigurationManager.ConnectionStrings[ConName].ConnectionString);
            OpenCon();

            da.InsertCommand = new SqlCommand("ExecuteData", con);
            da.InsertCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@Query", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pQuery);
            da.InsertCommand.Parameters.Add(param);

            int rowsAffected = Convert.ToInt32(da.InsertCommand.ExecuteScalar());
            CloseCon();
            return rowsAffected;
        }
        catch (Exception ex) { return 1; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public int ExecuteData(string pQuery)
    {
        if (string.IsNullOrEmpty(pQuery)) { return 0; }
        con = new SqlConnection(ConfigurationManager.ConnectionStrings[ConName].ConnectionString);
        OpenCon();

        da.InsertCommand = new SqlCommand("ExecuteData", con);
        da.InsertCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter param = new SqlParameter("@Query", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pQuery);
        da.InsertCommand.Parameters.Add(param);

        int rowsAffected = da.InsertCommand.ExecuteNonQuery();
        CloseCon();
        return rowsAffected;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public bool IsNullOrEmpty(DataTable dt)
    {
        if (dt == null) { return true; }
        if (dt.Rows.Count == 0) { return true; }
        return false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public void InsertError(string errorPage,string errorFunction)
    {
        string userID = ((string)HttpContext.Current.Session["UserName"]);
        string userRole = Convert.ToString(HttpContext.Current.Session["Role"]);
        //string errorPage = "EmployeMaster.aspx";
        //string errorFunction = "btnSaveInsert";
        DateTime dt = DateTime.Now;
        StringBuilder errorQuery = new StringBuilder();
        errorQuery.Append("INSERT INTO [ErrorLog]([ErrorPage],[ErrorFunction],[ErrorDate] ,[LoginUserID],[LoginUserRole]) VALUES ('" + errorPage + "','" + errorFunction + "'");
        errorQuery.Append(",'" + DateFun.HijToGrn(DateFun.GrnToHij(dt)) + "','" + userID + "','" + userRole + "')");

        Int32 returnValue = ExecuteData(errorQuery.ToString());
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}