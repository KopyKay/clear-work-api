using FluentValidation;

namespace ClearWork.Application.AnnualTaxRates.Commands.CreateUserAnnualTaxRate;

public class CreateUserAnnualTaxRateCommandValidator : AbstractValidator<CreateUserAnnualTaxRateCommand>
{
    public CreateUserAnnualTaxRateCommandValidator()
    {
        RuleFor(command => command.Year)
            .InclusiveBetween(2000, 2100);
        
        RuleFor(command => command.PensionRate)
            .InclusiveBetween(0m, 1m);

        RuleFor(command => command.DisabilityRate)
            .InclusiveBetween(0m, 1m);

        RuleFor(command => command.SicknessRate)
            .InclusiveBetween(0m, 1m);

        RuleFor(command => command.HealthRate)
            .InclusiveBetween(0m, 1m);

        RuleFor(command => command.LowerTaxRate)
            .InclusiveBetween(0m, 1m);

        RuleFor(command => command.HigherTaxRate)
            .InclusiveBetween(0m, 1m);
        
        RuleFor(command => command.TaxFreeAmount)
            .GreaterThanOrEqualTo(0m);

        RuleFor(command => command.TaxThreshold)
            .GreaterThan(0m);

        RuleFor(command => command.StandardTaxDeductionMonthly)
            .GreaterThanOrEqualTo(0m);

        RuleFor(command => command.YoungPersonTaxReliefLimit)
            .GreaterThanOrEqualTo(0m);
    }
}