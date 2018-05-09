using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public partial class EmailConfig : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmailSettingPro ProCs  = new EmailSettingPro();
    EmailSettingSql SqlCs  = new EmailSettingSql();
    
    string GenralQuery = " SELECT * FROM EmailSetting ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	protected void Page_Load(object sender, EventArgs e)
	{
        /*** Fill Session ************************************/
        FormSession.FillSession("Config",pageDiv);
        
        /*** Fill Session ************************************/
       
		if (!Page.IsPostBack)
		{
            pnlMain.Attributes.Add("onkeypress", "javascript:return DefaultButton(event,'" + btnSave.ClientID + "');");

            MainMasterPage.ShowTitel(General.Msg("E-mail settings", "إعدادات البريد الإلكتروني"));

            if (!FormSession.getPerm(new string[] {"IEml" , "UEml" })) { Response.Redirect(@"~/Login.aspx"); btnSave.Enabled = false; } else { btnSave.Enabled = true; }
            
            UIDataEnabled(true);
            ViewState["CommandName"] = "NOT";

            PopulateUI();
		}
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    #region DataItem Events

    public void UIDataEnabled(bool pStatus)
    {
        txtEmlServerID.Enabled         = pStatus;
        txtEmlPortNo.Enabled           = pStatus;
        txtEmlSenderEmail.Enabled      = pStatus;
        txtEmlSenderPassword.Enabled   = pStatus;
        cbEmlCredential.Enabled        = pStatus;
        cbEmlSsl.Enabled               = pStatus;
        txtEmlCountDaysForSend.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProCs.DateType = FormSession.DateType;
            
            ProCs.EmlServerID         = txtEmlServerID.Text;
            ProCs.EmlPortNo           = txtEmlPortNo.Text;         
            ProCs.EmlSenderEmail      = txtEmlSenderEmail.Text;
            ProCs.EmlSenderPassword   = txtEmlSenderPassword.Text;
            ProCs.EmlCredential       = cbEmlCredential.Checked.ToString();
            ProCs.EmlSsl              = cbEmlSsl.Checked.ToString();
            ProCs.EmlCountDaysForSend = txtEmlCountDaysForSend.Text;

            ProCs.TransactionBy       = FormSession.LoginUsr;
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ClearItem() 
    { 
        txtEmlServerID.Text         = "";
        txtEmlPortNo.Text           = "";
        txtEmlSenderEmail.Text      = "";
        txtEmlSenderPassword.Text   = "";
        cbEmlCredential.Checked     = false;
        cbEmlSsl.Checked            = false;
        txtEmlCountDaysForSend.Text = ""; 
    }

    #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    #region ButtonAction Events
       
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

            string Action = ViewState["CommandName"].ToString();
            FillPropeties();
            
            SqlCs.InsertUpdate(ProCs);
            
            ClearItem();
            PopulateUI();
            ViewState["CommandName"] = "NOT";

            MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg("Save Data successfully", "تم الحفظ البيانات بنجاح"));
        }
        catch (Exception Ex) 
        { 
            DBFun.InsertError(FormSession.PageName, "btnSave");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	protected void btnCancel_Click(object sender, EventArgs e)
	{
		ViewState["CommandName"] = "NOT";
        ClearItem();
        PopulateUI();
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUI()
    {
        try
        {
            DataTable DT = DBFun.FetchData(GenralQuery);
            if (DBFun.IsNullOrEmpty(DT)) { return; }

            txtEmlServerID.Text       = DT.Rows[0]["EmlServerID"].ToString();
            txtEmlPortNo.Text         = DT.Rows[0]["EmlPortNo"].ToString();
            txtEmlSenderEmail.Text    = DT.Rows[0]["EmlSenderEmail"].ToString();
            txtEmlSenderPassword.Attributes["value"] = DT.Rows[0]["EmlSenderPassword"].ToString();
            cbEmlCredential.Checked   = Convert.ToBoolean(DT.Rows[0]["EmlCredential"]);
            cbEmlSsl.Checked          = Convert.ToBoolean(DT.Rows[0]["EmlSsl"]);
            txtEmlCountDaysForSend.Text = DT.Rows[0]["EmlCountDaysForSend"].ToString();
        }
        catch (Exception ex) { DBFun.InsertError(FormSession.PageName, "PopulateUI"); }
    }

    #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/
    #region Send Email Events

    protected void btnSend_Click(object sender, EventArgs e)
    {
        txtLogSend.Text = "";
        if (string.IsNullOrEmpty(txtSendToEmail.Text)) { txtLogSend.Text = General.Msg("You must enter e-mail","يجب إدخال بريد إلكتروني"); return; } 

        bool isEmail = Regex.IsMatch(txtSendToEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        if (!isEmail) { txtLogSend.Text = General.Msg("Email is not valid","أدخل بريد إلكتروني صحيح"); return; }

        bool isValid = true;
        if (string.IsNullOrEmpty(txtEmlServerID.Text) || string.IsNullOrEmpty(txtEmlPortNo.Text) || string.IsNullOrEmpty(txtEmlSenderEmail.Text) || string.IsNullOrEmpty(txtEmlSenderPassword.Text)) { isValid = false; }
        if (!isValid) { txtLogSend.Text = General.Msg("No Email Setting data entry","يجب إدخال إعدادات البريد الإلكتروني"); return; }

        SendEMail();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool SendEMail()
    {
        try
        {
            System.Net.Mail.MailMessage msgMail = new System.Net.Mail.MailMessage();
            msgMail.Subject = "Test Email From CMWEB";
            msgMail.Body    = "<b>Test Email</b>";             
               
            msgMail.To.Add(txtSendToEmail.Text.Trim());
            msgMail.From = new MailAddress(txtEmlSenderEmail.Text.Trim());
            msgMail.IsBodyHtml = true;
            msgMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            SmtpClient SClient = new SmtpClient();
            SClient = new SmtpClient(txtEmlServerID.Text, Convert.ToInt32(txtEmlPortNo.Text));
            SClient.DeliveryMethod = SmtpDeliveryMethod.Network; 
            
            if (cbEmlCredential.Checked)
            {
                SClient.UseDefaultCredentials = false;
                SClient.Credentials = new NetworkCredential(txtEmlSenderEmail.Text, txtEmlSenderPassword.Text);
            }
            
            if (cbEmlSsl.Checked)
            {
                SClient.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            }

            SClient.Send(msgMail);

            txtLogSend.Text = General.Msg("E-mail sent successfully","تم إرسال البريد الإلكتروني بنجاح");

            return true;
        }
        catch (Exception ex) { txtLogSend.Text = ex.Message; return false; }
    }  

    #endregion
    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*******************************************************************************************************************************/
    /*******************************************************************************************************************************/ 
    #region Custom Validate Events
    
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void CountDaysForSend_ServerValidate(Object source, ServerValidateEventArgs e)
    {        
        try
        {
            if (source.Equals(cvCountDaysForSend))
            {
                MessageFun.ValidMsg(this, ref cvCountDaysForSend, false, General.Msg("You must enter Send Email befor","يجب إدخال عدد الأيام"));    
   
                if (string.IsNullOrEmpty(txtEmlCountDaysForSend.Text)) { e.IsValid = false; }

                if (!string.IsNullOrEmpty(txtEmlCountDaysForSend.Text))
                {
                    MessageFun.ValidMsg(this, ref cvCountDaysForSend, true, General.Msg("The number of days must be greater than 0", "عدد الأيام بجب ان يكون أكبر من 0")); 
                    
                    if (Convert.ToInt32(txtEmlCountDaysForSend.Text) < 1) { e.IsValid = false; } 
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