using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class DisplaySummaryResult
{
    public string ConvenienceFee { get; set; }
    public string CurrentPremium { get; set; }
    public string Premium { get; set; }
    public string ProductName { get; set; }
}

public class DisplayPaymentSummaryResult
{
    public string Message { get; set; }
    public List<DisplaySummaryResult> Result { get; set; }
    public int ResultStatus { get; set; }
    public long TotalItems { get; set; }
    public long TotalPage { get; set; }
}

