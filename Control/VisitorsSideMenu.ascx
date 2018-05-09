<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VisitorsSideMenu.ascx.cs" Inherits="Control_VisitorsSideMenu" %>

<script type="text/javascript" src="../JScript/sdmenu.js"></script>

<div style="float: left; width:150px" id="my_menu" class="sdmenu1" onclick="loadfun('my_menu');">
    <%--Cards--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="litVisitors" runat="server" Text="<%$ Resources:Menu, litVisitors %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;" cellspacing="0">
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnNewVisitors" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Visitors/Visitors.aspx?ID=i"
                        Text="<%$ Resources:Menu, btnCreate %>">
                    </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdateVisitors" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Visitors/Visitors.aspx?ID=u"
                        Text="<%$ Resources:Menu, btnUpdate %>">
                    </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnImportVisitors" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Visitors/ImportVisitors.aspx"
                        Text="<%$ Resources:Menu, btnImport %>">
                    </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnImportImagesVisitors" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Visitors/ImportImagesVisitors.aspx"
                        Text="<%$ Resources:Menu, btnImportImages %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnPrintCards" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/PrintCard.aspx?Type=PVCrd"
                        Text="<%$ Resources:Menu, btnPrint %>" meta:resourcekey="btnPrintCardsResource1"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnTemplatesCard" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/PrintCard.aspx?Type=TVCrd"
                        Text="<%$ Resources:Menu, btnTemplates %>" meta:resourcekey="btnTemplatesCardResource1"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/search.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnHistoryVisitors" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Visitors/VisitorsSearch.aspx"
                        Text="<%$ Resources:Menu, btnSearch %>"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <%--Cards--%>
    <%--<div>
        <span>
            <center>
                <asp:Literal ID="litCards" runat="server" Text="<%$ Resources:Menu, litCards %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" class="Table_sdmenu1"
            cellspacing="0">
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnCreateCards" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/CardMaster.aspx?ac=i"
                        Text="<%$ Resources:Menu, btnCreate %>" meta:resourcekey="btnCreateCardsResource1"></asp:LinkButton>
                </td>
            </tr>
            
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/search.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnCardHistory" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/CardHistory.aspx"
                        Text="<%$ Resources:Menu, btnSearch %>" meta:resourcekey="btnCardHistoryResource1"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>--%>
</div>