using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class AvailableCOCDetails
{
    public long ActiveCOCs { get; set; }
    public long AvailableCOCs { get; set; }
    public bool CanPurchase { get; set; }
}

public class AvailableCOCResult
{
    public string Message { get; set; }
    public AvailableCOCDetails Result { get; set; }
    public int ResultStatus { get; set; }
    public long TotalItems { get; set; }
    public long TotalPage { get; set; }
}


