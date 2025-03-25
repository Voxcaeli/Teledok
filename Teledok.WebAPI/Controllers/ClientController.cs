using Microsoft.AspNetCore.Mvc;
using Teledok.Persistence.DTO.ClientDTO.Commands;
using Teledok.Persistence.DTO.ClientDTO.Queries;
using Teledok.Persistence.Repositories.Interfaces;

namespace Teledok.WebAPI.Controllers;

/// <summary>
/// Контроллер клиента
/// </summary>
/// <param name="repositoryClient">Репозиторий клиента</param>
[ApiController]
[Route("[controller]")]
public class ClientController(IRepositoryClient repositoryClient)
    : ControllerBase
{
    /// <summary>
    /// Асинхронное получение клиентов
    /// </summary>
    /// <returns>Полный список клиентов</returns>
    [HttpGet]
    public async Task<List<BriefClient>> GetAllAsync()
    {
        var clients = await repositoryClient.GetAllAsync();

        return clients;
    }

    /// <summary>
    /// Асинхронное получение диапазона клиентов
    /// </summary>
    /// <param name="countSkip">Количество пропускаемых клиентов</param>
    /// <param name="countTake">Количество отображаемых клиентов</param>
    /// <returns>Диапазон клиентов</returns>
    [HttpGet("{countSkip}/{countTake}")]
    public async Task<List<BriefClient>> GetRangeAsync(int countSkip, int countTake)
    {
        var rangeClients = await repositoryClient.GetRangeAsync(countSkip, countTake);

        return rangeClients;
    }

    /// <summary>
    /// Асинхронное получение данных клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Полная информация о клиенте</returns>
    [HttpGet("{id}")]
    public async Task<DetailsClient> GetDetailsAsync(int id)
    {
        var client = await repositoryClient.GetDetailsAsync(id);

        return client;
    }

    /// <summary>
    /// Добавление нового клиента
    /// </summary>
    /// <param name="client">Добавляемый клиент</param>
    /// <returns>Идентификатор добавленного клиента</returns>
    [HttpPost]
    public async Task<int> AddAsync([FromBody] CreateClient client)
    {
        var id = await repositoryClient.AddAsync(client);

        return id;
    }

    /// <summary>
    /// Асинхронное изменение клиента
    /// </summary>
    /// <param name="client">Изменяемый клиента</param>
    [HttpPut]
    public async Task UpdateAsync([FromBody] UpdateClient client)
    {
        await repositoryClient.UpdateAsync(client);
    }

    /// <summary>
    /// Асинхронное удаление клиента
    /// </summary>
    /// <param name="id">Идентификатор удаляемого клиента</param>
    [HttpDelete]
    public async Task DeleteAsync(int id)
    {
        await repositoryClient.DeleteAsync(id);
    }
}