using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Teledok.Persistence.DTO.FounderDTO.Commands;
using Teledok.Persistence.DTO.FounderDTO.Queries;
using Teledok.Persistence.Repositories.Interfaces;

namespace Teledok.WebAPI.Controllers.V1;

/// <summary>
/// Контроллер учредителя ЮЛ / основателя ИП
/// </summary>
/// <param name="repositoryFounder">Репозиторий учредителя ЮЛ / основателя ИП</param>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class FounderController(IRepositoryFounder repositoryFounder)
    : ControllerBase
{
    /// <summary>
    /// Асинхронное получение полного списка учредителей ЮЛ и основателей ИП
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /api/founders
    /// </remarks>
    /// <returns>Полный список учредителей ЮЛ и основателей ИП</returns>
    /// <response code="200">Успешно</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<BriefFounder>> GetAllAsync()
    {
        var founders = await repositoryFounder.GetAllAsync();

        return founders;
    }

    /// <summary>
    /// Асинхронное получение диапазона учредителей ЮЛ и основателей ИП
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /api/founders
    /// </remarks>
    /// <param name="countSkip">Количество пропускаемых учредителей ЮЛ и основателей ИП</param>
    /// <param name="countTake">Количество отображаемых учредителей ЮЛ и основателей ИП</param>
    /// <returns>Диапазон учредителей ЮЛ и основателей ИП</returns>
    /// <response code="200">Успешно</response>
    [HttpGet("{countSkip}/{countTake}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<BriefFounder>> GetRangeAsync(int countSkip, int countTake)
    {
        var rangeFounders = await repositoryFounder.GetRangeAsync(countSkip, countTake);

        return rangeFounders;
    }

    /// <summary>
    /// Асинхронное получение данных учредителя ЮЛ / основателя ИП
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /api/founders/5
    /// </remarks>
    /// <param name="id">Идентификатор учредителя ЮЛ / основателя ИП</param>
    /// <returns>Полная информация об учредителе ЮЛ / основателе ИП</returns>
    /// <response code="200">Успешно</response>
    /// <response code="404">Учредитель не найден</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<DetailsFounder> GetDetailsAsync(int id)
    {
        var founder = await repositoryFounder.GetDetailsAsync(id);

        return founder;
    }

    /// <summary>
    /// Асинхронное добавление нового учредителя ЮЛ / основателя ИП
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// POST /api/founders
    /// {
    ///     person id: "Идентификатор блока персональных данных",
    ///     client list: "Список юридических лиц, основанных учредителем / ИП, основанный физическим лицов"
    /// }
    /// </remarks>
    /// <param name="founder">Добавляемый учредитель ЮЛ / основатель ИП</param>
    /// <returns>Идентификатор добавленного учредителя ЮЛ / основателя ИП</returns>
    /// <response code="200">Успешно</response>
    /// <response code="400">Идентификатор блока персональных данных не введён</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<int> AddAsync([FromBody] CreateFounder founder)
    {
        var id = await repositoryFounder.AddAsync(founder);

        return id;
    }

    /// <summary>
    /// Асинхронное изменение учредителя ЮЛ / основателя ИП
    /// </summary>
    /// <param name="founder">Изменяемый учредитель ЮЛ / основатель ИП</param>
    /// <response code="200">Успешно</response>
    /// <response code="404">Учредитель ЮЛ / основатель ИП не найден</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task UpdateAsync([FromBody] UpdateFounder founder)
    {
        await repositoryFounder.UpdateAsync(founder);
    }

    /// <summary>
    /// Асинхронное удаление учредителя ЮЛ / основателя ИП
    /// </summary>
    /// <param name="id">Идентификатор удаляемого учредителя ЮЛ / основателя ИП</param>
    /// <response code="200">Успешно</response>
    /// <response code="404">Учредитель ЮЛ / основатель ИП не найден</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task DeleteAsync(int id)
    {
        await repositoryFounder.DeleteAsync(id);
    }
}