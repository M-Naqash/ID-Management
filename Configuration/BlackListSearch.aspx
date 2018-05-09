<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="BlackListSearch.aspx.cs" Inherits="Configuration_BlackListSearch" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../Control/ConfigurationSideMenu.ascx" TagName="ConfigurationSideMenu" TagPrefix="CSM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <CSM:ConfigurationSideMenu ID="ConfigurationSideMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnSearch">
            <div id="pageDiv" runat="server">
                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblBlaIdentityNo" runat="server" Text="Identity No. :" 
                                meta:resourcekey="lblBlaIdentityNoResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtBlaIdentityNo" runat="server" AutoCompleteType="Disabled" 
                                Width="400px" meta:resourcekey="txtBlaIdentityNoResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblBlaNameAr" runat="server" Text="Name (Ar) :" 
                                meta:resourcekey="lblBlaNameArResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtBlaNameAr" runat="server" AutoCompleteType="Disabled" 
                                Width="400px" meta:resourcekey="txtBlaNameArResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblBlaNameEn" runat="server" Text="Name (En) :" 
                                meta:resourcekey="lblBlaNameEnResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtBlaNameEn" runat="server" AutoCompleteType="Disabled" 
                                Width="400px" meta:resourcekey="txtBlaNameEnResource1"></asp:TextBox>
                        </td>
                    </tr>      
                    <tr>
                        <td align="center" colspan="2" class="borderButton">
                            <asp:Button ID="btnSearch" runat="server" CssClass="buttonBG" Text="Search" 
                                OnClick="btnSearch_Click" ValidationGroup="vgSearch" Width="80px" 
                                meta:resourcekey="btnSearchResource1"/>
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Text="Cancel" 
                                OnClick="btnCancel_Click" Width="80px" 
                                meta:resourcekey="btnCancelResource1"/>
                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" 
                                Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="td2Allalign" width="100%">
                            <asp:ValidationSummary ID="vsSearch"    runat="server" CssClass="MsgValidation" 
                                EnableClientScript="False" ValidationGroup="vgSearch" 
                                meta:resourcekey="vsSearchResource1"/>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="Body_BG" valign="top" align="center" colspan="4">
                    
                           <%-- <asp:Panel ID="pnlGrd" runat="server" ScrollBars="Vertical"  Width="980px">--%>
                                <asp:GridView ID="grdData" GridLines="None" Width="100%" runat="server" OnRowCreated="grdData_RowCreated"
                                        AutoGenerateColumns="False" AllowPaging="True" BorderStyle="Outset" 
                                        CellPadding="5" CellSpacing="5" ShowFooter="True" 
                                    OnPageIndexChanging="grdData_PageIndexChanging" 
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
                                    <HeaderStyle CssClass="CalenderHeadBG" />
                                </asp:GridView>
                            <%--</asp:Panel>--%>
                        </td>
                    </tr>
                </table>
            </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

