<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="UsrHome.aspx.cs" Inherits="UsrHome" %>

<%@ Register src="../Control/UsersSideMenu.ascx" tagname="UsersSideMenu" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <uc1:UsersSideMenu ID="SideMenu" runat="server" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

