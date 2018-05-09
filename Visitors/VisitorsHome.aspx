<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="VisitorsHome.aspx.cs" Inherits="Visitors_VisitorsHome" %>

<%@ Register Src="../Control/VisitorsSideMenu.ascx" TagName="VisitorsSideMenu" TagPrefix="CSM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <CSM:VisitorsSideMenu ID="SideMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

