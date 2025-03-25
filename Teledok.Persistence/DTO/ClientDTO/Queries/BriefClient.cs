namespace Teledok.Persistence.DTO.ClientDTO.Queries;

/// <summary>
/// Краткие данные клиента
/// </summary>
public class BriefClient
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
}