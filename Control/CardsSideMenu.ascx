<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CardsSideMenu.ascx.cs" Inherits="CardsSideMenu" %>

<script type="text/javascript" src="../JScript/sdmenu.js"></script>

<div style="float: left; width:150px" id="my_menu" class="sdmenu1" onclick="loadfun('my_menu');">
    <%--Cards--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="litCards" runat="server" Text="<%$ Resources:Menu, litCards %>" 
                    meta:resourcekey="litCardsResource1"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;"
            cellspacing="0">
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnCreateCards" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/CardMaster.aspx?ac=i"
                        Text="<%$ Resources:Menu, btnCreate %>" 
                        meta:resourcekey="btnCreateCardsResource1"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdateCards" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/CardMaster.aspx?ac=u"
                        Text="<%$ Resources:Menu, btnUpdate %>" meta:resourcekey="btnUpdateCardsResource1"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/delete.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnApproveCards" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/ApproveCard.aspx"
                        Text="<%$ Resources:Menu, btnApprove %>" meta:resourcekey="btnApproveCardsResource1"></asp:LinkButton>
                </td>
            </tr>
            <%--<tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnReturnCards" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/ReturnCard.aspx"
                        Text="<%$ Resources:Menu, btnReturn %>"></asp:LinkButton>
                </td>
            </tr>--%>
            <%--<tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnRecipientCard" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/RecipientCard.aspx"
                        Text="<%$ Resources:Menu, btnRecipient %>"></asp:LinkButton>
                </td>
            </tr>--%>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnPrintCards" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/PrintCard.aspx?Type=PCard"
                        Text="<%$ Resources:Menu, btnPrint %>" meta:resourcekey="btnPrintCardsResource1"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnTemplatesCard" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/PrintCard.aspx?Type=TCard"
                        Text="<%$ Resources:Menu, btnTemplates %>" meta:resourcekey="btnTemplatesCardResource1"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnCardHistory" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/CardHistory.aspx"
                        Text="<%$ Resources:Menu, btnCardSearch %>" meta:resourcekey="btnCardHistoryResource1"></asp:LinkButton>
                </td>
            </tr>
            <%--<tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnRecipientCardHistory" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/RecipientCardHistory.aspx"
                        Text="<%$ Resources:Menu, btnRecipientCardHistory %>" meta:resourcekey="btnRecipientCardHistoryResource1"></asp:LinkButton>
                </td>
            </tr>--%>
            <%--<tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnViewCard" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/PrintCard.aspx?Type=ViCard"
                        Text="<%$ Resources:Menu, btnViewCard %>" meta:resourcekey="btnViewCardResource1"></asp:LinkButton>
                </td>
            </tr>--%>
        </table>
    </div>

    <%--Issue State--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="litIssueState" runat="server" Text="<%$ Resources:Menu, litIssueState %>"
                    meta:resourcekey="litIssueStateResource1"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;"
            cellspacing="0">
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdateIssue" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/IssueState.aspx?ac=uc"
                        Text="<%$ Resources:Menu, btnUpdate %>" meta:resourcekey="btnUpdateIssueResource1"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>

    <%--Stickers--%>
    <div>
        <span>
            <center>
                <asp:Literal ID="LitStickerCar" runat="server" Text="<%$ Resources:Menu, LitStickerCar %>" ></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;"
            cellspacing="0">
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnCreateSticker" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/Stickers.aspx"
                        Text="<%$ Resources:Menu, btnCreate %>" meta:resourcekey="btnCreateStickerResource1"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnPrintSticker" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/PrintCard.aspx?Type=PStck"
                        Text="<%$ Resources:Menu, btnPrint %>" meta:resourcekey="btnPrintStickerResource1"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnTemplatesSticker" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/PrintCard.aspx?Type=TStck"
                        Text="<%$ Resources:Menu, btnTemplates %>" meta:resourcekey="btnTemplatesStickerResource1"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>

    <%--Cards Missing--%>
    <%--<div>
        <span>
            <center>
                <asp:Literal ID="litCardsMissing" runat="server" Text="<%$ Resources:Menu, litCardsMissing %>"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;"
            cellspacing="0">
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnNewMissingCards" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/MissingCard.aspx?ac=i"
                        Text="<%$ Resources:Menu, btnCreate %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdMissingCards" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/MissingCard.aspx?ac=u"
                        Text="<%$ Resources:Menu, btnUpdate %>"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>--%>

    <%--Cards print--%>
    <%--<div>
        <span>
            <center>
                <asp:Literal ID="litVisitorsCards" runat="server" Text="<%$ Resources:Menu, litVisitorsCards %>"
                    meta:resourcekey="litVisitorsCardsResource1"></asp:Literal>
            </center>
        </span>
        <table width="100%" bgcolor="#c0c0c0" style="border-left-width: 1px; border-right-width: 1px;"
            cellspacing="0">
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnCreateVisCards" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/VisitorsCards.aspx?ac=i"
                        Text="<%$ Resources:Menu, btnCreate %>" ></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnUpdateVisCards" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/VisitorsCards.aspx?ac=u"
                        Text="<%$ Resources:Menu, btnUpdate %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnPrintVisCards" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/PrintCard.aspx?Type=PVCard"
                        Text="<%$ Resources:Menu, btnPrint %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/edit.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnTemplatesVisCard" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/PrintCard.aspx?Type=TVCard"
                        Text="<%$ Resources:Menu, btnTemplates %>"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#c0c0c0" class="Body_BG">
                    <img src="../Images/menuIcon/new.jpeg" style="border-width: 0px;" />
                </td>
                <td align="center" width="85%">
                    <asp:LinkButton ID="btnVisCardHistory" runat="server" Enabled="False" OnClick="btnSideMenu_Click" PostBackUrl="~/Cards/VisitorsCardHistory.aspx"
                        Text="<%$ Resources:Menu, btnSearch %>"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>--%>
    
</div>
