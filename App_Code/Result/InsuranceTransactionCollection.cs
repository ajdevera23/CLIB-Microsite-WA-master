using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CocCollection
/// </summary>
public class InsuranceTransactionCollection
{
    public InsuranceTransactionCollection()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Fields/Properties
    private string _cocNumber;
    public string CocNumber
    {
        get { return _cocNumber; }
        set { _cocNumber = value; }
    }

    private DateTime? _startDate;
    public DateTime? EffectiveDate
    {
        get { return _startDate; }
        set { _startDate = value; }
    }

    private DateTime? _endDate;
    public DateTime? TerminationDate
    {
        get { return _endDate; }
        set { _endDate = value; }
    }

    //private string _productName;
    //public string ProductName
    //{
    //    get { return _productName; }
    //    set { _productName = value; }
    //}

    #endregion
}