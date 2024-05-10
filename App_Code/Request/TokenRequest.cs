using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TokenRequest
/// </summary>
public class TokenRequest
{
    public TokenRequest()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private Int64 _categoryId;
    private Int64 _productId;
    private Int64 _providerId;
    private Int64 _partnerId;
    private Int64 _platformId;




    public Int64 CategoryId
    {
        get { return _categoryId; }
        set { _categoryId = value; }
    }

    public Int64 ProductId
    {
        get { return _productId; }
        set { _productId = value; }
    }

    public Int64 ProviderId
    {
        get { return _providerId; }
        set { _providerId = value; }
    }
    public Int64 PartnerId
    {
        get { return _partnerId; }
        set { _partnerId = value; }
    }
    public Int64 PlatformId
    {
        get { return _platformId; }
        set { _platformId = value; }
    }

    #region BENEFECIARY INFORMATION
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


    #region GUARDIAN INFORMATION
    private string _guardianBirthday;
    public string GuardianBirthday
    {
        get { return _guardianBirthday; }
        set { _guardianBirthday = value; }
    }

    private string _guardianRelationship;
    public string GuardianRelationship
    {
        get { return _guardianRelationship; }
        set { _guardianRelationship = value; }
    }
    private string _guardianName;
    public string GuardianName
    {
        get { return _guardianName; }
        set { _guardianName = value; }
    }
    #endregion

    private string _platformname;
    public string PlatformName
    {
        get { return _platformname; }
        set { _platformname = value; }
    }
    private string _token;
    public string Token
    {
        get { return _token; }
        set { _token = value; }
    }
    private string _referenceCode;
    public string ReferenceCode
    {
        get { return _referenceCode; }
        set { _referenceCode = value; }
    }

    private string _referenceNumber;
    public string ReferenceNumber
    {
        get { return _referenceNumber; }
        set { _referenceNumber = value; }
    }

    private string _productCode;
    public string ProductCode
    {
        get { return _productCode; }
        set { _productCode = value; }
    }
    private string _partnerCode;
    public string PartnerCode
    {
        get { return _partnerCode; }
        set { _partnerCode = value; }
    }

    private string _email;
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }
    private string _contactNumber;
    public string ContactNumber
    {
        get { return _contactNumber; }
        set { _contactNumber = value; }
    }
    private string _firstName;
    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    private string _middleName;
    public string MiddleName
    {
        get { return _middleName; }
        set { _middleName = value; }
    }

    private string _lastName;
    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    private string _productName;
    public string ProductName
    {
        get { return _productName; }
        set { _productName = value; }
    }

    private string _cocNumber;
    public string COCNumber
    {
        get { return _cocNumber; }
        set { _cocNumber = value; }
    }

    private string _categoryCode;
    public string CategoryCode
    {
        get { return _categoryCode; }
        set { _categoryCode = value; }
    }

    private string _productDescription;
    public string ProductDescription
    {
        get { return _productDescription; }
        set { _productDescription = value; }
    }

    public string _premium;
    public string Premium
    {
        get { return _premium; }
        set { _premium = value; }
    }
    public string _deadlineOfPayment;
    public string DeadlineOfPayment
    {
        get { return _deadlineOfPayment; }
        set { _deadlineOfPayment = value; }
    }

    public string _issueDateTime;
    public string IssueDateTime
    {
        get { return _issueDateTime; }
        set { _issueDateTime = value; }
    }

    public string _effectiveDateTime;
    public string EffectiveDateTime
    {
        get { return _effectiveDateTime; }
        set { _effectiveDateTime = value; }
    }

    public string _terminationDate;
    public string TerminationDate
    {
        get { return _terminationDate; }
        set { _terminationDate = value; }
    }
    private List<UploadCollection> _uploadCollection;
    public List<UploadCollection> UploadCollection
    {
        get { return _uploadCollection; }
        set { _uploadCollection = value; }
    }

    private string _ipAddress;
    public string IpAddress
    {
        get { return _ipAddress; }
        set { _ipAddress = value; }
    }
    #region ADC ERaffle
    private string _DOB;
    public string DOB
    {
        get { return _DOB; }
        set { _DOB = value; }
    }
    private string _address;
    public string Address
    {
        get { return _address; }
        set { _address = value; }
    }
    private string _city;
    public string City
    {
        get { return _city; }
        set { _city = value; }
    }
    private string _province;
    public string Province
    {
        get { return _province; }
        set { _province = value; }
    }
    private string _zipCode;
    public string ZipCode
    {
        get { return _zipCode; }
        set { _zipCode = value; }
    }
    private string _natureofWork;
    public string NatureofWork
    {
        get { return _natureofWork; }
        set { _natureofWork = value; }
    }
    private string _employer;
    public string Employer
    {
        get { return _employer; }
        set { _employer = value; }
    }
    private string _validID;
    public string ValidID
    {
        get { return _validID; }
        set { _validID = value; }
    }
    private string _validIDNum;
    public string ValidIDNum
    {
        get { return _validIDNum; }
        set { _validIDNum = value; }
    }
    private string _agentID;
    public string AgentID
    {
        get { return _agentID; }
        set { _agentID = value; }
    }
    private string _type;
    public string Type
    {
        get { return _type; }
        set { _type = value; }
    }
    private string _suffix;
    public string Suffix
    {
        get { return _suffix; }
        set { _suffix = value; }
    }

    public string ClientID
    {
        get { return _clientID; }
        set { _clientID = value; }
    }
    private string _clientID;
    public string Photo
    {
        get { return _photo; }
        set { _photo = value; }
    }

    private string _photo;

    #endregion

    #region ClientReferral
    private string _branchCode;
    public string BranchCode
    {
        get { return _branchCode; }
        set { _branchCode = value; }
    }

    private string _areaCode;
    public string AreaCode
    {
        get { return _areaCode; }
        set { _areaCode = value; }
    }

    private string _Region;
    public string Region
    {
        get { return _Region; }
        set { _Region = value; }
    }

    private string _branchName;
    public string BranchName
    {
        get { return _branchName; }
        set { _branchName = value; }
    }
    private string _groupName;
    public string GroupName
    {
        get { return _groupName; }
        set { _groupName = value; }
    }

    private string _groupContactPerson;
    public string GroupContactPerson
    {
        get { return _groupContactPerson; }
        set { _groupContactPerson = value; }
    }

    private string _interests;
    public string Interests
    {
        get { return _interests; }
        set { _interests = value; }
    }

    private string _appointments;
    public string Appointments
    {
        get { return _appointments; }
        set { _appointments = value; }
    }
    public string ClientType
    {
        get { return _clientType; }
        set { _clientType = value; }
    }

    private string _clientType;

    #endregion

    #region MicroBizProtek

    private string _q1;
    public string Q1
    {
        get { return _q1; }
        set { _q1 = value; }
    }

    private string _q2;
    public string Q2
    {
        get { return _q2; }
        set { _q2 = value; }
    }
    private string _q3;
    public string Q3
    {
        get { return _q3; }
        set { _q3 = value; }
    }
    private string _q4;
    public string Q4
    {
        get { return _q4; }
        set { _q4 = value; }
    }
    private string _q5;
    public string Q5
    {
        get { return _q5; }
        set { _q5 = value; }
    }

    private string _insuredAmount;
    public string InsuredAmount
    {
        get { return _insuredAmount; }
        set { _insuredAmount = value; }
    }
    private string _businessName;
    public string BusinessName
    {
        get { return _businessName; }
        set { _businessName = value; }
    }
    private string _businessType;
    public string BusinessType
    {
        get { return _businessType; }
        set { _businessType = value; }
    }
    private string _startOfBusiness;
    public string StartOfBusiness
    {
        get { return _startOfBusiness; }
        set { _startOfBusiness = value; }
    }
    private string _gender;
    public string Gender
    {
        get { return _gender; }
        set { _gender = value; }
    }
    private string _civilStat;
    public string CivilStat
    {
        get { return _civilStat; }
        set { _civilStat = value; }
    }

     private string _attachCategory;
    public string AttachmentCategory
    {
        get { return _attachCategory; }
        set { _attachCategory = value; }
    }

    private string _relationship;
    public string Relationship
    {
        get { return _relationship; }
        set { _relationship = value; }
    }

    private string _fullName;
    public string FullName
    {
        get { return _fullName; }
        set { _fullName = value; }
    }

    private string _appDependentID;
    public string AppDependentID
    {
        get { return _appDependentID; }
        set { _appDependentID = value; }
    }
    private string _appDepRelationship;
    public string AppDepRelationship
    {
        get { return _appDepRelationship; }
        set { _appDepRelationship = value; }
    }

    private string _groupMail;
    public string GroupMail
    {
        get { return _groupMail; }
        set { _groupMail = value; }
    }


    private string _token2;
    public string Token2
    {
        get { return _token2; }
        set { _token2 = value; }
    }

    private string _referenceCode2;
    public string ReferenceCode2
    {
        get { return _referenceCode2; }
        set { _referenceCode2 = value; }
    }
    private string _fullName2;
    public string FullName2
    {
        get { return _fullName2; }
        set { _fullName2 = value; }
    }

    private string _firstName2;
    public string FirstName2
    {
        get { return _firstName2; }
        set { _firstName2 = value; }
    }

    private string _middleName2;
    public string MiddleName2
    {
        get { return _middleName2; }
        set { _middleName2 = value; }
    }

    private string _lastName2;
    public string LastName2
    {
        get { return _lastName2; }
        set { _lastName2 = value; }
    }

    private string _DOB2;
    public string DOB2
    {
        get { return _DOB2; }
        set { _DOB2 = value; }
    }

    public string ClientID2
    {
        get { return _clientID2; }
        set { _clientID2 = value; }
    }
    private string _clientID2;

    private string _gender2;
    public string Gender2
    {
        get { return _gender2; }
        set { _gender2 = value; }
    }

    private string _suffix2;
    public string Suffix2
    {
        get { return _suffix2; }
        set { _suffix2 = value; }
    }

    private string _propOwner;
    public string PropOwner
    {
        get { return _propOwner; }
        set { _propOwner = value; }
    }

    private string _nationality;
    public string Nationality
    {
        get { return _nationality; }
        set { _nationality = value; }
    }


    private string _sourceOfFunds;
    public string SourceOfFunds
    {
        get { return _sourceOfFunds; }
        set { _sourceOfFunds = value; }
    }

    private string _servicingUnit;
    public string ServicingUnit
    {
        get { return _servicingUnit; }
        set { _servicingUnit = value; }
    }
    #endregion
}