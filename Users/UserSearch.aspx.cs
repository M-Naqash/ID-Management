﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class UserSearch : BasePage
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DataTable dt;
    string MainPer = "Usr";
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //   --------------------Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Users",pageDiv);
            //   --------------------Common Code ----------------------------------------------------------------- //

            if (!IsPostBack)
            {
                MainMasterPage.ShowTitel(General.Msg("Users History", "سجلات المستخدمين"));   
                if (!FormSession.PermUsr.Contains("S" + MainPer)) { Response.Redirect(@"~/Login.aspx"); }
                btnSearch.Enabled = FormSession.PermUsr.Contains("S" + MainPer);

                FormCtrl.FillDDL("StatusWithSelect",ddlUsrStatus);
            }
        }
        catch (Exception e1) { }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            StringBuilder QS = new StringBuilder();
            QS.Append(" SELECT * FROM AppUsers WHERE UsrLoginID = UsrLoginID ");

            if (!string.IsNullOrEmpty(txtUsrLoginID.Text))  { QS.Append(" AND UsrLoginID   = '" + txtUsrLoginID.Text + "'"); }
            if (!string.IsNullOrEmpty(txtUsrFullName.Text)) { QS.Append(" AND UsrFullName LIKE '%" + txtUsrFullName.Text + "'"); }
            if (!string.IsNullOrEmpty(txtUsrEmailID.Text))  { QS.Append(" AND UsrEmailID   = '" + txtUsrEmailID.Text + "'"); }  
            if (ddlUsrStatus.SelectedIndex > 0)             { QS.Append(" AND UsrStatus = '" + ddlUsrStatus.SelectedValue + "'"); }
            dt = DBFun.FetchData(QS.ToString());
            if (!DBFun.IsNullOrEmpty(dt))
            {
                grdData.DataSource = (DataTable)dt;
                grdData.DataBind();
            }
            else
            {
                FormCtrl.FillGridEmpty(ref grdData,20,"No records found with the given search criterion","لا توجد سجلات بحسب شروط البحث المحددة");
            }
        }
        catch (Exception e1) { }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e) { ClearUI(); }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ClearUI() { FormCtrl.Clean(this); }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e) { FormCtrl.GridRowColor(e.Row); }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        btnSearch_Click(null, null);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}