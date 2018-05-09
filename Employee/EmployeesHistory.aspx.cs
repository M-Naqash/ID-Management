using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Employee_EmployeesHistory : BasePage
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
            FormSession.FillSession("Employees", pageDiv);
            //   --------------------Common Code ----------------------------------------------------------------- //

            if (!IsPostBack)
            {
                if (!FormSession.getPerm("SEmployees")) { Response.Redirect(@"~/Login.aspx"); }
                MainMasterPage.ShowTitel(General.Msg("Employees History", "سجلات الموظفين"));
                Fillddl();
                DataLang();

                if (!FormSession.getPerm("ICrd")) { grdData.Columns[16].Visible = false; }
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Fillddl()
    {
        DataTable CompDT = DBFun.FetchData("SELECT * FROM Companies ");
        if (!DBFun.IsNullOrEmpty(CompDT)) { FormCtrl.PopulateDDL(ddlCompID, CompDT, "CompName" + FormSession.Language, "CompID", General.Msg("-Select Company-", "-اختر الشركة-")); }

        DataTable SecDT = DBFun.FetchData("SELECT * FROM SectionsExternal ");
        if (!DBFun.IsNullOrEmpty(SecDT)) { FormCtrl.PopulateDDL(ddlSecID, SecDT, "SecName" + FormSession.Language, "SecID", General.Msg("-Select Sections External-", "-اختر الجهة الخارجية-")); }
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
            QS.Append(" SELECT * FROM EmployeeWithHaveFPInfoView WHERE EmpID = EmpID ");

            if (!string.IsNullOrEmpty(txtEmpID.Text)) { QS.Append(" AND EmpID = '" + txtEmpID.Text + "'"); }
            if (ddlEmpType.SelectedIndex > 0) { QS.Append(" AND EmpType = '" + ddlEmpType.SelectedValue + "' "); }
            if (!string.IsNullOrEmpty(txtEmpName.Text)) { QS.Append(" AND EmpName" + FormSession.Language + " LIKE '" + txtEmpName.Text + "%'"); }
            if (!string.IsNullOrEmpty(txtNationalID.Text)) { QS.Append(" AND EmpNationalID = '" + txtNationalID.Text + "' "); }
            if (ddlCompID.SelectedIndex > 0) { QS.Append(" AND CompID = '" + ddlCompID.SelectedValue + "' "); }
            if (ddlSecID.SelectedIndex > 0) { QS.Append(" AND SecID = '" + ddlSecID.SelectedValue + "' "); }

            if (ddlHaveCard.SelectedValue == "0") { }
            else if (ddlHaveCard.SelectedValue == "1") { QS.Append(" AND HaveCard = 'True'"); }
            else if (ddlHaveCard.SelectedValue == "2") { QS.Append(" AND HaveCard = 'False'"); }

            QS.Append(" ORDER BY EmpID ");

            dt = DBFun.FetchData(QS.ToString());
            if (!DBFun.IsNullOrEmpty(dt))
            {
                grdData.DataSource = (DataTable)dt;
                grdData.DataBind();
            }
            else
            {
                FormCtrl.FillGridEmpty(ref grdData, 20, "No records found with the given search criterion", "لا توجد سجلات بحسب شروط البحث المحددة");
            }
        }
        catch (Exception Ex) 
        {
            DBFun.InsertError(FormSession.PageName, "Search");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void DataLang()
    {
        if (FormSession.Language == "Ar")
        {
            grdData.Columns[2].Visible = false;
            grdData.Columns[4].Visible = false;
            grdData.Columns[6].Visible = false;
            grdData.Columns[10].Visible = false;
            grdData.Columns[12].Visible = false;
        }
        else
        {
            grdData.Columns[3].Visible = false;
            grdData.Columns[5].Visible = false;
            grdData.Columns[7].Visible = false;
            grdData.Columns[11].Visible = false;
            grdData.Columns[13].Visible = false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e) { ClearUI(); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ClearUI() { FormCtrl.Clean(pageDiv); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e) 
    { 
        try
        {
            FormCtrl.GridRowColor(e.Row);

            switch (e.Row.RowType)
            {
                case DataControlRowType.Pager:
                    {
                        break;
                    }
                default:
                    {
                        e.Row.Cells[17].Visible = false;
                        break;
                    }
            }
        }
        catch (Exception ex) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        Search();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public static string HaveFingerPrintGrd(object Val)
    {
        if (Val == DBNull.Value) { return ""; }
        else if (string.IsNullOrEmpty(Val.ToString())) { return ""; }
        else if (Val.ToString() == "True") { return General.Msg("Yes","نعم"); }
        else if (Val.ToString() == "False") { return General.Msg("No","لا"); }
        
        return "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case ("NewCard"):
                    string EmpID = e.CommandArgument.ToString();
                    Response.Redirect(@"~/Cards/CardMaster.aspx?ac=i&ID=" + EmpID);
                    break;
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            ImageButton _btn = (ImageButton)e.Row.Cells[16].Controls[1];
            string HaveCard = e.Row.Cells[17].Text;

            if (_btn != null && !string.IsNullOrEmpty(HaveCard))
            {
                if (HaveCard == "True") { _btn.Visible = false; } else if (HaveCard == "False") { _btn.Visible = true; }
            }
            else
            {
                 _btn.Visible = false;
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
}