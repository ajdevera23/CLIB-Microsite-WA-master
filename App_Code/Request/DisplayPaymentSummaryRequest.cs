using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class OptionalCoverageCollections
{
    public bool Answer { get; set; }
    public long QuestionNo { get; set; }
}

public class DisplayPaymentSummaryRequest
{
    public string Token { get; set; }
    public string Birthdate { get; set; }
    public string CategoryCode { get; set; }
    public long IntegrationId { get; set; }
    public bool IsCovidCoverage { get; set; }
    public long NumberOfCOCs { get; set; }
    public List<OptionalCoverageCollections> OptionalCoverageCollections { get; set; }
    public string PartnerCode { get; set; }
    public string PaymentChannel { get; set; }
    public string PlatformKey { get; set; }
    public string PlatformName { get; set; }
    public string ProductCode { get; set; }
    public string ProviderCode { get; set; }
    public string SecondaryProductCode { get; set; }
    public int TravelDurationDays { get; set; }
    public string TravelDurationFrom { get; set; }
    public string TravelDurationTo { get; set; }
    public string ReferralCode { get; set; }

}
