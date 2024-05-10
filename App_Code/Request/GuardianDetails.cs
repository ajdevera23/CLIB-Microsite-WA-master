using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GuardianDetails
/// </summary>
public class GuardianDetails
{
    public GuardianDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string _guardianBirthday;
    public string GuardianBirthday
    {
        get { return _guardianBirthday; }
        set { _guardianBirthday = value; }
    }

    private string _guardianContactNo;
    public string GuardianContactNo
    {
        get { return _guardianContactNo; }
        set { _guardianContactNo = value; }
    }

    private string _guardianName;
    public string GuardianName
    {
        get { return _guardianName; }
        set { _guardianName = value; }
    }

    private string _guardianRelationship;
    public string GuardianRelationship
    {
        get { return _guardianRelationship; }
        set { _guardianRelationship = value; }
    }
}