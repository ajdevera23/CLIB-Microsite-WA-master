using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class GetDocumentBasedOnBenefit
{
    public string BenefitCode { get; set; }
    public long BenefitId { get; set; }
    public long ClaimsDocumentsId { get; set; }
    public string ClaimsDocumentsName { get; set; }
    public string DocumentType { get; set; }
}
public class GetDocumentBasedOnBenefitResult
{
    public string Message { get; set; }
    public List<GetDocumentBasedOnBenefit> Result { get; set; }
    public int ResultStatus { get; set; }
    public long TotalItems { get; set; }
    public long TotalPage { get; set; }
}
