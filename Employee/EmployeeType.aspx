<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeType.aspx.cs" Inherits="Employee_EmployeeType" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../Control/EmployeeSideMenu.ascx" TagName="EmployeeSideMenu" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc2:EmployeeSideMenu ID="EmployeeSideMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnIDSearch">
            <div id="pageDiv" runat="server">
                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                    <tr>
                        <td width="100%" colspan="4">
                            <asp:ValidationSummary ID="vsFetch" runat="server" CssClass="errorValidation" EnableClientScript="False"
                                ValidationGroup="vgFetch" meta:resourcekey="vsFetchResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td2Allalign" colspan="4">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <span class="requiredStar">*</span>
                                        <asp:Label ID="lblSearchBy" runat="server" Text="Search By :" 
                                            meta:resourcekey="lblSearchByResource1"></asp:Label>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" Width="173px" 
                                            meta:resourcekey="ddlSearchByResource1">
                                            <asp:ListItem Text="Employee ID" Value="EmpID" 
                                                meta:resourcekey="ListItemResource1"></asp:ListItem>
                                            <asp:ListItem Text="Employee Name" Value="EmpName" 
                                                meta:resourcekey="ListItemResource2"></asp:ListItem>
                                            <asp:ListItem Text="National\Iqama ID" Value="EmpNationalID" 
                                                meta:resourcekey="ListItemResource3"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIDSearch" runat="server" AutoCompleteType="Disabled" 
                                            Width="280px" meta:resourcekey="txtIDSearchResource1"></asp:TextBox>
                                        &nbsp;
                                    </td>
                                     <td>
                                        <asp:CustomValidator ID="cvIDSearch" runat="server" Text="&lt;img src='../Images/icon/Exclamation.gif' title='!' /&gt;"
                                            ValidationGroup="vgFetch" OnServerValidate="IDSearch_ServerValidate" EnableClientScript="False"
                                            ControlToValidate="txtValid" meta:resourcekey="cvIDSearchResource1"></asp:CustomValidator>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnIDSearch" runat="server" OnClick="btnIDSearch_Click" ValidationGroup="vgFetch"
                                            CssClass="buttonBG" Text="Fetch " Width="80px" 
                                            meta:resourcekey="btnIDSearchResource1" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblEmployeeID" runat="server" Text="Employee ID :" 
                                meta:resourcekey="lblEmployeeIDResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtEmployeeID" runat="server" Enabled="False" AutoCompleteType="Disabled"
                                Width="180px" meta:resourcekey="txtEmployeeIDResource1"></asp:TextBox>
                        </td>
                        <td class="td1Allalign">
                            <asp:Label ID="lblEmpName" runat="server" Text="Employee Name :" 
                                meta:resourcekey="lblEmpNameResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtEmpName" runat="server" Enabled="False" AutoCompleteType="Disabled"
                                Width="180px" meta:resourcekey="txtEmpNameResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblProcessType" runat="server" Text="Process Type :" 
                                meta:resourcekey="lblProcessTypeResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:DropDownList ID="ddlProcessType" runat="server" Width="185px" 
                                meta:resourcekey="ddlProcessTypeResource1">
                                <asp:ListItem Text="-Select Process Type-" Value="0" 
                                    meta:resourcekey="ListItemResource4"></asp:ListItem>
                                <asp:ListItem Text="Transfer to Aramco Employee" Value="Mng" 
                                    meta:resourcekey="ListItemResource5"></asp:ListItem>
                                <asp:ListItem Text="Transfer to Third party" Value="Emp" 
                                    meta:resourcekey="ListItemResource6"></asp:ListItem>
                                <asp:ListItem Text="Transfer to Contractor" Value="Con" 
                                    meta:resourcekey="ListItemResource7"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4" class="borderButton">
                            <asp:Button ID="btnSave" runat="server" CssClass="buttonBG" Text="Save" OnClick="btnSave_Click"
                                ValidationGroup="vgSave" Width="80px" 
                                meta:resourcekey="btnSaveResource1" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Text="Cancel" OnClick="btnCancel_Click"
                                Width="80px" meta:resourcekey="btnCancelResource1" />
                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" 
                                Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>
                            <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" ValidationGroup="ShowMsg"
                                OnServerValidate="ShowMsg_ServerValidate" EnableClientScript="False" 
                                ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="td2Allalign" width="100%">
                            <asp:ValidationSummary ID="vsSave" runat="server" CssClass="MsgValidation" EnableClientScript="False"
                                ValidationGroup="vgSave" meta:resourcekey="vsSaveResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess" EnableClientScript="False"
                                ValidationGroup="vgShowMsg" meta:resourcekey="vsShowMsgResource1" />
                        </td>
                    </tr>
                </table>
            </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
