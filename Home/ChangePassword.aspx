<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%--<%@ Register src="../Control/HomeSideMenu.ascx" tagname="HomeSideMenu" tagprefix="uc2" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <uc2:HomeSideMenu ID="HomeSideMenu1" runat="server" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="pageDiv" runat="Server">
        <table border="0" cellpadding="0" cellspacing="4" width="100%">
            <tr>
                <td class="td1Allalign">
                    <span class="requiredStar">*</span>
                    <asp:Label ID="lblOldPassword" runat="server" Text="Old Password :" 
                        meta:resourcekey="lblOldPasswordResource1"></asp:Label>
                </td>
                <td class="td2Allalign">
                    <asp:TextBox ID="txtOldpassword" runat="server" TextMode="Password" 
                        AutoCompleteType="Disabled" Width="180px" 
                        meta:resourcekey="txtOldpasswordResource1"></asp:TextBox>
                    <asp:CustomValidator id="cvOld" runat="server"
                        ValidationGroup="vgSave" OnServerValidate="OldValidate_ServerValidate"
                        EnableClientScript="False" ControlToValidate="cvtxt" 
                        meta:resourcekey="cvOldResource1"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="td1Allalign">
                    <span class="requiredStar">*</span>
                    <asp:Label ID="lblNewPassword" runat="server" Text="New Password :" 
                        meta:resourcekey="lblNewPasswordResource1"></asp:Label>
                </td>
                <td class="td2Allalign">
                    <asp:TextBox ID="txtNewpassword" runat="server" TextMode="Password" 
                        AutoCompleteType="Disabled" Width="180px" 
                        meta:resourcekey="txtNewpasswordResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvtxtNewpassword" runat="server" ControlToValidate="txtNewpassword"
                        EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='New Password is required!' /&gt;"
                        ValidationGroup="vgSave" meta:resourcekey="rvtxtNewpasswordResource1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="td1Allalign">
                    <span class="requiredStar">*</span>
                    <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password :" 
                        meta:resourcekey="lblConfirmPasswordResource1" ></asp:Label>
                </td>
                <td class="td2Allalign">
                    <asp:TextBox ID="txtConfirmpassword" runat="server" TextMode="Password" 
                        AutoCompleteType="Disabled" Width="180px" 
                        meta:resourcekey="txtConfirmpasswordResource1"></asp:TextBox>
                    <asp:CustomValidator id="cvConfirm" runat="server"
                        ValidationGroup="vgSave" OnServerValidate="ConfirmValidate_ServerValidate"
                        EnableClientScript="False" ControlToValidate="cvtxt" 
                        meta:resourcekey="cvConfirmResource1"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle" colspan="2" class="borderButton">
                    <center>
                        <asp:Button ID="btnUpdate" runat="server" CssClass="buttonBG" Text="Update" 
                            OnClick="btnUpdate_Click" ValidationGroup="vgSave" Width="80px" 
                            meta:resourcekey="btnUpdateResource1"/>
                        <asp:TextBox ID="cvtxt" runat="server" Text="02120" Visible="False" Width="10px"></asp:TextBox>
                        <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                            EnableClientScript="False" ControlToValidate="cvtxt">
                        </asp:CustomValidator>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="td2Allalign" width="100%">
                    <asp:ValidationSummary ID="vsSave"    runat="server" CssClass="MsgValidation" EnableClientScript="False" ValidationGroup="vgSave"/>
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"    EnableClientScript="False" ValidationGroup="vgShowMsg"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
