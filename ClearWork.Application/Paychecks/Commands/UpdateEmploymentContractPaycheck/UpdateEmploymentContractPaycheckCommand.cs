using System.Text.Json.Serialization;
using MediatR;

namespace ClearWork.Application.Paychecks.Commands.UpdateEmploymentContractPaycheck;

public record UpdateEmploymentContractPaycheckCommand : IRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    
    [JsonIgnore]
    public int WorkplaceId { get; set; }
    
    [JsonIgnore]
    public int ContractId { get; set; }
    
    public DateOnly? PaymentDate { get; init; }
    public decimal? GrossAmount { get; init; }
    public decimal? NetAmount { get; init; }
}