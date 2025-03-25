namespace Teledok.Domain;

/// <summary>
/// Учредитель ЮЛ / Основатель ИП
/// </summary>
public class Founder
{
    /// <summary>
    /// Идентификатор учредителя ЮЛ / основателя ИП
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор персональных данных учредителя ЮЛ / основателя ИП
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// Персональные данные учредителя ЮЛ / основателя ИП
    /// </summary>
    public required Person Person { get; set; }

    /// <summary>
    /// Дата добавления сведений в реестр учредителей ЮЛ / основателей ИП
    /// </summary>
    public required DateOnly AddedDate { get; set; }

    /// <summary>
    /// Дата обновления сведений в реестре учредителей ЮЛ / основателей ИП
    /// </summary>
    public required DateOnly UpdateDate { get; set; }

    /// <summary>
    /// Список юридических лиц, основанных учредителем / ИП, основанный физическим лицом
    /// </summary>
    public required List<Client> Clients { get; set; }
}