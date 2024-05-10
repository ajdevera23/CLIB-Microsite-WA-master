using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaseResult
/// </summary>
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class ProductProfileResult
{
    public string Message { get; set; }
    public string Result { get; set; }
    public int ResultStatus { get; set; }
    public long TotalItems { get; set; }
    public long TotalPage { get; set; }
}

