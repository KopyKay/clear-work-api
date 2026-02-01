using FluentValidation;

namespace ClearWork.Application.Workplaces.Commands.CreateUserWorkplace;

public class CreateUserWorkplaceCommandValidator : AbstractValidator<CreateUserWorkplaceCommand>
{
    public CreateUserWorkplaceCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(command => command.BaseHourlyRate)
            .GreaterThan(0);

        When(command => command.PpkSettings is not null, () =>
        {
            RuleFor(command => command.PpkSettings!.EmployeeRate)
                .InclusiveBetween(0, 1);

            RuleFor(command => command.PpkSettings!.EmployerRate)
                .InclusiveBetween(0, 1);
        });
    }
}