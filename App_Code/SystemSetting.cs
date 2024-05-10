using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SystemSetting
/// </summary>
public class SystemSetting
{
    public SystemSetting()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string EventSource
    {
        get
        {
            try
            {
                return SystemUtility.Configuration.GetAppSetting(InsuranceLibraryConfiguration, "EventSource");
            }
            catch
            {
                return "CLIBMicrosite";
            }
        }
    }
    public static string EventLogDirectory
    {
        get
        {
            try
            {
                return SystemUtility.Configuration.GetAppSetting(InsuranceLibraryConfiguration, "EventLogDirectory");
            }
            catch
            {
                return @"C:\EventLogs\CLIBMicrosite";
            }
        }
    }
    public static System.Configuration.Configuration InsuranceLibraryConfiguration
    {
        get { return SystemUtility.Configuration.GetLocalConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location); }
    }

    public static string IsTlsRequired
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["IsTlsRequired"];

        }

    }
    public static string ProxyServerXendit
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["ProxyServer"];
        }
    }
    public static string Authorization
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["Authorization"];
        }
    }
    public static string BasicAuthorization
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["BasicAuthorization"];
        }
    }


    public static int ProxyPort
    {
        get
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ProxyPort"]);

        }
    }
    public static int SecurityProtocolType
    {
        get
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SecurityProtocolType"]);

        }
        // dapat 3072
    }
}