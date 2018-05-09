using System;
using System.Data;
using System.Data.SqlClient;

public class EmailSettingSql : DataLayerBase
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ParameterDirection IN  = ParameterDirection.Input;
    ParameterDirection OU  = ParameterDirection.Output;
    DataRowVersion     DRV = DataRowVersion.Proposed;

    SqlDbType IntDB = SqlDbType.Int;
    SqlDbType VchDB = SqlDbType.VarChar;
    SqlDbType DtDB  = SqlDbType.DateTime;
    SqlDbType BtDB  = SqlDbType.Bit;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool InsertUpdate(EmailSettingPro Pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[EmailSetting_InsertUpdate]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@EmlServerID"        , VchDB , 100 , IN, false, 0, 0, "", DRV, Pro.EmlServerID));
            sqlCommand.Parameters.Add(new SqlParameter("@EmlPortNo"          , IntDB , 255 , IN, false, 0, 0, "", DRV, Pro.EmlPortNo));
            sqlCommand.Parameters.Add(new SqlParameter("@EmlSenderEmail"     , VchDB , 200 , IN, false, 0, 0, "", DRV, Pro.EmlSenderEmail));
            sqlCommand.Parameters.Add(new SqlParameter("@EmlSenderPassword"  , VchDB , 100 , IN, false, 0, 0, "", DRV, Pro.EmlSenderPassword));
            sqlCommand.Parameters.Add(new SqlParameter("@EmlSsl"             , BtDB  , 1   , IN, false, 0, 0, "", DRV, Pro.EmlSsl));
            sqlCommand.Parameters.Add(new SqlParameter("@EmlCredential"      , BtDB  , 1   , IN, false, 0, 0, "", DRV, Pro.EmlCredential));
            sqlCommand.Parameters.Add(new SqlParameter("@EmlCountDaysForSend", IntDB , 10  , IN, false, 0, 0, "", DRV, Pro.EmlCountDaysForSend));
            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy"      , VchDB , 50  , IN, false, 0, 0, "", DRV, Pro.TransactionBy));

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
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}