<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true"
    CodeFile="ReportsMain.aspx.cs" Inherits="Reports_ReportsMain" Culture="auto"
    UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Src="~/Control/Calendar2.ascx" TagPrefix="uc" TagName="Calendar2" %>
<%@ Register Assembly="Stimulsoft.Report.WebFx" Namespace="Stimulsoft.Report.WebFx"
    TagPrefix="cc1" %>
<%@ Register Assembly="Stimulsoft.Report.WebDesign" Namespace="Stimulsoft.Report.Web"
    TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../JScript/sdmenu.js"></script>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="float: left; width: 155px" id="my_menu" class="sdmenu1" onclick="loadfun('my_menu');">
                <div>
                    <span>
                        <center>
                            <asp:Literal ID="litReportsGroups" runat="server" Text="Reports Groups" meta:resourcekey="litReportsGroupsResource1"></asp:Literal>
                        </center>
                    </span>
                    <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;"
                        cellspacing="0">
                        <tr>
                            <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                                <asp:ListBox ID="lstReportsGroups" runat="server" Width="150px" Height="110px" AutoPostBack="True"
                                    OnSelectedIndexChanged="lstReportsGroups_SelectedIndexChanged" CssClass="myListBox"
                                    Rows="20" meta:resourcekey="lstReportsGroupsResource1"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnViewreport" />
            <asp:PostBackTrigger ControlID="btnEditReport" />
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="lstReport" />
        </Triggers>
        <ContentTemplate>
            <div id="pageDiv" runat="server" class="PageDir">
                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                    <tr>
                        <td>
                            <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;"
                                cellspacing="0">
                                <tr>
                                    <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                                        <asp:ListBox ID="lstReport" runat="server" Width="100%" Height="110px" AutoPostBack="True"
                                            OnSelectedIndexChanged="lstReport_SelectedIndexChanged" CssClass="myListBox"
                                            Rows="20" meta:resourcekey="lstReportResource1"></asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: 2px outset; height: 50px;" width="100%">
                            <table width="100%">
                                <tr>
                                    <td width="50%">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <h3>
                                                    <asp:Label ID="lblSelectedreport" runat="server" meta:resourcekey="lblSelectedreportResource1"></asp:Label>
                                                </h3>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td width="50%">
                                        <asp:Button ID="btnEditReport" runat="server" Text="Edit Report" CssClass="buttonBG"
                                            OnClick="btnEditReport_Click" Enabled="False" Width="100px" meta:resourcekey="btnEditReportResource1" />
                                        <asp:Button ID="btnSetAsDefault" runat="server" Text="Set As Default" CssClass="buttonBG"
                                            Width="100px" OnClick="btnSetAsDefault_Click" Enabled="False" meta:resourcekey="btnSetAsDefaultResource1" />
                                        <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="buttonBG" OnClick="btnExport_Click"
                                            Enabled="False" Width="100px" Visible="False" meta:resourcekey="btnExportResource1" />
                                        <asp:TextBox ID="cvValid" runat="server" Text="02120" Visible="False" Width="10px"
                                            meta:resourcekey="cvValidResource1"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: 2px outset; height: 200px;" width="100%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <asp:Panel ID="pnlDateFromTo" runat="server" Visible="False" meta:resourcekey="pnlDateFromToResource1">
                                                <tr>
                                                    <td class="td1align" valign="middle">
                                                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date :" meta:resourcekey="lblStartDateResource1"></asp:Label>
                                                    </td>
                                                    <td class="td2align" valign="middle">
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <uc:Calendar2 ID="calStartDate" runat="server" CalendarType="System" />
                                                                </td>
                                                                <td valign="middle">
                                                                    <asp:CustomValidator ID="cvStartDate" runat="server" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Start Date is required!' /&gt;"
                                                                        ValidationGroup="vgView" OnServerValidate="Date_ServerValidate" EnableClientScript="False"
                                                                        ControlToValidate="cvValid" meta:resourcekey="cvStartDateResource1"></asp:CustomValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:CustomValidator ID="cvCompareDates" runat="server" Text="&lt;img src='../Images/icon/Exclamation.gif' title='start date more than end date!' /&gt;"
                                                                        ValidationGroup="vgView" ErrorMessage="start date more than end date!" OnServerValidate="Date_ServerValidate"
                                                                        EnableClientScript="False" ControlToValidate="cvValid" meta:resourcekey="cvCompareDatesResource1"></asp:CustomValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="td1align" valign="middle">
                                                        <asp:Label ID="lblEndDate" runat="server" Text="End Date :" meta:resourcekey="lblEndDateResource1"></asp:Label>
                                                    </td>
                                                    <td class="td2align" valign="middle">
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <uc:Calendar2 ID="calEndDate" runat="server" CalendarType="System" />
                                                                </td>
                                                                <td valign="middle">
                                                                    <asp:CustomValidator ID="cvEndDate" runat="server" Text="&lt;img src='../Images/icon/Exclamation.gif' title='End Date is required!' /&gt;"
                                                                        ValidationGroup="vgView" OnServerValidate="Date_ServerValidate" EnableClientScript="False"
                                                                        ControlToValidate="cvValid" meta:resourcekey="cvEndDateResource1"></asp:CustomValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlEmployee" runat="server" Visible="False" meta:resourcekey="pnlEmployeeResource1">
                                                <tr>
                                                    <td colspan="4">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <span class="requiredStar">*</span>
                                                                    <asp:Label ID="lblIDSearch" runat="server" Text="Select Employee By :" meta:resourcekey="lblIDSearchResource1"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlIDSearch" runat="server" Width="150px" meta:resourcekey="ddlIDSearchResource1">
                                                                        <asp:ListItem Value="EmpID" Text="Employee ID" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                                        <asp:ListItem Value="EmpNameEn" Text="Employee Name (En)" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                                        <asp:ListItem Value="EmpNameAr" Text="Employee Name (Ar)" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtIDSearch" runat="server" Width="150px" meta:resourcekey="txtIDSearchResource1"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:CustomValidator ID="cvIDSearch" runat="server" ValidationGroup="vgView" OnServerValidate="IDSearch_ServerValidate"
                                                                        EnableClientScript="False" ControlToValidate="cvValid" meta:resourcekey="cvIDSearchResource1"></asp:CustomValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlCreatedBy" runat="server" Visible="False" meta:resourcekey="pnlCreatedByResource1">
                                                <tr>
                                                    <td class="td1align" valign="middle">
                                                        <asp:Label ID="lblCreatedBy" runat="server" Text="Created by :" meta:resourcekey="lblCreatedByResource1"></asp:Label>
                                                    </td>
                                                    <td class="td2align" valign="middle">
                                                        <asp:DropDownList ID="ddlCreatedBy" runat="server" Width="168px" meta:resourcekey="ddlCreatedByResource1">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rvCreatedBy" runat="server" EnableClientScript="False"
                                                            ControlToValidate="ddlCreatedBy" ValidationGroup="vgView" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Created By is required!' /&gt;"
                                                            meta:resourcekey="rvCreatedByResource1"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlPrintedBy" runat="server" Visible="False" meta:resourcekey="pnlPrintedByResource1">
                                                <tr>
                                                    <td class="td1align" valign="middle">
                                                        <asp:Label ID="lblPrintedBy" runat="server" Text="Printed By :" meta:resourcekey="lblPrintedByResource1"></asp:Label>
                                                    </td>
                                                    <td class="td2align" valign="middle">
                                                        <asp:DropDownList ID="ddlPrintedBy" runat="server" Width="168px" meta:resourcekey="ddlPrintedByResource1">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rvPrintedBy" runat="server" EnableClientScript="False"
                                                            ControlToValidate="ddlPrintedBy" ValidationGroup="vgView" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Printed By is required' /&gt;"
                                                            meta:resourcekey="rvPrintedByResource1"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlIssue" runat="server" Visible="False" meta:resourcekey="pnlIssueResource1">
                                                <tr>
                                                    <td class="td1align" valign="middle">
                                                        <asp:Label ID="lblIssue" runat="server" Text="Issue Status :" meta:resourcekey="lblIssueResource1"></asp:Label>
                                                    </td>
                                                    <td class="td2align" valign="middle">
                                                        <asp:DropDownList ID="ddlIssue" runat="server" Width="168px" meta:resourcekey="ddlIssueResource1">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rvIssue" runat="server" EnableClientScript="False"
                                                            ControlToValidate="ddlIssue" ValidationGroup="vgView" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Issue Status is required' /&gt;"
                                                            meta:resourcekey="rvIssueResource1"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlEmpType" runat="server" Visible="False" meta:resourcekey="pnlEmpTypeResource1">
                                                <tr>
                                                    <td colspan="4">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblEmpType" runat="server" Text="Employee Type :" meta:resourcekey="lblEmpTypeResource1"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlEmpType" runat="server" Width="150px" OnSelectedIndexChanged="ddlEmpType_SelectedIndexChanged" meta:resourcekey="ddlEmpTypeResource1" AutoPostBack="True">
                                                                        <asp:ListItem Value="All" Text="-All Employee Type-" Selected="True" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                                                        <asp:ListItem Value="Mng" Text="Aramco Employee" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                                                        <asp:ListItem Value="Emp" Text="Third party" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                                                        <asp:ListItem Value="Con" Text="Contractor" meta:resourcekey="ListItemResource8"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <div id="divCompany" runat="server" visible="false">
                                                                    <td class="td1align" valign="middle">
                                                                        <asp:Label ID="lblCompID" runat="server" Text="Company :" meta:resourcekey="lblCompIDResource1"></asp:Label>
                                                                    </td>
                                                                    <td class="td2align" valign="middle">
                                                                        <asp:DropDownList ID="ddlCompID" runat="server" Width="168px" meta:resourcekey="ddlCompIDResource1">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </div>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblHasCard" runat="server" Text="Card Issue Status :" meta:resourcekey="lblHasCardResource1"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlHasCard" runat="server" Width="150px" meta:resourcekey="ddlHasCardResource1">
                                                                        <asp:ListItem Value="A" Text="-All Employees-" Selected="True" meta:resourcekey="ListItemResource9"></asp:ListItem>
                                                                        <asp:ListItem Value="T" Text="They have a card" meta:resourcekey="ListItemResource10"></asp:ListItem>
                                                                        <asp:ListItem Value="F" Text="They don't have card" meta:resourcekey="ListItemResource11"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:ValidationSummary runat="server" ID="vsView" ValidationGroup="vgView" EnableClientScript="False"
                                            CssClass="errorValidation" ShowSummary="False" meta:resourcekey="vsViewResource1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="borderButton">
                                        <asp:Button ID="btnViewreport" runat="server" CssClass="buttonBG" Text="View Report"
                                            Enabled="False" OnClick="btnViewreport_Click" Width="100px" ValidationGroup="vgView"
                                            meta:resourcekey="btnViewreportResource1" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Text="Cancel" Visible="False"
                                            Width="100px" meta:resourcekey="btnCancelResource1" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table border="0" cellpadding="0" cellspacing="4" width="100%">
        <tr>
            <td>
                <cc2:StiWebDesigner ID="StiWebDesigner1" runat="server" OnSaveReport="StiWebDesigner1_SaveReport"
                    OnPreInit="StiWebDesigner1_PreInit" Height="30px" Width="250px" />
            </td>
        </tr>
        <tr>
            <td>
                <cc1:StiWebViewerFx ID="StiWebViewerFx1" runat="server" Width="912px" Height="600px"
                    Background="White" OnPreInit="StiWebViewerFx1_PreInit" />
            </td>
        </tr>
    </table>
</asp:Content>
