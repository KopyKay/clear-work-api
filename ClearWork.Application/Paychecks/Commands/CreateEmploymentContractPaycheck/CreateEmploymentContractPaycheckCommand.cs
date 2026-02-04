using System.Text.Json.Serialization;
using MediatR;

namespace ClearWork.Application.Paychecks.Commands.CreateEmploymentContractPaycheck;

public record CreateEmploymentContractPaycheckCommand : IRequest<int>
{
    [JsonIgnore]
    public int WorkplaceId { get; set; }
    
    [JsonIgnore]
    public int ContractId { get; set; }

    public DateOnly PaymentDate { get; init; }
    public decimal GrossAmount { get; init; }
    public decimal NetAmount { get; init; }
}