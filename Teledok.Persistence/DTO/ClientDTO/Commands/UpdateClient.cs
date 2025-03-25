namespace Teledok.Persistence.DTO.ClientDTO.Commands;

/// <summary>
/// Обновление данных клиента
/// </summary>
public class UpdateClient
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название клиента (опционально)
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Тип клиента (опционально)
    /// </summary>
    public int? Type { get; set; }

    /// <summary>
    /// ИНН клиента (опционально)
    /// </summary>
    public string? TIN { get; set; }

    /// <summary>
    /// Список идентификаторов учредителей ЮЛ / основателей ИП (опционально)
    /// </summary>
    public List<int>? Founders { get; set; }
}