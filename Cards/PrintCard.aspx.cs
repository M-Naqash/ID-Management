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
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class PrintCard : BasePage
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DataTable dt;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //   --------------------Common Code ----------------------------------------------------------------- //
            string Type = (Request.QueryString["Type"] != null) ? Request.QueryString["Type"] : "";
            if (Type == "PCard" || Type == "TCard" || Type == "PStck" || Type == "TStck") { FormSession.FillSession("Card", pageDiv); }
            if (Type == "PVCrd" || Type == "TVCrd") { FormSession.FillSession("Visitor", pageDiv); }
            
            //   --------------------Common Code ----------------------------------------------------------------- //
            if (!IsPostBack)
            {
                string CardType = "";
                if (Type == "PCard")  { CardType = "CardPrint";    /**/ CardsSideMenu1.Visible = true; /**/ MainMasterPage.ShowTitel(General.Msg("Print Card", "طباعة البطاقات")); }
                if (Type == "TCard")  { CardType = "CardTemplate"; /**/ CardsSideMenu1.Visible = true;  /**/ MainMasterPage.ShowTitel(General.Msg("Templates Card", "نماذج البطاقات")); }
                
                if (Type == "PStck") { CardType = "SCardPrint";    /**/CardsSideMenu1.Visible = true;/**/  MainMasterPage.ShowTitel(General.Msg("Print Sticker Cars", "طباعة ملصقات السيارات")); }
                if (Type == "TStck") { CardType = "SCardTemplate"; /**/CardsSideMenu1.Visible = true;/**/  MainMasterPage.ShowTitel(General.Msg("Templates Sticker Cars", "نماذج ملصقات السيارات")); }

                if (Type == "PVCrd") { CardType = "VCardPrint";    /**/ VisitorsSideMenu1.Visible = true; /**/ MainMasterPage.ShowTitel(General.Msg("Print Events Cards", "طباعة بطاقات المناسبات")); }
                if (Type == "TVCrd") { CardType = "VCardTemplate"; /**/ VisitorsSideMenu1.Visible = true; /**/ MainMasterPage.ShowTitel(General.Msg("Templates Events Cards", "نماذج بطاقات المناسبات")); }

                //if (Type == "ViCard") { CardType = "CardView";/**/ CardsSideMenu1.Visible = true;  /**/ MainMasterPage.ShowTitel(General.Msg("View Card", "عرض البطاقات")); }
                
                hfdConnStr.Value   = ConfigurationManager.ConnectionStrings["constring"].ConnectionString.Replace("\\","....");
                hfdLoginUser.Value = FormSession.LoginUsr.Replace("\\","....");
                hfdLang.Value      = FormSession.Language;
                hfdType.Value      = CardType;
                string Value = hfdConnStr.Value + "," + hfdLoginUser.Value + "," + hfdLang.Value + "," + hfdType.Value;
                ClientScript.RegisterStartupScript(this.GetType(), "key", "javascript:Connect('" + Value + "');", true);
            }
        }
        catch (Exception e1)
        {
            DBFun.InsertError(FormSession.PageName, "PageLoad");
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}