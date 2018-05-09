//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using AjaxControlToolkit;

using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Visitors_ImportImagesVisitors : BasePage
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        //---Common Code ----------------------------------------------------------------- //
        FormSession.FillSession("Visitors",null);
        //---Common Code ----------------------------------------------------------------- //

        if (!IsPostBack)
        {
            MainMasterPage.ShowTitel(General.Msg("Import images from a folder", "الاستيراد من مجلد الصور"));

            if (!FormSession.getPerm(new string[] { "ImpVis" })) { Response.Redirect(@"~/Login.aspx"); }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void AjaxFileUpload1_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        string filePath = MapPath("~/App_Data/" + e.FileName);
        string fileContentType = e.ContentType;
        int fileSize = e.FileSize;
        string fileName = e.FileName;       
        string path = MapPath("~/App_Data/" + e.FileName);

        AjaxFileUpload1.SaveAs(path);
        //e.PostedUrl = string.Format("?preview=1&fileId={0}", e.FileId);

        FileInfo fileInfo = new FileInfo(filePath);
        string ID = Path.GetFileNameWithoutExtension(path);

        // The byte[] to save the data in
        byte[] data = new byte[fileInfo.Length];

        // Load a filestream and put its content into the byte[]
        using (FileStream fs = fileInfo.OpenRead())
        {
            fs.Read(data, 0, data.Length);
        }

        byte[] EncryptData = CryptoImage.EncryptBytes(data);
        int EncryptfileSize = EncryptData.Length;

        System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection();

        sqlConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        String SqlCmdText1 = "UPDATE VisitorsCard SET VisImage =@VisImage,VisImageContentType=@VisImageContentType,VisImageLength=@VisImageLength WHERE VisIdentityNo = @VisIdentityNo";
        System.Data.SqlClient.SqlCommand sqlCmdObj1 = new System.Data.SqlClient.SqlCommand(SqlCmdText1, sqlConnection);

        sqlCmdObj1.Parameters.Add("@VisIdentityNo", System.Data.SqlDbType.VarChar, 100).Value = ID;
        sqlCmdObj1.Parameters.Add("@VisImage", System.Data.SqlDbType.Binary, EncryptfileSize).Value = EncryptData;
        sqlCmdObj1.Parameters.Add("@VisImageContentType", System.Data.SqlDbType.VarChar, 255).Value = fileContentType;
        sqlCmdObj1.Parameters.Add("@VisImageLength", System.Data.SqlDbType.Int).Value = EncryptfileSize;

        sqlConnection.Open();
        sqlCmdObj1.ExecuteNonQuery();
        sqlConnection.Close();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file) {
    //    // User can save file to File System, database or in session state
    //    if(file.ContentType.Contains("jpg") || file.ContentType.Contains("gif") || file.ContentType.Contains("png") || file.ContentType.Contains("jpeg")) 
    //    {
    //        // Limit preview file for file equal or under 4MB only, otherwise when GetContents invoked
    //        // System.OutOfMemoryException will thrown if file is too big to be read.
    //        if(file.FileSize <= 1024 * 1024 * 4) 
    //        {
    //            Session["fileContentType_" + file.FileId] = file.ContentType;
    //            Session["fileContents_" + file.FileId] = file.GetContents();

    //            // Set PostedUrl to preview the uploaded file.
    //            file.PostedUrl = string.Format("?preview=1&fileId={0}", file.FileId);
    //        } else {
    //            file.PostedUrl = "fileTooBig.gif";
    //        }

    //        // Since we never call the SaveAs method(), we need to delete the temporary fileß
    //        //file.DeleteTemporaryData();
    //    }

    //     //In a real app, you would call SaveAs() to save the uploaded file somewhere
    //     //AjaxFileUpload1.SaveAs(MapPath("~/App_Data/" + file.FileName), true);

    //}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}