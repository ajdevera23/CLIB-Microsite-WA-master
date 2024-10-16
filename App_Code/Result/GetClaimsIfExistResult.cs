using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class GetClaimsIfExist
{
    public long BenefitCoverageId { get; set; }
    public long ClaimsId { get; set; }
    public bool IsClaimsExists { get; set; }
    public string ProductCode { get; set; }
}
public class GetClaimsIfExistResult
{
    public string Message { get; set; }
    public List<GetClaimsIfExist> Result { get; set; }
    public int ResultStatus { get; set; }
    public long TotalItems { get; set; }
    public long TotalPage { get; set; }
}


