using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class CaptureImageEn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtEmpID.Text = (Request.QueryString["ID"] != null) ? Request.QueryString["ID"] : "";
        hdnType.Value = (Request.QueryString["Type"] != null) ? Request.QueryString["Type"] : "";

        string url = Request.Url.ToString();
        StringBuilder str = new StringBuilder();

        string[] arrUrl = url.Split('/');
        for (int i = 0; i < (arrUrl.Length - 1); i++)
        {
            str.Append(arrUrl[i].ToString());
            str.Append("/");
            if (arrUrl[i].ToString() == "/") { str.Append("/"); }
        }

        hdnServer.Value = str.ToString();
    }
}