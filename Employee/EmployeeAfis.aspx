<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeAfis.aspx.cs" Inherits="Employee_EmployeeAfis" %>

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
                <td>
                    <asp:HiddenField ID="hfdConnStr"   runat="server" />
                    <asp:HiddenField ID="hfdLoginUser" runat="server" />
                    <asp:HiddenField ID="hfdLang"      runat="server" />
                                                                                   
                    <object id="FBObject" name="FBObject" classid="CLSID:4C4BEECC-FF09-4A44-ACDB-01512C9F2C50">
                        <p>cannot load Afis activeX</p>
                    </object>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

