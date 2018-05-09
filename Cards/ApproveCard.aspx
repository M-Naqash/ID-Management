<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true"
    CodeFile="ApproveCard.aspx.cs" Inherits="ApproveCard" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="../Control/CardsSideMenu.ascx" TagName="CardsSideMenu" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc2:CardsSideMenu ID="CardsSideMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript">
        function SelectAll(id) {
            var grid = document.getElementById("grdIssueCondition.ClientID");
            var cell;
            if (grid.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid.rows.length; i++) {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];

                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++) {
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type == "checkbox") {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }
    </script>
    <script type = "text/javascript">
        function Confirm() {
            var doc = document.forms[0];
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (document.getElementById('<%=hfdLang.ClientID %>').value == "En") {
                if (confirm("Are you sure you want to reject the card?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
            }
            else {
                if (confirm("هل انت متأكد من رفض البطاقة ؟")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type ="text/javascript" language = "javascript">
        function EmpView(empID) {
            window.open('../Employee/EmployeeView.aspx?ac=View&EmpID=' + empID, 'null', 'fullscreen=no,titlebar=no,toolbar=no,statusbar=no,scrollbars=yes,height=400px,width=860px,top=0px,left=150px', 'movable=no')
        }
    </script>
    <script language="javascript" type="text/javascript">
        function showWait() {
            //if ($get('fudReqFile').value.length > 0) {
            $get('upWaiting').style.display = 'block';
            //}
        }
    </script>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div id="pageDiv" runat="server">
                <%--<asp:ScriptManager ID="Scriptmanager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

                <asp:UpdateProgress ID="upWaiting" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <center>
                                <iframe id="ifrmProgress" runat="server" src="Progress.aspx" scrolling="no" frameborder="0" height="400px" width="450px" ></iframe> 
                            </center>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                    <tr>
                        <td align="<%= General.getAlign(1) %>" valign="middle">
                            <asp:Label ID="lblEmployeeID" runat="server" Text="Employee ID :" meta:resourcekey="lblEmployeeIDResource1"></asp:Label>
                        </td>
                        <td align="<%= General.getAlign(2) %>" valign="middle">
                            <asp:DropDownList ID="ddlEmployee" runat="server" Width="173px" meta:resourcekey="ddlEmployeeResource1">
                            </asp:DropDownList>
                        </td>
                        <td align="<%= General.getAlign(1) %>" valign="middle">
                            <asp:Label ID="lblEmpCardType" runat="server" Text="Employee Type :" meta:resourcekey="lblEmpCardTypeResource1"></asp:Label>
                        </td>
                        <td align="<%= General.getAlign(2) %>" valign="middle">
                            <asp:DropDownList ID="ddlEmpType" runat="server" Width="173px" meta:resourcekey="ddlEmpTypeResource1">
                                <asp:ListItem Value="0" Text="-Select Employee Type-"  meta:resourcekey="ListItemSelectEmployeeResource1"></asp:ListItem>
                                <asp:ListItem Text="Aramco Employee" Value="Mng" meta:resourcekey="ListItemManagerResource1"></asp:ListItem>
                                <asp:ListItem Text="Third party" Value="Emp" meta:resourcekey="ListItemEmployeeResource1"></asp:ListItem>
                                <asp:ListItem Text="Contractor" Value="Con" meta:resourcekey="ListItemContractorResource1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="<%= General.getAlign(1) %>" valign="middle">
                            <asp:Label ID="lblIssueID" runat="server" Text="Issue ID :" meta:resourcekey="lblIssueIDResource1"></asp:Label>
                        </td>
                        <td align="<%= General.getAlign(2) %>" valign="middle">
                            <asp:DropDownList ID="ddlIssue" runat="server" Width="173px" meta:resourcekey="ddlIssueResource1">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4" class="borderButton">
                            <asp:Button ID="btnSearch" runat="server" CssClass="buttonBG" Text="Search" OnClick="btnSearch_Click"
                                meta:resourcekey="btnSearchResource1" Width="80px" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Text="Cancel" OnClick="btnCancel_Click"
                                meta:resourcekey="btnCancelResource1" Width="80px" />
                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" 
                                Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>
                            <asp:HiddenField ID="hfdLang" runat="server" />
                            <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                                ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                EnableClientScript="False" ControlToValidate="txtValid" 
                                meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="td2Allalign" width="100%">
                            <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess" EnableClientScript="False"
                                ValidationGroup="vgShowMsg" meta:resourcekey="vsShowMsgResource1" />
                        </td>
                    </tr>
                    <tr>
                        <div id="divbtn" runat="server" visible="False">
                            <td colspan="4" valign="top" align="<%= General.getAlign(2) %>">
                                <asp:Button ID="btnApproveSelectCards" runat="server" Text="Approve selected cards"
                                    CssClass="buttonBG" OnClick="btnApproveSelectCards_Click" meta:resourcekey="btnApproveSelectCardsResource1" />
                                <asp:Button ID="btnCancelSelectCards" runat="server" Text="Cancel selected cards" OnClientClick="Confirm()"
                                    CssClass="buttonBG" OnClick="btnCancelSelectCards_Click" meta:resourcekey="btnCancelSelectCardsResource1" />
                            </td>
                        </div>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:GridView ID="grdData" GridLines="None" Width="950px" runat="server" OnRowCreated="grdData_RowCreated"
                                CellPadding="5" CellSpacing="5" OnRowCommand="grdData_RowCommand" AutoGenerateColumns="False" OnRowDataBound="grdData_RowDataBound"
                                AllowPaging="True" BorderStyle="Outset" ShowFooter="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                EnableModelValidation="True" meta:resourcekey="grdCardprintResource1" >
                                <Columns>
                                    <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource1">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" meta:resourcekey="chkSelectResource1" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource2">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnApprove" CommandName="Approve" runat="server" CommandArgument='<%# Eval("CardID") %>'
                                                Text="Approve" meta:resourcekey="btnApproveResource1"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:TemplateField>
                                    <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSeparator" runat="server" Text="/" meta:resourcekey="lblSeparatorResource1"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="           " meta:resourcekey="TemplateFieldResource4">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnCancel" CommandName="Reject" runat="server" CommandArgument='<%# Eval("CardID") %>'
                                                 Text="Reject" OnClientClick="Confirm()" meta:resourcekey="btnCancelResource2"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reject Reason" meta:resourcekey="TemplateFieldRejectReasonResource">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgRejectReason" runat="server" Width="200px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CardID" HeaderText="Card ID" ReadOnly="True" SortExpression="CardID"
                                        meta:resourcekey="BoundFieldResource1">
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Employee ID" SortExpression="EmpID" meta:resourcekey="BoundFieldResource2">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnViewEmp" CommandArgument='<%# Eval("EmpID") %>'  runat="server" Text='<%# Eval("EmpID") %>' ></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="EmpID" HeaderText="Employee ID" SortExpression="EmpID"
                                        meta:resourcekey="BoundFieldResource2">
                                        <HeaderStyle CssClass="GridColumn" />  CommandName="ViewEmpInfo"
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField DataField="EmpName" HeaderText="Employee Name" SortExpression="EmpName"
                                        meta:resourcekey="BoundFieldResource3">
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Employee Type" SortExpression="EmpType" meta:resourcekey="BoundFieldResource6">
                                        <ItemTemplate>
                                            <%# General.DisplayEmpType(Eval("EmpType"))%>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IssueName" HeaderText="Issue Name" SortExpression="IssueName"
                                        meta:resourcekey="BoundFieldResource4">
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Start Date" SortExpression="StartDate" meta:resourcekey="TemplateFieldResource5">
                                        <ItemTemplate>
                                            <%# DateFun.GrdDisplayDate(Eval("StartDate"))%>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expiry Date" SortExpression="ExpiryDate" meta:resourcekey="TemplateFieldResource6">
                                        <ItemTemplate>
                                            <%# DateFun.GrdDisplayDate(Eval("ExpiryDate"))%>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BorderStyle="Outset" />
                                <HeaderStyle CssClass="CalenderHeadBG" />
                                <FooterStyle CssClass="CalenderHeadBG" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
