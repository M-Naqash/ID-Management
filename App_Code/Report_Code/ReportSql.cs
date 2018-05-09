using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

public class ReportSql : DataLayerBase
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool UpdateTemplate(ReportPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[Report_UpdateTemplate]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@RepID"     , SqlDbType.VarChar, 10   , ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.RepID));
            sqlCommand.Parameters.Add(new SqlParameter("@RepTemp"   , SqlDbType.VarChar, 50000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.RepTemp));
            sqlCommand.Parameters.Add(new SqlParameter("@Lang"      , SqlDbType.VarChar, 500  , ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.Lang));
            sqlCommand.Parameters.Add(new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50   , ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.ModifiedBy));
            //sqlCommand.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.ModifiedDate));

            MainConnection.Open();

            sqlCommand.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
            MainConnection.Close();
            sqlCommand.Dispose();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}