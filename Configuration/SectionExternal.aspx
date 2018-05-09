<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="SectionExternal.aspx.cs" Inherits="Configuration_SectionExternal" %>

<%@ Register Src="../Control/ConfigurationSideMenu.ascx" TagName="ConfigSideMenu" TagPrefix="CSM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <CSM:ConfigSideMenu ID="SideMenu" runat="server" />
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
                                <asp:Label ID="lblPkID" runat="server" Text="Company ID :" 
                                    meta:resourcekey="lblPkIDResource1"></asp:Label>
                            </td>
                            <td class="td2Allalign">
                                <asp:DropDownList ID="ddlPkID" runat="server" Width="200px" AutoPostBack="True" 
                                    OnSelectedIndexChanged="ddlPkID_SelectedIndexChanged" 
                                    meta:resourcekey="ddlPkIDResource1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rvPkID" runat="server" ControlToValidate="ddlPkID"
                                    EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Company ID is required!' /&gt;"
                                    ValidationGroup="vgSave" Enabled="False" 
                                    meta:resourcekey="rvPkIDResource1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </div>
                    
                    <tr>
                        <td class="td1Allalign" style="width:50%">
                            <span class="requiredStar">*</span>
                            <asp:Label ID="lblNameAr" runat="server" Text="Section Name (Ar) :" 
                                meta:resourcekey="lblNameArResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtNameAr" runat="server" AutoCompleteType="Disabled" 
                                Width="195px" meta:resourcekey="txtNameArResource1"></asp:TextBox>
                            <asp:CustomValidator id="cvNameAr" runat="server" Text="&lt;img src='../Images/Icon/Exclamation.gif' title='!' /&gt;"
                                ValidationGroup="vgSave" OnServerValidate="Name_ServerValidate" 
                                EnableClientScript="False" ControlToValidate="txtValid" 
                                meta:resourcekey="cvNameArResource1"></asp:CustomValidator>
                        </td>
                    </tr>
                           
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblNameEn" runat="server" Text="Section Name (En) :" 
                                meta:resourcekey="lblNameEnResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtNameEn" runat="server" AutoCompleteType="Disabled" 
                                Width="195px" meta:resourcekey="txtNameEnResource1"></asp:TextBox>
                            <asp:CustomValidator id="cvNameEn" runat="server" Text="&lt;img src='../Images/Icon/Exclamation.gif' title='!' /&gt;"
                                ValidationGroup="vgSave" OnServerValidate="Name_ServerValidate" 
                                EnableClientScript="False" ControlToValidate="txtValid" Enabled="False" 
                                meta:resourcekey="cvNameEnResource1"></asp:CustomValidator>
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
                                meta:resourcekey="vsShowMsgResource1" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="PKID" BorderStyle="Outset" CellPadding="5" CellSpacing="5"
                                OnRowCreated="grdData_RowCreated" AllowPaging="True" 
                                OnPageIndexChanging="grdData_PageIndexChanging" Width="100%" 
                                meta:resourcekey="grdDataResource1">
                                <Columns>
                                    <asp:BoundField DataField="PKID" HeaderText="Section ID" ReadOnly="True" 
                                        SortExpression="PKID" meta:resourcekey="BoundFieldResource1">
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NameAr" HeaderText="Section Name (Ar)" 
                                        SortExpression="NameAr" meta:resourcekey="BoundFieldResource2">
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NameEn" HeaderText="Section Name (En)" 
                                        SortExpression="NameEn" meta:resourcekey="BoundFieldResource3">
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

