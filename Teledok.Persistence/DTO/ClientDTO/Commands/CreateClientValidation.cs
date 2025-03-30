using FluentValidation;
using Teledok.Domain;

namespace Teledok.Persistence.DTO.ClientDTO.Commands;

/// <summary>
/// Валидация клиента
/// </summary>
public class CreateClientValidation
    : AbstractValidator<CreateClient>
{
    /// <summary>
    /// Максимальное количество символов названия юридического лица / ИП
    /// </summary>
    private const int MaxLengthName = 50;

    /// <summary>
    /// Максимальное значение типа юридического лица / ИП
    /// </summary>
    private readonly int MaxClientType = Enum.GetNames<ClientType>().Length;

    /// <summary>
    /// Количество символов ИНН для юридического лица
    /// </summary>
    private const int CountIndividualEntrepreneurTIN = 12;

    /// <summary>
    /// Количество символов ИНН для ИП
    /// </summary>
    private const int CountLegalEntityTIN = 10;

    /// <summary>
    /// Тип клиента
    /// </summary>
    private ClientType ClientType;

    /// <summary>
    /// Создание валидатора клиента
    /// </summary>
    public CreateClientValidation()
    {
        RuleFor(client => client.Name)
            .NotEmpty()
            .MaximumLength(MaxLengthName);

        RuleFor(client => client.Type)
            .NotEmpty()
            .GreaterThan(-1)
            .LessThan(MaxClientType);


        RuleFor(client => GetTINLength(client))
            .Equal(GetValidClientTypeLength(ClientType));
    }

    /// <summary>
    /// Получение количества цифр ИНН для клиента
    /// </summary>
    /// <param name="client">Клиент</param>
    /// <returns>Количество цифр ИНН</returns>
    private int GetTINLength(CreateClient client)
    {
        ClientType = (ClientType)client.Type;
        return int.Parse(client.TIN).ToString().Length;
    }

    /// <summary>
    /// Получение валидного количества цифр ИНН для клиента
    /// </summary>
    /// <param name="type">Тип клиента</param>
    /// <returns>Валидное количество цифр ИНН</returns>
    private static int GetValidClientTypeLength(ClientType type)
    {
        switch (type)
        {
            case ClientType.IndividualEntrepreneur:
                return CountIndividualEntrepreneurTIN;

            case ClientType.LegalEntity:
                return CountLegalEntityTIN;
        }

        return CountIndividualEntrepreneurTIN;
    }
}