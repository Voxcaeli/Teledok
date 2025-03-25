namespace Teledok.Persistence.DTO.FounderDTO.Queries;

/// <summary>
/// Краткие данные учредителя ЮЛ / основателя ИП
/// </summary>
public class BriefFounder
{
    /// <summary>
    /// Идентификатор учредителя ЮЛ / основателя ИП
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// ФИО учредителя ЮЛ / основателя ИП
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Дата рождения учредителя ЮЛ / основателя ИП (опционально)
    /// </summary>
    public string? Birthday { get; set; }
}