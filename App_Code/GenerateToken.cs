using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GenerateToken
/// </summary>
public class GenerateToken
{
    public string GenerateTokenAuth()
    {
        string token = "";
        token = TokenAuth4.TokenAuth.Generate(System.Configuration.ConfigurationManager.AppSettings["Passkey"]);
        return token;
    }
}