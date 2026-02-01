using FluentValidation;

namespace ClearWork.Application.AnnualTaxRates.Commands.UpdateUserAnnualTaxRate;

public class UpdateUserAnnualTaxRateCommandValidator : AbstractValidator<UpdateUserAnnualTaxRateCommand>
{
    public UpdateUserAnnualTaxRateCommandValidator()
    {
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
