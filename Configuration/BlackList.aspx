<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="BlackList.aspx.cs" Inherits="Configuration_BlackList" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../Control/ConfigurationSideMenu.ascx" TagName="ConfigurationSideMenu" TagPrefix="CSM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <CSM:ConfigurationSideMenu ID="ConfigurationSideMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnSave">
            <div id="pageDiv" runat="server">
                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                    
                    <div id="divUpdDel" runat="server" visible="False">
                        <tr>
                            <td class="td1Allalign">
                                <span class="requiredStar">*</span>
                                <asp:Label ID="lblBlaIdentityNo" runat="server" Text="Identity No. :" 
                                    meta:resourcekey="lblBlaIdentityNoResource1"></asp:Label>
                            </td>
                            <td class="td2Allalign">
                                <asp:DropDownList ID="ddlBlaIdentityNo" runat="server" Width="355px" AutoPostBack="True" 
                                    OnSelectedIndexChanged="ddlBlaIdentityNo_SelectedIndexChanged" 
                                    meta:resourcekey="ddlBlaIdentityNoResource1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rvBlaIdentityNo" runat="server" ControlToValidate="ddlBlaIdentityNo"
                                    EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Identity No is required' /&gt;"
                                    ValidationGroup="vgSave" Enabled="False" 
                                    meta:resourcekey="rvBlaIdentityNoResource1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </div>
                    
                    <div id="divAdd" runat="server" visible="true">
                        <tr>
                            <td class="td1Allalign" style="width:50%">
                                <span class="requiredStar">*</span>
                                <asp:Label ID="lblBlaIdentityNo2" runat="server" Text="Identity No. :" 
                                    meta:resourcekey="lblBlaIdentityNo2Resource1"></asp:Label>
                            </td>
                            <td class="td2Allalign">
                                <asp:TextBox ID="txtBlaIdentityNo" runat="server" AutoCompleteType="Disabled" 
                                    Width="350px" meta:resourcekey="txtBlaIdentityNoResource1"></asp:TextBox>
                                <asp:CustomValidator id="cvBlaIdentityNo" runat="server" Text="&lt;img src='../Images/Icon/Exclamation.gif' title='' /&gt;"
                                    ValidationGroup="vgSave" OnServerValidate="IdentityNo_ServerValidate" 
                                    EnableClientScript="False" ControlToValidate="txtValid" 
                                    meta:resourcekey="cvBlaIdentityNoResource1"></asp:CustomValidator>
                            </td>
                        </tr>
                    </div>
                    
                    <tr>
                        <td class="td1Allalign" style="width:50%">
                            <span class="requiredStar">*</span>
                            <asp:Label ID="lblBlaNameAr" runat="server" Text="Name (Ar) :" 
                                meta:resourcekey="lblBlaNameArResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtBlaNameAr" runat="server" AutoCompleteType="Disabled" 
                                Width="350px" meta:resourcekey="txtBlaNameArResource1"></asp:TextBox>
                            <asp:CustomValidator id="cvBlaNameAr" runat="server" Text="&lt;img src='../Images/Icon/Exclamation.gif' title='' /&gt;"
                                ValidationGroup="vgSave" OnServerValidate="Name_ServerValidate" 
                                EnableClientScript="False" ControlToValidate="txtValid" 
                                meta:resourcekey="cvBlaNameArResource1"></asp:CustomValidator>
                        </td>
                    </tr>
                           
                    <tr>
                        <td class="td1Allalign">
                            <span class="requiredStar">*</span>
                            <asp:Label ID="lblBlaNameEn" runat="server" Text="Name (En) :" 
                                meta:resourcekey="lblBlaNameEnResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtBlaNameEn" runat="server" AutoCompleteType="Disabled" 
                                Width="350px" meta:resourcekey="txtBlaNameEnResource1"></asp:TextBox>
                            <asp:CustomValidator id="cvBlaNameEn" runat="server" Text="&lt;img src='../Images/Icon/Exclamation.gif' title='' /&gt;"
                                ValidationGroup="vgSave" OnServerValidate="Name_ServerValidate" 
                                EnableClientScript="False" ControlToValidate="txtValid" 
                                meta:resourcekey="cvBlaNameEnResource1"></asp:CustomValidator>
                        </td>
                    </tr>

                    <tr>
                        <td class="td1Allalign">
                            <span class="requiredStar">*</span>
                            <asp:Label ID="lblNatID" runat="server" Text="Nationality :" 
                                meta:resourcekey="lblNatIDResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:DropDownList ID="ddlNatID" runat="server" Width="355px" 
                                meta:resourcekey="ddlNatIDResource1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rvNatID" runat="server" ControlToValidate="ddlNatID"
                                EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Nationality is required' /&gt;"
                                ValidationGroup="vgSave" meta:resourcekey="rvNatIDResource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblBlaReason" runat="server" Text="Reason :" meta:resourcekey="lblReasonResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtBlaReason" runat="server" AutoCompleteType="Disabled" Width="350px" Height="99px" TextMode="MultiLine"></asp:TextBox>
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

                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="BlaIdentityNo" BorderStyle="Outset" CellPadding="5" CellSpacing="5"
                                OnRowCreated="grdData_RowCreated" AllowPaging="True" 
                                OnPageIndexChanging="grdData_PageIndexChanging" Width="100%" 
                                meta:resourcekey="grdDataResource1">
                                <Columns>
                                    <asp:BoundField DataField="BlaIdentityNo" HeaderText="Identity No." 
                                        ReadOnly="True" SortExpression="BlaIdentityNo" 
                                        meta:resourcekey="BoundFieldResource1">
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BlaNameAr" HeaderText="Name (Ar)" 
                                        SortExpression="BlaNameAr" meta:resourcekey="BoundFieldResource2">
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BlaNameEn" HeaderText="Name (En)" 
                                        SortExpression="BlaNameEn" meta:resourcekey="BoundFieldResource3" >
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NatName" HeaderText="Nationality" 
                                        SortExpression="NatName" meta:resourcekey="BoundFieldResource4" >
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BlaReason" HeaderText="Reason" 
                                        SortExpression="NatName" meta:resourcekey="BoundFieldReasonResource" >
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                </Columns>
                                <PagerStyle BorderStyle="Outset" />
                                <HeaderStyle CssClass="CalenderHeadBG" />
                                <FooterStyle CssClass="CalenderHeadBG" />
                            </asp:GridView>
                        </td>
                    </tr>
                
                </table>
            </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


