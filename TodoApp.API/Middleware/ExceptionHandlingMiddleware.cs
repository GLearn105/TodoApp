using System.Net;
using System.Text.Json;
using TodoApp.API.Models;
using TodoApp.Domain.Exceptions;

namespace TodoApp.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Teruskan request ke middleware/handler berikutnya
                await _next(context);
            }
            catch (Exception ex)
            {
                // Jika ada exception, tangkap di sini
                _logger.LogError(ex, "Unhandled exception terjadi: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = exception switch
            {
                NotFoundException ex => new ErrorResponse
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Title = "Not Found",
                    Detail = ex.Message
                },
                BadRequestException ex => new ErrorResponse
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = "Bad Request",
                    Detail = ex.Message
                },
                Domain.Exceptions.ValidationException ex => new ErrorResponse
                {
                    Status = (int)HttpStatusCode.UnprocessableEntity,
                    Title = "Validation Error",
                    Detail = ex.Message,
                    Errors = ex.Errors
                },
                _ => new ErrorResponse
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "Terjadi kesalahan pada server. Silakan coba lagi."
                }
            };

            response.StatusCode = errorResponse.Status;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(errorResponse, options);
            await response.WriteAsync(json);
        }
    }
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
