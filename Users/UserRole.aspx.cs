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

public partial class UserRole : BasePage
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DataTable dt;
    AppUsersPro ProClass = new AppUsersPro();
    AppUsersSql SqlClass = new AppUsersSql();
   
    string MainPer     = "Usr";
    string MainName1Ar = "مجموعة صلاحيات";
    string MainName2Ar = "مجموعة الصلاحيات";
    string MainNameEn  = "Role Permissions";

    string MainQuery = " SELECT * FROM RoleUserPermissions ";
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
                pnlMain.Attributes.Add("onkeypress", "javascript:return DefaultButton(event,'" + btnSave.ClientID + "');");

                if (Request.QueryString["ac"] != null)
                {

                    if (Request.QueryString["ac"].ToString() == "i")
                    {
                        if (!FormSession.PermUsr.Contains("I" + MainPer)) { Response.Redirect(@"~/Login.aspx"); }
                        btnSave.Enabled = FormSession.PermUsr.Contains("I" + MainPer);
                        ViewState["CommandName"] = "Save";
                        btnSave.Text = General.Msg("Save","حفظ");
                        MainMasterPage.ShowTitel(General.Msg("Add " + MainNameEn, "إضافة " + MainName1Ar));
                        divUpdDel.Visible = false;
                    }

                    if (Request.QueryString["ac"].ToString() == "u")
                    {
                        if (!FormSession.PermUsr.Contains("U" + MainPer)) { Response.Redirect(@"~/Login.aspx"); }
                        btnSave.Enabled = FormSession.PermUsr.Contains("U" + MainPer);
                        ViewState["CommandName"] = "Update";
                        btnSave.Text = General.Msg("Update","تعديل");
                        MainMasterPage.ShowTitel(General.Msg("Update " + MainNameEn, "تعديل " + MainName1Ar));  
                        divUpdDel.Visible     = true;

                        Fillddl();
                    }

                    if (Request.QueryString["ac"].ToString() == "d")
                    {
                        if (!FormSession.PermUsr.Contains("D" + MainPer)) { Response.Redirect(@"~/Login.aspx"); }
                        btnSave.Enabled = FormSession.PermUsr.Contains("D" + MainPer);
                        ViewState["CommandName"] = "Delete";
                        btnSave.Text = General.Msg("Delete","حذف");
                        MainMasterPage.ShowTitel(General.Msg("Delete " + MainNameEn, "حذف " + MainName1Ar));
                        divUpdDel.Visible = true;
                        
                        Fillddl();
                    }
                } 
            }
        }
        catch (Exception e1)
        {
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Fillddl()
    {
        ddlRoleID.Items.Clear();
        
        rvRoleID.Enabled = true;
        dt = DBFun.FetchData(MainQuery);
        if (!DBFun.IsNullOrEmpty(dt))
        {
            FormCtrl.PopulateDDL(ddlRoleID, dt, "RoleName"  + General.Lang(), "RoleID", General.Msg("-Select " + MainNameEn + "-","-اختر " + MainName2Ar + "-")); 
            rvRoleID.InitialValue = ddlRoleID.Items[0].Text; 
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        ProClass.DateType     = FormSession.DateType;

        if (ddlRoleID.SelectedIndex > 0) { ProClass.RoleID = ddlRoleID.SelectedValue; }
        
        ProClass.RoleNameAr      = txtRoleNameAr.Text;
        ProClass.RoleNameEn      = txtRoleNameEn.Text;
        ProClass.RolePermissions = CryptorEngine.Encrypt(PermissionsCtl.getPermissions(), true);  
        ProClass.TransactionBy   = FormSession.LoginUsr;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
            FillPropeties();

            if (ViewState["CommandName"].ToString() == "Save")
            {
                SqlClass.RoleInsert(ProClass);
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " added successfully", "تمت إضافة " + MainName2Ar + " بنجاح"));
            }

            if (ViewState["CommandName"].ToString() == "Update")
            {
                SqlClass.RoleUpdate(ProClass);
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " updated successfully", "تم تعديل " + MainName2Ar + " بنجاح"));
            }

            if (ViewState["CommandName"].ToString() == "Delete")
            {
                //dt = DBFun.FetchData("SELECT BrcID FROM CollegesFaculty WHERE BrcID = " + ddlPkID.SelectedValue);
                //if (!DBFun.IsNullOrEmpty(dt))
                if (txtRoleNameEn.Text == "admin" || txtRoleNameAr.Text == "مدير النظام")
                {
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Error, General.Msg("You can not delete a Role the system administrator", "لا يمكن حذف مجموعة صلاحيات مدير النظام"));
                    //MessageFun.ShowMsg(this, MessageFun.TypeMsg.Error, General.Msg("Deletion can not because of the presence of related records", "لا يمكن الحذف بسبب وجود سجلات مرتبطة"));
                }
                else
                {
                SqlClass.RoleDelete(ProClass);
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " deleted successfully", "تم حذف " + MainName2Ar + " بنجاح"));
                }
            }

            ClearUI();
            Fillddl();
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "btnSaveDelete");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pID)
    {
        try
        {
            dt = DBFun.FetchData(MainQuery + " WHERE RoleID = " + pID + "");
            if (DBFun.IsNullOrEmpty(dt)) { return; }

            txtRoleNameAr.Text = dt.Rows[0]["RoleNameAr"].ToString();
            txtRoleNameEn.Text = dt.Rows[0]["RoleNameEn"].ToString();

            if (dt.Rows[0]["RoleNameEn"].ToString() == "admin" || dt.Rows[0]["RoleNameAr"].ToString() == "مدير النظام") 
            {
                txtRoleNameEn.Enabled = false;
                txtRoleNameAr.Enabled = false;
                PermissionsCtl.EnablePermissions(false, true); 
            }
            else 
            { 
                txtRoleNameEn.Enabled = true;
                txtRoleNameAr.Enabled = true;
                PermissionsCtl.EnablePermissions(true, true); 
            }
            
            PermissionsCtl.PopulatePermissions(dt.Rows[0]["RolePermissions"].ToString(),dt.Rows[0]["RoleNameEn"].ToString());
        }
        catch (Exception e1) { }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
    protected void ClearUI()
    {
        txtRoleNameAr.Text = "";
        txtRoleNameEn.Text = "";
        ddlRoleID.ClearSelection();
        PermissionsCtl.EnablePermissions(true, true);
        PermissionsCtl.Clear();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e) { ClearUI(); }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlRoleID_SelectedIndexChanged(object sender, EventArgs e)
    {
        PermissionsCtl.EnablePermissions(true, true);
        PermissionsCtl.Clear();
        PopulateUI(ddlRoleID.SelectedValue);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    /**************************************************************************************************************************************/
    /**************************************************************************************************************************************/ 
    #region Custom Validate Events
    
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Name_ServerValidate(Object source, ServerValidateEventArgs e)
    {        
        string sqlUpdate = ViewState["CommandName"].ToString() == "Update" ? " AND RoleID != " + ddlRoleID.SelectedValue : "";

        try
        {
            if (ViewState["CommandName"].ToString() == "Save" || ViewState["CommandName"].ToString() == "Update")
            {
                if (source.Equals(cvRoleNameAr))
                {
                    if (string.IsNullOrEmpty(txtRoleNameAr.Text))
                    {
                        MessageFun.ValidMsg(this, ref cvRoleNameAr, false, General.Msg(MainNameEn + " Name (Ar) is required", "اسم " + MainName2Ar + " (ع) مطلوب"));
                        e.IsValid = false;
                        return;
                    }
                    else
                    {
                        dt = DBFun.FetchData(MainQuery + " WHERE RoleNameAr = '" + txtRoleNameAr.Text + "' " + sqlUpdate);
                        if (!DBFun.IsNullOrEmpty(dt))
                        {
                            MessageFun.ValidMsg(this, ref cvRoleNameAr, true, General.Msg(MainNameEn + " name (Ar) already exists", "اسم " + MainName2Ar + " (ع) موجود مسبقا"));
                            e.IsValid = false;
                            return;
                        }
                    }
                }

                if (source.Equals(cvRoleNameEn))
                {
                    if (string.IsNullOrEmpty(txtRoleNameEn.Text))
                    {
                        MessageFun.ValidMsg(this, ref cvRoleNameEn, false, General.Msg(MainNameEn + " Name (En) is required", "اسم " + MainName2Ar + " (E) مطلوب"));
                        e.IsValid = false;
                        return;
                    }
                    else
                    {
                        dt = DBFun.FetchData(MainQuery + " WHERE RoleNameEn = '" + txtRoleNameEn.Text + "' " + sqlUpdate);
                        if (!DBFun.IsNullOrEmpty(dt))
                        {
                            MessageFun.ValidMsg(this, ref cvRoleNameEn, true, General.Msg(MainNameEn + " name (En) already exists", "اسم " + MainName2Ar + " (E) موجود مسبقا"));
                            e.IsValid = false;
                            return;
                        }
                    }
                }
            }
        }
        catch { e.IsValid = false; }
    }

    #endregion
    /**************************************************************************************************************************************/
    /**************************************************************************************************************************************/

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
