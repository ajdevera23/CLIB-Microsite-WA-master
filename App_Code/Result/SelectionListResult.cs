using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TokenResult
/// </summary>
public class SelectionResult

{
    public string Message { get; set; }
    public List<SelectionListResult> Result { get; set; }
    public int ResultStatus { get; set; }
    public long TotalItems { get; set; }
    public long TotalPage { get; set; }

}
public class SelectionListResult
{
    public long DefinitionId { get; set; }
    public string DisplayText { get; set; }
    public long FieldSelectionId { get; set; }
    public string SelectionDescription { get; set; }
}
