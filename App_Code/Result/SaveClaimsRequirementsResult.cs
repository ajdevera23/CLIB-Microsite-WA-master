using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TokenResult
/// </summary>

public class SaveClaimsRequirements
{
    public long Id { get; set; }
}

public class SaveClaimsRequirementsResult
{
    public long DocumentId { get; set; }
    public string Message { get; set; }
    public SaveClaimsRequirements Result { get; set; }
    public int ResultStatus { get; set; }
    public HttpPostedFile Document { get; set; }
}
