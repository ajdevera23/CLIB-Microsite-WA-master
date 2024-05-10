using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaseResult
/// </summary>
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Results
{
    public string Code { get; set; }
    public object Message { get; set; }
    public string MessageID { get; set; }
    public int MsgCount { get; set; }
    public string Name { get; set; }
    public int ResultStatus { get; set; }
    public int Telco_ID { get; set; }
    public string Timestamp { get; set; }
    public string TransID { get; set; }
}

public class SMSResults
{
    public string Message { get; set; }
    public Result Result { get; set; }
    public int ResultStatus { get; set; }
    public int TotalItems { get; set; }
    public int TotalPage { get; set; }
}

