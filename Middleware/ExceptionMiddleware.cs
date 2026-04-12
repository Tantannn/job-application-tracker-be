namespace JobTracker.API.Middleware;

using System.Net;
using System.Text.Json;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await next(context);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Unhandled exception");
      await HandleExceptionAsync(context, ex);
    }
  }

  private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
  {
    var (statusCode, message) = ex switch
    {
      UnauthorizedAccessException => (HttpStatusCode.Unauthorized, ex.Message),
      InvalidOperationException => (HttpStatusCode.Conflict, ex.Message),
      KeyNotFoundException => (HttpStatusCode.NotFound, ex.Message),
      _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
    };

    context.Response.ContentType = "application/json";
    context.Response.StatusCode = (int)statusCode;

    var response = JsonSerializer.Serialize(new
    {
      status = (int)statusCode,
      message
    });

    await context.Response.WriteAsync(response);
  }
}