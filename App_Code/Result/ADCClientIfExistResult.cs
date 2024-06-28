using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ADCClientIfExist
{
    public string ADCCity { get; set; }
    public string ADCClientId { get; set; }
    public DateTime ADCDateOfBirth { get; set; }
    public string ADCEmailAddress { get; set; }
    public string ADCEmployer { get; set; }
    public string ADCFirstName { get; set; }
    public string ADCLastName { get; set; }
    public string ADCMiddleName { get; set; }
    public string ADCMobileNumber { get; set; }
    public string ADCNatureOfWork { get; set; }
    public string ADCProvince { get; set; }
    public string ADCSuffix { get; set; }
    public string ADCValidIDNumber { get; set; }
    public string ADCValidIDPresented { get; set; }
    public string ADCZipCode { get; set; }
    public string Address { get; set; }
    public string Appointment { get; set; }
    public string BirthDate { get; set; }
    public string ClientType { get; set; }
    public string GroupContactPerson { get; set; }
    public string GroupName { get; set; }
    public string IDPhoto { get; set; }
    public string Interests { get; set; }
}
    
public class ADCClientIfExistResult
{
    public string Message { get; set; }
    public List<ADCClientIfExist> Result { get; set; }
    public int ResultStatus { get; set; }
    public long TotalItems { get; set; }
    public long TotalPage { get; set; }
}


