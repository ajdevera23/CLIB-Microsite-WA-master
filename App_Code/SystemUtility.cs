using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for SystemUtility
/// </summary>
public class SystemUtility
{
    public SystemUtility()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string JsonHttpPost(string jsonRequest, string requestUrl)
    {
        string jsonResponse = string.Empty;

        if (SystemSetting.IsTlsRequired == "true")
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)(SystemSetting.SecurityProtocolType);
        }

        using (var webClient = new WebClient())
        {
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            webClient.Headers[HttpRequestHeader.Accept] = "application/json";
            return webClient.UploadString(requestUrl, "POST", jsonRequest);
        }
    }

    public static string JsonHttpPostXendit(string jsonRequest, string requestUrl)
    {
        string jsonResponse = string.Empty;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)(SystemSetting.SecurityProtocolType);
       //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        using (var webClient = new WebClient())
        {
           // WebProxy proxy = new WebProxy(SystemSetting.ProxyServerXendit, SystemSetting.ProxyPort);
            //webClient.Proxy = proxy;

            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            webClient.Headers[HttpRequestHeader.Accept] = "application/json";
            webClient.Headers.Add(SystemSetting.Authorization, SystemSetting.BasicAuthorization);

            return webClient.UploadString(requestUrl, "POST", jsonRequest);
        }
    }

    public static byte[] JsonHttpPostByte(byte[] jsonRequest, string requestUrl)
    {
        string jsonResponse = string.Empty;

        if (SystemSetting.IsTlsRequired == "true")
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)(SystemSetting.SecurityProtocolType);
        }

        using (var webClient = new WebClient())
        {
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            webClient.Headers[HttpRequestHeader.Accept] = "application/json";
            return webClient.UploadData(requestUrl, "POST", jsonRequest);
        }
    }
    public enum AppendStringLocation
    {
        Top,
        Bottom
    }
    public class EventLog
    {
        public static int SaveError(string errorMessage)
        {
            return Save(errorMessage, EventLogEntryType.Error);
        }
        private static int Save(string message, EventLogEntryType eventLogEntryType)
        {
            int eventID = SystemUtility.RandomGenerator.GetNewEventID();
            try
            {
                System.Diagnostics.EventLog.WriteEntry(SystemSetting.EventSource, message, eventLogEntryType, eventID);

            }
            catch (Exception ex)
            {
                try
                {
                    SaveToFile(ex.ToString(), EventLogEntryType.Error, eventID);
                    SaveToFile(message, eventLogEntryType, eventID);
                }
                catch
                {
                    return 0;
                }
            }

            return eventID;
        }
    }

    public class RandomGenerator
    {
        #region Public Methods
        public static int GetNewEventID()
        {
            // Delay for 100 milliseconds
            System.Threading.Thread.Sleep(100);

            System.Random random = new System.Random();
            string returnValue = string.Empty;
            string intPool = "123456789";

            for (int idx1 = 0; idx1 < 4; idx1++)
            {
                returnValue += intPool[random.Next(0, intPool.Length)].ToString();
            }

            return Convert.ToInt32(returnValue);
        }
        #endregion
    }
    private static void SaveToFile(string message, System.Diagnostics.EventLogEntryType eventLogEntryType, int eventID)
    {
        System.Threading.Thread.Sleep(100);

        string logFileName = DateTime.Today.ToString("yyyy-MM-dd");
        string logDirectory = SystemSetting.EventLogDirectory;
        StringBuilder logDetails = new StringBuilder();

        logDetails.AppendLine(string.Format("Event ID: {0}", eventID));
        logDetails.AppendLine(string.Format("DateTime: {0:MM-dd-yyyy hh:mm:ss tt}", DateTime.Now));
        logDetails.AppendLine(string.Format("Event Type: {0}", eventLogEntryType.ToString()));
        logDetails.AppendLine(string.Format("Event Source: {0}", SystemSetting.EventSource));
        logDetails.AppendLine(string.Format("Details: {0}{1}{0}", Environment.NewLine, message));
        logDetails.AppendLine(string.Format("******** End of line ********{0}{0}{0}", Environment.NewLine));

        FileManager.CreateDirectory(logDirectory, false);
        FileManager.AppendStringToFile(Path.Combine(logDirectory, logFileName), AppendStringLocation.Top, logDetails.ToString());
    }
    public class FileManager
    {
        #region Public Methods
        public static void CreateDirectory(string directoryPath, bool overwriteExistingPath)
        {
            if (overwriteExistingPath)
            {
                Directory.CreateDirectory(directoryPath);
            }
            else
            {
                if (Directory.Exists(directoryPath))
                {
                    // do nothing
                }
                else
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }
        }

        public static void AppendStringToFile(string filePath, AppendStringLocation appendStringLocation, string stringValue)
        {
            StringBuilder oldFileContent = new StringBuilder();
            String newFileContent = null;

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        oldFileContent.AppendLine(reader.ReadLine());
                    }
                }

                File.Delete(filePath);
            }
            else
            {
                // do nothing
            }

            switch (appendStringLocation)
            {
                case AppendStringLocation.Top:
                    {
                        newFileContent = string.Concat(stringValue, oldFileContent.ToString());
                        break;
                    }
                case AppendStringLocation.Bottom:
                    {
                        newFileContent = string.Concat(oldFileContent.ToString(), stringValue);
                        break;
                    }
                default:
                    {
                        throw new Exception(string.Format("Unknown option {0}.{1}.", appendStringLocation.GetType(), appendStringLocation.ToString()));
                    }
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(newFileContent);
            }
        }
        #endregion
    }
    public class Configuration
    {
        #region Public Methods
        public static string GetAppSetting(string key)
        {
            string returnValue = ConfigurationManager.AppSettings[key];

            if (Validation.IsEmptyString(returnValue))
            {
                throw new Exception(string.Format("Key {0} was not found in the configuration file.", key));
            }
            else
            {
                return returnValue;
            }
        }

        public static string GetAppSetting(System.Configuration.Configuration config, string key)
        {
            string returnValue = config.AppSettings.Settings[key].Value;

            if (Validation.IsEmptyString(returnValue))
            {
                throw new Exception(string.Format("Key {0} was not found in the configuration file.", key));
            }
            else
            {
                return returnValue;
            }
        }

        public static string GetConnectionString(string name)
        {
            string returnValue = ConfigurationManager.ConnectionStrings[name].ConnectionString;

            if (Validation.IsEmptyString(returnValue))
            {
                throw new Exception(string.Format("ConnectionString {0} was not found in the configuration file.", name));
            }
            else
            {
                return returnValue;
            }
        }

        public static System.Configuration.Configuration GetLocalConfiguration(string exeName)
        {
            return System.Configuration.ConfigurationManager.OpenExeConfiguration(exeName);
        }

        public static void SetAppSetting(System.Configuration.Configuration config, string key, string value)
        {
            config.AppSettings.Settings[key].Value = value;
            config.Save();
        }
        #endregion
    }
    public class Validation
    {
        #region Public Methods
        public static bool HasColumn(SqlDataReader sqlDataReader, string columnName)
        {
            sqlDataReader.GetSchemaTable().DefaultView.RowFilter = string.Format("ColumnName= '{0}'", columnName);

            return sqlDataReader.GetSchemaTable().DefaultView.Count > 0;
        }

        public static bool IsEmptyString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            else
            {
                if (value.Trim().Length.Equals(0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool IsNullable<T>(T t)
        {
            return false;
        }

        public static bool IsNullable<T>(T? t) where T : struct
        {
            return true;
        }
        #endregion
    }


}