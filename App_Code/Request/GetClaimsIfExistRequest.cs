﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class GetClaimsIfExistRequest
{
    public string Token { get; set; }
    public string ClaimsReferenceNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PlatformKey { get; set; }
}

