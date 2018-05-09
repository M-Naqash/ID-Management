using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Data;

public class SendEmail
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    DataTable dt;
    SmtpClient SClient = new SmtpClient();
    System.Net.Mail.MailMessage msgMail = new System.Net.Mail.MailMessage();
    string Host     = "";
    string Port     = "";
    string Sender   = "";
    string EmpEmail = "";
    string Lang     = "";
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public SendEmail() { }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool GetEMailServer(out string pServer,out string pPort,out string pSender )
    {
        pServer = "";
        pPort   = "";
        pSender = "";

        try
        {
            bool isValid = true;
            dt = DBFun.FetchData("Select EmlServerID,EmlPortNo,EmlSenderEmail From EmailSetting ");
            if (!DBFun.IsNullOrEmpty(dt))
            {
                if (dt.Rows[0]["EmlServerID"] != DBNull.Value) { pServer = dt.Rows[0]["EmlServerID"].ToString(); } else { isValid = false; }
                if (dt.Rows[0]["EmlPortNo"] != DBNull.Value) { pPort = dt.Rows[0]["EmlPortNo"].ToString(); } else { isValid = false; }
                if (dt.Rows[0]["EmlSenderEmail"] != DBNull.Value) { pSender = dt.Rows[0]["EmlSenderEmail"].ToString(); } else { isValid = false; }
                return isValid;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e1) { return false; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool GetEmpEMail(string pEmpID,out string pEmpEmail , out string pLang)
    {
        pLang     = "En";
        pEmpEmail = "";

        try
        {
            bool isValid = true;
            dt = DBFun.FetchData("SELECT EmpEmailID,Language FROM EmployeeMaster WHERE EmpID = '" + pEmpID + "'");
            if (!DBFun.IsNullOrEmpty(dt)) 
            {
                if (dt.Rows[0]["Language"]   != DBNull.Value) { pLang     = dt.Rows[0]["Language"].ToString(); }
                if (dt.Rows[0]["EmpEmailID"] != DBNull.Value) { pEmpEmail = dt.Rows[0]["EmpEmailID"].ToString(); } else { isValid = false; }
                return isValid;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e1) { return false; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool GetUsrEMail(out string pLoginID, out string pLang)
    {
        pLang    = "En";
        pLoginID = "";

        try
        {
            bool isValid = true;
            dt = DBFun.FetchData("SELECT UsrEmailID,UsrLanguage FROM AppUsers WHERE UsrLoginID = '" + pLoginID + "'");
            if (!DBFun.IsNullOrEmpty(dt)) 
            {
                if (dt.Rows[0]["UsrLanguage"] != DBNull.Value) { pLang    = dt.Rows[0]["UsrLanguage"].ToString(); }
                if (dt.Rows[0]["UsrEmailID"]  != DBNull.Value) { pLoginID = dt.Rows[0]["UsrEmailID"].ToString(); } else { isValid = false; }
                return isValid;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e1) { return false; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetEmpName(string pEmpID, string pLang)
    {
        try
        {
            dt = DBFun.FetchData("Select EmpNameEn, EmpNameAr FROM EmployeeMaster WHERE EmpID = '" + pEmpID + "'");
            if (!DBFun.IsNullOrEmpty(dt))
            {
                if (pLang == "Ar") { return dt.Rows[0]["EmpNameAr"].ToString(); } else { return dt.Rows[0]["EmpNameEn"].ToString(); }
            }
            return string.Empty;
        }
        catch (Exception e1) { return string.Empty; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetCardInfo(string pCardID)
    {
        try
        {
            dt = DBFun.FetchData("SELECT ExpiryDate FROM CardMaster WHERE CardID = " + pCardID + "");
            if (!DBFun.IsNullOrEmpty(dt))
            {
                if (dt.Rows[0]["ExpiryDate"] != DBNull.Value)
                {
                    if      (HttpContext.Current.Session["DateFormat"].ToString() == "Gregorian") { return (Convert.ToDateTime(dt.Rows[0]["ExpiryDate"])).ToString("dd/MM/yyyy"); }
                    else if (HttpContext.Current.Session["DateFormat"].ToString() == "Hijri")     { return Convert.ToString(DateFun.GrnToHij(Convert.ToDateTime(dt.Rows[0]["ExpiryDate"]))); }
                }
            }
            return string.Empty;
        }
        catch (Exception e1) { return string.Empty; }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SendMailToEmp(string pEmpID, string pCrdID, string pEmailType, string pCardApproval)
    {
        try
        {
            bool isValidMail = GetEMailServer(out Host,out Port,out Sender);
            bool isValidEmp  = GetEmpEMail(pEmpID,out EmpEmail, out Lang);
            if (isValidMail && isValidEmp)
            {
                string Subject = EmpMsgSubject(Lang, "SubjCrdStatus");
                string EmailText = EmpMsgEmailText(pCrdID, Lang, pEmailType, "EmailTextCardApproval", pCardApproval);

                SClient.Host = Host;
                SClient.Port = Convert.ToInt32(Port);
                MailAddress FromEmailAdd = new MailAddress(Sender);
                string EmailBody = @"
                <html>
                <head>
                    <title>For Employee ID : " + pEmpID + @" </title>
                </head>
                <body >           
                    <div>
                        <table>
                            <tr>
                                <td style=""height: 59px; width: 600px; "">
                                    <span style=""font-size: 18pt; font-family:Arial"">
                                        <br /> "
                                        + EmailText
                                        + @"<br />                                        
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </div>
                </body>
                </html>";
                msgMail.Body = EmailBody;
                msgMail.Subject = Subject;
                msgMail.To.Add(EmpEmail);
                msgMail.From = FromEmailAdd;
                msgMail.IsBodyHtml = true;

                SClient.Send(msgMail);
            }
        } 
        catch (Exception e1) { }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//    public void SendMailToMGR(string pUser, string pErqID, string pDateType, string pLang,string pUrl)
//    {
//        try
//        {
//            string Host = GetEMailServer();
//            int Port = GetEMailPort();
//            string FromEmail = GetSenderEMailID();
//            string usrLang;
//            string ToEmail = GetUsrEMailID(pUser, out usrLang);
//            GetEmpReqDetails(pErqID, pDateType);
//            string date = ErqStartDate;
//            if (!string.IsNullOrEmpty(ErqEndDate)) { if (ErqStartDate != ErqEndDate) { date += " - " + ErqEndDate; } }
//            string EmpName = GetEmpName(EmpID, pLang);

//            string Subject = ShowMsg(usrLang, "Request ID : " + pErqID + " is Waiting", "طلب بالرقم " + pErqID + " بالإنتظار");
//            string EmailText = ShowMsg(usrLang,
//                                          " Dear Manager "
//                                        + "<br /> "
//                                        + "&nbsp;&nbsp;&nbsp; Request ID : " + pErqID
//                                        + " <br /> "
//                                        + "&nbsp;&nbsp;&nbsp; Employee ID : " + EmpID
//                                        + " <br /> "
//                                        + "&nbsp;&nbsp;&nbsp; Employee Name : " + GetEmpName(EmpID, "EN")
//                                        + " <br /> "
//                                        + "&nbsp;&nbsp;&nbsp; Request Type : " + GetReqName(RetID, "EN")
//                                        + " <br /> "
//                                        + "&nbsp;&nbsp;&nbsp; Request Date : " + date
//                                        + " <br /> "
//                                        + "&nbsp;&nbsp;&nbsp; Request Status : Waiting"
//                                        + " <br /> "
//                                        + " <a href='" + pUrl +"'>AMS WEB</a> "
                                        
//                                        ," عزيزي المدير "
//                                        + "<br /> "
//                                         + "&nbsp;&nbsp;&nbsp; رقم الطلب : " + pErqID
//                                        + " <br /> "
//                                        + "&nbsp;&nbsp;&nbsp; رقم الموظف : " + EmpID
//                                        + " <br /> "
//                                        + "&nbsp;&nbsp;&nbsp; Employee Name : " + GetEmpName(EmpID, "AR")
//                                        + " <br /> "
//                                        + "&nbsp;&nbsp;&nbsp; نوع الطلب : " + GetReqName(RetID, "AR")
//                                        + " <br /> "
//                                        + "&nbsp;&nbsp;&nbsp; تاريخ الطلب : " + date
//                                        + " <br /> "
//                                        + "&nbsp;&nbsp;&nbsp; حالة الطلب : إنتظار"
//                                        + " <br /> "
//                                        + " <a href='" + pUrl +"'>AMS WEB</a> "
//                                        );

//            if (!string.IsNullOrEmpty(Host) && !string.IsNullOrEmpty(FromEmail) && !string.IsNullOrEmpty(ToEmail))
//            {
//                SClient.Host = Host;
//                SClient.Port = Port;
//                MailAddress FromEmailAdd = new MailAddress(FromEmail);
//                string EmailBody = @"
//                <html>
//                <head>
//                    <title>For Manager : " + pUser + @" </title>
//                </head>
//                <body >           
//                    <div>
//                        <table>
//                            <tr>
//                                <td style=""height: 59px; width: 600px; "">
//                                    <span style=""font-size: 16pt; font-family:Arial"">                                        
//                                        <br /> "
//                                        + EmailText
//                                        + @" <br />                                        
//                                    </span>
//                                </td>
//                            </tr>
//                        </table>
//                    </div>
//                </body>
//                </html>";
//                msgMail.Body = EmailBody;
//                msgMail.Subject = Subject;
//                msgMail.To.Add(ToEmail);
//                msgMail.From = FromEmailAdd;
//                msgMail.IsBodyHtml = true;

//                SClient.Send(msgMail);
//            }
//        }
//        catch (Exception e1) { }
    //}
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string ShowMsg(string plang,string pEn, string pAr) { if (plang == "Ar") { return pAr; } else { return pEn; } }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string EmpMsgSubject(string plang, string pType)
    {
            if (pType == "Subject") { return ShowMsg(Lang, "Card finish ", "تنبيه إنتهاء بطاقة"); }
            if (pType == "SubjCrdStatus") { return ShowMsg(Lang, "Card Status Approval ", "حالة الموافقة للبطاقة"); }

        return "";
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string EmpMsgEmailText(string pCrdID ,string plang,string pEmailType, string pType, string pCardApproval) 
    { 
        if (pEmailType == "W") 
        { 
            if (pType == "EmailText") 
            {
                string ExpiryDate = GetCardInfo(pCrdID);
                return ShowMsg(Lang," Dear Employee "
                                    + "<br /> "
                                    + "&nbsp;&nbsp;&nbsp; The card ID : " + pCrdID + " is finish at date : " + ExpiryDate
                                    + " <br /> ",
                                      " عزيزي الموظف "
                                    + "<br /> "
                                    + "&nbsp;&nbsp;&nbsp; البطاقة رقم : " + pCrdID + " سوف تنتهي بتاريخ : " + ExpiryDate
                                    + " <br /> "); }

            if (pType == "EmailTextCardApproval")
            {
                return ShowMsg(Lang, " Dear Employee "
                                    + "<br /> "
                                    + "&nbsp;&nbsp;&nbsp; Status of the request for approval on the card is : " + GetCardStatusApproval("En", pCardApproval)
                                    + " <br /> ",
                                      " عزيزي الموظف "
                                    + "<br /> "
                                    + "&nbsp;&nbsp;&nbsp; حالة الطلب للموافقة على البطاقة هي : " + GetCardStatusApproval("Ar", pCardApproval)
                                    + " <br /> ");
            }
        }

        return "";
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string GetCardStatusApproval(string plang, string pCardStatus)
    {
        if (pCardStatus == "Approve") { return ShowMsg(plang, "Approved", "موافقة"); }
        if (pCardStatus == "Reject") { return ShowMsg(plang, "Rejected", "مرفوضة"); }

        return string.Empty;
    }

}