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
            client.Headers.Add("Host", "api.docthread.ai");
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var data = new NameValueCollection
            {
                { "client_id", "clib-aica-sandbox-api" },
                { "client_secret", "bH1airsgEFzhug0wjizkDkfNdePRkSdV" },
                { "grant_type", "client_credentials" }
            };

            try
            {
                byte[] response = client.UploadValues("https://api.docthread.ai/v1/CLI0240930/auth/token", "POST", data);
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
