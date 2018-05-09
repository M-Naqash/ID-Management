using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;

public partial class Visitors_ImportVisitors : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DataSet ExcelDataSet = new DataSet();
    VisitorsPro ProClass = new VisitorsPro();
    VisitorsSql SqlClass = new VisitorsSql();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //---Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Visitors",pageDiv);
            //---Common Code ----------------------------------------------------------------- //

            if (!IsPostBack)
            {
                MainMasterPage.ShowTitel(General.Msg("Import from Excel File", "الاستيراد من ملف اكسل"));

                if (!FormSession.getPerm(new string[] { "ImpVis" })) { Response.Redirect(@"~/Login.aspx"); }
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {    
            string filePath = ServerMapPath(@"../Import/Visitors/VisitorsBlank.xls");

            HttpResponse res = GetHttpResponse();
            res.Clear();
            res.AppendHeader("content-disposition", "attachment; filename=VisitorsBlank.xls");
            res.ContentType = "application/x-msexcel";
            // res.ContentType = "application/octet-stream";
            res.WriteFile(filePath);
            res.Flush();
            res.End();
        }
        catch (Exception ex) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (fudFilePath.HasFile)
            {
                string OldName = fudFilePath.FileName;
                string[] nameArr = OldName.Split('.');
                string FName = nameArr[0];
                string FType = nameArr[1];
                
                string EmpType = "Visitors";
                string FileName = FormSession.LoginUsr + "-" + EmpType + "." + FType;
                fudFilePath.SaveAs(Server.MapPath(@"../" + EmpType + "/") + FileName);      
                
                string Path = Server.MapPath(@"../" + EmpType + "/") + FileName;

                string ExcelConnectionString = getExcelConnectionString(Path);
                if (!string.IsNullOrEmpty(ExcelConnectionString))
                {
                    string SheetName = GetExcelSheetNames(ExcelConnectionString);
                    FillExcelDataSet(ExcelConnectionString, SheetName);

                    FillDB();

                    if (File.Exists(Path)) { File.Delete(Path); }
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg("succsss Import", "تمت عملية الاستيراد"));
                }
            }
        }
        catch (Exception ex)
        {
            MessageFun.ShowAdminMsg(this, ex.ToString());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static string ServerMapPath(string path) { return HttpContext.Current.Server.MapPath(path); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static HttpResponse GetHttpResponse() { return HttpContext.Current.Response; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ClearUI() 
    {
        txtAddCount.Text       = "";
        txtErrCount.Text       = "";
        txtBlackListCount.Text = "";
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool FindBlackList(string pID)
    {
        try
        {
            DataTable DT = DBFun.FetchData("SELECT BlaID FROM BlackList WHERE BlaIdentityNo = '" + pID + "'");
            if (!DBFun.IsNullOrEmpty(DT)) { return true; } else { return false; }
        }
        catch (Exception e1)
        {
            DBFun.InsertError(FormSession.PageName, "FindBlackList");
            return false;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Excell

    public string getExcelConnectionString(string pFile)
    {
        return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pFile + ";Extended Properties=\"Excel 12.0;HDR=Yes;ImportMixedTypes=Text \"";

        //if (pFile.EndsWith(".xls")) { return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pFile + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1;ImportMixedTypes=Text \""; }
        //else if (pFile.EndsWith(".xlsx")) { return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pFile + ";Extended Properties=\"Excel 12.0;HDR=Yes;ImportMixedTypes=Text \""; }
        //else { return ""; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string GetExcelSheetNames(string pExcelConnectionString)
    {
        OleDbConnection oledbConn = new OleDbConnection(pExcelConnectionString);
        DataTable SchemaSheetNames = null;

        oledbConn.Open();
        SchemaSheetNames = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null); // Get the data table containg the schema guid.
        if (SchemaSheetNames == null) { oledbConn.Close(); oledbConn.Dispose(); return ""; }
        else
        {
            String[] excelSheets = new String[SchemaSheetNames.Rows.Count];
            int iCount = 0;
            foreach (DataRow row in SchemaSheetNames.Rows)  // Add the sheet name to the string array.
            {
                excelSheets[iCount] = row["TABLE_NAME"].ToString();
                iCount += 1;
            }

            oledbConn.Close();
            oledbConn.Dispose();
            return excelSheets[0];
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillExcelDataSet(string pExcelConnectionString, string pSheetName)
    {
        OleDbConnection oledbConn = new OleDbConnection(pExcelConnectionString);
        oledbConn.Open();
        OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + pSheetName + "]", oledbConn);
        OleDbDataAdapter oleda = new OleDbDataAdapter();
        oleda.SelectCommand = cmd;
        ExcelDataSet = new DataSet();
        oleda.Fill(ExcelDataSet, "ExcelDataTable");
        oledbConn.Close();
        oledbConn.Dispose();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillDB()
    {
        string[] ExcelFiled = new string[] {"IdentityNo","NameAr","NameEn","MobileNo","StartDate","ExpiryDate","Region1","Region2","Region3","Region4" ,"Region5","Region6","Region7","Region8","Region9","Description"};

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        ProClass.DateType      = FormSession.DateType;
        ProClass.TransactionBy = FormSession.LoginUsr;
        ProClass.CardStatus = "0";
        ProClass.isPrinted  = false;
        ProClass.TmpID      = FindTmp();
        if (ProClass.TmpID == "0") 
        { 
            MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg("No one has provided a Template for this type of card, you can not import data", "لم تتم إضافة نموذج لهذا النوع من البطاقات,لا يمكن استيراد البيانات"));
            return; 
        }

        int Count  = 0;
        int ICount = 0;
        int ECount = 0;
        int BCount = 0;

        foreach (DataRow DR in ExcelDataSet.Tables[0].Rows)
        {
            if (DR[ExcelFiled[0]] != DBNull.Value)
            {
                ProClass.VisIdentityNo = DR[ExcelFiled[0]].ToString();
                ProClass.VisNameAr     = DR[ExcelFiled[1]].ToString();
                if (!string.IsNullOrEmpty(DR[ExcelFiled[2]].ToString())) { ProClass.VisNameEn = DR[ExcelFiled[2]].ToString(); }
                if (!string.IsNullOrEmpty(DR[ExcelFiled[3]].ToString())) { ProClass.VisMobileNo = DR[ExcelFiled[3]].ToString(); }
            
                ProClass.StartDate  = DR[ExcelFiled[4]].ToString();
                ProClass.ExpiryDate = DR[ExcelFiled[5]].ToString();

                ProClass.VisRegion1 = Convert.ToBoolean(Convert.ToInt32(DR[ExcelFiled[6]].ToString()));
                ProClass.VisRegion2 = Convert.ToBoolean(Convert.ToInt32(DR[ExcelFiled[7]].ToString()));
                ProClass.VisRegion3 = Convert.ToBoolean(Convert.ToInt32(DR[ExcelFiled[8]].ToString()));
                ProClass.VisRegion4 = Convert.ToBoolean(Convert.ToInt32(DR[ExcelFiled[9]].ToString()));
                ProClass.VisRegion5 = Convert.ToBoolean(Convert.ToInt32(DR[ExcelFiled[10]].ToString()));
                ProClass.VisRegion6 = Convert.ToBoolean(Convert.ToInt32(DR[ExcelFiled[11]].ToString()));
                ProClass.VisRegion7 = Convert.ToBoolean(Convert.ToInt32(DR[ExcelFiled[12]].ToString()));
                ProClass.VisRegion8 = Convert.ToBoolean(Convert.ToInt32(DR[ExcelFiled[13]].ToString()));
                ProClass.VisRegion9 = Convert.ToBoolean(Convert.ToInt32(DR[ExcelFiled[14]].ToString()));

                if (!string.IsNullOrEmpty(DR[ExcelFiled[15]].ToString())) { ProClass.Description = DR[ExcelFiled[15]].ToString(); }
                ProClass.CopiesCount = (Convert.ToInt16(FindCount(ProClass.VisIdentityNo)) + 1).ToString();
                
                try
                {
                    bool isBlakList = FindBlackList(ProClass.VisIdentityNo);
                    if (isBlakList)
                    {
                        BCount += 1;
                    }
                    else
                    {
                        SqlClass.Insert(ProClass);
                        ICount += 1;
                    }
                }
                catch (Exception ex) { ECount += 1; }
            }

            Count += 1;
        }  

        txtAddCount.Text       = ICount.ToString();
        txtErrCount.Text       = ECount.ToString();
        txtBlackListCount.Text = BCount.ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string FindCount(string pID)
    {
        try
        {
            string count = "0";

            DataTable CountDT = DBFun.FetchData("SELECT COUNT(VisCardID) count FROM VisitorsCard WHERE isPrinted = 'True' AND VisIdentityNo = '" + pID + "'");
            if (DBFun.IsNullOrEmpty(CountDT)) { }
            else
            {
                if (Convert.ToInt32(CountDT.Rows[0]["count"]) > 0) { count = CountDT.Rows[0]["count"].ToString(); }
            }

            return count;
        }
        catch (Exception e1)
        {
            DBFun.InsertError(FormSession.PageName, "PopulateUI");
            return "0";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string FindTmp()
    {
        try
        {
            string tmp = "0";

            DataTable TmpDT = DBFun.FetchData("SELECT * FROM CardTemplate WHERE TmpType = 'VCard'");
            if (!DBFun.IsNullOrEmpty(TmpDT)) 
            { 
                tmp = TmpDT.Rows[0]["TmpID"].ToString(); 
            }

            return tmp;
        }
        catch (Exception e1)
        {
            DBFun.InsertError(FormSession.PageName, "PopulateUI");
            return "0";
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events
    
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
}