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
using System.Globalization;

public class DateFun
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static GregorianCalendar Grn = new GregorianCalendar();
    static UmAlQuraCalendar Umq = new UmAlQuraCalendar();
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static DateTime HijToGrn(string pHdate)
    {      
        string[] PartDate = pHdate.Split('/');
        int day, month, year;
        Int32.TryParse(PartDate[0], out day);
        Int32.TryParse(PartDate[1], out month);
        Int32.TryParse(PartDate[2], out year);
        return new DateTime(year, month, day, Umq);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrnToHij(DateTime pGdate)
    {
        int year  = Umq.GetYear(pGdate);
        int month = Umq.GetMonth(pGdate);
        int day   = Umq.GetDayOfMonth(pGdate);
        return fd(day.ToString()) + "/" + fd(month.ToString()) + "/" + year.ToString();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrnToHij(string pGdate)
    {
        int year  = Umq.GetYear(Convert.ToDateTime(pGdate));
        int month = Umq.GetMonth(Convert.ToDateTime(pGdate));
        int day   = Umq.GetDayOfMonth(Convert.ToDateTime(pGdate));
        return day.ToString() + "/" + month.ToString() + "/" + year.ToString();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static DateTime strTodt(string pStrDate)
    {
        if (!string.IsNullOrEmpty(pStrDate))
        {
            string[] partDT = pStrDate.Split(' ');

            string[] PartDate = partDT[0].Split('/');
            int day, month, year;
            Int32.TryParse(PartDate[0], out day);
            Int32.TryParse(PartDate[1], out month);
            Int32.TryParse(PartDate[2], out year);
            return new DateTime(year, month, day);
        }
        else { return new DateTime(); }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void PopulateGreYear(ref DropDownList ddl)
    {
        int minYear = DateTime.Now.Year - 70;
        int maxYear = DateTime.Now.Year + 10;
        for (int i = minYear; i <= maxYear; i++) { ddl.Items.Add(i.ToString()); }
        ddl.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void PopulateHijYear(ref DropDownList ddl)
    {
        string StrDate = GrnToHij(DateTime.Now);

        string[] PartDate = StrDate.Split('/');
        Int32 Y = Convert.ToInt32(PartDate[2]);
        int minYear = Y - 70;
        int maxYear = 1450;

        for (int i = minYear; i <= maxYear; i++) { ddl.Items.Add(i.ToString()); }
        ddl.Items.FindByValue(Y.ToString()).Selected = true;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void PopulateGreMonth(ref DropDownList ddl)
    {
        string[] MonthsEn = { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        string[] MonthsAr = { "", "يناير", "فبراير", "مارس", "ابريل", "مايو", "يونيو", "يوليو", "اغسطس", "سبتمبر", "أكتوبر", "نوفمبر", "ديسمبر" };
        ListItem item;

        ddl.Items.Clear();
        for (int i = 1; i < MonthsEn.Length; i++)
        {
            item = new ListItem();
            if (HttpContext.Current.Session["Language"].ToString() == "Ar") { item.Text = MonthsAr[i]; } else { item.Text = MonthsEn[i]; }
            item.Value = i.ToString();
            ddl.Items.Add(item);
        }
        ddl.SelectedIndex = DateTime.Now.Month - 1;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void PopulateHijMonth(ref DropDownList ddl)
    {
        string[] MonthsEn = { "", "Muharram", "Safar", "Rabii I", "Rabii II", "Jumada I", "Jumada II", "Rajab", "Sha'aban", "Ramadan", "Shawwal", "Dhu al-Qa'aida", "Dhual-Hijja" };
        string[] MonthsAr = { "", "محرم", "صفر", "ربيع اول", "ربيع ثاني", "جمادى الأول", "جمادى الثاني", "رجب", "شعبان", "رمضان", "شوال", "ذو القعدة", "ذو الحجة" };
        ListItem item;

        ddl.Items.Clear();
        for (int i = 1; i < MonthsEn.Length; i++)
        {
            item = new ListItem();
            if (HttpContext.Current.Session["Language"].ToString() == "Ar") { item.Text = MonthsAr[i]; } else { item.Text = MonthsEn[i]; }
            item.Value = i.ToString();
            ddl.Items.Add(item);
        }

        string date = GrnToHij(DateTime.Now);
        string[] arrDate = date.Split('/');
        Int32 M = Convert.ToInt32(arrDate[1]);
        ddl.SelectedIndex = M - 1;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string fd(string dt) { if (dt.Length < 2) { return "0" + dt; } else { return dt; } }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string displayDateGrd(object pGre, string pDateType)
    {
        if (pGre == DBNull.Value) { return ""; }
        
        DateTime greg = (DateTime)pGre;
        
        if (pDateType == "Gregorian")
        {      
            return greg.ToString("dd/MM/yyyy");
        }
        else
        {
            int year  = Umq.GetYear(greg);
            int month = Umq.GetMonth(greg);
            int day   = Umq.GetDayOfMonth(greg);
            return fd(year.ToString()) + "/" + fd(month.ToString()) + "/" + fd(day.ToString());
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrdDisplayDate(object pDate,object pType)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        string DType = "Gregorian";

        if (pDate != DBNull.Value && pType != DBNull.Value) 
        { 
            DateTime date = (DateTime)pDate;
            if      (pType.ToString() == "S") { DType = HttpContext.Current.Session["DateFormat"].ToString(); }
            else if (pType.ToString() == "G") { DType = "Gregorian"; } 
            else if (pType.ToString() == "H") { DType = "Hijri"; }

            if (DType == "Gregorian") { return fd(Grn.GetDayOfMonth(date).ToString()) + "/" + fd(Grn.GetMonth(date).ToString()) + "/" + fd(Grn.GetYear(date).ToString()); }
            if (DType == "Hijri")     { return DateFun.GrnToHij(Convert.ToDateTime(pDate)).ToString(); }
        } 
        else { return null; }

        return null;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrdDisplayDateTime(object pDate,object pType)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        string DType = "Gregorian";

        if (pDate != DBNull.Value && pType != DBNull.Value) 
        { 
            DateTime date = (DateTime)pDate;

            string Time = fd(Grn.GetHour(date).ToString()) + ":" + fd(Grn.GetMinute(date).ToString());
            
            if      (pType.ToString() == "S") { DType = HttpContext.Current.Session["DateFormat"].ToString(); }
            else if (pType.ToString() == "G") { DType = "Gregorian"; } 
            else if (pType.ToString() == "H") { DType = "Hijri"; }

            if (DType == "Gregorian") { return fd(Grn.GetDayOfMonth(date).ToString()) + "/" + fd(Grn.GetMonth(date).ToString()) + "/" + fd(Grn.GetYear(date).ToString()) + " " + Time; }
            if (DType == "Hijri")     { return DateFun.GrnToHij(Convert.ToDateTime(pDate)).ToString() + " " + Time; }
        } 
        else { return null; }

        return null;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string GrdDisplayDate(object pDate)
    {
        string DateType = HttpContext.Current.Session["DateFormat"].ToString();

        if (pDate == DBNull.Value) { return ""; }
        
        DateTime date = (DateTime)pDate;
        
        if (DateType == "Gregorian") { return String.Format("{0:dd/MM/yyyy}", date); }
        else { return fd(Umq.GetYear(date).ToString()) + "/" + fd(Umq.GetMonth(date).ToString()) + "/" + fd(Umq.GetDayOfMonth(date).ToString()); }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string ToAnyFormat(string pDateType, object pDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        if (pDate != DBNull.Value)
        {
            if (pDateType == "Gregorian") { return (Convert.ToDateTime(pDate)).ToString("dd/MM/yyyy"); }
            else if (pDateType == "Hijri") { return GrnToHij(Convert.ToDateTime(pDate)).ToString(); }
            else { return string.Empty; }
        }
        else
        {
            return string.Empty;
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static int ConvertDateTimeToInt(string pDateType, string pDate)
    {
        string[] PartDate = pDate.Split('/'); //(General.ToAnyFormat(pDateType, pDate)).Split('/');
        string Y = PartDate[2];
        string M = PartDate[1];
        string D = PartDate[0];
        return (Convert.ToInt32(Y) * 10000) + (Convert.ToInt32(M) * 100) + Convert.ToInt32(D);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string SelectDateFormat(string pDateType, string pDate)
    {
        string strValue = "";
        if (pDateType == "Gregorian")  { strValue = strTodt(String.Format("{0:dd/MM/yyyy}", pDate)).ToShortDateString(); }
        else if (pDateType == "Hijri") { strValue = HijToGrn(pDate).ToShortDateString(); }

        return strValue;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string SaveDB(Char pDateType, string pDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        string strValue = "";
        string DType = "Gregorian";

        if      (pDateType == 'S') { DType = HttpContext.Current.Session["DateFormat"].ToString(); } 
        else if (pDateType == 'G') { DType = "Gregorian"; } 
        else if (pDateType == 'H') { DType = "Hijri"; }


        if      (DType == "Gregorian")  { strValue = strTodt(String.Format("{0:dd/MM/yyyy}", pDate)).ToShortDateString(); }
        else if (DType == "Hijri")      { strValue = HijToGrn(pDate).ToShortDateString(); }

        return strValue;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static DateTime GetGregDateTime(string pDate,Char pDateType, string Time)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        DateTime DValue = new DateTime();
        string DType = "Gregorian";

        if      (pDateType == 'S') { DType = HttpContext.Current.Session["DateFormat"].ToString(); } 
        else if (pDateType == 'G') { DType = "Gregorian"; } 
        else if (pDateType == 'H') { DType = "Hijri"; }


        if      (DType == "Gregorian")  { DValue = strToDatetime(String.Format("{0:dd/MM/yyyy}", pDate), Time); }
        else if (DType == "Hijri")      { DValue = HijToGrnDatetime(pDate, Time); }

        return DValue;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    public static DateTime strToDatetime(string pStrDate,string Time)
    {
        if (!string.IsNullOrEmpty(pStrDate))
        {
            string[] PartDate = pStrDate.Split('/');
            int day, month, year;
            Int32.TryParse(PartDate[0], out day);
            Int32.TryParse(PartDate[1], out month);
            Int32.TryParse(PartDate[2], out year);

            if (Time == "T") { return new DateTime(year, month, day, 23, 59, 59); }
            else /* (Time == "F") */ { return new DateTime(year, month, day, 0, 0, 0); }  
        }
        else { return new DateTime(); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public static DateTime HijToGrnDatetime(string pHdate,string Time)
    {      
        string[] PartDate = pHdate.Split('/');
        int day, month, year;
        Int32.TryParse(PartDate[0], out day);
        Int32.TryParse(PartDate[1], out month);
        Int32.TryParse(PartDate[2], out year);

        if (Time == "T") { return new DateTime(year, month, day, 23, 59, 59, Umq); }
        else /* (Time == "F") */ { return new DateTime(year, month, day, 0, 0, 0, Umq); } 
        
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string dtToStr_ddMMyyyy(DateTime pGdate)
    {
        int year  = pGdate.Year;
        int month = pGdate.Month;
        int day   = pGdate.Day;
        return fd(day.ToString()) + "/" + fd(month.ToString()) + "/" + fd(year.ToString());
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string FormatKAMCDate(string pDate)
    {
        if (!string.IsNullOrEmpty(pDate) && pDate.Length == 8)
        {
            string y = pDate.Substring(0, 4);
            string m = pDate.Substring(4, 2);
            string d = pDate.Substring(6, 2);
            
            return d + "/" + m + "/" + y;
        }
        return "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
