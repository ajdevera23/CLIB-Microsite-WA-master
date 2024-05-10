using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TokenResult
/// </summary>
public class PurchaseResult
{
    public string Message { get; set; }
    public List<Result> Result { get; set; }
    public int ResultStatus { get; set; }
    public int TotalItems { get; set; }
    public int TotalPage { get; set; }
}

public class Result
{
    public string COCNumber { get; set; }

    public string EffectiveDate { get; set; }
    public string Premium { get; set; }
    public string TerminationDate { get; set; }
    public string Address { get; set; }
    public object BarangayDescription { get; set; }
    public int BarangayId { get; set; }
    public string Birthdate { get; set; }
    public object CardNumber { get; set; }
    public object City { get; set; }
    public string CivilStatus { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public object GCashInsuranceAccountId { get; set; }
    public string Gender { get; set; }
    public string IClickCustomerNo { get; set; }
    public string InsuranceCustomerNo { get; set; }
    public object InsuredClass { get; set; }
    public object Landline { get; set; }
    public string LastName { get; set; }
    public object LicenseNumber { get; set; }

    private string _middlename;
    public string MiddleName
    {
        get { return _middlename; }
        set { _middlename = value; }
    }

    public string MobileNumber { get; set; }
    public object Nationality { get; set; }
    public object NatureOfWork { get; set; }
    public object Occupation { get; set; }
    public object OtherCustomerNo { get; set; }
    public string PlaceOfBirth { get; set; }
    public object Province { get; set; }
    public object SignaturePath { get; set; }
    public string SourceOfFunds { get; set; }
    public object Suffix { get; set; }
    public object TinID { get; set; }
    public string ValidIDNumber { get; set; }
    public object ValidIDPresented { get; set; }
    public object ZipCode { get; set; }
}
