<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginHeader.ascx.cs" Inherits="LoginHeader" %>

<table width="1200px" border="0" cellspacing="0" cellpadding="0" class="HeaderBG">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
    
    <tr><td height="5"></td></tr>
    <tr>
        <td valign="top">
            <table dir="ltr" border="0" cellspacing="0" cellpadding="0" class="tp_LogoHeader">
                <tr>
                    <td align="left" class="tdBarlogin" valign="top">
                        <table dir="ltr" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="Skin">
                                    <asp:LinkButton CssClass="Skin" ID="lnkChangLang" runat="server" OnClick="lnkChangLang_Click" Height ="16px" Width="16px"></asp:LinkButton>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td></td></tr>           
    <tr><td></td></tr>
</table>