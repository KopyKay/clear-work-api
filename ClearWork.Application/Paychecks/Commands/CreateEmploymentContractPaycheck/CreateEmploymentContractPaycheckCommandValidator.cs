using FluentValidation;

namespace ClearWork.Application.Paychecks.Commands.CreateEmploymentContractPaycheck;

public class CreateEmploymentContractPaycheckCommandValidator : AbstractValidator<CreateEmploymentContractPaycheckCommand>
{
    public CreateEmploymentContractPaycheckCommandValidator()
    {
        RuleFor(command => command.PaymentDate)
            .NotEmpty()
            .WithMessage("Payment date is required.");
        
        RuleFor(command => command.GrossAmount)
            .GreaterThan(0)
            .WithMessage("Gross amount must be greater than 0.");

        RuleFor(command => command.NetAmount)
            .GreaterThan(0)
            .WithMessage("Net amount must be greater than 0.")
            .LessThanOrEqualTo(command => command.GrossAmount)
            .WithMessage("Net amount cannot be greater than gross amount.");
    }
}