using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

public class IssuesSql : DataLayerBase
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public int Insert(IssuesPro businessObject)
    {
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandText = "dbo.[Issue_Insert]";
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Connection = MainConnection;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@IsID", SqlDbType.Int, 10, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IsID));
            sqlCommand.Parameters.Add(new SqlParameter("@IsNameEn", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IsNameEn));         
            sqlCommand.Parameters.Add(new SqlParameter("@IsNameAr", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IsNameAr));
            sqlCommand.Parameters.Add(new SqlParameter("@IsDescription", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IsDescription));
            sqlCommand.Parameters.Add(new SqlParameter("@IsType", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IsType));
            sqlCommand.Parameters.Add(new SqlParameter("@IsRepeat", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IsRepeat));
            sqlCommand.Parameters.Add(new SqlParameter("@ISCondition", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.ISCondition));

            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.TransactionBy));

            MainConnection.Open();

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            rowsAffected = Convert.ToInt32(sqlCommand.Parameters["@IsID"].Value);
            return rowsAffected;
        }
        catch (Exception ex)
        {
            throw new Exception("Issue::Insert::Error occured.", ex);
        }
        finally
        {
            MainConnection.Close();
            sqlCommand.Dispose();
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Update(IssuesPro businessObject)
    {
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandText = "dbo.[Issue_Update]";
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Connection = MainConnection;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@IsID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IsID));
            sqlCommand.Parameters.Add(new SqlParameter("@IsNameEn", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IsNameEn));         
            sqlCommand.Parameters.Add(new SqlParameter("@IsNameAr", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IsNameAr));
            sqlCommand.Parameters.Add(new SqlParameter("@IsDescription", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IsDescription));
            sqlCommand.Parameters.Add(new SqlParameter("@IsType", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IsType));
            sqlCommand.Parameters.Add(new SqlParameter("@IsRepeat", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IsRepeat));
            sqlCommand.Parameters.Add(new SqlParameter("@ISCondition", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.ISCondition));

            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.TransactionBy));

            MainConnection.Open();

            sqlCommand.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Issue::Update::Error occured.", ex);
        }
        finally
        {
            MainConnection.Close();
            sqlCommand.Dispose();
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Delete(string pID, string TransactionBy)
    {
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandText = "dbo.[Issue_Delete]";
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Connection = MainConnection;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@IsID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pID));
            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, TransactionBy));
            
            MainConnection.Open();
            sqlCommand.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Issue::Delete::Error occured.", ex);
        }
        finally
        {
            MainConnection.Close();
            sqlCommand.Dispose();
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int InsertCondition(IssuesPro businessObject)
    {
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandText = "dbo.[IssueCondition_Insert]";
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Connection = MainConnection;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@ConditionID", SqlDbType.Int, 10, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, businessObject.ConditionID));
            sqlCommand.Parameters.Add(new SqlParameter("@IsID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IsID));
            sqlCommand.Parameters.Add(new SqlParameter("@ConditionName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.ConditionName));         
            sqlCommand.Parameters.Add(new SqlParameter("@ConditionType", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.IsType));
            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, businessObject.TransactionBy));

            MainConnection.Open();

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            rowsAffected = Convert.ToInt32(sqlCommand.Parameters["@ConditionID"].Value);
            return rowsAffected;
        }
        catch (Exception ex)
        {
            throw new Exception("IssueCondition::Insert::Error occured.", ex);
        }
        finally
        {
            MainConnection.Close();
            sqlCommand.Dispose();
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool DeleteAllCondition(string pID, string TransactionBy)
    {
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandText = "dbo.[IssueCondition_DeleteAll]";
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Connection = MainConnection;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@IsID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pID));
            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, TransactionBy));

            MainConnection.Open();
            sqlCommand.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("IssueCondition::DeleteAll::Error occured.", ex);
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