using FluentValidation;

namespace Teledok.Persistence.DTO.FounderDTO.Commands;

/// <summary>
/// Валидация учредителя ЮЛ / основателя ИП
/// </summary>
public class CreateFounderValidation
    : AbstractValidator<CreateFounder>
{
    /// <summary>
    /// Создание валидатора учредителя ЮЛ / основателя ИП
    /// </summary>
    public CreateFounderValidation()
    {
        RuleFor(founder => founder.PersonId)
            .NotEmpty();
    }
}