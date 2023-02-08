using BaseV1.WebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BaseV1.WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly ILoggerManager _loggerManager;

        public ExceptionMiddleware(
            RequestDelegate next,
            IWebHostEnvironment env,
            ILogger<ExceptionMiddleware> logger, ILoggerManager loggerManager)
        {
            _next = next;
            _env = env;
            _logger = logger;
            _loggerManager = loggerManager;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _loggerManager.LogError(ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = _env.IsDevelopment() ?
                    new ProblemDetails
                    {
                        Status = context.Response.StatusCode,
                        Detail = ex.Message,
                        Instance = ex.StackTrace,                        
                    }
                    :
                    new ProblemDetails
                    {
                        Status = context.Response.StatusCode,
                        Detail = "Internal Server Error"
                    };

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
