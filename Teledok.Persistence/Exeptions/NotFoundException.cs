namespace Teledok.Persistence.Exeptions;

/// <summary>
/// Ошибка отсутствия данных
/// </summary>
/// <param name="name">Название объекта, где произошла ошибка</param>
/// <param name="key">Объект ключа</param>
public class NotFoundException(string name, object key)
    : Exception(message: $"Entity \"{name}\" ({key} not found)")
{ }