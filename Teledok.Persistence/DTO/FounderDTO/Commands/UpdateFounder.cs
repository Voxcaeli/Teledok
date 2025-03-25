namespace Teledok.Persistence.DTO.FounderDTO.Commands;

/// <summary>
/// Обновление данных учредителя ЮЛ / основателя ИП
/// </summary>
public class UpdateFounder
{
    /// <summary>
    /// Идентификатор учредителя ЮЛ / основателя ИП
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор персональных данных учредителя ЮЛ / основателя ИП (опционально)
    /// </summary>
    public int? PersonId { get; set; }

    /// <summary>
    /// Список юридических лиц, основанных учредителем / ИП, основанный физическим лицом (опционально)
    /// </summary>
    public List<int>? Clients { get; set; }
}