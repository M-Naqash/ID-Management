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

public static class InfoTab
{   
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static int FindCount() { return 7; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static int FindPageIndex(string pName) 
    {
        try
        {
            if (pName == "Home")      { return 1; }
            if (pName == "Users")     { return 2; }
            if (pName == "Employees") { return 3; }
            if (pName == "Card")      { return 4; }
            if (pName == "Visitors")  { return 5; }
            if (pName == "Reports")   { return 6; }
            if (pName == "Config")    { return 7; }
            return -1;
        }
        catch (Exception EX) { return -1; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string FindPageInfo(int pIndex , string pInfo)
    {
        try
        {
            string PageName = "", PageTitel = "", PageUrl = "";

            if (pIndex == 1) { PageName = "Home";      PageTitel = General.Msg("Home", "البداية");          PageUrl = @"~/Home/Home.aspx"; }
            if (pIndex == 2) { PageName = "Users";     PageTitel = General.Msg("Users", "المستخدمون");      PageUrl = @"~/Users/UsrHome.aspx"; }
            if (pIndex == 3) { PageName = "Employees"; PageTitel = General.Msg("Employees", "الموظفين");    PageUrl = @"~/Employee/EmployeesHome.aspx"; }
            if (pIndex == 4) { PageName = "Card";      PageTitel = General.Msg("Cards", "البطاقات");        PageUrl = @"~/Cards/CrdHome.aspx"; }
            if (pIndex == 5) { PageName = "Visitors";  PageTitel = General.Msg("Events", "المناسبات");        PageUrl = @"~/Visitors/VisitorsHome.aspx"; }
            
            if (pIndex == 6) { PageName = "Reports";   PageTitel = General.Msg("Reports", "التقارير");       PageUrl = @"~/Reports/ReportsMain.aspx"; }
            if (pIndex == 7) { PageName = "Config";    PageTitel = General.Msg("Configuration", "الإعدادات"); PageUrl = @"~/Configuration/ConfigHome.aspx"; }


            if (pInfo == "Name")  { return PageName;  }
            if (pInfo == "Titel") { return PageTitel; }
            if (pInfo == "Url")   { return PageUrl;   }
            return "";
        }
        catch (Exception EX) { return ""; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string FindCss(int pIndex) { if (pIndex == 2) { return "Employees"; } else { return "MainNav"; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string getPerm()
    {
        try
        {
            string Perm = HttpContext.Current.Session["Permissions"].ToString();
            //if (HttpContext.Current.Session["Role"].ToString() == "User")     {  Perm += ",Home,UsrHome";  }
            //if (HttpContext.Current.Session["Role"].ToString() == "Employee") {  Perm += ",Home,EmpHome";  }
            return Perm;
        }
        catch (Exception EX) { return ""; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static bool IsAdd(int pIndex)
    {
        try
        {
            string[] Perm = getPerm().Split(',');
            string PagePerm = FindPageInfo(pIndex, "Name");
            if (!string.IsNullOrEmpty(PagePerm)) { if (Perm.Contains(PagePerm)) { return true; } else { return false; } } else { return false; }
        }
        catch (Exception EX) { return false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void AddTabs(int pIndex, int pIndexTab, string pLang, HtmlTableCell pnl) //Panel pnl)
    {
        Table tb = new Table();
        //tb.Attributes.Add("align", General.getAlign(2));

        if (pIndex == pIndexTab) { tb.CssClass = "NormalTab"; } else { tb.CssClass = "ActiveTab"; }
        tb.CellPadding = 0;
        tb.CellSpacing = 0;
        tb.ID = "Tabpage" + pIndex.ToString();

        TableRow tr = new TableRow();
        
        /*TdLeft*/
        TableCell tdleft = new TableCell();
        if (pIndex == pIndexTab) { tdleft.CssClass = "NormalTab_Left"; } else { tdleft.CssClass = "ActiveTab_Left"; }
        tdleft.ID = "TdLeft" + pIndex.ToString();
        /*TdLeft*/

        /*Tdmid*/
        TableCell tdmidd = new TableCell();
        tdmidd.CssClass = "TabPadding";
        tdmidd.ID = "Tdmid" + pIndex.ToString();
        tdmidd.Width = 136;
        //if (pIndex == 3 || pIndex == 4) { tdmidd.Width = 150; } else { tdmidd.Width = 100; }
        HyperLink h1 = new HyperLink();

        h1.CssClass = FindCss(pIndex);
        h1.ID = "Tab" + pIndex.ToString();
        h1.Text = FindPageInfo(pIndex, "Titel");
        h1.NavigateUrl = FindPageInfo(pIndex, "Url");

        tdmidd.Controls.Add(h1);
        /*Tdmid*/

        /*Tdright*/
        TableCell tdright = new TableCell();
        if (pIndex == pIndexTab) { tdright.CssClass = "NormalTab_Right"; } else { tdright.CssClass = "ActiveTab_Right"; }
        tdright.ID = "Tdright" + pIndex.ToString();
        /*Tdright*/

        if (pLang == "Ar") { tr.Cells.Add(tdright);} else { tr.Cells.Add(tdleft); }
        tr.Cells.Add(tdmidd);
        if (pLang == "Ar") { tr.Cells.Add(tdleft);} else { tr.Cells.Add(tdright); }

        tb.Rows.Add(tr);

        pnl.Controls.Add(tb);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void ShowTabs(int pIndex, string pLang, HtmlTableCell pnl) //Panel pnl)
    {
        try
        {
            if (pLang == "Ar") { for (int i = FindCount(); i > 0 ; i--) { if (IsAdd(i)) { AddTabs(i, pIndex, pLang, pnl); } }} 
            else { for (int i = 1; i <= FindCount(); i++) { if (IsAdd(i)) { AddTabs(i, pIndex, pLang, pnl); } } } 
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void FillTabsList(DropDownList ddl)
    {
        try
        {
            for (int i = 1; i <= FindCount(); i++) 
            { 
                string PageName  = FindPageInfo(i, "Name");
                string PageTitel = FindPageInfo(i, "Titel");
                ddl.Items.Add(new ListItem(PageTitel, PageName));
            }

            ListItem lsMsg = new ListItem(General.Msg("-Select Home page-", "-اختر صفحة البداية-"),"0");
            ddl.Items.Insert(0, lsMsg);
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string FindUrlHome(string PageName)
    {
        try
        {
            int PageIndex = FindPageIndex(PageName);
            if (PageIndex != -1) { return FindPageInfo(PageIndex, "Url"); }

            return "";
        }
        catch (Exception e1) { return ""; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string FindFirstTab()
    {
        string url = "";
        
        try
        {
            string[] Perm = getPerm().Split(',');
            
            for (int i = 1; i <= FindCount(); i++)
            { 
                string PagePerm = FindPageInfo(i, "Name");
                if (!string.IsNullOrEmpty(PagePerm))
                {
                    if (Perm.Contains(PagePerm)) { url = FindPageInfo(i, "Url"); break; }
                }
            }
            
        }
        catch (Exception e1) { url = ""; }

        return url;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
