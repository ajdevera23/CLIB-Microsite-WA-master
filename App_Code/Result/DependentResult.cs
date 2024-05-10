using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TokenResult
/// </summary>
public class DependentResult
{
    public string Relationship { get; set; }
    public int RelationshipCount { get; set; }
}
public class RootDependent
{
    public string Message { get; set; }
    public int ResultStatus { get; set; }
    public int TotalItems { get; set; }
    public int TotalPage { get; set; }
    public List<DependentResult> Result { get; set; }
}
