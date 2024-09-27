using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class GetExistingDocumentsRequest
{
    public string Token { get; set; }
    public long ClaimsDocumentsId { get; set; }
    public string ClaimsReferenceNumber { get; set; }
    public string PlatformKey { get; set; }
}

