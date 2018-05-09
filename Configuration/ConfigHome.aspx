<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="ConfigHome.aspx.cs" Inherits="ConfigHome" %>

<%@ Register src="../Control/ConfigurationSideMenu.ascx" tagname="ConfigurationSideMenu" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ConfigurationSideMenu ID="SideMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

