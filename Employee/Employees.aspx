<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true"
    CodeFile="Employees.aspx.cs" Inherits="Employee_Employees" %>

<%@ Register Src="../Control/Calendar2.ascx" TagPrefix="uc" TagName="Calendar2" %>
<%@ Register Src="../Control/ImageCtl.ascx" TagName="ImageCtl" TagPrefix="uc3" %>
<%@ Register Src="../Control/EmployeeSideMenu.ascx" TagName="EmployeeSideMenu" TagPrefix="CSM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <CSM:EmployeeSideMenu ID="EmployeeSideMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript" language="JavaScript">
        window.onbeforeunload = function (e) { document.getElementById('<%=btnCancel.ClientID %>').click(); }

    </script>
    <asp:Panel ID="pnlMain" runat="server">
        <div id="pageDiv" runat="server">
            <table border="0" cellpadding="0" cellspacing="4" width="100%">
                <tr>
                    <td width="100%" colspan="3" class="td2align">
                        <table id="Table1" runat="server" width="100%">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <asp:ValidationSummary ID="vsFetch" runat="server" CssClass="MsgValidation" EnableClientScript="False"
                                        ValidationGroup="vgFetch" />
                                </td>
                            </tr>
                            <tr id="Tr2" runat="server">
                                <td id="Td2" class="borderButton" runat="server">
                                    <span class="requiredStar">*</span>
                                    <asp:Label ID="lblIDSearch" runat="server" Text="Employee Identity :" meta:resourcekey="lblIDSearchResource1"></asp:Label>
                                    <asp:TextBox ID="txtIDSearch" runat="server" Width="320px"></asp:TextBox>
                                    <asp:CustomValidator ID="cvIDSearch" runat="server" ValidationGroup="vgFetch" OnServerValidate="IDSearch_ServerValidate"
                                        EnableClientScript="False" ControlToValidate="cvtxtValid"></asp:CustomValidator>
                                    &nbsp;
                                    <asp:Button ID="btnIDSearch" runat="server" OnClick="btnIDSearch_Click" ValidationGroup="vgFetch"
                                        CssClass="buttonBG" Text="Search" Width="80px" meta:resourcekey="btnIDSearchResource1" />
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td id="Td3" runat="server">
                                    <asp:ValidationSummary ID="vsSearch" runat="server" CssClass="MsgValidation" EnableClientScript="False"
                                        ValidationGroup="vgSearch" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnUploadDoc" />
                                <asp:PostBackTrigger ControlID="CalBirthDate" />
                                <asp:PostBackTrigger ControlID="CalHireDate" />
                            </Triggers>
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td class="td1Allalign">
                                            <asp:Literal ID="litEmpId" runat="server" Text="*"></asp:Literal>
                                            <asp:Label ID="lblEmpIdentity" runat="server" Text="Employee Identity:" meta:resourcekey="lblEmpIdentityResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:TextBox ID="txtEmpIdentity" runat="server" AutoCompleteType="Disabled" Width="168px"
                                                MaxLength="10" Enabled="False" meta:resourcekey="txtEmpIdentityResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvEmpIdentity" runat="server" ControlToValidate="txtEmpIdentity"
                                                EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Employee Identity is required!' /&gt;"
                                                ValidationGroup="vgSave" meta:resourcekey="rvEmpIdentityResource1"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="td1Allalign">
                                            <asp:Label ID="lblEmpCardType" runat="server" Text="Employee Type :" meta:resourcekey="lblEmpCardTypeResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:DropDownList ID="ddlEmpType" runat="server" Width="173px" Enabled="False">
                                                <asp:ListItem Text="Aramco Employee" Value="Mng" meta:resourcekey="ListItemManagerResource1"></asp:ListItem>
                                                <asp:ListItem Text="Third party" Value="Emp" meta:resourcekey="ListItemEmployeeResource1"></asp:ListItem>
                                                <asp:ListItem Text="Contractor" Value="Con" meta:resourcekey="ListItemContractorResource1"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlEmpType" runat="server" ControlToValidate="ddlEmpType"
                                                EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Employee Type is required!' /&gt;"
                                                ValidationGroup="Save" meta:resourcekey="rfvddlEmpTypeResource1"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign">
                                            <asp:Literal ID="LitEmpNameAr" runat="server" Text="*" meta:resourcekey="LitEmpNameArResource1"></asp:Literal>
                                            <asp:Label ID="lblEmpNameAr" runat="server" Text="Employee Name (Ar) :" meta:resourcekey="lblEmpNameArResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign" colspan="2">
                                            <asp:TextBox ID="txtEmpNameAr" runat="server" Width="268px" Enabled="False" onkeypress="return ArabicOnly(event);"
                                                meta:resourcekey="txtEmpNameArResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvEmpNameAr" runat="server" EnableClientScript="False"
                                                ControlToValidate="txtEmpNameAr" ValidationGroup="vgSave" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Employee Name (Ar) is required!' /&gt;"
                                                meta:resourcekey="rvEmpNameArResource1"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign">
                                            <asp:Literal ID="LitEmpNameEn" runat="server" Text="*" meta:resourcekey="LitEmpNameEnResource1"></asp:Literal>
                                            <asp:Label ID="lblEmpNameEn" runat="server" Text="Employee Name (En) :" meta:resourcekey="lblEmpNameEnResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign" colspan="2">
                                            <asp:TextBox ID="txtEmpNameEn" runat="server" Width="268px" Enabled="False" onkeypress="return EnglishOnly(event);"
                                                meta:resourcekey="txtEmpNameEnResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvEmpNameEn" runat="server" EnableClientScript="False"
                                                meta:resourcekey="rvEmpNameEnResource1" ControlToValidate="txtEmpNameEn" ValidationGroup="vgSave"
                                                Text="&lt;img src='../Images/icon/Exclamation.gif' title='Employee Name (En) is required!' /&gt;"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign">
                                            <asp:Literal ID="LitNationality" runat="server" Text="*" meta:resourcekey="LitNationalityResource1"></asp:Literal>
                                            <asp:Label ID="lblNationality" runat="server" Text="Nationality :" meta:resourcekey="lblNationalityResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:DropDownList ID="ddlNatID" runat="server" Width="173px" Enabled="False">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlNatID" runat="server" EnableClientScript="False"
                                                ControlToValidate="ddlNatID" ValidationGroup="vgSave" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Nationality is required!' /&gt;"
                                                meta:resourcekey="rfvddlNatIDResource1"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="td1Allalign">
                                            <asp:Literal ID="LitBirthDate" runat="server" Text="*" meta:resourcekey="LitBirthDateResource1"></asp:Literal>
                                            <asp:Label ID="lblBirthDate" runat="server" Text="Birth Date :" meta:resourcekey="lblBirthDateResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <uc:Calendar2 ID="CalBirthDate" runat="server" CalendarType="System" />
                                                    </td>
                                                    <td valign="middle">
                                                        <asp:CustomValidator ID="cvcalBirthDate" runat="server" Text="&lt;img src='../Images/icon/Exclamation.gif' title='!' /&gt;"
                                                            ValidationGroup="vgSave" OnServerValidate="BirthDateValidate_ServerValidate"
                                                            EnableClientScript="False" ControlToValidate="cvtxtValid" meta:resourcekey="cvcalBirthDateResource1"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign">
                                            <asp:Literal ID="LitJobTitleAr" runat="server" Text="*" meta:resourcekey="LitJobTitleArResource1"></asp:Literal>
                                            <asp:Label ID="lblJobTitleAr" runat="server" Text="Job Title (Ar) :" meta:resourcekey="lblJobTitleArResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:TextBox ID="txtJobTitleAr" runat="server" Width="168px" Enabled="False" onkeypress="return ArabicOnly(event);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtJobTitleAr" runat="server" EnableClientScript="False"
                                                ControlToValidate="txtJobTitleAr" ValidationGroup="vgSave" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Job title (Ar) is required!' /&gt;"
                                                meta:resourcekey="rfvtxtJobTitleArResource1"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="td1Allalign">
                                            <asp:Literal ID="LitJobTitleEn" runat="server" Text="*" meta:resourcekey="LitJobTitleResource1"></asp:Literal>
                                            <asp:Label ID="lblJobTitleEn" runat="server" Text="Job Title (En) :" meta:resourcekey="lblJobTitleResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:TextBox ID="txtJobTitleEn" runat="server" Width="168px" Enabled="False" onkeypress="return EnglishOnly(event);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtJobTitleEn" runat="server" EnableClientScript="False"
                                                ControlToValidate="txtJobTitleEn" ValidationGroup="vgSave" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Job title (En) is required!' /&gt;"
                                                meta:resourcekey="rfvtxtJobTitleEnResource1"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign">
                                            <asp:Literal ID="LitNationalID" runat="server" Text="*" meta:resourcekey="LitNationalIDResource1"></asp:Literal>
                                            <asp:Label ID="lblEmpNationalID" runat="server" Text="National\Iqama ID :" meta:resourcekey="lblEmpNationalIDResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:TextBox ID="txtEmpNationalID" runat="server" Width="168px" Enabled="False" onkeypress="return NumberOnly(event);"
                                                meta:resourcekey="txtEmpNationalIDResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvtxtEmpNationalID" runat="server" EnableClientScript="False"
                                                ControlToValidate="txtEmpNationalID" ValidationGroup="vgSave" Text="&lt;img src='../Images/icon/Exclamation.gif' title='National\Iqama ID is required!' /&gt;"
                                                meta:resourcekey="rvtxtEmpNationalIDResource1"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="td1Allalign">
                                            <asp:Literal ID="LitBloodGroup" runat="server" Text="*" meta:resourcekey="LitBloodGroupResource1"></asp:Literal>
                                            <asp:Label ID="lblBloodGroup" runat="server" Text="Blood Group :" meta:resourcekey="lblBloodGroupResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:DropDownList ID="ddlBloodGroup" runat="server" Width="173px" Enabled="False">
                                                <asp:ListItem Value="0" Text="-Select Blood Group-" meta:resourcekey="ListItemBloodGroupResource1"></asp:ListItem>
                                                <asp:ListItem Text="A+" Value="A+"></asp:ListItem>
                                                <asp:ListItem Text="A-" Value="A-"></asp:ListItem>
                                                <asp:ListItem Text="B+" Value="B+"></asp:ListItem>
                                                <asp:ListItem Text="B-" Value="B-"></asp:ListItem>
                                                <asp:ListItem Text="O+" Value="O+"></asp:ListItem>
                                                <asp:ListItem Text="O-" Value="O-"></asp:ListItem>
                                                <asp:ListItem Text="AB+" Value="AB+"></asp:ListItem>
                                                <asp:ListItem Text="AB-" Value="AB-"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlBloodGroup" runat="server" InitialValue="0"
                                                EnableClientScript="False" ControlToValidate="ddlBloodGroup" ValidationGroup="vgSave"
                                                Text="&lt;img src='../Images/icon/Exclamation.gif' title='Blood Group is required!' /&gt;"
                                                meta:resourcekey="rfvddlBloodGroupResource1">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <div id="divContract" runat="server" visible="False">
                                        <tr>
                                            <td class="td1Allalign">
                                                <asp:Literal ID="LitCompID" runat="server" Text="*" meta:resourcekey="LitCompIDResource1"></asp:Literal>
                                                <asp:Label ID="lblCompID" runat="server" Text="Compnay :" meta:resourcekey="lblCompIDResource1"></asp:Label>
                                            </td>
                                            <td class="td2Allalign">
                                                <asp:DropDownList ID="ddlCompID" runat="server" Width="173px" Enabled="False">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCompID" runat="server" EnableClientScript="False"
                                                    meta:resourcekey="rfvCompIDResource1" ControlToValidate="ddlCompID" ValidationGroup="vgSave"
                                                    Text="&lt;img src='../Images/icon/Exclamation.gif' title='Compnay is required!' /&gt;"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </div>
                                    <div id="divSection" runat="server" visible="False">
                                        <tr>
                                            <td class="td1Allalign">
                                                <asp:Literal ID="litSecID" runat="server" Text="*" meta:resourcekey="litSecIDResource1"></asp:Literal>
                                                <asp:Label ID="lblSections" runat="server" Text="Section :" meta:resourcekey="lblSectionsResource1"></asp:Label>
                                            </td>
                                            <td class="td2Allalign">
                                                <asp:DropDownList ID="ddlSecID" runat="server" Width="173px" Enabled="False">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSecID" runat="server" EnableClientScript="False"
                                                    meta:resourcekey="rfvSecIDResource1" ControlToValidate="ddlSecID" ValidationGroup="vgSave"
                                                    Text="&lt;img src='../Images/icon/Exclamation.gif' title='Section is required!' /&gt;"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </div>
                                    <tr>
                                        <td class="td1Allalign">
                                            <asp:Literal ID="LitEmail" runat="server" Text="*" meta:resourcekey="LitEmailResource1"></asp:Literal>
                                            <asp:Label ID="lblEmail" runat="server" Text="Email :" meta:resourcekey="lblEmailResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:TextBox ID="txtEmail" runat="server" Width="168px" Enabled="False"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="rvEmailIDCorrect" runat="server" ErrorMessage="Please enter email in correct format"
                                                Text="&lt;img src='../Images/icon/Exclamation.gif' title='Please enter email in correct format!' /&gt;"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"
                                                EnableClientScript="False" ValidationGroup="vgSave" meta:resourcekey="rvEmailIDCorrectResource1"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" EnableClientScript="False"
                                                ControlToValidate="txtEmail" ValidationGroup="vgSave" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Email is required!' /&gt;"
                                                meta:resourcekey="rfvtxtEmailResource1"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="td1Allalign">
                                            <asp:Label ID="lblMobile" runat="server" Text="Mobile :" meta:resourcekey="lblMobileResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:TextBox ID="txtMobile" runat="server" Width="168px" Enabled="False" onkeypress="return NumberOnly(event);"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign">
                                            <asp:Literal ID="LitIdHireDate" runat="server" Text="*" Visible="False" meta:resourcekey="LitIdHireDateResource1"></asp:Literal>
                                            <asp:Label ID="lblHireDate" runat="server" Text="Hire Date :" meta:resourcekey="lblHireDateResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <uc:Calendar2 ID="CalHireDate" runat="server" CalendarType="System" />
                                                    </td>
                                                    <td valign="middle">
                                                        <asp:CustomValidator ID="cvCalHireDate" runat="server" Text="&lt;img src='../Images/icon/Exclamation.gif' title='!' /&gt;"
                                                            ValidationGroup="vgSave" OnServerValidate="HireDateValidate_ServerValidate" EnableClientScript="False"
                                                            ControlToValidate="cvtxtValid"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="td1Allalign">
                                            <asp:Literal ID="LitGender" runat="server" Text="*" Visible="False" meta:resourcekey="LitGenderResource1"></asp:Literal>
                                            <asp:Label ID="lblGender" runat="server" Text="Gender :" meta:resourcekey="lblGenderResource1"></asp:Label>
                                        </td>
                                        <td class="td2Allalign">
                                            <asp:RadioButtonList ID="rdlGender" runat="server" Enabled="False" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="M" meta:resourcekey="ListItemMaleResource1">Male</asp:ListItem>
                                                <asp:ListItem Value="F" meta:resourcekey="ListItemFemaleResource1">Female</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td2align" colspan="4">
                                            <table width="100%">
                                                <tr>
                                                    <td class="td2align">
                                                        <asp:Label ID="lblDocName" runat="server" Text="Document Name :" meta:resourcekey="lblDocNameResource1"></asp:Label>
                                                        &nbsp;
                                                        <asp:TextBox ID="txtDocName" runat="server" Width="126px" meta:resourcekey="txtDocNameResource1"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:FileUpload ID="fuDocs" runat="server" meta:resourcekey="fuDocsResource1" />
                                                        &nbsp;
                                                        <asp:Button ID="btnUploadDoc" runat="server" CssClass="buttonBG" Text="Upload" OnClick="btnUploadDoc_Click"
                                                            meta:resourcekey="btnUploadDocResource1"></asp:Button>
                                                        <asp:RegularExpressionValidator ID="refUploadFile" runat="server" ControlToValidate="fuDocs" meta:resourcekey="refUploadFileResource1"
                                                            ErrorMessage="jpg and pdf formats are allowed to be upload" ValidationExpression="(.+\.([Pp][Dd][Ff])|.+\.([Jj][Pp][Gg]))"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td2align">
                                                        <asp:GridView ID="grdDocs" runat="server" DataKeyNames="DocID" AutoGenerateColumns="False"
                                                            OnRowDataBound="grdDocs_RowDataBound" OnRowCommand="grdDocs_RowCommand" OnRowDeleting="grdDocs_RowDeleting"
                                                            CellPadding="4" Width="100%" ForeColor="#333333" GridLines="None" meta:resourcekey="grdDocsResource1">
                                                            <RowStyle BackColor="#EFF3FB" />
                                                            <Columns>
                                                                <asp:BoundField DataField="DocID" Visible="False" SortExpression="DocID" meta:resourcekey="BoundFieldResource1" />
                                                                <asp:BoundField DataField="EmpID" Visible="False" SortExpression="EmpID" meta:resourcekey="BoundFieldResource2" />
                                                                <asp:BoundField DataField="DocName" HeaderText="Document Name" SortExpression="DocName"
                                                                    meta:resourcekey="BoundFieldResource3" />
                                                                <asp:BoundField DataField="DocPath" HeaderText="Document Path" SortExpression="DocPath"
                                                                    Visible="false" meta:resourcekey="BoundFieldResource4" />
                                                                <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgbtnDownload" runat="server" CommandName="Doc_Download" CommandArgument='<%# Eval("DocPath") %>'
                                                                            ImageUrl="~/Images/icon/downFile.png" meta:resourcekey="imgbtnDownloadResource1" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Doc_Delete" CommandArgument='<%# Eval("DocID") + ";" + Eval("DocPath") %>'
                                                                            ImageUrl="~/Images/icon/button_delete.png" meta:resourcekey="imgbtnDeleteResource2" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td valign="top">
                        <table>
                            <tr>
                                <td class="td2align">
                                    <uc3:ImageCtl ID="EmpImage" runat="server" txtID="txtEmpNationalID" Type="Employee"
                                        IsEncryption="true" CaptureEnable="true" EmptyIDMsgEn="Please enter Employee Identity"
                                        EmptyIDMsgAr="من فضلك أدخل رقم الهوية" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" class="borderButton">
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSave" runat="server" CssClass="buttonBG" Text="Save" Width="100px"
                            ValidationGroup="vgSave" Enabled="False" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Text="Cancel" Width="100px"
                            OnClick="btnCancel_Click" meta:resourcekey="btnCancelResource1" />
                        &nbsp;
                        <asp:TextBox ID="cvtxtValid" runat="server" Text="02120" Visible="False" Width="10px"
                            meta:resourcekey="cvtxtValidResource1"></asp:TextBox>
                        <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" ValidationGroup="ShowMsg"
                            OnServerValidate="ShowMsg_ServerValidate" EnableClientScript="False" ControlToValidate="cvtxtValid"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td width="100%" colspan="3" class="td2align">
                        <asp:ValidationSummary ID="vsSave" runat="server" CssClass="MsgValidation" EnableClientScript="False"
                            ValidationGroup="vgSave" meta:resourcekey="vsSaveResource1" />
                        <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess" EnableClientScript="False"
                            ValidationGroup="vgShowMsg" meta:resourcekey="vsShowMsgResource1" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
