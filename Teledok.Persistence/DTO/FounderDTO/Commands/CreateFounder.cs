namespace Teledok.Persistence.DTO.FounderDTO.Commands;

/// <summary>
/// Создание нового учредителя ЮЛ / основателя ИП
/// </summary>
public class CreateFounder
{
    /// <summary>
    /// Идентификатор персональных данных учредителя ЮЛ / основателя ИП
    /// </summary>
    public required int PersonId { get; set; }

    /// <summary>
    /// Список юридических лиц, основанных учредителем / основатель ИП (единолично в списке)
    /// </summary>
    public List<int>? Clients { get; set; }
}