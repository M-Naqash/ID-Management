using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Data;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class AutoComplete : System.Web.Services.WebService 
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    string departmentList = string.Empty;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public AutoComplete() { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod(true)]
    public string[] GetEmpIDList(string prefixText, int count)
    {
        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT EmpID FROM EmployeeMaster WHERE EmpID LIKE '%" + prefixText + "%'");

        List<string> items = new List<string>(count);
        DataTable PDT = DBFun.FetchData(Q.ToString());
        if (!DBFun.IsNullOrEmpty(PDT)) { for (int i = 0; i < PDT.Rows.Count; i++) { items.Add(PDT.Rows[i][0].ToString()); } }

        return items.ToArray();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod(true)]
    public string[] GetEmpNameArList(string prefixText, int count)
    {
        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT EmpNameAr FROM EmployeeMaster WHERE EmpNameAr IS NOT NULL AND EmpNameAr LIKE '%" + prefixText + "%'");

        List<string> items = new List<string>(count);
        DataTable PDT = DBFun.FetchData(Q.ToString());
        if (!DBFun.IsNullOrEmpty(PDT)) { for (int i = 0; i < PDT.Rows.Count; i++) { items.Add(PDT.Rows[i][0].ToString()); } }

        return items.ToArray();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    [WebMethod(true)]
    public string[] GetEmpNameEnList(string prefixText, int count)
    {
        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT EmpNameEn FROM EmployeeMaster WHERE EmpNameEn IS NOT NULL AND EmpNameEn LIKE '%" + prefixText + "%'");

        List<string> items = new List<string>(count);
        DataTable PDT = DBFun.FetchData(Q.ToString());
        if (!DBFun.IsNullOrEmpty(PDT)) { for (int i = 0; i < PDT.Rows.Count; i++) { items.Add(PDT.Rows[i][0].ToString()); } }

        return items.ToArray();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    [WebMethod(true)]
    public string[] GetEmpNationalIDList(string prefixText, int count)
    {
        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT EmpNationalID FROM EmployeeMaster WHERE EmpNationalID IS NOT NULL AND EmpNationalID LIKE '%" + prefixText + "%'");

        List<string> items = new List<string>(count);
        DataTable PDT = DBFun.FetchData(Q.ToString());
        if (!DBFun.IsNullOrEmpty(PDT)) { for (int i = 0; i < PDT.Rows.Count; i++) { items.Add(PDT.Rows[i][0].ToString()); } }

        return items.ToArray();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    [WebMethod(true)]
    public string[] GetEmpMobileNoList(string prefixText, int count)
    {
        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT EmpMobileNo FROM EmployeeMaster WHERE EmpMobileNo IS NOT NULL AND EmpMobileNo LIKE '%" + prefixText + "%'");

        List<string> items = new List<string>(count);
        DataTable PDT = DBFun.FetchData(Q.ToString());
        if (!DBFun.IsNullOrEmpty(PDT)) { for (int i = 0; i < PDT.Rows.Count; i++) { items.Add(PDT.Rows[i][0].ToString()); } }

        return items.ToArray();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public string[] GetVisIDList(string prefixText, int count)
    {
        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT DISTINCT VisIdentityNo FROM VisitorsCard WHERE VisIdentityNo LIKE '%" + prefixText + "%'");

        List<string> items = new List<string>(count);
        DataTable PDT = DBFun.FetchData(Q.ToString());
        if (!DBFun.IsNullOrEmpty(PDT)) { for (int i = 0; i < PDT.Rows.Count; i++) { items.Add(PDT.Rows[i][0].ToString()); } }

        return items.ToArray();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [WebMethod(true)]
    public string[] GetVisNameArList(string prefixText, int count)
    {
        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT DISTINCT VisNameAr FROM VisitorsCard WHERE VisNameAr IS NOT NULL AND VisNameAr LIKE '%" + prefixText + "%'");

        List<string> items = new List<string>(count);
        DataTable PDT = DBFun.FetchData(Q.ToString());
        if (!DBFun.IsNullOrEmpty(PDT)) { for (int i = 0; i < PDT.Rows.Count; i++) { items.Add(PDT.Rows[i][0].ToString()); } }

        return items.ToArray();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    [WebMethod(true)]
    public string[] GetVisNameEnList(string prefixText, int count)
    {
        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT DISTINCT VisNameEn FROM VisitorsCard WHERE VisNameEn IS NOT NULL AND VisNameEn LIKE '%" + prefixText + "%'");

        List<string> items = new List<string>(count);
        DataTable PDT = DBFun.FetchData(Q.ToString());
        if (!DBFun.IsNullOrEmpty(PDT)) { for (int i = 0; i < PDT.Rows.Count; i++) { items.Add(PDT.Rows[i][0].ToString()); } }

        return items.ToArray();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    [WebMethod(true)]
    public string[] GetVisMobileNoList(string prefixText, int count)
    {
        StringBuilder Q = new StringBuilder();
        Q.Append(" SELECT DISTINCT VisMobileNo FROM VisitorsCard WHERE VisMobileNo IS NOT NULL AND VisMobileNo LIKE '%" + prefixText + "%'");

        List<string> items = new List<string>(count);
        DataTable PDT = DBFun.FetchData(Q.ToString());
        if (!DBFun.IsNullOrEmpty(PDT)) { for (int i = 0; i < PDT.Rows.Count; i++) { items.Add(PDT.Rows[i][0].ToString()); } }

        return items.ToArray();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
}
