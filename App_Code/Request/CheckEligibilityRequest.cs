using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TokenResult
/// </summary>
public class CheckEligibilityRequest
     
{
    public string Token { get; set; }
    public string Birthdate { get; set; }
    public string CategoryCode { get; set; }
    public string FirstName { get; set; }
    public long IntegrationId { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public long NumberOfCOCs { get; set; }
    public string PartnerCode { get; set; }
    public string PlatformKey { get; set; }
    public string PlatformName { get; set; }
    public string ProductCode { get; set; }
    public string ProviderCode { get; set; }
    public string ProductName { get; set; }

}