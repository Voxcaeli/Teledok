using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Teledok.Persistence.DTO.ClientDTO.Commands;
using Teledok.Persistence.DTO.ClientDTO.Queries;
using Teledok.Persistence.Repositories.Interfaces;

namespace Teledok.WebAPI.Controllers.V2;

/// <summary>
/// Контроллер клиента
/// </summary>
/// <param name="repositoryClient">Репозиторий клиента</param>
[ApiController]
[ApiVersion("2.0")]
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
}