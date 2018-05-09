using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    protected override void OnPreInit(EventArgs e)
    {
      
        base.OnPreInit(e);
        if (Session["MyTheme"] == null)
        {
            Session.Add("MyTheme", "ThemeEn");
            Page.Theme = Session["MyTheme"].ToString();
        }
        else
        {
            Page.Theme = Session["MyTheme"].ToString();
        }
    }

     public String CurrentCulture
     {
         get
         {
             if (null != Session["PreferedCulture"])
                 return Session["PreferedCulture"].ToString();
             else
                 return "en-US";
         }
         set
         {
             Session["PreferedCulture"] = value;
         }
     }

     protected override void InitializeCulture()
     {
        string CurrentCulture = "en-US";
        if (Session["Language"] != null)
        {
            if (Session["Language"].ToString() == "Ar") { CurrentCulture = "ar-Sa"; } else { CurrentCulture = "en-US";}
        }
        else
        {
            CurrentCulture = "en-US";
        }

         UICulture = CurrentCulture;
         System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
         System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(CurrentCulture);
         base.InitializeCulture();

     }
}

