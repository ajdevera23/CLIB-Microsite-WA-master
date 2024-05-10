using System;
using System.Web;

namespace YourNamespace
{
    public class SameSiteCookieModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        public void Dispose() { }

        private void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            if (HttpContext.Current.Response.Headers["Set-Cookie"] != null)
            {
                string[] cookies = HttpContext.Current.Response.Headers.GetValues("Set-Cookie");
                for (int i = 0; i < cookies.Length; i++)
                {
                    // Add SameSite=None attribute to all cookies
                    cookies[i] += "; SameSite=Strict";
                }
                HttpContext.Current.Response.Headers.Set("Set-Cookie", cookies.ToString());
            }
        }
    }
}
