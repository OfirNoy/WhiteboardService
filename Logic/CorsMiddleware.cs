using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WhiteboardService.Logic
{
    internal class WhiteboardCorsMiddleware
    {
        const string WB_CORS_ORIGINS = "WB_CORS_ORIGINS";
        
        private readonly RequestDelegate _next;
        private List<string> _allowedOrigins = new List<string>();

        public WhiteboardCorsMiddleware(RequestDelegate next)
        {
            var cors = Environment.GetEnvironmentVariable(WB_CORS_ORIGINS);
            if (!string.IsNullOrEmpty(cors))
            {
                _allowedOrigins.AddRange(cors.Split(' '));
            }
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue("Origin", out var originValue) &&
                _allowedOrigins.Contains(originValue))
            {
                httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                httpContext.Response.Headers.Add("Access-Control-Allow-Headers", "x-requested-with");
                httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,OPTIONS");
                httpContext.Response.Headers.Add("Access-Control-Allow-Origin", originValue);

                if (httpContext.Request.Method == "OPTIONS")
                {
                    httpContext.Response.StatusCode = 204;
                    return Task.FromResult<string>("");
                }
            }

            return _next(httpContext);
        }
    }
    public static class CorsMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WhiteboardCorsMiddleware>();
        }
    }
}
