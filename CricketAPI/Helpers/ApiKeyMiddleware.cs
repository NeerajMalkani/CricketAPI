using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace CricketAPI.Helpers
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private
        const string APIKEY = "XApiKey";
        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Response response = new Response();
            if (!context.Request.Headers.TryGetValue(APIKEY, out
                    var extractedApiKey))
            {
                Common.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized", "API Key not provided", out response);
                await context.Response.WriteAsJsonAsync(response);
                return;
            }
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(APIKEY);
            if (!apiKey.Equals(extractedApiKey))
            {
                Common.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized", "Authorization has been denied for this request", out response);
                await context.Response.WriteAsJsonAsync(response);
                return;
            }
            await _next(context);
        }
    }
}
