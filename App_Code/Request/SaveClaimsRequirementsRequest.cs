using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class SaveClaimsRequirementsRequest
{
    public string Token { get; set; }
    public long ClaimsDocumentsId { get; set; }
    public string ClaimsReferenceNumber { get; set; }
    public string FileLocation { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public string PlatformKey { get; set; }
    public string CreatedBy { get; set; }
    public string FileData { get; set; }
}
