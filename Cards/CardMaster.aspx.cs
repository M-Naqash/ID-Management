using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class CardMaster : BasePage
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

    /* CardStatus In ( 0 = Editable , 1 = InProcess , 2 = Active , 3 = inActive , 4 Cancelled )   */
    /* InActiveStatus In ( 0 = Not , 1 = Cancelled , 2 = rejected , 3 = Expiryed , 4 = returned , 5 = Missing)   */
    /* IsApproved In ( -1 = rejected, 0 = Wiat , 1 = Approved )   */
    /* isPrinted In  ( 0 = NotPrinted , 1 = Printed )   */

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    CardsPro ProClass = new CardsPro();
    CardsSql SqlClass = new CardsSql();
    DataTable dt;

    string MainPer = "Cards";
    string MainName1Ar = "بطاقة";
    string MainName2Ar = "البطاقة";
    string MainNameEn = "Card";

    string MainQuery = " SELECT * FROM CardMaster ";

    string ddlCardstatusenable = string.Empty;
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //   --------------------Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Card", pageDiv);
            
            FormCtrl.RefreshGridEmpty(ref grdData, 20, "This employee does not have cards","هذا الموظف لا يملك بطاقات");
            //   --------------------Common Code ----------------------------------------------------------------- //

            if (!IsPostBack)
            {
                if (!FormSession.getPerm(new string[] { "ICrd", "UCrd"})) { Response.Redirect(@"~/Login.aspx"); }
                ViewState["EmpID"] = "";

                divCountPrint.Visible = false;
                FillDDL();

                MainMasterPage.ShowTitel(General.Msg("Add Cards", "إصدار بطاقة"));
                ButtonAction("111000");
                DataItemStatus(false, "");

                if (Request.QueryString["ID"] != null)
                {
                    ddlSearchBy.SelectedIndex = 0;
                    txtSearchBy.Text = Request.QueryString["ID"].ToString();
                    ViewState["EmpID"] = txtSearchBy.Text;
                    Fetch();
                }

                //if (Request.QueryString["ac"] != null)
                //{
                //    if (Request.QueryString["ac"].ToString() == "i")
                //    {
                //        btnSave.Text = General.Msg("Save","حفظ");
                //        MainMasterPage.ShowTitel(General.Msg("Add Cards", "إصدار بطاقة"));
                //        ButtonAction("10");
                //        ddlIssue.Enabled = true;

                //        if (Request.QueryString["ID"] != null) 
                //        {
                //            ddlSearchBy.SelectedIndex = 0;
                //            txtSearchBy.Text = Request.QueryString["ID"].ToString();
                //            ViewState["EmpID"] = txtSearchBy.Text;
                //            Fetch();
                //        }
                //    }
                //    if (Request.QueryString["ac"].ToString() == "u")
                //    {
                //        btnSave.Text = General.Msg("Update","تعديل");
                //        MainMasterPage.ShowTitel(General.Msg("Update Cards", "تعديل بطاقة"));
                //        ButtonAction("10");
                //        ddlIssue.Enabled = false;
                //    }
                //}
            }
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "PageLoad");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public void FillDDL()
    {
        dt = DBFun.FetchData("SELECT * FROM IssueState WHERE IsType = 'Card' ORDER BY CreatedBy");
        if (!DBFun.IsNullOrEmpty(dt)) { FormCtrl.PopulateDDL(ddlIssue, dt, "IsName" + General.Lang(), "IsID", General.Msg("-Select Issue-", "-اختر الإصدار-")); }

        dt = DBFun.FetchData("SELECT * FROM CardTemplate WHERE TmpType = 'Card'");
        if (!DBFun.IsNullOrEmpty(dt)) { FormCtrl.PopulateDDL(ddlTemplate, dt, "TmpName", "TmpID", General.Msg("-Select Template-", "-اختر النموذج-")); }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public void DataItemStatus(bool pStatus, string pType)
    {
        if (pType == "Add") { ddlIssue.Enabled = true; } else if(pType == "Edit") { ddlIssue.Enabled = false; }
        ddlIssue.Enabled = pStatus;
        txtCardCount.Enabled = pStatus;
        ddlTemplate.Enabled = pStatus;
        ddlCardstatus.Enabled = pStatus;
        calStartDate.setEnabled(pStatus);
        calEndDate.setEnabled(pStatus);
        txtDescription.Enabled = pStatus;
        txtEmpID.Enabled = pStatus;
        txtEmpNameEn.Enabled = pStatus;
        txtEmpNameAr.Enabled = pStatus;
        cblConditions.Enabled = pStatus;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public void FillPropetiesObject()
    {
        try
        {
            ProClass.DateType        = FormSession.DateType;
            ProClass.IsID            = Convert.ToInt16(ddlIssue.SelectedValue);
            ProClass.CardStatus      = ddlCardstatus.SelectedValue;
            ProClass.InActiveStatus  = "0";
            ProClass.IsApproved      = 0;
            ProClass.isPrinted       = false;
            ProClass.CardCount       = FindCount(txtEmpID.Text) + 1;   
            ProClass.Description     = txtDescription.Text;
            ProClass.StartDate       = calStartDate.getDate();
            ProClass.ExpiryDate      = calEndDate.getDate();
            ProClass.TransactionBy   = FormSession.LoginUsr;
            ProClass.TransactionDate = DateTime.Now.ToShortDateString();
            
            if (!string.IsNullOrEmpty(txtEmpID.Text)) { ProClass.EmpID = txtEmpID.Text.Trim(); }
            if (ddlTemplate.SelectedIndex > 0) { ProClass.TmpID = Convert.ToInt16(ddlTemplate.SelectedValue); }
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "FillPropetiesObject()");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public void FillGrid()
    {
        string EmpID = ViewState["EmpID"].ToString();
        string EmpType = "";

        string selectQ = " SELECT CardID,EmpID,EmpNameEn,EmpNameAr,IsID,StartDate,ExpiryDate,CardStatus,InActiveStatus,IsApproved,isPrinted,EmpType "
                       + " , IsName" + General.Lang() + " AS IssueName "           
                       + " FROM CardInfoView "
                       + " WHERE EmpID = '" + EmpID + "' ORDER BY CardID DESC";

        dt = DBFun.FetchData(selectQ);
        if (!DBFun.IsNullOrEmpty(dt))
        {
            txtEmpID.Text     = ViewState["EmpID"].ToString();
            txtEmpNameEn.Text = dt.Rows[0]["EmpNameEn"].ToString();
            txtEmpNameAr.Text = dt.Rows[0]["EmpNameAr"].ToString();
            EmpType           = dt.Rows[0]["EmpType"].ToString();

            grdData.DataSource = (DataTable)dt;
            grdData.DataBind();
            if (Request.QueryString["ac"].ToString() == "u") { PopulateUpdate(EmpID); } //else { FindEmpType(EmpID, ""); }
        }
        else 
        { 
            ClearUI(); 
            FormCtrl.FillGridEmpty(ref grdData,20,"This employee does not have cards","هذا الموظف لا يملك بطاقات");
            DataTable Empdt = DBFun.FetchData("SELECT EmpID,EmpType,EmpNameEn,EmpNameAr FROM EmployeeInfoView WHERE EmpID = '" + EmpID + "'");
            if (!DBFun.IsNullOrEmpty(Empdt))
            {
                txtEmpID.Text     = EmpID;
                txtEmpNameEn.Text = Empdt.Rows[0]["EmpNameEn"].ToString();
                txtEmpNameAr.Text = Empdt.Rows[0]["EmpNameAr"].ToString();
                EmpType           = Empdt.Rows[0]["EmpType"].ToString();

                //FindEmpType(EmpID, "");
            }
        }

        SetTmp(EmpType);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetTmp(string EmpType)
    {
        if (!string.IsNullOrEmpty(EmpType))
        {
            DataTable Tmpdt = DBFun.FetchData("SELECT TmpID FROM CardTemplate WHERE TmpType ='Card' AND EmpType = '" + EmpType + "'");
            if (!DBFun.IsNullOrEmpty(Tmpdt)) { ddlTemplate.SelectedIndex = ddlTemplate.Items.IndexOf(ddlTemplate.Items.FindByValue(Tmpdt.Rows[0]["TmpID"].ToString())); }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string FlipHDate(string pHDate)
    {
        if (!string.IsNullOrEmpty(pHDate))
        {
            string[] arr = pHDate.Split('/');
            if (arr.Length == 3) { return arr[2] + "/" + arr[1] + "/" + arr[0]; }
        }
        return "";
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ButtonAction(String pBtn) //string pBtn = [Fetch,Add,Edit,Delete,cancel,Save]
    {
        string mPerem = "Crd";

        btnFetch.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[0].ToString()));
        if (FormSession.PermUsr.Contains("I" + mPerem)) { btnAdd.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[1].ToString())); } else { btnAdd.Enabled = false; }
        if (FormSession.PermUsr.Contains("U" + mPerem)) { btnEdit.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[2].ToString())); } else { btnEdit.Enabled = false; }
        //if (FormSession.PermUsr.Contains("D" + MainPer)) { btnDelete.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[3].ToString())); } else { btnDelete.Enabled = false; }

        btnCancel.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[4].ToString()));
        btnSave.Enabled = Convert.ToBoolean(Convert.ToInt32(pBtn[5].ToString()));
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ClearUI();
        DataItemStatus(true,"Add");
        ButtonAction("000011");

        ViewState["CommandName"] = "Add";
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["CommandName"] = "Edit";
        DataItemStatus(true,"Edit");
        ButtonAction("000011");
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearUI();
        ButtonAction("111000");
        DataItemStatus(false, "");
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

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

            FillPropetiesObject();

            if (ViewState["CommandName"].ToString() == "Add") //(btnSave.Text == "Save") || (btnSave.Text == "حفظ")
            {
                int returnValue = SqlClass.Insert(ProClass);
                if (returnValue > 0)
                {
                    if (cblConditions.Items.Count > 0)
                    {
                        for (int i = 0; i < cblConditions.Items.Count; i++)
                        {
                            bool retValue = SqlClass.InsertCondition(returnValue.ToString(), cblConditions.Items[i].Value, cblConditions.Items[i].Text, cblConditions.Items[i].Selected);
                        }
                    }

                    ClearUI();
                    FillGrid();
                    ButtonAction("111000");
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " created successfully", "تمت إضافة بيانات " + MainName2Ar + " بنجاح"));
                }
            }

            if (ViewState["CommandName"].ToString() == "Edit") //(btnSave.Text == "Update") || (btnSave.Text == "تعديل")
            {
                dt = DBFun.FetchData("SELECT EmpID,CardID FROM CardMaster WHERE EmpID = '" + txtEmpID.Text.Trim() + "' AND CardStatus = 0 ORDER BY CardID DESC");
                if (DBFun.IsNullOrEmpty(dt)) { return; }
                Int32 cardID = Convert.ToInt32(dt.Rows[0]["CardID"]);

                ProClass.CardID = cardID.ToString();
                bool returnValue = SqlClass.Update(ProClass);
                if (returnValue)
                {
                    for (int i = 0; i < cblConditions.Items.Count; i++)
                    {
                        bool retValue = SqlClass.UpdateCondition(cardID.ToString(), cblConditions.Items[i].Value, cblConditions.Items[i].Selected);
                    }
                    ClearUI();
                    FillGrid();
                    ButtonAction("111000");
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " data updated successfully", "تمت تعديل بيانات " + MainName2Ar + " بنجاح"));
                }
            }
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "btnSave");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
    protected void ClearUI()
    {
        ddlIssue.SelectedIndex = -1;
        txtCardCount.Text = "";
        ddlTemplate.SelectedIndex = -1;
        ddlCardstatus.SelectedIndex = -1;
        calStartDate.ClearDate();
        calEndDate.ClearDate();
        txtDescription.Text = "";
        txtEmpID.Text = "";
        txtEmpNameEn.Text = "";
        txtEmpNameAr.Text = "";
        cblConditions.Items.Clear();

        divCountPrint.Visible = false;

        grdData.DataSource = new DataTable();
        grdData.DataBind();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PopulateUpdate(string EmpID)
    {
        try
        {
            string selectQ = " SELECT CardID,EmpID,EmpNameEn,EmpNameAr,IsID,IsNameEn,IsNameAr,StartDate,ExpiryDate,Description "
                           + ",CreatedBy,CreatedDate,CardStatus,TmpID,CardCount "
                           + " FROM CardInfoView "
                           + " WHERE CardStatus = 0 AND EmpID = '" + EmpID + "' ORDER BY CardID DESC";

            DataTable Empdt = DBFun.FetchData(selectQ);
            if (DBFun.IsNullOrEmpty(Empdt)) { return; }

            txtEmpID.Text = EmpID;
            txtEmpNameEn.Text = Empdt.Rows[0]["EmpNameEn"].ToString();
            txtEmpNameAr.Text = Empdt.Rows[0]["EmpNameAr"].ToString();
            
            ddlIssue.SelectedIndex = ddlIssue.Items.IndexOf(ddlIssue.Items.FindByValue(Empdt.Rows[0]["IsID"].ToString()));
            txtCardCount.Text = Empdt.Rows[0]["CardCount"].ToString();
            
            DataTable Condt = DBFun.FetchData("SELECT * FROM CardCondition WHERE ConditionType = 'Card' AND CardID = " + Empdt.Rows[0]["CardID"].ToString() + "");
            if (!DBFun.IsNullOrEmpty(Condt))
            {
                divCondition1.Visible = true;
                divCondition2.Visible = true;
                for (int i = 0; i < Condt.Rows.Count; i++)
                {
                    ListItem li = new ListItem(Condt.Rows[i]["ConditionName"].ToString(), Condt.Rows[i]["ConditionID"].ToString());
                    cblConditions.Items.Add(li);
                    cblConditions.Items[i].Selected = Convert.ToBoolean(Condt.Rows[i]["ConditionStatus"]);
                }
            }
            else
            {
                divCondition1.Visible = false;
                divCondition2.Visible = false;
            }
            ddlTemplate.SelectedIndex = ddlTemplate.Items.IndexOf(ddlTemplate.Items.FindByValue(Empdt.Rows[0]["TmpID"].ToString()));
            ddlCardstatus.SelectedIndex = ddlCardstatus.Items.IndexOf(ddlCardstatus.Items.FindByValue(Empdt.Rows[0]["CardStatus"].ToString()));

            if (Empdt.Rows[0]["StartDate"] != DBNull.Value) { calStartDate.setDBDate(Empdt.Rows[0]["StartDate"], "S"); }
            if (Empdt.Rows[0]["ExpiryDate"] != DBNull.Value) { calEndDate.setDBDate(Empdt.Rows[0]["ExpiryDate"], "S"); }

            txtDescription.Text = Empdt.Rows[0]["Description"].ToString();
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "PopulateUI");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    {

                        e.Row.Attributes.Add("onmouseover", "mouseout('alt_row_highlight',this);");
                        e.Row.Attributes.Add("onmouseout", "mouseover('alt_row_nohighlight',this);");
                        e.Row.Attributes.Add("onmousemove", "mousemove('alt_row_nohighlight',this);");
                        break;
                    }
            }
        }
        catch (Exception Ex)
        {
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlIssue_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Enabled) { FillCondition(); } //FillLost();
        }
        catch (Exception Ex)
        {
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillCondition()
    {
        if (ddlIssue.SelectedIndex > 0)
        {
            cblConditions.Items.Clear();
            dt = DBFun.FetchData("SELECT ISCondition FROM IssueState WHERE ISCondition = '1' AND IsID = " + ddlIssue.SelectedValue);
            if (DBFun.IsNullOrEmpty(dt)) { divCondition1.Visible = false; divCondition2.Visible = false; return; }

            DataTable Condt = DBFun.FetchData("SELECT * FROM IssueConditions WHERE ConditionType = 'Card' AND  IsID = " + ddlIssue.SelectedValue + "");
            if (DBFun.IsNullOrEmpty(Condt)) { return; }
            for (int i = 0; i < Condt.Rows.Count; i++) 
            {
                ListItem li = new ListItem(Condt.Rows[i]["ConditionName"].ToString(), Condt.Rows[i]["ConditionID"].ToString());
                cblConditions.Items.Add(li); 
            }
            divCondition1.Visible = true; divCondition2.Visible = true;
        }
        else
        {
            divCondition1.Visible = false; divCondition2.Visible = false; return;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnFetch_Click(object sender, EventArgs e) 
    {
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

        Fetch();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Fetch() 
    {
        string EmpID = ViewState["EmpID"].ToString();

        DateTime LastCardExpire = GetExpireDateCard(EmpID);

        if (LastCardExpire > DateTime.Now)
        {
            MessageFun.ShowMsg(this, MessageFun.TypeMsg.Warning, General.Msg("This employee has already active card and not expired", "هذا الموظف لدية بالفعل بطاقة سارية المفعول"));
        }

        int CountCard = FindCount(EmpID);

        if (CountCard > 0) 
        {  
            lblCntPrint.Text = General.Msg("This employee has " + CountCard.ToString() + " printed Cards ", "هذا الموظف لديه " + CountCard.ToString() + " بطاقات مطبوعة");
            lblCntPrint.ForeColor = Color.Red;
            divCountPrint.Visible = true;
        }

        FillGrid(); 
        ButtonAction("111000");
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public DateTime GetExpireDateCard(string pEmpID)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        DateTime dtExpireCard = new DateTime(1900, 01, 01);

        DataTable Carddt = DBFun.FetchData("select * from CardMaster where EmpID = '" + pEmpID + "' AND CardStatus = '2' ORDER BY CardID DESC");

        if (!DBFun.IsNullOrEmpty(Carddt)) { dtExpireCard = Convert.ToDateTime(Carddt.Rows[0]["ExpiryDate"].ToString()); }

        return dtExpireCard;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int FindCount(string pID)
    {
        int CardCount = 0;
        try
        {
            DataTable CountDT = DBFun.FetchData("SELECT COUNT(CardID) count FROM CardMaster WHERE isPrinted = 'True' AND EmpID = '" + pID + "'");
            if (DBFun.IsNullOrEmpty(CountDT)) { }
            else
            {
                CardCount = Convert.ToInt32(CountDT.Rows[0]["count"]);
            }
        }
        catch (Exception Ex)
        {
            DBFun.InsertError(FormSession.PageName, "FindCount");
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }

        return CardCount;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchBy.Text = "";

        if      (ddlSearchBy.SelectedValue == "EmpID")         { auSearchBy.ServiceMethod = "GetEmpIDList"; }
        else if (ddlSearchBy.SelectedValue == "EmpNameAr")     { auSearchBy.ServiceMethod = "GetEmpNameArList"; }
        else if (ddlSearchBy.SelectedValue == "EmpNameEn")     { auSearchBy.ServiceMethod = "GetEmpNameEnList"; }
        else if (ddlSearchBy.SelectedValue == "EmpNationalID") { auSearchBy.ServiceMethod = "GetEmpNationalIDList"; }
        else if (ddlSearchBy.SelectedValue == "EmpMobileNo")   { auSearchBy.ServiceMethod = "GetEmpMobileNoList"; }
    }

    /*###############################################################################################################################*/
    /*###############################################################################################################################*/
    #region Custom Validate Events

    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void DateValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvcalStartDate))
            {
                if (string.IsNullOrEmpty(calStartDate.getDate())) 
                { 
                    General.ValidMsg(this, ref cvcalStartDate,false, "Start Date is required", "تاريخ الإصدار مطلوب");
                    e.IsValid = false;
                    return;
                }

                if (!string.IsNullOrEmpty(calStartDate.getDate()) && !String.IsNullOrEmpty(calEndDate.getDate()))
                {
                    int iStartDate = DateFun.ConvertDateTimeToInt("Gregorian", calStartDate.getDate());
                    int iEndDate = DateFun.ConvertDateTimeToInt("Gregorian", calEndDate.getDate());
                    if (iStartDate > iEndDate) 
                    { 
                        General.ValidMsg(this, ref cvcalStartDate,true, "start date more than end date!", "تاريخ الإصدار أكبر من تاريخ الإنتهاء");
                        e.IsValid = false; 
                        return;
                    }
                }
            }
            else if (source.Equals(cvcalEndDate))
            {
                if (string.IsNullOrEmpty(calEndDate.getDate())) 
                { 
                    General.ValidMsg(this, ref cvcalEndDate,false, "Expiry Date is required", "تاريخ الإنتهاء مطلوب");
                    e.IsValid = false;
                    return;
                } 
            }
        }
        catch { e.IsValid = false; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void SearchBy_ServerValidate(Object source, ServerValidateEventArgs e)
    {        
        try
        {
            if (source.Equals(cvSearchBy))
            {
                if (string.IsNullOrEmpty(txtSearchBy.Text))
                {
                    General.ValidMsg(this, ref cvSearchBy,false, "You must enter a value in the search box", "يجب إدخال قيمة في مربع البحث");
                    e.IsValid = false;
                    return;
                }
                else
                {
                    ViewState["EmpID"] = "";
                    dt = DBFun.FetchData(" SELECT EmpID,CompID,EmpNationalID FROM EmployeeInfoView WHERE " + ddlSearchBy.SelectedValue + " = '" + txtSearchBy.Text.Trim() + "' ");
                    if (DBFun.IsNullOrEmpty(dt))
                    {
                        General.ValidMsg(this, ref cvSearchBy, true, "The employee does not exist", "الموظف غير موجود");
                        e.IsValid = false;
                        return;
                    }

                    string EmpNationalID = dt.Rows[0]["EmpNationalID"].ToString();

                    DataTable BlackDT = DBFun.FetchData("SELECT * FROM BlackList WHERE BlaIdentityNo = '" + EmpNationalID + "' ");
                    if (!DBFun.IsNullOrEmpty(BlackDT))
                    {
                        General.ValidMsg(this, ref cvSearchBy, true, "Employee entered is included in the blacklist and can not be issued a card", "الموظف المدخل موجود في القائمة السوداء لا يمكن اصدار بطاقة له");
                        e.IsValid = false;
                        return;
                    }

                    if (Request.QueryString["ac"].ToString() == "i")
                    {
                        string EmpID = dt.Rows[0]["EmpID"].ToString();
                        DataTable Editdt = DBFun.FetchData("select * from CardMaster where EmpID = '" + EmpID + "' AND CardStatus IN (0,1)");
                        if (!DBFun.IsNullOrEmpty(Editdt))
                        {
                            General.ValidMsg(this, ref cvSearchBy,true, "employee has editable card or InProcess, you can not create a new card until the Cancelled of the previous", "الموظف لديه بطاقة قابلة للتعديل أو تحت الإجراء ، لايمكن إنشاء بطاقة جديدة حتى إلغاء السابقة");
                            e.IsValid = false;
                            return;
                        }
                    }

                    ViewState["EmpID"] = dt.Rows[0]["EmpID"].ToString();
                }
            }
        }
        catch (Exception ex) { e.IsValid = false; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ConditionsValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvConditions))
            {
                if (ddlCardstatus.SelectedValue == "1" && cblConditions.Items.Count > 0 ) 
                {
                   for (int i = 0; i < cblConditions.Items.Count ; i++) { if (!cblConditions.Items[i].Selected) { e.IsValid = false; } }
                }
                else { e.IsValid = true; }
            }
        }
        catch { e.IsValid = false; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void TemplateValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvTemplate))
            {
                DataTable Empdt = DBFun.FetchData("SELECT EmpType FROM EmployeeInfoView WHERE EmpID = '" + txtEmpID.Text + "'");
                if (!DBFun.IsNullOrEmpty(Empdt))
                {
                    DataTable Tmpdt = DBFun.FetchData("SELECT TmpID FROM CardTemplate WHERE TmpType ='Card' AND EmpType = '" + Empdt.Rows[0]["EmpType"].ToString() + "'");
                    if (!DBFun.IsNullOrEmpty(Tmpdt))
                    {
                        ddlTemplate.SelectedIndex = ddlTemplate.Items.IndexOf(ddlTemplate.Items.FindByValue(Tmpdt.Rows[0]["TmpID"].ToString()));
                        
                    }

                    //if (Empdt.Rows[0]["EmpStatusAr"] != DBNull.Value)
                    //{
                    //    DataTable Rdt = DBFun.FetchData("SELECT TmpID FROM CardTemplate WHERE TmpType ='Card' AND EmpType = 'Re'");
                    //    if (Empdt.Rows[0]["EmpStatusAr"].ToString() == "إنهاء التعيين") { ddlTemplate.SelectedIndex = ddlTemplate.Items.IndexOf(ddlTemplate.Items.FindByValue(Rdt.Rows[0]["TmpID"].ToString())); }
                    //}

                    if (ddlTemplate.SelectedIndex > -1) { e.IsValid = true; return; }
                }
                e.IsValid = false;
            }
        }
        catch { e.IsValid = false; }
        //ShowMsg("No card Template for this employee, please add the first card Template", "لا يوجد نموذج بطاقة لهذا الموظف,الرجاء إضافة النموذج أولا");
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void IssueValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvIssue))
            {
                if (ddlIssue.SelectedIndex <= 0)
                { 
                    General.ValidMsg(this, ref cvIssue,false, "Issue Type is required!", "نوع الإصدار مطلوب !");
                    e.IsValid = false;
                    return;
                }
                else if (!string.IsNullOrEmpty(txtEmpID.Text) && ddlIssue.SelectedIndex > -1)
                {
                    dt = DBFun.FetchData("select IsRepeat from IssueState where IsID = " + ddlIssue.SelectedValue + "");
                    if (!DBFun.IsNullOrEmpty(dt))
                    {
                        if (dt.Rows[0]["IsRepeat"].ToString() != "1")
                        {
                            DataTable IsIDdt = DBFun.FetchData("select * from CardMaster where EmpID = '" + txtEmpID.Text.Trim() + "' and IsID = '" + ddlIssue.SelectedValue + "' AND isPrinted IN (1)");
                            if (!DBFun.IsNullOrEmpty(IsIDdt)) 
                            { 
                                General.ValidMsg(this, ref cvIssue,true, "Card cannot be created since Issue selected is non repeatable", "لا يمكن إنشاء بطاقة للإصدار المحدد, لأنها غير قابلة لإعادة الإصدار");
                                e.IsValid = false; 
                                return;
                            }
                        }
                    }

                    dt = DBFun.FetchData("select * from CardMaster where EmpID = '" + txtEmpID.Text.Trim() + "'  AND isPrinted = 1 ");  //AND CardStatus = 2
                    if (!DBFun.IsNullOrEmpty(dt) && ddlIssue.SelectedValue == "181")
                    {
                        General.ValidMsg(this, ref cvIssue,true, "This employee has a previous card can not be issued a new card type", "هذا الموظف لديه بطاقة سابقة لا يمكن إصدار بطاقة من النوع جديد");
                        e.IsValid = false;
                        return;
                    }
                    
                    if (DBFun.IsNullOrEmpty(dt) && ddlIssue.SelectedValue != "181")
                    {
                        General.ValidMsg(this, ref cvIssue,true, "This employee does not have a card can not be issued previous card of the specified type", "هذا الموظف ليس لديه بطاقة سابقة لا يمكن إصدار بطاقة من النوع المحدد");
                        e.IsValid = false;
                        return;
                    }
                }
            }
        }
        catch { e.IsValid = false; }
    }

    #endregion
    /*###############################################################################################################################*/
    /*###############################################################################################################################*/
    
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}