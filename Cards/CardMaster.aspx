<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true"
    CodeFile="CardMaster.aspx.cs" Inherits="CardMaster" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="../Control/CardsSideMenu.ascx" TagName="CardsSideMenu" TagPrefix="uc2" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagPrefix="uc" TagName="Calendar2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc2:CardsSideMenu ID="CardsSideMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <%--script--%>
    <script type="text/javascript" src="../JScript/AutoComplete.js"></script>
    <%--script--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnFetch">
                <div id="pageDiv" runat="server">
                    <table border="0" cellpadding="0" cellspacing="4" width="100%">
                        <tr>
                            <td width="100%">
                                <asp:ValidationSummary ID="vsFetch" runat="server" CssClass="errorValidation" EnableClientScript="False"
                                    ValidationGroup="Fetch" meta:resourcekey="vsFetchResource1" />
                            </td>
                        </tr>
                        <tr id="Tr2" runat="server">
                            <td id="Td2" class="borderButton" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="td1Allalign">
                                            <span class="requiredStar">*</span>
                                            <asp:Label ID="lblSearchBy" runat="server" Text="Search by:" meta:resourcekey="lblSearchByResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">&nbsp;
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" Width="200px"
                                            meta:resourcekey="ddlSearchByResource1" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Text="Employee ID" Value="EmpID" Selected="True" meta:resourcekey="SearchListItemResource1"></asp:ListItem>
                                            <asp:ListItem Text="Employee Name (Ar)" Value="EmpNameAr" meta:resourcekey="SearchListItemResource2"></asp:ListItem>
                                            <asp:ListItem Text="Employee Name (En)" Value="EmpNameEn" meta:resourcekey="SearchListItemResource3"></asp:ListItem>
                                            <asp:ListItem Text="National\Iqama ID" Value="EmpNationalID" meta:resourcekey="SearchListItemResource4"></asp:ListItem>
                                            <asp:ListItem Text="Mobile No." Value="EmpMobileNo" meta:resourcekey="SearchListItemResource5"></asp:ListItem>
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
                                                ValidationGroup="Fetch" OnServerValidate="SearchBy_ServerValidate" EnableClientScript="False"
                                                ControlToValidate="txtValid" meta:resourcekey="cvSearchByResource1"></asp:CustomValidator>
                                            &nbsp;
                                        <asp:Button ID="btnFetch" runat="server" OnClick="btnFetch_Click" ValidationGroup="Fetch"
                                            CssClass="buttonBG" Text="Fetch Card Data" meta:resourcekey="btnFetchCardHistoryResource1" Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td id="Td3" runat="server">
                                <asp:ValidationSummary ID="vsSearch" runat="server" CssClass="MsgValidation" EnableClientScript="False"
                                    ValidationGroup="vgSearch" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:GridView ID="grdData" GridLines="None" Width="950px" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="True" BorderStyle="Outset" CellPadding="5" CellSpacing="5" OnRowCreated="grdData_RowCreated"
                                    OnPageIndexChanging="grdData_PageIndexChanging" PageSize="5" EnableModelValidation="True"
                                    meta:resourcekey="grdCardprintResource1">
                                    <Columns>
                                        <asp:BoundField DataField="CardID" HeaderText="Card ID" SortExpression="CardID" meta:resourcekey="BoundFieldResource1">
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IssueName" HeaderText="Issue" SortExpression="IssueName"
                                            meta:resourcekey="BoundFieldResource3"></asp:BoundField>
                                        <asp:TemplateField HeaderText="StartDate" SortExpression="StartDate" meta:resourcekey="TemplateFieldResource2">
                                            <ItemTemplate>
                                                <%# DateFun.displayDateGrd(Eval("StartDate"), FormSession.DateType)%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ExpiryDate" SortExpression="ExpiryDate" meta:resourcekey="TemplateFieldResource3">
                                            <ItemTemplate>
                                                <%# DateFun.displayDateGrd(Eval("ExpiryDate"), FormSession.DateType)%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card Status" SortExpression="CardStatus" meta:resourcekey="TemplateFieldResource1">
                                            <ItemTemplate>
                                                <%# General.DisplayCardStatus(Eval("CardStatus"))%>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridColumn" />
                                            <ItemStyle CssClass="GridColumn" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" SortExpression="InActiveStatus" meta:resourcekey="TemplateFieldResource4">
                                            <ItemTemplate>
                                                <%# General.DisplayInActiveStatus(Eval("InActiveStatus"))%>
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
                                    </Columns>
                                    <PagerStyle BorderStyle="Outset" />
                                    <HeaderStyle CssClass="CalenderHeadBG" />
                                    <FooterStyle CssClass="CalenderHeadBG" />
                                </asp:GridView>
                            </td>
                        </tr>

                        <tr>
                            <td class="td2Allalign">
                                <table>
                                    <div id="divCountPrint" runat="server" visible="false">
                                        <tr>
                                            <td class="td2Allalign" colspan="4">
                                                <asp:Label ID="lblCntPrint" runat="server" Text="" Font-Bold="true" meta:resourcekey="lblCntPrintResource1"></asp:Label>
                                            </td>
                                        </tr>
                                    </div>
                                    <tr>
                                        <td colspan="4" valign="middle" class="borderButton">
                                            <asp:Button ID="btnAdd" runat="server" CssClass="buttonBG" Text="Add" OnClick="btnAdd_Click"
                                                Width="80px" meta:resourcekey="btnAddResource1"></asp:Button>
                                            <asp:Button ID="btnEdit" runat="server" CssClass="buttonBG" Text="Edit" OnClick="btnEdit_Click"
                                                Enabled="False" Width="80px" meta:resourcekey="btnEditResource1"></asp:Button>
                                            <asp:Button ID="btnSave" runat="server" CssClass="buttonBG" Text="Save" OnClick="btnSave_Click"
                                                ValidationGroup="Save" Enabled="False" Width="80px" meta:resourcekey="btnSaveResource1"></asp:Button>
                                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Width="80px" Text="Cancel"
                                                OnClick="btnCancel_Click" Enabled="False" meta:resourcekey="btnCancelResource1" />

                                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False"
                                                Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>
                                            <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None"
                                                ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                                EnableClientScript="False" ControlToValidate="txtValid"
                                                meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:ValidationSummary ID="vsSave" runat="server" CssClass="errorValidation" EnableClientScript="False"
                                                ValidationGroup="Save" meta:resourcekey="vsSaveResource1" />
                                            <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess" EnableClientScript="False"
                                                ValidationGroup="vgShowMsg" meta:resourcekey="vsShowMsgResource1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign">
                                            <asp:Label ID="lblEmpID" runat="server" Text="Employee ID :" meta:resourcekey="lblEmpIDResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:TextBox ID="txtEmpID" runat="server" AutoCompleteType="Disabled" Width="168px"
                                                Enabled="False" meta:resourcekey="txtEmpIDResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvtxtEmpID" runat="server" ControlToValidate="txtEmpID"
                                                EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Employee ID is required!' /&gt;"
                                                ValidationGroup="Save" meta:resourcekey="rvtxtEmpIDResource1"></asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign">
                                            <asp:Label ID="lblEmpNameAr" runat="server" Text="Employee Name (Ar) :" meta:resourcekey="lblEmpNameArResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:TextBox ID="txtEmpNameAr" runat="server" AutoCompleteType="Disabled" Width="168px"
                                                Enabled="False" meta:resourcekey="txtEmpNameArResource1"></asp:TextBox>
                                        </td>
                                        <td class="td1Allalign">
                                            <asp:Label ID="lblEmpNameEn" runat="server" Text="Employee Name (En) :" meta:resourcekey="lblEmpNameEnResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:TextBox ID="txtEmpNameEn" runat="server" AutoCompleteType="Disabled" Width="168px"
                                                Enabled="False" meta:resourcekey="txtEmpNameEnResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <%--<div id="divCon" runat="server" visible="false">
                                    <tr>
                                        <td align="<%= General.getAlign(1) %>" valign="middle">
                                            <asp:Label ID="lblComp" runat="server" Text="Company Name :" meta:resourcekey="lblCompResource1"></asp:Label>
                                        </td>
                                        <td align="<%= General.getAlign(2) %>" valign="middle">
                                            <asp:DropDownList ID="ddlCompany" runat="server" Width="173px" Enabled="false" meta:resourcekey="ddlCompanyResource1">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="<%= General.getAlign(1) %>" valign="middle">
                                            <span class="requiredStar">*</span>
                                            <asp:Label ID="lblContractors" runat="server" Text="Contract Number :" meta:resourcekey="lblContractorsResource1"></asp:Label>
                                        </td>
                                        <td align="<%= General.getAlign(2) %>" valign="middle">
                                            <asp:DropDownList ID="ddlContractors" runat="server" Width="173px" meta:resourcekey="ddlContractorsResource1"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlContractors" runat="server" ControlToValidate="ddlContractors"
                                                EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Nationality is required!' /&gt;"
                                                ValidationGroup="Save" meta:resourcekey="rfvddlContractorsResource1" Enabled="false"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </div>--%>
                                    <tr>
                                        <td class="td1align" valign="top">
                                            <span class="requiredStar">*</span>
                                            <asp:Label ID="lblIssueName" runat="server" Text="Issue Type :" meta:resourcekey="lblIssueNameResource1"></asp:Label>
                                        </td>
                                        <td class="td2align" valign="top">
                                            <asp:DropDownList ID="ddlIssue" runat="server" Width="173px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlIssue_SelectedIndexChanged" Height="16px" meta:resourcekey="ddlIssueResource1"
                                                >
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtCardCount" runat="server" Width="20px" Visible="False" meta:resourcekey="txtLostCountResource1"></asp:TextBox>
                                            <asp:CustomValidator ID="cvIssue" runat="server" Text="&lt;img src='../Images/icon/Exclamation.gif' title='!' /&gt;"
                                                ValidationGroup="Save" OnServerValidate="IssueValidate_ServerValidate" EnableClientScript="False"
                                                ControlToValidate="txtValid" meta:resourcekey="cvIssueResource1"></asp:CustomValidator>
                                        </td>
                                        <td class="td1align" valign="top">
                                            <div id="divCondition1" runat="server" visible="False">
                                                <span class="requiredStar">*</span>
                                                <asp:Label ID="lblIssueCondition" runat="server" Text="Issue Condition :" meta:resourcekey="lblIssueConditionResource1"></asp:Label>
                                            </div>
                                        </td>
                                        <td class="td2align" valign="top">
                                            <div id="divCondition2" runat="server" visible="False">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBoxList ID="cblConditions" runat="server" Width="200px" meta:resourcekey="cblConditionsResource1">
                                                            </asp:CheckBoxList>
                                                        </td>
                                                        <td>
                                                            <asp:CustomValidator ID="cvConditions" runat="server" Text="&lt;img src='../Images/icon/Exclamation.gif' title='All the conditions must be chosen' /&gt;"
                                                                ErrorMessage="All the conditions must be chosen" ValidationGroup="Save" OnServerValidate="ConditionsValidate_ServerValidate"
                                                                EnableClientScript="False" ControlToValidate="txtValid" meta:resourcekey="cvConditionsResource1"></asp:CustomValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign">
                                            <span class="requiredStar">*</span>
                                            <asp:Label ID="lblDateStart" runat="server" Text="Start Date :" meta:resourcekey="lblDateStartResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <uc:Calendar2 ID="calStartDate" runat="server" CalendarType="System" />
                                                    </td>
                                                    <td valign="middle">
                                                        <asp:CustomValidator ID="cvcalStartDate" runat="server" Text="&lt;img src='../Images/icon/Exclamation.gif' title='!' /&gt;"
                                                            ValidationGroup="Save" OnServerValidate="DateValidate_ServerValidate" EnableClientScript="False"
                                                            ControlToValidate="txtValid" meta:resourcekey="cvcalStartDateResource1"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="td1Allalign">
                                            <span class="requiredStar">*</span>
                                            <asp:Label ID="Label1" runat="server" Text="End Date :" meta:resourcekey="lblEndDateResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <uc:Calendar2 ID="calEndDate" runat="server" CalendarType="System" />
                                                    </td>
                                                    <td valign="middle">
                                                        <asp:CustomValidator ID="cvcalEndDate" runat="server" Text="&lt;img src='../Images/icon/Exclamation.gif' title='End Date is required!' /&gt;"
                                                            ValidationGroup="Save" OnServerValidate="DateValidate_ServerValidate" EnableClientScript="False"
                                                            ControlToValidate="txtValid" meta:resourcekey="cvcalEndDateResource1"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign">
                                            <span class="requiredStar">*</span>
                                            <asp:Label ID="lblCardStatus" runat="server" Text="Card Status :" meta:resourcekey="lblCardStatusResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:DropDownList ID="ddlCardstatus" runat="server" Width="173px" meta:resourcekey="ddlCardstatusResource1">
                                                <asp:ListItem Text="Editable" Value="0" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                <asp:ListItem Text="InProcess" Value="1" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                <asp:ListItem Text="Canceled" Value="4" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                <asp:ListItem Text="Active" Value="2" Enabled="False" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                                <asp:ListItem Text="InActive" Value="3" Enabled="False" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td1Allalign">
                                            <span class="requiredStar">*</span>
                                            <asp:Label ID="lblTemplateID" runat="server" Text="Template ID :"
                                                meta:resourcekey="lblTemplateIDResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:DropDownList ID="ddlTemplate" runat="server" Width="173px" meta:resourcekey="ddlTemplateResource1">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="cvTemplate" runat="server" Text="&lt;img src='../Images/icon/Exclamation.gif' title='No card Template for this employee, please add the first card Template' /&gt;"
                                                ErrorMessage="No card Template for this employee, please add the first card Template"
                                                ValidationGroup="Save" OnServerValidate="TemplateValidate_ServerValidate" EnableClientScript="False"
                                                ControlToValidate="txtValid" meta:resourcekey="cvTemplateResource1"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign">
                                            <asp:Label ID="lblDescription" runat="server" Text="Description : " meta:resourcekey="lblDescriptionResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign" colspan="3">
                                            <asp:TextBox ID="txtDescription" runat="server" Width="460px" Height="40px" Enabled="False" AutoCompleteType="Disabled"
                                                EnableViewState="False" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                    </table>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
