<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="ClientSign.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Signature Capture</title>
    
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
	<meta name="CODE_LANGUAGE" content="C# .NET 7.1"/>
	<meta name="vs_defaultClientScript" content="JavaScript"/>
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
	
    <script language="Javascript" type="text/jscript">
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        function getURL() {
            if (location.search != "") {
                var y = location.search.split("=");
                document.FORM1.SigText.value = y[1];
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        function OnClear() { document.FORM1.SigPlus1.ClearTablet(); } //Clears the signature, in case of error or mistake
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        function OnSign() { document.FORM1.SigPlus1.TabletState = 1; } //Turns tablet on 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        function OnSave() {
            if (document.FORM1.SigText.value == "") {
                alert("Please Enter EmpID Before Continuing...");
                return false;
            }

            if (document.FORM1.SigPlus1.NumberOfTabletPoints > 0) {
                document.FORM1.SigPlus1.TabletState = 0; //Turns tablet off
                document.FORM1.SigPlus1.AutoKeyStart();
                document.FORM1.SigPlus1.AutoKeyData = document.FORM1.SigText.value;
                //pass here the data you want to use to encrypt the signature
                //this demo simply encrypts to the name typed in by the user
                //you'll probably want to make sure your encryption data you use is
                //more useful...that you encrypt the signature to the data important
                //to your app, and what the client has agreed to
                document.FORM1.SigPlus1.AutoKeyFinish();
                document.FORM1.SigPlus1.EncryptionMode = 2;
                document.FORM1.SigPlus1.SigCompressionMode = 1;
                document.FORM1.hidden.value = document.FORM1.SigPlus1.SigString;
                //pass the signature ASCII hex string to the hidden field,
                //so it will be automatically passed when the page is submitted
                document.FORM1.submit();
            }
            else {
                alert("Please Sign Before Continuing...");
                return false;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    </script>
    <style type="text/css">
        #SigText
        {
            width: 221px;
        }
        #submit1
        {
            width: 85px;
        }
        #button1
        {
            width: 85px;
        }
        #SignBtn
        {
            width: 85px;
        }
    </style>
</head>
<body onload = "getURL();">
    <form id="FORM1" method="post" name="FORM1" action="ServerImage.aspx" >

			<p><asp:Label ID="Label1" runat="server" Text="Employee ID :" ></asp:Label>
			&nbsp;
			<input id="SigText" type="text" name="SigText" readonly="readonly"/></p>
			
			<table border="1" cellpadding="0">
			  <tr>
			     <td>
					<object id="SigPlus1" style="LEFT: 0px; WIDTH: 320px; TOP: 0px; HEIGHT: 180px" height="75"
							classid="clsid:69A40DA3-4D42-11D0-86B0-0000C025864A" name="SigPlus1">
							<param name="_Version" value="131095"/>
							<param name="_ExtentX" value="8467"/>
							<param name="_ExtentY" value="4763"/>
							<param name="_StockProps" value="9"/>
					</object>
				 </td>
			   </tr>
			</table>
			<p>
               <input id="SignBtn" onclick="OnSign()" type="button" value="Sign" name="SignBtn"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			   <input id="button1" onclick="OnClear()" type="button" value="Clear" name="ClearBtn"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			   <input id="submit1" onclick="OnSave()" type="button" value="Submit" name="Submit"/>&nbsp;&nbsp;&nbsp;&nbsp;
			   <input id="hidden" type="hidden" name="hidden"/>
			</p>
		</form>
</body>
</html>
