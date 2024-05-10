using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TokenResult
/// </summary>
public class DestinationTypeResult

{
    public string Message { get; set; }
    public List<DestinationTypeListResult> Result { get; set; }
    public int ResultStatus { get; set; }
    public long TotalItems { get; set; }
    public long TotalPage { get; set; }

}
public class DestinationTypeListResult
{
    public string Destination { get; set; }
    public string VisaType { get; set; }
}
