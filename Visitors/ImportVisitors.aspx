<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="ImportVisitors.aspx.cs" Inherits="Visitors_ImportVisitors" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

    <%@ Register Src="../Control/VisitorsSideMenu.ascx" TagName="VisitorsSideMenu" TagPrefix="CSM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <CSM:VisitorsSideMenu ID="SideMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
         <Triggers>
           <asp:PostBackTrigger ControlID="btnImport" />
           <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="pnlMain" runat="server" DefaultButton="btnImport">
            <div id="pageDiv" runat="server">
                <table border="0" cellpadding="0" cellspacing="4" width="100%">  
                    <tr>
                        <td class="borderButton">
                            <asp:Label ID="lblFilePath" runat="server" Text="File Path:" 
                                meta:resourcekey="lblFilePathResource1"></asp:Label>
                            &nbsp;
                            <asp:FileUpload ID="fudFilePath" runat="server" Width="500px" 
                                meta:resourcekey="fudFilePathResource1"/>
                            &nbsp;
                            <asp:ImageButton ID="btnImport" runat="server" OnClick="btnImport_Click" 
                                ImageUrl="~/Images/icon/table_import.png" 
                                meta:resourcekey="btnImportResource1"/>
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            &nbsp;
                            <asp:ImageButton ID="btnExport" runat="server" OnClick="btnExport_Click" 
                                ImageUrl="~/images/icon/export_excel.png" 
                                meta:resourcekey="btnExportResource1"/>
                            <asp:TextBox ID="txtValid" runat="server" Text="02120" Visible="False" 
                                Width="10px" meta:resourcekey="txtValidResource1"></asp:TextBox>
                            <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                                ValidationGroup="vgShowMsg" OnServerValidate="ShowMsg_ServerValidate" 
                                EnableClientScript="False" ControlToValidate="txtValid" 
                                meta:resourcekey="cvShowMsgResource1"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td2Allalign" width="100%">
                            <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess"   
                                EnableClientScript="False" ValidationGroup="vgShowMsg" 
                                meta:resourcekey="vsShowMsgResource1"/>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="Body_BG" valign="top" align="center">
                            <table>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblAddCount" runat="server" Text="Add Import Rows # :" meta:resourcekey="lblAddCountResource1" 
                                            ></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtAddCount" runat="server" AutoCompleteType="Disabled" 
                                            Enabled="False" Width="168px" meta:resourcekey="txtAddCountResource1"></asp:TextBox>
                                    </td>                                         
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblErrCount" runat="server" Text="Error Import Rows # :" 
                                            meta:resourcekey="lblErrCountResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtErrCount" runat="server" AutoCompleteType="Disabled" 
                                            Enabled="False" Width="168px" meta:resourcekey="txtErrCountResource1"></asp:TextBox>
                                    </td>                                         
                                </tr>
                                <tr>
                                    <td class="td1Allalign">
                                        <asp:Label ID="lblBlackListCount" runat="server" Text="Black List Import Rows # :" 
                                            meta:resourcekey="lblBlackListCountResource1"></asp:Label>
                                    </td>
                                    <td class="td2Allalign">
                                        <asp:TextBox ID="txtBlackListCount" runat="server" AutoCompleteType="Disabled" 
                                            Enabled="False" Width="168px" meta:resourcekey="txtBlackListCountResource1"></asp:TextBox>
                                    </td>                                         
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="Body_BG">
                            <table>
                                <tr>
                                    <td class="td2Allalign">
                                        <asp:Label ID="lblNote1" runat="server" 
                                            Text="Note 1: The date format must be dd/MM/yyyy according to the calendar in the program." 
                                            ForeColor="Red" meta:resourcekey="lblNote1Resource1"></asp:Label>
                                    </td>                                        
                                </tr>
                                <tr>
                                    <td class="td2Allalign">
                                        <asp:Label ID="lblNote2" runat="server" 
                                            Text="Note 2: The allowable values for regions is 0 or 1." 
                                            ForeColor="Red" meta:resourcekey="lblNote2Resource1"></asp:Label>
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

