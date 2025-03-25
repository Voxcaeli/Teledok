namespace Teledok.Persistence.DTO.ClientDTO.Commands;

/// <summary>
/// Создание нового клиента
/// </summary>
public class CreateClient
{
    /// <summary>
    /// Название клиента
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Тип клиента
    /// </summary>
    public required int Type { get; set; }

    /// <summary>
    /// ИНН клиента
    /// </summary>
    public required string TIN { get; set; }

    /// <summary>
    /// Список идентификаторов учредителей ЮЛ / основателей ИП
    /// </summary>
    public required List<int> Founders { get; set; }
}