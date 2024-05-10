using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TokenResult
/// </summary>

public class SecondaryProductResultList
{
    public double CoreBenefitAmount { get; set; }
    public long IntegrationId { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public string ProviderCode { get; set; }
}
public class SecondaryProductResult
{
    public string Message { get; set; }
    public List<SecondaryProductResultList> Result { get; set; }
    public int ResultStatus { get; set; }
    public long TotalItems { get; set; }
    public long TotalPage { get; set; }
}
