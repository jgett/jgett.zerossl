using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace Jgett.ZeroSsl
{
    // 1) Add to bin folder:
    //  Jgett.ZeroSsl.dll
    //
    // 2) Add to web.config:
    //  <appSettings>
	//      <add key = "AcmeChallengePath" value="c:\somepath\to\zerossl\downloads" />
	//  </appSettings>
    //
    //  <system.webServer>
    //      <handlers>
    //          <add name = "ZeroSslHandler" path=".well-known/acme-challenge/*" verb="GET" type="Jgett.ZeroSsl.Handler" />
    //      </handlers>
    //  </system.webServer>

    public class Handler : IHttpHandler
    {
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            // <add key="AcmeChallengePath" value="g:\secure\acme" />
            string acmePath = ConfigurationManager.AppSettings["AcmeChallengePath"];

            if (string.IsNullOrEmpty(acmePath))
                throw new Exception("Missing required AppSetting: AcmeChallengePath");

            string fileName = context.Request.Url.PathAndQuery.Replace("/.well-known/acme-challenge/", string.Empty);

            if (string.IsNullOrEmpty(acmePath))
                throw new Exception("Missing required AppSetting: AcmeChallengePath");

            string filePath = Path.Combine(acmePath, fileName);

            if (File.Exists(filePath))
            {
                var text = File.ReadAllText(filePath);
                context.Response.Write(text);
            }
            else
            {
                context.Response.StatusCode = 404;
                context.Response.SuppressContent = true;
                context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}
