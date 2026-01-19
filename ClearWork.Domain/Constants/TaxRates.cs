namespace ClearWork.Domain.Constants;

public class TaxRates
{
    public const decimal PensionRateDefaultValue = 0.0976m;
    public const decimal DisabilityRateDefaultValue = 0.015m;
    public const decimal SicknessRateDefaultValue = 0.0245m;
    public const decimal HealthRateDefaultValue = 0.09m;
    
    public const decimal TaxFreeAmountDefaultValue = 30000m;
    public const decimal TaxThresholdDefaultValue = 120000m;
    public const decimal LowerTaxRateDefaultValue = 0.12m;
    public const decimal HigherTaxRateDefaultValue = 0.32m;
    
    public const decimal StandardTaxDeductionMonthlyDefaultValue = 250m;
    public const decimal YoungPersonTaxReliefLimitDefaultValue = 85528m;
}