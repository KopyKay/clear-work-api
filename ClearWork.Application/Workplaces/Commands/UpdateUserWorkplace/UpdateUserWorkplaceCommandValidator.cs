using FluentValidation;

namespace ClearWork.Application.Workplaces.Commands.UpdateUserWorkplace;

public class UpdateUserWorkplaceCommandValidator : AbstractValidator<UpdateUserWorkplaceCommand>
{
    public UpdateUserWorkplaceCommandValidator()
    {
        When(command => !string.IsNullOrWhiteSpace(command.Name), () =>
        {
            RuleFor(command => command.Name)
                .MaximumLength(255);
        });

        When(command => command.BaseHourlyRate.HasValue, () =>
        {
            RuleFor(command => command.BaseHourlyRate)
                .GreaterThan(0);
        });
        
        When(command => command.PpkSettings is not null, () =>
        {
            RuleFor(command => command.PpkSettings!.EmployeeRate)
                .InclusiveBetween(0, 1);

            RuleFor(command => command.PpkSettings!.EmployerRate)
                .InclusiveBetween(0, 1);
        });
    }
}