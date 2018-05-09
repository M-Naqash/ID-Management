using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class ImageCtl : System.Web.UI.UserControl
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DataTable dt;
    TextBox txt;
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _EmptyIDMsgEn;
    public  string EmptyIDMsgEn { get { return _EmptyIDMsgEn; } set { if (_EmptyIDMsgEn != value) { _EmptyIDMsgEn = value; } } }

    private string _EmptyIDMsgAr;
    public  string EmptyIDMsgAr { get { return _EmptyIDMsgAr; } set { if (_EmptyIDMsgAr != value) { _EmptyIDMsgAr = value; } } }

    private string _TitelEn = "Image";
    public  string TitelEn { get { return _TitelEn; } set { if (_TitelEn != value) { _TitelEn = value; } } }

    private string _TitelAr = "الصورة";
    public  string TitelAr { get { return _TitelAr; } set { if (_TitelAr != value) { _TitelAr = value; } } }

    protected string _Type;
    public string Type { get { return _Type; } set { _Type = value; } }  //if (_Type != value) { _Type = value; }

    private bool _CaptureEnable = true;
    public bool CaptureEnable { get { return _CaptureEnable; } set { if (_CaptureEnable != value) { _CaptureEnable = value; } } }

    private string _ValidationGroup ;
    public string ValidationGroup { get { return _ValidationGroup; } set { if (_ValidationGroup != value) { _ValidationGroup = value; } } }

    private bool _IsEncryption = false;
    public bool IsEncryption { get { return _IsEncryption; } set { if (_IsEncryption != value) { _IsEncryption = value; } } }

    private string _txtID;
    public  string txtID { get { return _txtID; } set { if (_txtID != value) { _txtID = value; } } }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Page_Load(object sender, EventArgs e)
    {
        txt = this.Parent.Parent.FindControl(txtID) as TextBox;
        lblTitel.Text = General.Msg(TitelEn, TitelAr);

        if (!string.IsNullOrEmpty(ValidationGroup)) { cvImage.ValidationGroup = ValidationGroup; } else { cvImage.Visible = false; }
        
        if (!IsPostBack)
        {
            if (Type == "Logo") { imgPhoto.Height = 79; imgPhoto.Width = 350; lblSize.Visible = true; } else { imgPhoto.Height = 110; imgPhoto.Width = 120; lblSize.Visible = false;}

            if (CaptureEnable)
            {
                imbCapture.Attributes.Add("onclick", "ImageCapture('" + General.Msg("CaptureImageEn", "CaptureImageAr") + "','" + txt.ClientID + "','" + Type + "','" +  General.Msg(EmptyIDMsgEn, EmptyIDMsgAr) + "')");
                imbCapture.Visible = true;
            }
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string FindID() 
    { 
        if (Type == "Visitors")   { return txt.Text; } 
        if (Type == "Employee")   { return txt.Text; } 
        
        if (Type == "Logo")       { return "Logo"; }
        
        return "";
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected string FindQuery() 
    {
        if (Type == "Visitors") { return "SELECT TOP 1 VisImage Image,VisImageContentType ImageType,VisImageLength ImageLen FROM VisitorsCard WHERE VisIdentityNo='" + FindID() + "' ORDER BY VisCardID"; }
        if (Type == "Employee") { return "SELECT image Image,ImageContentType ImageType,ImageLength ImageLen FROM EmployeeMaster WHERE EmpNationalID='" + FindID() + "'"; } 
        
        if (Type == "Logo")     { return "SELECT AppLogo Image,AppLogoImageType ImageType,AppLogoImageLength ImageLen FROM ApplicationSetup "; }
        
        return "";
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    protected string FindTempImagePath() 
    { 
        if (Type == "Visitors")   { return @"~/Images/Temp/Visitors/"; } 
        if (Type == "Employee")   { return @"~/Images/Temp/Employee/"; } 
        if (Type == "Logo")       { return @"~/Images/Temp/Setting/"; }
        
        return "";
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    
    protected string EmptyImage() 
    { 
        if (Type == "Visitors")   { return "~/Images/icon/NoPersonalImage.gif"; } 
        if (Type == "Employee")   { return "~/Images/icon/NoPersonalImage.gif"; } 

        if (Type == "Logo")       { return "~/Images/icon/InsertLogo.JPG"; }
        return "";
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ClearImage() 
    { 
        int returnValue = DBFun.ExecuteData("DELETE FROM TempImage WHERE Type = '" + Type + "' AND EmpID='" + FindID() + "'");
        imgPhoto.ImageUrl = EmptyImage(); 
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void GetImage( out byte[] pImage,out string pImageContentType,out int pImageLength)
    {
        pImage = new Byte[0];
        pImageContentType = "";
        pImageLength = 0;
            
        string fileName = FindID() + ".jpeg";
        string path = Server.MapPath(FindTempImagePath()) + fileName;
        bool available = System.IO.File.Exists(path);
        try
        {
            dt = DBFun.FetchData("SELECT * FROM TempImage WHERE Type = '" + Type + "' AND EmpID='" + FindID() + "'");
            if (DBFun.IsNullOrEmpty(dt))
            {
                if (available)
                {
                    try
                    {
                        int len = System.IO.Path.GetFullPath(path).Length;
                        byte[] imageData = new Byte[100000];
                        System.IO.FileStream newFile = new System.IO.FileStream(path, System.IO.FileMode.Open);
                        newFile.Read(imageData, 0, imageData.Length);
                        pImage = (byte[])imageData;
                        pImageContentType = "image/pjpeg";
                        pImageLength = imageData.Length;
                    }
                    catch (Exception e1) { System.IO.File.Delete(path); }
                }
                else
                {
                    DataTable imgdt = DBFun.FetchData(FindQuery());
                    if (!DBFun.IsNullOrEmpty(imgdt))
                    {
                        if (!IsEncryption) { pImage = (Byte[])imgdt.Rows[0]["Image"]; } else { pImage = CryptoImage.DecryptBytes((Byte[])imgdt.Rows[0]["Image"]);}
                        pImageContentType = imgdt.Rows[0]["ImageType"].ToString();
                        pImageLength      = Convert.ToInt32(imgdt.Rows[0]["ImageLen"]);
                    }
                }
            }
            else
            {
                pImage            = (Byte[])dt.Rows[0]["photo"];
                pImageContentType = dt.Rows[0]["PhotoType"].ToString();
                pImageLength      = Convert.ToInt32(dt.Rows[0]["PhotoLength"]);
            }

            int returnValue = DBFun.ExecuteData("DELETE FROM TempImage WHERE Type = '" + Type + "' AND EmpID='" + FindID() + "'");
        }
        catch (Exception e2)
        {
            int returnValue = DBFun.ExecuteData("DELETE FROM TempImage WHERE Type = '" + Type + "' AND EmpID='" + FindID() + "'");
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void setImage(string pID) {  imgPhoto.ImageUrl = "~/Images/Pages/ReadImage.aspx?Type=" + Type + "&ID=" + pID + ""; }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void PopulateImage(string ID)
    {
        try
        {
            dt = (DataTable)DBFun.FetchData("SELECT * FROM TempImage WHERE Type = '" + Type + "' AND EmpID='" + ID  + "'");
            if (DBFun.IsNullOrEmpty(dt)) 
            {
                if (imgPhoto.ImageUrl != EmptyImage()) 
                { 
                    string ImagePath = FindTempImagePath() + ID  + ".jpeg"; 
                    imgPhoto.ImageUrl = ImagePath;
                    if (!File.Exists(ImagePath)) { setImage(ID); }
                    return; 
                }
                else { imgPhoto.ImageUrl = EmptyImage(); return; }
            }

            imgPhoto.ImageUrl = "~/Images/Pages/ReadImage.aspx?Type=" + Type + "Tmp&ID=" + ID + "";
        }
        catch (Exception e1) { DBFun.InsertError("Image", "PopulateImage()"); }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void btnHidden_OnServerclick(object sender, EventArgs e)
    {
        if ( !string.IsNullOrEmpty(txt.Text))
        {
            int iFileLength = fileUpload.PostedFile.ContentLength;
            byte[] inputBuffer = new byte[iFileLength];
            Stream inputStream;
            inputStream = fileUpload.PostedFile.InputStream;
            inputStream.Read(inputBuffer, 0, iFileLength);
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection();
            sqlConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
            String SqlCmdText = "DELETE FROM TempImage WHERE Type = '" + this.Type + "' AND EmpID= @EmpID";
            System.Data.SqlClient.SqlCommand sqlCmdObj = new System.Data.SqlClient.SqlCommand(SqlCmdText, sqlConnection);
            sqlCmdObj.Parameters.Add("@EmpID", System.Data.SqlDbType.VarChar, 255).Value = FindID();
            sqlConnection.Open();
            sqlCmdObj.ExecuteNonQuery();
            sqlConnection.Close();

            sqlConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
            String SqlCmdText1 = "INSERT INTO TempImage (EmpID,photo,PhotoType,PhotoLength,Type) VALUES (@EmpID,@photo,@PhotoType,@PhotoLength, '" + Type + "' )";
            System.Data.SqlClient.SqlCommand sqlCmdObj1 = new System.Data.SqlClient.SqlCommand(SqlCmdText1, sqlConnection);

            sqlCmdObj1.Parameters.Add("@EmpID", System.Data.SqlDbType.VarChar, 255).Value = FindID();
            sqlCmdObj1.Parameters.Add("@photo", System.Data.SqlDbType.Binary, iFileLength).Value = inputBuffer;
            sqlCmdObj1.Parameters.Add("@PhotoType", System.Data.SqlDbType.VarChar, 255).Value = "image/pjpeg";
            sqlCmdObj1.Parameters.Add("@PhotoLength", System.Data.SqlDbType.Int).Value = iFileLength;


            sqlConnection.Open();
            sqlCmdObj1.ExecuteNonQuery();
            sqlConnection.Close();

            PopulateImage(FindID());
        }
        else
        {
            imgPhoto.ImageUrl = EmptyImage();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('" + General.Msg(EmptyIDMsgEn,EmptyIDMsgAr) + "');", true); 
            return;
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void EnabledImage(bool status) 
    {
        btnUpload.Disabled = fileUpload.Disabled = !status;
        imbCapture.Enabled = status; 
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*##############################################################################################################################*/
    /*##############################################################################################################################*/
    #region Custom Validate Events
     
    protected void ImageValidate_ServerValidate(Object source, ServerValidateEventArgs e)
    {
        try
        {
            if (source.Equals(cvImage))
            {
               
                if (String.IsNullOrEmpty(imgPhoto.ImageUrl) ||  imgPhoto.ImageUrl == EmptyImage() ) 
                { e.IsValid = false; } else { e.IsValid = true; }
            }
        }
        catch { e.IsValid = false; }
    }

    #endregion
    /*##############################################################################################################################*/
    /*##############################################################################################################################*/
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
