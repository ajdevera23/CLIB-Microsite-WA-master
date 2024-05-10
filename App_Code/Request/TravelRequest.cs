using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class OptionalCoverageCollection
{
    public bool Answer { get; set; }
    public int QuestionNo { get; set; }
}
public class TravelRequest : BaseRequest
{
    public string BookingReferenceNumber { get; set; }
    public string Destination { get; set; }
    public bool IsCovidCoverage { get; set; }
    public List<OptionalCoverageCollection> OptionalCoverageCollections { get; set; }
    public string PassportNumber { get; set; }
    public string PurposeOfTravel { get; set; }
    public int TravelDurationDays { get; set; }
    public string TravelDurationFrom { get; set; }
    public string TravelDurationTo { get; set; }
    public string VisaType { get; set; }
    public string SecondaryProductCode { get; set; }
}

