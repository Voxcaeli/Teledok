using FluentValidation;

namespace Teledok.Persistence.DTO.PersonDTO.Commands;

/// <summary>
/// Валидация персональных данных
/// </summary>
public class CreatePersonValidation
    : AbstractValidator<CreatePerson>
{
    /// <summary>
    /// Максимальное количество символов имени физического лица
    /// </summary>
    private const int MaxLengthName = 30;

    /// <summary>
    /// Максимальное количество символов фамилии физического лица
    /// </summary>
    private const int MaxLengthSurname = 50;

    /// <summary>
    /// Максимальное количество символов отчества физического лица
    /// </summary>
    private const int MaxLengthPatronomic = 50;

    /// <summary>
    /// Максимальная дата для установления даты рождения
    /// </summary>
    private readonly DateOnly MaxDate = DateOnly.FromDateTime(DateTime.Now);

    /// <summary>
    /// Создание валидатора персональных данных
    /// </summary>
    public CreatePersonValidation()
    {
        RuleFor(person => person.Name)
            .NotEmpty()
            .MaximumLength(MaxLengthName);

        RuleFor(person => person.Surname)
            .NotEmpty()
            .MaximumLength(MaxLengthSurname);

        RuleFor(person => person.Patronomic)
            .MaximumLength(MaxLengthPatronomic);

        RuleFor(person => DateOnly.Parse(person.Birthday!))
            .LessThan(MaxDate);
    }
}