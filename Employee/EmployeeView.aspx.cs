using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Services;

public partial class Employee_EmployeeView : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    EmployeePro ProClass = new EmployeePro();
    EmployeeSql SqlClass = new EmployeeSql();
    DataTable dt;

    string MainPer = "Employees";
    string MainName1Ar = "موظف";
    string MainName2Ar = "الموظف";
    string MainNameEn = "Employee";

    string MainQuery = " SELECT * FROM Employees ";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //---Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Employees", pageDiv);
            //---Common Code ----------------------------------------------------------------- //

            if (!IsPostBack)
            {
                pnlMain.Attributes.Add("onkeypress", "javascript:return DefaultButton(event,'" + btnIDSearch.ClientID + "');");

                if (!FormSession.getPerm(new string[] { "IMng", "UMng", "IEmp", "UEmp", "ICon", "UCon" })) { Response.Redirect(@"~/Login.aspx"); }

                if (Request.QueryString["ac"] != null)
                {
                    string ac = Request.QueryString["ac"].ToString();
                    ViewState["ac"] = ac;

                    if (ac == "View")
                    {
                        //if (!FormSession.getPerm("UMng")) { btnSave.Enabled = false; btnIDSearch.Enabled = false; }
                        string iEmpID = Request.QueryString["EmpID"].ToString();
                        divContract.Visible = true;
                        divSection.Visible = true;
                        DataItemEnabled(false);
                        Fillddl();
                        btnIDSearch.Enabled = btnUploadDoc.Enabled = false;

                        if (Request.QueryString["EmpID"] != null)
                        {
                            DataTable EmpDT = DBFun.FetchData("SELECT * FROM EmployeeMaster WHERE EmpID = '" + iEmpID + "' ");
                            FillGrdDocs(EmpDT.Rows[0]["EmpNationalID"].ToString());

                            if (!DBFun.IsNullOrEmpty(EmpDT))
                            {
                                ddlEmpType.SelectedIndex = ddlEmpType.Items.IndexOf(ddlEmpType.Items.FindByValue(EmpDT.Rows[0]["EmpType"].ToString()));
                                FillFromDT(EmpDT);
                            }
                        }
                    }
                }
            }

            if (IsPostBack)
            {
                EmpImage.PopulateImage(txtEmpNationalID.Text);
            }
        }
        catch (Exception e1) { DBFun.InsertError(FormSession.PageName, "PageLoad"); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool getStatus(char BtnStatus) { return Convert.ToBoolean(Convert.ToInt32(BtnStatus.ToString())); }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Fillddl()
    {
        DataTable NatDT = DBFun.FetchData("SELECT * FROM Nationality ");
        if (!DBFun.IsNullOrEmpty(NatDT))
        {
            FormCtrl.PopulateDDL(ddlNatID, NatDT, "NatName" + FormSession.Language, "NatID", General.Msg("-Select Nationality-", "-اختر الجنسية-"));
            rfvddlNatID.InitialValue = ddlNatID.Items[0].Text;
        }

        DataTable CompDT = DBFun.FetchData("SELECT * FROM Companies");
        if (!DBFun.IsNullOrEmpty(CompDT))
        {
            FormCtrl.PopulateDDL(ddlCompID, CompDT, "CompName" + FormSession.Language, "CompID", General.Msg("-Select Company-", "-اختر الشركة-"));
            rfvCompID.InitialValue = ddlCompID.Items[0].Text;
        }

        DataTable SecDT = DBFun.FetchData("SELECT * FROM SectionsExternal");
        if (!DBFun.IsNullOrEmpty(SecDT))
        {
            FormCtrl.PopulateDDL(ddlSecID, SecDT, "SecName" + FormSession.Language, "SecID", General.Msg("-Select Sections-", "-اختر الجهة الخارجية-"));
            rfvSecID.InitialValue = ddlSecID.Items[0].Text;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillPropeties()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ProClass.DateType      = FormSession.DateType;
            ProClass.EmpID         = txtEmpIdentity.Text;
            ProClass.EmpType       = ddlEmpType.SelectedValue;
            ProClass.EmpNameEn     = txtEmpNameEn.Text;
            ProClass.EmpNameAr     = txtEmpNameAr.Text;
            ProClass.EmpBirthDate  = CalBirthDate.getDate();
            ProClass.EmpJobTitleAr = txtJobTitleAr.Text;
            ProClass.EmpJobTitleEn = txtJobTitleEn.Text;
            ProClass.EmpNationalID = txtEmpNationalID.Text;
            ProClass.EmpHireDate   = CalHireDate.getDate();
            ProClass.EmpMobileNo   = txtMobile.Text;
            ProClass.EmpEmailID    = txtEmail.Text;

            if (ddlNatID.SelectedIndex > 0) { ProClass.NatID = ddlNatID.SelectedValue; }
            if (ddlBloodGroup.SelectedIndex > 0) { ProClass.EmpBloodGroup = ddlBloodGroup.SelectedValue; }
            if (ddlCompID.SelectedIndex > 0) { ProClass.CompID = ddlCompID.SelectedValue; }
            if (ddlSecID.SelectedIndex > 0) { ProClass.SecID = ddlSecID.SelectedValue; }
            if (rdlGender.SelectedIndex > -1) { ProClass.EmpGender = Convert.ToChar(rdlGender.SelectedValue); }

            ProClass.TransactionBy   = FormSession.LoginUsr;
            ProClass.TransactionDate = DateTime.Now.ToShortDateString();
            //////////////////////////////////////////////
            Byte[] pImage = new Byte[0];
            string pImageContentType = "";
            int pImageLength = 0;
            EmpImage.GetImage(out pImage, out pImageContentType, out pImageLength);

            if (pImageLength != 0) { ProClass.EmpImage = CryptoImage.EncryptBytes(pImage); } else { ProClass.EmpImage = pImage; }
            ProClass.EmpImageContentType = pImageContentType;
            ProClass.EmpImageLength = pImageLength;
            //////////////////////////////////////////////

        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "FillPropeties");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void DataItemEnabled(bool status)
    {
        txtEmpIdentity.Enabled = status;
        txtEmpNameAr.Enabled = status;
        txtEmpNameEn.Enabled = status;
        ddlNatID.Enabled = status;
        CalBirthDate.setEnabled(status);
        txtEmpNationalID.Enabled = status;
        txtJobTitleAr.Enabled = status;
        txtJobTitleEn.Enabled = status;
        ddlBloodGroup.Enabled = status;
        ddlCompID.Enabled = status;
        ddlSecID.Enabled = status;
        CalHireDate.setEnabled(status);
        rdlGender.Enabled = status;
        txtMobile.Enabled = status;
        txtEmail.Enabled = status;

        EmpImage.EnabledImage(status);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        
        ClearUI();
        DataItemEnabled(false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnIDSearch_Click(object sender, EventArgs e)
    {
        ClearUI();

        PopulateUI(txtIDSearch.Text.Trim());
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void PopulateUI(string pID)
    {
        try
        {

            DataTable myTableDT = DBFun.FetchData("SELECT * FROM EmployeeMaster WHERE EmpID = '" + pID + "' AND EmpType = '" + ddlEmpType.SelectedValue + "' ");

            FillFromDT(myTableDT);

            txtEmpIdentity.Text = txtIDSearch.Text;
            DataItemEnabled(true);
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "PopulateUI");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillFromDT(DataTable DT)
    {
        txtEmpIdentity.Text   = DT.Rows[0]["EmpID"].ToString();
        txtEmpNameAr.Text     = DT.Rows[0]["EmpNameAr"].ToString();
        txtEmpNameEn.Text     = DT.Rows[0]["EmpNameEn"].ToString();
        txtJobTitleAr.Text    = DT.Rows[0]["EmpJobTitleAr"].ToString();
        txtJobTitleEn.Text    = DT.Rows[0]["EmpJobTitleEn"].ToString();
        txtEmpNationalID.Text = DT.Rows[0]["EmpNationalID"].ToString();
        txtMobile.Text        = DT.Rows[0]["EmpMobileNo"].ToString();
        txtEmail.Text         = DT.Rows[0]["EmpEmailID"].ToString();

        CalBirthDate.setDBDate(DT.Rows[0]["EmpBirthDate"], "S");
        CalHireDate.setDBDate(DT.Rows[0]["EmpHireDate"], "S");

        ddlNatID.SelectedIndex = ddlNatID.Items.IndexOf(ddlNatID.Items.FindByValue(DT.Rows[0]["NatID"].ToString()));
        ddlBloodGroup.SelectedIndex = ddlBloodGroup.Items.IndexOf(ddlBloodGroup.Items.FindByValue(DT.Rows[0]["EmpBloodGroup"].ToString()));
        if (DT.Rows[0]["CompID"] != DBNull.Value) { ddlCompID.SelectedIndex = ddlCompID.Items.IndexOf(ddlCompID.Items.FindByValue(DT.Rows[0]["CompID"].ToString())); }
        if (DT.Rows[0]["SecID"] != DBNull.Value) { ddlSecID.SelectedIndex = ddlSecID.Items.IndexOf(ddlSecID.Items.FindByValue(DT.Rows[0]["SecID"].ToString())); }
        if (DT.Rows[0]["EmpGender"] != DBNull.Value) { rdlGender.SelectedIndex = rdlGender.Items.IndexOf(rdlGender.Items.FindByValue(DT.Rows[0]["EmpGender"].ToString())); }

        if ((DT.Rows[0]["image"] == DBNull.Value) || (DT.Rows[0]["ImageLength"].ToString() == "0")) { EmpImage.ClearImage(); } else { EmpImage.setImage(DT.Rows[0]["EmpNationalID"].ToString()); }

    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ClearUI()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        int returnValue = DBFun.ExecuteData("DELETE FROM TempImage WHERE Type = 'Employee' AND EmpID='" + txtEmpNationalID.Text + "'");

        txtEmpIdentity.Text   = "";
        txtEmpNameAr.Text     = "";
        txtEmpNameEn.Text     = "";
        txtJobTitleAr.Text    = "";
        txtJobTitleEn.Text    = "";
        txtEmpNationalID.Text = "";
        txtMobile.Text        = "";
        txtEmail.Text         = "";

        CalBirthDate.ClearDate();
        CalHireDate.ClearDate();

        ddlNatID.SelectedIndex      = -1;
        ddlBloodGroup.SelectedIndex = -1;
        ddlCompID.SelectedIndex     = -1;
        ddlSecID.SelectedIndex      = -1;
        rdlGender.SelectedIndex     = 0;

        EmpImage.ClearImage();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetNameType(string pType)
    {
        string typeName = "";
        try
        {
            if (pType.Contains("Mng")) { typeName = General.Msg("Aramco Employee", "موظفي أرامكو"); }
            if (pType.Contains("Emp")) { typeName = General.Msg("Third party Employees", "موظفي الجهات الخارجية"); }
            if (pType.Contains("Con")) { typeName = General.Msg("Contractors Company", "متعاقدي الشركات"); }

            return typeName;
        }
        catch (Exception e1)
        {
            return string.Empty;
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region grdDocs Events

    protected void btnUploadDoc_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtEmpNationalID.Text))
            {
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Validation, General.Msg("Please enter the National ID", "يجب إدخال رقم الهوية"));
                return;
            }

            if (string.IsNullOrEmpty(txtDocName.Text))
            {
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Validation, General.Msg("Please enter the Document Name","يجب إدخال اسم المستند"));
                return;
            }

            if (fuDocs.PostedFile != null && fuDocs.PostedFile.FileName != "")
            {
                string dateFile = String.Format("{0:ddMMYyyyyHHmmss}", DateTime.Now);
                string FileName = System.IO.Path.GetFileName(fuDocs.PostedFile.FileName);
                string[] nameArr = FileName.Split('.');
                string name = nameArr[0];
                string type = nameArr[1];
                string NewFileName = txtEmpNationalID.Text + "-Doc" + dateFile + "." + type;
                fuDocs.PostedFile.SaveAs(Server.MapPath(@"../Import/EmployeesFiles/") + NewFileName);

                ProClass.EmpID = txtEmpNationalID.Text;
                ProClass.DocName = txtDocName.Text;
                ProClass.DocPath = NewFileName;
                ProClass.TransactionBy = FormSession.LoginUsr;

                SqlClass.Docs_Insert(ProClass);
            }
            else
            {
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Validation, General.Msg("You must choose a file","يجب اختيار ملف"));
                return;
            }

            FillGrdDocs(txtEmpNationalID.Text);
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "btnUploadDoc");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void FillGrdDocs(string pID)
    {        
        DataTable DocDT = DBFun.FetchData(" SELECT * FROM EmployeeDocs WHERE EmpID = '" + pID + "'");
        if (!DBFun.IsNullOrEmpty(DocDT))
        {
            grdDocs.DataSource = (DataTable)DocDT;
            grdDocs.DataBind();
        }
        else { FormCtrl.FillGridEmpty(ref grdDocs,20,"No Document","لا توجد مستندات"); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected void grdDocs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ImageButton _btnDown = (ImageButton)e.Row.Cells[4].Controls[1];
            ScriptManager.GetCurrent(this).RegisterPostBackControl(_btnDown);
        }
        catch (Exception e1) { }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        try
        {
            switch (e.CommandName)
            {
                case ("Doc_Download"):
                    {
                        string DocPath = e.CommandArgument.ToString();
                       
                        string filePath = Server.MapPath(@"../Import/EmployeesFiles/") + DocPath;
                        string[] fileNameArr = DocPath.Split('\\');
                        string fileName = fileNameArr[fileNameArr.Length - 1];
                        HttpResponse res = HttpContext.Current.Response;
                        res.Clear();
                        res.AppendHeader("content-disposition", "attachment; filename=" + fileName);
                        res.ContentType = "application/octet-stream";
                        res.WriteFile(filePath);
                        res.Flush();
                        res.End();
                        break;
                    }
                
                case ("Doc_Delete"):
                    {
                        string[] Arg = e.CommandArgument.ToString().Split(';');

                        string DocID   = Arg[0];
                        string DocPath = Arg[1];
                       
                        
                        ProClass.DocID         = DocID;
                        ProClass.TransactionBy = FormSession.LoginUsr;
                        
                        SqlClass.Docs_Delete(ProClass);
                        string Path = Server.MapPath(@"../Import/EmployeesFiles/") + DocPath;
                        if (File.Exists(Path)) { File.Delete(Path); }

                        FillGrdDocs(txtEmpNationalID.Text);
                        break;
                    }
            }
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "grdDocs_RowCommand");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdDocs_RowDeleting(object sender, GridViewDeleteEventArgs e) { }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}