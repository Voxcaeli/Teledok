using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Teledok.Persistence.DTO.PersonDTO.Commands;
using Teledok.Persistence.DTO.PersonDTO.Queries;
using Teledok.Persistence.Repositories.Interfaces;

namespace Teledok.WebAPI.Controllers.V2;

/// <summary>
/// Контроллер персональных данных
/// </summary>
/// <param name="repositoryPerson">Репозиторий персональных данных</param>
[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PersonController(IRepositoryPerson repositoryPerson)
    : ControllerBase
{
    /// <summary>
    /// Асинхронное получение полного списка блоков с краткой информацией о персональных данных физических лиц
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /api/people
    /// </remarks>
    /// <returns>Полный список блоков с краткой информацией о персональных данных физических лиц</returns>
    /// <response code="200">Успешно</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<BriefPerson>> GetAllAsync()
    {
        var people = await repositoryPerson.GetAllAsync();

        return people;
    }

    /// <summary>
    /// Асинхронное получение диапазона блоков с краткой информацией о персональных данных физических лиц
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /api/people
    /// </remarks>
    /// <param name="countSkip">Количество пропускаемых блоков с персональной информацией</param>
    /// <param name="countTake">Количество отображаемых блоков с персональной информацией</param>
    /// <returns>Диапазон блоков с краткой информацией о персональных данных физических лиц</returns>
    /// <response code="200">Успешно</response>
    [HttpGet("{countSkip}/{countTake}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<BriefPerson>> GetRangeAsync(int countSkip, int countTake)
    {
        var rangePeople = await repositoryPerson.GetRangeAsync(countSkip, countTake);

        return rangePeople;
    }

    /// <summary>
    /// Асинхронное получение списка с полной информацией о персональных данных физического лица
    /// </summary>
    /// <param name="id">Идентификатор получаемого блока персональных данных</param>
    /// <remarks>
    /// Пример запроса:
    /// GET /api/people/5
    /// </remarks>
    /// <returns>Полная информация о персональных данных физического лица</returns>
    /// <response code="200">Успешно</response>
    /// <response code="404">Персональные данные не найдены</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<DetailsPerson> GetDetailsAsync(int id)
    {
        var detailsPerson = await repositoryPerson.GetDetailsAsync(id);

        return detailsPerson;
    }
}