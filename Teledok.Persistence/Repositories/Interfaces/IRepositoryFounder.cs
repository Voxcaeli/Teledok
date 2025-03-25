using Teledok.Persistence.DTO.FounderDTO.Commands;
using Teledok.Persistence.DTO.FounderDTO.Queries;

namespace Teledok.Persistence.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория учредителя ЮЛ / основателя ИП
/// </summary>
public interface IRepositoryFounder
{
    /// <summary>
    /// Получение краткой информации обо всех учредителях ЮЛ и осноателей ИП
    /// </summary>
    /// <returns>Список учредителей ЮЛ и основателей ИП</returns>
    Task<List<BriefFounder>> GetAllAsync();

    /// <summary>
    /// Получение диапазона с краткой информацией обо всех учредителях ЮЛ и осноателей ИП
    /// </summary>
    /// <param name="countSkip">Количество пропускаемых блоков с информацией обо всех учредителях ЮЛ и осноателей ИП</param>
    /// <param name="countTake">Количество отображаемых блоков с информацией обо всех учредителях ЮЛ и осноателей ИП</param>
    /// <returns>Диапазон с краткой информацией обо всех учредителях ЮЛ и осноателей ИП</returns>
    Task<List<BriefFounder>> GetRangeAsync(int countSkip, int countTake);

    /// <summary>
    /// Получение полной информации об учредителе ЮЛ / основателе ИП по его идентификатору
    /// </summary>
    /// <param name="id">Идентификатор учредителя ЮЛ / основателе ИП</param>
    /// <returns>Информация об учредителе ЮЛ / основателе ИП</returns>
    Task<DetailsFounder> GetDetailsAsync(int id);

    /// <summary>
    /// Добавление нового учредителя ЮЛ / основателя ИП
    /// </summary>
    /// <param name="founder">Добавляемый учредитель ЮЛ / основатель ИП</param>
    /// <returns>Идентификатор добавленного учредителя ЮЛ / основателя ИП</returns>
    Task<int> AddAsync(CreateFounder founder);

    /// <summary>
    /// Обновление данных учредителя ЮЛ / основателя ИП
    /// </summary>
    /// <param name="person">Изменяемый учредитель ЮЛ / основатель ИП</param>
    Task UpdateAsync(UpdateFounder founder);

    /// <summary>
    /// Удаление учредителя ЮЛ / основателя ИП
    /// </summary>
    /// <param name="id">Идентификатор учредителя ЮЛ / основателя ИП</param>
    Task DeleteAsync(int id);
}