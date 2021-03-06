﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_VisitorsSideMenu : System.Web.UI.UserControl
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        FormSession.FillSession("",null);
        PermSideMenu();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnSideMenu_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        Response.Redirect(lb.PostBackUrl);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void PermSideMenu()
    {
        btnNewVisitors.Enabled          = FormSession.getPerm("IVis");
        btnUpdateVisitors.Enabled       = FormSession.getPerm("UVis");
        btnImportVisitors.Enabled       = FormSession.getPerm("ImpVis");
        btnImportImagesVisitors.Enabled = FormSession.getPerm("ImpVis");
        btnPrintCards.Enabled           = FormSession.getPerm("PCrdVis"); 
        btnTemplatesCard.Enabled        = FormSession.getPerm("TCrdVis");   
        btnHistoryVisitors.Enabled      = FormSession.getPerm("SVis");

        //btnCreateCards.Enabled          = FormSession.getPerm("ICrdVis");       
        //btnCardHistory.Enabled          = FormSession.getPerm("SCrdVis");   
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}