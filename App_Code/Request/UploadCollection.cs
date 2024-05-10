using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BeneficiaryCollection
/// </summary>
public class UploadCollection
{
    public UploadCollection()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Fields / Properties

    

    private string _firstName;
    public string fld_FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    private string _middleName;
    public string fld_MiddleName
    {
        get { return _middleName; }
        set { _middleName = value; }
    }

    private string _lastName;
    public string fld_LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    private string _mobileNumber1;
    public string fld_MobileNumber1
    {
        get { return _mobileNumber1; }
        set { _mobileNumber1 = value; }
    }

    private string _mobileNumber2;
    public string fld_MobileNumber2
    {
        get { return _mobileNumber2; }
        set { _mobileNumber2 = value; }
    }

    private string _referenceNumber;
    public string fld_ReferenceNumber
    {
        get { return _referenceNumber; }
        set { _referenceNumber = value; }
    }

    private string _referenceNumberProvider;
    public string fld_ReferenceNumberProvider
    {
        get { return _referenceNumberProvider; }
        set { _referenceNumberProvider = value; }
    }

    private string _partnerCode;
    public string fld_PartnerCode
    {
        get { return _partnerCode; }
        set { _partnerCode = value; }
    }

    private string _productCode;
    public string fld_ProductCode
    {
        get { return _productCode; }
        set { _productCode = value; }
    }

    private string _effectiveDate;
    public string fld_EffectiveDate
    {
        get { return _effectiveDate; }
        set { _effectiveDate = value; }
    }

   

    private string _terminationDate;
    public string fld_TerminationDate
    {
        get { return _terminationDate; }
        set { _terminationDate = value; }
    }

    private string _homeAddress;
    public string fld_HomeAddress
    {
        get { return _homeAddress; }
        set { _homeAddress = value; }
    }

    private string _birthDate;
    public string fld_BirthDate
    {
        get { return _birthDate; }
        set { _birthDate = value; }
    }
    private string _gender;
    public string fld_Gender
    {
        get { return _gender; }
        set { _gender = value; }
    }

    private string _emailAddress;
    public string fld_EmailAddress
    {
        get { return _emailAddress; }
        set { _emailAddress = value; }
    }

    private string _civilStatus;
    public string fld_CivilStatus
    {
        get { return _civilStatus; }
        set { _civilStatus = value; }
    }


    #endregion
}