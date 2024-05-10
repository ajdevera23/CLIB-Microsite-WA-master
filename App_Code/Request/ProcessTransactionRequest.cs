using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProcessTransactionRequest
/// </summary>
public class ProcessTransactionRequest
{
    public ProcessTransactionRequest()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private List<BeneficiaryCollection> _beneficiaryCollection;
    public List<BeneficiaryCollection> BeneficiaryCollection
    {
        get { return _beneficiaryCollection; }
        set { _beneficiaryCollection = value; }
    }
    private CustomerDetails _customerDetails;
    public CustomerDetails CustomerDetails
    {
        get { return _customerDetails; }
        set { _customerDetails = value; }
    }
    private GuardianDetails _guardianDetails;
    public GuardianDetails GuardianDetails
    {
        get { return _guardianDetails; }
        set { _guardianDetails = value; }
    }
    private GeneralDetails _generalDetails;
    public GeneralDetails GeneralDetails
    {
        get { return _generalDetails; }
        set { _generalDetails = value; }
    }

    //private GuardianDetails _guardianDetails;
    //public GuardianDetails GuardianDetails
    //{
    //    get { return _guardianDetails; }
    //    set { _guardianDetails = value; }
    //}
    private string _token;
    public string Token
    {
        get { return _token; }
        set { _token = value; }
    }
}