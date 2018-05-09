using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Login : System.Web.UI.Page
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DataTable dt;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_PreInit(object sender, EventArgs e) 
    { 
        string Applang = Session["Language"] != null ? Applang = Session["Language"].ToString() : Applang = General.getAppLanguage();
        Page.Theme = "Theme" + Applang;
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(General.getCulture(Applang));
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e) 
    {
        //string fg = CryptorEngine.Encrypt("admin", true);
        if (!IsPostBack) { /*txtLoginID.Text = txtPassword.Attributes["value"] = "admin";*/ }

        txtLoginID.Focus();

        if (Session["Language"] != null) { pageDiv.Attributes.Add("dir", General.getDir(Session["Language"].ToString())); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string getwidth() 
    {
        if (Session["Language"] != null) { if (Session["Language"].ToString() == "Ar") { return "15%"; } }
        return "25%";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            dt = DBFun.FetchData(" SELECT * FROM ApplicationSetup");
            if (!DBFun.IsNullOrEmpty(dt))
            {
                if (dt.Rows[0]["AppCalendar"].ToString() == "H") { Session["DateFormat"] = "Hijri"; } else { Session["DateFormat"] = "Gregorian"; }
            }
            UserLogin();
        }
        catch (Exception e1)
        {
            DBFun.InsertError("Login.aspx", "btnLogin");
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void UserLogin()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        string loginDate = DateTime.Now.ToString("dd/MM/yyyy");

        Session["Permissions"] = null;
        StringBuilder Q = new StringBuilder();
        Q.Append("SELECT * FROM AppUsers WHERE UsrStatus = '1' AND UsrLoginID = @P1 ");
        if (txtLoginID.Text != "admin") { Q.Append(" AND GETDATE() BETWEEN UsrStartDate AND UsrExpiryDate "); }

        DataTable dt = DBFun.FetchData(Q.ToString(), new string[] { txtLoginID.Text });
        if (!DBFun.IsNullOrEmpty(dt))
        {
            if (CryptorEngine.Decrypt(dt.Rows[0]["UsrPassword"].ToString(), true) == txtPassword.Text)
            {
                try
                {
                    Session["UserName"]        = txtLoginID.Text;
                    Session["Permissions"]     = CryptorEngine.Decrypt(dt.Rows[0]["UsrPermission"].ToString(), true);
                    string ss = Session["Permissions"].ToString(); 
                    Session["PreferedCulture"] = "en-US";
                    Session["Language"]        = dt.Rows[0]["UsrLanguage"].ToString();
                    Session["Role"]            = "User";
                    Session["MyTheme"]         = "Theme" + Session["Language"].ToString();


                    string url = InfoTab.FindFirstTab();
                    if (!string.IsNullOrEmpty(url)) { Response.Redirect(url); }
                    else 
                    {  
                         MessageFun.ShowMsg(this, MessageFun.TypeMsg.Error, General.Msg("You do not have access,Please contact the Administrator", "لا يمكنك الدخول,الرجاء مراجعة مدير النظام"));
                    }
                    
                    return;
                }
                catch (Exception e1)
                {
                    DBFun.InsertError("Login.aspx", "UserLogin");
                }
            }
        }

        MessageFun.ShowMsg(this, MessageFun.TypeMsg.Error, General.Msg("You do not have access,Please contact the Administrator", "لا يمكنك الدخول,الرجاء مراجعة مدير النظام")); 
    } 
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
