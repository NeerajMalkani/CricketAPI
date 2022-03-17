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
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                serializerSettings.Formatting = Formatting.Indented;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response, serializerSettings));
                return;
            }
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(APIKEY);
            if (!apiKey.Equals(extractedApiKey))
            {
                Common.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized", "Authorization has been denied for this request", out response);
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response, serializerSettings));
                return;
            }
            await _next(context);
        }
    }
}
