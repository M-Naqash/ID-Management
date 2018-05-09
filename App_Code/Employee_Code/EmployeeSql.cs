using System;
using System.Data;
using System.Data.SqlClient;

public class EmployeeSql : DataLayerBase
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Insert(EmployeePro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[Employee_Insert]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@EmpID", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpID));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpType", SqlDbType.VarChar, 3, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpType));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpNameAr", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpNameAr));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpNameEn", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpNameEn));
            sqlCommand.Parameters.Add(new SqlParameter("@NatID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.NatID));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpBirthDate", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateFun.SaveDB('S', pro.EmpBirthDate)));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpJobTitleAr", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpJobTitleAr));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpJobTitleEn", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpJobTitleEn));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpNationalID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpNationalID));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpBloodGroup", SqlDbType.VarChar, 3, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpBloodGroup));
            sqlCommand.Parameters.Add(new SqlParameter("@CompID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.CompID));
            sqlCommand.Parameters.Add(new SqlParameter("@SecID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.SecID));

            if (!String.IsNullOrEmpty(pro.EmpHireDate))
            { sqlCommand.Parameters.Add(new SqlParameter("@EmpHireDate", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateFun.SaveDB('S', pro.EmpHireDate))); }
            sqlCommand.Parameters.Add(new SqlParameter("@EmpGender", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpGender));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpMobileNo", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpMobileNo));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpEmailID", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpEmailID));

            if (pro.EmpImageLength > 0)
            {
                sqlCommand.Parameters.Add(new SqlParameter("@Image", SqlDbType.Image, 1000000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpImage));
                sqlCommand.Parameters.Add(new SqlParameter("@ImageContentType", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpImageContentType));
                sqlCommand.Parameters.Add(new SqlParameter("@ImageLength", SqlDbType.BigInt, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpImageLength.ToString()));
            }
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
    public bool Update(EmployeePro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[Employee_Update]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@EmpID", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpID));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpType", SqlDbType.VarChar, 3, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpType));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpNameAr", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpNameAr));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpNameEn", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpNameEn));
            sqlCommand.Parameters.Add(new SqlParameter("@NatID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.NatID));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpBirthDate", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateFun.SaveDB('S', pro.EmpBirthDate)));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpJobTitleAr", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpJobTitleAr));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpJobTitleEn", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpJobTitleEn));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpNationalID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpNationalID));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpBloodGroup", SqlDbType.VarChar, 3, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpBloodGroup));
            sqlCommand.Parameters.Add(new SqlParameter("@CompID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.CompID));
            sqlCommand.Parameters.Add(new SqlParameter("@SecID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.SecID));

            if (!String.IsNullOrEmpty(pro.EmpHireDate))
            { sqlCommand.Parameters.Add(new SqlParameter("@EmpHireDate", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateFun.SaveDB('S', pro.EmpHireDate))); }
            sqlCommand.Parameters.Add(new SqlParameter("@EmpGender", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpGender));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpMobileNo", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpMobileNo));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpEmailID", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpEmailID));

            if (pro.EmpImageLength > 0)
            {
                sqlCommand.Parameters.Add(new SqlParameter("@Image", SqlDbType.Image, 1000000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpImage));
                sqlCommand.Parameters.Add(new SqlParameter("@ImageContentType", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpImageContentType));
                sqlCommand.Parameters.Add(new SqlParameter("@ImageLength", SqlDbType.BigInt, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpImageLength.ToString()));
            }
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
    public bool UpdateType(EmployeePro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[Employee_UpdateType]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@EmpID", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpID));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpType", SqlDbType.VarChar, 3, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.EmpType));
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
    public bool Docs_Insert(EmployeePro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[EmployeeDocs_Insert]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@DocID"  , SqlDbType.Int    , 10  , ParameterDirection.Output, false, 0, 0, "" , DataRowVersion.Proposed, pro.DocID));
            sqlCommand.Parameters.Add(new SqlParameter("@EmpID"  , SqlDbType.VarChar, 20  , ParameterDirection.Input  , false, 0, 0, "", DataRowVersion.Proposed, pro.EmpID));
            sqlCommand.Parameters.Add(new SqlParameter("@DocName", SqlDbType.VarChar, 1000, ParameterDirection.Input  , false, 0, 0, "", DataRowVersion.Proposed, pro.DocName));
            sqlCommand.Parameters.Add(new SqlParameter("@DocPath", SqlDbType.VarChar, 8000, ParameterDirection.Input  , false, 0, 0, "", DataRowVersion.Proposed, pro.DocPath));

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
    public bool Docs_Delete(EmployeePro pro)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[EmployeeDocs_Delete]",MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@DocID"        , SqlDbType.Int    , 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pro.DocID));
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
    public bool Booked_Insert(string pBkdID)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[BookedID_Insert]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@BkdID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pBkdID));

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
    public bool Booked_Delete(string pBkdID)
    {
        SqlCommand sqlCommand = new SqlCommand("dbo.[BookedID_Delete]", MainConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCommand.Parameters.Add(new SqlParameter("@BkdID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pBkdID));
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