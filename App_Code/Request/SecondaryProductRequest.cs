using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class SecondaryProductRequest
{
    public string Token { get; set; }
    public string PartnerCode { get; set; }
    public long PlatformId { get; set; }
    public string PlatformKey { get; set; }
    public string PrimaryProductCode { get; set; }
}
