<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="Footer" %>

<table width="1200px" border="0" cellspacing="0" cellpadding="0" class="Footer">
    <tr>
        <!--Footer-->
        <td align="center" valign="top">
            <table  border="0" cellspacing="0" cellpadding="0" class="Footer">
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkPrivacyPolicy" runat="server" Text="Privacy Policy" 
                            meta:resourcekey="lnkPrivacyPolicyResource1"></asp:LinkButton>
                        <%--<a href="#" target="_blank">Privacy Policy</a> --%>
                    </td>
                    <td>
                        &nbsp; &nbsp; | &nbsp; &nbsp; 
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkTerms" runat="server" Text="Terms of Service" 
                            meta:resourcekey="lnkTermsResource1"></asp:LinkButton>
                        <%--<a href="#" target="_blank">Terms of Service</a> --%>
                    </td>
                    <td>
                        &nbsp; &nbsp; | &nbsp; &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblProgName" runat="server" Text="CM Secure " 
                            meta:resourcekey="lblProgNameResource1"/>
                        &nbsp;
                    </td>
                    <td>
                        © 2009-
                    </td>
                    <td>
                        <asp:Label ID="lblcurrentYear" runat="server" 
                            meta:resourcekey="lblcurrentYearResource1"/>
                    </td>
                    <td>
                        &nbsp; &nbsp; | &nbsp; &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblComName" runat="server" Text="Almaalim Company" 
                            meta:resourcekey="lblComNameResource1"/> 
                    </td>
                </tr>
                <tr><td height="30px" colspan="9"></td></tr>
            </table>
        </td>
        <!--/Footer-->
    </tr>
</table> 