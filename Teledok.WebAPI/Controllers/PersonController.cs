using Microsoft.AspNetCore.Mvc;
using Teledok.Persistence.DTO.PersonDTO.Commands;
using Teledok.Persistence.DTO.PersonDTO.Queries;
using Teledok.Persistence.Repositories.Interfaces;

namespace Teledok.WebAPI.Controllers;

/// <summary>
/// Контроллер персональных данных
/// </summary>
/// <param name="repositoryPerson">Репозиторий персональных данных</param>
[ApiController]
[Route("[controller]")]
public class PersonController(IRepositoryPerson repositoryPerson)
    : ControllerBase
{
    /// <summary>
    /// Асинхронное получение полного списка блоков с краткой информацией о персональных данных физических лиц
    /// </summary>
    /// <returns>Полный список блоков с краткой информацией о персональных данных физических лиц</returns>
    [HttpGet]
    public async Task<List<BriefPerson>> GetAllAsync()
    {
        var people = await repositoryPerson.GetAllAsync();

        return people;
    }

    /// <summary>
    /// Асинхронное получение диапазона блоков с краткой информацией о персональных данных физических лиц
    /// </summary>
    /// <param name="countSkip">Количество пропускаемых блоков с персональной информацией</param>
    /// <param name="countTake">Количество отображаемых блоков с персональной информацией</param>
    /// <returns>Диапазон блоков с краткой информацией о персональных данных физических лиц</returns>
    [HttpGet("{countSkip}/{countTake}")]
    public async Task<List<BriefPerson>> GetRangeAsync(int countSkip, int countTake)
    {
        var rangePeople = await repositoryPerson.GetRangeAsync(countSkip, countTake);

        return rangePeople;
    }

    /// <summary>
    /// Асинхронное получение списка с полной информацией о персональных данных физического лица
    /// </summary>
    /// <param name="id">Идентификатор получаемого блока персональных данных</param>
    /// <returns>Полная информация о персональных данных физического лица</returns>
    [HttpGet("{id}")]
    public async Task<DetailsPerson> GetDetailsAsync(int id)
    {
        var detailsPerson = await repositoryPerson.GetDetailsAsync(id);

        return detailsPerson;
    }

    /// <summary>
    /// Асинхронное добавление новых персональных данных
    /// </summary>
    /// <param name="person">Добавляемые персональные данные физического лица</param>
    /// <returns>Идентификатор добавленного блока персональных данных</returns>
    [HttpPost]
    public async Task<int> AddAsync([FromBody] CreatePerson person)
    {
        var id = await repositoryPerson.AddAsync(person);

        return id;
    }

    /// <summary>
    /// Асинхронное изменение персональных данных
    /// </summary>
    /// <param name="person">Персональные данные физического лица</param>
    [HttpPut]
    public async Task UpdateAsync([FromBody] UpdatePerson person)
    {
        await repositoryPerson.UpdateAsync(person);
    }

    /// <summary>
    /// Асинхронное удаление блока персональных данных
    /// </summary>
    /// <param name="id">Идентификатор удаляемого блока персональных данных</param>
    [HttpDelete]
    public async Task DeleteAsync(int id)
    {
        await repositoryPerson.DeleteAsync(id);
    }
}