<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="CrdHome.aspx.cs" Inherits="CrdHome" %>

<%@ Register src="../Control/CardsSideMenu.ascx" tagname="CardsSideMenu" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:CardsSideMenu ID="SideMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

