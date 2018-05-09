<%@ Page Title="Capture Image" Language="C#" AutoEventWireup="true" CodeFile="CaptureImageEn.aspx.cs" Inherits="CaptureImageEn" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="JavaScript" type="text/javascript">
        var Success
        function Initialisation() {
            // var doc = document.form1.

            selectbutton.disabled = false
            scanbutton.disabled = true
            uploadbutton.disabled = true
            rectbutton.disabled = true
            cropbutton.disabled = true
            leftbutton.disabled = true
            rightbutton.disabled = true
        }

        function SelectClick() {
            csxi.SelectTwainDevice();
            if (csxi.TwainConnected) { scanbutton.disabled = false }
        }

        function ScanClick() {
            csxi.Acquire();
            if (csxi.ImageHeight != 0) {
                uploadbutton.disabled = false
                rectbutton.disabled = false
                leftbutton.disabled = false
                rightbutton.disabled = false
            }
            else {
                uploadbutton.disabled = true
                rectbutton.disabled = true
                cropbutton.disabled = true
                leftbutton.disabled = true
                rightbutton.disabled = true
            }
        }

        function UploadClick() {
            //Add the URL to the file saving script, including the http:// prefix.
            //The third parameter is the name that would appear in the HTML input type=file tag. csNetUpload does not use it.
            if (document.form1.txtEmpID.value == '') {
                return;
            }
            else {
                var empID = document.form1.txtEmpID.value;
                var Type = document.form1.hdnType.value;

                var name = empID + '.' + Type + '.jpeg';
                var server = document.form1.hdnServer.value + 'SaveToDB.aspx';

                Success = csxi.PostImage(server, name, 'RemoteFile', 2);
                if (Success) {
                     //alert('Image Uploaded')
                }
                else {
                    alert('Upload Failed')
                }
                window.opener.document.forms(0).submit();
                self.close();
            }
        }

        function RectClick() {
            csxi.MouseSelectRectangle();
            cropbutton.disabled = false;
        }

        function CropClick() {
            csxi.CropToSelection();
            cropbutton.disabled = true;
        }

        function LeftClick() {
            csxi.Rotate(90);
        }

        function RightClick() {
            csxi.Rotate(270);
        }
    </script>
</head>
<body onload="Initialisation()">
    <form id="form1" runat="server">
        <table id="Table1" runat="server">
            <tr>
                <td colspan="4">
                    <asp:HiddenField ID="hdnServer" runat="server" />
                    <asp:HiddenField ID="hdnType" runat="server" />
                </td>
            </tr>
            
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblEmpID" runat="server" Text="Employee ID :"></asp:Label>                 
                    <asp:TextBox ID="txtEmpID" runat="server" Enabled="false"></asp:TextBox> 
                </td> 
            </tr>
        </table>        
    </form>

    <table>
        <tr>
            <td></td>
            <td >
                <table>
                    <tr>
                        <td><input type="button" name="selectbutton" value="Select Device" onclick="SelectClick()" style="width:100px"/></td>
                        <td><input type="button" name="scanbutton"   value="Capture Image" onclick="ScanClick()"   style="width:100px"/></td>
                        <td><input type="button" name="uploadbutton" value="Upload Image"  onclick="UploadClick()" style="width:100px"/></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table>
                    <tr><td><input type="button" name="rectbutton"  value="Select Area"  onclick="RectClick()"  style="width:100px"/></td></tr>
                    <tr><td><input type="button" name="cropbutton"  value="Crop"         onclick="CropClick()"  style="width:100px"/></td></tr>
                    <tr><td><input type="button" name="leftbutton"  value="Rotate Left"  onclick="LeftClick()"  style="width:100px"/></td></tr>
                    <tr><td><input type="button" name="rightbutton" value="Rotate Right" onclick="RightClick()" style="width:100px"/></td></tr>
                </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <object classid="clsid:5220cb21-c88d-11cf-b347-00aa00a28331">
                                <param name="LPKPath" value="../../CabFile/csximage.lpk"/>
                            </object>

                            <object id="csxi" classid="clsid:62E57FC5-1CCD-11D7-8344-00C1261173F0" codebase="../../CabFile/csXImage.cab" width="500" height="350"></object>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>  
                            
</body>
</html>
