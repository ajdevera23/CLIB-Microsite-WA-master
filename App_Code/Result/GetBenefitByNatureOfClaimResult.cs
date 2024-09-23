using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class GetBenefitByNatureOfClaim
{
    public string Benefit { get; set; }
    public string BenefitCode { get; set; }
    public long BenefitId { get; set; }
    public double CoverageAmount { get; set; }
}

public class GetBenefitByNatureOfClaimResult
{
    public string Message { get; set; }
    public List<GetBenefitByNatureOfClaim> Result { get; set; }
    public int ResultStatus { get; set; }
    public long TotalItems { get; set; }
    public long TotalPage { get; set; }
}

