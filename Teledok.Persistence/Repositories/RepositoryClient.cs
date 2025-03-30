using Microsoft.EntityFrameworkCore;
using Teledok.Domain;
using Teledok.Persistence.DBContext;
using Teledok.Persistence.DTO.ClientDTO.Commands;
using Teledok.Persistence.DTO.ClientDTO.Queries;
using Teledok.Persistence.Exeptions;
using Teledok.Persistence.Helpers;
using Teledok.Persistence.Repositories.Interfaces;

namespace Teledok.Persistence.Repositories;

/// <summary>
/// Репозиторий клиента
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class RepositoryClient(TeledokDBContext context)
    : IRepositoryClient
{
    /// <summary>
    /// Асинхронное получение краткой информации обо всех клиентах
    /// </summary>
    /// <returns>Список клиентов</returns>
    public async Task<List<BriefClient>> GetAllAsync()
    {
        var clients = await context.Clients
            .Select(client => new BriefClient
            {
                Id = client.Id,
                Name = client.Name,
                Type = Helper.GetClientTypeDescription(client.Type)
            })
            .AsNoTracking()
            .ToListAsync();

        return clients;
    }

    /// <summary>
    /// Асинхронное получение диапазона с краткой информацией обо всех клиентах
    /// </summary>
    /// <param name="countSkip">Количество пропускаемых клиентов</param>
    /// <param name="countTake">Количество отображаемых клиентов</param>
    /// <returns>Диапазон с краткой информацией обо всех клиентах</returns>
    public async Task<List<BriefClient>> GetRangeAsync(int countSkip, int countTake)
    {
        var clients = await context.Clients
            .Select(client => new BriefClient
            {
                Id = client.Id,
                Name = client.Name,
                Type = Helper.GetClientTypeDescription(client.Type)
            })
            .AsNoTracking()
            .Skip(countSkip)
            .Take(countTake)
            .ToListAsync();

        return clients;
    }

    /// <summary>
    /// Асинхронное получение полной информации о клиенте
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Полная информация о клиенте</returns>
    public async Task<DetailsClient> GetDetailsAsync(int id)
    {
        // Список строкового описания учредителей ЮЛ / основателя ИП
        var founders = new List<string>();

        // Получение учредителей ЮЛ / основателя ИП
        var clientFounders = await context.Clients
            .Where(client => client.Id == id)
            .Select(client => client.Founders)
            .AsNoTracking()
            .SingleOrDefaultAsync();

        if (clientFounders != null)
        {
            foreach (var founder in clientFounders)
            {
                var person = await Helper.GetPersonByIdAsync(context, founder.PersonId);

                founders.Add($"{Helper.GetFullName(person!)} (id = {founder.Id})");
            }
        }

        var client = await context.Clients
            .Select(client => new DetailsClient
            {
                Id = client.Id,
                Name = client.Name,
                Type = Helper.GetClientTypeDescription(client.Type),
                TIN = client.TIN,
                AddedDate = client.AddedDate.ToShortDateString(),
                UpdateDate = client.UpdateDate.ToShortDateString(),
                Founders = founders
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(client => client.Id == id)
            ?? throw new NotFoundException(nameof(Client), id);

        return client;
    }

    /// <summary>
    /// Асинхронное добавление нового клиента
    /// </summary>
    /// <param name="client">Новый клиент</param>
    /// <returns>Идентификатор добавленного клиента</returns>
    public async Task<int> AddAsync(CreateClient client)
    {
        // Список учредителей ЮЛ / основатель ИП
        List<Founder> founders = [];

        await context.Founders
            .ForEachAsync(founder => {
                client.Founders.ForEach(founderId => {
                    if (founder.Id == founderId)
                    {
                        founders.Add(founder);
                    }
                });
            });

        // Выброс исключения при количестве основателя ИП более 1
        if (client.Type == (int)ClientType.IndividualEntrepreneur
                            && founders.Count > 1)
        {
            throw new LimitOverException();
        }

        var newClient = new Client
        {
            Name = client.Name,
            Type = (ClientType)client.Type,
            TIN = client.TIN,
            AddedDate = DateOnly.FromDateTime(DateTime.Today),
            UpdateDate = DateOnly.FromDateTime(DateTime.Today),
            Founders = founders
        };

        await context.AddAsync(newClient);
        await context.SaveChangesAsync();

        return newClient.Id;
    }

    /// <summary>
    /// Асинхронное обновление данных клиента
    /// </summary>
    /// <param name="person">Изменяемый клиент</param>
    public async Task UpdateAsync(UpdateClient client)
    {
        var updatedClient = await GetByIdAsync(client.Id);

        if (client.Name != string.Empty)
        {
            updatedClient.Name = client.Name!;
        }

        if (client.Type is not null)
        {
            updatedClient.Type = (ClientType)client.Type;
        }

        if (client.TIN != string.Empty)
        {
            updatedClient.TIN = client.TIN!;
        }

        // Добавляемые учредители к ЮЛ / основатель к ИП 
        var addedFounders = new List<Founder>();

        await context.Founders
            .ForEachAsync(founder => {
                client.Founders!.ForEach(founderId => {
                    if (founder.Id == founderId)
                    {
                        addedFounders.Add(founder);
                    }
                });
            });

        // Проверка наличия учредителей у ЮЛ / основателя у ИП и добавление при их отсутствии
        addedFounders.ForEach(addedFounder => {
            if (!updatedClient.Founders.Contains(addedFounder))
            {
                updatedClient.Founders.Add(addedFounder);
            }
        });

        // Изменение даты обновления сведений в ЕГРЮЛ/ЕГРИП
        updatedClient.UpdateDate = DateOnly.FromDateTime(DateTime.Today);

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Асинхронное удаление клиента
    /// </summary>
    /// <param name="id">Идентификатор удалённого клиента</param>
    public async Task DeleteAsync(int id)
    {
        var client = await GetByIdAsync(id);

        context.Clients.Remove(client);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Асинхронное получение клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Объект клиента</returns>
    /// <exception cref="NotFoundException"></exception>
    private async Task<Client> GetByIdAsync(int id)
    {
        var client = await context.Clients
            .SingleOrDefaultAsync(client => client.Id == id)
            ?? throw new NotFoundException(nameof(Client), id);

        return client;
    }
}