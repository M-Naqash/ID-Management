<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true"
    CodeFile="CardHistory.aspx.cs" Inherits="CardHistory" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="../Control/CardsSideMenu.ascx" TagName="CardsSideMenu" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc2:CardsSideMenu ID="CardsSideMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">


<style type="text/css">

.modalBackground
{
    background-color:Gray;
    filter:alpha(opacity=50);
    opacity:0.7;
}
.pnlBackGround
{
 position:fixed;
    top:10%;
    left:10px;
    width:300px;
    height:125px;
    text-align:center;
    background-color:White;
    border:solid 3px black;
}
</style>

    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnSearch">
            <div id="pageDiv" runat="server">
                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblID" runat="server" Text="Employee ID :" meta:resourcekey="lblIDResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtEmpID" runat="server" AutoCompleteType="Disabled" Width="168px"
                                meta:resourcekey="txtEmpIDResource1"></asp:TextBox>
                        </td>
                        <td class="td1Allalign">
                            <asp:Label ID="lblEmpCardType" runat="server" Text="Employee Type :" meta:resourcekey="lblEmpCardTypeResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:DropDownList ID="ddlEmpType" runat="server" Width="173px" meta:resourcekey="ddlEmpTypeResource1">
                                <asp:ListItem Text="-Select Employee Type-" Value="0" meta:resourcekey="ListItemSelectEmployeeResource1"></asp:ListItem>
                                <asp:ListItem Text="Aramco Employee" Value="Mng" meta:resourcekey="ListItemManagerResource1"></asp:ListItem>
                                <asp:ListItem Text="Third party" Value="Emp" meta:resourcekey="ListItemEmployeeResource1"></asp:ListItem>
                                <asp:ListItem Text="Contractor" Value="Con" meta:resourcekey="ListItemContractorResource1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblEmpName" runat="server" Text="Employee Name :" meta:resourcekey="lblEmpNameResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtEmpName" runat="server" AutoCompleteType="Disabled" Width="168px"
                                meta:resourcekey="txtEmpNameResource1"></asp:TextBox>
                        </td>
                        <td class="td1Allalign">
                            <asp:Label ID="lblCompany" runat="server" Text="Company :" meta:resourcekey="lblEmpTypeResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:DropDownList ID="ddlCompID" runat="server" Width="173px" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblEmpNationalID" runat="server" Text="National\Iqama ID :" meta:resourcekey="lblEmpNationalIDResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtEmpNationalID" runat="server" AutoCompleteType="Disabled" Width="168px"
                                MaxLength="10" meta:resourcekey="txtEmpNationalIDResource1"></asp:TextBox>
                        </td>
                        
                        <td class="td1Allalign">
                            <asp:Label ID="lblSection" runat="server" Text="Section :" meta:resourcekey="lblSectionResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:DropDownList ID="ddlSecID" runat="server" Width="173px" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblCardID" runat="server" Text="Card ID :" meta:resourcekey="lblCardIDResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtCardID" runat="server" AutoCompleteType="Disabled" Width="168px"
                                meta:resourcekey="txtCardIDResource1"></asp:TextBox>
                        </td>
                        <td class="td1Allalign">
                            <asp:Label ID="lblIssueID" runat="server" Text="Issue ID :" meta:resourcekey="lblIssueIDResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:DropDownList ID="ddlIssue" runat="server" Width="173px" meta:resourcekey="ddlIssueResource1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblCardStatus" runat="server" Text="Card Status :" meta:resourcekey="lblCardStatusResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:DropDownList ID="ddlCardstatus" runat="server" Width="173px" meta:resourcekey="ddlCardstatusResource1">
                                <asp:ListItem Text="-Select status-" Value="325" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                <asp:ListItem Text="Editable" Value="0" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                <asp:ListItem Text="InProcess" Value="1" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                <asp:ListItem Text="Canceled" Value="4" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                <asp:ListItem Text="Active" Value="2" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                <asp:ListItem Text="InActive" Value="3" meta:resourcekey="ListItemResource6"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="td1Allalign">
                            <asp:Label ID="lblApprovalStatus" runat="server" Text="Approval Status :" meta:resourcekey="lblApprovStatusResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:DropDownList ID="ddlApprovalStatus" runat="server" Width="173px" meta:resourcekey="ddlCardstatusResource1">
                                <asp:ListItem Text="-Select status-" Value="325" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                <asp:ListItem Text="Approved" Value="1" meta:resourcekey="ListItemResource8"></asp:ListItem>
                                <asp:ListItem Text="Reject" Value="-1" meta:resourcekey="ListItemResource9"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblCreatedBy" runat="server" Text="Created By :" meta:resourcekey="lblCreatedByResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:DropDownList ID="ddlCreatedBy" runat="server" Width="173px" meta:resourcekey="ddlCreatedByResource1">
                            </asp:DropDownList>
                        </td>
                        <td class="td1Allalign">
                            <asp:Label ID="lblPrintedBy" runat="server" Text="Printed By :" meta:resourcekey="lblPrintedByResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:DropDownList ID="ddlPrintedBy" runat="server" Width="173px" meta:resourcekey="ddlPrintedByResource1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4" class="borderButton">
                            <asp:Button ID="btnSearch" runat="server" CssClass="buttonBG" Text="Search" OnClick="btnSearch_Click"
                                meta:resourcekey="btnSearchResource1" Width="80px" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Text="Cancel" OnClick="btnCancel_Click"
                                meta:resourcekey="btnCancelResource1" Width="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Body_BG" valign="top" align="center" colspan="4">
                            <asp:Panel ID="pnlGrdTrans" runat="server" ScrollBars="Vertical" Width="980px" meta:resourcekey="pnlGrdTransResource1">
                                <asp:GridView ID="grdData" GridLines="None" Width="1300px"  runat="server" OnRowCreated="grdData_RowCreated"
                                    AutoGenerateColumns="False" AllowPaging="True" BorderStyle="Outset" 
                                    CellPadding="5" OnRowDataBound="grdData_RowDataBound" OnRowCommand="grdData_RowCommand"
                                    CellSpacing="5" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                    meta:resourcekey="grdCardprintResource1" EnableModelValidation="True" 
                                    >
                                    <Columns>
                                        <asp:BoundField DataField="CardID" HeaderText="ID" ReadOnly="True" SortExpression="CardID"
                                            meta:resourcekey="BoundFieldResource1">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpID" HeaderText="Employee ID" SortExpression="EmpID"
                                            meta:resourcekey="BoundFieldResource2">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpName" HeaderText="Name" SortExpression="EmpName" meta:resourcekey="BoundFieldResource3">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IssueName" HeaderText="Issue" SortExpression="IssueName"
                                            meta:resourcekey="BoundFieldResource4">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="StartDate" SortExpression="StartDate" meta:resourcekey="TemplateFieldResource1">
                                            <ItemTemplate>
                                                <%# DateFun.displayDateGrd(Eval("StartDate"), FormSession.DateType)%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ExpiryDate" SortExpression="ExpiryDate" meta:resourcekey="TemplateFieldResource2">
                                            <ItemTemplate>
                                                <%# DateFun.displayDateGrd(Eval("ExpiryDate"), FormSession.DateType)%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card Status" SortExpression="CardStatus" meta:resourcekey="TemplateFieldResource3">
                                            <ItemTemplate>
                                                <%# General.DisplayCardStatus(Eval("CardStatus"))%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="InActive Status" SortExpression="InActiveStatus" meta:resourcekey="TemplateFieldResource4">
                                            <ItemTemplate>
                                                <%# General.DisplayInActiveStatus(Eval("InActiveStatus"))%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="InActive Status" SortExpression="InActiveStatus" meta:resourcekey="TemplateFieldResource4">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnVRejectReason" CommandName="Reject" CommandArgument='<%# Eval("RejectReason") %>'  runat="server" Text='<%# General.DisplayInActiveStatus(Eval("InActiveStatus")) %>' ></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved Status" SortExpression="IsApproved" meta:resourcekey="TemplateFieldResource5">
                                            <ItemTemplate>
                                                <%# General.DisplayApprovedStatus(Eval("IsApproved"))%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Printed Status" SortExpression="isPrinted" meta:resourcekey="TemplateFieldResource6">
                                            <ItemTemplate>
                                                <%# General.DisplayPrintedStatus(Eval("isPrinted"))%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CreatedBy" HeaderText="<%$ Resources:Grid, grdCreatedBy %>"
                                            SortExpression="CreatedBy">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Grid, grdCreatedDate %>" SortExpression="CreatedDate">
                                            <ItemTemplate>
                                                <%# DateFun.displayDateGrd(Eval("CreatedDate"), FormSession.DateType)%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PrintedBy" HeaderText="<%$ Resources:Grid, grdPrintedBy %>"
                                            SortExpression="PrintedBy">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="<%$ Resources:Grid, grdPrintedDate %>" SortExpression="PrintedDate">
                                            <ItemTemplate>
                                                <%# DateFun.displayDateGrd(Eval("PrintedDate"), FormSession.DateType)%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By"
                                            SortExpression="ApprovedBy" meta:resourcekey="BoundFieldApprovedByResource">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RejectReason" HeaderText="Reject Reason"
                                            SortExpression="RejectReason" Visible="false" >
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="CalenderHeadBG" />
                                </asp:GridView>

                                <asp:Button ID="btnShow" runat="server" style="display:none" Text="Show Modal Popup" />

                                <cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="ModalPanel" TargetControlID="btnShow"
                                    OkControlID="OKButton" BackgroundCssClass="modalBackground">
                                </cc1:ModalPopupExtender>

                                <asp:Panel ID="ModalPanel" runat="server" Width="300px" CssClass="pnlBackGround">
                                    <asp:Label ID="lblRejectRes" runat="server" Text=""></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Button ID="OKButton" runat="server" Text="Close" />
                                </asp:Panel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
