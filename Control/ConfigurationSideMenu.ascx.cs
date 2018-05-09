using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ConfigurationSideMenu : System.Web.UI.UserControl
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        FormSession.FillSession("",null);
        PermSideMenu();
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSideMenu_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        Response.Redirect(lb.PostBackUrl);
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void PermSideMenu()
    {
        btnCreateBlackList.Enabled = FormSession.getPerm("IBla");
        btnUpdateBlackList.Enabled = FormSession.getPerm("UBla");
        btnDeleteBlackList.Enabled = FormSession.getPerm("DBla");
        btnSearchBlackList.Enabled = FormSession.getPerm("SBla");

        btnCreateCompanies.Enabled = FormSession.getPerm("ICompanies");
        btnUpdateCompanies.Enabled = FormSession.getPerm("UCompanies");
        btnDeleteCompanies.Enabled = FormSession.getPerm("DCompanies");

        btnCreateSections.Enabled = FormSession.getPerm("ISections");
        btnUpdateSections.Enabled = FormSession.getPerm("USections");
        btnDeleteSections.Enabled = FormSession.getPerm("DSections");

        btnCreateNationality.Enabled = FormSession.getPerm("INat");
        btnUpdateNationality.Enabled = FormSession.getPerm("UNat");
        btnDeleteNationality.Enabled = FormSession.getPerm("DNat");

        btnEmailConfig.Enabled = FormSession.PermUsr.Contains("UEml");
        btnSettingCompany.Enabled = FormSession.PermUsr.Contains("UConfig");
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void RedirectPage()
    {
        FormSession.FillSession("",null);
        PermSideMenu();
        foreach (Control ctrl in this.Controls)
        {
            if (ctrl is LinkButton) 
            {  
                LinkButton lb = (LinkButton)ctrl;
                string id = lb.ID; 
                if (lb.Enabled) { Response.Redirect(lb.PostBackUrl); break; }
            }
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}