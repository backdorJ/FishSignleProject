using System.Text.Json;
using FishShop.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FishShop.API.Middlewares;

/// <summary>
/// Middleware для ошибок
/// </summary>
/// <param name="next"></param>
/// <param name="logger"></param>
public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            logger.LogInformation(
                "Начало exception middleware. Обработка запроса {path}",
                context.Request.Path);
            
            await next(context);
            
            logger.LogInformation(
                "Конец exception middleware. Обработка запроса {path}",
                context.Request.Path);
        }
        catch (Exception exception)
        {
            logger.LogCritical(
                exception.Message,
                "Произошла ошибка при обработки запроса {path}",
                context.Request.Path);
            
            await ExceptionResponseAsync(context, exception);
        }
    }

    private async Task ExceptionResponseAsync(HttpContext context, Exception exception)
    {
        var problemDetails = exception switch
        {
            ValidationException or RequiredException or ApplicationBaseException => new ProblemDetails
            {
                Title = exception.HelpLink,
                Status = StatusCodes.Status400BadRequest,
                Detail = exception.Message,
            },
            UnauthorizedAccessException => new ProblemDetails
            {
                Title = exception.HelpLink,
                Status = StatusCodes.Status403Forbidden,
                Detail = exception.Message,
            },
            _ => new ProblemDetails
            {
                Title = exception.HelpLink,
                Status = StatusCodes.Status500InternalServerError,
                Detail = exception.Message
            }
        };

        context.Response.StatusCode = problemDetails.Status!.Value;
        var result = JsonSerializer.Serialize(problemDetails);
        context.Response.ContentType = "application/json";
        context.Request.ContentLength = result.Length;
        await context.Response.WriteAsync(result);
    }
}