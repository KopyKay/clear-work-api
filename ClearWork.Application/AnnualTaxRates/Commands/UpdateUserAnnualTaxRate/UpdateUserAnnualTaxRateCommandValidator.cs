using FluentValidation;

namespace ClearWork.Application.AnnualTaxRates.Commands.UpdateUserAnnualTaxRate;

public class UpdateUserAnnualTaxRateCommandValidator : AbstractValidator<UpdateUserAnnualTaxRateCommand>
{
    public UpdateUserAnnualTaxRateCommandValidator()
    {
        When(command => command.PensionRate.HasValue, () =>
        {
            RuleFor(command => command.PensionRate)
                .InclusiveBetween(0m, 1m);
        });

        When(command => command.DisabilityRate.HasValue, () =>
        {
            RuleFor(command => command.DisabilityRate)
                .InclusiveBetween(0m, 1m);
        });

        When(command => command.SicknessRate.HasValue, () =>
        {
            RuleFor(command => command.SicknessRate)
                .InclusiveBetween(0m, 1m);
        });

        When(command => command.HealthRate.HasValue, () =>
        {
            RuleFor(command => command.HealthRate)
                .InclusiveBetween(0m, 1m);
        });

        When(command => command.LowerTaxRate.HasValue, () =>
        {
            RuleFor(command => command.LowerTaxRate)
                .InclusiveBetween(0m, 1m);
        });

        When(command => command.HigherTaxRate.HasValue, () =>
        {
            RuleFor(command => command.HigherTaxRate)
                .InclusiveBetween(0m, 1m);
        });

        When(command => command.TaxFreeAmount.HasValue, () =>
        {
            RuleFor(command => command.TaxFreeAmount)
                .GreaterThanOrEqualTo(0m);
        });

        When(command => command.TaxThreshold.HasValue, () =>
        {
            RuleFor(command => command.TaxThreshold)
                .GreaterThan(0m);
        });

        When(command => command.StandardTaxDeductionMonthly.HasValue, () =>
        {
            RuleFor(command => command.StandardTaxDeductionMonthly)
                .GreaterThanOrEqualTo(0m);
        });

        When(command => command.YoungPersonTaxReliefLimit.HasValue, () =>
        {
            RuleFor(command => command.YoungPersonTaxReliefLimit)
                .GreaterThanOrEqualTo(0m);
        });
    }
}
