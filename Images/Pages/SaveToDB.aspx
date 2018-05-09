<%@ Page Language="c#" AutoEventWireup="false"%>
<%
	try
	{
		int iFileLength;
		HttpFileCollection files = HttpContext.Current.Request.Files;
		HttpPostedFile uploadfile = files["RemoteFile"];
        //HttpPostedFile myFile = filMyFile.PostedFile;
        String strImageName = uploadfile.FileName;

        string[] arrID = strImageName.Split('.');
        string ImageName = arrID[0];
        string Type      = arrID[1];

        //string script = ImageName;
        //ScriptManager.RegisterStartupScript(this, GetType(),"ServerControlScript", script, true);

        //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ImageName + "');", true);
        
		iFileLength = uploadfile.ContentLength;
		Byte[] inputBuffer = new Byte[iFileLength];
		System.IO.Stream inputStream;
		inputStream = uploadfile.InputStream;
		inputStream.Read(inputBuffer,0,iFileLength);

        System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection();

        sqlConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        String SqlCmdText = "DELETE FROM TempImage WHERE Type =@Type AND EmpID=@EmpID";
        System.Data.SqlClient.SqlCommand sqlCmdObj = new System.Data.SqlClient.SqlCommand(SqlCmdText, sqlConnection);

        sqlCmdObj.Parameters.Add("@EmpID", System.Data.SqlDbType.VarChar, 255).Value = ImageName;
        sqlCmdObj.Parameters.Add("@Type", System.Data.SqlDbType.VarChar, 255).Value  = Type;
        
        sqlConnection.Open();
        sqlCmdObj.ExecuteNonQuery();
        sqlConnection.Close();


       

        sqlConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        String SqlCmdText1 = "INSERT INTO TempImage (EmpID,photo,PhotoType,PhotoLength,Type) VALUES (@EmpID,@photo,@PhotoType,@PhotoLength,@Type)";
        System.Data.SqlClient.SqlCommand sqlCmdObj1 = new System.Data.SqlClient.SqlCommand(SqlCmdText1, sqlConnection);

        sqlCmdObj1.Parameters.Add("@EmpID", System.Data.SqlDbType.VarChar, 255).Value = ImageName;
        sqlCmdObj1.Parameters.Add("@photo", System.Data.SqlDbType.Binary, iFileLength).Value = inputBuffer;
        sqlCmdObj1.Parameters.Add("@PhotoType", System.Data.SqlDbType.VarChar, 255).Value = "image/pjpeg";
        sqlCmdObj1.Parameters.Add("@PhotoLength", System.Data.SqlDbType.Int).Value = iFileLength;
        sqlCmdObj1.Parameters.Add("@Type", System.Data.SqlDbType.VarChar, 255).Value  = Type;

        sqlConnection.Open();
        sqlCmdObj1.ExecuteNonQuery();
        sqlConnection.Close();
	}
	catch(System.Data.SqlClient.SqlException e)
	{
	}		
%>