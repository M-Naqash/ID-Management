<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmailConfig.aspx.cs" Inherits="EmailConfig" culture="auto" uiculture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Src="../Control/ConfigurationSideMenu.ascx" TagName="ConfigurationSideMenu" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:ConfigurationSideMenu ID="ConfigurationSideMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMain" runat="server">
            
            <div id="pageDiv" runat="server">
                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                    <tr>
                        <td class="td2Allalign">
                            <table id="itemData" runat="server" class="itemData" cellpadding="1" width="100%">
                                <tr>
                                    <td class="td1Allalign" style="width:25%">
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="lblEmlServerID" runat="server" Text="Server ID :" 
                                            meta:resourcekey="lblEmlServerIDResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtEmlServerID" runat="server" AutoCompleteType="Disabled" 
                                            Width="400px" meta:resourcekey="txtEmlServerIDResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvEmlServerID" runat="server" 
                                            ControlToValidate="txtEmlServerID" EnableClientScript="False" 
                                            Text="&lt;img src='../App_Themes/ThemeEn/Images/Validation/Exclamation.gif' title='Server ID is required' /&gt;" 
                                            ValidationGroup="vgSave" meta:resourcekey="rvEmlServerIDResource1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="lblEmlPortNo" runat="server" Text="Port No. :" 
                                            meta:resourcekey="lblEmlPortNoResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                         <asp:TextBox ID="txtEmlPortNo" runat="server" AutoCompleteType="Disabled" 
                                             Width="400px" meta:resourcekey="txtEmlPortNoResource1"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="rvEmlPortNo" runat="server" 
                                            ControlToValidate="txtEmlPortNo" EnableClientScript="False" 
                                            Text="&lt;img src='../App_Themes/ThemeEn/Images/Validation/Exclamation.gif' title=Port No. is required' /&gt;" 
                                            ValidationGroup="vgSave" meta:resourcekey="rvEmlPortNoResource1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="lblEmlSenderEmail" runat="server" Text="Sender Email ID :" 
                                            meta:resourcekey="lblEmlSenderEmailResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtEmlSenderEmail" runat="server" AutoCompleteType="Disabled" 
                                            Width="400px" meta:resourcekey="txtEmlSenderEmailResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvEmlSenderEmail" runat="server" 
                                            ControlToValidate="txtEmlSenderEmail" EnableClientScript="False" 
                                            Text="&lt;img src='../App_Themes/ThemeEn/Images/Validation/Exclamation.gif' title='Sender Email ID is required' /&gt;" 
                                            ValidationGroup="vgSave" meta:resourcekey="rvEmlSenderEmailResource1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr >
                                    <td class="td1Allalign">
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="lblEmlSenderPassword" runat="server" 
                                            Text="Sender Email Password :" 
                                            meta:resourcekey="lblEmlSenderPasswordResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtEmlSenderPassword" runat="server" 
                                            AutoCompleteType="Disabled" Width="400px" TextMode="Password" 
                                            meta:resourcekey="txtEmlSenderPasswordResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvEmlSenderPassword" runat="server" 
                                            ControlToValidate="txtEmlSenderPassword" EnableClientScript="False" 
                                            Text="&lt;img src='../App_Themes/ThemeEn/Images/Validation/Exclamation.gif' title='Sender Email Password is required' /&gt;" 
                                            ValidationGroup="vgSave" meta:resourcekey="rvEmlSenderPasswordResource1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign"></td>   
                                    <td class="td2Allalign">
                                        <asp:CheckBox ID="cbEmlCredential" runat="server" Text="Enable Credentials" 
                                            meta:resourcekey="cbEmlCredentialResource1"/>
                                    </td>
                                </tr> 
                                <tr>
                                    <td class="td1Allalign"></td>   
                                    <td class="td2Allalign">
                                        <asp:CheckBox ID="cbEmlSsl" runat="server" Text="Enable SSL" 
                                            meta:resourcekey="cbEmlSslResource1"/>
                                    </td>
                                </tr> 

                                <tr><td style="height: 10px" colspan="2"></td></tr>

                                <tr>
                                    <td colspan="2">
                                        <table width="100%" class="rp_Title">
                                            <tr>
                                                <td style="width:100%; text-align:center; vertical-align:middle; margin:auto auto auto auto;">
                                                    <asp:Label ID="lblTitel2" runat="server" 
                                                        Text="Notification Setting of End Cards" meta:resourcekey="lblTitel2Resource1"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td class="td1Allalign">
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="lblEmlCountDaysForSend" runat="server" Text="Send Email befor :" 
                                            meta:resourcekey="lblEmlCountDaysForSendResource1"></asp:Label>  
                                    </td>  
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtEmlCountDaysForSend" runat="server" Enabled="False" 
                                            Width="400px" onkeypress="return NumberOnly(event);" 
                                            meta:resourcekey="txtEmlCountDaysForSendResource1"></asp:TextBox>
                                        <asp:CustomValidator id="cvCountDaysForSend" runat="server"
                                            Text="&lt;img src='../App_Themes/ThemeEn/Images/Validation/Exclamation.gif' title='' /&gt;"
                                            ValidationGroup="vgSave" OnServerValidate="CountDaysForSend_ServerValidate"
                                            EnableClientScript="False" ControlToValidate="txtValid" 
                                            meta:resourcekey="cvCountDaysForSendResource1"></asp:CustomValidator>
                                            &nbsp;
                                            <asp:Label ID="lblDays" runat="server" 
                                            Text="Day\Days From the end of the card" meta:resourcekey="lblDaysResource1"></asp:Label> 
                                    </td>     
                                </tr>  

                                <tr><td style="height: 10px" colspan="2"></td></tr>

                                <tr>
                                    <td colspan="2">
                                        <table width="100%" class="rp_Title">
                                            <tr>
                                                <td style="width:100%; text-align:center; vertical-align:middle; margin:auto auto auto auto;">
                                                    <asp:Label ID="lblTitle" runat="server" Text="Send Test Email" 
                                                        meta:resourcekey="lblTitleResource1"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td class="td1Allalign">
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="lblSendToEmail" runat="server" Text="Send To Email :" 
                                            meta:resourcekey="lblSendToEmailResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSendToEmail" runat="server" AutoCompleteType="Disabled" 
                                            Width="250px" meta:resourcekey="txtSendToEmailResource1"></asp:TextBox>
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnSend" runat="server" CssClass="buttonBG" Text="Save" 
                                            OnClick="btnSend_Click" ValidationGroup="Save" Width="80px" 
                                            meta:resourcekey="btnSendResource1"></asp:Button>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:TextBox ID="txtLogSend" runat="server" AutoCompleteType="Disabled" 
                                            Width="70%" Height="200px" TextMode="MultiLine" ReadOnly="True" 
                                            meta:resourcekey="txtLogSendResource1"></asp:TextBox>
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="middle" class="borderButton">
                            &nbsp;&nbsp;
                            <asp:Button ID="btnSave" runat="server" CssClass="buttonBG" Text="Save" 
                                OnClick="btnSave_Click" ValidationGroup="vgSave" Width="80px" 
                                meta:resourcekey="btnSaveResource1"/>
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Text="Cancel" 
                                OnClick="btnCancel_Click" Width="80px" meta:resourcekey="btnCancelResource1"/>
                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" 
                                Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>
                            <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                                ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                EnableClientScript="False" ControlToValidate="txtValid" 
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

