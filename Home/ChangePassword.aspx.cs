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
using System.Data.SqlClient;

public partial class ChangePassword : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    AppUsersPro AppPro = new AppUsersPro();
    AppUsersSql AppSql = new AppUsersSql();
   
    DataTable dt;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //   --------------------Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Home",pageDiv);
            MainMasterPage.ShowTitel(General.Msg("Change Password", "تغيير كلمة المرور"));
            //   --------------------Common Code ----------------------------------------------------------------- //
            if (!string.IsNullOrEmpty(txtOldpassword.Text)) { ViewState["OldPass"] = txtOldpassword.Text; }
            if (ViewState["OldPass"] != null) { txtOldpassword.Attributes["value"] = ViewState["OldPass"].ToString(); }

            if (!string.IsNullOrEmpty(txtNewpassword.Text)) { ViewState["NewPass"] = txtNewpassword.Text; }
            if (ViewState["NewPass"] != null) { txtNewpassword.Attributes["value"] = ViewState["NewPass"].ToString(); }

            if (!string.IsNullOrEmpty(txtConfirmpassword.Text)) { ViewState["ConfirmPass"] = txtConfirmpassword.Text; }
            if (ViewState["ConfirmPass"] != null) { txtConfirmpassword.Attributes["value"] = ViewState["ConfirmPass"].ToString(); }
        }
        catch (Exception e1) { DBFun.InsertError(FormSession.PageName, "Page_Load"); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
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

            AppPro.UsrLoginID  = FormSession.LoginUsr;
            AppPro.UsrPassword = CryptorEngine.Encrypt(txtNewpassword.Text, true);
            AppPro.TransactionBy = FormSession.LoginUsr;

            AppSql.UpdatePassword(AppPro);

            ClearUI();

            MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg("Password updated successfully", "تم تعديل كلمة المرور بنجاح"));
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "btnSave");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ClearUI()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        ViewState["OldPass"] = null;
        txtOldpassword.Attributes["value"] = null;

        ViewState["NewPass"] = null;
        txtNewpassword.Attributes["value"] = null;

        ViewState["ConfirmPass"] = null;
        txtConfirmpassword.Attributes["value"] = null;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events
    
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void OldValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvOld))
            {
                if (string.IsNullOrEmpty(txtOldpassword.Text))
                {
                    MessageFun.ValidMsg(this, ref cvOld, false, General.Msg("Old Password is required", "كلمة المرور السابقة مطلوبة"));
                    e.IsValid = false;
                    return;
                }
                else
                {
                    if (FormSession.Role == "User")    { dt = DBFun.FetchData("SELECT UsrPassword FROM AppUsers        WHERE UsrLoginID = '" + FormSession.LoginUsr + "'"); }
                    if (FormSession.Role == "Faculty") { dt = DBFun.FetchData("SELECT EmpPassword FROM EmployeeFaculty WHERE EmpLoginID = '" + FormSession.LoginUsr + "'"); }
                    if (FormSession.Role == "Officer") { dt = DBFun.FetchData("SELECT EmpPassword FROM EmployeeOfficer WHERE EmpLoginID = '" + FormSession.LoginUsr + "'"); }

                    if (!DBFun.IsNullOrEmpty(dt)) 
                    {
                        if (CryptorEngine.Decrypt(dt.Rows[0][0].ToString(), true) != txtOldpassword.Text)
                        {
                            MessageFun.ValidMsg(this, ref cvOld, true, General.Msg("The previous password is incorrect", "كلمة المرور السابقة غير صحيحة"));
                            e.IsValid = false;
                            return;
                        }
                    }
                }
            }
        }
        catch { e.IsValid = false; }
    }   
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ConfirmValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvConfirm))
            {
                if (string.IsNullOrEmpty(txtConfirmpassword.Text))
                {
                    MessageFun.ValidMsg(this, ref cvConfirm, false, General.Msg("Confirm Password is required", "يجب إدخال تأكيد كلمة المرور"));
                    e.IsValid = false;
                    return;
                }
                else
                {
                    if (txtNewpassword.Text != txtConfirmpassword.Text)
                    {
                        MessageFun.ValidMsg(this, ref cvConfirm, true, General.Msg("Password and Confirm Password must be same", "كلمة المرور وتأكيد كلمة المرور غير متطابقتين"));
                        e.IsValid = false;
                        return;
                    }
                }
            }
        }
        catch { e.IsValid = false; }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
