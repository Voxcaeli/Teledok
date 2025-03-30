using Microsoft.EntityFrameworkCore;
using Teledok.Domain;
using Teledok.Persistence.DBContext;
using Teledok.Persistence.DTO.FounderDTO.Commands;
using Teledok.Persistence.DTO.FounderDTO.Queries;
using Teledok.Persistence.Exeptions;
using Teledok.Persistence.Helpers;
using Teledok.Persistence.Repositories.Interfaces;

namespace Teledok.Persistence.Repositories;

/// <summary>
/// Репозиторий учредителя ЮЛ / основателя ИП
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class RepositoryFounder(TeledokDBContext context)
    : IRepositoryFounder
{
    /// <summary>
    /// Асинхронное получение краткой информации обо всех учредителях ЮЛ и осноателей ИП
    /// </summary>
    /// <returns>Список учредителей ЮЛ и основателей ИП</returns>
    public async Task<List<BriefFounder>> GetAllAsync()
    {
        var founders = await context.Founders
            .Select(founder => new BriefFounder
            {
                Id = founder.Id,
                Name = Helper.GetFullName(founder.Person),
                Birthday = founder.Person.Birthday.GetValueOrDefault().ToShortDateString()
            })
            .AsNoTracking()
            .ToListAsync();

        return founders;
    }

    /// <summary>
    /// Асинхронное получение диапазона с краткой информацией обо всех учредителях ЮЛ и осноателей ИП
    /// </summary>
    /// <param name="countSkip">Количество пропускаемых блоков с информацией обо всех учредителях ЮЛ и осноателей ИП</param>
    /// <param name="countTake">Количество отображаемых блоков с информацией обо всех учредителях ЮЛ и осноателей ИП</param>
    /// <returns>Диапазон с краткой информацией обо всех учредителях ЮЛ и осноателей ИП</returns>
    public async Task<List<BriefFounder>> GetRangeAsync(int countSkip, int countTake)
    {
        var founders = await context.Founders
            .Select(founder => new BriefFounder
            {
                Id = founder.Id,
                Name = Helper.GetFullName(founder.Person),
                Birthday = founder.Person.Birthday.GetValueOrDefault().ToShortDateString()
            })
            .AsNoTracking()
            .Skip(countSkip)
            .Take(countTake)
            .ToListAsync();

        return founders;
    }

    /// <summary>
    /// Асинхронное получение полной информации об учредителе ЮЛ / основателе ИП по его идентификатору
    /// </summary>
    /// <param name="id">Идентификатор учредителя ЮЛ / основателе ИП</param>
    /// <returns>Информация об учредителе ЮЛ / основателе ИП</returns>
    public async Task<DetailsFounder> GetDetailsAsync(int id)
    {
        var clientList = new List<string>();

        var founderClients = await context.Founders
            .Where(founder => founder.Id == id)
            .Select(founder => founder.Clients)
            .AsNoTracking()
            .SingleOrDefaultAsync();

        if (founderClients != null)
        {
            foreach (var client in founderClients)
            {
                clientList.Add($"{client.Name} (id = {client.Id})");
            }
        }

        var founder = await context.Founders
            .Select(founder => new DetailsFounder
            {
                Id = founder.Id,
                PersonId = founder.PersonId,
                FullName = Helper.GetFullName(founder.Person),
                ShortName = Helper.GetShortName(founder.Person),
                Birthday = founder.Person.Birthday.GetValueOrDefault().ToShortDateString(),
                Address = founder.Person.Address,
                AddedDate = founder.AddedDate.ToShortDateString(),
                UpdateDate = founder.UpdateDate.ToShortDateString(),
                Clients = clientList
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(founder => founder.Id == id)
            ?? throw new NotFoundException(nameof(Person), id);

        return founder;
    }

    /// <summary>
    /// Асинхронное добавление нового учредителя ЮЛ / основателя ИП
    /// </summary>
    /// <param name="founder">Добавляемый учредитель ЮЛ / основатель ИП</param>
    /// <returns>Идентификатор добавленного учредителя ЮЛ / основателя ИП</returns>
    public async Task<int> AddAsync(CreateFounder founder)
    {
        var person = await Helper.GetPersonByIdAsync(context, founder.PersonId);
        List<Client> clients = [];

        await context.Clients.ForEachAsync(client =>
            founder.Clients!.ForEach(clientId => {
                if (client.Id == clientId)
                {
                    clients.Add(client);
                }
            }));

        var newFounder = new Founder
        {
            PersonId = founder.PersonId,
            Person = person!,
            AddedDate = DateOnly.FromDateTime(DateTime.Today),
            UpdateDate = DateOnly.FromDateTime(DateTime.Today),
            Clients = clients
        };

        await context.AddAsync(newFounder);
        await context.SaveChangesAsync();

        return newFounder.Id;
    }

    /// <summary>
    /// Асинхронное обновление данных учредителя ЮЛ / основателя ИП
    /// </summary>
    /// <param name="person">Изменяемый учредитель ЮЛ / основатель ИП</param>
    public async Task UpdateAsync(UpdateFounder founder)
    {
        var updatedFounder = await GetByIdAsync(founder.Id);

        if (updatedFounder.Clients is null)
        {
            updatedFounder.Clients = [];
        }

        if (founder.PersonId is not null)
        {
            updatedFounder.PersonId = founder.PersonId.Value;
            
            var person = await Helper.GetPersonByIdAsync(context, founder.PersonId!.Value);
            updatedFounder.Person = person!;
        }

        // Добавляемые ЮЛ к учредителю / ИП к основателю 
        var addedClients = new List<Client>();

        await context.Clients
            .ForEachAsync(client => {
                founder.Clients!.ForEach(clientId => {
                    if (client.Id == clientId)
                    {
                        addedClients.Add(client);
                    }
                });
            });

        // Проверка наличия ЮЛ у учредителя / ИП у основателя и добавление при их отсутствии
        addedClients.ForEach(addedClient => {
            if (!updatedFounder.Clients.Contains(addedClient))
            {
                updatedFounder.Clients.Add(addedClient);
            }
        });

        // Изменение даты обновления сведений в реестре учредителей ЮЛ / основателей ИП
        updatedFounder.UpdateDate = DateOnly.FromDateTime(DateTime.Today);

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Асинхронное удаление учредителя ЮЛ / основателя ИП
    /// </summary>
    /// <param name="id">Идентификатор учредителя ЮЛ / основателя ИП</param>
    public async Task DeleteAsync(int id)
    {
        var founder = await GetByIdAsync(id);

        context.Founders.Remove(founder);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Асинхронное получение учредителя ЮЛ / основателя ИП
    /// </summary>
    /// <param name="id">Идентификатор учредителя ЮЛ / основателя ИП</param>
    /// <returns>Объект учредителя ЮЛ / основателя ИП</returns>
    /// <exception cref="NotFoundException"></exception>
    private async Task<Founder> GetByIdAsync(int id)
    {
        var founder = await context.Founders
            .SingleOrDefaultAsync(founder => founder.Id == id)
            ?? throw new NotFoundException(nameof(Founder), id);

        return founder;
    }
}