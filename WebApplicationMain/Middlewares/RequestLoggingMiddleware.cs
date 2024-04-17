using DnsClient;
using WebApplicationMain.Models;
using WebApplicationMain.Services;

namespace WebApplicationMain.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogServices _services;

        public RequestLoggingMiddleware(RequestDelegate next, ILogServices services)
        {
            _next = next;
            this._services = services;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var user = context.User.Identity?.Name;
            var timestamp = DateTime.UtcNow;
            var email = context.User.FindFirst("Email")?.ToString();
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            var userAgent = context.Request.Headers["User-Agent"];
            var requestUrl = context.Request.Path;
            var requestMethod = context.Request.Method;


            await _next(context);

            var response = context.Response;
            var statusCode = response.StatusCode;

            SaveLogToDatabase(user, email, timestamp, ipAddress, userAgent, requestUrl, requestMethod ,statusCode);
        }

        private void SaveLogToDatabase(string? user, string? email ,DateTime timestamp, string ipAddress, string userAgent, string requestUrl, string requestMethod ,int StatusCode)
        {
            Log newlog = new Log
            {
                Name = user,
                Email = email,
                TimeStamp = timestamp,
                IpAddress = ipAddress,
                UserAgent = userAgent,
                RequestMethod = requestMethod,
                RequestUrl = requestUrl,
                StatudeCode = StatusCode
            };

            _services.CreateAsync(newlog);
        }
    }
}
