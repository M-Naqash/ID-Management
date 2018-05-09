<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UsersSideMenu.ascx.cs" Inherits="UsersSideMenu" %>

<script type="text/javascript" src="../JScript/sdmenu.js"></script>

<div style="float: left; width:150px" id="my_menu" class="sdmenu1" onclick="loadfun('my_menu');">
    <%--Users--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="litUsers" runat="server" Text="<%$ Resources:Menu, litUsers %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;" cellspacing="0">
            <tr >
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td  align="center" width="85%">
                    <asp:LinkButton ID="btnCreateUsers" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Users/ApplicationUsers.aspx?ac=i"
                        Text="<%$ Resources:Menu, btnCreate %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" alt="" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdateUsers" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Users/ApplicationUsers.aspx?ac=u"
                        Text="<%$ Resources:Menu, btnUpdate %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/delete.jpeg" style="border-width: 0px;" alt="" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnDeleteUsers" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Users/ApplicationUsers.aspx?ac=d"
                        Text="<%$ Resources:Menu, btnDelete %>"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <span>
            <center>
                <asp:Literal ID="litSearch" runat="server" Text="<%$ Resources:Menu, litSearch %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;" cellspacing="0">
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/search.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnSearchUsers" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Users/UserSearch.aspx"
                        Text="<%$ Resources:Menu, btnSearch %>"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <span>
           <center>
               <asp:Literal ID="litRole" runat="server" Text="<%$ Resources:Menu, litRole %>"></asp:Literal>
          </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;" cellspacing="0">
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnCreateRole" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Users/UserRole.aspx?ac=i"
                        Text="<%$ Resources:Menu, btnCreate %>" meta:resourcekey="btnCreateRoleResource1"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdateRole" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Users/UserRole.aspx?ac=u"
                        Text="<%$ Resources:Menu, btnUpdate %>" meta:resourcekey="btnUpdateRoleResource1"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/delete.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnDeleteRole" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Users/UserRole.aspx?ac=d"
                        Text="<%$ Resources:Menu, btnDelete %>" meta:resourcekey="btnDeleteRoleResource1"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</div>
