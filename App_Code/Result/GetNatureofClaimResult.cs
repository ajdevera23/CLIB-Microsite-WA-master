using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class GetNatureofClaim
{
    public string NatureOfClaim { get; set; }
}

public class GetNatureofClaimResult
{
    public string Message { get; set; }
    public GetNatureofClaim Result { get; set; }
    public int ResultStatus { get; set; }
    public long TotalItems { get; set; }
    public long TotalPage { get; set; }
}
