using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class GetBenefitByNatureOfClaimRequest
{
    public string Token { get; set; }
    public long BenefitCoverageId { get; set; }
    public string NatureOfClaim { get; set; }
    public string PlatformKey { get; set; }
}

