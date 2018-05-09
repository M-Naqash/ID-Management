<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeesHome.aspx.cs" Inherits="Employee_EmployeesHome" %>

<%@ Register Src="../Control/EmployeeSideMenu.ascx" TagName="EmployeeSideMenu" TagPrefix="CSM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <CSM:EmployeeSideMenu ID="SideMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

