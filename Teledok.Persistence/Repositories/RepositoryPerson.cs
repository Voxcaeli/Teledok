using Microsoft.EntityFrameworkCore;
using Teledok.Domain;
using Teledok.Persistence.DBContext;
using Teledok.Persistence.DTO.PersonDTO.Commands;
using Teledok.Persistence.DTO.PersonDTO.Queries;
using Teledok.Persistence.Exeptions;
using Teledok.Persistence.Helpers;
using Teledok.Persistence.Repositories.Interfaces;

namespace Teledok.Persistence.Repositories;

/// <summary>
/// Репозиторий персональных данных
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class RepositoryPerson(TeledokDBContext context)
    : IRepositoryPerson
{
    /// <summary>
    /// Асинхронное получение полного списка блоков с краткой информацией о персональных данных физических лиц
    /// </summary>
    /// <returns>Полный список блоков с краткой информацией о персональных данных физических лиц</returns>
    public async Task<List<BriefPerson>> GetAllAsync()
    {
        var persons = await context.People
            .Select(person => new BriefPerson
            {
                Id = person.Id,
                FullName = Helper.GetFullName(person),
                Birthday = person.Birthday.ToShortDateString()
            })
            .AsNoTracking()
            .ToListAsync();

        return persons;
    }

    /// <summary>
    /// Асинхронное получение диапазона блоков с краткой информацией о персональных данных физических лиц
    /// </summary>
    /// <param name="countSkip">Количество пропускаемых блоков с персональной информацией</param>
    /// <param name="countTake">Количество отображаемых блоков с персональной информацией</param>
    /// <returns>Диапазон блоков с краткой информацией о персональных данных физических лиц</returns>
    public async Task<List<BriefPerson>> GetRangeAsync(int countSkip, int countTake)
    {
        var persons = await context.People
            .Select(person => new BriefPerson
            {
                Id = person.Id,
                FullName = Helper.GetFullName(person),
                Birthday = person.Birthday.ToShortDateString()
            })
            .AsNoTracking()
            .Skip(countSkip)
            .Take(countTake)
            .ToListAsync();

        return persons;
    }

    /// <summary>
    /// Асинхронное получение списка с полной информацией о персональных данных физического лица
    /// </summary>
    /// <param name="id">Идентификатор получаемого блока персональных данных</param>
    /// <returns>Список с полной информацией о персональных данных физического лица</returns>
    public async Task<DetailsPerson> GetDetailsAsync(int id)
    {
        var person = await context.People
            .Select(person => new DetailsPerson
            {
                Id = person.Id,
                Surname = person.Surname,
                Name = person.Name,
                Patronomic = person.Patronomic,
                FullName = Helper.GetFullName(person),
                ShortName = Helper.GetShortName(person),
                Birthday = person.Birthday.ToShortDateString(),
                Address = person.Address
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(person => person.Id == id)
            ?? throw new NotFoundException(nameof(Person), id);

        return person;
    }

    /// <summary>
    /// Асинхронное добавление новых персональных данных
    /// </summary>
    /// <param name="person">Добавляемые персональные данные физического лица</param>
    /// <returns>Идентификатор добавленного блока персональных данных</returns>
    public async Task<int> AddAsync(CreatePerson person)
    {
        var newPerson = new Person
        {
            Name = person.Name,
            Surname = person.Surname,
            Patronomic = person.Patronomic ?? string.Empty,
            Birthday = person.Birthday,
            Address = person.Address ?? string.Empty
        };

        await context.AddAsync(newPerson);
        await context.SaveChangesAsync();

        return newPerson.Id;
    }

    /// <summary>
    /// Асинхронное обновление персональных данных
    /// </summary>
    /// <param name="person">Блок персональных данных</param>
    public async Task UpdateAsync(UpdatePerson person)
    {
        var updatedPerson = await GetByIdAsync(person.Id);

        if (person.Name != string.Empty)
        {
            updatedPerson.Name = person.Name!;
        }

        if (person.Surname != string.Empty)
        {
            updatedPerson.Surname = person.Surname!;
        }

        updatedPerson.Patronomic = person.Patronomic;
        updatedPerson.Birthday = person.Birthday;
        updatedPerson.Address = person.Address;

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Асинхронное удаление блока персональных данных
    /// </summary>
    /// <param name="id">Идентификатор удаляемого блока персональных данных</param>
    public async Task DeleteAsync(int id)
    {
        var person = await GetByIdAsync(id);

        context.People.Remove(person);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Асинхронное получение блока персональных данных
    /// </summary>
    /// <param name="id">Идентификатор блока персональных данных</param>
    /// <returns>Объект персональных данных</returns>
    /// <exception cref="NotFoundException"></exception>
    private async Task<Person> GetByIdAsync(int id)
    {
        var person = await context.People
            .SingleOrDefaultAsync(person => person.Id == id)
            ?? throw new NotFoundException(nameof(Person), id);

        return person;
    }
}