<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeSideMenu.ascx.cs"
    Inherits="Control_EmployeeSideMenu" %>
<script type="text/javascript" src="../JScript/sdmenu.js"></script>
<div style="float: left; width: 150px" id="my_menu" class="sdmenu1" onclick="loadfun('my_menu');">
    <%--Employee Manager--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="litEmpManager" runat="server" Text="<%$ Resources:Menu, litEmpManager %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;" cellspacing="0">
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt="" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnCreateEmpManager" runat="server" Enabled="False" OnClick="btnSideMenu_Click"
                        PostBackUrl="~/Employee/Employees.aspx?ac=IMng" Text="<%$ Resources:Menu, btnCreate %>">
                    </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdateEmpManager" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Employee/Employees.aspx?ac=UMng"
                        Text="<%$ Resources:Menu, btnUpdate %>">
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>

    <%--Employee Employee--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="LitEmpEmployee" runat="server" Text="<%$ Resources:Menu, LitEmpEmployee %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;" cellspacing="0">
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt="" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnCreateEmpEmployee" runat="server" Enabled="False" OnClick="btnSideMenu_Click"
                        PostBackUrl="~/Employee/Employees.aspx?ac=IEmp" Text="<%$ Resources:Menu, btnCreate %>">
                    </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdateEmpEmployee" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Employee/Employees.aspx?ac=UEmp"
                        Text="<%$ Resources:Menu, btnUpdate %>">
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>

    <%--Employee Contractor--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="LitEmpContractor" runat="server" Text="<%$ Resources:Menu, LitEmpContractor %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;" cellspacing="0">
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt="" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnCreateEmpContractor" runat="server" Enabled="False" OnClick="btnSideMenu_Click"
                        PostBackUrl="~/Employee/Employees.aspx?ac=ICon" Text="<%$ Resources:Menu, btnCreate %>">
                    </asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt=""/>
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdateEmpContractor" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Employee/Employees.aspx?ac=UCon"
                        Text="<%$ Resources:Menu, btnUpdate %>">
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>

    <%--EmployeeType--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="LitEmpType" runat="server" Text="<%$ Resources:Menu, LitEmpType %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;" cellspacing="0">
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt="" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdateEmpType" runat="server" Enabled="False" OnClick="btnSideMenu_Click"
                        PostBackUrl="~/Employee/EmployeeType.aspx" Text="<%$ Resources:Menu, btnUpdate %>"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>

    <%--EmployeeSearch--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="litEmployeeSearch" runat="server" Text="<%$ Resources:Menu, litSearch %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;" cellspacing="0">
            <tr>
                <td align="center" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" alt="" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnSearchEmployee" runat="server" Enabled="False" OnClick="btnSideMenu_Click"
                        PostBackUrl="~/Employee/EmployeesHistory.aspx" Text="<%$ Resources:Menu, btnSearch %>"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    
    <%--EmployeeFingerPrint--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="litEmpFP" runat="server" Text="<%$ Resources:Menu, litEmpFP %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;"
            cellspacing="0">
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/search.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnEmpFP" runat="server" Enabled="False" PostBackUrl="~/Employee/EmployeeFingerPrint.aspx"
                        OnClick="btnSideMenu_Click" Text="<%$ Resources:Menu, btnFingerPrints %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_sdmenu1">
                    <img src="../Images/menuIcon/search.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnAfis" runat="server" Enabled="False" PostBackUrl="~/Employee/EmployeeAfis.aspx"
                        OnClick="btnSideMenu_Click" Text="<%$ Resources:Menu, btnAfis %>"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</div>
