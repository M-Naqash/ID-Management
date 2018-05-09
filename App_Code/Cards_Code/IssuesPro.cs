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

public class IssuesPro
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _IsID;
    public string IsID { get { return _IsID; } set { _IsID = value; } }

    private string _IsType;
    public string IsType { get { return _IsType; } set { _IsType = value; } }

    private string _IsNameEn;
    public string IsNameEn { get { return _IsNameEn; } set { _IsNameEn = value; } }

    private string _IsNameAr;
    public string IsNameAr { get { return _IsNameAr; } set { _IsNameAr = value; } }

    private string _IsDescription;
    public string IsDescription { get { return _IsDescription; } set { _IsDescription = value; } }

    private string _IsRepeat;
    public string IsRepeat { get { return _IsRepeat; } set { _IsRepeat = value; } }

    private string _ISCondition;
    public string ISCondition
    { get { return _ISCondition; } set { _ISCondition = value; } }

    private string _ConditionID;
    public string ConditionID { get { return _ConditionID; } set { _ConditionID = value; } }

    private string _ConditionName;
    public string ConditionName { get { return _ConditionName; } set { _ConditionName = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }

    private string _TransactionDate;
    public string TransactionDate { get { return _TransactionDate; } set { _TransactionDate = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
