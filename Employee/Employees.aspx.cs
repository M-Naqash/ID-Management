

using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Services;

public partial class Employee_Employees : BasePage
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

                    if (ac == "IMng")
                    {
                        if (!FormSession.getPerm("IMng")) { btnSave.Enabled = false; btnIDSearch.Enabled = false; }

                        btnSave.Text = General.Msg("Save", "حفظ");
                        MainMasterPage.ShowTitel(General.Msg("Add Aramco Employee", "إضافة موظف أرامكو"));
                        ddlEmpType.SelectedIndex = ddlEmpType.Items.IndexOf(ddlEmpType.Items.FindByValue("Mng"));
                        divContract.Visible = false;
                        rfvCompID.Enabled = false;
                        divSection.Visible = false;
                        rfvSecID.Enabled = false;
                    }

                    if (ac == "UMng")
                    {
                        if (!FormSession.getPerm("UMng")) { btnSave.Enabled = false; btnIDSearch.Enabled = false; }

                        btnSave.Text = General.Msg("Update", "تعديل");
                        MainMasterPage.ShowTitel(General.Msg("Update Aramco Employee", "تعديل موظف أرامكو"));
                        ddlEmpType.SelectedIndex = ddlEmpType.Items.IndexOf(ddlEmpType.Items.FindByValue("Mng"));
                        divContract.Visible = false;
                        rfvCompID.Enabled = false;
                        divSection.Visible = false;
                        rfvSecID.Enabled = false;
                    }

                    if (ac == "IEmp")
                    {
                        if (!FormSession.getPerm("IEmp")) { btnSave.Enabled = false; btnIDSearch.Enabled = false; }

                        btnSave.Text = General.Msg("Save", "حفظ");
                        MainMasterPage.ShowTitel(General.Msg("Add Third party Employee", "إضافة موظف جهات خارجية "));
                        ddlEmpType.SelectedIndex = ddlEmpType.Items.IndexOf(ddlEmpType.Items.FindByValue("Emp"));
                        divContract.Visible = false;
                        rfvCompID.Enabled = false;
                        divSection.Visible = true;
                        rfvSecID.Enabled = true;
                        txtIDSearch.Text = FindMaxID();
                    }

                    if (ac == "UEmp")
                    {
                        if (!FormSession.getPerm("UEmp")) { btnSave.Enabled = false; btnIDSearch.Enabled = false; }

                        btnSave.Text = General.Msg("Update", "تعديل");
                        MainMasterPage.ShowTitel(General.Msg("Update Third party Employee", "تعديل موظف جهات خارجية "));
                        ddlEmpType.SelectedIndex = ddlEmpType.Items.IndexOf(ddlEmpType.Items.FindByValue("Emp"));
                        divContract.Visible = false;
                        rfvCompID.Enabled = false;
                        divSection.Visible = true;
                        rfvSecID.Enabled = true;
                    }

                    if (ac == "ICon")
                    {
                        if (!FormSession.getPerm("ICon")) { btnSave.Enabled = false; btnIDSearch.Enabled = false; }

                        btnSave.Text = General.Msg("Save", "حفظ");
                        MainMasterPage.ShowTitel(General.Msg("Add Contractor", "إضافة متعاقد"));
                        ddlEmpType.SelectedIndex = ddlEmpType.Items.IndexOf(ddlEmpType.Items.FindByValue("Con"));
                        divContract.Visible = true;
                        rfvCompID.Enabled = true;
                        divSection.Visible = false;
                        rfvSecID.Enabled = false;
                        txtIDSearch.Text = FindMaxID();
                    }

                    if (ac == "UCon")
                    {
                        if (!FormSession.getPerm("UCon")) { btnSave.Enabled = false; btnIDSearch.Enabled = false; }

                        btnSave.Text = General.Msg("Update", "تعديل");
                        MainMasterPage.ShowTitel(General.Msg("Update Contractor", "تعديل متعاقد"));
                        ddlEmpType.SelectedIndex = ddlEmpType.Items.IndexOf(ddlEmpType.Items.FindByValue("Con"));
                        divContract.Visible = true;
                        rfvCompID.Enabled = true;
                        divSection.Visible = false;
                        rfvSecID.Enabled = false;
                    }
                }

                EmpImage.EnabledImage(false);
                Fillddl();
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
    protected void ButtonAction(string pBtn, bool search) //string pBtn = [Save,Cancel]
    {
        btnSave.Enabled = btnCancel.Enabled = false;

        btnSave.Enabled = getStatus(pBtn[0]);
        btnCancel.Enabled = getStatus(pBtn[1]);

        btnIDSearch.Enabled = txtIDSearch.Enabled = search;
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
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (!Page.IsValid)
        {
            ValidatorCollection ValidatorColl = Page.Validators;
            for (int k = 0; k < ValidatorColl.Count; k++)
            {
                if (!ValidatorColl[k].IsValid && !String.IsNullOrEmpty(ValidatorColl[k].ErrorMessage)) { vsSave.ShowSummary = true; return; }
                vsSave.ShowSummary = false;
            }
            return;
        }
        try
        {
            if ((btnSave.Text == "Save") || (btnSave.Text == "حفظ"))
            {
                if (ddlEmpType.SelectedValue == "Emp" || ddlEmpType.SelectedValue == "Con") { txtIDSearch.Text = txtEmpIdentity.Text = FindMaxID(); }

                dt = DBFun.FetchData("select EmpID from EmployeeMaster where EmpID = '" + txtEmpIdentity.Text.Trim() + "'");
                if (!DBFun.IsNullOrEmpty(dt))
                {
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Error, General.Msg("Employee ID already exists,Please enter different ID", "رقم الموظف موجود مسبقا,من فضلك اختر رقما آخر"));
                }
                else
                {
                    if (Request.QueryString["ac"] != null)
                    {
                        FillPropeties();
                        SqlClass.Insert(ProClass);
                        string empid = txtEmpIdentity.Text;
                        ClearUI();
                        //if (ddlEmpType.SelectedValue == "Emp" || ddlEmpType.SelectedValue == "Con") { SqlClass.Booked_Delete(txtIDSearch.Text); }
                        txtIDSearch.Text = "";
                        MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " data added successfully, EmployeeID is '" + empid + "'", "تمت إضافة بيانات " + MainName2Ar + " بنجاح, رقم الموظف هو '" + empid + "'"));

                        if (ddlEmpType.SelectedValue == "Emp" || ddlEmpType.SelectedValue == "Con") { txtIDSearch.Text = FindMaxID(); }
                    }
                }
            }

            if ((btnSave.Text == "Update") || (btnSave.Text == "تعديل"))
            {

                dt = DBFun.FetchData("select * from EmployeeMaster where EmpID = '" + txtEmpIdentity.Text.Trim() + "'");
                if (DBFun.IsNullOrEmpty(dt))
                {
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Error, General.Msg("This ID No part of " + GetNameType(Request.QueryString["ac"].ToString()) + " ,Please enter different ID", "هذا الرقم لا يوجد ضمن " + GetNameType(Request.QueryString["ac"].ToString()) + " ,من فضلك اختر رقما آخر"));
                }
                else
                {
                    if (Request.QueryString["ac"] != null)
                    {
                        FillPropeties();
                        SqlClass.Update(ProClass);
                        ClearUI();
                        txtIDSearch.Text = "";
                        MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " data updated successfully", "تم تعديل بيانات " + MainName2Ar + " بنجاح"));
                    }
                }
            }

            ButtonAction("00", true);
            DataItemEnabled(false);
            EmpImage.PopulateImage(txtEmpNationalID.Text);
        }


        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "btnSave");
            EmpImage.PopulateImage(txtEmpNationalID.Text);
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //if ((btnSave.Text == "Save") || (btnSave.Text == "حفظ"))
        //{
        //    if (ddlEmpType.SelectedValue == "Emp" || ddlEmpType.SelectedValue == "Con") { SqlClass.Booked_Delete(txtIDSearch.Text); }
        //}

        ClearUI();
        ButtonAction("00", true);
        DataItemEnabled(false);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnIDSearch_Click(object sender, EventArgs e)
    {
        ClearUI();

        if (!Page.IsValid)
        {
            ValidatorCollection ValidatorColl = Page.Validators;
            for (int k = 0; k < ValidatorColl.Count; k++)
            {
                if (!ValidatorColl[k].IsValid && !String.IsNullOrEmpty(ValidatorColl[k].ErrorMessage)) { vsFetch.ShowSummary = true; return; }
                vsFetch.ShowSummary = false;
            }
            return;
        }

        PopulateUI(txtIDSearch.Text.Trim());
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void PopulateUI(string pID)
    {
        try
        {
            if ((btnSave.Text == "Update") || (btnSave.Text == "تعديل"))
            {
                DataTable myTableDT = DBFun.FetchData("SELECT * FROM EmployeeMaster WHERE EmpID = '" + pID + "' AND EmpType = '" + ddlEmpType.SelectedValue + "' ");

                if (DBFun.IsNullOrEmpty(myTableDT))
                {
                    ButtonAction("00", true);
                    MessageFun.ShowMsg(this, vsSearch, cvShowMsg, MessageFun.TypeMsg.Warning, "vgSearch", General.Msg("This employee No part of " + GetNameType(Request.QueryString["ac"].ToString()), "الموظف غير موجود ضمن " + GetNameType(Request.QueryString["ac"].ToString())));
                    return;
                }
                FillFromDT(myTableDT);
            }
            txtEmpIdentity.Text = txtIDSearch.Text;
            ButtonAction("11", false);
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

        FillGrdDocs(txtEmpNationalID.Text);
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
    public string FindMaxID()
    {
        string MaxID = "20000001";
        DataTable MaxDT = DBFun.FetchData("SELECT E.MaxID FROM (SELECT MAX(CONVERT(INT,EmpID) + 1) MaxID FROM EmployeeMaster WHERE EmpType IN ('Emp','Con')) AS E");
        if (DBFun.IsNullOrEmpty(MaxDT)) { }
        else { if (MaxDT.Rows[0]["MaxID"] != DBNull.Value) { MaxID = (Convert.ToInt64(MaxDT.Rows[0]["MaxID"].ToString())).ToString(); } else { } }

        bool isFound = true;
        while (isFound)
        {
            isFound = CheckFoundID(MaxID);
            if (isFound) { MaxID = (Convert.ToInt64(MaxID) + 1).ToString(); }
        }

        return MaxID;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool CheckFoundID(string pMaxID)
    {
        DataTable SDT = DBFun.FetchData("SELECT EmpID FROM EmployeeMaster WHERE EmpID = '" + pMaxID + "'");
        if (!DBFun.IsNullOrEmpty(SDT)) { return true; } else { return false; }

        //DataTable SDT = DBFun.FetchData("SELECT BkdID FROM BookedID WHERE BkdID = '" + pMaxID + "'");
        //if (!DBFun.IsNullOrEmpty(SDT)) { return true; } 
        //else 
        //{
        //    SqlClass.Booked_Insert(pMaxID);

        //    return false;
        //}
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
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Validation, General.Msg("Please enter the Document Name", "يجب إدخال اسم المستند"));
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
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Validation, General.Msg("You must choose a file", "يجب اختيار ملف"));
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
        else { FormCtrl.FillGridEmpty(ref grdDocs, 20, "No Document", "لا توجد مستندات"); }
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

    /*#############################################################################################################################*/
    /*#############################################################################################################################*/
    #region Custom Validate Events

    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void IDSearch_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvIDSearch))
            {
                if (string.IsNullOrEmpty(txtIDSearch.Text))
                {
                    General.ValidMsg(this, ref cvIDSearch, false, "Employee identity is required", "رقم الموظف مطلوب");
                    e.IsValid = false;
                    return;
                }

                else
                {
                    if ((btnSave.Text == "Save") || (btnSave.Text == "حفظ"))
                    {
                        dt = DBFun.FetchData("SELECT * FROM EmployeeMaster WHERE EmpID = '" + txtIDSearch.Text.Trim() + "' ");
                        if (!DBFun.IsNullOrEmpty(dt))
                        {
                            General.ValidMsg(this, ref cvIDSearch, true, "Employee ID is already exists,Please enter different ID", "رقم الموظف مضاف مسبقاً ,من فضلك اختر رقما آخر");
                            e.IsValid = false;
                            return;
                        }
                    }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BirthDateValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvcalBirthDate))
            {
                if (string.IsNullOrEmpty(CalBirthDate.getDate()))
                {
                    General.ValidMsg(this, ref cvcalBirthDate, false, "Birth Date is required", "تاريخ الميلاد مطلوب");
                    e.IsValid = false;
                    return;
                }
                else
                {
                    int iStartDate = DateFun.ConvertDateTimeToInt(FormSession.DateType, CalBirthDate.getDate());
                    int iEndDate   = DateFun.ConvertDateTimeToInt(FormSession.DateType,(DateFun.ToAnyFormat(FormSession.DateType, DateTime.Now)));
                    General.ValidMsg(this, ref cvcalBirthDate, true, "Birthday greater than today's date", "تاريخ الميلاد أكبر من تاريخ اليوم");
                    
                    if (iStartDate > iEndDate) { e.IsValid = false; }
                }
            }
        }
        catch { e.IsValid = false; }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void HireDateValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvCalHireDate))
            {
                if (!string.IsNullOrEmpty(CalHireDate.getDate()))
                {
                    int iStartDate = DateFun.ConvertDateTimeToInt(FormSession.DateType, CalHireDate.getDate());
                    int iEndDate   = DateFun.ConvertDateTimeToInt(FormSession.DateType,(DateFun.ToAnyFormat(FormSession.DateType, DateTime.Now)));
                    General.ValidMsg(this, ref cvCalHireDate, true, "Hire date greater than today's date", "تاريخ التعيين أكبر من تاريخ اليوم");
                    
                    if (iStartDate > iEndDate) { e.IsValid = false; }
                }
            }
        }
        catch { e.IsValid = false; }
    }

    #endregion
    /*#############################################################################################################################*/
    /*#############################################################################################################################*/

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}