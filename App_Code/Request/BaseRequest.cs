using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class BaseRequest
{
    public string Token { get; set; }
    public string Address { get; set; }
    public string BeneficiaryName { get; set; }
    public string Barangay { get; set; }
    public string BeneficiaryRelationship { get; set; }
    public string Birthdate { get; set; }
    public string CategoryCode { get; set; }
    public string City { get; set; }
    public string CivilStatus { get; set; }
    public string DateTimeFormat { get; set; }
    public string EmailAddress { get; set; }
    public string EmployerBusinessName { get; set; }
    public string FirstName { get; set; }
    public string Gender { get; set; }
    public string GuardianBirthday { get; set; }
    public string GuardianContactNo { get; set; }
    public string GuardianName { get; set; }
    public string GuardianRelationship { get; set; }
    public string IDNumber { get; set; }
    public long IntegrationId { get; set; }
    public bool IsExist { get; set; }
    public bool IsWithVoucher { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string MobileNumber { get; set; }
    public string Nationality { get; set; }
    public string NatureOfWork { get; set; }
    public long NumberOfCOCs { get; set; }
    public string PartnerCode { get; set; }
    public string Occupation { get; set; }
    public string PlaceOfBirth { get; set; }
    public string PlatformName { get; set; }
    public string PlatformKey { get; set; }
    public string ProductCode { get; set; }
    public string ProviderCode { get; set; }
    public string Province { get; set; }
    public string ReferenceNumber { get; set; }
    public string SourceOfFunds { get; set; }
    public string Suffix { get; set; }
    public string TinId { get; set; }
    public string UserId { get; set; }
    public string ValidID { get; set; }
    public string VoucherCode { get; set; }
    public string ZipCode { get; set; }
    public string PaymentChannel { get; set; }
    public string AgentCode {set; get;}
    public string ReferralCode {set; get;}
    public string Remarks { get; set; }
}


