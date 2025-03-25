namespace Teledok.Domain;

/// <summary>
/// Персональные данные физического лица
/// </summary>
public class Person
{
    /// <summary>
    /// Идентификатор персональных данных физического лица
    /// </summary>
    public int Id { get; set; }

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
    /// Дата рождения физического лица
    /// </summary>
    public required DateOnly Birthday { get; set; }

    /// <summary>
    /// Адрес регистрации физического лица (опционально)
    /// </summary>
    public string? Address { get; set; }
}