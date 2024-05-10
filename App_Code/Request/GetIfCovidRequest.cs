using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class GetIfCovidRequest
{
    public string Token { get; set; }
    public long Age { get; set; }
    public string PlatformKey { get; set; }
    public string SecondaryProductCode { get; set; }
}
