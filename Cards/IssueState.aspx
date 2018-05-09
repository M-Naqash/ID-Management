<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true"
    CodeFile="IssueState.aspx.cs" Inherits="IssueState" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register src="../Control/CardsSideMenu.ascx" tagname="CardsSideMenu" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc2:cardssidemenu id="CardsSideMenu1" runat="server" Visible ="False"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript" language="javascript">
        function expandcollapse(obj, row) {
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);

            if (div.style.display == "none") {
                div.style.display = "block";
                if (row == 'alt') {
                    img.src = "../Images/icon/minus.gif";
                }
                else {
                    img.src = "../Images/icon/minus.gif";
                }
                img.alt = "Close to view other ";
            }
            else {
                div.style.display = "none";
                if (row == 'alt') {
                    img.src = "../Images/icon/plus.gif";
                }
                else {
                    img.src = "../Images/icon/plus.gif";
                }
                img.alt = "Expand to show ";
            }
        }
 
    </script>
       
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnSave">
            <div id="pageDiv" runat="server">
                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                    <tr>
                        <td colspan="4" align="center">
                            <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="IsID" BorderStyle="Outset" 
                                CellPadding="5" CellSpacing="5"
                                OnRowCreated="grdData_RowCreated" AllowPaging="True" OnPageIndexChanging="grdData_PageIndexChanging"
                                EnableModelValidation="True"
                                Width="950px" meta:resourcekey="grdDataResource1">
                                <Columns>
                                    <asp:BoundField DataField="IsID" HeaderText="ID" ReadOnly="True" SortExpression="IsID" meta:resourcekey="BoundFieldResource1">
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IsNameAr" HeaderText="Name (Ar)" SortExpression="IsNameAr" meta:resourcekey="BoundFieldResource22">
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IsNameEn" HeaderText="Name (En)" SortExpression="IsNameEn" meta:resourcekey="BoundFieldResource2">
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Is Repeat" SortExpression="IsRepeat" meta:resourcekey="BoundFieldResource4">
                                        <ItemTemplate>
                                            <%# GrdDisplayBool(Eval("IsRepeat")) %>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is Condition" SortExpression="ISCondition"  meta:resourcekey="BoundFieldResource5">
                                        <ItemTemplate>
                                            <%# GrdDisplayBool(Eval("ISCondition")) %>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="CalenderHeadBG" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" colspan="4">
                            <asp:ValidationSummary ID="vsSave" runat="server" CssClass="errorValidation" 
                                EnableClientScript="False" ValidationGroup="Save" 
                                meta:resourcekey="vsSaveResource1"/>
                        </td>
                    </tr>
            
                    <div id="divUpdDel" runat="server" visible="False">
                        <tr>
                            <td class="td1Allalign">
                                <span class="requiredStar">*</span>
                                <asp:Label ID="lblIssueID" runat="server" Text="Issue ID :" meta:resourcekey="lblIssueIDResource1"></asp:Label>
                            </td>
                            <td class="td2Allalign">
                                <asp:DropDownList ID="ddlIssue" runat="server" Width="173px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlIssue_SelectedIndexChanged" 
                                    meta:resourcekey="ddlIssueResource1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlIssue" runat="server" ControlToValidate="ddlIssue"
                                    EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Issue ID is required!' /&gt;"
                                    ValidationGroup="Save" Enabled="False" 
                                    meta:resourcekey="rfvddlIssueResource1"></asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </div>
                    <tr>
                        <td class="td1Allalign" width="200px">
                            <span class="requiredStar">*</span>
                            <asp:Label ID="lblIssueNameAr" runat="server" Text="Issue Name (Ar) :" meta:resourcekey="lblIssueNameArResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtIssueNameAr" runat="server" AutoCompleteType="Disabled" 
                                Width="168px" meta:resourcekey="txtIssueNameArResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtIssueNameAr" runat="server" ControlToValidate="txtIssueNameAr"
                                EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Issue Name (Ar) is required!' /&gt;"
                                ValidationGroup="Save" meta:resourcekey="rfvtxtIssueNameArResource1"></asp:RequiredFieldValidator>
                        </td>
                        <td class="td1Allalign">
                            <span class="requiredStar">*</span>
                            <asp:Label ID="lblIssueNameEn" runat="server" Text="Issue Name (En) :" meta:resourcekey="lblIssueNameEnResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <asp:TextBox ID="txtIssueNameEn" runat="server" AutoCompleteType="Disabled" 
                                Width="168px" meta:resourcekey="txtIssueNameEnResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtIssueNameEn" runat="server" ControlToValidate="txtIssueNameEn"
                                EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Issue Name (En) is required!' /&gt;"
                                ValidationGroup="Save" meta:resourcekey="rfvtxtIssueNameEnResource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1Allalign">
                            <span class="requiredStar">*</span>
                            <asp:Label ID="lblIsRepeat" runat="server" Text="Is Repeat:" meta:resourcekey="lblIsRepeatResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdlIsRepeat" runat="server" RepeatDirection="Horizontal"
                                            meta:resourcekey="rdlIsRepeatResource1">
                                            <asp:ListItem Value="1" meta:resourcekey="ListItemResource4">True</asp:ListItem>
                                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource5">False</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>      
                                        <asp:RequiredFieldValidator ID="rfvrdlIsRepeat" runat="server" ControlToValidate="rdlIsRepeat"
                                            EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Is Repeat is required!' /&gt;"
                                            ValidationGroup="Save" meta:resourcekey="rfvrdlIsRepeatResource1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="td1Allalign">
                            <span class="requiredStar">*</span>
                            <asp:Label ID="lblIscondition" runat="server" Text="Is Condition :" meta:resourcekey="lblIsconditionResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdlIsCondition" runat="server" RepeatDirection="Horizontal"
                                            meta:resourcekey="rdlIsConditionResource1" 
                                            onselectedindexchanged="rdlIsCondition_SelectedIndexChanged" 
                                            AutoPostBack="True">
                                            <asp:ListItem Value="1" meta:resourcekey="ListItemResource6">True</asp:ListItem>
                                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource7">False</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td> 
                                        <asp:RequiredFieldValidator ID="rfvrdlIsCondition" runat="server" ControlToValidate="rdlIsCondition"
                                            EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Is Condition is required!' /&gt;"
                                            ValidationGroup="Save" meta:resourcekey="rfvrdlIsConditionResource1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <div id="divIssue" runat="server">
                        <tr>
                            <td class="td1Allalign">
                                <asp:Label ID="lblNewIssueCondition" runat="server" Text="New Issue Condition :"
                                    meta:resourcekey="lblNewIssueConditionResource1"></asp:Label>
                            </td>
                            <td class="td2Allalign" colspan="3">
                                <asp:TextBox ID="txtIssueCondition" runat="server" Width="168px"
                                    meta:resourcekey="txtIssueConditionResource1" AutoCompleteType="Disabled"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="rfvtxtIssueCondition" runat="server" ControlToValidate="txtIssueCondition"
                                    EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='New Issue Condition is required!' /&gt;"
                                    ValidationGroup="AddIss" meta:resourcekey="rfvtxtIssueConditionResource1"></asp:RequiredFieldValidator>

                                &nbsp;&nbsp;
                                <asp:Button ID="btnAddIssue" runat="server" CssClass="buttonBG" Text="Add Condition"
                                    OnClick="btnAddIssue_Click" ValidationGroup="AddIss" meta:resourcekey="btnAddIssueResource1"
                                    Width="100px"></asp:Button>
                                &nbsp;&nbsp;
                                <asp:Button ID="btnDeleteIssue" runat="server" CssClass="buttonBG" Text="Delete Condition"
                                    OnClick="btnDeleteIssue_Click" ValidationGroup="DelIss" meta:resourcekey="btnDeleteIssueResource1"
                                    Width="100px"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="td1align" valign="top">
                                <span class="requiredStar" runat="server" id="spnConditions" visible="False">*</span>
                                <asp:Label ID="lblConditions" runat="server" Text="Conditions :" meta:resourcekey="lblConditionsResource1"></asp:Label>
                                <asp:CustomValidator id="cvcblConditions" runat="server"
                                    Text="&lt;img src='../Images/icon/Exclamation.gif' title='Conditions is required!' /&gt;" 
                                    ValidationGroup="Save" OnServerValidate="ConditionsValidate_ServerValidate"
                                    EnableClientScript="False" ControlToValidate="txtValid" 
                                    meta:resourcekey="cvcblConditionsResource1"></asp:CustomValidator>
                            </td>
                            <td class="td2Allalign" colspan="3">
                                <table border="0" cellpadding="0" cellspacing="0" >
                                    <tr>
                                        <td>                            
                                            <asp:CheckBoxList ID="cblConditions" runat="server" Width="300px" 
                                                meta:resourcekey="cblConditionsResource2">
                                            </asp:CheckBoxList>
                                        </td>
                                        <td> 
                                            <asp:CustomValidator id="cvDelConditions" runat="server"
                                                Text="&lt;img src='../Images/icon/Exclamation.gif' title='Conditions is required!' /&gt;" 
                                                ValidationGroup="DelIss" OnServerValidate="ConditionsValidate_ServerValidate"
                                                EnableClientScript="False" ControlToValidate="txtValid" 
                                                meta:resourcekey="cvDelConditionsResource1"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </div>
                    <tr>
                        <td class="td1Allalign">
                            <asp:Label ID="lblIssueDescription" runat="server" Text="Issue Description :" meta:resourcekey="lblIssueDescriptionResource1"></asp:Label>
                        </td>
                        <td class="td2Allalign" colspan="3">
                            <asp:TextBox ID="txtIssuedescription" runat="server" TextMode="MultiLine" 
                                Width="750px" EnableViewState="False" 
                                meta:resourcekey="txtIssuedescriptionResource1"></asp:TextBox>
                        </td>
                    </tr>
            
                    <tr>
                        <td colspan="4" valign="middle" align="center" class="borderButton">
                            <asp:Button ID="btnSave" runat="server" CssClass="buttonBG" Text="Save"
                                OnClick="btnSave_Click" ValidationGroup="Save"
                                meta:resourcekey="btnSaveIssueResource1" Width="60px">
                            </asp:Button>
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Text="Cancel" meta:resourcekey="btnCancelResource1"
                                OnClick="btnCancel_Click" Width="60px" />
                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" 
                                Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>
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
                    
                </table>
            </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
