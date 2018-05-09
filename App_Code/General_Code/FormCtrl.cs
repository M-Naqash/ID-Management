using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;

public class FormCtrl
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public FormCtrl() { }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void Clean(Control PCtrl)
    {
        foreach (Control ctrl in PCtrl.Controls)
        {
            if (ctrl is TextBox) { ((TextBox)ctrl).Text = String.Empty; }
            if (ctrl is DropDownList) { ((DropDownList)ctrl).SelectedIndex = -1; }
            if (ctrl is RadioButtonList) { ((RadioButtonList)ctrl).SelectedIndex = -1; }
            if (ctrl is GridView) { 
                ((GridView)ctrl).DataSource = new DataTable(); 
                ((GridView)ctrl).DataBind();
            }

            if (ctrl.Controls.Count > 0) { Clean(ctrl); }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public bool PopulateDDL(DropDownList ddl, DataTable dt, string Text, string Value, string Msg)
    {
        try
        {
            if (IsNullOrEmpty(dt)) { return false; }

            ddl.DataSource = null;
            ddl.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem ls = new ListItem(dt.Rows[i][Text].ToString(), dt.Rows[i][Value].ToString());
                ddl.Items.Add(ls);
            }
            
            ListItem lsMsg = new ListItem(Msg,Msg);
            ddl.Items.Insert(0, lsMsg);

            return true;
        }
        catch (Exception e1) { throw e1; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public bool PopulateDDL2(DropDownList ddl, DataTable dt, string Text, string Value, string Msg)
    {
        try
        {
            if (IsNullOrEmpty(dt)) { return false; }

            ddl.DataSource = null;
            ddl.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem ls = new ListItem(dt.Rows[i][Text].ToString(), dt.Rows[i][Value].ToString());
                ddl.Items.Add(ls);
            }
            
            ListItem lsMsg = new ListItem(Msg,"0");
            ddl.Items.Insert(0, lsMsg);

            return true;
        }
        catch (Exception e1) { throw e1; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public bool PopulateDDLEmp(DropDownList ddl, DataTable dt, string Text1, string Text2, string Value, string Msg)
    {
        try
        {
            if (IsNullOrEmpty(dt)) { return false; }

            ddl.DataSource = null;
            ddl.Items.Clear();        

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem ls = new ListItem(dt.Rows[i][Text1].ToString() + "-" + dt.Rows[i][Text2].ToString() , dt.Rows[i][Value].ToString());
                ddl.Items.Add(ls);
            }

            ListItem lsMsg = new ListItem(Msg,Msg);
            ddl.Items.Insert(0, lsMsg);
            return true;
        }
        catch (Exception e1) { throw e1; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public bool PopulateDDLCollegeOfficer(DropDownList ddl, DataTable dt, string Text1, string Text2, string Value, string Msg)
    {
        try
        {
            if (IsNullOrEmpty(dt)) { return false; }

            ddl.DataSource = null;
            ddl.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem ls = new ListItem(General.DisplayCollegeType(dt.Rows[i][Text1]) + "\\ " + dt.Rows[i][Text2].ToString(), dt.Rows[i][Value].ToString());
                ddl.Items.Add(ls);
            }

            ListItem lsMsg = new ListItem(Msg, Msg);
            ddl.Items.Insert(0, lsMsg);

            return true;
        }
        catch (Exception e1) { throw e1; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public bool IsNullOrEmpty(DataTable dt)
    {
        if (dt == null) { return true; }
        if (dt.Rows.Count == 0) { return true; }
        return false;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public void FillGridEmpty(ref GridView pGrid,int pHeight,string pMsgEn,string pMsgAr)
    {
        try
        {
            DataTable dt = new DataTable();
            DataColumn column;
            String columnName = string.Empty;
            for (int i = 0; i < pGrid.Columns.Count; i++) //to get column in gridview
            {
                DataControlField field = pGrid.Columns[i];
                BoundField bfield = field as BoundField;
                if (bfield != null) { columnName = bfield.DataField; /*Get DataFiled column*/ } else { columnName = field.SortExpression;/*Get TemplateFiled column*/ }
                column = new DataColumn(columnName);
                column.AllowDBNull = true;
                dt.Columns.Add(column);
            } 
            dt.Rows.Add(dt.NewRow());
            pGrid.DataSource = dt;
            pGrid.DataBind();
            int totalcolums = pGrid.Rows[0].Cells.Count;
            pGrid.Rows[0].Cells.Clear();
            pGrid.Rows[0].Cells.Add(new TableCell());
            pGrid.Rows[0].Cells[0].ColumnSpan = totalcolums;
            if (HttpContext.Current.Session["Language"].ToString() == "Ar") { pGrid.Rows[0].Cells[0].Text = pMsgAr; } else { pGrid.Rows[0].Cells[0].Text = pMsgEn; }
            pGrid.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            pGrid.Rows[0].Cells[0].VerticalAlign = VerticalAlign.Middle;
            pGrid.Rows[0].Cells[0].Height = pHeight;
            pGrid.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
            pGrid.Rows[0].Cells[0].Font.Size = 12;
        }
        catch (Exception e1) { }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public void GridRowColor(GridViewRow gr)
    {
        try
        {
            if (gr.RowType == DataControlRowType.DataRow)
            {
                gr.Attributes.Add("onmouseover", "mouseout('alt_row_highlight',this);");
                gr.Attributes.Add("onmouseout", "mouseover('alt_row_nohighlight',this);");
                gr.Attributes.Add("onmousemove", "mousemove('alt_row_nohighlight',this);");
            }
        }
        catch (Exception EX) { }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static void RefreshGridEmpty(ref GridView gv,int pHeight,string pMsgEn,string pMsgAr)
    {
        if (gv.Rows.Count > 0) { if (gv.Rows[0].Cells[0].Text == General.Msg(pMsgEn, pMsgAr)) { FillGridEmpty(ref gv, pHeight,pMsgEn,pMsgAr); } }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static bool isGridEmpty(string RowText)
    {
        if (RowText == "No Data Found" || RowText == "لا توجد بيانات") { return true; } 
        return false;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static bool isGridEmpty(string RowText,string EmptyText)
    {
        if (RowText == EmptyText ) { return true; } 
        return false;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*###############################################################################################################################*/
    /*###############################################################################################################################*/
    #region FillDDL
    
    static string[,] Religions = new string[4, 3] { { "-Select Religion-", "-اختر الديانة-", "0" } , { "Muslim", "مسلم", "Muslim" }, 
                                                    { "Christian", "مسيحي", "Christian" }           , { "Jewish", "يهودي", "Jewish" } };
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static string[,] WorkTypes = new string[5, 3] { { "-Select Work Type-", "-اختر طبيعة العمل-", "0" } ,{ "Teaching", "تدريسية", "Teaching" },
                                                    { "Research", "بحثية", "Research" }, { "Administrative", "إدارية", "Administrative" }, 
                                                    { "Other", "أخرى", "Other" } };
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static string[,] Qualifications = new string[6, 3] { { "-Select Qualification-", "-اختر المؤهل العلمي-", "0" } ,{ "Doctorate", "دكتوراه", "Doctorate" }
                                                        ,{ "Fellowship", "زمالة", "Fellowship" }, { "Master", "ماجستير", "Master" } 
                                                        ,{ "Bachelor", "بكالوريوس", "Bachelor" } ,{ "Other", "أخرى", "Other" } };

    static string[,] QualificationsOfficer = new string[9, 3] { { "-Select Qualification-", "-اختر المؤهل العلمي-", "0" } ,{ "Elementary", "ابتدائية", "Elementary" }
                                                        ,{ "Intermediate", "متوسطة", "Intermediate" }, { "Secondary", "ثانوية", "Secondary" } 
                                                        ,{ "Diploma", "دبلوم", "Diploma" } ,{ "Bachelor", "بكالوريوس", "Bachelor" }
                                                        , { "Master", "ماجستير", "Master" } ,{ "Doctorate", "دكتوراه", "Doctorate" }
                                                        ,{ "Other", "أخرى", "Other" } };
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static string[,] MaritalStatus = new string[4, 3] { { "-Select Marital-", "-اختر الحالة الاجتماعية-", "0" } , { "Single", "أعزب", "Single" }, 
                                                      { "Married", "متزوج", "Married" }, { "Other", "أخرى", "Other" } };
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static string[,] Status = new string[2, 3] { { "Active", "فعال", "1" } , { "InActive", "غير فعال", "0" } };
    static string[,] StatusWithSelect = new string[3, 3] { { "-Select Status-", "-اختر الحالة -", "-" } , { "Active", "فعال", "1" } , { "InActive", "غير فعال", "0" } };
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static string[,] CardStatus = new string[3, 3] { { "Editable", "قابل للتعديل", "0" } , { "InProcess", "تحت الإجراء", "1" } , { "Cancelled", "ملغاة", "4" } };
    static string[,] CardStatusFull = new string[5, 3] { { "Editable", "قابل للتعديل", "0" } , { "InProcess", "تحت الإجراء", "1" } , { "Cancelled", "ملغاة", "4" } 
                                                         , { "Active", "فعالة", "2" } , { "inActive", "غير فعالة", "3" }};
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static string[,] RetReason = new string[6, 3] { { "-Select Reason-", "-اختر السبب-", "0" } ,{ "Resignation", "استقالة", "Resignation" }
                                                        ,{ "Retirement", "تقاعد", "Retirement" }, { "Separation", "فصل", "Separation" } 
                                                        ,{ "expiration Contract", "إنتهاء العقد", "expiration Contract" } ,{ "Other", "أخرى", "Other" } };
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public string[,] FindValues(string pType)
    {
        try
        {
            if (pType == "Religion")         { return Religions; }
            if (pType == "WorkType")         { return WorkTypes; }
            if (pType == "Qualification")    { return Qualifications; }
            if (pType == "MaritalStatus")    { return MaritalStatus; }
            if (pType == "Status")           { return Status; }
            if (pType == "StatusWithSelect") { return StatusWithSelect; }
            if (pType == "CardStatus")       { return CardStatus; }
            if (pType == "CardStatusFull")   { return CardStatusFull; }
            if (pType == "RetReason")        { return RetReason; }
            if (pType == "QualificationsOfficer") { return QualificationsOfficer; }
            
            return null;
        }
        catch (Exception e1) { throw e1; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public void FillDDL(string pType,DropDownList ddl)
    {
        try
        {
            string[,] values = FindValues(pType);
            ddl.Items.Clear();
            
            for (int i = 0; i < values.GetLength(0); i++) { ListItem ls = new ListItem(General.Msg(values[i, 0], values[i, 1]), values[i, 2]); /***/ ddl.Items.Add(ls); }
        }
        catch (Exception e1) { throw e1; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public string getText(string pType, object pValue, object pValueOthers)
    {
        try
        {
            string[,] values = FindValues(pType);
            if (pValue == null) { return null; }
            for (int i = 0; i < values.GetLength(0); i++) 
            {
                if (pValue.ToString() == "True" ) { return General.Msg(values[i, 0], values[i, 1]); }
                if (pValue.ToString() == "False") { return General.Msg(values[i, 0], values[i, 1]); }
                
                if (pValue.ToString() == values[i, 2]) 
                {
                    if (pValue.ToString() == "Other") { return pValueOthers.ToString(); } else { return General.Msg(values[i, 0], values[i, 1]); }
                } 
            }
                return null;
        }
        catch (Exception e1) { return null; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    static public void FillNumberYearsddl(DropDownList ddl)
    {
        ddl.Items.Clear();
        ListItem ls = new ListItem(General.Msg("-Select Number of Years-", "-اختر عدد السنوات-"),"0");  /***/ ddl.Items.Add(ls);

        for (int i = 1; i <= 10; i++)  {  ls = new ListItem(General.Msg(i.ToString(), i.ToString()), i.ToString());  /***/ ddl.Items.Add(ls); }
    }

    #endregion
    /*###############################################################################################################################*/
    /*###############################################################################################################################*/
}