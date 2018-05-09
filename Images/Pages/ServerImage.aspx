<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServerImage.aspx.cs" Inherits="ServerImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Server Image</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C# .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		
		<script type="text/javascript" language="javascript">
    function OnUnload()
    {
//    window.close();
    window.opener.document.forms(0).submit();
    
    
		
	self.close();
    
    }
    
    </script>
		
		<script runat="server">
      // ASP.NET page code goes here
		</script>
</head>
<body>
    <form id="Form1" method="post" runat="server" >
    
    <asp:Button ID="btnSave" runat="server"  Text="Close" OnClientClick="OnUnload()"  />
                                                                                    
                                                                                     
    </form>
</body>
</html>
