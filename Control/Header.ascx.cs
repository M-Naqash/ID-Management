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

public partial class Header : System.Web.UI.UserControl
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    AppUsersPro AppPro = new AppUsersPro();
    AppUsersSql AppSql = new AppUsersSql();
    
    string dateFormat = string.Empty;
    string Language = string.Empty;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["DateFormat"] != null) { dateFormat = Session["DateFormat"].ToString(); }
        if (dateFormat == "Gregorian") { System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); }
        else if (dateFormat == "Hijri") { System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-Sa"); }

        if (Session["Language"] != null)
        {
            Language = Session["Language"].ToString();
            if (Language == "Ar") { lnkChangeLang.ImageUrl = "~/App_Themes/ThemeEn/images/english_icon.png"; } 
            else { lnkChangeLang.ImageUrl = "~/App_Themes/ThemeEn/images/Arabic-icon.png"; }
        }

        if (!IsPostBack) { lnkLogout2.Text = "[" + Session["UserName"].ToString() + "]"; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lnkChangeLang_Click(object sender, EventArgs e)
    {
        if (Session["Language"].ToString() == "Ar") { Session["Language"] = "En"; } else { Session["Language"] = "Ar"; }
        
        if (Session["Language"].ToString() == "Ar") { Session["MyTheme"] = "ThemeAr"; }
        if (Session["Language"].ToString() == "En") { Session["MyTheme"] = "ThemeEn"; }

        AppPro.UsrLoginID    = FormSession.LoginUsr;
        AppPro.UsrLanguage   = Session["Language"].ToString();
        AppPro.TransactionBy = FormSession.LoginUsr;
        AppSql.UpdateLanguage(AppPro);

        Server.Transfer(Request.FilePath);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session["Permissions"] = null;
        Session.Contents.RemoveAll();
        Session.Abandon();
        Response.Redirect(@"~/Login.aspx");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lnkChangePassword_Click(object sender, EventArgs e)
    {
        Server.Transfer(@"~/Home/ChangePassword.aspx");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}


