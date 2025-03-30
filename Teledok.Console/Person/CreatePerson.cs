namespace Teledok.Console.Person;

/// <summary>
/// Добавление персональных данных нового физического лица
/// </summary>
public class CreatePerson
{
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
    /// Дата рождения физического лица (опционально)
    /// </summary>
    public string? Birthday { get; set; }

    /// <summary>
    /// Адрес физического лица (опционально)
    /// </summary>
    public string? Address { get; set; }
}