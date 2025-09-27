using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DemoAPI.Middleware
{
    public class CustomLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomLoggingMiddleware> _logger;

        public CustomLoggingMiddleware(RequestDelegate next, ILogger<CustomLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Handling request: {context.Request.Method} {context.Request.Path}");
            await _next(context);
            _logger.LogInformation($"Finished handling request: {context.Request.Method} {context.Request.Path}");
        }
    }

    public static class CustomLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomLoggingMiddleware>();
        }
    }
}
