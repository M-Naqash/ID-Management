<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true"
    CodeFile="PrintCard.aspx.cs" Inherits="PrintCard" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../Control/CardsSideMenu.ascx"    TagName="CardsSideMenu"    TagPrefix="uc2" %>
<%@ Register Src="../Control/VisitorsSideMenu.ascx" TagName="VisitorsSideMenu" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc2:CardsSideMenu    id="CardsSideMenu1"    runat="server" Visible ="False"/>
    <uc3:VisitorsSideMenu id="VisitorsSideMenu1" runat="server" Visible ="False"/>
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
    <div id="pageDiv" runat="Server">
        <table border="0" cellpadding="0" cellspacing="4" width="100%">
            <tr>
                <td align="center" valign="top">
                    <asp:HiddenField ID="hfdConnStr" runat="server" />
                    <asp:HiddenField ID="hfdLoginUser" runat="server" />
                    <asp:HiddenField ID="hfdLang" runat="server" />
                    <asp:HiddenField ID="hfdType" runat="server" />
                    <object id="CPObject" name="CPObject" classid="CLSID:A2CF2F89-5A02-456F-B34F-FEF77E554705" codebase= "../CabFile/CMWEB_SportCity_PrintSetup.cab#Version=1,0,0,0">
                        <p>cannot load Card Print activeX</p>
                    </object>
                    <%--<object id="CPObject" name="CPObject" classid="CLSID:CAF60891-0EF6-47D8-86C2-99C6DA2834CF" codebase= "../CabFile/CMWEB_Royal_PrintSetup.cab#Version=1,0,0,0">
                        <p>cannot load Card Print activeX</p>
                    </object>--%>
                    
                    
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
