using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class GetExistingDocuments
{
    public string FileData { get; set; }
    public string FileLocation { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public long SubmittedRequirementsId { get; set; }
}
public class GetExistingDocumentsResults
{
    public string Message { get; set; }
    public List<GetExistingDocuments> Result { get; set; }
    public int ResultStatus { get; set; }
    public long TotalItems { get; set; }
    public long TotalPage { get; set; }
}
