namespace ClearWork.Application.AnnualTaxRates.Dtos;

public class AnnualTaxRateDto
{
    public int Id { get; set; }
    public int Year { get; set; }
    public decimal PensionRate { get; set; }
    public decimal DisabilityRate { get; set; }
    public decimal SicknessRate { get; set; }
    public decimal HealthRate { get; set; }
    public decimal TaxFreeAmount { get; set; }
    public decimal TaxThreshold { get; set; }
    public decimal LowerTaxRate { get; set; }
    public decimal HigherTaxRate { get; set; }
    public decimal StandardTaxDeductionMonthly { get; set; }
    public decimal YoungPersonTaxReliefLimit { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}