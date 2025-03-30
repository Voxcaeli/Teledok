namespace Teledok.Console.Person;

/// <summary>
/// Обновление персональных данных физического лица
/// </summary>
public class UpdatePerson
{
    /// <summary>
    /// Идентификатор персональных данных физического лица
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Имя физического лица (опционально)
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Фамилия физического лица (опционально)
    /// </summary>
    public string? Surname { get; set; }

    /// <summary>
    /// Отчество физического лица (опционально)
    /// </summary>
    public string? Patronomic { get; set; }

    /// <summary>
    /// Дата рождения физического лица (опционально)
    /// </summary>
    public string? Birthday { get; set; }

    /// <summary>
    /// Адрес физического лица (опционально)
    /// </summary>
    public string? Address { get; set; }
}