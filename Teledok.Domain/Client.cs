namespace Teledok.Domain;

/// <summary>
/// Тип клиента
/// </summary>
public enum ClientType
{
    /// <summary>
    /// Индивидуальный предприниматель
    /// </summary>
    IndividualEntrepreneur,
    /// <summary>
    /// Юридическое лицо
    /// </summary>
    LegalEntity
}

/// <summary>
/// Клиент
/// </summary>
public class Client
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название клиента
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Тип клиента
    /// </summary>
    public required ClientType Type { get; set; }

    /// <summary>
    /// ИНН клиента
    /// </summary>
    public required string TIN { get; set; }

    /// <summary>
    /// Дата добавления сведений в ЕГРЮЛ/ЕГРИП
    /// </summary>
    public required DateOnly AddedDate { get; set; }

    /// <summary>
    /// Дата обновления сведений в ЕГРЮЛ/ЕГРИП
    /// </summary>
    public required DateOnly UpdateDate { get; set; }

    /// <summary>
    /// Список учредителей юридических лиц / основателя ИП
    /// </summary>
    public required List<Founder> Founders { get; set; }
}