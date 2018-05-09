<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true"
    CodeFile="UserRole.aspx.cs" Inherits="UserRole" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

    <%@ Register src="../Control/UsersSideMenu.ascx" tagname="UsersSideMenu" tagprefix="uc2" %>
    <%@ Register src="../Control/PermissionsCtl.ascx" tagname="PermissionsCtl" tagprefix="uc3" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc2:userssidemenu id="UsersSideMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMain" runat="server">
            <div id="pageDiv" runat="server">
                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                    <div id="divUpdDel" runat="server" visible="False">
                        <tr>
                            <td class="td1Allalign">
                                <span class="requiredStar">*</span>
                                <asp:Label ID="lblRolename" runat="server" Text="Role ID :" 
                                    meta:resourcekey="lblRolenameResource1"></asp:Label>
                            </td>
                            <td class="td2Allalign">
                                <asp:DropDownList ID="ddlRoleID" runat="server" Width="200px" 
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlRoleID_SelectedIndexChanged" 
                                    meta:resourcekey="ddlRoleIDResource1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rvRoleID" runat="server" ControlToValidate="ddlRoleID"
                                    EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Role ID is required!' /&gt;"
                                    ValidationGroup="vgSave" Enabled="False" 
                                    meta:resourcekey="rvRoleIDResource1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </div>
                    
                    <tr>
                        <td class="td1Allalign" style="width:50%">
                            <span class="requiredStar">*</span>
                            <asp:Label ID="lblRoleNameAr" runat="server" Text="Role Name (Ar) :" 
                                meta:resourcekey="lblRoleNameArResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtRoleNameAr" runat="server" AutoCompleteType="Disabled" 
                                Width="195px" meta:resourcekey="txtRoleNameArResource1" onkeypress="return ArabicOnly(event);"></asp:TextBox>
                            <asp:CustomValidator id="cvRoleNameAr" runat="server" Text="&lt;img src='../Images/Icon/Exclamation.gif' title='!' /&gt;"
                                ValidationGroup="vgSave" OnServerValidate="Name_ServerValidate" 
                                EnableClientScript="False" ControlToValidate="cvtxt" 
                                meta:resourcekey="cvRoleNameArResource1"></asp:CustomValidator>
                        </td>
                    </tr>
                           
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblRoleNameEn" runat="server" Text="Role Name (En) :" 
                                meta:resourcekey="lblRoleNameEnResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtRoleNameEn" runat="server" AutoCompleteType="Disabled" onkeypress="return EnglishOnly(event);"
                                Width="195px" meta:resourcekey="txtRoleNameEnResource1"></asp:TextBox>
                            <asp:CustomValidator id="cvRoleNameEn" runat="server" Text="&lt;img src='../Images/Icon/Exclamation.gif' title='!' /&gt;"
                                ValidationGroup="vgSave" OnServerValidate="Name_ServerValidate" 
                                EnableClientScript="False" ControlToValidate="cvtxt" Enabled="False" 
                                meta:resourcekey="cvRoleNameEnResource1"></asp:CustomValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="center" colspan="2">
                            <uc3:permissionsctl id="PermissionsCtl" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td align="center" colspan="2" class="borderButton">
                            <asp:Button ID="btnSave" runat="server" CssClass="buttonBG" Text="Save" 
                                OnClick="btnSave_Click" ValidationGroup="vgSave" Width="80px" 
                                meta:resourcekey="btnSaveResource1" />
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
