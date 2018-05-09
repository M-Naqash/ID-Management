using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class LoginMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Applang = Session["Language"] != null ? Applang = Session["Language"].ToString() : Applang = General.getAppLanguage();

        MainDiv.Attributes.Add("dir", General.getDir(Applang)); 
        Session["Language"] = Applang; 
        Session["MyTheme"] = "Theme" + Applang;

        lblHeading.Text = General.Msg("Login", "شاشة الدخول");
    }
}
