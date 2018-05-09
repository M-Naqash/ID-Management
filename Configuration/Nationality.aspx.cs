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

public partial class Configuration_Nationality : BasePage
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    NationalityPro ProClass = new NationalityPro();
    NationalitySql SqlClass = new NationalitySql();
    DataTable dt;
    
    string MainPer     = "Nat";
    string MainName1Ar = "جنسية - دولة";
    string MainName2Ar = "الجنسية - الدولة";
    string MainName3Ar = "الجنسية";
    string MainName4Ar = "الدولة";
    string MainName1En  = "Nationality - Country";
    string MainName2En  = "Nationality";
    string MainName3En  = "Country";

    string MainQuery = " SELECT NatID AS PKID,NatNameAr AS NameAr,NatNameEn AS NameEn,CountryNameAr,CountryNameEn FROM Nationality ";
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
                        if (!FormSession.PermUsr.Contains("I" + MainPer)) { Response.Redirect(@"~/Login.aspx"); }
                        btnSave.Enabled = FormSession.PermUsr.Contains("I" + MainPer);
                        ViewState["CommandName"] = "Save";
                        btnSave.Text = General.Msg("Save","حفظ");
                        MainMasterPage.ShowTitel(General.Msg("Add " + MainName1En, "إضافة " + MainName1Ar));
                        divUpdDel.Visible = false;
                    }

                    if (Request.QueryString["ac"].ToString() == "u")
                    {
                        if (!FormSession.PermUsr.Contains("U" + MainPer)) { Response.Redirect(@"~/Login.aspx"); }
                        btnSave.Enabled = FormSession.PermUsr.Contains("U" + MainPer);
                        ViewState["CommandName"] = "Update";
                        btnSave.Text = General.Msg("Update","تعديل");
                        MainMasterPage.ShowTitel(General.Msg("Update " + MainName1En, "تعديل " + MainName1Ar));                        
                        divUpdDel.Visible = true;
                        Fillddl();
                    }

                    if (Request.QueryString["ac"].ToString() == "d")
                    {
                        if (!FormSession.PermUsr.Contains("D" + MainPer)) { Response.Redirect(@"~/Login.aspx"); }
                        btnSave.Enabled = FormSession.PermUsr.Contains("D" + MainPer);
                        ViewState["CommandName"] = "Delete";
                        btnSave.Text = General.Msg("Delete","حذف");
                        MainMasterPage.ShowTitel(General.Msg("Delete " + MainName1En, "حذف " + MainName1Ar));
                        divUpdDel.Visible = true;
                        Fillddl();
                    }
                }
            }
        }
        catch (Exception EX) { DBFun.InsertError(FormSession.PageName, "PageLoad"); }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Fillddl()
    {
        ddlPkID.Items.Clear();
        
        rvPkID.Enabled = true;
        dt = DBFun.FetchData(MainQuery);
        if (!DBFun.IsNullOrEmpty(dt))
        {
            FormCtrl.PopulateDDL(ddlPkID, dt, "Name"  + General.Lang(), "PKID", General.Msg("-Select " + MainName1En + "-","-اختر " + MainName2Ar + "-")); 
            rvPkID.InitialValue = ddlPkID.Items[0].Text; 
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlPkID_SelectedIndexChanged(object sender, EventArgs e) { PopulateUI(ddlPkID.SelectedValue); }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pID)
    {
        dt = DBFun.FetchData(MainQuery + " WHERE NatID = " + pID + "");
        if (DBFun.IsNullOrEmpty(dt)) { return; }
        txtNameAr.Text = dt.Rows[0]["NameAr"].ToString();
        txtNameEn.Text = dt.Rows[0]["NameEn"].ToString();
        txtCountryNameAr.Text = dt.Rows[0]["CountryNameAr"].ToString();
        txtCountryNameEn.Text = dt.Rows[0]["CountryNameEn"].ToString();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ClearUI()
    {
        txtNameAr.Text = "";
        txtNameEn.Text = "";
        txtCountryNameAr.Text = "";
        txtCountryNameEn.Text = "";
        ddlPkID.ClearSelection();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void FillPropeties()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        ProClass.DateType        = FormSession.DateType;
        ProClass.NameAr          = txtNameAr.Text;
        ProClass.NameEn          = txtNameEn.Text;
        ProClass.CountryNameAr   = txtCountryNameAr.Text;
        ProClass.CountryNameEn   = txtCountryNameEn.Text;
        ProClass.TransactionBy   = FormSession.LoginUsr;
        ProClass.TransactionDate = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
        if (ddlPkID.SelectedIndex > 0) { ProClass.PKID = ddlPkID.SelectedValue; }
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
                SqlClass.Insert(ProClass);
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg( MainName1En + " added successfully", "تمت إضافة " + MainName2Ar + " بنجاح"));
            }

            if (ViewState["CommandName"].ToString() == "Update")
            {
                SqlClass.Update(ProClass);
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg( MainName1En + " updated successfully", "تم تعديل " + MainName2Ar + " بنجاح"));
            }

            if (ViewState["CommandName"].ToString() == "Delete")
            {
                string Q = " SELECT NatID FROM EmployeeMaster WHERE NatID = " + ddlPkID.SelectedValue + " ";
                         
                dt = DBFun.FetchData(Q);
                if (!DBFun.IsNullOrEmpty(dt)) 
                { 
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Error, General.Msg("Deletion can not because of the presence of related records", "لا يمكن الحذف بسبب وجود سجلات مرتبطة")); 
                }
                else
                {
                    SqlClass.Delete(ProClass);
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainName1En + " deleted successfully", "تم حذف " + MainName2Ar + " بنجاح"));
                }
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
        string sqlUpdate = ViewState["CommandName"].ToString() == "Update" ? " AND NatID != " + ddlPkID.SelectedValue : "";

        try
        {
            if (ViewState["CommandName"].ToString() == "Save" || ViewState["CommandName"].ToString() == "Update")
            {
                if (source.Equals(cvNameAr))
                {
                    if (string.IsNullOrEmpty(txtNameAr.Text))
                    {
                        MessageFun.ValidMsg(this, ref cvNameAr, false, General.Msg(MainName2En + " Name (Ar) is required", "اسم " + MainName3Ar + " (ع) مطلوب"));
                        e.IsValid = false;
                        return;
                    }
                    else
                    {
                        dt = DBFun.FetchData(MainQuery + " WHERE NatNameAr = '" + txtNameAr.Text + "' " + sqlUpdate);
                        if (!DBFun.IsNullOrEmpty(dt))
                        {
                            MessageFun.ValidMsg(this, ref cvNameAr, true, General.Msg(MainName2En + " name (Ar) already exists", "اسم " + MainName3Ar + " (ع) موجود مسبقا"));
                            e.IsValid = false;
                            return;
                        }
                    }
                }

                if (source.Equals(cvNameEn))
                {
                    if (string.IsNullOrEmpty(txtNameEn.Text))
                    {
                        MessageFun.ValidMsg(this, ref cvNameEn, false, General.Msg(MainName2En + " Name (En) is required", "اسم " + MainName3Ar + " (E) مطلوب"));
                        e.IsValid = false;
                        return;
                    }
                    else
                    {
                        dt = DBFun.FetchData(MainQuery + " WHERE NatNameEn = '" + txtNameEn.Text + "' " + sqlUpdate);
                        if (!DBFun.IsNullOrEmpty(dt))
                        {
                            MessageFun.ValidMsg(this, ref cvNameEn, true, General.Msg(MainName2En + " name (En) already exists", "اسم " + MainName3Ar + " (E) موجود مسبقا"));
                            e.IsValid = false;
                            return;
                        }
                    }
                }

                if (source.Equals(cvCountryNameAr))
                {
                    if (string.IsNullOrEmpty(txtCountryNameAr.Text))
                    {
                        MessageFun.ValidMsg(this, ref cvNameAr, false, General.Msg(MainName3En + " Name (Ar) is required", "اسم " + MainName4Ar + " (ع) مطلوب"));
                        e.IsValid = false;
                        return;
                    }
                    else
                    {
                        dt = DBFun.FetchData(MainQuery + " WHERE CountryNameAr = '" + txtCountryNameAr.Text + "' " + sqlUpdate);
                        if (!DBFun.IsNullOrEmpty(dt))
                        {
                            MessageFun.ValidMsg(this, ref cvNameAr, true, General.Msg(MainName3En + " name (Ar) already exists", "اسم " + MainName4Ar + " (ع) موجود مسبقا"));
                            e.IsValid = false;
                            return;
                        }
                    }
                }

                if (source.Equals(cvCountryNameEn))
                {
                    if (string.IsNullOrEmpty(txtCountryNameEn.Text))
                    {
                        MessageFun.ValidMsg(this, ref cvNameEn, false, General.Msg(MainName3En + " Name (En) is required", "اسم " + MainName4Ar + " (E) مطلوب"));
                        e.IsValid = false;
                        return;
                    }
                    else
                    {
                        dt = DBFun.FetchData(MainQuery + " WHERE CountryNameEn = '" + txtCountryNameEn.Text + "' " + sqlUpdate);
                        if (!DBFun.IsNullOrEmpty(dt))
                        {
                            MessageFun.ValidMsg(this, ref cvNameEn, true, General.Msg(MainName3En + " name (En) already exists", "اسم " + MainName4Ar + " (E) موجود مسبقا"));
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

