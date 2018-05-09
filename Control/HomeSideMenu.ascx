<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeSideMenu.ascx.cs" Inherits="HomeSideMenu" %>

<script type="text/javascript" src="../JScript/sdmenu.js"></script>

<div style="float: left; width:150px" id="my_menu" class="sdmenu1" onclick="loadfun('my_menu');">
    <%--Home--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="litHome" runat="server" Text="<%$ Resources:Menu, litHome %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" class="Table_sdmenu1" cellspacing="0">
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnHome" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Home/Home.aspx" Text="<%$ Resources:Menu, btnHome %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnChangPassword" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Home/ChangePassword.aspx" Text="<%$ Resources:Menu, btnChangPassword %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnChangLang" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Home/ChangeDefaultLang.aspx" Text="<%$ Resources:Menu, btnChangLang %>"></asp:LinkButton>
                </td>
            </tr>
            <%--<tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdateOthers" runat="server" Enabled="False" OnClick="btnSideMenu_Click"
                        Text="<%$ Resources:Menu, btnUpdateOthers %>"></asp:LinkButton>
                </td>
            </tr>--%>

        </table>
    </div>
</div>
