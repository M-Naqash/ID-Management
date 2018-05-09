using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;
using System.Threading;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Web.Profile;
using System.Drawing;

public class MessageFun
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum TypeMsg { Info, Success,Warning,Error,Validation };
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void ShowMsg(Page pg, ValidationSummary vs, CustomValidator cv, TypeMsg Type,string VG, string pMsg) 
    {
        vs.ValidationGroup = VG;
        cv.ErrorMessage    = pMsg;
        cv.ValidationGroup = VG;

        if      (Type == TypeMsg.Info)       { vs.CssClass = "MsgInfo";       /****/ vs.ForeColor = ColorTranslator.FromHtml("#00529B"); }
        else if (Type == TypeMsg.Success)    { vs.CssClass = "MsgSuccess ";   /****/ vs.ForeColor = ColorTranslator.FromHtml("#4F8A10"); } 
        else if (Type == TypeMsg.Warning)    { vs.CssClass = "MsgWarning";    /****/ vs.ForeColor = ColorTranslator.FromHtml("#9F6000"); }
        else if (Type == TypeMsg.Error)      { vs.CssClass = "MsgError";      /****/ vs.ForeColor = ColorTranslator.FromHtml("#D8000C"); }
        else if (Type == TypeMsg.Validation) { vs.CssClass = "MsgValidation"; /****/ vs.ForeColor = ColorTranslator.FromHtml("#D63301"); }
        
        
        pg.Validate(VG);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void ShowMsg(Page pg, TypeMsg Type, string pMsg) 
    {
        string VG = "vgShowMsg";
        ValidationSummary vs = pg.Master.FindControl("ContentPlaceHolder2").FindControl("vsShowMsg") as ValidationSummary; 
        CustomValidator   cv = pg.Master.FindControl("ContentPlaceHolder2").FindControl("cvShowMsg") as CustomValidator; 

        vs.ValidationGroup = VG;
        cv.ValidationGroup = VG;
        cv.ErrorMessage    = pMsg;
        
        if      (Type == TypeMsg.Info)       { vs.CssClass = "MsgInfo";       /****/ vs.ForeColor = ColorTranslator.FromHtml("#00529B"); }
        else if (Type == TypeMsg.Success)    { vs.CssClass = "MsgSuccess ";   /****/ vs.ForeColor = ColorTranslator.FromHtml("#4F8A10"); } 
        else if (Type == TypeMsg.Warning)    { vs.CssClass = "MsgWarning";    /****/ vs.ForeColor = ColorTranslator.FromHtml("#9F6000"); }
        else if (Type == TypeMsg.Error)      { vs.CssClass = "MsgError";      /****/ vs.ForeColor = ColorTranslator.FromHtml("#D8000C"); }
        else if (Type == TypeMsg.Validation) { vs.CssClass = "MsgValidation"; /****/ vs.ForeColor = ColorTranslator.FromHtml("#D63301"); }
        
        
        pg.Validate(VG);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void ShowAdminMsg(Page pg, ValidationSummary vs, CustomValidator cv,string VG,  string pEx) 
    {
        string MMsg = General.Msg("Transaction failed to commit please contact your administrator. ","النظام غير قادر على حفظ البيانات, الرجاء الاتصال بمدير النظام. ");
        string DMsg = General.Msg("<a href='#' onclick=\"alert('" + pEx + "');\">To find out the error details Click here </a> ","<a href='#' onclick=\"alert('" + pEx + "');\">لمعرفة تفاصيل الخطأ اضغط هنا </a> ");
        vs.ValidationGroup = VG;
        pEx = pEx.Replace("'"," ");
        cv.ErrorMessage    = MMsg + DMsg;
        cv.ValidationGroup = VG;
        vs.CssClass = "MsgError";
        vs.ForeColor = ColorTranslator.FromHtml("#D8000C");
        pg.Validate(VG);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void ShowAdminMsg(Page pg, string pEx) 
    {
        string MMsg = General.Msg("Transaction failed to commit please contact your administrator. ","النظام غير قادر على حفظ البيانات, الرجاء الاتصال بمدير النظام. ");
        string DMsg = General.Msg("<a style=\"color:Blue\" href='#' onclick=\"alert('" + pEx.Replace("'","") + "');\">To find out the error details Click here </a> ","<a style=\"color:Blue\" href='#' onclick=\"alert('" + pEx.Replace("'","") + "');\">لمعرفة تفاصيل الخطأ اضغط هنا </a> ");
        
        string VG = "vgShowMsg";
        ValidationSummary vs = pg.Master.FindControl("ContentPlaceHolder2").FindControl("vsShowMsg") as ValidationSummary; 
        CustomValidator   cv = pg.Master.FindControl("ContentPlaceHolder2").FindControl("cvShowMsg") as CustomValidator; 
        
        
        vs.ValidationGroup = VG;
        pEx = pEx.Replace("'"," ");
        cv.ErrorMessage    = MMsg + DMsg;
        cv.ValidationGroup = VG;
        vs.CssClass = "MsgError";
        vs.ForeColor = ColorTranslator.FromHtml("#D8000C");
        pg.Validate(VG);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void ValidMsg(Page pg, ref CustomValidator cv,bool isShow, string pMsg) 
    { 
        if (isShow) { cv.ErrorMessage = pMsg; } else { cv.ErrorMessage = ""; }
        cv.Text = pg.Server.HtmlDecode("&lt;img src='../Images/Icon/Exclamation.gif' title='" + pMsg + "' /&gt;");  
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}