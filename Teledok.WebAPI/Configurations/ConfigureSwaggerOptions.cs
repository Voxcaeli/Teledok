using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Teledok.WebAPI.Configurations;

/// <summary>
/// Настройка параметров Swagger'а
/// </summary>
/// <param name="provider">Провайдер, описывающий версии API</param>
public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    : IConfigureOptions<SwaggerGenOptions>
{
    /// <summary>
    /// Конфигурирование Swagger'а
    /// </summary>
    /// <param name="options">Настройка описания Swagger'а</param>
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            var apiVersion = description.ApiVersion.ToString();

            options.SwaggerDoc(description.GroupName,
                new OpenApiInfo
                {
                    Version = apiVersion,
                    Title = $"Teledok API {apiVersion}",
                    Description = "A simple web service that provides a RESTful API for managing clients and founders.",
                    Contact = new OpenApiContact
                    {
                        Name = "Eugene Zhigulev",
                        Email = "voxcaeli@yandex.ru",
                        Url = new Uri("https://t.me/voxcaeli")
                    },
                });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }
    }
}