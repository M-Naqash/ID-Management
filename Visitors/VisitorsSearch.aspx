<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="VisitorsSearch.aspx.cs" Inherits="Visitors_VisitorsSearch" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../Control/VisitorsSideMenu.ascx" TagName="VisitorsSideMenu" TagPrefix="CSM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <CSM:VisitorsSideMenu ID="SideMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnSearch">
    <div id="pageDiv" runat="server">
        <table border="0" cellpadding="0" cellspacing="4" width="100%">
            <tr>
                <td class="td1Allalign">
                    <asp:Label ID="lblVisCardID" runat="server" Text="Card ID :" 
                        meta:resourcekey="lblVisCardIDResource1"></asp:Label>
                </td>
                <td class="td2Allalign">
                    <asp:TextBox ID="txtVisCardID" runat="server" AutoCompleteType="Disabled" 
                        Width="168px" meta:resourcekey="txtVisCardIDResource1"></asp:TextBox>
                </td>
                <td class="td1Allalign">
                    <asp:Label ID="lblVisIdentityNo" runat="server" Text="Identity No. :" 
                        meta:resourcekey="lblVisIdentityNoResource1"></asp:Label>
                </td>
                <td class="td2Allalign">
                    <asp:TextBox ID="txtVisIdentityNo" runat="server" AutoCompleteType="Disabled" 
                        Width="168px" meta:resourcekey="txtVisIdentityNoResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td1Allalign">
                    <asp:Label ID="lblVisNameAr" runat="server" Text="Name (Ar) :" 
                        meta:resourcekey="lblVisNameArResource1"></asp:Label>
                </td>
                <td class="td2Allalign">
                    <asp:TextBox ID="txtVisNameAr" runat="server" AutoCompleteType="Disabled" 
                        Width="168px" meta:resourcekey="txtVisNameArResource1"></asp:TextBox>
                </td>
                <td class="td1Allalign">
                    <asp:Label ID="lblVisNameEn" runat="server" Text="Name (En) :" 
                        meta:resourcekey="lblVisNameEnResource1"></asp:Label>
                </td>
                <td class="td2Allalign">
                    <asp:TextBox ID="txtVisNameEn" runat="server" AutoCompleteType="Disabled" 
                        Width="168px" meta:resourcekey="txtVisNameEnResource1"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td class="td1Allalign">
                    <asp:Label ID="lblVisMobileNo" runat="server" Text="Mobile No :" 
                        meta:resourcekey="lblVisMobileNoResource1"></asp:Label>
                </td>
                <td class="td2Allalign">
                    <asp:TextBox ID="txtVisMobileNo" runat="server" AutoCompleteType="Disabled" 
                        Width="168px" meta:resourcekey="txtVisMobileNoResource1"></asp:TextBox>
                </td>
                <td class="td1Allalign">
                    <asp:Label ID="lblCardStatus" runat="server" Text="Card Status :" 
                        meta:resourcekey="lblCardStatusResource1"></asp:Label>
                </td>
                <td class="td2Allalign"> 
                    <asp:DropDownList ID="ddlCardstatus" runat="server" Width="173px" 
                        meta:resourcekey="ddlCardstatusResource1">
                        <asp:ListItem Text="-Select status-" Value="325" 
                            meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="Editable"  Value="0" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Active"    Value="2" meta:resourcekey="ListItemResource3"></asp:ListItem>
                        <asp:ListItem Text="InActive"  Value="3" meta:resourcekey="ListItemResource4"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td1Allalign">
                    <asp:Label ID="lblCreatedBy" runat="server" Text="Created By :" 
                        meta:resourcekey="lblCreatedByResource1"></asp:Label>
                </td>
                <td class="td2Allalign">
                    <asp:DropDownList ID="ddlCreatedBy" runat="server" Width="173px" 
                        meta:resourcekey="ddlCreatedByResource1"></asp:DropDownList>
                </td>
                <td class="td1Allalign">
                    <asp:Label ID="lblPrintedBy" runat="server" Text="Printed By :" 
                        meta:resourcekey="lblPrintedByResource1"></asp:Label>
                </td>
                <td class="td2Allalign">
                    <asp:DropDownList ID="ddlPrintedBy" runat="server" Width="173px" 
                        meta:resourcekey="ddlPrintedByResource1"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4" class="borderButton">
                    <asp:Button ID="btnSearch" runat="server" CssClass="buttonBG" Text="Search" 
                        OnClick="btnSearch_Click" Width="80px" meta:resourcekey="btnSearchResource1"/>
                    <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Text="Cancel" 
                        OnClick="btnCancel_Click" Width="80px" meta:resourcekey="btnCancelResource1"/>
                </td>
            </tr>
            <tr>
                <td class="Body_BG" valign="top" align="center" colspan="4">
                    
                <asp:Panel ID="pnlGrd" runat="server" ScrollBars="Vertical"  Width="980px" 
                        meta:resourcekey="pnlGrdResource1">
                    <asp:GridView ID="grdData" GridLines="None" Width="1350px" runat="server" OnRowCreated="grdData_RowCreated"
                       AutoGenerateColumns="False" AllowPaging="True"
                        BorderStyle="Outset" CellPadding="5" CellSpacing="5" ShowFooter="True" 
                        OnPageIndexChanging="grdData_PageIndexChanging" 
                        meta:resourcekey="grdDataResource1">
                        <Columns>
                            <asp:BoundField DataField="VisCardID" HeaderText="Card ID" ReadOnly="True" 
                                SortExpression="VisCardID" meta:resourcekey="BoundFieldResource1">
                                <HeaderStyle CssClass="GridColumn" />
                                <ItemStyle CssClass="GridColumn" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VisIdentityNo" HeaderText="Identity No." 
                                SortExpression="VisIdentityNo" meta:resourcekey="BoundFieldResource2">
                                <HeaderStyle CssClass="GridColumn" />
                                <ItemStyle CssClass="GridColumn" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VisNameAr" HeaderText="Name (Ar)" 
                                SortExpression="VisNameAr" meta:resourcekey="BoundFieldResource3">
                                <HeaderStyle CssClass="GridColumn" />
                                <ItemStyle CssClass="GridColumn" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VisNameEn" HeaderText="Name (En)" 
                                SortExpression="VisNameEn" meta:resourcekey="BoundFieldResource4">
                                <HeaderStyle CssClass="GridColumn" />
                                <ItemStyle CssClass="GridColumn" />
                            </asp:BoundField>
                             <asp:BoundField DataField="VisMobileNo" HeaderText="VisMobile No." 
                                SortExpression="VisMobileNo" meta:resourcekey="BoundFieldResource5">
                                <HeaderStyle CssClass="GridColumn" />
                                <ItemStyle CssClass="GridColumn" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="StartDate" SortExpression="StartDate" 
                                meta:resourcekey="TemplateFieldResource1">            
                                <ItemTemplate>                                                                              
                                    <%# DateFun.GrdDisplayDate(Eval("StartDate"), "S")%>                                                                                                          
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridColumn" />
                                <ItemStyle CssClass="GridColumn" />                        
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="ExpiryDate" SortExpression="ExpiryDate" 
                                meta:resourcekey="TemplateFieldResource2">            
                            <ItemTemplate>                                                         
                                <%# DateFun.GrdDisplayDate(Eval("ExpiryDate"), "S")%>                                                                                                               
                            </ItemTemplate>
                            <HeaderStyle CssClass="GridColumn" />
                            <ItemStyle CssClass="GridColumn" />       
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Card Status" SortExpression="CardStatus" 
                                meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>                                                         
                                    <%# General.DisplayCardStatus(Eval("CardStatus"))%>                                                                                                               
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridColumn" />
                                <ItemStyle CssClass="GridColumn" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Printed Status" SortExpression="isPrinted" 
                                meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>                                                         
                                    <%# General.DisplayPrintedStatus(Eval("isPrinted"))%>                                                                                                               
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridColumn" />
                                <ItemStyle CssClass="GridColumn" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="CreatedBy" HeaderText="<%$ Resources:Grid, grdCreatedBy %>"
                                SortExpression="CreatedBy" meta:resourcekey="BoundFieldResource6">
                                <HeaderStyle CssClass="GridColumn" />
                                <ItemStyle CssClass="GridColumn" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PrintedBy" HeaderText="<%$ Resources:Grid, grdPrintedBy %>"
                                SortExpression="PrintedBy" meta:resourcekey="BoundFieldResource7">
                                <HeaderStyle CssClass="GridColumn" />
                                <ItemStyle CssClass="GridColumn" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="CalenderHeadBG" />
                    </asp:GridView>
                </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    </asp:Panel>

     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

