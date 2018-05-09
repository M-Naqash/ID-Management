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

public partial class EmployeeFingerPrint : BasePage
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmployeePro ProClass = new EmployeePro();
    EmployeeSql SqlClass = new EmployeeSql();
    DataTable dt;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //   --------------------Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Employees", pageDiv);
            //   --------------------Common Code ----------------------------------------------------------------- //

            if (!IsPostBack)
            {
                txtEmpIDSearch.Enabled = FormSession.getPerm("VFPEmp");
                btnSearchDetails.Enabled = FormSession.getPerm("VFPEmp");
                MainMasterPage.ShowTitel(General.Msg("Employees FingerPrint", "بصمات الموظفين"));

                if (FormSession.getPerm("VFPEmp"))
                {
                    hfdConnStr.Value   = ConfigurationManager.ConnectionStrings["constring"].ConnectionString.Replace("\\","....");
                    hfdLoginUser.Value = FormSession.LoginUsr.Replace("\\","....");
                    hfdLang.Value      = FormSession.Language;
                    string ID = hfdConnStr.Value + "," + hfdLoginUser.Value + "," + hfdLang.Value;
                    ClientScript.RegisterStartupScript(this.GetType(), "key", "javascript:Connect('" + ID + "');", true);
                }
            }
        }
        catch (Exception e1) { DBFun.InsertError(FormSession.PageName, "PageLoad"); }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    public void ClearUI()
    {
        txtEmpIDSearch.Text = "";       
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSearchDetails_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtEmpIDSearch.Text))
        {
            dt = DBFun.FetchData("select * from EmployeeMaster WHERE EmpID = '" + txtEmpIDSearch.Text + "'");
            if (!DBFun.IsNullOrEmpty(dt))
            {
                string ID = hfdConnStr.Value + "%" + txtEmpIDSearch.Text + "%" + hfdLoginUser.Value + "%" + hfdLang.Value + "%" + hfdFile.Value;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "Connect('" + ID + "');", true);
            }
            else
            {
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Error, General.Msg("Employee ID does not exist,Please enter different ID", "رقم الموظف غير موجود,من فضلك اختر رقما آخر"));
                //string ID = hfdConnStr.Value + "%%" + hfdLoginUser.Value + "%" + hfdLang.Value + "%" + hfdFile.Value;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "Connect('" + ID + "');", true);
            }
        }
        else
        {
            General.ShowMsg(this,"You must enter the employee ID", "يجب إدخال رقم الموظف");
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
}
