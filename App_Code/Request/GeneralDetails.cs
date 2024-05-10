using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GeneralDetails
/// </summary>
public class GeneralDetails
{
    public GeneralDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string _branchCode;
    public string BranchCode
    {
        get { return _branchCode; }
        set { _branchCode = value; }
    }



    private string _dateTimeFormat;
    public string DateTimeFormat
    {
        get { return _dateTimeFormat; }
        set { _dateTimeFormat = value; }
    }


    private string _forRenewal;
    public string ForRenewal
    {
        get { return _forRenewal; }
        set { _forRenewal = value; }
    }

    private Int64 _integrationId;
    public Int64 IntegrationId
    {
        get { return _integrationId; }
        set { _integrationId = value; }
    }
    private string _isPaid;
    public string IsPaid
    {
        get { return _isPaid; }
        set { _isPaid = value; }
    }

    private int _numberOfCOCs;
    public int NumberOfCOCs
    {
        get { return _numberOfCOCs; }
        set { _numberOfCOCs = value; }
    }

    private string _platformAPI;
    public string PlatformAPI
    {
        get { return _platformAPI; }
        set { _platformAPI = value; }
    }
    private string _referenceCode;
    public string ReferenceCode
    {
        get { return _referenceCode; }
        set { _referenceCode = value; }
    }
    private string _referenceNo;
    public string ReferenceNo
    {
        get { return _referenceNo; }
        set { _referenceNo = value; }
    }
    private string _sourceCOC;
    public string SourceCOC
    {
        get { return _sourceCOC; }
        set { _sourceCOC = value; }
    }
    private int _transactionSourceId;
    public int TransactionSourceId
    {
        get { return _transactionSourceId; }
        set { _transactionSourceId = value; }
    }

    private int _transactionTypeId;
    public int TransactionTypeId
    {
        get { return _transactionTypeId; }
        set { _transactionTypeId = value; }
    }
}