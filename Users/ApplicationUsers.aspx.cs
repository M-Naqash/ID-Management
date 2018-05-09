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
using System.Net.Mail;
using System.Data.SqlClient;

public partial class ApplicationUsers : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DataTable dt;
    AppUsersPro ProClass = new AppUsersPro();
    AppUsersSql SqlClass  = new AppUsersSql();

    string MainPer     = "Usr";
    string MainName1Ar = "مستخدم";
    string MainName2Ar = "المستخدم";
    string MainNameEn  = "User";

    string MainQuery = " SELECT * FROM AppUsers ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //   --------------------Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Users",pageDiv);
            //   --------------------Common Code ----------------------------------------------------------------- //

            if (!string.IsNullOrEmpty(txtUsrPassword.Text)) { ViewState["Pass"] = txtUsrPassword.Text; }
            if (ViewState["Pass"] != null) { txtUsrPassword.Attributes["value"] = ViewState["Pass"].ToString(); }

            if (!IsPostBack)
            {
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
                        txtUsrLoginID.Enabled = true;
                    }

                    if (Request.QueryString["ac"].ToString() == "u")
                    {
                        if (!FormSession.PermUsr.Contains("U" + MainPer)) { Response.Redirect(@"~/Login.aspx"); }
                        btnSave.Enabled = FormSession.PermUsr.Contains("U" + MainPer);
                        ViewState["CommandName"] = "Update";
                        btnSave.Text = General.Msg("Update","تعديل");
                        MainMasterPage.ShowTitel(General.Msg("Update " + MainNameEn, "تعديل " + MainName1Ar));  
                        divUpdDel.Visible     = true;
                        txtUsrLoginID.Enabled = false;

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
                        txtUsrLoginID.Enabled = false;
                        
                        Fillddl();
                    }
 
                    FormCtrl.FillDDL("Status",ddlUsrStatus);
                }
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Fillddl()
    {
        rvUsrLoginIDSearch.Enabled = true;
        ddlUsrLoginID.Items.Clear();
        dt = DBFun.FetchData("SELECT * FROM AppUsers");
        if (!DBFun.IsNullOrEmpty(dt))
        {
            FormCtrl.PopulateDDL(ddlUsrLoginID, dt, "UsrLoginID", "UsrLoginID", General.Msg("-Select " + MainNameEn + "-","-اختر " + MainName2Ar + "-")); 
            rvUsrLoginIDSearch.InitialValue = ddlUsrLoginID.Items[0].Text; 
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool FindLoginID()
    {
        if (!string.IsNullOrEmpty(txtUsrLoginID.Text))
        {
            dt = DBFun.FetchData("SELECT * FROM AppUsers WHERE UsrLoginID = '" + txtUsrLoginID.Text + "'");
            if (!DBFun.IsNullOrEmpty(dt)) { return true; } else { return false; }
        }

        return false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            ProClass.DateType    = FormSession.DateType;
            ProClass.UsrLoginID  = txtUsrLoginID.Text;
            ProClass.UsrPassword = CryptorEngine.Encrypt(txtUsrPassword.Text, true);
            ProClass.UsrFullName = txtUsrFullName.Text;
            ProClass.UsrEmailID  = txtUsrEmailID.Text;
            //UsrPro.UsrEmpID       = txtUsrEmpID.Text;

            ProClass.UsrStartDate  = calUsrStartDate.getDate();
            if (!string.IsNullOrEmpty(ProClass.UsrStartDate)) { ProClass.UsrStartDateType = calUsrStartDate.getDateType(); }
            ProClass.UsrExpiryDate = calUsrExpiryDate.getDate();
            if (!string.IsNullOrEmpty(ProClass.UsrExpiryDate)) { ProClass.UsrExpiryDateType = calUsrExpiryDate.getDateType(); }
            
            if (ddlUsrStatus.SelectedIndex > -1)   { ProClass.UsrStatus   = Convert.ToBoolean(Convert.ToInt16(ddlUsrStatus.SelectedValue)); }     
            ProClass.UsrDescription = txtUsrDescription.Text;
            
            ProClass.UsrLanguage = "En";
            ProClass.UsrPermission = CryptorEngine.Encrypt(PermCtl.getPermissions(), true); 
            
            ProClass.TransactionBy = FormSession.LoginUsr;
        }
        catch (Exception e1)
        {
            DBFun.InsertError(FormSession.PageName, "FillUsrObject");
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
            FillPropeties();

            if (ViewState["CommandName"].ToString() == "Save")
            {
                SqlClass.Insert(ProClass);
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " added successfully", "تمت إضافة " + MainName2Ar + " بنجاح"));
            }

            if (ViewState["CommandName"].ToString() == "Update")
            {
                SqlClass.Update(ProClass);
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " updated successfully", "تم تعديل " + MainName2Ar + " بنجاح"));
            }


            if (ViewState["CommandName"].ToString() == "Delete")
            {
                if (txtUsrLoginID.Text == "admin")
                {
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Error, General.Msg("You can not delete a User : system administrator", "لا يمكن حذف المستخدم : مدير النظام"));
                    return;
                }

                bool isFound = false;
                //string Q = " SELECT LogTransactionBy AS CreatedBy FROM TransactionLog WHERE LogTransactionBy = '" + ddlUsrLoginID.SelectedValue + "' "
                //         + " UNION "
                         string Q = " SELECT CreatedBy AS CreatedBy FROM CardMaster WHERE CreatedBy = '" + ddlUsrLoginID.SelectedValue + "'"
                         + " UNION "
                         + " SELECT PrintedBy AS CreatedBy FROM CardMaster WHERE PrintedBy = '" + ddlUsrLoginID.SelectedValue + "'"
                         + " UNION "
                         + " SELECT ApprovedBy AS CreatedBy FROM CardMaster WHERE ApprovedBy = '" + ddlUsrLoginID.SelectedValue + "'"
                         + " UNION "
                         + " SELECT CreatedBy AS CreatedBy FROM StickerMaster WHERE CreatedBy = '" + ddlUsrLoginID.SelectedValue + "'"
                         + " UNION "
                         + " SELECT PrintedBy AS CreatedBy FROM StickerMaster WHERE PrintedBy = '" + ddlUsrLoginID.SelectedValue + "'"
                         + " UNION "
                         + " SELECT ApprovedBy AS CreatedBy FROM StickerMaster WHERE ApprovedBy = '" + ddlUsrLoginID.SelectedValue + "'"
                         + " UNION "
                         + " SELECT CreatedBy AS CreatedBy FROM VisitorsCard WHERE CreatedBy = '" + ddlUsrLoginID.SelectedValue + "'"
                         + " UNION "
                         + " SELECT PrintedBy AS CreatedBy FROM VisitorsCard WHERE PrintedBy = '" + ddlUsrLoginID.SelectedValue + "'";
                dt = DBFun.FetchData(Q);
                if (!DBFun.IsNullOrEmpty(dt)) { isFound = true; }

                if (isFound)
                {
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Error, General.Msg("Deletion can not because of the presence of related records", "لا يمكن الحذف بسبب وجود سجلات مرتبطة"));
                }
                else
                {
                    SqlClass.Delete(ProClass);
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " deleted successfully", "تم حذف " + MainName2Ar + " بنجاح"));
                }
            }

            ddlUsrLoginID.ClearSelection();
            Fillddl();
            ClearUI();
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "btnSave");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void ddlUsrLoginID_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtUsrPassword.Attributes["value"] = ""; 
        //txtEmpID.Text = "";
        ClearUI();
        PopulateUI(ddlUsrLoginID.SelectedValue.Trim());
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pID)
    {
        try
        {
            dt = DBFun.FetchData("SELECT * FROM AppUsers WHERE UsrLoginID = '" + pID + "'");
            if (DBFun.IsNullOrEmpty(dt)) { return; }

            txtUsrLoginID.Text                 = dt.Rows[0]["UsrLoginID"].ToString();
            if (txtUsrLoginID.Text == "admin") { EnableItem(false); } else { EnableItem(true); }

            txtUsrPassword.Attributes["value"] = CryptorEngine.Decrypt(dt.Rows[0]["UsrPassword"].ToString(), true);
            txtUsrFullName.Text                = dt.Rows[0]["UsrFullName"].ToString();
            txtUsrEmailID.Text                 = dt.Rows[0]["UsrEmailID"].ToString();
            
            //txtUsrEmpID.Text = dt.Rows[0]["UsrEmpID"].ToString();
            
            calUsrStartDate.setDBDate(dt.Rows[0]["UsrStartDate"],dt.Rows[0]["UsrStartDateType"].ToString());
            calUsrExpiryDate.setDBDate(dt.Rows[0]["UsrExpiryDate"],dt.Rows[0]["UsrExpiryDateType"].ToString());
            
            ddlUsrStatus.SelectedIndex   = ddlUsrStatus.Items.IndexOf(ddlUsrStatus.Items.FindByValue( Convert.ToInt16(dt.Rows[0]["UsrStatus"]).ToString()));            
            txtUsrDescription.Text = dt.Rows[0]["UsrDescription"].ToString();
            
            PermCtl.PopulatePermissions(dt.Rows[0]["UsrPermission"].ToString(),txtUsrLoginID.Text);
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void EnableItem( bool pStatus)
    {
        calUsrStartDate.setEnabled(pStatus);
        calUsrExpiryDate.setEnabled(pStatus);

        calUsrStartDate.setValidationEnabled(pStatus);
        calUsrExpiryDate.setValidationEnabled(pStatus);
        cvCompareDates.Enabled = pStatus;

        spnUsrStartDate.Visible  = pStatus;
        spnUsrExpiryDate.Visible = pStatus;

        ddlUsrStatus.Enabled   = pStatus;
        PermCtl.EnablePermissions(pStatus, true);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ClearUI()
    {
        txtUsrLoginID.Text = "";
        ViewState["Pass"]  = null;
        txtUsrPassword.Attributes["value"] = ""; 
        txtUsrFullName.Text = "";
        txtUsrEmailID.Text = "";
            
        //txtUsrEmpID.Text = "";
            
        calUsrStartDate.ClearDate(); 
        calUsrExpiryDate.ClearDate(); 
            
        ddlUsrStatus.SelectedIndex = 0; 
        txtUsrDescription.Text = "";
            
        PermCtl.Clear();

        EnableItem(true);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearUI();
        ddlUsrLoginID.ClearSelection();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnViewdetails_Click(object sender, EventArgs e)
    {
        //PopulateEmployeedetails(Convert.ToString(txtEmpID.Text));
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events
    
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void DateValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvCompareDates))
            {
                if (!String.IsNullOrEmpty(calUsrStartDate.getDate()) && !String.IsNullOrEmpty(calUsrExpiryDate.getDate()))
                {
                    int iStartDate = DateFun.ConvertDateTimeToInt(FormSession.DateType, calUsrStartDate.getDate());
                    int iEndDate = DateFun.ConvertDateTimeToInt(FormSession.DateType, calUsrExpiryDate.getDate());
                    if (iStartDate > iEndDate) { e.IsValid = false; } else { e.IsValid = true; }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void UsrLoginID_ServerValidate(Object source, ServerValidateEventArgs e)
    {
       try
        {
            if (source.Equals(cvUsrLoginID))
            {
                if (string.IsNullOrEmpty(txtUsrLoginID.Text))
                {
                    General.ValidMsg(this, ref cvUsrLoginID, false, "Login ID is required!", "اسم دخول المستخدم مطلوب !");
                    e.IsValid = false;
                    return;
                }
                else
                {
                    if (ViewState["CommandName"].ToString() == "Save")
                    {
                        dt = DBFun.FetchData(MainQuery + " WHERE UsrLoginID = '" + txtUsrLoginID.Text + "'");
                        if (!DBFun.IsNullOrEmpty(dt))
                        {
                            MessageFun.ValidMsg(this, ref cvUsrLoginID, true, General.Msg("The Login ID already exists", "اسم الدخول موجود مسبقا"));
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
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
