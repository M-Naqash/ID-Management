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

public partial class Visitors_Visitors : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    VisitorsPro ProClass = new VisitorsPro();
    VisitorsSql SqlClass = new VisitorsSql();
    DataTable dt;

    string MainName1Ar = "مناسبة";
    string MainName2Ar = "المناسبة";
    string MainNameEn  = "Event";

    string MainQuery = " SELECT * FROM VisitorsCard ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //---Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Visitors",pageDiv);
            //---Common Code ----------------------------------------------------------------- //

            if (!IsPostBack)
            {
                pnlMain.Attributes.Add("onkeypress", "javascript:return DefaultButton(event,'" + btnIDSearch.ClientID + "');");

                if (Request.QueryString["ID"] == null) { Response.Redirect(@"~/Login.aspx"); }
                ViewState["VisIdentityNo"] = "";
                ViewState["Action"] = "";
                ButtonAction("00",true);
                //ddlTmpID.Enabled = false;
                //VisImage.EnabledImage(false);
                
                if (Request.QueryString["ID"].ToString() == "i")
                {
                    MainMasterPage.ShowTitel(General.Msg("Add " + MainNameEn + " Card", "إضافة بطاقة " + MainName1Ar));
                    if (!FormSession.PermUsr.Contains("IVis")) { Response.Redirect(@"~/Login.aspx"); }
                    ViewState["Action"] = "A";
                    
                    btnSave.Enabled = btnCancel.Enabled = false;
                    Fillddl();
                }
                
                if (Request.QueryString["ID"].ToString() == "u")
                {
                    MainMasterPage.ShowTitel(General.Msg("Update " + MainNameEn + " Card", "تعديل بطاقة " + MainName1Ar));
                    if (!FormSession.PermUsr.Contains("UVis")) { Response.Redirect(@"~/Login.aspx"); }
                    ViewState["Action"] = "U";
                    
                    btnSave.Enabled = btnCancel.Enabled = false;
                    Fillddl();
                }

                ddlTmpID.Enabled = false;
            }

            if (IsPostBack) { VisImage.PopulateImage(txtVisIdentityNo.Text); }
        }
        catch (Exception ex) { DBFun.InsertError(FormSession.PageName, "PageLoad"); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Fillddl()
    {
        DataTable TmpDT = DBFun.FetchData("SELECT * FROM CardTemplate WHERE TmpType = 'VCard'");
        if (!DBFun.IsNullOrEmpty(TmpDT)) 
        { 
            FormCtrl.PopulateDDL(ddlTmpID, TmpDT, "TmpName", "TmpID", General.Msg("-Select Template-","-اختر النموذج-")); 
            rvTmpID.InitialValue = ddlTmpID.Items[0].Value; 
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ButtonAction(String pBtn,bool search) //string pBtn = [Save,Cancel]
    {
        btnSave.Enabled   = Convert.ToBoolean(Convert.ToInt32(pBtn[0].ToString()));
        btnCancel.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[1].ToString()));
        
        if (pBtn[0] != '0' && ViewState["Action"].ToString() == "A") { btnSave.Enabled = FormSession.PermUsr.Contains("IVis"); }
        if (pBtn[0] != '0' && ViewState["Action"].ToString() == "U") { btnSave.Enabled = FormSession.PermUsr.Contains("UVis"); }
        
        btnIDSearch.Enabled = search;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProClass.DateType = FormSession.DateType;
            
            ProClass.VisCardID     = txtVisCardID.Text;
            ProClass.VisIdentityNo = txtVisIdentityNo.Text;
            ProClass.VisNameAr     = txtVisNameAr.Text;
            if (!string.IsNullOrEmpty(txtVisNameEn.Text)) { ProClass.VisNameEn = txtVisNameEn.Text; }
            if (!string.IsNullOrEmpty(txtVisMobileNo.Text)) { ProClass.VisMobileNo = txtVisMobileNo.Text; }
            
            ProClass.StartDate  = calStartDate.getDate();
            ProClass.ExpiryDate = calExpiryDate.getDate();

            ProClass.VisRegion1 = chkbRegion.Items.FindByValue("Region1").Selected;
            ProClass.VisRegion2 = chkbRegion.Items.FindByValue("Region2").Selected;
            ProClass.VisRegion3 = chkbRegion.Items.FindByValue("Region3").Selected;
            ProClass.VisRegion4 = chkbRegion.Items.FindByValue("Region4").Selected;
            ProClass.VisRegion5 = chkbRegion.Items.FindByValue("Region5").Selected;
            ProClass.VisRegion6 = chkbRegion.Items.FindByValue("Region6").Selected;
            ProClass.VisRegion7 = chkbRegion.Items.FindByValue("Region7").Selected;
            ProClass.VisRegion8 = chkbRegion.Items.FindByValue("Region8").Selected;
            ProClass.VisRegion9 = chkbRegion.Items.FindByValue("Region9").Selected;

            if (!string.IsNullOrEmpty(txtDescription.Text)) { ProClass.Description = txtDescription.Text; }
            
            if (ddlTmpID.SelectedIndex > 0) { ProClass.TmpID = ddlTmpID.SelectedValue; }

            ProClass.CopiesCount = (Convert.ToInt16(FindCount(txtVisIdentityNo.Text)) + 1).ToString();
            ProClass.CardStatus = "0";
            ProClass.isPrinted  = false;
            ProClass.TransactionBy   = FormSession.LoginUsr;
            
            ////////
            Byte[]  pImage = new Byte[0];
            string  pImageContentType = "";
            int     pImageLength = 0;
            VisImage.GetImage( out pImage , out pImageContentType, out pImageLength);

            if (pImageLength != 0) { ProClass.VisImage = CryptoImage.EncryptBytes(pImage); } else { ProClass.VisImage = pImage; }
            ProClass.VisImageContentType = pImageContentType;
            ProClass.VisImageLength = pImageLength;
            ////////
                
        }
        catch (Exception ex) { DBFun.InsertError(FormSession.PageName, "FillPropeties"); }
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
           
        string VisIdentityNo = ViewState["VisIdentityNo"].ToString();
        PopulateUI(VisIdentityNo);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void PopulateUI(string pID)
    {
        try
        {
            string Action = ViewState["Action"].ToString();

            if (string.IsNullOrEmpty(pID))
            {
                lblMsg.Text = General.Msg("Visitor number does not exist", "رقم الزائر غير موجود سابقا");
                divCardCount.Visible = true;

                if (Action == "A")
                { 
                    if      (ddlSearchBy.SelectedValue == "VisIdentityNo") { txtVisIdentityNo.Text = txtSearchBy.Text; txtVisIdentityNo.Enabled = false; }
                    else if (ddlSearchBy.SelectedValue == "VisNameAr")     { txtVisNameAr.Text     = txtSearchBy.Text; txtVisIdentityNo.Enabled = true;  }
                    else if (ddlSearchBy.SelectedValue == "VisNameEn")     { txtVisNameEn.Text     = txtSearchBy.Text; txtVisIdentityNo.Enabled = true;  }
                    else if (ddlSearchBy.SelectedValue == "VisMobileNo")   { txtVisMobileNo.Text   = txtSearchBy.Text; txtVisIdentityNo.Enabled = true;  }

                    ddlTmpID.SelectedIndex = 1;
                    ButtonAction("11", false);
                }
                
                return;
            }
            else
            {
                DataTable CDT = DBFun.FetchData("SELECT TOP 1 * FROM VisitorsCard WHERE VisIdentityNo = '" + pID + "' ORDER BY VisCardID DESC");
                if (!DBFun.IsNullOrEmpty(CDT))
                {
                    if (Action == "A" || Action == "U")
                    {
                        txtVisIdentityNo.Text = CDT.Rows[0]["VisIdentityNo"].ToString();
                        txtVisNameAr.Text     = CDT.Rows[0]["VisNameAr"].ToString();
                        txtVisNameEn.Text     = CDT.Rows[0]["VisNameEn"].ToString();
                        txtVisMobileNo.Text   = CDT.Rows[0]["VisMobileNo"].ToString();

                        if ((CDT.Rows[0]["VisImage"] == DBNull.Value) || (CDT.Rows[0]["VisImageLength"].ToString() == "0")) { VisImage.ClearImage(); } else { VisImage.setImage(CDT.Rows[0]["VisIdentityNo"].ToString()); }
                    }

                    if (Action == "U")
                    {
                        txtVisCardID.Text = CDT.Rows[0]["VisCardID"].ToString();
                        txtDescription.Text = CDT.Rows[0]["Description"].ToString();

                        if (CDT.Rows[0]["StartDate"] != DBNull.Value) { calStartDate.setDBDate(CDT.Rows[0]["StartDate"], "S"); }
                        if (CDT.Rows[0]["ExpiryDate"] != DBNull.Value) { calExpiryDate.setDBDate(CDT.Rows[0]["ExpiryDate"], "S"); }

                        chkbRegion.Items.FindByValue("Region1").Selected = Convert.ToBoolean(CDT.Rows[0]["VisRegion1"]);
                        chkbRegion.Items.FindByValue("Region2").Selected = Convert.ToBoolean(CDT.Rows[0]["VisRegion2"]);
                        chkbRegion.Items.FindByValue("Region3").Selected = Convert.ToBoolean(CDT.Rows[0]["VisRegion3"]);
                        chkbRegion.Items.FindByValue("Region4").Selected = Convert.ToBoolean(CDT.Rows[0]["VisRegion4"]);
                        chkbRegion.Items.FindByValue("Region5").Selected = Convert.ToBoolean(CDT.Rows[0]["VisRegion5"]);
                        chkbRegion.Items.FindByValue("Region6").Selected = Convert.ToBoolean(CDT.Rows[0]["VisRegion6"]);
                        chkbRegion.Items.FindByValue("Region7").Selected = Convert.ToBoolean(CDT.Rows[0]["VisRegion7"]);
                        chkbRegion.Items.FindByValue("Region8").Selected = Convert.ToBoolean(CDT.Rows[0]["VisRegion8"]);
                        chkbRegion.Items.FindByValue("Region9").Selected = Convert.ToBoolean(CDT.Rows[0]["VisRegion9"]);
                    }

                    ddlTmpID.SelectedIndex = 1;
                    //FindCount(pID);

                    if (Action == "A")
                    {
                        bool active = FindActiveCard(txtVisIdentityNo.Text);
                        if (active)
                        {
                            lblMsg.Text = General.Msg("This visitor has a valid card", "هذا الزائر لديه بطاقة سارية المفعول");
                            divCardCount.Visible = true;
                        }
                        ButtonAction("11", false);
                    }
                    else if (Action == "U" && CDT.Rows[0]["isPrinted"].ToString() == "False")
                    {
                        bool active = FindActiveCard(txtVisIdentityNo.Text);
                        if (active)
                        {
                            lblMsg.Text = General.Msg("This visitor has a valid card", "هذا الزائر لديه بطاقة سارية المفعول");
                            divCardCount.Visible = true;
                        }
                        ButtonAction("11", false);
                    }
                    else if (Action == "U" && CDT.Rows[0]["isPrinted"].ToString() == "True")
                    {
                        lblMsg.Text = General.Msg("This card has printed, can not be modified", "هذه البطاقة تمت طباعتها, لا يمكن تعديلها");
                        divCardCount.Visible = true;
                        ButtonAction("00", true);
                    }
                }
            }
        }
        catch (Exception ex) { DBFun.InsertError(FormSession.PageName, "PopulateUI"); }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchBy.Text = "";

        if      (ddlSearchBy.SelectedValue == "VisIdentityNo") { auSearchBy.ServiceMethod = "GetVisIDList"; }
        else if (ddlSearchBy.SelectedValue == "VisNameAr")     { auSearchBy.ServiceMethod = "GetVisNameArList"; }
        else if (ddlSearchBy.SelectedValue == "VisNameEn")     { auSearchBy.ServiceMethod = "GetVisNameEnList"; }
        else if (ddlSearchBy.SelectedValue == "VisMobileNo")   { auSearchBy.ServiceMethod = "GetVisMobileNoList"; }

        txtSearchBy.Focus();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

            FillPropeties();
            
            if (ViewState["Action"].ToString() == "A")
            {
                SqlClass.Insert(ProClass);                
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " data added successfully", "تمت إضافة بيانات " + MainName2Ar + " بنجاح"));
            }
            
            if (ViewState["Action"].ToString() == "U")
            {
                SqlClass.Update(ProClass);
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " data updated successfully", "تم تعديل بيانات " + MainName2Ar + " بنجاح"));
            }

            ClearUI();
            ButtonAction("00",true); 
            //VisImage.EnabledImage(false);
        }
        catch (Exception ex)
        {
            DBFun.InsertError(FormSession.PageName, "btnSave");
            VisImage.PopulateImage(txtVisIdentityNo.Text);
            MessageFun.ShowAdminMsg(this, ex.Message);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e) 
    { 
        ClearUI(); 
        ButtonAction("00",true); 
        //VisImage.EnabledImage(false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ClearUI()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        int returnValue = DBFun.ExecuteData("DELETE FROM TempImage WHERE Type = 'Visitors' AND EmpID='" + txtVisIdentityNo.Text + "'");

        txtVisCardID.Text     = "";
        txtVisIdentityNo.Text = "";
        txtVisNameAr.Text     = ""; 
        txtVisNameEn.Text     = ""; 
        txtVisMobileNo.Text     = "";
        calStartDate.ClearDate();
        calExpiryDate.ClearDate();

        for (int i = 1; i < 10; i++) { chkbRegion.Items.FindByValue("Region" + i.ToString()).Selected = false; }

        txtDescription.Text     = ""; 
        ddlTmpID.SelectedIndex  = -1;
        VisImage.ClearImage();
        divCardCount.Visible = false;
        txtVisIdentityNo.Enabled = false;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////     
    public Int64 DateToInt(string pDate)
    {
        if (!string.IsNullOrEmpty(pDate))
        {
            string d = pDate.Substring(0, 2);
            string m = pDate.Substring(3, 2);
            string y = pDate.Substring(6, 4);
            return Convert.ToInt64(y + m + d);
        }

        return 0;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string FindCount(string pID)
    {
        try
        {
            string count = "0";

            DataTable CountDT = DBFun.FetchData("SELECT COUNT(VisCardID) count FROM VisitorsCard WHERE isPrinted = 'True' AND VisIdentityNo = '" + pID + "'");
            if (DBFun.IsNullOrEmpty(CountDT)) { }
            else
            {
                if (Convert.ToInt32(CountDT.Rows[0]["count"]) > 0) { count = CountDT.Rows[0]["count"].ToString(); }
            }

            return count;
        }
        catch (Exception e1)
        {
            DBFun.InsertError(FormSession.PageName, "FindCount");
            return "0";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool FindActiveCard(string pID)
    {
        try
        {
            bool isActive = false;

            DataTable DT = DBFun.FetchData("SELECT COUNT(VisCardID) count FROM VisitorsCard WHERE CardStatus = 2 AND ExpiryDate > GETDATE() AND VisIdentityNo = '" + pID + "'");
            if (DBFun.IsNullOrEmpty(DT)) { }
            else
            {
                if (Convert.ToInt32(DT.Rows[0]["count"]) > 0) { isActive = true; }
            }

            return isActive;
        }
        catch (Exception e1)
        {
            DBFun.InsertError(FormSession.PageName, "FindActiveCard");
            return false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool FindBlackList(string pID)
    {
        try
        {
            DataTable DT = DBFun.FetchData("SELECT BlaID FROM BlackList WHERE BlaIdentityNo = '" + pID + "'");
            if (!DBFun.IsNullOrEmpty(DT)) { return true; } else { return false; }
        }
        catch (Exception e1)
        {
            DBFun.InsertError(FormSession.PageName, "FindBlackList");
            return false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void txtVisIdentityNo_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtVisIdentityNo.Text))
        {
            bool active = FindActiveCard(txtVisIdentityNo.Text);
            if (active)
            {
                lblMsg.Text = General.Msg("This visitor has a valid card", "هذا الزائر لديه بطاقة سارية المفعول");
                divCardCount.Visible = true;
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events
    
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void SearchBy_ServerValidate(Object source, ServerValidateEventArgs e)
    {        
        try
        {
            if (source.Equals(cvSearchBy))
            {
                if (string.IsNullOrEmpty(txtSearchBy.Text))
                {
                    General.ValidMsg(this, ref cvSearchBy, false, "You must enter a value in the search box", "يجب إدخال قيمة في مربع البحث");
                    e.IsValid = false;
                    return;
                }
                else
                {
                    ViewState["VisIdentityNo"] = "";
                    DataTable VDT = DBFun.FetchData(" SELECT VisIdentityNo FROM VisitorsCard WHERE " + ddlSearchBy.SelectedValue + " = '" + txtSearchBy.Text.Trim() + "' ");
                    if (!DBFun.IsNullOrEmpty(VDT))
                    {
                        string VisIdentityNo = VDT.Rows[0]["VisIdentityNo"].ToString();
                        bool isBlakList = FindBlackList(VisIdentityNo);
                        if (isBlakList)
                        {
                            General.ValidMsg(this, ref cvSearchBy, true, "Visitor Identity No. entered is included in the blacklist and can not be issued a card", "رقم هوية الزائر المدخل موجود في القائمة السوداء لا يمكن اصدار بطاقة له");
                            e.IsValid = false;
                            txtVisIdentityNo.Text = "";
                            ButtonAction("00", true);
                            return;
                        }

                        ViewState["VisIdentityNo"] = VDT.Rows[0]["VisIdentityNo"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)  { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void DateValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvcalStartDate))
            {
                if (string.IsNullOrEmpty(calStartDate.getDate())) 
                { 
                    General.ValidMsg(this, ref cvcalStartDate,false, "Start Date is required", "تاريخ الإصدار مطلوب");
                    e.IsValid = false;
                    return;
                } 
                
                if (!string.IsNullOrEmpty(calStartDate.getDate()) && !String.IsNullOrEmpty(calExpiryDate.getDate()))
                {
                    Int64 iStartDate = calStartDate.getIntDate();
                    Int64 iEndDate     = calExpiryDate.getIntDate();
                    if (iStartDate > iEndDate) 
                    { 
                        General.ValidMsg(this, ref cvcalStartDate,true, "start date more than end date!", "تاريخ الإصدار أكبر من تاريخ الإنتهاء");
                        e.IsValid = false; 
                        return;
                    }
                }
            }
            else if (source.Equals(cvcalExpiryDate))
            {
                if (string.IsNullOrEmpty(calExpiryDate.getDate())) 
                { 
                    General.ValidMsg(this, ref cvcalExpiryDate,false, "Expiry Date is required", "تاريخ الإنتهاء مطلوب");
                    e.IsValid = false;
                    return;
                } 
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Region_ServerValidate(Object source, ServerValidateEventArgs e)
    {        
        try
        {
            if (source.Equals(cvRegion))
            {
                e.IsValid = false;
                for (int i = 1; i < 10; i++) { if (chkbRegion.Items.FindByValue("Region" + i.ToString()).Selected) { e.IsValid = true; return; } }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ID_ServerValidate(Object source, ServerValidateEventArgs e)
    {        
        try
        {
            if (source.Equals(cvVisIdentityNo))
            {
                if (string.IsNullOrEmpty(txtVisIdentityNo.Text))
                {
                    General.ValidMsg(this, ref cvVisIdentityNo, false, "Identity NO. is required", "رقم الهوية مطلوب");
                    e.IsValid = false;
                    return;
                }
                else
                {
                    bool isBlakList = FindBlackList(txtVisIdentityNo.Text);
                    if (isBlakList)
                    {
                        General.ValidMsg(this, ref cvVisIdentityNo, true, "Visitor Identity No. entered is included in the blacklist and can not be issued a card", "رقم هوية الزائر المدخل موجود في القائمة السوداء لا يمكن اصدار بطاقة له");
                        e.IsValid = false;
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
