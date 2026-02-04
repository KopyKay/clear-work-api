using FluentValidation;

namespace ClearWork.Application.Paychecks.Commands.UpdateEmploymentContractPaycheck;

public class UpdateEmploymentContractPaycheckCommandValidator : AbstractValidator<UpdateEmploymentContractPaycheckCommand>
{
    public UpdateEmploymentContractPaycheckCommandValidator()
    {
        When(command => command.GrossAmount.HasValue, () =>
        {
            RuleFor(command => command.GrossAmount!.Value)
                .GreaterThan(0)
                .WithMessage("Gross amount must be greater than 0.");
        });

        When(command => command.NetAmount.HasValue, () =>
        {
            RuleFor(command => command.NetAmount!.Value)
                .GreaterThan(0)
                .WithMessage("Net amount must be greater than 0.");
        });

        When(command => command.NetAmount.HasValue && command.GrossAmount.HasValue, () =>
        {
            RuleFor(command => command.NetAmount!.Value)
                .LessThanOrEqualTo(command => command.GrossAmount!.Value)
                .WithMessage("Net amount cannot be greater than gross amount.");
        });
    }
}