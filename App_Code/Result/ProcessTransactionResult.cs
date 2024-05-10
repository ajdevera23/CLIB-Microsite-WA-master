using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for ProcessTransactionResult
/// </summary>
public class ProcessTransactionResult:BaseResult
{
    public ProcessTransactionResult()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Fields/Properties
    private InsuranceTransactionCollection[] _insuranceTransactionCollection;

    public InsuranceTransactionCollection[] InsuranceTransactionCollection
    {
        get { return _insuranceTransactionCollection; }
        set { _insuranceTransactionCollection = value; }
    }

    
    #endregion
}