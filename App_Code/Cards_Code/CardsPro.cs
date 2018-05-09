using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CardsPro
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _DateFormat;
    public string DateFormat { get { return _DateFormat; } set { _DateFormat = value; } }

    private string _DateType;
    public string DateType { get { return _DateType; } set { _DateType = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _CardID;
    public string CardID { get { return _CardID; } set { _CardID = value; } }

    private string _EmpID;
    public string EmpID { get { return _EmpID; } set { _EmpID = value; } }

    private int _IsID;
    public int IsID { get { return _IsID; } set { _IsID = value; } }

    private string _StartDate;
    public string StartDate { get { return _StartDate; } set { _StartDate = value; } }

    private string _ExpiryDate;
    public string ExpiryDate { get { return _ExpiryDate; } set { _ExpiryDate = value; } }

    private string _ApprovedBy;
    public string ApprovedBy { get { return _ApprovedBy; } set { _ApprovedBy = value; } }

    private string _ApprovedDate;
    public string ApprovedDate { get { return _ApprovedDate; } set { _ApprovedDate = value; } }
  
    private string _CancelBy;
    public string CancelBy { get { return _CancelBy; } set { _CancelBy = value; } }

    private string _CancelDate;
    public string CancelDate { get { return _CancelDate; } set { _CancelDate = value; } }

    private int _TmpID;
    public int TmpID { get { return _TmpID; } set { _TmpID = value; } }

    private string _Description;
    public string Description { get { return _Description; } set { _Description = value; } }

    private string _ApprovalStatus; 
    public string ApprovalStatus { get { return _ApprovalStatus; } set { _ApprovalStatus = value; } }

    private string _CardStatus;
    public string CardStatus { get { return _CardStatus; } set { _CardStatus = value; } }

    private string _InActiveStatus;
    public string InActiveStatus { get { return _InActiveStatus; } set { _InActiveStatus = value; } }

    private int _IsApproved;
    public int IsApproved { get { return _IsApproved; } set { _IsApproved = value; } }

    private bool _isPrinted;
    public bool isPrinted { get { return _isPrinted; } set { _isPrinted = value; } }

    private string _NationalID;
    public string NationalID { get { return _NationalID; } set { _NationalID = value; } }

    private int _CardCount;
    public int CardCount { get { return _CardCount; } set { _CardCount = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private string _CreatedBy;
    public string CreatedBy { get { return _CreatedBy; } set { _CreatedBy = value; } }

    private string _CreatedDate;
    public string CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }

    private string _PrintedBy;
    public string PrintedBy { get { return _PrintedBy; } set { _PrintedBy = value; } }

    private string _PrintedDate;
    public string PrintedDate { get { return _PrintedDate; } set { _PrintedDate = value; } }

    private string _TransactionBy;
    public string TransactionBy { get { return _TransactionBy; } set { _TransactionBy = value; } }

    private string _TransactionDate;
    public string TransactionDate { get { return _TransactionDate; } set { _TransactionDate = value; } }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}