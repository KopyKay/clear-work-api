using System.Text.Json.Serialization;
using MediatR;

namespace ClearWork.Application.AnnualTaxRates.Commands.UpdateUserAnnualTaxRate;

public record UpdateUserAnnualTaxRateCommand : IRequest
{
    [JsonIgnore]
    public int Year { get; set; }
    
    public decimal? PensionRate { get; init; }
    public decimal? DisabilityRate { get; init; }
    public decimal? SicknessRate { get; init; }
    public decimal? HealthRate { get; init; }
    public decimal? TaxFreeAmount { get; init; }
    public decimal? TaxThreshold { get; init; }
    public decimal? LowerTaxRate { get; init; }
    public decimal? HigherTaxRate { get; init; }
    public decimal? StandardTaxDeductionMonthly { get; init; }
    public decimal? YoungPersonTaxReliefLimit { get; init; }
}