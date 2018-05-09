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

public class ApplicationSetupPro
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _AppCompany;
    public string AppCompany { get { return _AppCompany; } set { _AppCompany = value; } }

    private string _AppDisplay;
    public string AppDisplay { get { return _AppDisplay; } set { _AppDisplay = value; } }

    private string _AppAddress1;
    public string AppAddress1 { get { return _AppAddress1; } set { _AppAddress1 = value; } }

    private string _AppAddress2;
    public string AppAddress2 { get { return _AppAddress2; } set { _AppAddress2 = value; } }

    private string _AppCity;
    public string AppCity { get { return _AppCity; } set { _AppCity = value; } }

    private string _AppCountry;
    public string AppCountry { get { return _AppCountry; } set { _AppCountry = value; } }

    private string _AppPOBox;
    public string AppPOBox { get { return _AppPOBox; } set { _AppPOBox = value; } }

    private string _AppTelNo1;
    public string AppTelNo1 { get { return _AppTelNo1; } set { _AppTelNo1 = value; } }

    private string _AppTelNo2;
    public string AppTelNo2 { get { return _AppTelNo2; } set { _AppTelNo2 = value; } }

    private string _AppFax;
    public string AppFax { get { return _AppFax; } set { _AppFax = value; } }

    private string _AppUrl;
    public string AppUrl { get { return _AppUrl; } set { _AppUrl = value; } }

    private string _AppEmail;
    public string AppEmail { get { return _AppEmail; } set { _AppEmail = value; } }

    private string _AppCalendar;
    public string AppCalendar { get { return _AppCalendar; } set { _AppCalendar = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private byte[] _AppLogo;
    public byte[] AppLogo { get { return _AppLogo; } set { _AppLogo = value; } }

    private string _AppLogoImageType;
    public string AppLogoImageType { get { return _AppLogoImageType; } set { _AppLogoImageType = value; } }

    private int _AppLogoImageLength;
    public int AppLogoImageLength { get { return _AppLogoImageLength; } set { _AppLogoImageLength = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }

    private string _TransactionDate;
    public string TransactionDate { get { return _TransactionDate; } set { _TransactionDate = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   
}
