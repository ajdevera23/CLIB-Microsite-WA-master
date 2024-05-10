using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class FieldValidationResult
{
    public string Message { get; set; }
    public object Result { get; set; }
    public int ResultStatus { get; set; }
    public int TotalItems { get; set; }
    public int TotalPage { get; set; }
}

