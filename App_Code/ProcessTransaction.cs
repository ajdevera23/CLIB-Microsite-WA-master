using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for ProcessTransaction
/// </summary>
public class ProcessTransaction
{
    public ProcessTransaction()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public ProcessTransactionResult ProcessTransactionDetails(ProcessTransactionRequest processTransactionRequest)
    {
        ProcessTransactionResult returnValue = new ProcessTransactionResult();

        string method = "ProcessInsuranceTransaction";

        JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
        };

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(processTransactionRequest, microsoftDateFormatSettings);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["MicroInsuranceWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<ProcessTransactionResult>(jsonResult);

        return returnValue;
    }
    public BaseResult SendCOCToClient(TokenRequest token)
    {
        BaseResult returnValue = new BaseResult();

        string method = "TagRefCodeIsUsed";

        JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
        };

        string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

        string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseResult>(jsonResult);

        
        return returnValue;
    }

    public BaseResult UploadExcel(TokenRequest token)
    {
        BaseResult returnValue = new BaseResult();

        string method = "UploadExcel";

        JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
        };

        string json = Newtonsoft.Json.JsonConvert.SerializeObject(token);
        byte[] jsonRequest = Encoding.UTF8.GetBytes(json);

        byte[] jsonResult = SystemUtility.JsonHttpPostByte(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);
        string result = Encoding.UTF8.GetString(jsonResult);
        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseResult>(result);
        
        return returnValue;
    }

    public BaseResult SendMBPEmail(TokenRequest token)
    {

        BaseResult returnValue = new BaseResult();

        string method = "TestMBPMail";

        //JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
        //{
        //    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
        //};

        string json = Newtonsoft.Json.JsonConvert.SerializeObject(token);
        byte[] jsonRequest = Encoding.UTF8.GetBytes(json);

        byte[] jsonResult = SystemUtility.JsonHttpPostByte(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);
        string result = Encoding.UTF8.GetString(jsonResult);
        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseResult>(result);

        return returnValue;

        
    }

    public BaseResult SendIQREmail(TokenRequest token)
    {

        BaseResult returnValue = new BaseResult();

        string method = "TestIQRMail";

        //JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
        //{
        //    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
        //};

        string json = Newtonsoft.Json.JsonConvert.SerializeObject(token);
        byte[] jsonRequest = Encoding.UTF8.GetBytes(json);

        byte[] jsonResult = SystemUtility.JsonHttpPostByte(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);
        string result = Encoding.UTF8.GetString(jsonResult);
        returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseResult>(result);

        return returnValue;


    }

    //public BaseResult UploadExcel(TokenRequest token)
    //{
    //    BaseResult returnValue = new BaseResult();

    //    string method = "UploadExcel";

    //    JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
    //    {
    //        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
    //    };

    //    string jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(token);

    //    string jsonResult = SystemUtility.JsonHttpPost(jsonRequest, ConfigurationManager.AppSettings["CLIBMicrositeWS"].Trim() + method);

    //    returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseResult>(jsonResult);

    //    return returnValue;
    //}



}