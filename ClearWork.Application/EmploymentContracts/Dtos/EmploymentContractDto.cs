using ClearWork.Domain.Enums;

namespace ClearWork.Application.EmploymentContracts.Dtos;

public class EmploymentContractDto
{
    public int Id { get; set; }
    public int WorkplaceId { get; set; }
    public ContractType ContractType { get; set; }
    public EmploymentLevel EmploymentLevel { get; set; }
    public decimal HourlyRate { get; set; }
    public int? PaymentDay { get; set; }
    public PaymentFrequency PaymentFrequency { get; set; }
    public DateTimeOffset StartDateTime { get; set; }
    public DateTimeOffset? EndDateTime { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public bool IsActive { get; set; }
}