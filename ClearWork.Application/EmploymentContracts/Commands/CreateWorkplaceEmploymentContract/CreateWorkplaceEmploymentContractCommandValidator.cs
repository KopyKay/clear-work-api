using FluentValidation;

namespace ClearWork.Application.EmploymentContracts.Commands.CreateWorkplaceEmploymentContract;

public class CreateWorkplaceEmploymentContractCommandValidator : AbstractValidator<CreateWorkplaceEmploymentContractCommand>
{
    public CreateWorkplaceEmploymentContractCommandValidator()
    {
        RuleFor(command => command.ContractType)
            .IsInEnum()
            .WithMessage("Invalid contract type.");

        RuleFor(command => command.EmploymentLevel)
            .IsInEnum()
            .WithMessage("Invalid employment level.");

        RuleFor(command => command.HourlyRate)
            .GreaterThan(0)
            .WithMessage("Hourly rate must be greater than 0.");

        RuleFor(command => command.PaymentFrequency)
            .IsInEnum()
            .WithMessage("Invalid payment frequency.");

        RuleFor(command => command.StartDateTime)
            .NotEmpty()
            .WithMessage("Start date is required.");

        When(command => command.PaymentDay.HasValue, () =>
        {
            RuleFor(command => command.PaymentDay!.Value)
                .InclusiveBetween(1, 31)
                .WithMessage("Payment day must be between 1 and 31.");
        });

        When(command => command.EndDateTime.HasValue, () =>
        {
            RuleFor(command => command)
                .Must(command => command.EndDateTime > command.StartDateTime)
                .WithMessage("End date must be after start date.");
        });
    }
}