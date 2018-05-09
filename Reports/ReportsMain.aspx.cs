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
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Web;
using System.Data.SqlClient;
using Stimulsoft.Report.Dictionary;

public partial class Reports_ReportsMain : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ReportPro ProClass = new ReportPro();
    ReportSql SqlClass = new ReportSql();
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        //---Common Code ----------------------------------------------------------------- //
        string Type = (Request.QueryString["ID"] != null) ? Request.QueryString["ID"] : "";
        FormSession.FillSession("Reports",pageDiv);
        //---Common Code ----------------------------------------------------------------- //
        
        if (!IsPostBack)
        {
            if (!FormSession.PermUsr.Contains("Reports")) { Response.Redirect(@"~/Login.aspx"); }
            MainMasterPage.ShowTitel(General.Msg("Reports", "التقارير"));
            lblSelectedreport.Text = General.Msg("Please select Report","من فضلك اختر التقرير");
            
            FillReportsGroups();
            FillIssue();
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Reports Groups Events
    
    private void FillReportsGroups()
    {
        DataTable GDT = DBFun.FetchData(" SELECT * FROM ReportGroup ORDER BY RgpID ");
        if (!DBFun.IsNullOrEmpty(GDT))
        {
            foreach (DataRow DR in GDT.Rows)
            {
                if (FormSession.getPerm(new string[] { "Rep" + DR["RgpID"] })) 
                { 
                    ListItem _liReport = new ListItem(DR["RgpName" + FormSession.Language].ToString(), DR["RgpID"].ToString());
                    lstReportsGroups.Items.Add(_liReport);
                } 
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lstReportsGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear();

        lstReport.Items.Clear();
        lblSelectedreport.Text = General.Msg("Please select Report","من فضلك اختر التقرير");

        StiWebViewerFx1.Report = null;
        btnEditReport.Enabled = btnSetAsDefault.Enabled = btnViewreport.Enabled = false;
        pnlDateFromTo.Visible = pnlEmployee.Visible = false;
        pnlCreatedBy.Visible  = rvCreatedBy.Enabled = false;
        pnlPrintedBy.Visible  = rvPrintedBy.Enabled = false;
        pnlIssue.Visible      = rvIssue.Enabled     = false;

        UpdatePanel1.Update();

        if (lstReportsGroups.SelectedIndex > -1)
        {
            string RgpID = lstReportsGroups.SelectedValue;

            DataTable RepDT = DBFun.FetchData(" SELECT * FROM Report WHERE RgpID = " + RgpID + "");
            if (!DBFun.IsNullOrEmpty(RepDT))
            {
                FillReports(RgpID);
                FillCreatedBy(RgpID);
                FillPrintedBy(RgpID);
                FillCompany();
            }
        }

         UpdatePanel1.Update();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void FillReports(string RgpID)
    {
        DataTable RepDT = DBFun.FetchData(" SELECT * FROM Report WHERE RepVisible = 'True' AND RgpID = " + RgpID + "");
        if (!DBFun.IsNullOrEmpty(RepDT))
        {
            foreach (DataRow DR in RepDT.Rows)
            {
                ListItem _liReport = new ListItem(DR["RepName" + FormSession.Language].ToString(), DR["RepID"].ToString());
                lstReport.Items.Add(_liReport);
            }
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void FillPerm()
    {
        btnEditReport.Enabled   = FormSession.getPerm("ReptEdit");
        btnSetAsDefault.Enabled = FormSession.getPerm("RepSetToDef");
        btnExport.Enabled       = FormSession.getPerm("ReptUpload");
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void FillCreatedBy(string RgpID)
    {
        string Q = "";
        if      (RgpID == "4") { Q = "SELECT DISTINCT CreatedBy FROM CardInfoView         WHERE CreatedBy IS NOT NULL"; }
        else if (RgpID == "5") { Q = "SELECT DISTINCT CreatedBy FROM StickerInfoView      WHERE CreatedBy IS NOT NULL"; }
        else if (RgpID == "6") { Q = "SELECT DISTINCT CreatedBy FROM VisitorsCardInfoView WHERE CreatedBy IS NOT NULL"; }

        ddlCreatedBy.Items.Clear();

        DataTable DT = new DataTable();   
        DT = DBFun.FetchData(Q);

        if (!DBFun.IsNullOrEmpty(DT)) 
        { 
            FormCtrl.PopulateDDL(ddlCreatedBy, DT, "CreatedBy", "CreatedBy", General.Msg("-Select Created By-", "-اختر إنشاء بواسطة-")); 
            rvCreatedBy.InitialValue = ddlCreatedBy.Items[0].Text;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void FillPrintedBy(string RgpID)
    {
        string Q = "";
        if      (RgpID == "4") { Q = "SELECT DISTINCT PrintedBy FROM CardInfoView         WHERE PrintedBy IS NOT NULL"; }
        else if (RgpID == "5") { Q = "SELECT DISTINCT PrintedBy FROM StickerInfoView      WHERE PrintedBy IS NOT NULL"; }
        else if (RgpID == "6") { Q = "SELECT DISTINCT PrintedBy FROM VisitorsCardInfoView WHERE PrintedBy IS NOT NULL"; }

        ddlPrintedBy.Items.Clear();

        DataTable DT = new DataTable();   
        DT = DBFun.FetchData(Q);

        if (!DBFun.IsNullOrEmpty(DT)) 
        { 
            FormCtrl.PopulateDDL(ddlPrintedBy, DT, "PrintedBy", "PrintedBy", General.Msg("-Select Printed By-", "-اختر طباعة بواسطة-")); 
            rvPrintedBy.InitialValue = ddlPrintedBy.Items[0].Text;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void FillIssue()
    {
        DataTable DT = new DataTable();   
        DT = DBFun.FetchData("SELECT * FROM IssueState WHERE IsType = 'Card'");

        if (!DBFun.IsNullOrEmpty(DT)) 
        { 
            FormCtrl.PopulateDDL(ddlIssue, DT, General.Msg("IsNameEn","IsNameAr"), "IsID", General.Msg("-Select Issue-", "-اختر الإصدار-")); 
            rvIssue.InitialValue = ddlIssue.Items[0].Text;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void FillCompany()
    {
        DataTable DT = new DataTable();
        DT = DBFun.FetchData("SELECT * FROM Companies ");

        if (!DBFun.IsNullOrEmpty(DT))
        {
            FormCtrl.PopulateDDL2(ddlCompID, DT, General.Msg("CompNameEn", "CompNameAr"), "CompID", General.Msg("-All Company-", "-كل الشركات-"));
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool CheckBitWise(int panelPermission, int permission) { if ((panelPermission | permission) == panelPermission) { return true; } else { return false; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void lstReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear();

        StiWebViewerFx1.Report = null;
        btnEditReport.Enabled = btnSetAsDefault.Enabled = btnViewreport.Enabled = false;

        //UpdatePanel1.Update();

        if (lstReport.SelectedIndex > -1)
        {
            string RepID = lstReport.SelectedValue;
            string reportTitel = lstReport.SelectedItem.ToString();
            lblSelectedreport.Text = General.Msg("Report Selected : " + reportTitel,"التقرير المحدد : " + reportTitel );

            DataTable RepDT = DBFun.FetchData(" SELECT * FROM Report WHERE  RepID = '" + RepID + "'");
            if (!DBFun.IsNullOrEmpty(RepDT))
            {
                //RepDT.Rows[0]["RepID"].ToString()
                ViewState["RepID"]          = RepID;
                ViewState["ReportNameEn"]   = RepDT.Rows[0]["RepNameEn"].ToString();
                ViewState["ReportNameAr"]   = RepDT.Rows[0]["RepNameAr"].ToString();
                ViewState["pnlPermissions"] = Convert.ToInt32(RepDT.Rows[0]["RepPanels"]);

                int pnlPermissions    = Convert.ToInt32(RepDT.Rows[0]["RepPanels"]);
                pnlDateFromTo.Visible = CheckBitWise(pnlPermissions, 1);
                pnlEmployee.Visible   = CheckBitWise(pnlPermissions, 2);   
                pnlCreatedBy.Visible  = rvCreatedBy.Enabled  = CheckBitWise(pnlPermissions, 4);
                pnlPrintedBy.Visible  = rvPrintedBy.Enabled  = CheckBitWise(pnlPermissions, 8);
                pnlIssue.Visible      = rvIssue.Enabled      = CheckBitWise(pnlPermissions, 16);
                pnlEmpType.Visible    = CheckBitWise(pnlPermissions, 32);   

                FillPerm();

                btnViewreport.Enabled = true;
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Clear()
    {
        calStartDate.ClearDate();
        calEndDate.ClearDate();
        
        ddlIDSearch.SelectedIndex = -1;
        txtIDSearch.Text = "";
        ddlIssue.SelectedIndex = -1;
        ddlCreatedBy.SelectedIndex = ddlPrintedBy.SelectedIndex = -1;
        ddlEmpType.SelectedIndex = -1;
        ddlHasCard.SelectedIndex = -1;
        ddlCompID.SelectedIndex = -1;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnViewreport_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            ValidatorCollection ValidatorColl = Page.Validators;
            for (int k = 0; k < ValidatorColl.Count; k++)
            {
                if (!ValidatorColl[k].IsValid && !String.IsNullOrEmpty(ValidatorColl[k].ErrorMessage)) { vsView.ShowSummary = true; return; }
                vsView.ShowSummary = false;
            }
            return;
        }

        try
        {
            ShowReport();
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ShowReport()
    {
        StiWebViewerFx1.Report = null;

        if (lstReport.SelectedIndex < 0) { return; }
        string RepID = lstReport.SelectedValue;

        string RepTemp = "";
        DataTable RepDT = DBFun.FetchData(" SELECT * FROM Report WHERE RepID = '" + RepID + "'");
        if (!DBFun.IsNullOrEmpty(RepDT))
        {
            RepTemp = RepDT.Rows[0]["RepTemp" + FormSession.Language].ToString();
            string RepOrientation = RepDT.Rows[0]["RepOrientation"].ToString();
            ViewState["RepOrientation"] = RepDT.Rows[0]["RepOrientation"].ToString();
           
            StiReport StiRep = new StiReport();
            StiRep.LoadFromString(RepTemp);
            StiRep.Dictionary.Databases.Clear();
            StiRep.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Connection", General.ConnString));
            StiRep.GetSubReport += new StiGetSubReportEventHandler(rep_GetSubReport);
            StiRep.Dictionary.Synchronize();
            StiRep.Compile();

            /////// Fill Parameters to Report
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (pnlDateFromTo.Visible) 
            { 
                StiRep["ParamDateFrom"] = DateFun.GetGregDateTime(calStartDate.getDate(),'S',"F");
                StiRep["ParamDateTo"]   = DateFun.GetGregDateTime(calEndDate.getDate(), 'S',"T");
            }

            if (pnlEmployee.Visible)  { StiRep["EmpID"]     = ViewState["EmpID"].ToString(); }
            if (pnlCreatedBy.Visible) { StiRep["CreatedBy"] = ddlCreatedBy.SelectedValue; }
            if (pnlPrintedBy.Visible) { StiRep["PrintedBy"] = ddlPrintedBy.SelectedValue; }
            if (pnlIssue.Visible)     { StiRep["IssueID"]   = Convert.ToInt32(ddlIssue.SelectedValue); }
            if (pnlEmpType.Visible) 
            { 
                StiRep["EmpType"] = ddlEmpType.SelectedValue;
                StiRep["CompID"]  = ddlCompID.SelectedValue;
                StiRep["HasCard"] = ddlHasCard.SelectedValue;
            }

            StiRep["LoginID"] = FormSession.LoginUsr;

            StiRep.Render();
            StiWebViewerFx1.Report = StiRep;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void rep_GetSubReport(object sender, StiGetSubReportEventArgs e)
    {
        string RepOrientation = ViewState["RepOrientation"].ToString();
             
        StiReport HRep = new StiReport();
       
        DataTable HDT = DBFun.FetchData(" SELECT * FROM Report WHERE RepType = 'Header' AND RepOrientation ='" + RepOrientation + "'");
        if (!DBFun.IsNullOrEmpty(HDT))
        {
            string RepHeader = HDT.Rows[0]["RepTemp" + FormSession.Language].ToString();
            
            if (e.SubReportName == "SubReport1") { HRep.LoadFromString(RepHeader); }

            HRep.Dictionary.Databases.Clear();
            HRep.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("Connection", General.ConnString));
            e.Report = HRep;  
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpType.SelectedValue == "Con") { divCompany.Visible = true; }
        else { divCompany.Visible = false;  }
    }

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Report Events
    
    protected void btnSetAsDefault_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstReport.SelectedIndex < 0) { return; }
            string RepID = lstReport.SelectedValue;

            DataTable RepDT = DBFun.FetchData(" SELECT * FROM Report WHERE RepID = '" + RepID + "'");
            if (!DBFun.IsNullOrEmpty(RepDT))
            {
                string repStr = RepDT.Rows[0]["RepTempDef" + FormSession.Language].ToString();

                ProClass.RepID      = RepID;
                ProClass.RepTemp    = repStr;
                ProClass.Lang       = FormSession.Language;
                ProClass.ModifiedBy = FormSession.LoginUsr;

                SqlClass.UpdateTemplate(ProClass);
            }
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnUploadReport_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    string type = fileUpload.PostedFile.ContentType.ToString();
        //    fileUpload.SaveAs(Server.MapPath(@"../Reports/EditedReports/") + fileUpload.FileName);
        //    fileUpload.SaveAs(Server.MapPath(@"../Reports/DefaultReports/") + fileUpload.FileName);
        //    Server.Transfer("Reports.aspx");
        //}
        //catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnExport_Click(object sender, EventArgs e)
    {
        //if (lstReport.SelectedIndex > -1) { reportName = lstReport.SelectedValue; } 
        //else if (lstHeaderReport.SelectedIndex > -1) { reportName = lstHeaderReport.SelectedValue; } 
        //else { return; }

        //string sourceFilename = string.Empty;
        //string destinationFilename = string.Empty;
        //sourceFilename = Server.MapPath(@"..\Reports\EditedReports\" + FormSession.Language + "\\") + reportName + "";
        //destinationFilename = "attachment; filename=" + reportName;
        //Response.ContentType = "text/plain";
        //Response.AppendHeader("Content-Disposition", destinationFilename);
        //Response.TransmitFile(sourceFilename);
        //Response.End();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
    protected void btnEditReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstReport.SelectedIndex < 0) { return; }  //ShowMsg("Please Select Report to edit it", "رجاء حدد تقرير للتعديل");
        
            string RepID = lstReport.SelectedValue;
            DataTable RepDT = DBFun.FetchData(" SELECT * FROM Report WHERE RepID = '" + RepID + "'");
            if (!DBFun.IsNullOrEmpty(RepDT))
            {
                string repStr = RepDT.Rows[0]["RepTemp" + FormSession.Language].ToString();
                if (string.IsNullOrEmpty(repStr)) { return; }
                
                StiReport report = new StiReport();
                report.LoadFromString(repStr);
                report.Dictionary.Databases.Clear();
                report.Dictionary.Databases.Add(new StiSqlDatabase("Connection", General.ConnString));
                report.Dictionary.Synchronize();
                report.Compile();
                StiWebDesigner1.Design(report);
                ////this.InvokeRefreshPreview();
            }
        }
        catch (Exception ex) { }
    }
    
    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region StiWeb Events
    
    protected void StiWebDesigner1_PreInit(object sender, StiWebDesigner.StiPreInitEventArgs e)
    {
        e.WebDesigner.Localization =  FormSession.Language;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void StiWebViewerFx1_PreInit(object sender, Stimulsoft.Report.WebFx.StiWebViewerFx.StiPreInitEventArgs e)
    {
        StiWebViewerFx1.Localization = FormSession.Language;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void StiWebDesigner1_SaveReport(object sender, StiWebDesigner.StiSaveReportEventArgs e)
    {
        try
        {
            if (lstReport.SelectedIndex < 0) { return; }  //ShowMsg("Please Select Report to edit it", "رجاء حدد تقرير للتعديل");
        
            string RepID = lstReport.SelectedValue;
            StiReport report = e.Report;
            string repStr    = e.Report.SaveToString().ToString();

            ProClass.RepID      = RepID;
            ProClass.RepTemp    = repStr;
            ProClass.Lang       = FormSession.Language;
            ProClass.ModifiedBy = FormSession.LoginUsr;

            SqlClass.UpdateTemplate(ProClass);
            //ShowMsg("Report Updated Successfully", "تم تحديث التقرير بنجاح");
        }
        catch (Exception e1)
        {
            //ShowMsg("Transaction failed to commit please contact your administrator", "النظام غير قادر على رفع التقرير, الرجاء الاتصال بمدير النظام");
        }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events
    
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Date_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (pnlDateFromTo.Visible)
            {
                if (source.Equals(cvStartDate)) { if (string.IsNullOrEmpty(calStartDate.getDate())) { e.IsValid = false; } else { e.IsValid = true; } }
                if (source.Equals(cvEndDate))   { if (string.IsNullOrEmpty(calEndDate.getDate()))   { e.IsValid = false; } else { e.IsValid = true; } }
                if (source.Equals(cvCompareDates))
                {
                    try
                    {
                        if (!String.IsNullOrEmpty(calStartDate.getDate()) && !String.IsNullOrEmpty(calEndDate.getDate()))
                        {
                            int iStartDate = DateFun.ConvertDateTimeToInt(FormSession.DateType, calStartDate.getDate());
                            int iEndDate   = DateFun.ConvertDateTimeToInt(FormSession.DateType, calEndDate.getDate());
                            if (iStartDate > iEndDate) { e.IsValid = false; } else { e.IsValid = true; }
                        }
                    }
                    catch
                    {
                        e.IsValid = false;
                    }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void IDSearch_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvIDSearch) && pnlEmployee.Visible)
            {
                if (string.IsNullOrEmpty(txtIDSearch.Text))
                {
                    General.ValidMsg(this, ref cvIDSearch, false, "You must enter a value in the search text", "يجب إدخال قيمة في مربع البحث");
                    e.IsValid = false;
                    return;
                }
                else
                {
                    DataTable DT = new DataTable();
                    if      (ddlIDSearch.SelectedValue == "EmpID")      { DT = DBFun.FetchData("SELECT EmpID FROM EmployeeMaster WHERE EmpID     = '" + txtIDSearch.Text + "'"); }
                    else if (ddlIDSearch.SelectedValue == "EmpNameEn")  { DT = DBFun.FetchData("SELECT EmpID FROM EmployeeMaster WHERE EmpNameEn = '" + txtIDSearch.Text + "'"); }
                    else if (ddlIDSearch.SelectedValue == "EmpNameAr")  { DT = DBFun.FetchData("SELECT EmpID FROM EmployeeMaster WHERE EmpNameAr = '" + txtIDSearch.Text + "'"); }

                    ViewState["EmpID"] = "";
                    if (DBFun.IsNullOrEmpty(DT))
                    {
                        MessageFun.ValidMsg(this, ref cvIDSearch, true, General.Msg("Employee ID does not exist", "رقم الموظف غير موجود"));
                        e.IsValid = false;
                        return;
                    }
                    else
                    {
                        ViewState["EmpID"] = DT.Rows[0]["EmpID"].ToString();
                    }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
}