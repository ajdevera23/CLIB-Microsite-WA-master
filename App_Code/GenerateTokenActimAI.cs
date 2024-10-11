using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
public class GenerateTokenActimAI
{
    public string GenerateToken()
    {
        System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        string responseString = "";
        using (var client = new WebClient())
        {
            client.Headers.Add("Host", SystemSetting.ActimAI_Host);
            client.Headers.Add("Content-Type", SystemSetting.ActimAI_Content_Type);

            var data = new NameValueCollection
            {
                { "client_id", SystemSetting.ActimAI_client_id },
                { "client_secret", SystemSetting.ActimAI_client_secret },
                { "grant_type", SystemSetting.ActimAI_grant_type }
            };

            try
            {
                byte[] response = client.UploadValues(SystemSetting.ActimAI_Api_Docthread, "POST", data);
                 responseString = Encoding.UTF8.GetString(response);
            
            }
            catch (WebException ex)
            {
                using (var reader = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();
                    Console.WriteLine(responseText);
                }
            }
        }
    return responseString;

    }
}
