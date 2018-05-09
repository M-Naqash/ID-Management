using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

public class BlackListSql : DataLayerBase
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ParameterDirection IN  = ParameterDirection.Input;
    ParameterDirection OU  = ParameterDirection.Output;
    DataRowVersion     DRV = DataRowVersion.Proposed;

    SqlDbType IntDB = SqlDbType.Int;
    SqlDbType VchDB = SqlDbType.VarChar;
    SqlDbType DtDB  = SqlDbType.DateTime;
    SqlDbType BitDB = SqlDbType.Bit;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int Insert(BlackListPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[BlackList_Insert]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@BlaID"         , IntDB, 10 , OU, false, 0, 0, "", DRV, pro.BlaID));
            sqlCommand.Parameters.Add(new SqlParameter("@BlaIdentityNo" , VchDB, 100, IN, false, 0, 0, "", DRV, pro.BlaIdentityNo));           
            sqlCommand.Parameters.Add(new SqlParameter("@BlaNameEn"     , VchDB, 500, IN, false, 0, 0, "", DRV, pro.BlaNameEn));
            sqlCommand.Parameters.Add(new SqlParameter("@BlaNameAr"     , VchDB, 500, IN, false, 0, 0, "", DRV, pro.BlaNameAr));
            sqlCommand.Parameters.Add(new SqlParameter("@NatID"         , IntDB, 10 , IN, false, 0, 0, "", DRV, pro.NatID));
            sqlCommand.Parameters.Add(new SqlParameter("@BlaReason"     , VchDB, 8000 , IN, false, 0, 0, "", DRV, pro.BlaReason));
            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy" , VchDB, 50 , IN, false, 0, 0, "", DRV, pro.TransactionBy));
                     
            MainConnection.Open();

            int ID = sqlCommand.ExecuteNonQuery();
            ID = Convert.ToInt32(sqlCommand.Parameters["@BlaID"].Value);
            return ID;
        }
        catch (Exception ex)
        {
            return -1;
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
    public bool Update(BlackListPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[BlackList_Update]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@BlaIdentityNo" , VchDB, 100, IN, false, 0, 0, "", DRV, pro.BlaIdentityNo));           
            sqlCommand.Parameters.Add(new SqlParameter("@BlaNameEn"     , VchDB, 500, IN, false, 0, 0, "", DRV, pro.BlaNameEn));
            sqlCommand.Parameters.Add(new SqlParameter("@BlaNameAr"     , VchDB, 500, IN, false, 0, 0, "", DRV, pro.BlaNameAr));
            sqlCommand.Parameters.Add(new SqlParameter("@NatID"         , IntDB, 10 , IN, false, 0, 0, "", DRV, pro.NatID));
            sqlCommand.Parameters.Add(new SqlParameter("@BlaReason"     , VchDB, 8000 , IN, false, 0, 0, "", DRV, pro.BlaReason));
            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy" , VchDB, 50 , IN, false, 0, 0, "", DRV, pro.TransactionBy));
        
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
    public bool Delete(BlackListPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[BlackList_Delete]",MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@BlaIdentityNo", VchDB, 100, IN, false, 0, 0, "", DRV, pro.BlaIdentityNo));
            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy" , VchDB, 50 , IN, false, 0, 0, "", DRV, pro.TransactionBy));
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

