using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ReportPro
{
	private string _DateType;
    public  string DateType { get { return _DateType; } set { _DateType = value; } }

    private string _RepID;
    public string RepID { get { return _RepID; } set { _RepID = value; } }

    private string _RepTemp;
    public string RepTemp { get { return _RepTemp; } set { _RepTemp = value; } }

    private string _Lang;
    public string Lang { get { return _Lang; } set { _Lang = value; } }

    private string _CreatedBy;
    public string CreatedBy { get { return _CreatedBy; } set { _CreatedBy = value; } }

    private string _CreatedDate;
    public string CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }

    private string _ModifiedBy;
    public string ModifiedBy { get { return _ModifiedBy; } set { _ModifiedBy = value; } }

    private string _ModifiedDate;
    public string ModifiedDate { get { return _ModifiedDate; } set { _ModifiedDate = value; } }
}