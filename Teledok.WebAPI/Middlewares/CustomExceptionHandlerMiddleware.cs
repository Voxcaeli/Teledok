using FluentValidation;
using System.Net;
using System.Text.Json;
using Teledok.Persistence.Exeptions;

namespace Teledok.WebAPI.Middlewares;

/// <summary>
/// 
/// </summary>
public class CustomExceptionHandlerMiddleware(RequestDelegate next)
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context">Контекст Http-запросов</param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandlerExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Асинхронный вызов обработчика ошибок
    /// </summary>
    /// <param name="context">Контекст Http-запросов</param>
    /// <param name="ex">Возникшая ошибка при Http-запросе</param>
    private static Task HandlerExceptionAsync(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        if (ex is ValidationException validationException)
        {
            code = HttpStatusCode.BadRequest;
            result = JsonSerializer.Serialize(validationException.Errors);
        }

        if (ex is NotFoundException notFoundException)
        {
            code = HttpStatusCode.NotFound;
            result = JsonSerializer.Serialize(notFoundException.Message);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(ex.Message);
        }

        return context.Response.WriteAsync(result);
    }
}
