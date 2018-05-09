using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

public class NationalitySql : DataLayerBase
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Insert(NationalityPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[Nationality_Insert]",MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@NatID", SqlDbType.Int, 10, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, pro.PKID));
            sqlCommand.Parameters.Add(new SqlParameter("@NatNameAr", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.NameAr));
            sqlCommand.Parameters.Add(new SqlParameter("@NatNameEn", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.NameEn));
            sqlCommand.Parameters.Add(new SqlParameter("@CountryNameAr", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.CountryNameAr));
            sqlCommand.Parameters.Add(new SqlParameter("@CountryNameEn", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.CountryNameEn));
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
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Update(NationalityPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[Nationality_Update]",MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@NatID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.PKID));
            sqlCommand.Parameters.Add(new SqlParameter("@NatNameAr", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.NameAr));
            sqlCommand.Parameters.Add(new SqlParameter("@NatNameEn", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.NameEn));
            sqlCommand.Parameters.Add(new SqlParameter("@CountryNameAr", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.CountryNameAr));
            sqlCommand.Parameters.Add(new SqlParameter("@CountryNameEn", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.CountryNameEn));
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
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Delete(NationalityPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[Nationality_Delete]",MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@NatID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.PKID));
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
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}