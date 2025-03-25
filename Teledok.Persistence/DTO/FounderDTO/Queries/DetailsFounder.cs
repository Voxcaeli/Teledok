namespace Teledok.Persistence.DTO.FounderDTO.Queries;

/// <summary>
/// Подробные данные учредителя ЮЛ / основателя ИП
/// </summary>
public class DetailsFounder
{
    /// <summary>
    /// Идентификатор учредителя ЮЛ / основателя ИП
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор персональных данных учредителя ЮЛ / основателя ИП
    /// </summary>
    public required int PersonId { get; set; }

    /// <summary>
    /// Полное имя учредителя ЮЛ / основателя ИП
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Сокращённое имя учредителя ЮЛ / основателя ИП
    /// </summary>
    public required string ShortName { get; set; }

    /// <summary>
    /// Дата рождения учредителя ЮЛ / основателя ИП (опционально)
    /// </summary>
    public string? Birthday { get; set; }

    /// <summary>
    /// Адрес регистрации учредителя ЮЛ / основателя ИП (опционально)
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Дата добавления сведений в реестр учредителей ЮЛ / основателей ИП (опционально)
    /// </summary>
    public required string AddedDate { get; set; }

    /// <summary>
    /// Дата обновления сведений в реестре учредителей ЮЛ / основателей ИП (опционально)
    /// </summary>
    public required string UpdateDate { get; set; }

    /// <summary>
    /// Идентификаторы и названия ЮЛ / ИП, основанных учредителем
    /// </summary>
    public required List<string> Clients { get; set; }
}