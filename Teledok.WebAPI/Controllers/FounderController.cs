using Microsoft.AspNetCore.Mvc;
using Teledok.Persistence.DTO.FounderDTO.Commands;
using Teledok.Persistence.DTO.FounderDTO.Queries;
using Teledok.Persistence.Repositories.Interfaces;

namespace Teledok.WebAPI.Controllers;

/// <summary>
/// Контроллер учредителя ЮЛ / основателя ИП
/// </summary>
/// <param name="repositoryFounder">Репозиторий учредителя ЮЛ / основателя ИП</param>
[ApiController]
[Route("[controller]")]
public class FounderController(IRepositoryFounder repositoryFounder)
    : ControllerBase
{
    /// <summary>
    /// Асинхронное получение полного списка учредителей ЮЛ и основателей ИП
    /// </summary>
    /// <returns>Полный список учредителей ЮЛ и основателей ИП</returns>
    [HttpGet]
    public async Task<List<BriefFounder>> GetAllAsync()
    {
        var founders = await repositoryFounder.GetAllAsync();

        return founders;
    }

    /// <summary>
    /// Асинхронное получение диапазона учредителей ЮЛ и основателей ИП
    /// </summary>
    /// <param name="countSkip">Количество пропускаемых учредителей ЮЛ и основателей ИП</param>
    /// <param name="countTake">Количество отображаемых учредителей ЮЛ и основателей ИП</param>
    /// <returns>Диапазон учредителей ЮЛ и основателей ИП</returns>
    [HttpGet("{countSkip}/{countTake}")]
    public async Task<List<BriefFounder>> GetRangeAsync(int countSkip, int countTake)
    {
        var rangeFounders = await repositoryFounder.GetRangeAsync(countSkip, countTake);

        return rangeFounders;
    }

    /// <summary>
    /// Асинхронное получение данных учредителя ЮЛ / основателя ИП
    /// </summary>
    /// <param name="id">Идентификатор учредителя ЮЛ / основателя ИП</param>
    /// <returns>Полная информация об учредителе ЮЛ / основателе ИП</returns>
    [HttpGet("{id}")]
    public async Task<DetailsFounder> GetDetailsAsync(int id)
    {
        var founder = await repositoryFounder.GetDetailsAsync(id);

        return founder;
    }

    /// <summary>
    /// Добавление нового учредителя ЮЛ / основателя ИП
    /// </summary>
    /// <param name="founder">Добавляемый учредитель ЮЛ / основатель ИП</param>
    /// <returns>Идентификатор добавленного учредителя ЮЛ / основателя ИП</returns>
    [HttpPost]
    public async Task<int> AddAsync([FromBody] CreateFounder founder)
    {
        var id = await repositoryFounder.AddAsync(founder);

        return id;
    }

    /// <summary>
    /// Асинхронное изменение учредителя ЮЛ / основателя ИП
    /// </summary>
    /// <param name="founder">Изменяемый учредитель ЮЛ / основатель ИП</param>
    [HttpPut]
    public async Task UpdateAsync([FromBody] UpdateFounder founder)
    {
        await repositoryFounder.UpdateAsync(founder);
    }

    /// <summary>
    /// Асинхронное удаление учредителя ЮЛ / основателя ИП
    /// </summary>
    /// <param name="id">Идентификатор удаляемого учредителя ЮЛ / основателя ИП</param>
    [HttpDelete]
    public async Task DeleteAsync(int id)
    {
        await repositoryFounder.DeleteAsync(id);
    }
}