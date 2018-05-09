using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

public class VisitorsSql : DataLayerBase
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
    public int Insert(VisitorsPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[VisitorsCard_Insert]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@VisCardID", IntDB, 10, OU, false, 0, 0, "", DRV, pro.VisCardID));
            sqlCommand.Parameters.Add(new SqlParameter("@VisIdentityNo", VchDB, 100, IN, false, 0, 0, "", DRV, pro.VisIdentityNo));
            
            sqlCommand.Parameters.Add(new SqlParameter("@VisNameEn", VchDB, 500, IN, false, 0, 0, "", DRV, pro.VisNameEn));
            sqlCommand.Parameters.Add(new SqlParameter("@VisNameAr", VchDB, 500, IN, false, 0, 0, "", DRV, pro.VisNameAr));
            sqlCommand.Parameters.Add(new SqlParameter("@VisMobileNo", VchDB, 200, IN, false, 0, 0, "", DRV, pro.VisMobileNo));
            
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion1", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion1));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion2", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion2));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion3", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion3));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion4", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion4));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion5", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion5));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion6", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion6));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion7", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion7));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion8", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion8));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion9", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion9));

            sqlCommand.Parameters.Add(new SqlParameter("@StartDate", DtDB, 14, IN, false, 0, 0, "", DRV, DateFun.SaveDB('S', pro.StartDate)));
            sqlCommand.Parameters.Add(new SqlParameter("@ExpiryDate", DtDB, 14, IN, false, 0, 0, "", DRV, DateFun.SaveDB('S', pro.ExpiryDate)));
            sqlCommand.Parameters.Add(new SqlParameter("@TmpID", IntDB, 10, IN, false, 0, 0, "", DRV, pro.TmpID));
            sqlCommand.Parameters.Add(new SqlParameter("@Description", VchDB, 1000, IN, false, 0, 0, "", DRV, pro.Description));
            
            sqlCommand.Parameters.Add(new SqlParameter("@CardStatus", IntDB, 10, IN, false, 0, 0, "", DRV, pro.CardStatus));
            sqlCommand.Parameters.Add(new SqlParameter("@isPrinted", BitDB, 10, IN, false, 0, 0, "", DRV, pro.isPrinted));
            sqlCommand.Parameters.Add(new SqlParameter("@CopiesCount", IntDB, 10, IN, false, 0, 0, "", DRV, pro.CopiesCount));
            

            if (pro.VisImageLength > 0 )
            {
                sqlCommand.Parameters.Add(new SqlParameter("@VisImage", SqlDbType.Image, 1000000, IN, false, 0, 0, "", DRV, pro.VisImage));
                sqlCommand.Parameters.Add(new SqlParameter("@VisImageContentType", VchDB, 100, IN, false, 0, 0, "", DRV, pro.VisImageContentType));
                sqlCommand.Parameters.Add(new SqlParameter("@VisImageLength", SqlDbType.BigInt, 100, IN, false, 0, 0, "", DRV, pro.VisImageLength.ToString()));
            }

            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 50, IN, false, 0, 0, "", DRV, pro.TransactionBy));
                     
            MainConnection.Open();

            int ID = sqlCommand.ExecuteNonQuery();
            ID = Convert.ToInt32(sqlCommand.Parameters["@VisCardID"].Value);
            return ID;
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
    public bool Update(VisitorsPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[VisitorsCard_Update]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@VisCardID", IntDB, 10, IN, false, 0, 0, "", DRV, pro.VisCardID));
            sqlCommand.Parameters.Add(new SqlParameter("@VisIdentityNo", VchDB, 100, IN, false, 0, 0, "", DRV, pro.VisIdentityNo));
            
            sqlCommand.Parameters.Add(new SqlParameter("@VisNameEn", VchDB, 500, IN, false, 0, 0, "", DRV, pro.VisNameEn));
            sqlCommand.Parameters.Add(new SqlParameter("@VisNameAr", VchDB, 500, IN, false, 0, 0, "", DRV, pro.VisNameAr));
            sqlCommand.Parameters.Add(new SqlParameter("@VisMobileNo", VchDB, 200, IN, false, 0, 0, "", DRV, pro.VisMobileNo));
            
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion1", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion1));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion2", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion2));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion3", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion3));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion4", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion4));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion5", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion5));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion6", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion6));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion7", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion7));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion8", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion8));
            sqlCommand.Parameters.Add(new SqlParameter("@VisRegion9", BitDB, 1, IN, false, 0, 0, "", DRV, pro.VisRegion9));

            sqlCommand.Parameters.Add(new SqlParameter("@StartDate", DtDB, 14, IN, false, 0, 0, "", DRV, DateFun.SaveDB('S', pro.StartDate)));
            sqlCommand.Parameters.Add(new SqlParameter("@ExpiryDate", DtDB, 14, IN, false, 0, 0, "", DRV, DateFun.SaveDB('S', pro.ExpiryDate)));
            sqlCommand.Parameters.Add(new SqlParameter("@TmpID", IntDB, 10, IN, false, 0, 0, "", DRV, pro.TmpID));
            sqlCommand.Parameters.Add(new SqlParameter("@Description", VchDB, 1000, IN, false, 0, 0, "", DRV, pro.Description));
            
            sqlCommand.Parameters.Add(new SqlParameter("@CardStatus", IntDB, 10, IN, false, 0, 0, "", DRV, pro.CardStatus));
            //sqlCommand.Parameters.Add(new SqlParameter("@isPrinted", BitDB, 10, IN, false, 0, 0, "", DRV, pro.isPrinted));

            if (pro.VisImageLength > 0 )
            {
                sqlCommand.Parameters.Add(new SqlParameter("@VisImage", SqlDbType.Image, 1000000, IN, false, 0, 0, "", DRV, pro.VisImage));
                sqlCommand.Parameters.Add(new SqlParameter("@VisImageContentType", VchDB, 100, IN, false, 0, 0, "", DRV, pro.VisImageContentType));
                sqlCommand.Parameters.Add(new SqlParameter("@VisImageLength", SqlDbType.BigInt, 100, IN, false, 0, 0, "", DRV, pro.VisImageLength.ToString()));
            }

            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 50, IN, false, 0, 0, "", DRV, pro.TransactionBy));

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
    public bool Print(VisitorsPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[VisitorsCard_Print]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@VisCardID", IntDB, 10, IN, false, 0, 0, "", DRV, pro.VisCardID));
            sqlCommand.Parameters.Add(new SqlParameter("@CardStatus", IntDB, 10, IN, false, 0, 0, "", DRV, pro.CardStatus));
            sqlCommand.Parameters.Add(new SqlParameter("@isPrinted", BitDB, 10, IN, false, 0, 0, "", DRV, pro.isPrinted));
            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 50, IN, false, 0, 0, "", DRV, pro.TransactionBy));

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
    public bool UpdateStatus(VisitorsPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[VisitorsCard_UpdateStatus]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@VisCardID", IntDB, 10, IN, false, 0, 0, "", DRV, pro.VisCardID));
            sqlCommand.Parameters.Add(new SqlParameter("@CardStatus", IntDB, 10, IN, false, 0, 0, "", DRV, pro.CardStatus));
            sqlCommand.Parameters.Add(new SqlParameter("@TransactionBy", VchDB, 50, IN, false, 0, 0, "", DRV, pro.TransactionBy));

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