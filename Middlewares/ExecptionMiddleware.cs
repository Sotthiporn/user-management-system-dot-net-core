using System.Text.Json;
using user_management_dot_net_core.Exceptions;
using System.Net;

namespace user_management_dot_net_core.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "Not Found Exception");
                await HandleExceptionAsync(httpContext, HttpStatusCode.NotFound, "Resource not found.");
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation Exception");
                await HandleExceptionAsync(httpContext, HttpStatusCode.UnprocessableEntity, ex.Message);
            }
            catch (Exceptions.UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized Access Exception");
                await HandleExceptionAsync(httpContext, HttpStatusCode.Unauthorized, ex.Message ?? "Unauthorized access.");
            }
            catch (ForbiddenException ex)
            {
                _logger.LogError(ex, "Forbidden Exception");
                await HandleExceptionAsync(httpContext, HttpStatusCode.Forbidden, ex.Message ?? "Forbidden access.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, "An unexpected error occurred.");
            }
        }

        private Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var errorResponse = new
            {
                Success = false,
                Message = message
            };

            var response = JsonSerializer.Serialize(errorResponse);
            return context.Response.WriteAsync(response);
        }
    }
}
