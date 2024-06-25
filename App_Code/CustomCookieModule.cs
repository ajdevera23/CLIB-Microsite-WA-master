using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

public class CustomCookieModule : IHttpModule
{

    public void Init(HttpApplication context)
    {
        context.BeginRequest += OnBeginRequest;
        //context.EndRequest += OnEndRequest;
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

        //if (!IsWebResource(context.Request.Url.AbsolutePath))
        //{
        //    InjectScript(context);
        //}
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

    private bool IsWebResource(string absolutePath)
    {
        // Add conditions here if you want to exclude certain paths
        return absolutePath.EndsWith(".js", StringComparison.OrdinalIgnoreCase);
    }

    private void InjectScript(HttpContext context)
    {
        var scriptTag = "<script src=\"/JScript/GlobalScript.js\"></script>";

        // Append the script tag just before the closing </body> tag
        var response = context.Response;
        if (response.ContentType.Contains("text/html"))
        {
            response.Filter = new ScriptInjectionStream(response.Filter, scriptTag);
        }
    }

    public void Dispose()
    {
        // Clean-up code here, if any.
    }

    private void OnEndRequest(object sender, EventArgs e)
    {
        // Any clean-up code if necessary
    }

    internal class ScriptInjectionStream : MemoryStream
    {
        private readonly Stream _originalStream;
        private readonly string _scriptTag;
        private bool _scriptInjected;

        public ScriptInjectionStream(Stream originalStream, string scriptTag)
        {
            _originalStream = originalStream;
            _scriptTag = scriptTag;
            _scriptInjected = false;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            string html = Encoding.UTF8.GetString(buffer, offset, count);

            // Inject the script tag just before </body>
            if (!_scriptInjected && html.Contains("</body>"))
            {
                html = html.Replace("</body>", _scriptTag + "</body>");
                _scriptInjected = true;
            }

            byte[] outdata = Encoding.UTF8.GetBytes(html);
            _originalStream.Write(outdata, 0, outdata.Length);
            //_originalStream.Write(outdata, 0, outdata.GetLength(0));
        }

        // Implementing other methods of Stream class not used in this context
        // These are required to compile and satisfy the Stream abstract class

        public override void Flush()
        {
            _originalStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _originalStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _originalStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _originalStream.Read(buffer, offset, count);
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position
        {
            get { return _originalStream.Position; }
            set { _originalStream.Position = value; }
        }
    }
}

