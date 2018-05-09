<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true"
    CodeFile="ApplicationUsers.aspx.cs" Inherits="ApplicationUsers" meta:resourcekey="PageResource1" culture="auto" uiculture="auto" %>

<%@ Register Src="../Control/UsersSideMenu.ascx" TagName="UsersSideMenu" TagPrefix="uc2" %>
<%@ Register Src="../Control/PermissionsCtl.ascx" TagName="PermissionsCtl" TagPrefix="uc3" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagPrefix="uc" TagName="Calendar2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc2:UsersSideMenu ID="UsersSideMenu1" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="pageDiv" runat="server">
                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                    <tr>
                        <td align="center" >
                            <table width="70%">
                                <div id="divUpdDel" runat="server" visible="False">
                                    <tr>
                                        <td class="td1Allalign">
                                            <span class="requiredStar">*</span>
                                            <asp:Label ID="lblUsrLoginIDSearch" runat="server" Text="Login ID :" 
                                                meta:resourcekey="lblUsrLoginIDSearchResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:DropDownList ID="ddlUsrLoginID" runat="server" Width="173px" 
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlUsrLoginID_SelectedIndexChanged" 
                                                meta:resourcekey="ddlUsrLoginIDResource1">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rvUsrLoginIDSearch" runat="server" 
                                                ControlToValidate="ddlUsrLoginID" ValidationGroup="vgSave"
                                                EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='User ID is required!' /&gt;"
                                                 Enabled="False" meta:resourcekey="rvUsrLoginIDSearchResource1"></asp:RequiredFieldValidator>
                                        </td>
                                        <td ></td>
                                        <td></td>
                                    </tr>
                                </div>
                                <tr>
                                    <td class="td1Allalign">
                                        <span class="requiredStar">*</span>
                                        <asp:Label ID="lblUserLoginID" runat="server" Text="Login ID :" 
                                            meta:resourcekey="lblUserLoginIDResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtUsrLoginID" runat="server" AutoCompleteType="Disabled" 
                                            Width="168px" meta:resourcekey="txtUsrLoginIDResource1"></asp:TextBox>
                                        <asp:CustomValidator id="cvUsrLoginID" runat="server"
                                            Text="&lt;img src='../Images/icon/Exclamation.gif' title='Login ID is required!' /&gt;" 
                                            ValidationGroup="vgSave"
                                            OnServerValidate="UsrLoginID_ServerValidate"
                                            EnableClientScript="False" 
                                            ControlToValidate="cvtxt" meta:resourcekey="cvUsrLoginIDResource1"></asp:CustomValidator>
                                    </td>
                                    <td class="td1Allalign">
                                        <span class="requiredStar">*</span>
                                        <asp:Label ID="lblUsrPassword" runat="server" Text="Password :" 
                                            meta:resourcekey="lblUsrPasswordResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtUsrPassword" runat="server" AutoCompleteType="Disabled" 
                                            Width="168px" TextMode="Password" meta:resourcekey="txtUsrPasswordResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvUsrPassword" runat="server" 
                                            ControlToValidate="txtUsrPassword" ValidationGroup="vgSave"
                                            EnableClientScript="False" 
                                            Text="&lt;img src='../Images/icon/Exclamation.gif' title='Password is required!' /&gt;" 
                                            meta:resourcekey="rvUsrPasswordResource1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <span class="requiredStar">*</span>
                                        <asp:Label ID="lblUsrFullName" runat="server" Text="Full Name :" 
                                            meta:resourcekey="lblUsrFullNameResource1"></asp:Label>
                                    </td>
                                    <td class="td2align">
                                        <asp:TextBox ID="txtUsrFullName" runat="server" AutoCompleteType="Disabled" 
                                            Width="168px" meta:resourcekey="txtUsrFullNameResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvUsrFullName" runat="server" 
                                            ControlToValidate="txtUsrFullName" ValidationGroup="vgSave"
                                            EnableClientScript="False" 
                                            Text="&lt;img src='../Images/icon/Exclamation.gif' title='Full Name is required!' /&gt;" 
                                            meta:resourcekey="rvUsrFullNameResource1"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="td1Allalign" height="20">
                                        <asp:Label ID="lblUsrEmailID" runat="server" Text="Email ID :" 
                                            meta:resourcekey="lblUsrEmailIDResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtUsrEmailID" runat="server" AutoCompleteType="Disabled" 
                                            Width="168px" meta:resourcekey="txtUsrEmailIDResource1"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="rvUsrEmailID"  runat="server" ErrorMessage="Please enter email in correct format"
                                            Text="&lt;img src='../Images/icon/Exclamation.gif' title='Please enter email in correct format!' /&gt;"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                            ControlToValidate="txtUsrEmailID" EnableClientScript="False" 
                                            ValidationGroup="vgSave" meta:resourcekey="rvUsrEmailIDResource1"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <span id="spnUsrStartDate" runat="server" class="requiredStar">*</span>
                                        <asp:Label ID="lblUsrStartDate" runat="server" Text="Start Date :" 
                                            meta:resourcekey="lblUsrStartDateResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <uc:Calendar2 ID="calUsrStartDate" runat="server" CalendarType="System" ValidationGroup="vgSave"/>
                                                </td>
                                                <td> 
                                                    <asp:CustomValidator id="cvCompareDates" runat="server"
                                                        Text="&lt;img src='../Images/icon/Exclamation.gif' title='start date more than end date!' /&gt;" 
                                                        ValidationGroup="vgSave"
                                                        ErrorMessage="start date more than end date!"
                                                        OnServerValidate="DateValidate_ServerValidate"
                                                        EnableClientScript="False" 
                                                        ControlToValidate="cvtxt" meta:resourcekey="cvCompareDatesResource1"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="td1Allalign">
                                        <span id="spnUsrExpiryDate" runat="server" class="requiredStar">*</span>
                                        <asp:Label ID="lblUsrExpiryDate" runat="server" Text="End Date :" 
                                            meta:resourcekey="lblUsrExpiryDateResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <uc:Calendar2 ID="calUsrExpiryDate" runat="server" CalendarType="System" ValidationGroup="vgSave"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <span class="requiredStar">*</span>
                                        <asp:Label ID="lblUsrStatus" runat="server" Text="Status :" 
                                            meta:resourcekey="lblUsrStatusResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:DropDownList ID="ddlUsrStatus" runat="server" Width="173px" 
                                            meta:resourcekey="ddlUsrStatusResource1"></asp:DropDownList>
                                    </td>
                                    <td class="td1Allalign">
                                       
                                    </td>
                                    <td class="td2Allalign">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblUsrDescription" runat="server" Text="Description :" 
                                            meta:resourcekey="lblUsrDescriptionResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign" colspan="3">
                                        <asp:TextBox ID="txtUsrDescription" runat="server" Width="520px" 
                                            AutoCompleteType="Disabled" meta:resourcekey="txtUsrDescriptionResource1"></asp:TextBox>
                                    </td>
                                </tr>
                    </table>
                </td>
            </tr>


            <tr>
                <td align="center">
                    <uc3:PermissionsCtl ID="PermCtl" runat="server" ShowRole="true" />
                </td>
            </tr>

            <tr>
                <td align="center" class="borderButton">
                    <asp:Button ID="btnSave" runat="server" CssClass="buttonBG" Text="Save" 
                        OnClick="btnSave_Click" ValidationGroup="vgSave" Width="80px" 
                        meta:resourcekey="btnSaveResource1">
                    </asp:Button>
                    <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Text="Cancel" 
                        OnClick="btnCancel_Click" Width="80px" meta:resourcekey="btnCancelResource1"/>
                    <asp:TextBox ID="cvtxt" runat="server" Text="02120" Visible="False" 
                        Width="10px" meta:resourcekey="cvtxtResource1"></asp:TextBox>
                    <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                        ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                        EnableClientScript="False" ControlToValidate="cvtxt" 
                        meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                </td>
            </tr>

            <tr>
                <td colspan="2" class="td2Allalign" width="100%">
                    <asp:ValidationSummary ID="vsSave"    runat="server" CssClass="MsgValidation" 
                        EnableClientScript="False" ValidationGroup="vgSave" 
                        meta:resourcekey="vsSaveResource1"/>
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"    
                        EnableClientScript="False" ValidationGroup="vgShowMsg" 
                        meta:resourcekey="vsShowMsgResource1"/>
                </td>
            </tr>

    </table>
    </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
