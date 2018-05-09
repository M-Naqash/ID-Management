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

public partial class Visitors_VisitorsSearch : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DataTable dt;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //---Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Visitors",pageDiv);
            //---Common Code ----------------------------------------------------------------- //

            if (!IsPostBack)
            {
                MainMasterPage.ShowTitel(General.Msg("Events Cards History", "سجلات بطاقات المناسبات"));
                if (!FormSession.PermUsr.Contains("SVis")) { Response.Redirect(@"~/Login.aspx"); }
                btnSearch.Enabled = btnCancel.Enabled = FormSession.PermUsr.Contains("SVis");
                Fillddl();
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Fillddl()
    {
        DataTable CDT = DBFun.FetchData("SELECT DISTINCT CreatedBy FROM VisitorsCard WHERE CreatedBy IS NOT NULL ");
        if (!DBFun.IsNullOrEmpty(CDT)) { FormCtrl.PopulateDDL(ddlCreatedBy, CDT, "CreatedBy", "CreatedBy", General.Msg("-Select Created By-", "-اختر إنشاء بواسطة-")); }

        DataTable PDT = DBFun.FetchData("SELECT DISTINCT PrintedBy FROM VisitorsCard WHERE PrintedBy IS NOT NULL ");
        if (!DBFun.IsNullOrEmpty(PDT)) { FormCtrl.PopulateDDL(ddlPrintedBy, PDT, "PrintedBy", "PrintedBy", General.Msg("-Select Printed By-", "-اختر طباعة بواسطة-")); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            StringBuilder QS = new StringBuilder();
            QS.Append(" SELECT * FROM VisitorsCard WHERE  1 = 1 ");

            if (!string.IsNullOrEmpty(txtVisCardID.Text))     { QS.Append(" AND VisCardID = '" + txtVisCardID.Text + "'"); }
            if (!string.IsNullOrEmpty(txtVisIdentityNo.Text)) { QS.Append(" AND VisIdentityNo = '" + txtVisIdentityNo.Text + "'"); }
            if (!string.IsNullOrEmpty(txtVisNameAr.Text))     { QS.Append(" AND VisNameAr LIKE '%" + txtVisNameAr.Text + "%'"); }
            if (!string.IsNullOrEmpty(txtVisNameEn.Text))     { QS.Append(" AND VisNameEn LIKE '%" + txtVisNameEn.Text + "%'"); }
            if (!string.IsNullOrEmpty(txtVisMobileNo.Text))   { QS.Append(" AND VisMobileNo = '" + txtVisMobileNo.Text + "'"); }
            if (ddlCardstatus.SelectedIndex > 0)              { QS.Append(" AND CardStatus = '" + ddlCardstatus.SelectedValue + "'"); }
            if (ddlCreatedBy.SelectedIndex  > 0)              { QS.Append(" AND CreatedBy = '" + ddlCreatedBy.SelectedValue + "'"); }
            if (ddlPrintedBy.SelectedIndex  > 0)              { QS.Append(" AND PrintedBy = '" + ddlPrintedBy.SelectedValue + "'"); }


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
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e) { ClearUI(); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ClearUI()
    {
        txtVisCardID.Text     = "";
        txtVisIdentityNo.Text = "";
        txtVisNameAr.Text     = "";
        txtVisNameEn.Text     = "";
        txtVisMobileNo.Text   = "";
        
        ddlCardstatus.SelectedIndex = -1;
        ddlCreatedBy.SelectedIndex  = -1;
        ddlPrintedBy.SelectedIndex  = -1;

        grdData.DataSource = new DataTable();
        grdData.DataBind();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        catch (Exception e1)
        {
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        btnSearch_Click(null, null);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}