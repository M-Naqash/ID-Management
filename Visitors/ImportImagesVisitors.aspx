<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.master" AutoEventWireup="true" CodeFile="ImportImagesVisitors.aspx.cs" Inherits="Visitors_ImportImagesVisitors" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../Control/VisitorsSideMenu.ascx" TagName="VisitorsSideMenu" TagPrefix="CSM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <CSM:VisitorsSideMenu ID="SideMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    
    <script type="text/javascript">

        function onClientUploadComplete(sender, e) {
            onImageValidated("TRUE", e);
        }

        function onImageValidated(arg, context) {

            var test = document.getElementById("testuploaded");
            test.style.display = 'block';

            var fileList = document.getElementById("fileList");
            var item = document.createElement('div');
            item.style.padding = '4px';

            if (arg == "TRUE") {
                var url = context.get_postedUrl();
                url = url.replace('&amp;', '&');
                item.appendChild(createThumbnail(context, url));
            } else {
                item.appendChild(createFileInfo(context));
            }

            fileList.appendChild(item);
        }

        function createFileInfo(e) {
            var holder = document.createElement('div');
            holder.appendChild(document.createTextNode(e.get_fileName() + ' with size ' + e.get_fileSize() + ' bytes'));

            return holder;
        }

        function createThumbnail(e, url) {
            var holder = document.createElement('div');
            var img = document.createElement("img");
            img.style.width = '80px';
            img.style.height = '80px';
            img.setAttribute("src", url);

            holder.appendChild(createFileInfo(e));
            holder.appendChild(img);

            return holder;
        }

        function onClientUploadStart(sender, e) {
            document.getElementById('uploadCompleteInfo').innerHTML = 'Please wait while uploading ' + e.get_filesInQueue() + ' files...';
        }

        function onClientUploadError(sender, e) {
            document.getElementById('uploadCompleteInfo').innerHTML = "There was an error while uploading.";
        }

        function onClientUploadCompleteAll(sender, e) {

            var args = JSON.parse(e.get_serverArguments()),
                unit = args.duration > 60 ? 'minutes' : 'seconds',
                duration = (args.duration / (args.duration > 60 ? 60 : 1)).toFixed(2);

            var info = 'At <b>' + args.time + '</b> server time <b>'
                + e.get_filesUploaded() + '</b> of <b>' + e.get_filesInQueue()
                + '</b> files were uploaded with status code <b>"' + e.get_reason()
                + '"</b> in <b>' + duration + ' ' + unit + '</b>';

            document.getElementById('uploadCompleteInfo').innerHTML = info;
        }
    </script>
    
    <div>
        <asp:Label runat="server" ID="myThrobber" Style="display: none;" 
            meta:resourcekey="myThrobberResource1"><img align="middle" alt="" src="../Images/icon/uploading.gif"/></asp:Label>
        <%--<ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="4" 
            Padding-Left="2" Padding-Right="1" Padding-Top="4" ThrobberID="myThrobber" OnClientUploadComplete="onClientUploadComplete"
            OnUploadComplete="AjaxFileUpload1_OnUploadComplete" MaximumNumberOfFiles="10"
            AllowedFileTypes="jpg,jpeg" 
            OnClientUploadCompleteAll="onClientUploadCompleteAll" 
            OnUploadCompleteAll="AjaxFileUpload1_UploadCompleteAll" 
            OnUploadStart="AjaxFileUpload1_UploadStart" 
            OnClientUploadStart="onClientUploadStart"
            OnClientUploadError="onClientUploadError"
            MaxFileSize="1024"/>--%>

        <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1"
            ThrobberID="myThrobber"
            ContextKeys="fred"
            AllowedFileTypes="jpg,jpeg"
            OnUploadComplete="AjaxFileUpload1_UploadComplete"
            
            runat="server" meta:resourcekey="AjaxFileUpload1Resource1"/>

            <%--OnClientUploadComplete ="onClientUploadComplete"--%>
    </div>
    <br />
    <div id="testuploaded" style="display: none; padding: 4px; border: gray 1px solid;">
        <h4>list of uploaded files:</h4>
        <hr />
        <div id="fileList">
        </div>
    </div>
</asp:Content>

