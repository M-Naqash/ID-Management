<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="Stickers.aspx.cs" Inherits="Stickers_Stickers" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="../Control/CardsSideMenu.ascx" TagName="CardsSideMenu" TagPrefix="uc2" %>
<%@ Register Src="~/Control/Calendar2.ascx" TagPrefix="uc" TagName="Calendar2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc2:CardsSideMenu ID="CardsSideMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script language="javascript" type="text/javascript">
        function Connect(ID) {
            try {
                var CPControl = document.applets('CPObject');
                CPControl.Start = ID;
            }
            catch (Error) { }
        }      
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnIDSearch">
            <div id="pageDiv" runat="server">
                <table border="0" cellpadding="0" cellspacing="4" width="100%">
                    <tr>
                        <td width="100%" colspan="3" class="td2align">                       
                            <table id="divUpdDel" runat="server" width="100%" >
                                <tr id="Tr1" runat="server">
                                    <td id="Td1" runat="server">
                                        <asp:ValidationSummary ID="vsFetch" runat="server" CssClass="MsgValidation" EnableClientScript="False" ValidationGroup="vgFetch"/>
                                    </td>
                                </tr>   
                                <tr id="Tr2" runat="server">
                                    <td id="Td2" class="borderButton" runat="server">
                                        <span class="requiredStar">*</span>
                                        <asp:Label ID="lblIDSearch" runat="server" Text="Registered Vehicle :" meta:resourcekey="lblIDSearchResource1"></asp:Label>
                                        <asp:TextBox ID="txtIDSearch" runat="server" Width="320px"></asp:TextBox>
                                        <asp:CustomValidator id="cvIDSearch" runat="server" ValidationGroup="vgFetch" OnServerValidate="IDSearch_ServerValidate"
                                            EnableClientScript="False" ControlToValidate="cvtxtValid"></asp:CustomValidator>
                                        &nbsp;
                                        <asp:Button ID="btnIDSearch" runat="server" OnClick="btnIDSearch_Click" ValidationGroup="vgFetch" CssClass="buttonBG" Text="Search" Width="80px" meta:resourcekey="btnIDSearchResource1"/>
                                    </td>
                                </tr>
                                  
                            </table>        
                        </td>
                    </tr>
                    <tr>
                        <td width="100%">
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdData" GridLines="None" Width="950px" runat="server" AutoGenerateColumns="False"
                                AllowPaging="True" BorderStyle="Outset" CellPadding="5" CellSpacing="5" OnRowCreated="grdData_RowCreated"
                                OnPageIndexChanging="grdData_PageIndexChanging" PageSize="5" meta:resourcekey="grdDataResource1">
                                <Columns>
                                    <asp:BoundField DataField="StickerID" HeaderText="Sticker ID" SortExpression="StickerID"
                                        meta:resourcekey="BoundFieldResource1">
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RegVehicle" HeaderText="Registered Vehicle" SortExpression="RegVehicle"
                                        meta:resourcekey="BoundFieldResource2"></asp:BoundField>
                                    <asp:BoundField DataField="EmpID" HeaderText="Employee ID" SortExpression="EmpID"
                                        meta:resourcekey="BoundFieldResource4"></asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Employee Name" SortExpression="FullName"
                                        meta:resourcekey="BoundFieldResource5"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Start Date" SortExpression="StartDate" meta:resourcekey="TemplateFieldResource2">
                                        <ItemTemplate>
                                            <%# DateFun.GrdDisplayDate(Eval("StartDate"))%>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expiry Date" SortExpression="ExpiryDate" meta:resourcekey="TemplateFieldResource1">
                                        <ItemTemplate>
                                            <%# DateFun.GrdDisplayDate(Eval("ExpiryDate"))%>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridColumn" />
                                        <ItemStyle CssClass="GridColumn" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company"
                                        meta:resourcekey="BoundFieldResource7"></asp:BoundField>
                                    <asp:BoundField DataField="StkTmpName" HeaderText="Template" SortExpression="StkTmpName"
                                        meta:resourcekey="BoundFieldResource8"></asp:BoundField>
                                </Columns>
                                <PagerStyle BorderStyle="Outset" />
                                <HeaderStyle CssClass="CalenderHeadBG" />
                                <FooterStyle CssClass="CalenderHeadBG" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Literal ID="litRegVehicle" runat="server" Text="*" meta:resourcekey="litEmpIdResource1"></asp:Literal>
                                        <asp:Label ID="lblRegVehicle" runat="server" Text="Registered Vehicle :" meta:resourcekey="lblRegVehicleResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtRegVehicle" runat="server" Text="  " Enabled="False" AutoCompleteType="Disabled"
                                            Width="168px" meta:resourcekey="txtRegVehicleResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvtxtRegVehicle" runat="server" ControlToValidate="txtRegVehicle"
                                            EnableClientScript="False" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Registered Vehicle is required!' /&gt;"
                                            ValidationGroup="vgSave" meta:resourcekey="rvtxtRegVehicleResource1"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="td1Allalign">
                                        <asp:Literal ID="LitEmployeeID" runat="server" Text="*" meta:resourcekey="litEmpIdResource1"></asp:Literal>
                                        <asp:Label ID="lblEmployeeID" runat="server" meta:resourcekey="lblEmployeeIDResource1"
                                            Text="Employee ID :"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtEmpid" runat="server" AutoCompleteType="Disabled" meta:resourcekey="txtEmpidResource1"
                                            Width="168px"></asp:TextBox>
                                        <asp:CustomValidator ID="cvEmpID" runat="server" ControlToValidate="cvtxtValid" EnableClientScript="False"
                                            meta:resourcekey="cvEmpIDResource1" OnServerValidate="EmpValidate_ServerValidate"
                                            Text="&lt;img src='../Images/icon/Exclamation.gif' title='!' /&gt;" ValidationGroup="vgSave"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <span class="requiredStar">*</span>
                                        <asp:Label ID="lblDateStart" runat="server" Text="Issue Date :" meta:resourcekey="lblDateStartResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <uc:Calendar2 ID="calStartdate" runat="server" CalendarType="System" ValidationGroup="vgSave" />
                                                </td>
                                                <td>
                                                    <asp:CustomValidator ID="cvcalStartDate" runat="server" Text="&lt;img src='../Images/icon/Exclamation.gif' title='!' /&gt;"
                                                        ValidationGroup="vgSearch" ErrorMessage="start date more than end date!" OnServerValidate="DateValidate_ServerValidate"
                                                        EnableClientScript="False" ControlToValidate="cvtxtValid" meta:resourcekey="cvcalStartDateResource1"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="td1Allalign">
                                        <span class="requiredStar">*</span>
                                        <asp:Label ID="lblEndDate" runat="server" Text="End Date :" meta:resourcekey="lblEndDateResource1"></asp:Label>
                                        </span>
                                    </td>
                                    <td class="td2Allalign">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td valign="top">
                                                    <uc:Calendar2 ID="calEnddate" runat="server" CalendarType="System" ValidationGroup="vgSave" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblOwner" runat="server" Text="Owner :" Font-Strikeout="False" meta:resourcekey="lblOwnerResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        &nbsp;<asp:TextBox ID="txtOwner" runat="server" Width="168px" meta:resourcekey="txtOwnerResource1"></asp:TextBox>
                                    </td>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblCarType" runat="server" Text="Car Type :" Font-Strikeout="False"
                                            meta:resourcekey="lblCarTypeResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        &nbsp;<asp:TextBox ID="txtCarType" runat="server" Width="168px" meta:resourcekey="txtCarTypeResource1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblModel" runat="server" Font-Strikeout="False" meta:resourcekey="lblModelResource1"
                                            Text="Model :"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtModel" runat="server" meta:resourcekey="txtModelResource1" Width="168px"></asp:TextBox>
                                    </td>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblColor" runat="server" Font-Strikeout="False" meta:resourcekey="lblColorResource1"
                                            Text="Color :"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        &nbsp;<asp:TextBox ID="txtColor" runat="server" meta:resourcekey="txtColorResource1"
                                            Width="168px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td class="td1Allalign">
                                        <span class="requiredStar">*</span>
                                        <asp:Label ID="lblTemplateID" runat="server" meta:resourcekey="lblTemplateIDResource1"
                                            Text="Template ID :"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:DropDownList ID="ddlTemplate" runat="server" AutoPostBack="True" 
                                            Enabled="False" Height="16px"
                                            meta:resourcekey="ddlTemplateResource1" Width="173px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlTemplate" runat="server" ControlToValidate="ddlTemplate"
                                            EnableClientScript="False" meta:resourcekey="rfvddlTemplateResource1" Text="&lt;img src='../Images/icon/Exclamation.gif' title='Template ID is required!' /&gt;"
                                            ValidationGroup="vgSave"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblCompany" runat="server" meta:resourcekey="lblCompanyResource1"
                                            Text="Company :" Visible="False"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:DropDownList ID="ddlCompID" runat="server" 
                                            meta:resourcekey="ddlCompIDResource1" Visible="False"
                                            Width="173px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="middle" class="borderButton">
                            &nbsp;&nbsp;
                            <asp:Button ID="btnSave" runat="server" CssClass="buttonBG" Text="Save" OnClick="btnSave_Click"
                                ValidationGroup="vgSave" Width="80px" meta:resourcekey="btnSaveResource1" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonBG" Text="Cancel" OnClick="btnCancel_Click"
                                Width="80px" meta:resourcekey="btnCancelResource1" />
                            <asp:TextBox ID="cvtxtValid" runat="server" Text="02120" Visible="False" 
                                Width="10px" meta:resourcekey="cvtxtValidResource1"></asp:TextBox>
                            <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" ValidationGroup="ShowMsg"
                                OnServerValidate="ShowMsg_ServerValidate" EnableClientScript="False" ControlToValidate="cvtxtValid"
                                meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="td2Allalign" width="100%">
                            <asp:ValidationSummary ID="vsSave" runat="server" CssClass="MsgValidation" EnableClientScript="False"
                                ValidationGroup="vgSave" meta:resourcekey="vsSaveResource1" />
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
