using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaseResult
/// </summary>
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class ReferralCodeResult
{
    public string AgentCode { get; set; }
    public double DiscountPHP { get; set; }
    public double DiscountPercent { get; set; }
    public double DiscountedPremium { get; set; }
    public string FreeInsuranceProductName { get; set; }
    public int SetCOC { get; set; }
    public bool IsValidFreeInsurance { get; set; }
}
public class ReferralCodeDisplayResult
{
    public string Message { get; set; }
    public ReferralCodeResult Result { get; set; }
    public int ResultStatus { get; set; }
    public int TotalItems { get; set; }
    public int TotalPage { get; set; }
}


