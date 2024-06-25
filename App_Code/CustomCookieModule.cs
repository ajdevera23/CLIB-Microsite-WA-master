﻿using System;
using System.Security.Cryptography;
using System.Web;

public class CustomCookieModule : IHttpModule
{

    public void Init(HttpApplication context)
    {
        context.BeginRequest += OnBeginRequest;
    }

    private void OnBeginRequest(object sender, EventArgs e)
    {
        HttpApplication application = (HttpApplication)sender;
        HttpContext context = application.Context;
        HttpCookie existingCookie = context.Request.Cookies["MyCookie"];

        if (existingCookie == null )
        {
            string sessionId = GenerateSecureSessionID();
            SetCookieWithoutSameSite(context.Response, "CLIB_Cookies", sessionId, DateTime.Now.AddHours(1), true, true, "Lax");
        }
    }

    public static string GenerateSecureSessionID()
    {
     
        byte[] sessionIdBytes = new byte[32];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(sessionIdBytes);
        }
        return BitConverter.ToString(sessionIdBytes).Replace("-", "").ToLower();
    }

    private void SetCookieWithoutSameSite(HttpResponse response, string key, string value, DateTime? expires = null, bool secure = true, bool httpOnly = true, string sameSite = "Lax")
    {
        string cookieValue = key + "=" + value;

        if (expires.HasValue)
        {
            cookieValue += "; Expires=" + expires.Value.ToString("R");
        }

        cookieValue += "; Path=/";
        cookieValue += "; SameSite=" + sameSite;

        if (secure)
        {
            cookieValue += "; Secure";
        }

        if (httpOnly)
        {
            cookieValue += "; HttpOnly";
        }

        response.AppendHeader("Set-Cookie", cookieValue);
    }

    public void Dispose()
    {
        // Clean-up code here, if any.
    }
}

