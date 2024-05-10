using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CustomerDetails
/// </summary>
public class CustomerDetails
{
    public CustomerDetails()
    {
        //
        // TODO: Add constructor logic heres
        //
    }
    #region Fields and Properties

    private string _address;
    public string Address
    {
        get { return _address; }
        set { _address = value; }
    }

   
    private string _birthDate;
    public string Birthdate
    {
        get { return _birthDate; }
        set { _birthDate = value; }
    }

    private string _city;
    public string City
    {
        get { return _city; }
        set { _city = value; }
    }

    private string _civilStatus;
    public string CivilStatus
    {
        get { return _civilStatus; }
        set { _civilStatus = value; }
    }

    private string _emailAddress;
    public string EmailAddress
    {
        get { return _emailAddress; }
        set { _emailAddress = value; }
    }

    private string _firstName;
    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    private string _gender;
    public string Gender
    {
        get { return _gender; }
        set { _gender = value; }
    }

    private string _iClickCustomerNo;
    public string IClickCustomerNo
    {
        get { return _iClickCustomerNo; }
        set { _iClickCustomerNo = value; }
    }

    private string _insuranceCustomerNo;
    public string InsuranceCustomerNo
    {
        get { return _insuranceCustomerNo; }
        set { _insuranceCustomerNo = value; }
    }

    private string _insuredClass;
    public string InsuredClass
    {
        get { return _insuredClass; }
        set { _insuredClass = value; }
    }

    private string _landline;
    public string Landline
    {
        get { return _landline; }
        set { _landline = value; }
    }

    private string _lastName;
    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    private string _licenseNumber;
    public string LicenseNumber
    {
        get { return _licenseNumber; }
        set { _licenseNumber = value; }
    }

    private string _middleName;
    public string MiddleName
    {
        get { return _middleName; }
        set { _middleName = value; }
    }

    private string _mobileNumber;
    public string MobileNumber
    {
        get { return _mobileNumber; }
        set { _mobileNumber = value; }
    }


    private string _nationality;
    public string Nationality
    {
        get { return _nationality; }
        set { _nationality = value; }
    }

    private string _natureOfWork;
    public string NatureOfWork
    {
        get { return _natureOfWork; }
        set { _natureOfWork = value; }
    }

    private string _otherCustomerNo;
    public string OtherCustomerNo
    {
        get { return _otherCustomerNo; }
        set { _otherCustomerNo = value; }
    }

    private string _placeOfBirth;
    public string PlaceOfBirth
    {
        get { return _placeOfBirth; }
        set { _placeOfBirth = value; }
    }

    private string _signaturePath;
    public string SignaturePath
    {
        get { return _signaturePath; }
        set { _signaturePath = value; }
    }

    private string _sourceOfFunds;
    public string SourceOfFunds
    {
        get { return _sourceOfFunds; }
        set { _sourceOfFunds = value; }
    }

    private string _tinID;
    public string TinID
    {
        get { return _tinID; }
        set { _tinID = value; }
    }
    private string _validIDNumber;
    public string ValidIDNumber
    {
        get { return _validIDNumber; }
        set { _validIDNumber = value; }
    }
    private string _validIDPresented;
    public string ValidIDPresented
    {
        get { return _validIDPresented; }
        set { _validIDPresented = value; }
    }

    private string _zipCode;
    public string ZipCode
    {
        get { return _zipCode; }
        set { _zipCode = value; }
    }

    #endregion
}