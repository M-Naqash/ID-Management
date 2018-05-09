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

public partial class Companies_Companies : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    CompaniesPro ProClass = new CompaniesPro();
    CompaniesSql SqlClass = new CompaniesSql();
    DataTable dt;
    string MainPer     = "Companies";
    string MainName1Ar = "شركة";
    string MainName2Ar = "الشركة";
    string MainNameEn  = "Company";

    string MainQuery = " SELECT CompID AS PKID,CompNameAr AS NameAr,CompNameEn AS NameEn FROM Companies ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //   --------------------Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Config", pageDiv);
            FillGrid();
            //   --------------------Common Code ----------------------------------------------------------------- //

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
                    }

                    if (Request.QueryString["ac"].ToString() == "u")
                    {
                        if (!FormSession.PermUsr.Contains("U" + MainPer)) { Response.Redirect(@"~/Login.aspx"); }
                        btnSave.Enabled = FormSession.PermUsr.Contains("U" + MainPer);
                        ViewState["CommandName"] = "Update";
                        btnSave.Text = General.Msg("Update","تعديل");
                        MainMasterPage.ShowTitel(General.Msg("Update " + MainNameEn, "تعديل " + MainName1Ar));                        
                        divUpdDel.Visible = true;
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
        catch (Exception EX) { DBFun.InsertError(FormSession.PageName, "PageLoad"); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Fillddl()
    {
        ddlPkID.Items.Clear();
        
        rvPkID.Enabled = true;
        dt = DBFun.FetchData(MainQuery);
        if (!DBFun.IsNullOrEmpty(dt))
        {
            FormCtrl.PopulateDDL(ddlPkID, dt, "Name"  + General.Lang(), "PKID", General.Msg("-Select " + MainNameEn + "-","-اختر " + MainName2Ar + "-")); 
            rvPkID.InitialValue = ddlPkID.Items[0].Text; 
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlPkID_SelectedIndexChanged(object sender, EventArgs e) { PopulateUI(ddlPkID.SelectedValue); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pID)
    {
        dt = DBFun.FetchData(MainQuery + " WHERE CompID = " + pID + "");
        if (DBFun.IsNullOrEmpty(dt)) { return; }
        txtNameAr.Text = dt.Rows[0]["NameAr"].ToString();
        txtNameEn.Text = dt.Rows[0]["NameEn"].ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ClearUI()
    {
        txtNameAr.Text = "";
        txtNameEn.Text = "";
        ddlPkID.ClearSelection();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        
        ProClass.DateType      = FormSession.DateType;
        ProClass.NameAr        = txtNameAr.Text;
        ProClass.NameEn        = txtNameEn.Text;
        ProClass.TransactionBy = FormSession.LoginUsr;

        if (ddlPkID.SelectedIndex > 0) { ProClass.PKID = ddlPkID.SelectedValue; }
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
                dt = DBFun.FetchData("SELECT CompID FROM EmployeeMaster WHERE CompID = " + ddlPkID.SelectedValue);
                if (!DBFun.IsNullOrEmpty(dt)) 
                { 
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Error, General.Msg("Deletion can not because of the presence of related records", "لا يمكن الحذف بسبب وجود سجلات مرتبطة")); 
                }
                else
                {
                    SqlClass.Delete(ProClass);
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " deleted successfully", "تم حذف " + MainName2Ar + " بنجاح"));
                }
            }

            ClearUI();
            Fillddl();
            FillGrid();
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "btnSave");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e) { ClearUI(); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e) { FormCtrl.GridRowColor(e.Row); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillGrid()
    {
        dt = DBFun.FetchData(MainQuery);
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
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/ 
    #region Custom Validate Events
    
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Name_ServerValidate(Object source, ServerValidateEventArgs e)
    {        
        string sqlUpdate = ViewState["CommandName"].ToString() == "Update" ? " AND CompID != " + ddlPkID.SelectedValue : "";

        try
        {
            if (ViewState["CommandName"].ToString() == "Save" || ViewState["CommandName"].ToString() == "Update")
            {
                if (source.Equals(cvNameAr))
                {
                    if (string.IsNullOrEmpty(txtNameAr.Text))
                    {
                        MessageFun.ValidMsg(this, ref cvNameAr, false, General.Msg(MainNameEn + " Name (Ar) is required", "اسم " + MainName2Ar + " (ع) مطلوب"));
                        e.IsValid = false;
                        return;
                    }
                    else
                    {
                        dt = DBFun.FetchData(MainQuery + " WHERE CompNameAr = '" + txtNameAr.Text + "' " + sqlUpdate);
                        if (!DBFun.IsNullOrEmpty(dt))
                        {
                            MessageFun.ValidMsg(this, ref cvNameAr, true, General.Msg(MainNameEn + " name (Ar) already exists", "اسم " + MainName2Ar + " (ع) موجود مسبقا"));
                            e.IsValid = false;
                            return;
                        }
                    }
                }

                if (source.Equals(cvNameEn))
                {
                    if (string.IsNullOrEmpty(txtNameEn.Text))
                    {
                        MessageFun.ValidMsg(this, ref cvNameEn, false, General.Msg(MainNameEn + " Name (En) is required", "اسم " + MainName2Ar + " (E) مطلوب"));
                        e.IsValid = false;
                        return;
                    }
                    else
                    {
                        dt = DBFun.FetchData(MainQuery + " WHERE CompNameEn = '" + txtNameEn.Text + "' " + sqlUpdate);
                        if (!DBFun.IsNullOrEmpty(dt))
                        {
                            MessageFun.ValidMsg(this, ref cvNameEn, true, General.Msg(MainNameEn + " name (En) already exists", "اسم " + MainName2Ar + " (E) موجود مسبقا"));
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
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/ 

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
