<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="SettingCompany.aspx.cs" Inherits="SettingCompany" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../Control/ConfigurationSideMenu.ascx" TagName="ConfigurationSideMenu" TagPrefix="CSM" %>
<%@ Register src="../Control/ImageCtl.ascx" tagname="ImageCtl" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <CSM:ConfigurationSideMenu ID="ConfigurationSideMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnSave">
            <div id="pageDiv" runat="server" class="td2Allalign">
                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                    <tr>             
                        <td valign="middle" align="center">
                            <table width="100%" border="0" cellspacing="0" cellpadding="2" align="center">
                                <tr>
                                    <td class="td1Allalign" style="width:50%">
                                        <span class="requiredStar">*</span>
                                        <asp:Label ID="lblAppCompany" runat="server" Text="Company Name :" 
                                            meta:resourcekey="lblAppCompanyResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtAppCompany" runat="server" Width="168px" 
                                            meta:resourcekey="txtAppCompanyResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvtxtAppCompany" runat="server" ControlToValidate="txtAppCompany"
                                            EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Company Name is required!' /&gt;"
                                            ValidationGroup="vgSave" meta:resourcekey="rvtxtAppCompanyResource1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblAppDisplay" runat="server" Text="Display Name :" 
                                            meta:resourcekey="lblAppDisplayResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtAppDisplay" runat="server" Width="168px" 
                                            meta:resourcekey="txtAppDisplayResource1"></asp:TextBox>
                                    </td>
                                </tr>                              
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblAppAddress1" runat="server" Text="Address 1 :" 
                                            meta:resourcekey="lblAppAddress1Resource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtAppAddress1" runat="server" Width="168px" 
                                            meta:resourcekey="txtAppAddress1Resource1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblAppAddress2" runat="server" Text="Address 1 :" 
                                            meta:resourcekey="lblAppAddress2Resource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtAppAddress2" runat="server" Width="168px" 
                                            meta:resourcekey="txtAppAddress2Resource1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblAppCity" runat="server" Text="City :" 
                                            meta:resourcekey="lblAppCityResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtAppCity" runat="server" Width="168px" 
                                            meta:resourcekey="txtAppCityResource1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblAppCountry" runat="server" Text="Country :" 
                                            meta:resourcekey="lblAppCountryResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtAppCountry" runat="server" Width="168px" 
                                            meta:resourcekey="txtAppCountryResource1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblAppPOBox" runat="server" Text="P.O Box :" 
                                            meta:resourcekey="lblAppPOBoxResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtAppPOBox" runat="server" Width="168px" 
                                            meta:resourcekey="txtAppPOBoxResource1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblAppTelNo1" runat="server" Text="Phone No 1 :" 
                                            meta:resourcekey="lblAppTelNo1Resource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtAppTelNo1" runat="server" Width="168px" 
                                            meta:resourcekey="txtAppTelNo1Resource1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblAppTelNo2" runat="server" Text="Phone No 2 :" 
                                            meta:resourcekey="lblAppTelNo2Resource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtAppTelNo2" runat="server" Width="168px" 
                                            meta:resourcekey="txtAppTelNo2Resource1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblAppFax" runat="server" Text="Fax No :" 
                                            meta:resourcekey="lblAppFaxResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtAppFax" runat="server" Width="168px" 
                                            meta:resourcekey="txtAppFaxResource1"></asp:TextBox>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblAppUrl" runat="server" Text="URL :" 
                                            meta:resourcekey="lblAppUrlResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtAppUrl" runat="server" Width="168px" 
                                            meta:resourcekey="txtAppUrlResource1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblAppEmail" runat="server" Text="Email :" 
                                            meta:resourcekey="lblAppEmailResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtAppEmail" runat="server" Width="168px" 
                                            meta:resourcekey="txtAppEmailResource1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <span class="requiredStar">*</span>
                                        <asp:Label ID="lblAppCalendar" runat="server" Text="Calendar :" 
                                            meta:resourcekey="lblAppCalendarResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:DropDownList ID="ddlAppCalendar" runat="server" Width="173px" 
                                            meta:resourcekey="ddlAppCalendarResource1">
                                            <asp:ListItem Text="-Select Calendar-" Value="0" 
                                                meta:resourcekey="ListItemResource1"></asp:ListItem>
                                            <asp:ListItem Text="Gregorian" Value="G" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                            <asp:ListItem Text="Hijri"     Value="H" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rvddlAppCalendar" runat="server" ControlToValidate="ddlAppCalendar"
                                            EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Calendar is required!' /&gt;"
                                            ValidationGroup="vgSave" InitialValue="0" 
                                            meta:resourcekey="rvddlAppCalendarResource1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        
                        </td>
                        <td width="400px" valign="top" align="center">
                            <table>
                                <tr>
                                    <td>
                                        <uc3:ImageCtl ID="imgLogo" runat="server"  txtID="txtAppCompany"   Type="Logo"
                                            CaptureEnable="false" ValidationGroup="vgSave"  
                                                    TitelEn="Logo"  TitelAr="الشعار"
                                                    EmptyIDMsgEn="Please enter the Company Name to select the image" 
                                            EmptyIDMsgAr="من فضلك ادخل اسم للشركة لاختيار صورة"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="borderButton">
                            <center>
                                <asp:Button ID="btnSave" runat="server" CssClass="buttonBG" Text="Save" OnClick="btnSave_Click" ValidationGroup="vgSave" Width="80px" 
                                    meta:resourcekey="btnSaveResource1"></asp:Button>
                                <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Text="Cancel" OnClick="btnCancel_Click" Width="80px" 
                                    meta:resourcekey="btnCancelResource1"></asp:Button>
                                 <asp:TextBox ID="cvtxt" runat="server" Text="02120" Visible="False" 
                                    Width="10px" meta:resourcekey="cvtxtResource1"></asp:TextBox>
                                <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                                    ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                    EnableClientScript="False" ControlToValidate="cvtxt" 
                                    meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                            </center>
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

