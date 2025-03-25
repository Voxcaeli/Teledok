namespace Teledok.Persistence.DTO.PersonDTO.Queries;

/// <summary>
/// Полное описание персональных данных 
/// </summary>
public class DetailsPerson
{
    /// <summary>
    /// Идентификатор персональных данных физического лица
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Имя физического лица
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Фамилия физического лица
    /// </summary>
    public required string Surname { get; set; }

    /// <summary>
    /// Отчество физического лица (опционально)
    /// </summary>
    public string? Patronomic { get; set; }

    /// <summary>
    /// Полное имя физического лица
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Сокращённое имя физического лица
    /// </summary>
    public required string ShortName { get; set; }

    /// <summary>
    /// Дата рождения физического лица
    /// </summary>
    public required string Birthday { get; set; }

    /// <summary>
    /// Адрес регистрации физического лица (опционально)
    /// </summary>
    public string? Address { get; set; }
}