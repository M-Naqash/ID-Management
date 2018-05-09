using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

public class AppUsersSql : DataLayerBase
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
    SqlDbType ChrDB = SqlDbType.Char;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Insert(AppUsersPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[AppUsers_Insert]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@UsrLoginID", VchDB, 50, IN, false, 0, 0, "", DRV, pro.UsrLoginID));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrPassword", VchDB, 100, IN, false, 0, 0, "", DRV, pro.UsrPassword));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrFullName", VchDB, 200, IN, false, 0, 0, "", DRV, pro.UsrFullName));
            
            if (!string.IsNullOrEmpty(pro.UsrStartDate))
            { 
                sqlCommand.Parameters.Add(new SqlParameter("@UsrStartDate", DtDB, 14, IN, false, 0, 0, "", DRV, DateFun.SaveDB(pro.UsrStartDateType, pro.UsrStartDate)));
                sqlCommand.Parameters.Add(new SqlParameter("@UsrStartDateType", ChrDB, 1, IN, false, 0, 0, "", DRV, pro.UsrStartDateType));
            } 

            if (!string.IsNullOrEmpty(pro.UsrExpiryDate))
            { 
                sqlCommand.Parameters.Add(new SqlParameter("@UsrExpiryDate", DtDB, 14, IN, false, 0, 0, "", DRV, DateFun.SaveDB(pro.UsrExpiryDateType, pro.UsrExpiryDate)));
                sqlCommand.Parameters.Add(new SqlParameter("@UsrExpiryDateType", ChrDB, 1, IN, false, 0, 0, "", DRV, pro.UsrExpiryDateType));
            } 
            
            sqlCommand.Parameters.Add(new SqlParameter("@UsrStatus", BitDB, 1, IN, false, 0, 0, "", DRV, pro.UsrStatus));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrPermission", VchDB, 8000, IN, false, 0, 0, "", DRV, pro.UsrPermission));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrLanguage", VchDB, 50, IN, false, 0, 0, "", DRV, pro.UsrLanguage));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrEmailID", VchDB, 200, IN, false, 0, 0, "", DRV, pro.UsrEmailID));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrEmpID", VchDB, 100, IN, false, 0, 0, "", DRV, pro.UsrEmpID));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrDescription", VchDB, 8000, IN, false, 0, 0, "", DRV, pro.UsrDescription));
            
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
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Update(AppUsersPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[AppUsers_Update]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        
        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@UsrLoginID", VchDB, 50, IN, false, 0, 0, "", DRV, pro.UsrLoginID));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrPassword", VchDB, 100, IN, false, 0, 0, "", DRV, pro.UsrPassword));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrFullName", VchDB, 200, IN, false, 0, 0, "", DRV, pro.UsrFullName));
            
            if (!string.IsNullOrEmpty(pro.UsrStartDate))
            { 
                sqlCommand.Parameters.Add(new SqlParameter("@UsrStartDate", DtDB, 14, IN, false, 0, 0, "", DRV, DateFun.SaveDB(pro.UsrStartDateType, pro.UsrStartDate)));
                sqlCommand.Parameters.Add(new SqlParameter("@UsrStartDateType", ChrDB, 1, IN, false, 0, 0, "", DRV, pro.UsrStartDateType));
            } 

            if (!string.IsNullOrEmpty(pro.UsrExpiryDate))
            { 
                sqlCommand.Parameters.Add(new SqlParameter("@UsrExpiryDate", DtDB, 14, IN, false, 0, 0, "", DRV, DateFun.SaveDB(pro.UsrExpiryDateType, pro.UsrExpiryDate)));
                sqlCommand.Parameters.Add(new SqlParameter("@UsrExpiryDateType", ChrDB, 1, IN, false, 0, 0, "", DRV, pro.UsrExpiryDateType));
            } 
            
            sqlCommand.Parameters.Add(new SqlParameter("@UsrStatus", BitDB, 1, IN, false, 0, 0, "", DRV, pro.UsrStatus));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrPermission", VchDB, 8000, IN, false, 0, 0, "", DRV, pro.UsrPermission));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrLanguage", VchDB, 50, IN, false, 0, 0, "", DRV, pro.UsrLanguage));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrEmailID", VchDB, 200, IN, false, 0, 0, "", DRV, pro.UsrEmailID));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrEmpID", VchDB, 100, IN, false, 0, 0, "", DRV, pro.UsrEmpID));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrDescription", VchDB, 8000, IN, false, 0, 0, "", DRV, pro.UsrDescription));

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
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Delete(AppUsersPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[AppUsers_Delete]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@UsrLoginID", VchDB, 50, IN, false, 0, 0, "", DRV, pro.UsrLoginID));
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
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool UpdateLanguage(AppUsersPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[AppUsers_UpdateLanguage]",MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@UsrLoginID", VchDB, 50, IN, false, 0, 0, "", DRV, pro.UsrLoginID));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrLanguage", VchDB, 50, IN, false, 0, 0, "", DRV, pro.UsrLanguage));
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
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool UpdatePassword(AppUsersPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[AppUsers_UpdatePassword]",MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@UsrLoginID", VchDB, 50, IN, false, 0, 0, "", DRV, pro.UsrLoginID));
            sqlCommand.Parameters.Add(new SqlParameter("@UsrPassword", VchDB, 100, IN, false, 0, 0, "", DRV, pro.UsrPassword));
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
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool RoleInsert(AppUsersPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[RoleUserPermissions_Insert]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        
        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@RoleID",          IntDB, 10, OU, false, 0, 0, "", DRV, pro.RoleID));
            sqlCommand.Parameters.Add(new SqlParameter("@RoleNameEn",      VchDB, 100, IN, false, 0, 0, "", DRV, pro.RoleNameEn));          
            sqlCommand.Parameters.Add(new SqlParameter("@RoleNameAr",      VchDB, 100, IN, false, 0, 0, "", DRV, pro.RoleNameAr));           
            sqlCommand.Parameters.Add(new SqlParameter("@RolePermissions", VchDB, 8000, IN, false, 0, 0, "", DRV, pro.RolePermissions));
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
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool RoleUpdate(AppUsersPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[RoleUserPermissions_Update]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@RoleID",          IntDB, 10, IN, false, 0, 0, "", DRV, pro.RoleID));
            sqlCommand.Parameters.Add(new SqlParameter("@RoleNameEn",      VchDB, 100, IN, false, 0, 0, "", DRV, pro.RoleNameEn));          
            sqlCommand.Parameters.Add(new SqlParameter("@RoleNameAr",      VchDB, 100, IN, false, 0, 0, "", DRV, pro.RoleNameAr));           
            sqlCommand.Parameters.Add(new SqlParameter("@RolePermissions", VchDB, 8000, IN, false, 0, 0, "", DRV, pro.RolePermissions));
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
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool RoleDelete(AppUsersPro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[RoleUserPermissions_Delete]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@RoleID", IntDB, 10, IN, false, 0, 0, "", DRV, pro.RoleID));
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
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
}