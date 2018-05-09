using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

public class ApplicationSetupSql : DataLayerBase
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
    public bool InsertUpdate(ApplicationSetupPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[ApplicationSetup_InsertUpdate]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@AppCompany", VchDB, 100, IN, false, 0, 0, "", DRV, pro.AppCompany));
            sqlCommand.Parameters.Add(new SqlParameter("@AppDisplay", VchDB, 100, IN, false, 0, 0, "", DRV, pro.AppDisplay));
            sqlCommand.Parameters.Add(new SqlParameter("@AppAddress1", VchDB, 255, IN, false, 0, 0, "", DRV, pro.AppAddress1));
            sqlCommand.Parameters.Add(new SqlParameter("@AppAddress2", VchDB, 255, IN, false, 0, 0, "", DRV, pro.AppAddress2));
            sqlCommand.Parameters.Add(new SqlParameter("@AppCity", VchDB, 50, IN, false, 0, 0, "", DRV, pro.AppCity));
            sqlCommand.Parameters.Add(new SqlParameter("@AppCountry", VchDB, 50, IN, false, 0, 0, "", DRV, pro.AppCountry));
            sqlCommand.Parameters.Add(new SqlParameter("@AppPOBox", VchDB, 50, IN, false, 0, 0, "", DRV, pro.AppPOBox));
            sqlCommand.Parameters.Add(new SqlParameter("@AppTelNo1", VchDB, 50, IN, false, 0, 0, "", DRV, pro.AppTelNo1));
            sqlCommand.Parameters.Add(new SqlParameter("@AppTelNo2", VchDB, 50, IN, false, 0, 0, "", DRV, pro.AppTelNo2));
            sqlCommand.Parameters.Add(new SqlParameter("@AppFax", VchDB, 50, IN, false, 0, 0, "", DRV, pro.AppFax));
            sqlCommand.Parameters.Add(new SqlParameter("@AppUrl", VchDB, 500, IN, false, 0, 0, "", DRV, pro.AppUrl));
            sqlCommand.Parameters.Add(new SqlParameter("@AppEmail", VchDB, 50, IN, false, 0, 0, "", DRV, pro.AppEmail));

            sqlCommand.Parameters.Add(new SqlParameter("@AppCalendar", VchDB, 1, IN, false, 0, 0, "", DRV, pro.AppCalendar));
            
            sqlCommand.Parameters.Add(new SqlParameter("@AppLogo", SqlDbType.Image, 1000000, IN, false, 0, 0, "", DRV, pro.AppLogo));
            sqlCommand.Parameters.Add(new SqlParameter("@AppLogoImageType", VchDB, 100, IN, false, 0, 0, "", DRV, pro.AppLogoImageType));
            sqlCommand.Parameters.Add(new SqlParameter("@AppLogoImageLength", SqlDbType.Int, 20, IN, false, 0, 0, "", DRV, pro.AppLogoImageLength));

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