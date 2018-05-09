using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class VisitorsPro
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private string _DateType;
    public string DateType { get { return _DateType; } set { _DateType = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _VisCardID;
    public string VisCardID { get { return _VisCardID; } set { _VisCardID = value; } }
    
    private string _VisIdentityNo;
    public string VisIdentityNo { get { return _VisIdentityNo; } set { _VisIdentityNo = value; } }

    private string _VisNameEn;
    public string VisNameEn { get { return _VisNameEn; } set { _VisNameEn = value; } }

    private string _VisNameAr;
    public string VisNameAr { get { return _VisNameAr; } set { _VisNameAr = value; } }

    private string _VisMobileNo;
    public string VisMobileNo { get { return _VisMobileNo; } set { _VisMobileNo = value; } }

    private bool _VisRegion1;
    public bool VisRegion1 { get { return _VisRegion1; } set { _VisRegion1 = value; } }

    private bool _VisRegion2;
    public bool VisRegion2 { get { return _VisRegion2; } set { _VisRegion2 = value; } }

    private bool _VisRegion3;
    public bool VisRegion3 { get { return _VisRegion3; } set { _VisRegion3 = value; } }

    private bool _VisRegion4;
    public bool VisRegion4 { get { return _VisRegion4; } set { _VisRegion4 = value; } }

    private bool _VisRegion5;
    public bool VisRegion5 { get { return _VisRegion5; } set { _VisRegion5 = value; } }

    private bool _VisRegion6;
    public bool VisRegion6 { get { return _VisRegion6; } set { _VisRegion6 = value; } }

    private bool _VisRegion7;
    public bool VisRegion7 { get { return _VisRegion7; } set { _VisRegion7 = value; } }

    private bool _VisRegion8;
    public bool VisRegion8 { get { return _VisRegion8; } set { _VisRegion8 = value; } }

    private bool _VisRegion9;
    public bool VisRegion9 { get { return _VisRegion9; } set { _VisRegion9 = value; } }

    private string _CardStatus;
    public string CardStatus { get { return _CardStatus; } set { _CardStatus = value; } }

    private string _StartDate;
    public string StartDate { get { return _StartDate; } set { _StartDate = value; } }

    private string _ExpiryDate;
    public string ExpiryDate { get { return _ExpiryDate; } set { _ExpiryDate = value; } }

    private string _TmpID;
    public string TmpID { get { return _TmpID; } set { _TmpID = value; } }

    private string _Description;
    public string Description { get { return _Description; } set { _Description = value; } }

    private byte[] _VisImage;
    public byte[] VisImage { get { return _VisImage; } set { _VisImage = value; } }

    private string _VisImageContentType;
    public string VisImageContentType { get { return _VisImageContentType; } set { _VisImageContentType = value; } }

    private int _VisImageLength;
    public int VisImageLength { get { return _VisImageLength; } set { _VisImageLength = value; } }
    
    private bool _isPrinted;
    public bool isPrinted { get { return _isPrinted; } set { _isPrinted = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _CopiesCount;
    public string CopiesCount { get { return _CopiesCount; } set { _CopiesCount = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }

    private string _TransactionDate;
    public string TransactionDate { get { return _TransactionDate; } set { _TransactionDate = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}