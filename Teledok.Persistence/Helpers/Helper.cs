using Microsoft.EntityFrameworkCore;
using Teledok.Domain;
using Teledok.Persistence.DBContext;

namespace Teledok.Persistence.Helpers
{
    /// <summary>
    /// Помощник проекта
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Получение полного имени в формате "Фамилия Имя Отчество"
        /// </summary>
        /// <param name="person">Персональные данные физического лица</param>
        /// <returns>Строковое представление полного имени физического лица</returns>
        public static string GetFullName(Person person)
        {
            var fullName = $"{person?.Surname} {person?.Name}";

            if (person?.Patronomic != string.Empty)
            {
                fullName += $" {person?.Patronomic}";
            }

            return fullName;
        }

        /// <summary>
        /// Получение короткого имени в формате "Фамилия И.О."
        /// </summary>
        /// <param name="person">Персональные данные физического лица</param>
        /// <returns>Строковое представление короткого имени физического лица</returns>
        public static string GetShortName(Person person)
        {
            var shortName = $"{person?.Surname} {person?.Name[0]}.";

            if (person?.Patronomic != string.Empty)
            {
                shortName += $"{person?.Patronomic![0]}.";
            }

            return shortName;
        }

        /// <summary>
        /// Получение строкового выражения типа клиента
        /// </summary>
        /// <param name="type">Тип клиента</param>
        /// <returns>Строковое выражения типа клиента</returns>
        public static string GetClientType(ClientType type)
        {
            switch (type)
            {
                case ClientType.IndividualEntrepreneur:
                    return "Индивидуальный предприниматель";

                case ClientType.LegalEntity:
                default:
                    return "Юридическое лицо";
            }
        }

        public static async Task<Person?> GetPersonByIdAsync(TeledokDBContext context, int id)
        {
            var person = await context.People
                .SingleOrDefaultAsync(person => person.Id == id);

            return person;
        }
    }
}