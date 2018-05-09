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

public class AppUsersPro
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _DateType;
    public string  DateType { get { return _DateType; } set { _DateType = value; } }

    protected string _UsrLoginID;
    public string UsrLoginID { get { return _UsrLoginID; } set { _UsrLoginID = value; } }

    private string _UsrPassword;
    public string UsrPassword { get { return _UsrPassword; } set { _UsrPassword = value; } }

    private string _UsrFullName;
    public string UsrFullName { get { return _UsrFullName; } set { _UsrFullName = value; } }

    private string _UsrStartDate;
    public string UsrStartDate { get { return _UsrStartDate; } set { _UsrStartDate = value; } }

    private Char _UsrStartDateType;
    public Char UsrStartDateType { get { return _UsrStartDateType; } set { _UsrStartDateType = value; } }

    private string _UsrExpiryDate;
    public string UsrExpiryDate { get { return _UsrExpiryDate; } set { _UsrExpiryDate = value; } }

    private Char _UsrExpiryDateType;
    public Char UsrExpiryDateType { get { return _UsrExpiryDateType; } set { _UsrExpiryDateType = value; } }

    private bool _UsrStatus;
    public bool UsrStatus { get { return _UsrStatus; } set { _UsrStatus = value; } }

    private string _UsrPermission;
    public string UsrPermission { get { return _UsrPermission; } set { _UsrPermission = value; } }

    private string _UsrLanguage;
    public string UsrLanguage { get { return _UsrLanguage; } set { _UsrLanguage = value; } }

    private string _UsrEmailID;
    public string UsrEmailID { get { return _UsrEmailID; } set { _UsrEmailID = value; } }

    private string _UsrEmpID;
    public string UsrEmpID { get { return _UsrEmpID; } set { _UsrEmpID = value; } }
    
    private string _UsrDescription;
    public string UsrDescription { get { return _UsrDescription; } set { _UsrDescription = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string _RoleID;
    public    string RoleID { get { return _RoleID; } set { _RoleID = value; } }
    
    private string _RoleNameEn;
    public   string RoleNameEn { get { return _RoleNameEn; } set { _RoleNameEn = value; } }

    private string _RoleNameAr;
    public   string RoleNameAr { get { return _RoleNameAr; } set { _RoleNameAr = value; } }

    private string _RolePermissions;
    public string RolePermissions { get { return _RolePermissions; } set { _RolePermissions = value; } }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }

    private string _TransactionDate;
    public string TransactionDate { get { return _TransactionDate; } set { _TransactionDate = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
