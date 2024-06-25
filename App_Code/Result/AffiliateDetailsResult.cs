using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class AffiliateDetails
{
    public string AffiliateCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public object MiddleName { get; set; }
}

public class AffiliateDetailsResult
{
    public string Message { get; set; }
    public List<AffiliateDetails> Result { get; set; }
    public int ResultStatus { get; set; }
    public int TotalItems { get; set; }
    public int TotalPage { get; set; }
}



