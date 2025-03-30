namespace Teledok.Console.Person;

/// <summary>
/// Краткое описание персональных данных 
/// </summary>
public class BriefPerson
{
    /// <summary>
    /// Идентификатор персональных данных
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Полное имя физического лица
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public required string Birthday { get; set; }
}