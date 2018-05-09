using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

public class CardsSql : DataLayerBase
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    SqlParameter param;
    DataTable dt;
    DateTime Date;
    SqlDataAdapter da = new SqlDataAdapter();
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public int Insert(CardsPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandText = "dbo.[Cards_Insert]";
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Connection = MainConnection;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@CardID", SqlDbType.Int, 10, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, pro.CardID));

            sqlCommand.Parameters.Add(new SqlParameter("@EmpID", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpID));
            sqlCommand.Parameters.Add(new SqlParameter("@IsID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.IsID));
            sqlCommand.Parameters.Add(new SqlParameter("@CardCount", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.CardCount));

            sqlCommand.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateFun.SaveDB('S', pro.StartDate)));
            sqlCommand.Parameters.Add(new SqlParameter("@ExpiryDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateFun.SaveDB('S', pro.ExpiryDate)));
            sqlCommand.Parameters.Add(new SqlParameter("@TmpID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.TmpID));
            sqlCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.Description));
            
            sqlCommand.Parameters.Add(new SqlParameter("@CardStatus", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.CardStatus));
            sqlCommand.Parameters.Add(new SqlParameter("@InActiveStatus", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.InActiveStatus));
            sqlCommand.Parameters.Add(new SqlParameter("@IsApproved", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.IsApproved));
            sqlCommand.Parameters.Add(new SqlParameter("@isPrinted", SqlDbType.Bit, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.isPrinted));

            sqlCommand.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.TransactionBy));
            sqlCommand.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.TransactionDate));

            MainConnection.Open();

            int rowsAffected = sqlCommand.ExecuteNonQuery();
            rowsAffected = Convert.ToInt32(sqlCommand.Parameters["@CardID"].Value);
            return rowsAffected;
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
    public bool Update(CardsPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandText = "dbo.[Cards_Update]";
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Connection = MainConnection;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@CardID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.CardID));

            sqlCommand.Parameters.Add(new SqlParameter("@EmpID", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpID));
            sqlCommand.Parameters.Add(new SqlParameter("@IsID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.IsID));
            sqlCommand.Parameters.Add(new SqlParameter("@CardCount", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.CardCount));

            sqlCommand.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateFun.SaveDB('S', pro.StartDate)));
            sqlCommand.Parameters.Add(new SqlParameter("@ExpiryDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateFun.SaveDB('S', pro.ExpiryDate)));
            sqlCommand.Parameters.Add(new SqlParameter("@TmpID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.TmpID));
            sqlCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.Description));
            sqlCommand.Parameters.Add(new SqlParameter("@CardStatus", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.CardStatus));

            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.TransactionBy));

            MainConnection.Open();

            sqlCommand.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Cards::Update::Error occured.", ex);
        }
        finally
        {
            MainConnection.Close();
            sqlCommand.Dispose();
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public bool InsertCondition(string pCardID, string pConditionID, string pConditionName, bool pStatus)
    {
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandText = "dbo.[CardCondition_Insert]";
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Connection = MainConnection;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@CardID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCardID));
            sqlCommand.Parameters.Add(new SqlParameter("@ConditionID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pConditionID));
            sqlCommand.Parameters.Add(new SqlParameter("@ConditionStatus", SqlDbType.Bit, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pStatus));
            sqlCommand.Parameters.Add(new SqlParameter("@ConditionName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pConditionName));
            sqlCommand.Parameters.Add(new SqlParameter("@ConditionType", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, "Card"));

            MainConnection.Open();
            sqlCommand.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("CardCondition::Insert::Error occured.", ex);
        }
        finally
        {
            MainConnection.Close();
            sqlCommand.Dispose();
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public bool UpdateCondition(string pCardID, string pConditionID, bool pStatus)
    {
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandText = "dbo.[CardCondition_Update]";
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Connection = MainConnection;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@CardID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCardID));
            sqlCommand.Parameters.Add(new SqlParameter("@ConditionID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pConditionID));
            sqlCommand.Parameters.Add(new SqlParameter("@ConditionStatus", SqlDbType.Bit, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pStatus));
            sqlCommand.Parameters.Add(new SqlParameter("@ConditionType", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, "Card"));

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
    public bool Approved(string pCardID, string pLoginUser, string pDate,string pIsApproved)
    {
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandText = "dbo.[Card_Approved]";
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Connection = MainConnection;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@CardID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCardID));
            sqlCommand.Parameters.Add(new SqlParameter("@ApprovedBy", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pLoginUser));
            sqlCommand.Parameters.Add(new SqlParameter("@ApprovedDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pDate));
            sqlCommand.Parameters.Add(new SqlParameter("@IsApproved", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pIsApproved));

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
    public bool Rejected(string pCardID, string pLoginUser, string pDate,string pIsApproved, string pRejectReason)
    {
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandText = "dbo.[Card_Rejected]";
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Connection = MainConnection;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@CardID"      , SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCardID));
            sqlCommand.Parameters.Add(new SqlParameter("@ApprovedBy"  , SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pLoginUser));
            sqlCommand.Parameters.Add(new SqlParameter("@ApprovedDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pDate));
            sqlCommand.Parameters.Add(new SqlParameter("@IsApproved"  , SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pIsApproved));
            sqlCommand.Parameters.Add(new SqlParameter("@RejectReason", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRejectReason));

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