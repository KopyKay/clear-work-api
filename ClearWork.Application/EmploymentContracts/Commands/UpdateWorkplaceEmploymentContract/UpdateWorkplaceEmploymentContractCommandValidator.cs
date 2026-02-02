using FluentValidation;

namespace ClearWork.Application.EmploymentContracts.Commands.UpdateWorkplaceEmploymentContract;

public class UpdateWorkplaceEmploymentContractCommandValidator : AbstractValidator<UpdateWorkplaceEmploymentContractCommand>
{
    public UpdateWorkplaceEmploymentContractCommandValidator()
    {
        When(command => command.ContractType.HasValue, () =>
        {
            RuleFor(command => command.ContractType!.Value)
                .IsInEnum()
                .WithMessage("Invalid contract type.");
        });

        When(command => command.EmploymentLevel.HasValue, () =>
        {
            RuleFor(command => command.EmploymentLevel!.Value)
                .IsInEnum()
                .WithMessage("Invalid employment level.");
        });

        When(command => command.HourlyRate.HasValue, () =>
        {
            RuleFor(command => command.HourlyRate!.Value)
                .GreaterThan(0)
                .WithMessage("Hourly rate must be greater than 0.");
        });

        When(command => command.PaymentFrequency.HasValue, () =>
        {
            RuleFor(command => command.PaymentFrequency!.Value)
                .IsInEnum()
                .WithMessage("Invalid payment frequency.");
        });

        When(command => command.PaymentDay.HasValue, () =>
        {
            RuleFor(command => command.PaymentDay!.Value)
                .InclusiveBetween(1, 31)
                .WithMessage("Payment day must be between 1 and 31.");
        });
    }
}