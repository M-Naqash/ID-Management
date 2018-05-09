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

public partial class Configuration_BlackList : BasePage
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    BlackListPro ProCs = new BlackListPro();
    BlackListSql SqlCs = new BlackListSql();
    DataTable dt;
    
    string MainQuery = " SELECT * FROM BlackListInfoView ";
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //   --------------------Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Config",pageDiv);
            FillGrid();
            //   --------------------Common Code ----------------------------------------------------------------- //

            if (!IsPostBack)
            {
                if (Request.QueryString["ac"] != null)
                {
                    if (Request.QueryString["ac"].ToString() == "i")
                    {
                        if (!FormSession.PermUsr.Contains("IBla")) { Response.Redirect(@"~/Login.aspx"); }
                        btnSave.Enabled = FormSession.PermUsr.Contains("IBla");
                        ViewState["CommandName"] = "Save";
                        btnSave.Text = General.Msg("Save","حفظ");
                        MainMasterPage.ShowTitel(General.Msg("Add Black List", "إضافة القائمة السوداء"));
                        divUpdDel.Visible = false;
                        divAdd.Visible = true;

                        FillNatddl();
                    }

                    if (Request.QueryString["ac"].ToString() == "u")
                    {
                        if (!FormSession.PermUsr.Contains("UBla")) { Response.Redirect(@"~/Login.aspx"); }
                        btnSave.Enabled = FormSession.PermUsr.Contains("UBla");
                        ViewState["CommandName"] = "Update";
                        btnSave.Text = General.Msg("Update","تعديل");
                        MainMasterPage.ShowTitel(General.Msg("Update Black List", "تعديل القائمة السوداء"));                        
                        divUpdDel.Visible = true;
                        divAdd.Visible = false;
                        Fillddl();

                        FillNatddl();
                    }

                    if (Request.QueryString["ac"].ToString() == "d")
                    {
                        if (!FormSession.PermUsr.Contains("DBla")) { Response.Redirect(@"~/Login.aspx"); }
                        btnSave.Enabled = FormSession.PermUsr.Contains("DBla");
                        ViewState["CommandName"] = "Delete";
                        btnSave.Text = General.Msg("Delete","حذف");
                        MainMasterPage.ShowTitel(General.Msg("Delete Black List" , "حذف القائمة السوداء"));
                        divUpdDel.Visible = true;
                        divAdd.Visible = false;
                        Fillddl();

                        FillNatddl();
                    }
                }
            }
        }
        catch (Exception ex) { DBFun.InsertError(FormSession.PageName, "PageLoad"); }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Fillddl()
    {
        ddlBlaIdentityNo.Items.Clear();
        
        rvBlaIdentityNo.Enabled = true;
        dt = DBFun.FetchData(MainQuery);
        if (!DBFun.IsNullOrEmpty(dt))
        {
            FormCtrl.PopulateDDL(ddlBlaIdentityNo, dt, "BlaIdentityNo", "BlaIdentityNo", General.Msg("-Select Identity No.-","-اختر رقم الهوية-")); 
            rvBlaIdentityNo.InitialValue = ddlBlaIdentityNo.Items[0].Text; 
        }

        FillNatddl();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillNatddl()
    {
        dt = DBFun.FetchData("SELECT * FROM Nationality");
        if (!DBFun.IsNullOrEmpty(dt))
        {
            FormCtrl.PopulateDDL(ddlNatID, dt, "NatName"  + General.Lang(), "NatID", General.Msg("-Select Nationality-","-اختر الجنسية-")); 
            rvNatID.InitialValue = ddlNatID.Items[0].Text; 
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlBlaIdentityNo_SelectedIndexChanged(object sender, EventArgs e) { PopulateUI(ddlBlaIdentityNo.SelectedValue); }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pID)
    {
        dt = DBFun.FetchData(MainQuery + " WHERE BlaIdentityNo = " + pID + "");
        if (DBFun.IsNullOrEmpty(dt)) { return; }
        txtBlaIdentityNo.Text = dt.Rows[0]["BlaIdentityNo"].ToString();     
        txtBlaNameAr.Text = dt.Rows[0]["BlaNameAr"].ToString();
        txtBlaNameEn.Text = dt.Rows[0]["BlaNameEn"].ToString();
        ddlNatID.SelectedIndex = ddlNatID.Items.IndexOf(ddlNatID.Items.FindByValue(dt.Rows[0]["NatID"].ToString()));
        txtBlaReason.Text = dt.Rows[0]["BlaReason"].ToString();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ClearUI()
    {
        txtBlaIdentityNo.Text = "";
        txtBlaNameAr.Text = "";
        txtBlaNameEn.Text = "";
        txtBlaReason.Text = "";
        ddlBlaIdentityNo.ClearSelection();
        ddlNatID.ClearSelection();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        ProCs.DateType = FormSession.DateType;

        if (ddlBlaIdentityNo.SelectedIndex > 0) { ProCs.BlaIdentityNo = ddlBlaIdentityNo.SelectedValue; } else { ProCs.BlaIdentityNo = txtBlaIdentityNo.Text; }
        
        ProCs.BlaNameAr     = txtBlaNameAr.Text;
        ProCs.BlaNameEn     = txtBlaNameEn.Text;
        ProCs.NatID         = ddlNatID.SelectedValue;
        if (!string.IsNullOrEmpty( txtBlaReason.Text)) { ProCs.BlaReason = txtBlaReason.Text; }
        ProCs.TransactionBy = FormSession.LoginUsr;
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
                SqlCs.Insert(ProCs);
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg("Save Data successfully", "تم الحفظ بنجاح"));
            }

            if (ViewState["CommandName"].ToString() == "Update")
            {
                SqlCs.Update(ProCs);
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg("Save Data successfully", "تم الحفظ بنجاح"));
            }

            if (ViewState["CommandName"].ToString() == "Delete")
            {
                //string Q = " SELECT NatID FROM EmployeeMaster WHERE NatID = " + ddlPkID.SelectedValue + " "
                //         + " UNION "
                //         + " SELECT NatID FROM BlackList WHERE NatID = " + ddlPkID.SelectedValue + " ";
                //dt = DBFun.FetchData(Q);
                //if (!DBFun.IsNullOrEmpty(dt)) 
                //{ 
                //    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Error, General.Msg("Deletion can not because of the presence of related records", "لا يمكن الحذف بسبب وجود سجلات مرتبطة")); 
                //}
                //else
                //{
                    SqlCs.Delete(ProCs);
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg("deleted Data successfully", "تم الحذف بنجاح"));
                //}
            }
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "btnSave");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }

        ClearUI();
        Fillddl();
        FillGrid();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e) { ClearUI(); }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e) { FormCtrl.GridRowColor(e.Row); }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillGrid()
    {
        string Q = "SELECT *," + General.Msg("NatNameEn", "NatNameAr") + " AS NatName FROM BlackListInfoView ";

        dt = DBFun.FetchData(Q);
        if (!DBFun.IsNullOrEmpty(dt))
        {
            grdData.DataSource = (DataTable)dt;
            grdData.DataBind();
        }
        else
        {
            FormCtrl.FillGridEmpty(ref grdData,20,"No Data Found","لا توجد بيانات");
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        FillGrid();
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
        string sqlUpdate = ViewState["CommandName"].ToString() == "Update" ? " AND BlaIdentityNo != " + ddlBlaIdentityNo.SelectedValue : "";

        try
        {
            if (ViewState["CommandName"].ToString() == "Save" || ViewState["CommandName"].ToString() == "Update")
            {
                if (source.Equals(cvBlaNameAr))
                {
                    if (string.IsNullOrEmpty(txtBlaNameAr.Text))
                    {
                        MessageFun.ValidMsg(this, ref cvBlaNameAr, false, General.Msg("Name (Ar) is required", "الاسم بالعربي مطلوب"));
                        e.IsValid = false;
                        return;
                    }
                    else
                    {
                        dt = DBFun.FetchData(MainQuery + " WHERE NatNameAr = '" + txtBlaNameAr.Text + "' " + sqlUpdate);
                        if (!DBFun.IsNullOrEmpty(dt))
                        {
                            MessageFun.ValidMsg(this, ref cvBlaNameAr, true, General.Msg("Name (Ar) already exists", "الاسم بالعربي موجود مسبقا"));
                            e.IsValid = false;
                            return;
                        }
                    }
                }

                if (source.Equals(cvBlaNameEn))
                {
                    if (string.IsNullOrEmpty(txtBlaNameEn.Text))
                    {
                        MessageFun.ValidMsg(this, ref cvBlaNameEn, false, General.Msg("Name (En) is required", "الاسم بالانجليزي مطلوب"));
                        e.IsValid = false;
                        return;
                    }
                    else
                    {
                        dt = DBFun.FetchData(MainQuery + " WHERE NatNameEn = '" + txtBlaNameEn.Text + "' " + sqlUpdate);
                        if (!DBFun.IsNullOrEmpty(dt))
                        {
                            MessageFun.ValidMsg(this, ref cvBlaNameEn, true, General.Msg("Name (En) already exists", "الاسم بالانجليزي موجود مسبقا"));
                            e.IsValid = false;
                            return;
                        }
                    }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void IdentityNo_ServerValidate(Object source, ServerValidateEventArgs e)
    {        
        //string sqlUpdate = ViewState["CommandName"].ToString() == "Update" ? " AND BlaIdentityNo != " + txtBlaIdentityNo.Text : "";

        try
        {
            if (ViewState["CommandName"].ToString() == "Save")
            {
                if (source.Equals(cvBlaIdentityNo))
                {
                    if (string.IsNullOrEmpty(txtBlaIdentityNo.Text))
                    {
                        MessageFun.ValidMsg(this, ref cvBlaIdentityNo, false, General.Msg("Identity No. is required", "رقم الهوية مطلوب"));
                        e.IsValid = false;
                        return;
                    }
                    else
                    {
                        dt = DBFun.FetchData(MainQuery + " WHERE BlaIdentityNo = '" + txtBlaIdentityNo.Text + "'");
                        if (!DBFun.IsNullOrEmpty(dt))
                        {
                            MessageFun.ValidMsg(this, ref cvBlaIdentityNo, true, General.Msg("Identity No. already exists", "رقم الهوية موجود مسبقا"));
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

