using Microsoft.EntityFrameworkCore;
using Teledok.Domain;

namespace Teledok.Persistence.DBContext;

/// <summary>
/// Контекст базы данных
/// </summary>
/// <param name="options">Настройки контекста базы данных</param>
public class TeledokDBContext(DbContextOptions<TeledokDBContext> options)
    : DbContext(options)
{
    /// <summary>
    /// Таблица клиентов
    /// </summary>
    public DbSet<Client> Clients { get; set; }

    /// <summary>
    /// Таблица учредителя ЮЛ / основателя ИП
    /// </summary>
    public DbSet<Founder> Founders { get; set; }

    /// <summary>
    /// Таблица персональных данных физических лиц
    /// </summary>
    public DbSet<Person> People { get; set; }
}