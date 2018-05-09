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

public partial class LoginHeader : System.Web.UI.UserControl
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string dateFormat = string.Empty;
    DataTable dt;
    string Language = string.Empty;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["DateFormat"] != null) { dateFormat = Session["DateFormat"].ToString(); }
        if (dateFormat == "Gregorian") { System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); }
        else if (dateFormat == "Hijri") { System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-Sa"); }

        if (Session["Language"] != null) { Language = Session["Language"].ToString(); } else { Session["Language"] = "En"; }

        if (!IsPostBack) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lnkChangLang_Click(object sender, EventArgs e)
    {
        if (Session["Language"].ToString() == "Ar") { Session["Language"] = "En"; } else { Session["Language"] = "Ar"; }
        if (Session["Language"].ToString() == "Ar") { Session["MyTheme"] = "ThemeAr"; } else { Session["MyTheme"] = "ThemeEn";  }
        Server.Transfer(Request.FilePath);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}





