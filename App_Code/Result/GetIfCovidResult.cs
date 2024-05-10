using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TokenResult
/// </summary>

public class GetIfCovidResult
{
    public string Message { get; set; }
    public bool Result { get; set; }
    public int ResultStatus { get; set; }
    public long TotalItems { get; set; }
    public long TotalPage { get; set; }
}
