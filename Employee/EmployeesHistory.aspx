<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeesHistory.aspx.cs" Inherits="Employee_EmployeesHistory" Culture="auto"
    UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Src="../Control/EmployeeSideMenu.ascx" TagName="EmployeeSideMenu" TagPrefix="CSM" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagPrefix="uc" TagName="Calendar2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <CSM:EmployeeSideMenu ID="EmployeeSideMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnSearch">
            <div id="pageDiv" runat="server">
                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblEmpID" runat="server" Text="Employee ID :" 
                                meta:resourcekey="lblEmpIDResource1" ></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtEmpID" runat="server" AutoCompleteType="Disabled" 
                                Width="180px" meta:resourcekey="txtEmpIDResource1"
                                ></asp:TextBox>
                        </td>
                        <td class="td1Allalign">
                            <asp:Label ID="lblEmpType" runat="server" Text="Employee Type :" 
                                meta:resourcekey="lblEmpTypeResource1" ></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:DropDownList ID="ddlEmpType" runat="server" Width="185px" 
                                meta:resourcekey="ddlEmpTypeResource1" >
                                <asp:ListItem Text="-Select Employee Type-" Value="0" 
                                    meta:resourcekey="ListItemResource1"></asp:ListItem>
                                <asp:ListItem Text="Aramco Employee" Value="Mng" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                <asp:ListItem Text="Third party" Value="Emp" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                <asp:ListItem Text="Contractor" Value="Con" 
                                    meta:resourcekey="ListItemResource4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblEmpName" runat="server" Text="Name :" 
                                meta:resourcekey="lblEmpNameResource1" ></asp:Label>
                        </td>
                        <td class="td2Allalign" >
                            <asp:TextBox ID="txtEmpName" runat="server" AutoCompleteType="Disabled" 
                                Width="180px" meta:resourcekey="txtEmpNameResource1"
                                ></asp:TextBox>
                        </td>
                        <td class="td1Allalign">
                            <asp:Label ID="lblNationalID" runat="server" Text="National\Iqama ID :" 
                                meta:resourcekey="lblNationalIDResource1" ></asp:Label>
                        </td>
                        <td class="td2Allalign" >
                            <asp:TextBox ID="txtNationalID" runat="server" AutoCompleteType="Disabled" 
                                Width="180px" meta:resourcekey="txtNationalIDResource1"
                                ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblCompany" runat="server" Text="Company :" 
                                meta:resourcekey="lblCompanyResource1" ></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:DropDownList ID="ddlCompID" runat="server" Width="185px"  
                                meta:resourcekey="ddlCompIDResource1">
                            </asp:DropDownList>
                        </td>

                        <td class="td1Allalign">
                            <asp:Label ID="lblSection" runat="server" Text="Section External :" 
                                meta:resourcekey="lblSectionResource1" ></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:DropDownList ID="ddlSecID" runat="server" Width="185px"  
                                meta:resourcekey="ddlSecIDResource1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblHaveCard" runat="server" Text="Have Card :" 
                                meta:resourcekey="lblHaveCardResource1" ></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:DropDownList ID="ddlHaveCard" runat="server" Width="185px" meta:resourcekey="ddlHaveCardResource1" >
                                <asp:ListItem Text="All" Value="0" meta:resourcekey="AllListItemResource1"></asp:ListItem>
                                <asp:ListItem Text="Yse" Value="1" meta:resourcekey="YseListItemResource2"></asp:ListItem>
                                <asp:ListItem Text="No"  Value="2" meta:resourcekey="NoListItemResource3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="td2Allalign">
                        </td>
                        <td class="td1Allalign">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4" class="borderButton">
                            <asp:Button ID="btnSearch" runat="server" CssClass="buttonBG" Text="Search" OnClick="btnSearch_Click"
                                ValidationGroup="vgSearch" Width="80px" 
                                meta:resourcekey="btnSearchResource1"  />
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Text="Cancel" OnClick="btnCancel_Click"
                                Width="80px" meta:resourcekey="btnCancelResource1" />
                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" 
                                Width="10px" meta:resourcekey="txtValidResource1"
                                ></asp:TextBox>
                            <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" ValidationGroup="ShowMsg"
                                OnServerValidate="ShowMsg_ServerValidate" EnableClientScript="False" 
                                ControlToValidate="txtValid" meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="4" class="td2Allalign" width="100%">
                            <asp:ValidationSummary ID="vsSearch" runat="server" CssClass="MsgValidation" EnableClientScript="False"
                                ValidationGroup="vgSearch" meta:resourcekey="vsSearchResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Body_BG" valign="top" align="center" colspan="4">
                            <asp:Panel ID="pnlGrdTrans" runat="server" ScrollBars="Vertical" Width="980px" 
                                meta:resourcekey="pnlGrdTransResource1">
                                <asp:GridView ID="grdData" GridLines="None" Width="1300px" runat="server" OnRowCreated="grdData_RowCreated"
                                    AutoGenerateColumns="False" AllowPaging="True" BorderStyle="Outset" CellPadding="5"
                                    CellSpacing="5" ShowFooter="True" OnRowCommand="grdData_RowCommand"
                                    OnPageIndexChanging="grdData_PageIndexChanging" 
                                    meta:resourcekey="grdDataResource1" onrowdatabound="grdData_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="EmpID" HeaderText="Employee ID" 
                                            SortExpression="EmpID" meta:resourcekey="BoundFieldResource1">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Employee Type" SortExpression="EmpType" 
                                            meta:resourcekey="TemplateFieldResource1" >
                                            <ItemTemplate>
                                                <%# General.DisplayEmpType(Eval("EmpType"))%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmpNameEn" HeaderText="Employee Name (En)" 
                                            SortExpression="EmpNameEn" meta:resourcekey="BoundFieldResource2">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpNameAr" HeaderText="Employee Name (Ar)" 
                                            SortExpression="EmpNameAr" meta:resourcekey="BoundFieldResource3">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NatNameEn" HeaderText="Nationlity Name (En)" 
                                            SortExpression="NatNameEn" meta:resourcekey="BoundFieldResource4">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NatNameAr" HeaderText="Nationlity Name (Ar)" 
                                            SortExpression="NatNameAr" meta:resourcekey="BoundFieldResource5"
                                            >
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpJobTitleEn" HeaderText="Job Titel (En)" 
                                            SortExpression="EmpJobTitleEn" meta:resourcekey="BoundFieldResource6">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpJobTitleAr" HeaderText="Job Titel (Ar)" 
                                            SortExpression="EmpJobTitleAr" meta:resourcekey="BoundFieldResource7">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpNationalID" HeaderText="National ID" 
                                            SortExpression="EmpNationalID" meta:resourcekey="BoundFieldResource8">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Birth Date" SortExpression="EmpBirthDate" 
                                            meta:resourcekey="TemplateFieldResource2" >
                                            <ItemTemplate>
                                                <%# DateFun.displayDateGrd(Eval("EmpBirthDate"), FormSession.DateType)%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CompNameEn" HeaderText="Company Name (En)" 
                                            SortExpression="CompNameEn" meta:resourcekey="BoundFieldResource9">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompNameAr" HeaderText="Company Name (Ar)" 
                                            SortExpression="CompNameAr" meta:resourcekey="BoundFieldResource10">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SecNameEn" HeaderText="Section Name (En)" 
                                            SortExpression="SecNameEn" meta:resourcekey="BoundFieldResource11">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SecNameAr" HeaderText="Section Name (Ar)" 
                                            SortExpression="SecNameAr" meta:resourcekey="BoundFieldResource12">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Have Fingerprint" SortExpression="HaveFingerPrint" 
                                            meta:resourcekey="TemplateFieldResource55" >
                                            <ItemTemplate>
                                                <%# HaveFingerPrintGrd(Eval("HaveFingerPrint"))%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Have Card" SortExpression="HaveCard2" 
                                            meta:resourcekey="TemplateFieldResource66" >
                                            <ItemTemplate>
                                                <%# HaveFingerPrintGrd(Eval("HaveCard"))%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" meta:resourcekey="TemplateFieldResource77">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnNewCard" CommandName="NewCard" CommandArgument='<%# Eval("EmpID") %>'
                                                runat="server" ImageUrl="~/Images/icon/CardAdd.png" meta:resourcekey="imgbtnNewCardResource1" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" /> <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                        <asp:BoundField DataField="HaveCard" HeaderText="HaveCard" SortExpression="HaveCard">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="CalenderHeadBG" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess" EnableClientScript="False"
                                ValidationGroup="vgShowMsg" meta:resourcekey="vsShowMsgResource1"  />    
                        </td>
                    </tr>
                </table>
            </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
