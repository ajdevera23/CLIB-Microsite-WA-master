using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Claim
{
    public string CRN { get; set; }
    public string NatureOfClaims { get; set; }
    public string ProductCode { get; set; }
    public List<Benefit> Benefits { get; set; }
}

public class Benefit
{
    public int BenefitId { get; set; }
    public string BenefitCode { get; set; }
    public string BenefitName { get; set; }
    public string CoverageAmount { get; set; }
}