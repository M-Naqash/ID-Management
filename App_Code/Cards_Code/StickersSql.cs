using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

public class StickersSql : DataLayerBase
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DateFun DTFun = new DateFun();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Insert(StickersPro Pro)
    {
        SqlCommand sqlCmd = new SqlCommand("dbo.[StickerMaster_Insert]", MainConnection);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCmd.Parameters.Add(new SqlParameter("@StickerID",  SqlDbType.Int, 10, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, Pro.StickerID));
            sqlCmd.Parameters.Add(new SqlParameter("@RegVehicle", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.RegVehicle));
            sqlCmd.Parameters.Add(new SqlParameter("@EmpID", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.EmpID));

            if (!string.IsNullOrEmpty(Pro.StartDate)) { sqlCmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateFun.SaveDB('S', Pro.StartDate))); }
            if (!string.IsNullOrEmpty(Pro.ExpiryDate)) { sqlCmd.Parameters.Add(new SqlParameter("@ExpiryDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateFun.SaveDB('S', Pro.ExpiryDate))); }

            sqlCmd.Parameters.Add(new SqlParameter("@Owner", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.Owner));
            sqlCmd.Parameters.Add(new SqlParameter("@CarType", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.CarType));
            sqlCmd.Parameters.Add(new SqlParameter("@Model", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.Model));
            sqlCmd.Parameters.Add(new SqlParameter("@Color", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.Color));

            sqlCmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.TemplateID));
            sqlCmd.Parameters.Add(new SqlParameter("@CompID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.CompID));

            sqlCmd.Parameters.Add(new SqlParameter("@Printed", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.Printed));
            sqlCmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.Status));
            
            sqlCmd.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.TransactionBy));
            sqlCmd.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.TransactionDate));

            MainConnection.Open();

            sqlCmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
            MainConnection.Close();
            sqlCmd.Dispose();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Update(StickersPro Pro)
    {
        SqlCommand sqlCmd = new SqlCommand("dbo.[StickerMaster_Update]", MainConnection);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCmd.Parameters.Add(new SqlParameter("@StickerID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.StickerID));
            sqlCmd.Parameters.Add(new SqlParameter("@RegVehicle", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.RegVehicle));
            sqlCmd.Parameters.Add(new SqlParameter("@EmpID", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.EmpID));

            if (!string.IsNullOrEmpty(Pro.StartDate)) { sqlCmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateFun.SaveDB('G', Pro.StartDate))); }
            if (!string.IsNullOrEmpty(Pro.ExpiryDate)) { sqlCmd.Parameters.Add(new SqlParameter("@ExpiryDate", SqlDbType.DateTime, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateFun.SaveDB('G', Pro.ExpiryDate))); }

            sqlCmd.Parameters.Add(new SqlParameter("@Owner", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.Owner));
            sqlCmd.Parameters.Add(new SqlParameter("@CarType", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.CarType));
            sqlCmd.Parameters.Add(new SqlParameter("@Model", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.Model));
            sqlCmd.Parameters.Add(new SqlParameter("@Color", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.Color));

            sqlCmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.TemplateID));
            sqlCmd.Parameters.Add(new SqlParameter("@CompID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.CompID));

            sqlCmd.Parameters.Add(new SqlParameter("@Printed", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, true));
            sqlCmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.Status));

            sqlCmd.Parameters.Add(new SqlParameter("@TransactionBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.TransactionBy));

            MainConnection.Open();

            sqlCmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
            MainConnection.Close();
            sqlCmd.Dispose();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool Approve(StickersPro Pro)
    {
        SqlCommand sqlCmd = new SqlCommand("dbo.[Stickers_Approval]", MainConnection);
        sqlCmd.CommandType = CommandType.StoredProcedure;

        try
        {
            sqlCmd.Parameters.Add(new SqlParameter("@StickerID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.StickerID));
            sqlCmd.Parameters.Add(new SqlParameter("@ApprovalStatus", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.ApprovalStatus));
            sqlCmd.Parameters.Add(new SqlParameter("@ApprovedBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.ApprovedBy));
            sqlCmd.Parameters.Add(new SqlParameter("@ApprovedDate", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Pro.ApprovedDate));

            MainConnection.Open();

            sqlCmd.ExecuteNonQuery();
            return true;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        finally
        {
            MainConnection.Close();
            sqlCmd.Dispose();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}