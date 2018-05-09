using System;
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

public partial class CardHistory : BasePage
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DataTable dt;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //   --------------------Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Card", pageDiv);
            //   --------------------Common Code ----------------------------------------------------------------- //
            if (!IsPostBack)
            {
                if (!FormSession.getPerm("SCrd")) { Response.Redirect(@"~/Login.aspx"); }
                MainMasterPage.ShowTitel(General.Msg("Cards History", "سجلات البطاقة"));
                Fillddl();
                //DataLang();
            }
        }
        catch (Exception e1) { }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Fillddl()
    {
        DataTable CompDT = DBFun.FetchData("SELECT * FROM Companies ");
        if (!DBFun.IsNullOrEmpty(CompDT)) { FormCtrl.PopulateDDL(ddlCompID, CompDT, "CompName" + FormSession.Language, "CompID", General.Msg("-Select Company-", "-اختر الشركة-")); }

        DataTable SecDT = DBFun.FetchData("SELECT * FROM SectionsExternal ");
        if (!DBFun.IsNullOrEmpty(SecDT)) { FormCtrl.PopulateDDL(ddlSecID, SecDT, "SecName" + FormSession.Language, "SecID", General.Msg("-Select Sections External-", "-اختر الجهة الخارجية-")); }

        dt = DBFun.FetchData("SELECT DISTINCT IsID,IsNameEn,IsNameAr FROM CardInfoView");
        if (!DBFun.IsNullOrEmpty(dt)) { FormCtrl.PopulateDDL(ddlIssue, dt, "IsName" + General.Lang(), "IsID", General.Msg("-Select Issue-", "-اختر الإصدار-")); }

        dt = DBFun.FetchData("SELECT DISTINCT CreatedBy FROM CardInfoView WHERE CreatedBy IS NOT NULL ");
        if (!DBFun.IsNullOrEmpty(dt)) { FormCtrl.PopulateDDL(ddlCreatedBy, dt, "CreatedBy", "CreatedBy", General.Msg("-Select Created By-", "-اختر إنشاء بواسطة-")); }

        dt = DBFun.FetchData("SELECT DISTINCT PrintedBy FROM CardInfoView WHERE PrintedBy IS NOT NULL ");
        if (!DBFun.IsNullOrEmpty(dt)) { FormCtrl.PopulateDDL(ddlPrintedBy, dt, "PrintedBy", "PrintedBy", General.Msg("-Select Printed By-", "-اختر طباعة بواسطة-")); }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            StringBuilder QS = new StringBuilder();
            
            QS.Append(" SELECT * "
                       + " , EmpName" + General.Lang() + " AS EmpName "
                       + " , IsName" + General.Lang() + " AS IssueName "
                       + " FROM CardInfoView "
                       + " WHERE  1 = 1 ");

            if (!string.IsNullOrEmpty(txtEmpID.Text)) { QS.Append(" AND EmpID   = '" + txtEmpID.Text + "'"); }
            if (ddlEmpType.SelectedIndex > 0) { QS.Append(" AND EmpType = '" + ddlEmpType.SelectedValue + "'"); }
            if (!string.IsNullOrEmpty(txtEmpName.Text)) { QS.Append(" AND EmpName" + FormSession.Language + " LIKE '%" + txtEmpName.Text + "'"); }
            if (!string.IsNullOrEmpty(txtEmpNationalID.Text)) { QS.Append(" AND EmpNationalID   = '" + txtEmpNationalID.Text + "'"); }
            if (ddlCompID.SelectedIndex > 0) { QS.Append(" AND CompID  = '" + ddlCompID.SelectedValue + "'"); }
            if (ddlSecID.SelectedIndex > 0) { QS.Append(" AND SecID  = '" + ddlSecID.SelectedValue + "'"); }

            if (!string.IsNullOrEmpty(txtCardID.Text)) { QS.Append(" AND CardID   = '" + txtCardID.Text + "'"); }
            if (ddlIssue.SelectedIndex > 0) { QS.Append(" AND IsID    = '" + ddlIssue.SelectedValue + "'"); }
            if (ddlCardstatus.SelectedIndex > 0) { QS.Append(" AND CardStatus = '" + ddlCardstatus.SelectedValue + "'"); }
            if (ddlApprovalStatus.SelectedIndex > 0) { QS.Append(" AND IsApproved = '" + ddlApprovalStatus.SelectedValue + "'"); }
            if (ddlCreatedBy.SelectedIndex > 0) { QS.Append(" AND CreatedBy = '" + ddlCreatedBy.SelectedValue + "'"); }
            if (ddlPrintedBy.SelectedIndex > 0) { QS.Append(" AND PrintedBy = '" + ddlPrintedBy.SelectedValue + "'"); }

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
    protected void ClearUI()
    {
        txtEmpID.Text = "";
        ddlEmpType.SelectedIndex = -1;
        ddlCompID.SelectedIndex = -1;
        ddlSecID.SelectedIndex = -1;
        txtEmpName.Text = "";
        
        txtEmpNationalID.Text = "";
        
        txtCardID.Text = "";
        ddlIssue.SelectedIndex = -1;
        ddlCardstatus.SelectedIndex = -1;
        ddlApprovalStatus.SelectedIndex = -1;
        ddlCreatedBy.SelectedIndex = -1;
        ddlPrintedBy.SelectedIndex = -1;

        grdData.DataSource = new DataTable();
        grdData.DataBind();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    {
                        e.Row.Attributes.Add("onmouseover", "mouseout('alt_row_highlight',this);");
                        e.Row.Attributes.Add("onmouseout", "mouseover('alt_row_nohighlight',this);");
                        e.Row.Attributes.Add("onmousemove", "mousemove('alt_row_nohighlight',this);");
                        break;
                    }

            }
        }
        catch (Exception Ex)
        {
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        btnSearch_Click(null, null);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnk = (LinkButton)e.Row.FindControl("btnVRejectReason");
                lnk.Attributes.Add("onclick", "launchModal('" + DataBinder.Eval(e.Row.DataItem, "RejectReason") + "')");
            }
        }
        catch (Exception e1) { }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        try
        {
            string reject = e.CommandArgument.ToString();
            GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            int rowIndex = gvr.RowIndex;
            GridViewRow GDR = grdData.Rows[rowIndex];
            //string reject = ((LinkButton)GDR.FindControl("RejectReason")).Text;

            switch (e.CommandName)
            {
                case ("Reject"):

                    lblRejectRes.Text = reject;
                    this.mpe.Show();
                    
                    break;
            }
        }
        catch (Exception Ex)
        {
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}