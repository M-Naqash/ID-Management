using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class PermissionsCtl : System.Web.UI.UserControl
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private bool _ShowRole;
    public  bool ShowRole { get { return _ShowRole; } set { if (_ShowRole != value) { _ShowRole = value; } } }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e) 
    {
        if (!IsPostBack)
        {
            divRole.Visible = true;
            if (ShowRole) { ddlRole.Visible = true; } else { ddlRole.Visible = false; }
            DataTable dt = DBFun.FetchData(" SELECT * FROM RoleUserPermissions ");
            if (!DBFun.IsNullOrEmpty(dt)) { FormCtrl.PopulateDDL(ddlRole, dt, "RoleName"  + General.Lang(), "RolePermissions", General.Msg("-Select Role Permissions-","-اختر مجموعة الصلاحيات-"));  }
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string getPermissions()
    {
        StringBuilder permStr = new StringBuilder();
        Control con = this;
        foreach (Control ctrl in con.Controls)
        {
            if (ctrl is CheckBoxList)
            {
                CheckBoxList CBL = ctrl as CheckBoxList;
                if (CBL != null)
                {
                    if (CBL.SelectedIndex > -1) 
                    {
                        if (CBL.ID == "cblUsr") { permStr.Append(",Users"); }
                        if (CBL.ID.Contains("Employees") && !permStr.ToString().Contains(",Employees")) { permStr.Append(",Employees"); }
                        if (CBL.ID.Contains("Cards")  && !permStr.ToString().Contains(",Card")) { permStr.Append(",Card"); }
                        if (CBL.ID.Contains("Visitors") && !permStr.ToString().Contains(",Visitors")) { permStr.Append(",Visitors"); }
                        if (CBL.ID == "cblRep" && !permStr.ToString().Contains(",Reports")) { permStr.Append(",Reports"); }
                        if (CBL.ID.Contains("Config") && !permStr.ToString().Contains(",Config")) { permStr.Append(",Config"); }
                    }
                    for (int i = 0; i < CBL.Items.Count; i++) { if (CBL.SelectedIndex > -1) { if (CBL.Items[i].Selected) { permStr.Append("," + CBL.Items[i].Value); } } }
                }
            }
            //else if (ctrl is DropDownList)
            //{
            //    DropDownList ddl = ctrl as DropDownList;
            //    if (ddl != null)
            //    {
            //        if (ddl.SelectedIndex > 0)
            //        {
            //            if (ddl.ID == "ddlApproveCards") { permStr.Append(",Crd"); }
            //            if (ddl.ID == "ddlApproveLic") { permStr.Append(",PLic"); }
            //        }
            //        permStr.Append("," + ddl.SelectedValue);
            //    }
            //}
        }

        return permStr.ToString();
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void PopulatePermissions(string pPer,string pUsr)
    {
        string PermCrypt = CryptorEngine.Decrypt(pPer, true);
        string[] PermStr = PermCrypt.Split(',');
        
        Control con = this;
        foreach (Control ctrl in con.Controls)
        {
            if (ctrl is CheckBoxList)
            {
                CheckBoxList CBL = ctrl as CheckBoxList;
                if (CBL != null)
                {
                    for (int i = 0; i < CBL.Items.Count; i++)
                    {
                        if (pUsr == "admin" || pUsr == "مدير النظام") { CBL.Items[i].Selected = true; } else { CBL.Items[i].Selected = PermStr.Contains(CBL.Items[i].Value); }
                    }
                }
            }
            else if (ctrl is DropDownList)
            {
                DropDownList ddl = ctrl as DropDownList;
                if (ddl != null) {  for (int i = 0; i < ddl.Items.Count; i++) { ddl.Items[i].Selected = PermStr.Contains(ddl.Items[i].Value); } }
            }
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Clear() { Clear(this); }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Clear(Control con)
    {
        foreach (Control ctrl in con.Controls)
        {
            if (ctrl is CheckBoxList)
            {
                CheckBoxList CBL = ctrl as CheckBoxList;
                if (CBL != null) { CBL.SelectedIndex = -1; }
            }
            else if (ctrl is DropDownList)
            {
                DropDownList ddl = ctrl as DropDownList;
                if (ddl != null ) 
                {
                    if (ddl.ID != "ddlRole") { ddl.SelectedIndex = -1; }
                }
            }
            else
            {
                if (ctrl.Controls.Count > 0) { Clear(ctrl); }
            }
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void EnablePermissions(bool cblStatus,bool ddlStatus) { EnablePermissions(this,cblStatus, ddlStatus); }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void EnablePermissions(Control con,bool cblStatus,bool ddlStatus)
    {
        foreach (Control ctrl in con.Controls)
        {
            if (ctrl is CheckBoxList)
            {
                CheckBoxList CBL = ctrl as CheckBoxList;
                if (CBL != null) { CBL.Enabled = cblStatus; }
            }
            else if (ctrl is DropDownList)
            {
                DropDownList ddl = ctrl as DropDownList;
                if (ddl != null ) 
                {
                    if (ddl.ID == "ddlRole") { ddl.Enabled = cblStatus; } else {  ddl.Enabled = ddlStatus; }
                }
            }
            else if (ctrl is ImageButton)
            {
                ImageButton imb = ctrl as ImageButton;
                if (imb != null ) { imb.Enabled = cblStatus; }
            }
                
            else
            {
                if (ctrl.Controls.Count > 0) { EnablePermissions(ctrl,cblStatus, ddlStatus); }
            }
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear();
        if (ddlRole.SelectedIndex > 0) { PopulatePermissions(ddlRole.SelectedValue, ddlRole.SelectedItem.Text); }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void CheckAll_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imb = sender as ImageButton;
        bool check = true;
        if ( imb.ID == "imbCheckAll")   { check = true;  }
        if ( imb.ID == "imbUnCheckAll") { check = false;  }

        foreach (Control ctrl in this.Controls)
        {
            if (ctrl is CheckBoxList)
            {
                CheckBoxList CBL = ctrl as CheckBoxList;
                if (CBL != null) { for (int i = 0; i < CBL.Items.Count; i++) { CBL.Items[i].Selected = check; } }
            }
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}