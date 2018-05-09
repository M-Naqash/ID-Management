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

public partial class ApproveCard : BasePage
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DataTable dt;
    CardsSql SqlClass = new CardsSql();
    SendEmail SE = new SendEmail();

    string MainPer = "Cards";
    string MainName1Ar = "بطاقة";
    string MainName2Ar = "البطاقة";
    string MainNameEn = "Card";

    string MainQuery = " SELECT * FROM CardMaster ";
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //   --------------------Common Code ----------------------------------------------------------------- //
            FormSession.FillSession("Card", pageDiv);
            FormCtrl.RefreshGridEmpty(ref grdData, 20, "No cards for Approve", "لا توجد بطاقات للموافقة عليها");
            hfdLang.Value = FormSession.Language;
            //   --------------------Common Code ----------------------------------------------------------------- //
            if (!IsPostBack)
            {
                MainMasterPage.ShowTitel(General.Msg("Approve Card", "الموافقة على البطاقات"));
                if (!FormSession.PermUsr.Contains("ACrd")) { Response.Redirect(@"~/Login.aspx"); }
                FillDDL();
                FillGrid();
            }
        }
        catch (Exception Ex)
        {
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillDDL()
    {
        //string IsApproved = "-100";
        //if (FormSession.PermUsr.Contains("ACrd")) { IsApproved = "0"; } else if (FormSession.PermUsr.Contains("SACrd")) {IsApproved = "1"; }

        dt = DBFun.FetchData("SELECT DISTINCT EmpID,EmpNameEn,EmpNameAr FROM CardInfoView WHERE CardStatus = 1 AND IsApproved = 0 ");
        if (!DBFun.IsNullOrEmpty(dt)) { FormCtrl.PopulateDDL(ddlEmployee, dt, "EmpName" + General.Lang(), "EmpID", General.Msg("-Select Employee-","-اختر الموظف-")); }

        dt = (DataTable)DBFun.FetchData("SELECT DISTINCT IsID,IsNameEn,IsNameAr FROM CardInfoView WHERE CardStatus = 1 AND IsApproved = 0 ");
        if (!DBFun.IsNullOrEmpty(dt)) { FormCtrl.PopulateDDL(ddlIssue, dt, "IsName" + General.Lang(), "IsID", General.Msg("-Select Issue-","-اختر الإصدار-")); }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        try
        {           
            string CardID = e.CommandArgument.ToString();
            GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            int rowIndex = gvr.RowIndex;
            GridViewRow GDR = grdData.Rows[rowIndex];
            string EmpID = ((LinkButton)GDR.FindControl("btnViewEmp")).Text;

            switch (e.CommandName)
            {
                    
                case ("Approve"):
                    if (FormSession.PermUsr.Contains("ACrd")) { SqlClass.Approved(CardID, FormSession.LoginUsr, DateTime.Now.ToShortDateString(), "1"); }

                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " Approved", MainName2Ar + " مقبولة"));
                    SE.SendMailToEmp(EmpID, CardID, "W", "Approve");
                    break;

                case ("Reject"):
                    string RejectReason = ((TextBox)GDR.FindControl("txtgRejectReason")).Text;
                    if (!string.IsNullOrEmpty(RejectReason))
                    {
                        string confirmValue = Request.Form["confirm_value"];
                        if (confirmValue == "Yes")
                        {
                            if (FormSession.PermUsr.Contains("ACrd")) { SqlClass.Rejected(CardID, FormSession.LoginUsr, DateTime.Now.ToShortDateString(), "-1",RejectReason); }
                            MessageFun.ShowMsg(this, MessageFun.TypeMsg.Warning, General.Msg(MainNameEn + " Rejected", MainName2Ar + " مرفوضة"));
                            SE.SendMailToEmp(EmpID, CardID, "W", "Reject");
                        }
                    }
                    else
                    {
                        MessageFun.ShowMsg(this, MessageFun.TypeMsg.Validation, General.Msg("You must enter the reason for rejection", "يجب إدخال سبب الرفض"));
                    }
                    break;
            }
            FillDDL();
            FillGrid();
        }
        catch (Exception Ex)
        {
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnk = (LinkButton)e.Row.FindControl("btnViewEmp");
                lnk.Attributes.Add("onclick", "EmpView('" + DataBinder.Eval(e.Row.DataItem, "EmpID") + "')");
            }
        }
        catch (Exception e1) { }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSearch_Click(object sender, EventArgs e) { FillGrid(); }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void FillGrid()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT CardID,EmpID,StartDate,ExpiryDate,IsID,EmpType "
                       + " , EmpName" + General.Lang() + " AS EmpName "
                       + " , IsName" + General.Lang() + " AS IssueName "
                       + " FROM CardInfoView "
                       + " WHERE CardStatus = 1 AND IsApproved = 0 ");

            if (ddlEmployee.SelectedIndex > 0) { query.Append(" AND EmpID   = '" + ddlEmployee.SelectedValue + "'"); }
            if (ddlEmpType.SelectedIndex > 0)  { query.Append(" AND EmpType = '" + ddlEmpType.SelectedValue + "'"); }
            if (ddlIssue.SelectedIndex > 0)    { query.Append(" AND IsID    = '" + ddlIssue.SelectedValue + "'"); }

            dt = DBFun.FetchData(query.ToString());
            if (!DBFun.IsNullOrEmpty(dt))
            {
                grdData.DataSource = (DataTable)dt;
                grdData.DataBind();
                divbtn.Visible = true;
            }
            else
            {
                FormCtrl.FillGridEmpty(ref grdData,20,"No cards for Approve","لا توجد بطاقات للموافقة عليها");
                divbtn.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancel_Click(object sender, EventArgs e) { ClearUI(); }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ClearUI()
    {
        ddlEmployee.SelectedIndex = -1;
        ddlEmpType.SelectedIndex = -1;
        ddlIssue.SelectedIndex = -1;

        FillGrid();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdData.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnApproveSelectCards_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            
            bool isCheck = false;
            foreach (GridViewRow row in grdData.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (((CheckBox)(row.FindControl("chkSelect"))).Checked) { isCheck = true;   break; }
                }
            }

            if (!isCheck)
            {
                MessageFun.ShowMsg(this, MessageFun.TypeMsg.Validation, General.Msg("Must select a card at least", "يجب اختيار بطاقة على الأقل"));
                return;
            }
            

            for (int i = 0; i < grdData.Rows.Count; i++)
            {
                GridViewRow gvr = grdData.Rows[i];
                bool isChecked = ((CheckBox)(gvr.FindControl("chkSelect"))).Checked;
                if (isChecked)
                {
                    if (FormSession.PermUsr.Contains("ACrd")) { SqlClass.Approved(gvr.Cells[4].Text, FormSession.LoginUsr, DateTime.Now.ToShortDateString(), "1"); }
                }
            }
            FillDDL();
            FillGrid();
            MessageFun.ShowMsg(this, MessageFun.TypeMsg.Success, General.Msg(MainNameEn + " Approved", MainName2Ar + " مقبولة"));
        }
        catch (Exception Ex)
        {
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnCancelSelectCards_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                bool isEmpty = false;
                bool isCheck = false;
                foreach (GridViewRow row in grdData.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        if (((CheckBox)(row.FindControl("chkSelect"))).Checked)
                        {
                            isCheck = true;
                            if (string.IsNullOrEmpty(((TextBox)row.FindControl("txtgRejectReason")).Text)) { isEmpty = true; break; }
                        }
                    }
                }

                if (!isCheck)
                {
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Validation, General.Msg("Must select a card at least", "يجب اختيار بطاقة على الأقل"));
                    return;
                }


                if (!isEmpty)
                {

                    for (int i = 0; i < grdData.Rows.Count; i++)
                    {
                        GridViewRow gvr = grdData.Rows[i];
                        bool isChecked = ((CheckBox)(gvr.FindControl("chkSelect"))).Checked;
                        if (isChecked)
                        {
                            string RejectReason = ((TextBox)gvr.FindControl("txtgRejectReason")).Text;
                            if (FormSession.PermUsr.Contains("ACrd")) { SqlClass.Rejected(gvr.Cells[5].Text, FormSession.LoginUsr, DateTime.Now.ToShortDateString(), "-1",RejectReason); }
                        }
                    }
                    FillDDL();
                    FillGrid();
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Warning, General.Msg(MainNameEn + " Rejected", MainName2Ar + " مرفوضة"));
                }
                else
                {
                    MessageFun.ShowMsg(this, MessageFun.TypeMsg.Validation, General.Msg("You must enter the reason for rejection", "يجب إدخال سبب الرفض"));
                }
            }
        }
        catch (Exception Ex)
        {
            MessageFun.ShowAdminMsg(this, Ex.Message);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ShowMsg_ServerValidate(Object source, ServerValidateEventArgs e) { e.IsValid = false; }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
