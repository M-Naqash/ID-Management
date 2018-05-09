using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

public class SectionsSql : DataLayerBase
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Insert(SectionsPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[SectionsExternal_Insert]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@SecID", SqlDbType.Int, 10, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, pro.PKID));
            sqlCommand.Parameters.Add(new SqlParameter("@SecNameEn", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.NameEn));
            sqlCommand.Parameters.Add(new SqlParameter("@SecNameAr", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.NameAr));
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
    public bool Update(SectionsPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[SectionsExternal_Update]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@SecID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.PKID));
            sqlCommand.Parameters.Add(new SqlParameter("@SecNameEn", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.NameEn));
            sqlCommand.Parameters.Add(new SqlParameter("@SecNameAr", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.NameAr));
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
    public bool Delete(SectionsPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[SectionsExternal_Delete]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@SecID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.PKID));
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