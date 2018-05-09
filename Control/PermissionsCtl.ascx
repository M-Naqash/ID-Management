<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PermissionsCtl.ascx.cs" Inherits="PermissionsCtl" %>

<table border="1" cellspacing="0" cellpadding="0" >
    <tr>
        <td colspan="2">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td align="center" class=".Logout">
                        <h4>
                            <bold>
                                <asp:Label ID="Label3" runat="server" Text="Permissions" meta:resourcekey="Label3Resource1" ></asp:Label>
                            <bold>
                        </h4>
                    </td>
                </tr>
                <div id="divRole" runat="server" visible="false">
                    <tr>
                        <td class="td1align" colspan="2" valign="bottom">
                            <asp:ImageButton ID="imbCheckAll" runat="server" onclick="CheckAll_Click"  ImageUrl="~/Images/icon/CheckAll.png" meta:resourcekey="imbCheckAllResource1" />
                            <asp:ImageButton ID="imbUnCheckAll" runat="server" onclick="CheckAll_Click" 
                                ImageUrl="~/Images/icon/UnCheckAll.png" 
                                meta:resourcekey="imbUnCheckAllResource1"/>
                            <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True" 
                                OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" Width="200px" 
                                Height="16px" meta:resourcekey="ddlRoleResource1">
                            </asp:DropDownList>
                        </td> 
                    </tr>
               </div>
            </table>
        </td>                                                  
    </tr>
    
    <tr>
        <td class="td1Allalign" style="width:250px">
            <asp:Label ID="lblUsr" runat="server" Text="Users :" 
                meta:resourcekey="lblUsrResource1"></asp:Label>
        </td>
        <td class="td2Allalign" style="width:650px">
            <asp:CheckBoxList ID="cblUsr" runat="server" RepeatDirection="Horizontal" 
                meta:resourcekey="cblUsrResource1">
                <asp:ListItem Text="Add"    Value="IUsr" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="UUsr" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Delete" Value="DUsr" meta:resourcekey="DeleteListItemResource"></asp:ListItem>
                <asp:ListItem Text="Search" Value="SUsr" meta:resourcekey="SearchListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblEmployeesManager" runat="server" Text="Aramco Employee :" meta:resourcekey="lblManagerResource1"></asp:Label>
        </td>
        <td class="cblFaculty">
            <asp:CheckBoxList ID="cblEmployeesManager" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Add"        Value="IMng" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update"     Value="UMng" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblEmployees" runat="server" Text="Third party Employee :" meta:resourcekey="lblEmployeesResource1"></asp:Label>
        </td>
        <td class="cblFaculty">
            <asp:CheckBoxList ID="cblEmployees" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Add"        Value="IEmp" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update"     Value="UEmp" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblEmployeesContractor" runat="server" Text="Contractor Company :" meta:resourcekey="lblContractorResource1"></asp:Label>
        </td>
        <td class="cblFaculty">
            <asp:CheckBoxList ID="cblEmployeesContractor" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Add"        Value="ICon" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update"     Value="UCon" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblEmployeesType" runat="server" Text="Employee Type :" meta:resourcekey="lblUpdateEmpTypeResource1"></asp:Label>
        </td>
        <td class="cblFaculty">
            <asp:CheckBoxList ID="cblEmployeesType" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Update"     Value="UEmpType" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblEmployeesHistory" runat="server" Text="Employee History :" meta:resourcekey="lblEmployeesHistoryResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblEmployeesHistory" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Search"   Value="SEmployees" meta:resourcekey="SearchListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblFP" runat="server" Text="Finger Print :" 
                meta:resourcekey="lblFPResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblEmployeesFP" runat="server" RepeatDirection="Horizontal" 
                meta:resourcekey="cblFPResource1">
                <asp:ListItem Text="View"    Value="VFPEmp" meta:resourcekey="ViewListItemResource"></asp:ListItem>
                <asp:ListItem Text="Enroll"  Value="FPEnroll" meta:resourcekey="EnrollListItemResource"></asp:ListItem>
                <asp:ListItem Text="Replace" Value="FPReplace" meta:resourcekey="ReplaceListItemResource"></asp:ListItem>
                <asp:ListItem Text="Verify"  Value="FPVerify"  meta:resourcekey="VerifyListItemResource"></asp:ListItem>
                <asp:ListItem Text="Delete,Clear All" Value="FPDelete" meta:resourcekey="DeleteClearListItemResource"></asp:ListItem>
                <asp:ListItem Text="Show Image" Value="FPShowImage" meta:resourcekey="ShowImageListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblCards" runat="server" Text="Cards :" meta:resourcekey="lblCardsResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblCards" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Add"          Value="ICrd" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update"       Value="UCrd" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Approve"      Value="ACrd" meta:resourcekey="ApproveListItemResource"></asp:ListItem>
                <asp:ListItem Text="Print"        Value="PCrd" meta:resourcekey="PrintListItemResource"></asp:ListItem>
                <asp:ListItem Text="Template"     Value="TCrd" meta:resourcekey="TemplateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Search"       Value="SCrd" meta:resourcekey="SearchListItemResource"></asp:ListItem>
                <asp:ListItem Text="Issue Update" Value="UIsCrd" meta:resourcekey="IssueUpdateListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblStickers" runat="server" Text="Stickers :" meta:resourcekey="lblStickersResource1"></asp:Label>
        </td>
        <td class="cblFaculty">
            <asp:CheckBoxList ID="cblCardsStickers" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Add"      Value="IStick" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Print"    Value="PStck"  meta:resourcekey="PrintListItemResource"></asp:ListItem>
                <asp:ListItem Text="Template" Value="TStck"  meta:resourcekey="TemplateListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblVisitors" runat="server" Text="Events :" meta:resourcekey="lblVisitorsResource1"></asp:Label>
        </td>
        <td class="cblFaculty">
            <asp:CheckBoxList ID="cblVisitors" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Add"      Value="IVis"    meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update"   Value="UVis"    meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Import"   Value="ImpVis"  meta:resourcekey="ImportListItemResource"></asp:ListItem>
                <asp:ListItem Text="Print"    Value="PCrdVis" meta:resourcekey="PrintListItemResource"></asp:ListItem>
                <asp:ListItem Text="Template" Value="TCrdVis" meta:resourcekey="TemplateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Search"   Value="SVis"    meta:resourcekey="SearchListItemResource"></asp:ListItem>

                <%--<asp:ListItem Text="Add Card"    Value="ICrdVis" meta:resourcekey="AddCardListItemResource"></asp:ListItem>--%>
                <%--<asp:ListItem Text="Search Card" Value="SCrdVis" meta:resourcekey="SearchCardListItemResource"></asp:ListItem>--%>
                <%--<asp:ListItem Text="Report"     Value="REmpCompanies" meta:resourcekey="ReportListItemResource"></asp:ListItem>--%>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblRep" runat="server" Text="Reports :" meta:resourcekey="lblRepResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblRep" runat="server" RepeatDirection="Horizontal" 
                meta:resourcekey="cblRepResource1">
                <asp:ListItem Text="Edit" Value="ReptEdit"              meta:resourcekey="liEditResource"></asp:ListItem>
                <asp:ListItem Text="Set To Default" Value="RepSetToDef" meta:resourcekey="liSetDefaultResource"></asp:ListItem>
                <%--<asp:ListItem Text="Upload\Export"  Value="ReptUpload"  meta:resourcekey="liUploadExportResource"></asp:ListItem>--%>
                <%-- <asp:ListItem Text="General Reports"       Value="Rep1" meta:resourcekey="liRep1Resource"></asp:ListItem>--%>
                <%--<asp:ListItem Text="Employees Reports"     Value="Rep2" meta:resourcekey="liRep2Resource"></asp:ListItem>--%>
                <asp:ListItem Text="Cards Reports"         Value="Rep4" meta:resourcekey="liRep4Resource"></asp:ListItem>
                <asp:ListItem Text="Stickers Reports"      Value="Rep5" meta:resourcekey="liRep5Resource"></asp:ListItem>
                <asp:ListItem Text="Visitors Reports"      Value="Rep6" meta:resourcekey="liRep6Resource"></asp:ListItem>
                <asp:ListItem Text="Administrator Reports" Value="Rep7" meta:resourcekey="liRep7Resource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblBlackListConfig" runat="server" Text="Black List :" meta:resourcekey="lblBlackListResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblBlackListConfig" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Add"    Value="IBla" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="UBla" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Delete" Value="DBla" meta:resourcekey="DeleteListItemResource"></asp:ListItem>
                <asp:ListItem Text="Search" Value="SBla" meta:resourcekey="SearchListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblCompaniesConfig" runat="server" Text="Companies :" meta:resourcekey="lblCompaniesResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblCompaniesConfig" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Add"    Value="ICompanies" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="UCompanies" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Delete" Value="DCompanies" meta:resourcekey="DeleteListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblSectionsConfig" runat="server" Text="Sections External :" meta:resourcekey="lblSectionsResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblSectionsConfig" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Add"    Value="ISections" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="USections" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Delete" Value="DSections" meta:resourcekey="DeleteListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblNatConfig" runat="server" Text="Nationality :" meta:resourcekey="lblNatResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblNatConfig" runat="server" RepeatDirection="Horizontal" 
                meta:resourcekey="cblNatResource1">
                <asp:ListItem Text="Add"    Value="INat" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="UNat" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Delete" Value="DNat" meta:resourcekey="DeleteListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    
    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblEmailConfig" runat="server" Text="E-mail settings :" meta:resourcekey="lblEmailConfigResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblEmailConfig" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Update" Value="UEml" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblConfig" runat="server" Text="institution Setting :" meta:resourcekey="lblConfigResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblConfig" runat="server" RepeatDirection="Horizontal" meta:resourcekey="cblConfigResource1">
                <asp:ListItem Text="Update" Value="UConfig" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <%--<tr>
        <td class="td1Allalign">
            <asp:Label ID="lblCardStudents" runat="server" Text="Student Card :" meta:resourcekey="lblCardStudentsResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblCardStudents" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Print"    Value="PCrdStudents" meta:resourcekey="PrintListItemResource"></asp:ListItem>
                <asp:ListItem Text="Template" Value="TCrdStudents" meta:resourcekey="TemplateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Search"   Value="SCrdStudents" meta:resourcekey="SearchListItemResource"></asp:ListItem>
                <asp:ListItem Text="Report"   Value="RCrdStudents" meta:resourcekey="ReportListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>--%>

   <%--  <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblEmpOfficer" runat="server" Text="Administrative Officer :" meta:resourcekey="lblEmpOfficerResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblEmpOfficer" runat="server" RepeatDirection="Horizontal" meta:resourcekey="cblEmpOfficerResource1">
                <asp:ListItem Text="Add"    Value="IEmpOfficer" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="UEmpOfficer" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Search" Value="SEmpOfficer" meta:resourcekey="SearchListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblBrcOfficer" runat="server" Text="Branches Administrative Officer :" meta:resourcekey="lblBrcOfficerResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblBrcOfficer" runat="server" RepeatDirection="Horizontal" meta:resourcekey="cblBrcOfficerResource1">
                <asp:ListItem Text="Add"    Value="IBrcOfficer" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="UBrcOfficer" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Delete" Value="DBrcOfficer" meta:resourcekey="DeleteListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblClgOfficer" runat="server" Text="Colleges Administrative Officer :" meta:resourcekey="lblClgOfficerResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblClgOfficer" runat="server" RepeatDirection="Horizontal" meta:resourcekey="cblClgOfficerResource1">
                <asp:ListItem Text="Add"    Value="IClgOfficer" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="UClgOfficer" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Delete" Value="DClgOfficer" meta:resourcekey="DeleteListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblMngOfficer" runat="server" Text="Managements Administrative Officer :" meta:resourcekey="lblMngOfficerResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblMngOfficer" runat="server" RepeatDirection="Horizontal" meta:resourcekey="cblMngOfficerResource1">
                <asp:ListItem Text="Add"    Value="IMngOfficer" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="UMngOfficer" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Delete" Value="DMngOfficer" meta:resourcekey="DeleteListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblDepOfficer" runat="server" Text="Departments Administrative Officer :" meta:resourcekey="lblDepOfficerResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblDepOfficer" runat="server" RepeatDirection="Horizontal" meta:resourcekey="cblDepOfficerResource1">
                <asp:ListItem Text="Add"    Value="IDepOfficer" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="UDepOfficer" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Delete" Value="DDepOfficer" meta:resourcekey="DeleteListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblDivOfficer" runat="server" Text="Units Administrative Officer :" meta:resourcekey="lblDivOfficerResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblDivOfficer" runat="server" RepeatDirection="Horizontal" meta:resourcekey="cblDivOfficerResource1">
                <asp:ListItem Text="Add"    Value="IDivOfficer" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="UDivOfficer" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Delete" Value="DDivOfficer" meta:resourcekey="DeleteListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblRnkJobOfficer" runat="server" Text="Ranks Job :" meta:resourcekey="lblRnkJobOfficerResource1"></asp:Label>
        </td>
        <td class="cblFaculty">
            <asp:CheckBoxList ID="cblRnkJobOfficer" runat="server" RepeatDirection="Horizontal" meta:resourcekey="cblRnkJobOfficerResource1">
                <asp:ListItem Text="Add"    Value="IRnkJob" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="URnkJob" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Delete" Value="DRnkJob" meta:resourcekey="DeleteListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblJobTitlesOfficer" runat="server" Text="Job Titles :" meta:resourcekey="lblJobTitlesOfficerResource1"></asp:Label>
        </td>
        <td class="cblFaculty">
            <asp:CheckBoxList ID="cblJobTitlesOfficer" runat="server" RepeatDirection="Horizontal" meta:resourcekey="cblJobTitlesOfficerResource1">
                <asp:ListItem Text="Add"    Value="IJobTitles" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="UJobTitles" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Delete" Value="DJobTitles" meta:resourcekey="DeleteListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblCrd" runat="server" Text="Cards :" meta:resourcekey="lblCrdResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <table border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td valign="middle">
                        <asp:CheckBoxList ID="cblIDCrd" runat="server" RepeatDirection="Horizontal" meta:resourcekey="cblCrdResource1">
                            <asp:ListItem Text="Add"          Value="ICrd"    meta:resourcekey="AddListItemResource"></asp:ListItem>
                            <asp:ListItem Text="Update"       Value="UCrd"    meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                            <asp:ListItem Text="Return"       Value="IRetCrd" meta:resourcekey="ReturnListItemResource"></asp:ListItem>
                            <asp:ListItem Text="Cancelled"    Value="ICanCrd" meta:resourcekey="CancelledListItemResource"></asp:ListItem>
                            <asp:ListItem Text="Print"        Value="PCrd"    meta:resourcekey="PrintListItemResource"></asp:ListItem>
                            <asp:ListItem Text="Template"     Value="TCrd"    meta:resourcekey="TemplateListItemResource"></asp:ListItem>
                            <asp:ListItem Text="Search"       Value="SCrd"    meta:resourcekey="SearchListItemResource"></asp:ListItem>
                            <asp:ListItem Text="Issue Update" Value="UIssCrd" meta:resourcekey="IssueUpdateListItemResource"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>  
        </td>
    </tr>
    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblMCrd" runat="server" Text="Missing Cards :" 
                meta:resourcekey="lblMCrdResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblMIDCrd" runat="server" RepeatDirection="Horizontal" meta:resourcekey="cblMCrdResource1">
                <asp:ListItem Text="Add"    Value="IMCrd" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="UMCrd" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>--%>
    
    
    <%--<tr>
        <td class="td1Allalign">
            <asp:Label ID="lblRep" runat="server" Text="Reports :" 
                meta:resourcekey="lblRepResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblRep" runat="server" RepeatDirection="Horizontal" 
                meta:resourcekey="cblRepResource1">
                <asp:ListItem Text="View" Value="ReportView" 
                    meta:resourcekey="ViewListItemResource"></asp:ListItem>
                <asp:ListItem Text="Edit" Value="ReportEdit" 
                    meta:resourcekey="EditListItemResource"></asp:ListItem>
                <asp:ListItem Text="Set To Default" Value="ReportSetToDef" 
                    meta:resourcekey="SetDefaultListItemResource"></asp:ListItem>
                <asp:ListItem Text="Upload\Export"  Value="ReportUpload" 
                    meta:resourcekey="UploadExportListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>--%>
    
   <%-- <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblNatConfig" runat="server" Text="Nationality :" meta:resourcekey="lblNatResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblNatConfig" runat="server" RepeatDirection="Horizontal" 
                meta:resourcekey="cblNatResource1">
                <asp:ListItem Text="Add"    Value="INat" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="UNat" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Delete" Value="DNat" meta:resourcekey="DeleteListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td class="td1Allalign">
            <asp:Label ID="lblPlcIdentConfig" runat="server" Text="Place Issuance Identity :" 
                meta:resourcekey="lblPlcIdentConfigResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblPlcIdentConfig" runat="server" RepeatDirection="Horizontal" meta:resourcekey="cblPlcIdentConfigResource1">
                <asp:ListItem Text="Add"    Value="IPlcIdent" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="UPlcIdent" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
                <asp:ListItem Text="Delete" Value="DPlcIdent" meta:resourcekey="DeleteListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

   
    <tr>
        <td class="td1Allalign">  
            <asp:Label ID="lblValidCardConfig" runat="server" Text="Card Validity Period  :" meta:resourcekey="lblValidCardConfigResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblValidCardConfig" runat="server" RepeatDirection="Horizontal" meta:resourcekey="cblValidCardConfigResource1">
                <asp:ListItem Text="Update" Value="UValidCard" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>--%>
    <%--<tr>
        <td class="td1Allalign">  
            <asp:Label ID="lblEmailConfig" runat="server" Text="Email Setting  :" meta:resourcekey="lblEmailConfigResource1"></asp:Label>
        </td>
        <td class="td2Allalign">
            <asp:CheckBoxList ID="cblEmailConfig" runat="server" RepeatDirection="Horizontal" meta:resourcekey="cblEmailConfigResource1">
                <asp:ListItem Text="Add"    Value="IEmailConfig" meta:resourcekey="AddListItemResource"></asp:ListItem>
                <asp:ListItem Text="Update" Value="UEmailConfig" meta:resourcekey="UpdateListItemResource"></asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>--%>  
</table>
