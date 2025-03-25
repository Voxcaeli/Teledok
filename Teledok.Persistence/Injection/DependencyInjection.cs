using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Teledok.Persistence.DBContext;
using Teledok.Persistence.Repositories;
using Teledok.Persistence.Repositories.Interfaces;

namespace Teledok.Persistence.Injection;

/// <summary>
/// Подключение зависимостей
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавление зависимостей
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <param name="configuration">Конфигурация подключения к базе данных</param>
    /// <returns>Объект интерфейса IServiceCollection</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services,
                                                    IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DbConnection");

        services.AddDbContext<TeledokDBContext>(options =>
            options.UseSqlServer(connection));

        services.AddScoped<IRepositoryClient, RepositoryClient>();
        services.AddScoped<IRepositoryFounder, RepositoryFounder>();
        services.AddScoped<IRepositoryPerson, RepositoryPerson>();

        return services;
    }
}