using System.Text.Json.Serialization;
using ClearWork.Domain.Enums;
using MediatR;

namespace ClearWork.Application.EmploymentContracts.Commands.UpdateWorkplaceEmploymentContract;

public record UpdateWorkplaceEmploymentContractCommand : IRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    
    [JsonIgnore]
    public int WorkplaceId { get; set; }
    
    public ContractType? ContractType { get; init; }
    public EmploymentLevel? EmploymentLevel { get; init; }
    public decimal? HourlyRate { get; init; }
    public int? PaymentDay { get; init; }
    public PaymentFrequency? PaymentFrequency { get; init; }
    public DateTimeOffset? StartDateTime { get; init; }
    public DateTimeOffset? EndDateTime { get; init; }
    public bool? IsActive { get; init; }
}