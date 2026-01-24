namespace ClearWork.Application.Paychecks.Dtos;

public class PaycheckDto
{
    public int Id { get; set; }
    public int EmploymentContractId { get; set; }
    public DateOnly PaymentDate { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal NetAmount { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
}