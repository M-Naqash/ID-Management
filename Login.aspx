<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div id="pageDiv" runat="Server">
    <center>
        <table>
            <tr>                    
                <td id="loginTD" runat="server" class="tp_logoin">
                    <table width="100%">
                        <tr>
                            <td width="<%= getwidth() %>"></td> <%--width="25%"--%>
                            <td>
                                <table cellspacing="10" style="height: 21px; width: 414px;"> 
                                    <tr>
                                        <td colspan="3">
                                            <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess" EnableClientScript="False" ValidationGroup="vgShowMsg" />
                                        </td>   
                                    </tr>   
                                    <tr>
                                        <td class="td1Allalign">
                                            <span class="requiredStar" style="font-size: medium; font-weight: 700;">*</span>
                                            <asp:Label ID="lbltxtLoginID" runat="server" Text="Login ID :" 
                                                meta:resourcekey="lbltxtLoginIDResource1" 
                                                style="text-align: center; font-size: medium" Font-Bold="True" ></asp:Label>
                                        </td>
                                        <td class="td2Allalign" width="200px">
                                            <asp:TextBox ID="txtLoginID" runat="server" Width="200px" 
                                                meta:resourcekey="txtLoginIDResource2" Font-Bold="True" Font-Size="Medium" 
                                                Height="22px" BorderStyle="None"></asp:TextBox>
                                        </td> 
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvtxtLoginID" runat="server" ControlToValidate="txtLoginID"
                                                EnableClientScript="False" Text="&lt;img src='Images/icon/Exclamation.gif' title='Login ID is required!' /&gt;"
                                                ValidationGroup="Save" meta:resourcekey="rfvtxtLoginIDResource1"></asp:RequiredFieldValidator>
                                        </td>                     
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign">
                                            <span class="requiredStar" style="font-size: medium; font-weight: 700;">*</span>
                                            <asp:Label ID="lblPassword" runat="server" Text="Password :" 
                                                meta:resourcekey="lblPasswordResource1" style="font-size: medium" 
                                                Font-Bold="True"></asp:Label>
                                        </td>
                                        <td class="td2Allalign" width="200px">
                                            <asp:TextBox ID="txtPassword" runat="server" Width="200px" TextMode="Password" 
                                                meta:resourcekey="txtPasswordResource1" Font-Bold="True" 
                                                Font-Size="Medium" Height="22px" BorderStyle="None"></asp:TextBox>
                                        </td> 
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvtxtPassword" runat="server" ControlToValidate="txtPassword"
                                                EnableClientScript="False" Text="&lt;img src='Images/icon/Exclamation.gif' title='Password is required!' /&gt;"
                                                ValidationGroup="Save" meta:resourcekey="rfvtxtPasswordResource1"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td1Allalign"></td>
                                        <td class="td1Allalign">
                                            <asp:Button ID="btnLogin" runat="server"  Text="login" Height="30px" Width="103px" 
                                                onclick="btnLogin_Click" ValidationGroup="Save"  BorderStyle="None"
                                                meta:resourcekey="btnLoginResource1" Font-Bold="True" Font-Size="Medium"/>   
                                        </td>
                                        <td class="td1Allalign">
                                            <asp:TextBox ID="cvtxt" runat="server" Text="02120" Visible="False" Width="10px" meta:resourcekey="cvtxtResource1"></asp:TextBox>
                                                <asp:CustomValidator id="cvShowMsg" runat="server" Display="None" 
                                                    ValidationGroup="ShowMsg" OnServerValidate="ShowMsg_ServerValidate"
                                                    EnableClientScript="False" ControlToValidate="cvtxt">
                                                </asp:CustomValidator>
                                        </td>
                                    </tr>              
                                </table>
                            </td>
                        </tr>
                    </table> 
                </td>
            </tr>
        </table>   
    </center>
    </div>
</asp:Content>

