using ClearWork.Domain.Entities;

namespace ClearWork.Domain.Repositories;

public interface IPaycheckRepository
{
    Task<IEnumerable<Paycheck>> GetEmploymentContractPaychecksAsync(int contractId);
    Task<Paycheck?> GetEmploymentContractPaycheckAsync(int contractId, int paycheckId);
}