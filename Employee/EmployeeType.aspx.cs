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
using System.Globalization;
using System.Web.Profile;
using System.Text;
using System.IO;
using System.Drawing;

public partial class Employee_EmployeeType : BasePage
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmployeePro ProClass = new EmployeePro();
    EmployeeSql SqlClass = new EmployeeSql();
    DataTable dt;

    string MainPer = "Employees";
    string MainName1Ar = "موظف";
    string MainName2Ar = "الموظف";
    string MainNameEn = "Employee";

    string MainQuery = " SELECT * FROM Employees ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //---Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Employees", pageDiv);
            //---Common Code ----------------------------------------------------------------- //

            if (!IsPostBack)
            {
                if (!FormSession.getPerm("UEmpType")) { Response.Redirect(@"~/Login.aspx"); }
                MainMasterPage.ShowTitel(General.Msg("Transferr Employees", "نقل الموظفين"));
                ButtonAction("00", true);
            }
            
        }
        catch (Exception e1) { DBFun.InsertError(FormSession.PageName, "PageLoad"); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ButtonAction(string pBtn, bool search) //string pBtn = [Save,Cancel]
    {
        btnSave.Enabled = btnCancel.Enabled = false;

        btnSave.Enabled = getStatus(pBtn[0]);
        btnCancel.Enabled = getStatus(pBtn[1]);

        btnIDSearch.Enabled = txtIDSearch.Enabled = search;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool getStatus(char BtnStatus) { return Convert.ToBoolean(Convert.ToInt32(BtnStatus.ToString())); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProClass.DateType = FormSession.DateType;
            ProClass.EmpID = txtEmployeeID.Text;
            ProClass.EmpType = ddlProcessType.SelectedValue;

            ProClass.TransactionBy = FormSession.LoginUsr;
            ProClass.TransactionDate = DateTime.Now.ToShortDateString();
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "FillPropeties");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (!Page.IsValid)
        {
            ValidatorCollection ValidatorColl = Page.Validators;
            for (int k = 0; k < ValidatorColl.Count; k++)
            {
                if (!ValidatorColl[k].IsValid && !String.IsNullOrEmpty(ValidatorColl[k].ErrorMessage)) { vsSave.ShowSummary = true; return; }
                vsSave.ShowSummary = false;
            }
            return;
        }

        try
        {
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (ddlProcessType.SelectedValue == ViewState["EmpType"].ToString())
            {
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Warning, General.Msg("Employee is already part of " + GetNameType(ViewState["EmpType"].ToString()), "الموظف موجود فعلياً ضمن قائمة " + GetNameType(ViewState["EmpType"].ToString())));
                return;
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            FillPropeties();
            SqlClass.UpdateType(ProClass);
            ClearUI();
            txtIDSearch.Text = "";
            MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " Update Employee type successfully", "تم تعديل نوع " + MainName2Ar + " بنجاح"));

            ButtonAction("00", true);
        }

        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "btnSave");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearUI();
        ButtonAction("00", true);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnIDSearch_Click(object sender, EventArgs e)
    {
        ClearUI();

        if (!Page.IsValid)
        {
            ValidatorCollection ValidatorColl = Page.Validators;
            for (int k = 0; k < ValidatorColl.Count; k++)
            {
                if (!ValidatorColl[k].IsValid && !String.IsNullOrEmpty(ValidatorColl[k].ErrorMessage)) { vsFetch.ShowSummary = true; return; }
                vsFetch.ShowSummary = false;
            }
            return;
        }

        PopulateUI();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void PopulateUI()
    {
        try
        {
            txtEmployeeID.Text = ViewState["EmpID"].ToString();
            txtEmpName.Text = ViewState["EmpName"].ToString();
            
            ButtonAction("11", false);
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "PopulateUI");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ClearUI()
    {
        txtEmployeeID.Text = "";
        txtEmpName.Text = "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetNameType(string pType)
    {
        string typeName = "";
        try
        {
            if (pType.Contains("Mng")) { typeName = General.Msg("Managers", "المدراء"); }
            if (pType.Contains("Emp")) { typeName = General.Msg("Employees", "الموظفين"); }
            if (pType.Contains("Con")) { typeName = General.Msg("Contractors Company", "متعاقدي الشركات"); }

            return typeName;
        }
        catch (Exception e1)
        {
            return string.Empty;
        }
    }

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void IDSearch_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvIDSearch))
            {
                if (string.IsNullOrEmpty(txtIDSearch.Text))
                {
                    General.ValidMsg(this, ref cvIDSearch, false, "You must enter a value in the search text", "يجب إدخال قيمة في مربع البحث");
                    e.IsValid = false;
                    return;
                }
                ///////////////////////////////////////////
                ViewState["EmpID"]   = "";
                ViewState["EmpType"] = "";
                ViewState["EmpName"] = "";

                if      (ddlSearchBy.SelectedValue == "EmpID")          { dt = DBFun.FetchData("SELECT EmpID,EmpType,EmpName" + FormSession.Language + " FROM EmployeeMaster WHERE EmpID = '" + txtIDSearch.Text + "'"); }
                else if (ddlSearchBy.SelectedValue == "EmpNationalID")  { dt = DBFun.FetchData("SELECT EmpID,EmpType,EmpName" + FormSession.Language + " FROM EmployeeMaster WHERE EmpNationalID = '" + txtIDSearch.Text + "'"); }
                else if (ddlSearchBy.SelectedValue == "EmpName")        { dt = DBFun.FetchData("SELECT EmpID,EmpType,EmpName" + FormSession.Language + " FROM EmployeeMaster WHERE EmpName" + FormSession.Language + " LIKE '%" + txtIDSearch.Text + "%'"); }

                if (DBFun.IsNullOrEmpty(dt))
                {
                    MessageFun.ValidMsg(this, ref cvIDSearch, true, General.Msg("Employee does not exist", " الموظف غير موجود"));
                    e.IsValid = false;
                    return;
                }

                ViewState["EmpID"] = dt.Rows[0][0].ToString();
                ViewState["EmpType"] = dt.Rows[0][1].ToString();
                ViewState["EmpName"] = dt.Rows[0][2].ToString();
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
}