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

public class FormSession
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private static string _LoginUsr;
    public  static string LoginUsr { get { return _LoginUsr; } set { _LoginUsr = value; } }

    private static string _Role;
    public  static string Role { get { return _Role; } set { _Role = value; } }

    private static string[] _PermUsr;
    public  static string[] PermUsr { get { return _PermUsr; } set { _PermUsr = value; } }

    private static string _GenderUsr;
    public  static string GenderUsr { get { return _GenderUsr; } set { _GenderUsr = value; } }

    private static string _DateType;
    public  static string DateType { get { return _DateType; } set { _DateType = value; } }

    private static string _Language;
    public  static string Language { get { return _Language; } set { _Language = value; } }

    private static string _PageIndex;
    public  static string PageIndex { get { return _PageIndex; } set { _PageIndex = value; } }

    private static string _PageName;
    public  static string PageName { get { return _PageName; } set { _PageName = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void FillSession(string pPageIndex,HtmlGenericControl pDiv)
    {
        try
        {
            if (!string.IsNullOrEmpty(pPageIndex)) { PageIndex = pPageIndex; }

            if (HttpContext.Current.Session["UserName"]    != null) { LoginUsr  = HttpContext.Current.Session["UserName"].ToString();    } else { HttpContext.Current.Response.Redirect(@"~/Login.aspx"); }
            if (HttpContext.Current.Session["Role"]        != null) { Role      = HttpContext.Current.Session["Role"].ToString();        } else { HttpContext.Current.Response.Redirect(@"~/Login.aspx"); }
            if (HttpContext.Current.Session["Permissions"] != null) { PermUsr   = HttpContext.Current.Session["Permissions"].ToString().Split(','); } else { HttpContext.Current.Response.Redirect(@"~/Login.aspx"); }
            if (HttpContext.Current.Session["DateFormat"]  != null) { DateType  = HttpContext.Current.Session["DateFormat"].ToString();  } else { HttpContext.Current.Response.Redirect(@"~/Login.aspx"); }
            if (HttpContext.Current.Session["Language"]    != null) { Language  = HttpContext.Current.Session["Language"].ToString();    } else { HttpContext.Current.Response.Redirect(@"~/Login.aspx"); }

            PageName = new System.IO.FileInfo((HttpContext.Current.Request.Url.AbsolutePath)).Name;
            if (pDiv != null) { pDiv.Attributes.Add("dir", General.getDir(Language)); }
        }
        catch (Exception EX) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static bool getPerm(string Perm)
    {
        try
        {
            if (string.IsNullOrEmpty(Perm)) { return false; }
            return PermUsr.Contains(Perm);
        }
        catch (Exception EX) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static bool getPerm(string[] Perm)
    {
        try
        {
            if (Perm.Length <= 0) { return false; }
            for (int i = 0; i < Perm.Length; i++) { if (PermUsr.Contains(Perm[i])) { return true; } }
            return false;
        }
        catch (Exception EX) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
