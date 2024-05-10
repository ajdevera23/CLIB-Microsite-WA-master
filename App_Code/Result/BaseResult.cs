using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaseResult
/// </summary>
public class BaseResult
{
    public BaseResult()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region Fields/Properties
    private ResultType _resultStatus;
    public ResultType ResultStatus
    {
        get { return _resultStatus; }
        set { _resultStatus = value; }
    }

    private string _message;


    public string Message
    {
        get { return _message; }
        set { _message = value; }
    }

    private int _messageID;


    public int MessageID
    {
        get { return _messageID; }
        set { _messageID = value; }
    }

    private int _logID;


    public int LogID
    {
        get { return _logID; }
        set { _logID = value; }
    }
    #endregion
}