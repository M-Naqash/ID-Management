<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImageCtl.ascx.cs" Inherits="ImageCtl" %>

<link href="../Css/fileuploadCss.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript">
    function ImageCapture(pageName,txtID,Type,msg) {
        var ID = document.getElementById(txtID);
        if (ID.value == '') { alert(msg); return false; }
        else {
            window.open('../Images/Pages/' + pageName + '.aspx?ID=' + ID.value + '&Type=' + Type, 'null', 'width=700,height=500,fullscreen=no,titlebar=no,toolbar=no,statusbar=no,scrollbars=yes', 'movable=no')
        }
    }

    function btnUploadClick() {
        try {
            var file = document.getElementById('<%=fileUpload.ClientID%>');
            file.click();
            return;
        }
        catch (err) {
            //Handle errors here
        }
    }


    function fundispimg(imgname) {
        var file = document.getElementById('<%=fileUpload.ClientID%>');
        if (file.value != null) {
            var Hidden = document.getElementById('<%=btnHidden.ClientID%>');
            Hidden.click();
        }
    }

    function FilePathIsValid() {
         var reg = '/^(([a-zA-Z]:)|(\))(\{1}|((\{1})[^\]([^/:<>"|]*))+)$/g'
         if (!reg.test(document.getElementById('xfuFile').value)) {
            alert('Please enter valid file path');
            return false
         } else
           return true
        }

    </script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="btnHidden"  />
    </Triggers>

    <ContentTemplate>
        <table style="border-width:thin; border-style:outset">
            <tr>
                <td style="border-width:thin; border-style:outset" colspan ="2" align="center">
                    <asp:Label ID="lblTitel" runat="server" Text="Image"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="imgDiv" runat="server">
                        <asp:Image ID="imgPhoto" runat="server" ImageUrl="~/Images/icon/NoPersonalImage.gif" Height="110px" Width="120px"/>
                    </div>
                </td>
                <td valign="top" style="border-width:thin; border-style:outset">
                    <table>
                        <tr>
                            <td>
                                <div class="file_input_div">
                                    <input id="btnUpload" type="button" class="file_input_button" runat="server" /> <%--onclick="btnUploadClick()"--%>
                                    <input id="fileUpload" type="file"  class="file_input_hidden" onchange="fundispimg(this.id);" runat="server" /> <%--"javascript: document.getElementById('fileName').value = this.value"--%>
                                </div>
                            </td>
                        </tr>    
                        <tr>    
                            <td>
                                <asp:ImageButton ID="imbCapture" runat="server" ImageUrl="~/Images/icon/captureImage.png" Visible="false"/> 
                            </td>
                        </tr>    
                        <tr> 
                            <td>
                                <asp:CustomValidator id="cvImage" runat="server"
                                    Text="&lt;img src='../Images/icon/Exclamation.gif' title='Image is required!' /&gt;" 
                                    ValidationGroup="Save" OnServerValidate="ImageValidate_ServerValidate"
                                    EnableClientScript="False" ControlToValidate="txtCustomValidator">
                                </asp:CustomValidator>
                                <asp:TextBox ID="txtCustomValidator" runat="server" Text="02120" Visible="False" Width="10px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="border-width:thin; border-style:outset" colspan ="2" align="center">
                    <%--<asp:Panel ID="Panel1" runat="server" Width="1px" Height="1px">--%>
                        <%--<asp:FileUpload ID="fileUpload1" runat="server" onchange="fundispimg(this.id);" onclick="if (!FilePathIsValid()) return false;" />--%>
                    <%--</asp:Panel>--%>
                    <input type="button" id="btnHidden" runat="server" onserverclick="btnHidden_OnServerclick" class="button" visible="true" style="display: none" />
                    <asp:Label ID="lblSize" runat="server" Text="97 X 350 Pixels" Visible="false"></asp:Label>
                    <%--<input type="text" id="fileName" class="file_input_textbox" readonly="readonly">--%>
 
            
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>