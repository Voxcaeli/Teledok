using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Teledok.Persistence.DTO.ClientDTO.Commands;
using Teledok.Persistence.DTO.ClientDTO.Queries;
using Teledok.Persistence.Repositories.Interfaces;

namespace Teledok.WebAPI.Controllers.V1;

/// <summary>
/// Контроллер клиента
/// </summary>
/// <param name="repositoryClient">Репозиторий клиента</param>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ClientController(IRepositoryClient repositoryClient)
    : ControllerBase
{
    /// <summary>
    /// Асинхронное получение клиентов
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /api/clients
    /// </remarks>
    /// <returns>Полный список клиентов</returns>
    /// <response code="200">Успешно</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<BriefClient>> GetAllAsync()
    {
        var clients = await repositoryClient.GetAllAsync();

        return clients;
    }

    /// <summary>
    /// Асинхронное получение диапазона клиентов
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /api/clients
    /// </remarks>
    /// <param name="countSkip">Количество пропускаемых клиентов</param>
    /// <param name="countTake">Количество отображаемых клиентов</param>
    /// <returns>Диапазон клиентов</returns>
    /// <response code="200">Успешно</response>
    [HttpGet("{countSkip}/{countTake}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<BriefClient>> GetRangeAsync(int countSkip, int countTake)
    {
        var rangeClients = await repositoryClient.GetRangeAsync(countSkip, countTake);

        return rangeClients;
    }

    /// <summary>
    /// Асинхронное получение данных клиента
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET /api/clients/5
    /// </remarks>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Полная информация о клиенте</returns>
    /// <response code="200">Успешно</response>
    /// <response code="404">Клиент не найден</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<DetailsClient> GetDetailsAsync(int id)
    {
        var client = await repositoryClient.GetDetailsAsync(id);

        return client;
    }

    /// <summary>
    /// Асинхронное добавление нового клиента
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// POST /api/clients
    /// {
    ///     name: "Название юридического лица / название ИП",
    ///     type: "Тип клиента",
    ///     TIN: "ИНН клиента",
    ///     founder list: "Список учредителей юридического лица / основатель ИП (единолично в списке)"
    /// }
    /// </remarks>
    /// <param name="client">Добавляемый клиент</param>
    /// <returns>Идентификатор добавленного клиента</returns>
    /// <response code="200">Успешно</response>
    /// <response code="400">
    /// Название юридического лица / ИП не введено,
    /// Тип клиента не введён,
    /// ИНН клиента не введено
    /// </response>
    [HttpPost]
    public async Task<int> AddAsync([FromBody] CreateClient client)
    {
        var id = await repositoryClient.AddAsync(client);

        return id;
    }

    /// <summary>
    /// Асинхронное изменение клиента
    /// </summary>
    /// <param name="client">Изменяемый клиент</param>
    /// <response code="200">Успешно</response>
    /// <response code="404">Клиент не найден</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task UpdateAsync([FromBody] UpdateClient client)
    {
        await repositoryClient.UpdateAsync(client);
    }

    /// <summary>
    /// Асинхронное удаление клиента
    /// </summary>
    /// <param name="id">Идентификатор удаляемого клиента</param>
    /// <response code="200">Успешно</response>
    /// <response code="404">Клиент не найден</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task DeleteAsync(int id)
    {
        await repositoryClient.DeleteAsync(id);
    }
}