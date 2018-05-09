<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ConfigurationSideMenu.ascx.cs" Inherits="ConfigurationSideMenu" %>

    <script type="text/javascript" src="../JScript/sdmenu.js"></script>

<div style="float: left; width:150px" id="my_menu" class="sdmenu1" onclick="loadfun('my_menu');">
     <%--BlackList--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="litBlackList" runat="server" Text="<%$ Resources:Menu, litBlackList %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;" cellspacing="0">
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnCreateBlackList" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Configuration/BlackList.aspx?ac=i"
                        Text="<%$ Resources:Menu, btnCreate %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdateBlackList" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Configuration/BlackList.aspx?ac=u"
                        Text="<%$ Resources:Menu, btnUpdate %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/delete.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnDeleteBlackList" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Configuration/BlackList.aspx?ac=d"
                        Text="<%$ Resources:Menu, btnDelete %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/search.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnSearchBlackList" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Configuration/BlackListSearch.aspx"
                        Text="<%$ Resources:Menu, btnSearch %>"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>

    <%--Companies--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="litCompanies" runat="server" Text="<%$ Resources:Menu, litCompanies %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;"
            cellspacing="0">
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnCreateCompanies" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Configuration/Companies.aspx?ac=i"
                        Text="<%$ Resources:Menu, btnCreate %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdateCompanies" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Configuration/Companies.aspx?ac=u"
                        Text="<%$ Resources:Menu, btnUpdate %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/delete.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnDeleteCompanies" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Configuration/Companies.aspx?ac=d"
                        Text="<%$ Resources:Menu, btnDelete %>"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>

    <%--Nationality--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="litNationality" runat="server" Text="<%$ Resources:Menu, litNationality %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;" cellspacing="0">
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnCreateNationality" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Configuration/Nationality.aspx?ac=i"
                        Text="<%$ Resources:Menu, btnCreate %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdateNationality" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Configuration/Nationality.aspx?ac=u"
                        Text="<%$ Resources:Menu, btnUpdate %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/delete.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnDeleteNationality" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Configuration/Nationality.aspx?ac=d"
                        Text="<%$ Resources:Menu, btnDelete %>"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>

    <%--Sections External--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="litSectionsExternal" runat="server" Text="<%$ Resources:Menu, litSectionsExternal %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;"
            cellspacing="0">
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnCreateSections" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Configuration/SectionExternal.aspx?ac=i"
                        Text="<%$ Resources:Menu, btnCreate %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdateSections" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Configuration/SectionExternal.aspx?ac=u"
                        Text="<%$ Resources:Menu, btnUpdate %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/delete.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnDeleteSections" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Configuration/SectionExternal.aspx?ac=d"
                        Text="<%$ Resources:Menu, btnDelete %>"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>

    <%--Setting--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="litSetting" runat="server" Text="<%$ Resources:Menu, litSetting %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;" cellspacing="0">
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnEmailConfig" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Configuration/EmailConfig.aspx"
                        Text="<%$ Resources:Menu, btnEmailConfig %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src= '<%= ImagePro.CreateImg %>' style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnSettingCompany" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Configuration/SettingCompany.aspx"
                        Text="<%$ Resources:Menu, btnSettingCompany %>"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</div>
