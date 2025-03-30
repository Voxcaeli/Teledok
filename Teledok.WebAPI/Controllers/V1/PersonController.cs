using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Teledok.Persistence.DTO.PersonDTO.Commands;
using Teledok.Persistence.DTO.PersonDTO.Queries;
using Teledok.Persistence.Repositories.Interfaces;

namespace Teledok.WebAPI.Controllers.V1;

/// <summary>
/// Контроллер персональных данных
/// </summary>
/// <param name="repositoryPerson">Репозиторий персональных данных</param>
[ApiController]
[ApiVersion("1.0")]
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

    /// <summary>
    /// Асинхронное добавление новых персональных данных
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// POST /api/people
    /// {
    ///     name: "Имя",
    ///     surname: "Фамилия",
    ///     surname: "Отчество",
    ///     surname: "Дата рождения (в формате год-месяц-день)",
    ///     surname: "Адрес"
    /// }
    /// </remarks>
    /// <param name="person">Добавляемые персональные данные физического лица</param>
    /// <returns>Идентификатор добавленного блока персональных данных</returns>
    /// <response code="200">Успешно</response>
    /// <response code="400">
    /// Имя не введено,
    /// Фамилия не введена
    /// </response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<int> AddAsync([FromBody] CreatePerson person)
    {
        var id = await repositoryPerson.AddAsync(person);

        return id;
    }

    /// <summary>
    /// Асинхронное изменение персональных данных
    /// </summary>
    /// <param name="person">Персональные данные физического лица</param>
    /// <response code="200">Успешно</response>
    /// <response code="404">Блок с персональными данными не найден</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task UpdateAsync([FromBody] UpdatePerson person)
    {
        await repositoryPerson.UpdateAsync(person);
    }

    /// <summary>
    /// Асинхронное удаление блока персональных данных
    /// </summary>
    /// <param name="id">Идентификатор удаляемого блока персональных данных</param>
    /// <response code="200">Успешно</response>
    /// <response code="404">Блок с персональными данными не найден</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task DeleteAsync(int id)
    {
        await repositoryPerson.DeleteAsync(id);
    }
}