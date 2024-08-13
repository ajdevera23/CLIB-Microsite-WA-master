using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class ReferralCodeRequest
{
    public string Token { get; set; }
    public int IntegrationId { get; set; }
    public string PlatformKey { get; set; }
    public double Premium { get; set; }
    public string ReferralCode { get; set; }
    public string BirthDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
}



