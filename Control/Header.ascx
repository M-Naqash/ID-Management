<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="Header" %>


<table width="1200px" border="0" cellspacing="0" cellpadding="0" class="HeaderBG">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
    
    <tr><td height="5"></td></tr>
    <tr>
        <td align="left" valign="top">
            <table dir="ltr" border="0" class="tp_LogoHeader">
                <tr>
                     <td align="left" class="tdBarlogin" valign="top">
                        <table dir="ltr">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="lnkChangeLang" runat="server" 
                                        OnClick="lnkChangeLang_Click" Height ="16px" 
                                        ImageUrl="~/App_Themes/ThemeEn/images/english_icon.png" 
                                        meta:resourcekey="lnkChangeLangResource1"/>
                                </td>
                                <td>
                                    <asp:ImageButton ID="lnkChangePassword" runat="server" 
                                        OnClick="lnkChangePassword_Click" Height ="16px" 
                                        ImageUrl="~/App_Themes/ThemeEn/images/Control/ChangePass16.png" 
                                        meta:resourcekey="lnkChangePasswordResource1" />
                                </td>
                                <td valign="middle">
                                    <asp:ImageButton ID="lnkLogout" runat="server" OnClick="lnkLogout_Click" 
                                        Height ="16px" ImageUrl="~/App_Themes/ThemeEn/images/logout16.png" 
                                        meta:resourcekey="lnkLogoutResource1" />
                                </td>
                                <td valign="middle">    
                                    <asp:LinkButton ID="lnkLogout2" runat="server" OnClick="lnkLogout_Click" 
                                        Height ="16px" meta:resourcekey="lnkLogout2Resource1"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr>
        <td class="td2Allalign">
            <table border="0" cellspacing="0" cellpadding="0">
               <tr>
                  <td width="16%"></td>
                  <td id="MenuHeader" runat="server" height="25px" class="tdMenuHeader"></td>
               </tr>
            </table>
        </td>
    </tr> 
</table>
