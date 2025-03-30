using System.Net.Http.Json;
using Teledok.Console.Person;

/// <summary>
/// Класс программы
/// </summary>
internal class Program
{
    /// <summary>
    /// Главная функция программы
    /// </summary>
    /// <param name="args">Список строковых аргументов</param>
    private static async Task Main(string[] args)
    {
        // Список всех блоков с персональными данными физических лиц [GET]
        var responseGetPeople = await new HttpClient().GetAsync("https://localhost:7254/api/v1/Person");
        var people = await responseGetPeople.Content.ReadFromJsonAsync<List<BriefPerson>>();

        foreach (var person in people!)
        {
            Console.WriteLine($"Id: {person.Id}," +
                              $" Name: {person.FullName}," +
                              $" Birthday: {person.Birthday}");
        }



        // Подробные персональные данные физического лица [GET]
        // Ввод: идентификатор выбранного блока персональных данных
        Console.WriteLine($"\nВведите идентификатор блока с персональными данными для их отображения: ");
        int id = int.Parse(Console.ReadLine()!);
        var responseGetPerson = await new HttpClient().GetAsync($"https://localhost:7254/api/v1/Person/{id}");
        var currentPerson = await responseGetPerson.Content.ReadFromJsonAsync<DetailsPerson>();

        Console.WriteLine($"Id: {currentPerson!.Id}," +
                          $"\nSurname: {currentPerson.Surname}," +
                          $"\nName: {currentPerson.Name}," +
                          $"\nPatronomic: {currentPerson.Patronomic}," +
                          $"\nFullName: {currentPerson.FullName}," +
                          $"\nShortName: {currentPerson.ShortName}," +
                          $"\nBirthday: {currentPerson.Birthday}," +
                          $"\nAddress: {currentPerson.Address}");



        // Создание персональных данных [POST]
        Console.WriteLine($"\nСоздание персональных данных");

        // Ввод: фамилия физического лица
        Console.WriteLine($"Введите фамилию: ");
        string createdSurname = Console.ReadLine()!;
        
        // Ввод: имя физического лица
        Console.WriteLine($"Введите имя: ");
        string createdPersonName = Console.ReadLine()!;
        
        // Ввод: отчество физического лица
        Console.WriteLine($"Введите отчество: ");
        string createdPatronomic = Console.ReadLine()!;
        
        // Ввод: дата рождения физического лица
        Console.WriteLine($"Введите дату рождения: ");
        string createdBirthday = Console.ReadLine()!;
        
        // Ввод: адрес физического лица
        Console.WriteLine($"Введите адрес регистрации: ");
        string createdAddress = Console.ReadLine()!;

        var createPerson = new CreatePerson
        {
            Name = createdPersonName,
            Surname = createdSurname,
            Patronomic = createdPatronomic,
            Birthday = createdBirthday,
            Address = createdAddress
        };

        await new HttpClient().PostAsJsonAsync("https://localhost:7254/api/v1/Person", createPerson);
        responseGetPeople = await new HttpClient().GetAsync("https://localhost:7254/api/v1/Person");
        people = await responseGetPeople.Content.ReadFromJsonAsync<List<BriefPerson>>();

        foreach (var person in people!)
        {
            Console.WriteLine($"Id: {person.Id}," +
                              $" Name: {person.FullName}," +
                              $" Birthday: {person.Birthday}");
        }



        // Обновление персональных данных [PUT]
        Console.WriteLine($"\nОбновление персональных данных");
        
        // Ввод: идентификатор обновляемого блока с персонаьными данными
        Console.WriteLine($"Введите идентификатор: ");
        int updatedPersonId = int.Parse(Console.ReadLine()!);
        
        // Ввод: новое имя физического лица
        Console.WriteLine($"Введите новое имя: ");
        string updatedPersonName = Console.ReadLine()!;
        
        // Ввод: новая фамилия физического лица
        Console.WriteLine($"Введите новую фамилию: ");
        string updatedSurname = Console.ReadLine()!;
        
        // Ввод: новое отчество физического лица
        Console.WriteLine($"Введите новое отчество: ");
        string updatedPatronomic = Console.ReadLine()!;
        
        // Ввод: новый день рождения физического лица
        Console.WriteLine($"Введите новый день рождения: ");
        string updatedBirthday = Console.ReadLine()!;
        
        // Ввод: новый адрес физического лица
        Console.WriteLine($"Введите новый адрес: ");
        string updatedAddress = Console.ReadLine()!;

        var updatedPerson = new UpdatePerson
        {
            Id = updatedPersonId,
            Name = updatedPersonName,
            Surname = updatedSurname,
            Patronomic = updatedPatronomic,
            Birthday = updatedBirthday,
            Address = updatedAddress
        };

        await new HttpClient().PutAsJsonAsync("https://localhost:7254/api/v1/Person", updatedPerson);

        responseGetPeople = await new HttpClient().GetAsync("https://localhost:7254/api/v1/Person");
        people = await responseGetPeople.Content.ReadFromJsonAsync<List<BriefPerson>>();

        foreach (var person in people!)
        {
            Console.WriteLine($"Id: {person.Id}," +
                              $" Name: {person.FullName}," +
                              $" Birthday: {person.Birthday}");
        }



        // Удаление блока с персональными данными [DELETE]
        Console.WriteLine($"\nУдаление блока с персональными данными");

        Console.WriteLine($"Введите идентификатор блока с персональными данными: ");
        var deletedId = int.Parse(Console.ReadLine()!);

        await new HttpClient().DeleteAsync($"https://localhost:7254/api/v1/Person/{deletedId}");

        responseGetPeople = await new HttpClient().GetAsync("https://localhost:7254/api/v1/Person");
        people = await responseGetPeople.Content.ReadFromJsonAsync<List<BriefPerson>>();

        foreach (var person in people!)
        {
            Console.WriteLine($"Id: {person.Id}," +
                              $" Name: {person.FullName}," +
                              $" Birthday: {person.Birthday}");
        }

        Console.ReadLine();
    }
}