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

public partial class Configuration_BlackListSearch : BasePage
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
            //   --------------------Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Config",pageDiv);
            //   --------------------Common Code ----------------------------------------------------------------- //

            if (!IsPostBack)
            {
                if (!FormSession.PermUsr.Contains("SBla")) {  Response.Redirect(@"~/Login.aspx"); }
                MainMasterPage.ShowTitel(General.Msg("Black List History", "بحث القائمة السوداء"));
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) { return; }
        Search();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Search()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            StringBuilder QS = new StringBuilder();
            QS.Append("SELECT *," + General.Msg("NatNameEn", "NatNameAr") + " AS NatName FROM BlackListInfoView WHERE 1=1 ");
            if (!string.IsNullOrEmpty(txtBlaIdentityNo.Text)) { QS.Append(" AND BlaIdentityNo LIKE '" + txtBlaIdentityNo.Text + "%'"); }
            if (!string.IsNullOrEmpty(txtBlaNameAr.Text))     { QS.Append(" AND BlaNameAr LIKE '" + txtBlaNameAr.Text + "%'"); }
            if (!string.IsNullOrEmpty(txtBlaNameEn.Text))     { QS.Append(" AND BlaNameEn LIKE '" + txtBlaNameEn.Text + "%'"); }

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
    protected void ClearUI() { FormCtrl.Clean(pageDiv); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e) { FormCtrl.GridRowColor(e.Row); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        Search();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events
    
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
}