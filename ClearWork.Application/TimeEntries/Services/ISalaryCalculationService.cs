using ClearWork.Domain.Entities;
using ClearWork.Domain.OwnedTypes;

namespace ClearWork.Application.TimeEntries.Services;

public interface ISalaryCalculationService
{
    WorkEntryCalculation CalculateWorkEntry(WorkEntry entry, EmploymentContract contract, AnnualTaxRate taxRate,
        User user, PpkSetting? ppkSettings);

    LeaveEntryCalculation CalculateLeaveEntry(LeaveEntry entry, EmploymentContract contract, AnnualTaxRate taxRate,
        User user);
}