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
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class Stickers_Stickers : BasePage
{
    StickersPro ProClass = new StickersPro();
    StickersSql SqlClass = new StickersSql();
    DataTable dt;

    string MainPer = "Stickers";
    string MainName1Ar = "ملصق";
    string MainName2Ar = "الملصق";
    string MainNameEn = "Sticker";

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //---Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Card", pageDiv);
            //---Common Code ----------------------------------------------------------------- //

            if (!IsPostBack)
            {
                if (!FormSession.getPerm("IStick")) { Response.Redirect(@"~/Login.aspx"); }
                btnSave.Enabled = FormSession.getPerm("IStick");
                MainMasterPage.ShowTitel(General.Msg("Add Sticker", "إصدار ملصق"));
                ButtonAction("100");
                Fillddl();
            }
        }
        catch (Exception e1) { DBFun.InsertError(FormSession.PageName, "PageLoad"); }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void Fillddl()
    {
        dt = DBFun.FetchData("SELECT * FROM CardTemplate WHERE TmpType = 'Stick'");
        if (!DBFun.IsNullOrEmpty(dt)) { FormCtrl.PopulateDDL(ddlTemplate, dt, "TmpName", "TmpID", General.Msg("-Select Template-", "-اختر النموذج-")); }
        
        DataTable CompDT = DBFun.FetchData("SELECT * FROM Companies ");
        if (!DBFun.IsNullOrEmpty(CompDT)) { FormCtrl.PopulateDDL(ddlCompID, CompDT, "CompName" + FormSession.Language, "CompID", General.Msg("-Select Company-", "-اختر الشركة-")); }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void btnIDSearch_Click(object sender, EventArgs e)
    {
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
        FillGrid(txtIDSearch.Text);
        ButtonAction("011");
        PopulateUI(txtIDSearch.Text);
        DataItemEnabled(true);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void ButtonAction(String pBtn) //string pBtn = [Fetch,Save,Cancel]
    {
        string action = "IStick";

        btnIDSearch.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[0].ToString()));
        btnSave.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[1].ToString()));
        btnCancel.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[2].ToString()));
        if (pBtn[0] != '0') { btnIDSearch.Enabled = FormSession.getPerm(action); }
        if (pBtn[1] != '0') { btnSave.Enabled = FormSession.getPerm(action); }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

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
                ProClass.Printed = false;
                ProClass.Status = 1;

                FillObject();
                SqlClass.Insert(ProClass);

                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg("Sticker details added successfully", "تم اضافة بيانات الملصق بنجاح"));
            }

            //if ((btnSave.Text == "Update") || (btnSave.Text == "تعديل"))
            //{
            //    ProClass.StickerID = ;
            //    ProClass.ModifiedBy = userName;
            //    ProClass.ModifiedDate = DateTime.Now.ToShortDateString();
            //    ProClass.Printed = false;
            //    ProClass.Status = true;
            //    ProClass.ExceptionReq = Convert.ToBoolean(ViewState["checkStick"]);

            //    FillObject();
            //    SqlClass.Update(ProClass);

            //    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg("Sticker details updated successfully", "تم تعديل بيانات الملصق بنجاح"));
            //    ViewState["checkStick"] = "False";
            //}

            FillGrid(txtIDSearch.Text.Trim());
            ClearUI();
        }
        catch (Exception EX)
        {
            DBFun.InsertError(FormSession.PageName, "btnSave");
            MessageFun.ShowAdminMsg(this, EX.Message);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public void FillGrid(string pRegVeh)
    {
        dt = DBFun.FetchData("SELECT StickerID,RegVehicle,EmpID,StartDate,ExpiryDate,TmpName AS StkTmpName "
                            + " , EmpName"  + General.Lang() + " AS FullName "
                            + " , CompName" + General.Lang() + " AS Company " 
                            + " FROM StickerInfoView where RegVehicle ='" + pRegVeh + "' ORDER BY StickerID DESC");
        if (!DBFun.IsNullOrEmpty(dt))
        {
            DataRow dr1 = (DataRow)dt.Rows[0];
            grdData.DataSource = (DataTable)dt;
            grdData.DataBind();

            //hdnStickerID.Value = dr1["StickerID"].ToString();
        }
        else
        {
            FormCtrl.FillGridEmpty(ref grdData, 20, "This Vehicle does not has Stickers", "هذه المركبة ليس لها ملصقات");

            dt = new DataTable();
            grdData.DataSource = (DataTable)dt;
            grdData.DataBind();

            ClearUI();
        }

        SetTmp("Stk");
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public void PopulateUI(string searchPar)
    {
        try
        {
            dt = DBFun.FetchData("SELECT * FROM StickerInfoView where RegVehicle = '" + searchPar + "' and Status = 1 ");

            if (DBFun.IsNullOrEmpty(dt)) { txtRegVehicle.Text = searchPar.ToString(); return; }

            DataRow dr = (DataRow)dt.Rows[0];

            txtRegVehicle.Text = searchPar.ToString();
            txtEmpid.Text = dr["EmpID"].ToString();
            
            txtOwner.Text = dr["Owner"].ToString();
            txtCarType.Text = dr["CarType"].ToString();
            txtModel.Text = dr["Model"].ToString();
            txtColor.Text = dr["Color"].ToString();

            //ddlTemplate.SelectedIndex = ddlTemplate.Items.IndexOf(ddlTemplate.Items.FindByValue(dr["EmpType"].ToString()));
            

            //if (ViewState["CommandName"].ToString() == "Update")
            //{
            //    calEnddate.setDBDate(dr["ExpiryDate"], "S");
            //    calStartdate.setDBDate(dr["StartDate"], "S");

            //    if (dr["ReturnBackDate"] != DBNull.Value)
            //    {
            //        divLicRtnDate1.Visible = true;
            //        divLicRtnDate2.Visible = true;
            //        calReturnDate.setDBDate(dr["ReturnBackDate"], "S");
            //    }
            //    else { divLicRtnDate1.Visible = false; divLicRtnDate2.Visible = false; }

            //    if (Convert.ToString(dr["Category"]) == "Contractor Car") { divLicRtnDate1.Visible = true; divLicRtnDate2.Visible = true; }

            //    if (dr["Printed"].ToString() == "1") { btnSave.Enabled = false; }
            //    else { btnSave.Enabled = true; }
            //}
        }
        catch (Exception Ex) { DBFun.InsertError(FormSession.PageName, "PopulateUI"); }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public void DataItemEnabled(bool Status)
    {
        txtEmpid.Enabled = Status;
        txtRegVehicle.Enabled = Status;
        txtCarType.Enabled = Status;
        txtColor.Enabled = Status;
        txtModel.Enabled = Status;
        txtOwner.Enabled = Status;
        ddlCompID.Enabled = Status;

        calStartdate.setEnabled(Status);
        calEnddate.setEnabled(Status);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearUI();
        DataItemEnabled(false);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void FillObject()
    {
        try
        {
            ProClass.DateType        = FormSession.DateType;
            
            ProClass.EmpID           = txtEmpid.Text;
            ProClass.RegVehicle      = txtRegVehicle.Text;
            ProClass.Owner           = txtOwner.Text;
            ProClass.CarType         = txtCarType.Text;
            ProClass.Model           = txtModel.Text;
            ProClass.Color           = txtColor.Text;
            ProClass.StartDate       = calStartdate.getDate();
            ProClass.ExpiryDate      = calEnddate.getDate();
         
            ProClass.TransactionBy   = FormSession.LoginUsr;
            ProClass.TransactionDate = DateTime.Now.ToShortDateString();

            if (ddlCompID.SelectedIndex > 0) { ProClass.CompID = Convert.ToInt32(ddlCompID.SelectedItem.Value); }
            if (ddlTemplate.SelectedIndex > 0) { ProClass.TemplateID = Convert.ToInt32(ddlTemplate.SelectedItem.Value); }
        }
        catch (Exception EX)
        {
            DBFun.InsertError(FormSession.PageName, "FillObject()");
            MessageFun.ShowAdminMsg(this, EX.Message);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public void ClearUI()
    {
        txtEmpid.Text       = "";
        txtRegVehicle.Text  = "";
        txtCarType.Text     = "";
        txtModel.Text       = "";
        txtColor.Text       = "";
        txtOwner.Text       = "";

        ddlCompID.SelectedIndex = -1;
        ddlTemplate.SelectedIndex = -1;

        calStartdate.ClearDate();
        calEnddate.ClearDate();
        
        ButtonAction("100");
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
        catch (Exception e1)
        {
            DBFun.InsertError(FormSession.PageName, "grdCardprint_RowCreated");
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        FillGrid(txtIDSearch.Text);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public void SetTmp(string EmpType)
    {
        if (!string.IsNullOrEmpty(EmpType))
        {
            DataTable Tmpdt = DBFun.FetchData("SELECT TmpID FROM CardTemplate WHERE TmpType ='Stick' AND EmpType = '" + EmpType + "'");
            if (!DBFun.IsNullOrEmpty(Tmpdt)) { ddlTemplate.SelectedIndex = ddlTemplate.Items.IndexOf(ddlTemplate.Items.FindByValue(Tmpdt.Rows[0]["TmpID"].ToString())); }
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

    #region Custom Validate Events
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void IDSearch_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvIDSearch))
            {
                if (string.IsNullOrEmpty(txtIDSearch.Text))
                {
                    General.ValidMsg(this, ref cvIDSearch, false, "Registered Vehicle is required", "رقم السيارة مطلوب");
                    e.IsValid = false;
                    return;
                }
            }
        }
        catch { e.IsValid = false; }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void EmpValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvEmpID))
            {
                dt = DBFun.FetchData("select EmpID from EmployeeInfoView where EmpID = '" + txtEmpid.Text.Trim() + "' ");
                if (DBFun.IsNullOrEmpty(dt))
                {
                    General.ValidMsg(this, ref cvEmpID, true, "Employee ID entered not exists, Please enter different ID", "رقم الموظف غير موجود, من فضلك اختر رقما آخر");
                    e.IsValid = false;
                    return;
                }
            }
        }
        catch { e.IsValid = false; }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void DateValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvcalStartDate))
            {
                if (string.IsNullOrEmpty(calStartdate.getDate()))
                {
                    General.ValidMsg(this, ref cvcalStartDate, false, "Start Date is required", "تاريخ البداية مطلوب");
                    e.IsValid = false;
                    return;
                }

                if (!string.IsNullOrEmpty(calStartdate.getDate()) && !String.IsNullOrEmpty(calEnddate.getDate()))
                {
                    int iStartDate = DateFun.ConvertDateTimeToInt("Gregorian", calStartdate.getDate());
                    int iEndDate = DateFun.ConvertDateTimeToInt("Gregorian", calEnddate.getDate());
                    if (iStartDate > iEndDate)
                    {
                        General.ValidMsg(this, ref cvcalStartDate, true, "start date more than end date!", "تاريخ الإصدار أكبر من تاريخ الإنتهاء");
                        e.IsValid = false;
                        return;
                    }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

    #endregion
}