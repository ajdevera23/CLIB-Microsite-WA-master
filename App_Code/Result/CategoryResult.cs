using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CategoryResult
/// </summary>
public class CategoryResult
{
    public CategoryResult()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string _categoryCode;
    public string CategoryCode
    {
        get { return _categoryCode; }
        set { _categoryCode = value; }
    }
    private string _categoryName;
    public string CategoryName
    {
        get { return _categoryName; }
        set { _categoryName = value; }
    }
    private string _iconPath;
    public string IconPath
    {
        get { return _iconPath; }
        set { _iconPath = value; }
    }
}