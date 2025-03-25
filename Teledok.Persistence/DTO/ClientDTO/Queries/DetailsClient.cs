namespace Teledok.Persistence.DTO.ClientDTO.Queries;

/// <summary>
/// Подробные сведения о клиенте
/// </summary>
public class DetailsClient
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название клиента
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Тип клиента
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// ИНН клиента
    /// </summary>
    public required string TIN { get; set; }

    /// <summary>
    /// Дата добавления сведений в ЕГРЮЛ/ЕГРИП
    /// </summary>
    public required string AddedDate { get; set; }

    /// <summary>
    /// Дата обновления сведений в ЕГРЮЛ/ЕГРИП
    /// </summary>
    public required string UpdateDate { get; set; }

    /// <summary>
    /// Идентификаторы и названия учредителей ЮЛ / основателя ИП
    /// </summary>
    public required List<string> Founders { get; set; }
}