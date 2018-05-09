using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ServerImage : System.Web.UI.Page
{

    protected void Page_PreInit(object sender, EventArgs e)
    {
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //Put user code to initialize the page here
        SIGPLUSLib.SigPlus sigObj = new SIGPLUSLib.SigPlus();
        sigObj.InitSigPlus();
        sigObj.AutoKeyStart();

        //use the same data to decrypt signature
        sigObj.AutoKeyData = Request.Form["SigText"];

        sigObj.AutoKeyFinish();
        sigObj.SigCompressionMode = 1;
        sigObj.EncryptionMode = 2;

        //Now, get sigstring from client
        //Sigstring can be stored in a database if 
        //a biometric signature is desired rather than an image
        sigObj.SigString = Request.Form["hidden"];
        if(sigObj.NumberOfTabletPoints() > 0)
        {

            
            sigObj.ImageFileFormat = 0;
            sigObj.ImageXSize = 500;
            sigObj.ImageYSize = 150;
            sigObj.ImagePenWidth = 8;
            sigObj.SetAntiAliasParameters(1, 600, 700);
            sigObj.JustifyX = 5;
            sigObj.JustifyY = 5;
            sigObj.JustifyMode = 5;
            long size;
            byte[] byteValue;
            sigObj.BitMapBufferWrite();
            size = sigObj.BitMapBufferSize();
            byteValue = new byte[size];
            byteValue = (byte[])sigObj.GetBitmapBufferBytes();
            sigObj.BitMapBufferClose();

            //System.IO.MemoryStream ms  = new System.IO.MemoryStream(byteValue);

            //byte[] SignData = ms.ToArray();

            Session["signature"] = byteValue;

            //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

            //String path;

            //path = System.AppDomain.CurrentDomain.BaseDirectory & "mySig.bmp";

            //path = "C:\\mySig.bmp";

            //img.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
            
            //Response.Write("Image saved successfully to " + path);
        }
        else
        {
            //signature has not been returned successfully!
        }

        ClientScript.RegisterStartupScript(this.GetType(), "key", "javascript:OnUnload();", true);
    }
}
