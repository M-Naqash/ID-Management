<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeFingerPrint.aspx.cs" Inherits="EmployeeFingerPrint" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register src="../Control/EmployeeSideMenu.ascx" tagname="EmployeeSideMenu" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <uc2:EmployeeSideMenu ID="EmployeeSideMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <script type="text/javascript" language="javascript">
        function Connect(ID) {
            try {
                var FBControl = document.applets('FBObject');
                FBControl.Start = ID;
            }
            catch (Error) { }
        }         
    </script>

    <div id="pageDiv" runat="Server">
        <table border="0" cellpadding="0" cellspacing="4" width="100%">
            <tr>
                <td  align="center">
                    <%--<span class="requiredStar">*</span>--%>
                    <asp:Label ID="lblEmpIDSearch" runat="server" Text="Employee ID :"  Visible="false"
                        meta:resourcekey="lblEmpIDSearchResource1"></asp:Label>
                    <asp:TextBox ID="txtEmpIDSearch" runat="server" AutoCompleteType="Disabled" Visible="false"
                        Width="320px" meta:resourcekey="txtEmpIDSearchResource1"></asp:TextBox>
                    &nbsp;
                    <asp:Button ID="btnSearchDetails" runat="server" OnClick="btnSearchDetails_Click" Visible="false"
                        CssClass="buttonBG" Text="Fetch Data" meta:resourcekey="btnSearchDetailsResource1" Width="80px" />
                    <asp:TextBox ID="cvtxtValid" runat="server" Text="02120" Visible="False" Width="10px"
                                meta:resourcekey="cvtxtValidResource1"></asp:TextBox>
                    <asp:CustomValidator ID="cvShowMsg" runat="server" Display="None" ValidationGroup="ShowMsg"
                            OnServerValidate="ShowMsg_ServerValidate" EnableClientScript="False" ControlToValidate="cvtxtValid"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="vsShowMsg" runat="server" CssClass="MsgSuccess" EnableClientScript="False"
                                ValidationGroup="vgShowMsg" meta:resourcekey="vsShowMsgResource1" />

                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hfdConnStr" runat="server" />
                    <asp:HiddenField ID="hfdLoginUser" runat="server" />
                    <asp:HiddenField ID="hfdLang" runat="server" />
                    <asp:HiddenField ID="hfdFile" runat="server" />
                                                                                   
                    <object id="FBObject" name="FBObject" classid="CLSID:CA00F7A7-3C80-4B81-AC06-B2D19D89B006">
                        <p>cannot load Finger Print activeX</p>
                    </object>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

