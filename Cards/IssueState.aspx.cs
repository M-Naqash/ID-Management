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
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;

public partial class IssueState : BasePage
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DataTable dt;
    IssuesPro isPro = new IssuesPro();
    IssuesSql isSql = new IssuesSql();
    string gvUniqueID = String.Empty;
    DataTable Issuedt = new DataTable();
    string CardType = string.Empty;

    string MainPer = "Issues";
    string MainName1Ar = "إصدار";
    string MainName2Ar = "الإصدار";
    string MainNameEn = "Issue";
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //   --------------------Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Card", pageDiv);

            CardType = "Card";

            //string Type = (Request.QueryString["ac"] != null) ? Request.QueryString["ac"] : "";
            //if (Type == "ic" || Type == "uc") { FormSession.FillSession("Card"); }
            //if (Type == "il" || Type == "ul") { FormSession.FillSession("Lic"); }
            
            //if (FormSession.Language == "Ar") { pageDiv.Attributes.Add("dir", "rtl"); } else { pageDiv.Attributes.Add("dir", "ltr"); }
            //PageName = new System.IO.FileInfo(Request.Url.AbsolutePath).Name;

            //if (Request.QueryString["ac"] != null)
            //{
            //    string s = Request.QueryString["ac"].ToString();
            //    if (s == "ic" || s == "uc") { CardType = "Card"; }
            //    if (s == "il" || s == "ul") { CardType = "LCard"; }
            // }

            
            //   --------------------Common Code ----------------------------------------------------------------- //
            if (Request.QueryString.Count > 0)
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["ac"] != null)
                    {
                        if (Request.QueryString["ac"].ToString() == "ic")
                        {
                            CardsSideMenu1.Visible = true;
                            if (!FormSession.PermUsr.Contains("IIsCrd")) { Response.Redirect(@"~/Login.aspx"); }
                            btnSave.Enabled = FormSession.PermUsr.Contains("IIsCrd");

                            btnSave.Text = General.Msg("Save","حفظ");
                            MainMasterPage.ShowTitel(General.Msg("Add Issue", "إضافة إصدار"));
                            divUpdDel.Visible = false;
                            FillGrid();
                        }

                        if (Request.QueryString["ac"].ToString() == "uc")
                        {
                            CardsSideMenu1.Visible = true;
                            if (!FormSession.PermUsr.Contains("UIsCrd")) { Response.Redirect(@"~/Login.aspx"); }
                            btnSave.Enabled = FormSession.PermUsr.Contains("UIsCrd");
                            btnSave.Text = General.Msg("Update","تعديل");
                            MainMasterPage.ShowTitel(General.Msg("Update Issue", "تعديل إصدار"));
                            divUpdDel.Visible = true;
                            FillGrid();
                            Fillddl();
                        }
                        if (Request.QueryString["ac"].ToString() == "dc")
                        {
                            CardsSideMenu1.Visible = true;
                            if (!FormSession.PermUsr.Contains("IsCrdDel")) { Response.Redirect(@"~/Login.aspx"); }
                            btnSave.Enabled = FormSession.PermUsr.Contains("IsCrdDel");
                            btnSave.Text = General.Msg("Delete","حذف");
                            MainMasterPage.ShowTitel(General.Msg("Delete Issue", "حذف إصدار"));
                            divUpdDel.Visible = true;
                            FillGrid();
                            Fillddl();
                        }
                    }
                }
            }
        }
        catch (Exception e1)
        { }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Fillddl()
    {
        rfvddlIssue.Enabled = true;
        ddlIssue.Items.Clear();
        dt = DBFun.FetchData("SELECT * FROM IssueState WHERE IsType = '" + CardType + "'");
        if (!DBFun.IsNullOrEmpty(dt))
        {
            FormCtrl.PopulateDDL(ddlIssue, dt, "IsName" + General.Lang(), "IsID", General.Msg("-Select Issue-","-اختر الإصدار-")); 
            rfvddlIssue.InitialValue = ddlIssue.Items[0].Text; 
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnAddIssue_Click(object sender, EventArgs e)
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

        cblConditions.Items.Add(txtIssueCondition.Text);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnDeleteIssue_Click(object sender, EventArgs e)
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

        //cblConditions.Items.Remove(cblConditions.SelectedValue);

        for (int i = cblConditions.Items.Count -1; i >= 0 ; i--) { if (cblConditions.Items[i].Selected) { cblConditions.Items.RemoveAt(i); } }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillIssueObject()
    {
        isPro.IsID            = ddlIssue.SelectedValue;
        isPro.IsType          = CardType;
        isPro.IsNameEn        = txtIssueNameEn.Text;
        isPro.IsNameAr        = txtIssueNameEn.Text;
        isPro.IsDescription   = txtIssuedescription.Text;
        isPro.TransactionBy   = FormSession.LoginUsr;
        isPro.TransactionDate = DateTime.Now.ToShortDateString();

        if (rdlIsRepeat.SelectedIndex > -1) { isPro.IsRepeat = rdlIsRepeat.SelectedValue; }
        if (rdlIsCondition.SelectedIndex > -1) { isPro.ISCondition = rdlIsCondition.SelectedValue; }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
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

            if ((btnSave.Text == "Save") || (btnSave.Text == "حفظ"))
            {

                FillIssueObject();
                int returnValue = isSql.Insert(isPro);
                if (returnValue > 0)
                {
                    for (int i = 0; i < cblConditions.Items.Count; i++)
                    {
                        isPro.IsID = returnValue.ToString();
                        isPro.ConditionName = cblConditions.Items[i].Text;
                        isSql.InsertCondition(isPro);
                    }
                    ClearUI();
                    FillGrid();
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " data added successfully", "تم إضافة بيانات " + MainName2Ar + " بنجاح"));
                }
            }

            if ((btnSave.Text == "Update") || (btnSave.Text == "تعديل"))
            {
                FillIssueObject();
                isSql.Update(isPro);

                isSql.DeleteAllCondition(ddlIssue.SelectedValue, FormSession.LoginUsr);
                for (int i = 0; i < cblConditions.Items.Count; i++)
                {
                    isPro.ConditionName = cblConditions.Items[i].Text;
                    isSql.InsertCondition(isPro);
                }
                ClearUI();
                FillGrid();
                ddlIssue.ClearSelection();
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " data updated successfully", "تم تعديل بيانات " + MainName2Ar + " بنجاح"));
            }

            if ((btnSave.Text == "Delete") || (btnSave.Text == "حذف"))
            {
                isSql.DeleteAllCondition(ddlIssue.SelectedValue, FormSession.LoginUsr);
                isSql.Delete(ddlIssue.SelectedValue, FormSession.LoginUsr);
                ClearUI();
                FillGrid();
                Fillddl();
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " Deleted successfully", "تم حذف " + MainName2Ar + " بنجاح"));
            }
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "btnSave");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillGrid()
    {
        dt = DBFun.FetchData("SELECT * FROM IssueState WHERE IsType = '" + CardType + "'");
        if (!DBFun.IsNullOrEmpty(dt))
        {
            grdData.DataSource = (DataTable)dt;
            grdData.DataBind();
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlIssue_SelectedIndexChanged(object sender, EventArgs e)
    {  
        ClearUI();
        PopulateUI(ddlIssue.SelectedValue);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI(string pID)
    {
        try
        {
            dt = DBFun.FetchData("SELECT * FROM IssueState WHERE IsID = " + pID + "");
            if (DBFun.IsNullOrEmpty(dt)) { return; }

            DataRow dr = (DataRow)dt.Rows[0];

            txtIssueNameEn.Text = dt.Rows[0]["IsNameEn"].ToString();
            txtIssueNameAr.Text = dt.Rows[0]["IsNameAr"].ToString();
            txtIssuedescription.Text = dt.Rows[0]["IsDescription"].ToString();
            rdlIsRepeat.SelectedIndex = rdlIsRepeat.Items.IndexOf(rdlIsRepeat.Items.FindByValue(dt.Rows[0]["IsRepeat"].ToString()));
            rdlIsCondition.SelectedIndex = rdlIsCondition.Items.IndexOf(rdlIsCondition.Items.FindByValue(dt.Rows[0]["ISCondition"].ToString()));
            rdlIsCondition_SelectedIndexChanged(null,null);
            DataTable Condt = DBFun.FetchData("SELECT * FROM IssueConditions WHERE IsID = " + pID + "");
            if (DBFun.IsNullOrEmpty(Condt)) { return; }
            for (int i = 0; i < Condt.Rows.Count; i++) { cblConditions.Items.Add(Condt.Rows[i]["ConditionName"].ToString()); }
        }
        catch (Exception Ex)
        {
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ClearUI()
    {
        txtIssueNameEn.Text = "";
        txtIssueNameAr.Text = "";
        txtIssuedescription.Text = "";
        rdlIsRepeat.SelectedIndex = -1;
        rdlIsCondition.SelectedIndex = -1;
        txtIssueCondition.Text = "";
        cblConditions.Items.Clear();
        rdlIsCondition_SelectedIndexChanged(null,null);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e) { ClearUI(); }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    {
                        e.Row.Attributes.Add("onmouseover", "mouseout('alt_row_highlight',this);");
                        e.Row.Attributes.Add("onmouseout", "mouseover('alt_row_nohighlight',this);");
                        e.Row.Attributes.Add("onmousemove", "mousemove('alt_row_nohighlight',this);");
                        break;
                    }
            }
        }
        catch (Exception Ex)
        {
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*####################################################################################################################################*/
    /*####################################################################################################################################*/
    #region Custom Validate Events
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ConditionsValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvcblConditions))
            {
                if (rdlIsCondition.SelectedIndex == 0 && cblConditions.Items.Count == 0) { e.IsValid = false; } else { e.IsValid = true; }
            }
            else if (source.Equals(cvDelConditions))
            {
                if (cblConditions.SelectedIndex < 0) { e.IsValid = false; } else { e.IsValid = true; }
            }
        }
        catch { e.IsValid = false; }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #endregion
    /*####################################################################################################################################*/
    /*####################################################################################################################################*/
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void rdlIsCondition_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdlIsCondition.SelectedIndex == 0) { spnConditions.Visible = true; rfvtxtIssueCondition.Enabled = true; }
        else { spnConditions.Visible = false; rfvtxtIssueCondition.Enabled = false; }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrdDisplayBool(object pBool)
    {
        switch (pBool.ToString())
        {
            case "0": return General.Msg("False", "لا");
            case "1": return General.Msg("True", "نعم");
            default: return "";
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
