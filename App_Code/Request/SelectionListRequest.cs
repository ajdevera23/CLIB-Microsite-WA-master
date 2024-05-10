using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TokenResult
/// </summary>
public class SelectionListRequest
     
{
    public int Limit { get; set; }
    public int Page { get; set; }
    public string Token { get; set; }
    public int DefinitionId { get; set; }
    public string PlatformKey { get; set; }

}