using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

public class CompaniesSql : DataLayerBase
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Insert(CompaniesPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[Companies_Insert]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@CompID", SqlDbType.Int, 10, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, pro.PKID));
            sqlCommand.Parameters.Add(new SqlParameter("@CompNameEn", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.NameEn));
            sqlCommand.Parameters.Add(new SqlParameter("@CompNameAr", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.NameAr));
            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.TransactionBy));

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
    public bool Update(CompaniesPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[Companies_Update]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@CompID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.PKID));
            sqlCommand.Parameters.Add(new SqlParameter("@CompNameEn", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.NameEn));
            sqlCommand.Parameters.Add(new SqlParameter("@CompNameAr", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.NameAr));
            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.TransactionBy));

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
    public bool Delete(CompaniesPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[Companies_Delete]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@CompID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.PKID));
            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.TransactionBy));

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