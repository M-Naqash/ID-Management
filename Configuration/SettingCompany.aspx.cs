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
using System.IO;

public partial class SettingCompany : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ApplicationSetupPro ProClass = new ApplicationSetupPro();
    ApplicationSetupSql SqlClass = new ApplicationSetupSql();
    DataTable dt;

    string MainPer   = "Config";
    string MainQuery = " SELECT * FROM ApplicationSetup ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //---Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Config",pageDiv);
            //---Common Code ----------------------------------------------------------------- //
            MainMasterPage.ShowTitel(General.Msg("institution Setting", "إعدادات المنشأة"));
            if (!FormSession.PermUsr.Contains("U" + MainPer)) { Response.Redirect(@"~/Login.aspx"); }
            btnSave.Enabled = FormSession.PermUsr.Contains("U" + MainPer);
            btnSave.Text = General.Msg("Save","حفظ");

            if (!IsPostBack) {  PopulateUI(); }

            if (IsPostBack) { imgLogo.PopulateImage("Logo");}
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSave_Click(object sender, EventArgs e)
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

        try
        {

            FillPropeties();
            SqlClass.InsertUpdate(ProClass);
            MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg("institution Setting saved successfully", "تم حفظ إعدادات المنشأة"));
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
    protected void FillPropeties()
    {
        try
        {
            ProClass.AppCompany = txtAppCompany.Text;
            ProClass.AppDisplay = txtAppDisplay.Text;
            
            ProClass.AppAddress1 = txtAppAddress1.Text;
            ProClass.AppAddress2 = txtAppAddress2.Text;
            ProClass.AppCity     = txtAppCity.Text;
            ProClass.AppCountry  = txtAppCountry.Text;
            ProClass.AppPOBox    = txtAppPOBox.Text;
            ProClass.AppTelNo1   = txtAppTelNo1.Text;
            ProClass.AppTelNo2   = txtAppTelNo2.Text;
            ProClass.AppFax      = txtAppFax.Text;
            ProClass.AppUrl      = txtAppUrl.Text;
            ProClass.AppEmail    = txtAppEmail.Text;
            
            if (ddlAppCalendar.SelectedIndex > -1) { ProClass.AppCalendar = ddlAppCalendar.SelectedValue; }
            
            ProClass.TransactionBy = FormSession.LoginUsr;
            
            Byte[]  pImage = new Byte[0];
            string pImageContentType = "";
            int    pImageLength = 0;

            imgLogo.GetImage( out pImage , out pImageContentType, out pImageLength);

            ProClass.AppLogo = pImage;
            ProClass.AppLogoImageType   = pImageContentType;
            ProClass.AppLogoImageLength = pImageLength;

        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI()
    {
        try
        {
            dt = DBFun.FetchData(MainQuery);
            if (DBFun.IsNullOrEmpty(dt)) { return; }
            
            txtAppCompany.Text  = dt.Rows[0]["AppCompany"].ToString();
            txtAppDisplay.Text  = dt.Rows[0]["AppDisplay"].ToString();
            txtAppAddress1.Text = dt.Rows[0]["AppAddress1"].ToString();
            txtAppAddress2.Text = dt.Rows[0]["AppAddress2"].ToString();
            txtAppCity.Text     = dt.Rows[0]["AppCity"].ToString();
            txtAppCountry.Text  = dt.Rows[0]["AppCountry"].ToString();
            txtAppPOBox.Text    = dt.Rows[0]["AppPOBox"].ToString();
            txtAppTelNo1.Text   = dt.Rows[0]["AppTelNo1"].ToString();
            txtAppTelNo2.Text   = dt.Rows[0]["AppTelNo2"].ToString();
            txtAppFax.Text      = dt.Rows[0]["AppFax"].ToString();
            txtAppUrl.Text      = dt.Rows[0]["AppUrl"].ToString();
            txtAppEmail.Text    = dt.Rows[0]["AppEmail"].ToString();
            
            ddlAppCalendar.SelectedIndex = ddlAppCalendar.Items.IndexOf(ddlAppCalendar.Items.FindByValue(dt.Rows[0]["AppCalendar"].ToString()));

            if ((dt.Rows[0]["AppLogo"] == DBNull.Value) || (dt.Rows[0]["AppLogoImageLength"].ToString() == "0")) { imgLogo.ClearImage(); } else {  imgLogo.setImage("Logo"); }     
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ClearUI() { PopulateUI(); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e) { ClearUI(); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/ 
    #region Custom Validate Events
    
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }

    #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/ 

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
