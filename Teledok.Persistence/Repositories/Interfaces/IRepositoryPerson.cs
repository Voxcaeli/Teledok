using Teledok.Persistence.DTO.PersonDTO.Commands;
using Teledok.Persistence.DTO.PersonDTO.Queries;

namespace Teledok.Persistence.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория персональных данных
/// </summary>
public interface IRepositoryPerson
{
    /// <summary>
    /// Получение полного списка блоков с краткой информацией о персональных данных физических лиц
    /// </summary>
    /// <returns>Полный список блоков с краткой информацией о персональных данных физических лиц</returns>
    Task<List<BriefPerson>> GetAllAsync();

    /// <summary>
    /// Получение диапазона блоков с краткой информацией о персональных данных физических лиц
    /// </summary>
    /// <param name="countSkip">Количество пропускаемых блоков с персональной информацией</param>
    /// <param name="countTake">Количество отображаемых блоков с персональной информацией</param>
    /// <returns>Диапазон блоков с краткой информацией о персональных данных физических лиц</returns>
    Task<List<BriefPerson>> GetRangeAsync(int countSkip, int countTake);

    /// <summary>
    /// Получение списка с полной информацией о персональных данных физического лица
    /// </summary>
    /// <param name="id">Идентификатор получаемого блока персональных данных</param>
    /// <returns>Список с полной информацией о персональных данных физического лица</returns>
    Task<DetailsPerson> GetDetailsAsync(int id);

    /// <summary>
    /// Добавление новых персональных данных
    /// </summary>
    /// <param name="person">Добавляемые персональные данные физического лица</param>
    /// <returns>Идентификатор добавленного блока персональных данных</returns>
    Task<int> AddAsync(CreatePerson person);

    /// <summary>
    /// Обновление персональных данных
    /// </summary>
    /// <param name="person">Блок персональных данных</param>
    Task UpdateAsync(UpdatePerson person);

    /// <summary>
    /// Удаление блока персональных данных
    /// </summary>
    /// <param name="id">Идентификатор удаляемого блока персональных данных</param>
    Task DeleteAsync(int id);
}