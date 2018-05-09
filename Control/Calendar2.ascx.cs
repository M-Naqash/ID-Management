using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

[ValidationProperty("Text")]
public partial class Control_Calendar2 : System.Web.UI.UserControl
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string DateType = "";
    GregorianCalendar Grn = new GregorianCalendar();
    UmAlQuraCalendar  Umq = new UmAlQuraCalendar();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _Date;
    public string Date
    {
        get { return _Date; }
        set { if (_Date != value) { _Date = value; } }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum CalendarEnum { Gregorian, Hijri, Both, System }
    public CalendarEnum CalendarType
    {
        get { return ((ViewState["CalendarType"] == null) ? CalendarEnum.System : (CalendarEnum)ViewState["CalendarType"]); }
        set { ViewState["CalendarType"] = value; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string ValidationGroup
    {
        get { return ((ViewState["vg"] == null) ? "" : ViewState["vg"].ToString()); }
        set { ViewState["vg"] = value; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //public TextBox txtSelectedDate { get { return this.txtDate; } }
    //public ImageButton ImageDate { get { return this.imgbtnShowGCalendar; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, System.EventArgs e)
    {   
        CalendarEnum CalendarType;
        try { CalendarType = (CalendarEnum)ViewState["CalendarType"]; } catch { CalendarType = CalendarEnum.System;}

        if      (CalendarType == CalendarEnum.Gregorian) { DateType = "G"; tdHCal.Visible = false; txtType.Width = 36; } //imgbtnShowHCalendar
        else if (CalendarType == CalendarEnum.Hijri)     { DateType = "H"; tdGCal.Visible = false; txtType.Width = 34; } //imgbtnShowGCalendar
        else if (CalendarType == CalendarEnum.Both)      { DateType = "B"; }
        else
        { 
            DateType = "S";
            if ( Session["DateFormat"].ToString() == "Gregorian" ) { tdHCal.Visible = false; txtType.Width = 36; } 
            if ( Session["DateFormat"].ToString() == "Hijri" )     { tdGCal.Visible = false; txtType.Width = 34; } 
        }
         
        if (!Page.IsPostBack) 
        { 
            try { rvDate.ValidationGroup = ViewState["vg"].ToString(); } catch { rvDate.Enabled =false; }
            this.pnlCalendar.Attributes.Add("style", "DISPLAY: none; POSITION: absolute"); 
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void CalDate_SelectionChanged(object sender, System.EventArgs e)
    {
        string TypeSpecifiedDate = ViewState["TypeSpecifiedDate"].ToString(); 

        if      (TypeSpecifiedDate == "H") { System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-Sa"); }
        else if (TypeSpecifiedDate == "G") { System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); }
        
        this.txtDate.Text = CalDate.SelectedDate.ToString("dd/MM/yyyy");
        if (DateType == "S") { this.txtType.Text = "S"; } else { this.txtType.Text = TypeSpecifiedDate; }
        CalDate.SelectedDates.Clear();
        this.pnlCalendar.Attributes.Add("style", "DISPLAY: none; POSITION: absolute");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e) { Changedate(ViewState["TypeSpecifiedDate"].ToString()); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e) { Changedate(ViewState["TypeSpecifiedDate"].ToString()); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Changedate(string pType)
    {
        try
        {
            if (pType == "G")
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); 
                CalDate.TodaysDate = new DateTime(Convert.ToInt32(ddlYears.SelectedValue), Convert.ToInt32(ddlMonths.SelectedValue), 1);
                //CalDate.SelectedDate =new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }
            else if (pType == "H")
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-Sa"); 
                CalDate.TodaysDate = new DateTime(Convert.ToInt32(ddlYears.SelectedValue), Convert.ToInt32(ddlMonths.SelectedValue), 1, Umq);
                //CalDate.SelectedDate =new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Umq);
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void imgbtnShowCalendar_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ib = (ImageButton)sender;
        if (ib.ID == "imgbtnShowGCalendar")
        {
            ViewState["TypeSpecifiedDate"] = "G"; 
            ddlYears.Items.Clear();
            ddlMonths.Items.Clear();
            DateFun.PopulateGreYear(ref ddlYears);
            DateFun.PopulateGreMonth(ref ddlMonths);
        }
        else if (ib.ID == "imgbtnShowHCalendar")
        { 
            ViewState["TypeSpecifiedDate"] = "H"; 
            ddlYears.Items.Clear();
            ddlMonths.Items.Clear();
            DateFun.PopulateHijYear(ref ddlYears);
            DateFun.PopulateHijMonth(ref ddlMonths);
        }

        Changedate(ViewState["TypeSpecifiedDate"].ToString());
        CssStyleCollection pnlCalendarStyle = this.pnlCalendar.Style;
        string DISPLAY = pnlCalendarStyle["DISPLAY"];

        if (DISPLAY == "none") { this.pnlCalendar.Attributes.Add("style", "POSITION: absolute"); } else { this.pnlCalendar.Attributes.Add("style", "DISPLAY: none; POSITION: absolute"); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void setEnabled(bool pStatus)
    {
        this.imgbtnShowGCalendar.Enabled = pStatus;
        this.imgbtnShowHCalendar.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void setValidationEnabled(bool pStatus)
    {
        this.rvDate.Enabled = pStatus;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ClearDate()
    {
        this.txtDate.Text = "";
        this.txtType.Text = "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void setDate(string pDate,string pType)
    {
        if (string.IsNullOrEmpty(pDate) || string.IsNullOrEmpty(pType)) { ClearDate(); }
        else 
        {
            this.txtType.Text = pType;
            this.txtDate.Text = pDate;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void setDBDate(object pDate,string pType)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        string DType = "Gregorian";

        if (pDate != DBNull.Value && !string.IsNullOrEmpty(pType)) 
        { 
            DateTime date = (DateTime)pDate;
            if (pType == "S") { DType = Session["DateFormat"].ToString(); } else if (pType == "G") { DType = "Gregorian"; } else if (pType == "H") { DType = "Hijri"; }

            this.txtType.Text = pType;
            if (DType == "Gregorian") { this.txtDate.Text = fd(Grn.GetDayOfMonth(date).ToString()) + "/" + fd(Grn.GetMonth(date).ToString()) + "/" + fd(Grn.GetYear(date).ToString()); }
             if (DType == "Hijri")    { this.txtDate.Text = DateFun.GrnToHij(Convert.ToDateTime(pDate)).ToString(); }
        } 
        else { ClearDate(); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string fd(string dt) { if (dt.Length < 2) { return "0" + dt; } else { return dt; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string getDate() { return this.txtDate.Text; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public Char getDateType() { return Convert.ToChar(this.txtType.Text); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void setTodayDate()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string DType = "Gregorian";

            if (DateType == "S") { DType = Session["DateFormat"].ToString(); } else if (DateType == "G") { DType = "Gregorian"; } else if (DateType == "H") { DType = "Hijri"; }

            this.txtType.Text = DateType;
            if (DType == "Gregorian") { this.txtDate.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now); }
            if (DType == "Hijri")     { this.txtDate.Text = DateFun.GrnToHij(DateTime.Now).ToString(); }

        }
        catch (Exception EX) { ClearDate(); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void imgbtnClearCalendar_Click(object sender, ImageClickEventArgs e) 
    {  
        CssStyleCollection pnlCalendarStyle = this.pnlCalendar.Style;
        string DISPLAY = pnlCalendarStyle["DISPLAY"];

        if (DISPLAY == "none") { } else { this.pnlCalendar.Attributes.Add("style", "DISPLAY: none; POSITION: absolute"); }
        txtDate.Text = "";   
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public Int64 getIntDate() 
    { 
        if (!string.IsNullOrEmpty(txtDate.Text))
        {
            string d = txtDate.Text.Substring(0, 2);
            string m = txtDate.Text.Substring(3, 2);
            string y = txtDate.Text.Substring(6, 4);
            return Convert.ToInt64(y + m + d);
        }
    
        return 0;
    }
}