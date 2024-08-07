using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class ResultTagAsPaid
{
    public string COCNumber { get; set; }
    public string EffectiveDate { get; set; }
    public string TerminationDate { get; set; }
    public string FreeInsurance { get; set; }
    public string FreeInsuranceCOCNumber { get; set; }
}

public class TagInsuraceAsPaidResult
{
    public string Message { get; set; }
    public List<ResultTagAsPaid> Result { get; set; }
    public int ResultStatus { get; set; }
    public long TotalItems { get; set; }
    public long TotalPage { get; set; }
}

