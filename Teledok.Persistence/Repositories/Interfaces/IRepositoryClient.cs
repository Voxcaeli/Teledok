using Teledok.Persistence.DTO.ClientDTO.Commands;
using Teledok.Persistence.DTO.ClientDTO.Queries;

namespace Teledok.Persistence.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория клиента
/// </summary>
public interface IRepositoryClient
{
    /// <summary>
    /// Получение краткой информации обо всех клиентах
    /// </summary>
    /// <returns>Список клиентов</returns>
    Task<List<BriefClient>> GetAllAsync();

    /// <summary>
    /// Получение диапазона с краткой информацией обо всех клиентах
    /// </summary>
    /// <param name="countSkip">Количество пропускаемых клиентов</param>
    /// <param name="countTake">Количество отображаемых клиентов</param>
    /// <returns>Диапазон с краткой информацией обо всех клиентах</returns>
    Task<List<BriefClient>> GetRangeAsync(int countSkip, int countTake);

    /// <summary>
    /// Получение полной информации о клиенте
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Полная информация о клиенте</returns>
    Task<DetailsClient> GetDetailsAsync(int id);

    /// <summary>
    /// Добавление нового клиента
    /// </summary>
    /// <param name="client">Новый клиент</param>
    /// <returns>Идентификатор добавленного клиента</returns>
    Task<int> AddAsync(CreateClient client);

    /// <summary>
    /// Обновление данных клиента
    /// </summary>
    /// <param name="person">Изменяемый клиент</param>
    Task UpdateAsync(UpdateClient founder);

    /// <summary>
    /// Удаление клиента
    /// </summary>
    /// <param name="id">Идентификатор удалённого клиента</param>
    Task DeleteAsync(int id);
}