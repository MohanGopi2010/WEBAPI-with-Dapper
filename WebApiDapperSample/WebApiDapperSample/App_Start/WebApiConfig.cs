using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApiDapperSample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Intermediate json converter. 
            var json = config.Formatters.JsonFormatter;

            //Preparing JSON Settings.
            JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                //JSON does not need to be neat. It'll increase the whitespace & increase the size of the RESPONSE.
                Formatting = Formatting.None
            };

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
