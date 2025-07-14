using TenderManagementSystem.Core.Exceptions;

namespace TenderManagementSystem.API.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Business exception has been thrown");

            context.Response.StatusCode = ex switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                RepositoryException => StatusCodes.Status400BadRequest,
                DuplicateKeyException => StatusCodes.Status400BadRequest,
                UserRegistrationException => StatusCodes.Status400BadRequest,
                LogInFailedException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };

            await WriteResponseAsync(context, ex);
        }
    }

    private static async Task WriteResponseAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        var traceId = context.Response.Headers["X-Trace-Id"].ToString();
        var response = new
        {
            Error = ex.Message.Replace(Environment.NewLine, " - "),
            StackTrace = context.Response.StatusCode == StatusCodes.Status500InternalServerError ? ex.StackTrace : null,
            TraceId = traceId,
            Timestamp = DateTime.UtcNow.ToString("o"),

        };

        await context.Response.WriteAsJsonAsync(response);
    }
}