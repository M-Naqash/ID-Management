<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="Visitors.aspx.cs" Inherits="Visitors_Visitors" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../Control/VisitorsSideMenu.ascx" TagName="VisitorsSideMenu" TagPrefix="CSM" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagPrefix="uc" TagName="Calendar2" %>
<%@ Register src="../Control/ImageCtl.ascx" tagname="ImageCtl" tagprefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <CSM:VisitorsSideMenu ID="SideMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server"> 
    
    <%--script--%>
    <script type="text/javascript" src="../JScript/AutoComplete.js"></script>
    <%--script--%>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlMain" runat="server">
            <div id="pageDiv" runat="server">
                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                    <tr>
                        <td width="100%" colspan="5" class="td2align">                       
                            <table id="divUpdDel" runat="server" width="100%" >
                                <tr>
                                    <td colspan="2">
                                        <asp:ValidationSummary ID="vsFetch" runat="server" CssClass="MsgValidation" 
                                            EnableClientScript="False" ValidationGroup="vgFetch" 
                                            meta:resourcekey="vsFetchResource1"/>
                                    </td>
                                </tr>   
                                <tr>
                                    <td class="td1Allalign">
                                        <span class="requiredStar">*</span>
                                        <asp:Label ID="lblSearchBy" runat="server" Text="Search by:" meta:resourcekey="lblSearchByResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        &nbsp;
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" Width="200px"
                                            meta:resourcekey="ddlSearchByResource1" AutoPostBack="True" 
                                            onselectedindexchanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Text="Identity No."       Value="VisIdentityNo" Selected="True" meta:resourcekey="SearchListItemResource1"></asp:ListItem>
                                            <asp:ListItem Text="Visitor Name (Ar)"  Value="VisNameAr"             meta:resourcekey="SearchListItemResource2"></asp:ListItem>
                                            <asp:ListItem Text="Visitor Name (En)"  Value="VisNameEn"             meta:resourcekey="SearchListItemResource3"></asp:ListItem>
                                            <asp:ListItem Text="Mobile No."         Value="VisMobileNo"           meta:resourcekey="SearchListItemResource4"></asp:ListItem>
                                        </asp:DropDownList>
                                    &nbsp;
                                        <asp:TextBox ID="txtSearchBy" runat="server" meta:resourcekey="txtSearchResource1" Width="200px"></asp:TextBox>
                                        <asp:Panel runat="server" ID="pnlSearchBy" Height="200px" Width="200px" ScrollBars="Vertical" />
                                        <ajaxToolkit:AutoCompleteExtender
                                            runat="server" 
                                            ID="auSearchBy" 
                                            TargetControlID="txtSearchBy"
                                            ServicePath="~/AutoComplete.asmx" 
                                            ServiceMethod="GetEmpIDList"
                                            MinimumPrefixLength="1" 
                                            CompletionInterval="1000"
                                            EnableCaching="true"
                                            OnClientItemSelected="SearchItemSelected"
                                            CompletionListElementID="pnlSearchBy"
                                            CompletionSetCount="12" />
                                        <asp:CustomValidator ID="cvSearchBy" runat="server" Text="&lt;img src='../Images/icon/Exclamation.gif' title='' /&gt;"
                                            ValidationGroup="vgFetch" OnServerValidate="SearchBy_ServerValidate" EnableClientScript="False"
                                            ControlToValidate="txtValid" meta:resourcekey="cvSearchByResource1"></asp:CustomValidator>
                                    &nbsp;
                                        <asp:Button ID="btnIDSearch" runat="server" OnClick="btnIDSearch_Click" 
                                            ValidationGroup="vgFetch" CssClass="buttonBG" Text="Search" Width="80px" meta:resourcekey="btnIDSearchResource1"/>
                                    </td>
                                </tr>   
                            </table>        
                        </td>
                    </tr>
         
                    <tr>
                        <td colspan="4" valign="top" width="100%">
                            <table>
                                <div id="divCardCount" runat="server" visible="False" width="100%"> 
                                    <tr>
                                        <td class="td2Allalign" colspan="2"  >
                                           <table style="height:20px; width: 198%;">
                                               <tr>
                                                    <td class="MsgLabelInfo" style="height:30px;width:100%">
                                                        <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                                                    </td>
                                               </tr>
                                           </table>
                                        </td>
                                    </tr>
                                </div>
                                
                                <tr>
                                    <td class="td1Allalign">
                                         <asp:Label ID="lblIdentityNo" runat="server" Text="Identity No. :" 
                                             meta:resourcekey="lblIdentityNoResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign" colspan="3">
                                        <asp:TextBox ID="txtVisIdentityNo" runat="server" AutoCompleteType="Disabled" onkeypress="return NumberOnly(event);"
                                            Enabled="False" Width="430px" meta:resourcekey="txtVisIdentityNoResource1" 
                                            AutoPostBack="True" ontextchanged="txtVisIdentityNo_TextChanged"></asp:TextBox>
                                        
                                        <asp:CustomValidator id="cvVisIdentityNo" runat="server" ValidationGroup="vgSave" OnServerValidate="ID_ServerValidate"
                                            EnableClientScript="False" ControlToValidate="txtValid" 
                                            Text="&lt;img src='../Images/icon/Exclamation.gif' title='Identity No. is required' /&gt;">
                                        </asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <span class="requiredStar">*</span>
                                        <asp:Label ID="lblVisNameAr" runat="server" Text="Name (Ar) :" 
                                            meta:resourcekey="lblVisNameArResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign" colspan="3">
                                        <asp:TextBox ID="txtVisNameAr" runat="server" AutoCompleteType="Disabled" onkeypress="return ArabicOnly(event);"
                                            Width="430px" meta:resourcekey="txtVisNameArResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvVisNameAr" runat="server" 
                                            EnableClientScript="False" ControlToValidate="txtVisNameAr" ValidationGroup="vgSave"    
                                            
                                            Text="&lt;img src='../Images/icon/Exclamation.gif' title='Name (Ar) is required' /&gt;" 
                                            meta:resourcekey="rvVisNameArResource1"></asp:RequiredFieldValidator>
                                    </td>                                         
                                </tr> 
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblVisNameEn" runat="server" Text="Name (En) :" 
                                            meta:resourcekey="lblVisNameEnResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign" colspan="3">
                                        <asp:TextBox ID="txtVisNameEn" runat="server" AutoCompleteType="Disabled" onkeypress="return EnglishOnly(event);"
                                            Width="430px" meta:resourcekey="txtVisNameEnResource1"></asp:TextBox>
                                    </td>
                                </tr>                   
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblVisMobileNo" runat="server" Text="Mobile No. :" 
                                            meta:resourcekey="lblVisMobileNoResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign" colspan="3">
                                        <asp:TextBox ID="txtVisMobileNo" runat="server" AutoCompleteType="Disabled" onkeypress="return NumberOnly(event);"
                                            Width="430px" meta:resourcekey="txtVisMobileNoResource1" ></asp:TextBox>
                                    </td> 
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <span class="requiredStar">*</span>
                                        <asp:Label ID="lblDateStart" runat="server" Text="Start Date :" 
                                            meta:resourcekey="lblDateStartResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <uc:Calendar2 ID="calStartDate" runat="server" CalendarType="System" />
                                                </td>
                                                <td valign="middle">    
                                                    <asp:CustomValidator id="cvcalStartDate" runat="server"
                                                        Text="&lt;img src='../Images/icon/Exclamation.gif' title='' /&gt;"
                                                        ValidationGroup="vgSave" OnServerValidate="DateValidate_ServerValidate"
                                                        EnableClientScript="False" ControlToValidate="txtValid" 
                                                        meta:resourcekey="cvcalStartDateResource1"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="td1Allalign">
                                        <span class="requiredStar">*</span>
                                        <asp:Label ID="lblExpiryDate" runat="server" Text="End Date :" 
                                            meta:resourcekey="lblExpiryDateResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <uc:Calendar2 ID="calExpiryDate" runat="server" CalendarType="System"/>
                                                </td>
                                                <td valign="middle">    
                                                    <asp:CustomValidator id="cvcalExpiryDate" runat="server"
                                                        Text="&lt;img src='../Images/icon/Exclamation.gif' title='Expiry Date is required' /&gt;" 
                                                        ValidationGroup="vgSave" OnServerValidate="DateValidate_ServerValidate"
                                                        EnableClientScript="False" ControlToValidate="txtValid" 
                                                        meta:resourcekey="cvcalExpiryDateResource1"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1align" valign="top">
                                        <asp:Label ID="lblVisRegion" runat="server" Text="Regions :" 
                                            meta:resourcekey="lblVisRegionResource1"></asp:Label>
                                    </td>
                                    <td class="td2align" colspan="3" valign="top">
                                        <table>
                                            <tr>
                                                <td valign="top">
                                                    <asp:CheckBoxList ID="chkbRegion" runat="server" Width="430px" 
                                                        RepeatColumns="3" meta:resourcekey="chkbRegionResource1">
                                                        <asp:ListItem Text="Region 1" Value="Region1" 
                                                            meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                        <asp:ListItem Text="Region 2" Value="Region2" 
                                                            meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                        <asp:ListItem Text="Region 3" Value="Region3" 
                                                            meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                        <asp:ListItem Text="Region 4" Value="Region4" 
                                                            meta:resourcekey="ListItemResource4"></asp:ListItem>
                                                        <asp:ListItem Text="Region 5" Value="Region5" 
                                                            meta:resourcekey="ListItemResource5"></asp:ListItem>
                                                        <asp:ListItem Text="Region 6" Value="Region6" 
                                                            meta:resourcekey="ListItemResource6"></asp:ListItem>
                                                        <asp:ListItem Text="Region 7" Value="Region7" 
                                                            meta:resourcekey="ListItemResource7"></asp:ListItem>
                                                        <asp:ListItem Text="Region 8" Value="Region8" 
                                                            meta:resourcekey="ListItemResource8"></asp:ListItem>
                                                        <asp:ListItem Text="Region 9" Value="Region9" 
                                                            meta:resourcekey="ListItemResource9"></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                                <td valign="top">
                                                    <asp:CustomValidator id="cvRegion" runat="server"
                                                        Text="&lt;img src='../Images/icon/Exclamation.gif' title='You must choose at least one Region' /&gt;"
                                                        ValidationGroup="vgSave" OnServerValidate="Region_ServerValidate"
                                                        EnableClientScript="False" ControlToValidate="txtValid" 
                                                        meta:resourcekey="cvRegionResource1"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                        </table>   
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblDescription" runat="server" Text="Description :" 
                                            meta:resourcekey="lblDescriptionResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign" colspan="3">
                                        <asp:TextBox ID="txtDescription" runat="server" AutoCompleteType="Disabled" 
                                            Width="430px" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                                    </td>
                                </tr>  
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblTmpID" runat="server" Text="Template ID :" 
                                            meta:resourcekey="lblTmpIDResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign" colspan="3">
                                        <asp:DropDownList ID="ddlTmpID" runat="server" Width="435px" 
                                            meta:resourcekey="ddlTmpIDResource1"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rvTmpID" runat="server" 
                                            EnableClientScript="False" ControlToValidate="ddlTmpID" ValidationGroup="vgSave"
                                            
                                            Text="&lt;img src='../Images/icon/Exclamation.gif' title='Template ID is required' /&gt;" 
                                            meta:resourcekey="rvTmpIDResource1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtVisCardID" runat="server" AutoCompleteType="Disabled" 
                                            Enabled="False" Width="430px" Visible="False" 
                                            meta:resourcekey="txtVisCardIDResource1"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td  valign="top">
                            <table>
                                <tr>
                                    <td class="td2align">
                                        <uc3:ImageCtl ID="VisImage" runat="server" txtID="txtVisIdentityNo" Type="Visitors" IsEncryption="true" CaptureEnable="true" EmptyIDMsgEn="Please Enter Identity No." EmptyIDMsgAr="من فضلك أدخل رقم الهوية"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" class="borderButton">
                            &nbsp;&nbsp;
                            <asp:Button ID="btnSave" runat="server" CssClass="buttonBG" Text="Save" ValidationGroup="vgSave"
                                Width="100px" Enabled="False" onclick="btnSave_Click" 
                                meta:resourcekey="btnSaveResource1"/>
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Text="Cancel" 
                                OnClick="btnCancel_Click" Width="100px" Enabled="False" 
                                meta:resourcekey="btnCancelResource1"/>
                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" 
                                Width="10px" meta:resourcekey="txtValidResource1" ></asp:TextBox>
                            <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                                ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                EnableClientScript="False" ControlToValidate="txtValid" 
                                meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" colspan="5" class="td2align">
                            <asp:ValidationSummary ID="vsSave"   runat="server" CssClass="MsgValidation" 
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

