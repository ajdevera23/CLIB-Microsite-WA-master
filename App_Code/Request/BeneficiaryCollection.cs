using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BeneficiaryCollection
/// </summary>
public class BeneficiaryCollection
{
    public BeneficiaryCollection()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Fields / Properties

    //private List<string> _beneficiaryCollection;
    //public List<string> BeneficiaryDetails
    //{
    //    get { return _beneficiaryCollection; }
    //    set { _beneficiaryCollection = value; }
    //}

    //private string _beneficiaryBirthday;
    //public string BeneficiaryBirthday
    //{
    //    get { return _beneficiaryBirthday; }
    //    set { _beneficiaryBirthday = value; }
    //}

    //private string _beneficiaryContactNo;
    //public string BeneficiaryContactNo
    //{
    //    get { return _beneficiaryContactNo; }
    //    set { _beneficiaryContactNo = value; }
    //}

    private string _beneficiaryName;
    public string BeneficiaryName
    {
        get { return _beneficiaryName; }
        set { _beneficiaryName = value; }
    }

    private string _beneficiaryRelationship;
    public string BeneficiaryRelationship
    {
        get { return _beneficiaryRelationship; }
        set { _beneficiaryRelationship = value; }
    }
    #endregion
}