<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="UserSearch.aspx.cs" Inherits="UserSearch" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../Control/UsersSideMenu.ascx" TagName="UsersSideMenu" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:UsersSideMenu ID="UsersSideMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnSearch">
            <div id="pageDiv" runat="server">
                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblUsrLoginID" runat="server" Text="Login ID :" 
                                meta:resourcekey="lblUsrLoginIDResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtUsrLoginID" runat="server" Width="168px" 
                                meta:resourcekey="txtUsrLoginIDResource1"></asp:TextBox>
                        </td>
                        <td class="td1Allalign">
                            <asp:Label ID="lblUsrFullName" runat="server" Text="Full Name :" 
                                meta:resourcekey="lblUsrFullNameResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtUsrFullName" runat="server" Width="168px" 
                                meta:resourcekey="txtUsrFullNameResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1Allalign" height="20">
                            <asp:Label ID="lblUsrEmailID" runat="server" Text="Email ID :" 
                                meta:resourcekey="lblUsrEmailIDResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtUsrEmailID" runat="server" Width="168px" 
                                meta:resourcekey="txtUsrEmailIDResource1"></asp:TextBox>
                        </td>
                        <td class="td1Allalign">
                            <asp:Label ID="lblUsrStatus" runat="server" Text="Status :" 
                                meta:resourcekey="lblUsrStatusResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:DropDownList ID="ddlUsrStatus" runat="server" Width="173px" 
                                meta:resourcekey="ddlUsrStatusResource1"></asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="center" colspan="4" class="borderButton">
                            <asp:Button ID="btnSearch" runat="server" CssClass="buttonBG" Text="Search" Width="80px"
                                OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1"/>
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Text="Cancel" Width="80px"
                                OnClick="btnCancel_Click" meta:resourcekey="btnCancelResource1"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="Body_BG" valign="top" align="center" colspan="4">
                            <asp:GridView ID="grdData" GridLines="None" Width="100%" runat="server" OnRowCreated="grdData_RowCreated"
                               AutoGenerateColumns="False" AllowPaging="True"
                                BorderStyle="Outset" CellPadding="5" CellSpacing="5" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                EnableModelValidation="True" meta:resourcekey="grdDataResource1">
                                <Columns>
                                    <asp:BoundField DataField="UsrLoginID" HeaderText="Login ID" 
                                        SortExpression="UsrLoginID" meta:resourcekey="BoundFieldResource1">
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UsrFullName" HeaderText="Full Name" 
                                        SortExpression="UsrFullName" meta:resourcekey="BoundFieldResource2">
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="UsrStartDateType" SortExpression="UsrStartDateType" 
                                        Visible="False" meta:resourcekey="BoundFieldResource3"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Start Date" SortExpression="UsrStartDate" 
                                        meta:resourcekey="TemplateFieldResource1" >            
                                        <ItemTemplate>                                                                              
                                            <%# DateFun.GrdDisplayDate(Eval("UsrStartDate"),Eval("UsrStartDateType"))%>                                                                                                          
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />                        
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="UsrExpiryDateType" 
                                        SortExpression="UsrExpiryDateType" Visible="False" 
                                        meta:resourcekey="BoundFieldResource4"></asp:BoundField>
                                    <asp:TemplateField HeaderText="End Date" SortExpression="UsrExpiryDate" 
                                        meta:resourcekey="TemplateFieldResource2" >            
                                        <ItemTemplate>                                                                              
                                            <%# DateFun.GrdDisplayDate(Eval("UsrExpiryDate"),Eval("UsrExpiryDateType"))%>                                                                                                        
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />                        
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status" SortExpression="UsrStatus" 
                                        meta:resourcekey="TemplateFieldResource3" >            
                                        <ItemTemplate>                                                                              
                                            <%# FormCtrl.getText("Status",Eval("UsrStatus"),null)%>                                                                                                          
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />                        
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UsrEmailID" HeaderText="Email ID" 
                                        SortExpression="UsrEmailID" meta:resourcekey="BoundFieldResource5">
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="CalenderHeadBG" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

