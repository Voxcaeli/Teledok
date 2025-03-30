namespace Teledok.WebAPI.Middlewares;

/// <summary>
/// Расширение для использования пользовательских обработчиков ошибок
/// </summary>
public static class UseCustomExceptionHandlerExtension
{
    /// <summary>
    /// Использование пользовательских обработчиков ошибок
    /// </summary>
    /// <param name="builder">Строитель приложения</param>
    /// <returns>Строитель приложения с пользовательскими обработчиками ошибок</returns>
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<CustomExceptionHandlerMiddleware>();

        return builder;
    }
}
