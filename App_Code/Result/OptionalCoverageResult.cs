using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TokenResult
/// </summary>

public class OptionalCoverageResultList
{
    public string OptionalCoverageDescription { get; set; }
    public string OptionalCoverageName { get; set; }
    public long QuestionNo { get; set; }
}

public class OptionalCoverageResult
{
    public string Message { get; set; }
    public List<OptionalCoverageResultList> Result { get; set; }
    public int ResultStatus { get; set; }
    public long TotalItems { get; set; }
    public long TotalPage { get; set; }
}
