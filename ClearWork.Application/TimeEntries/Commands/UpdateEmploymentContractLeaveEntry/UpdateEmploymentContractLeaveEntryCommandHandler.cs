using AutoMapper;
using ClearWork.Application.Extensions;
using ClearWork.Application.TimeEntries.Services;
using ClearWork.Application.Users;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
using ClearWork.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ClearWork.Application.TimeEntries.Commands.UpdateEmploymentContractLeaveEntry;

public class UpdateEmploymentContractLeaveEntryCommandHandler 
(
    ILogger<UpdateEmploymentContractLeaveEntryCommandHandler> logger,
    IAnnualTaxRateRepository annualTaxRateRepository,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    ITimeEntryRepository timeEntryRepository,
    ISalaryCalculationService salaryCalculationService,
    UserManager<User> userManager,
    IUserContext userContext,
    IMapper mapper      
)    
: IRequestHandler<UpdateEmploymentContractLeaveEntryCommand>
{
    public async Task Handle(UpdateEmploymentContractLeaveEntryCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Updating leave entry [{TimeEntryId}] in employment contract [{ContractId}] with {@UpdatedLeaveEntry}",
            request.Id, request.ContractId, request);
        
        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);
        
        var workplaceEmploymentContract = 
            await employmentContractRepository.GetWorkplaceEmploymentContractOrThrowAsync(request.WorkplaceId, request.ContractId);
        
        var timeEntry = await timeEntryRepository.GetEmploymentContractTimeEntryAsync(request.ContractId, request.Id, true)
                        ?? throw NotFoundException.ForChildResource<TimeEntry, EmploymentContract>(request.Id, request.ContractId);

        if (timeEntry is not LeaveEntry leaveEntry)
            throw new BusinessRuleException($"Time entry with id [{request.Id}] is not a leave entry.");
        
        ValidateDateChanges(request, leaveEntry);
        
        mapper.Map(request, leaveEntry);
        
        var entryYear = leaveEntry.StartDateTime.Year;
        var taxRate = await annualTaxRateRepository.GetUserAnnualTaxRateOrThrowAsync(userId, entryYear);
        
        var user = await userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException(nameof(User), userId);
        
        var newCalculation = salaryCalculationService.CalculateLeaveEntry(leaveEntry, workplaceEmploymentContract, taxRate, user);

        UpdateCalculation(leaveEntry.Calculation as LeaveEntryCalculation, newCalculation);
        
        await timeEntryRepository.SaveChangesAsync();
    }
    
    private static void ValidateDateChanges(UpdateEmploymentContractLeaveEntryCommand request, LeaveEntry existing)
    {
        var newStartDate = request.StartDateTime?.ToUniversalTime() ?? existing.StartDateTime;
        var newEndDate = request.EndDateTime?.ToUniversalTime() ?? existing.EndDateTime;

        if (newEndDate <= newStartDate)
            throw new InvalidDateRangeException();
    }
    
    private static void UpdateCalculation(LeaveEntryCalculation? existing, LeaveEntryCalculation newCalc)
    {
        if (existing == null)
            throw new BusinessRuleException("Existing calculation not found.");

        existing.GrossSalary = newCalc.GrossSalary;
        existing.PensionContribution = newCalc.PensionContribution;
        existing.DisabilityContribution = newCalc.DisabilityContribution;
        existing.SicknessContribution = newCalc.SicknessContribution;
        existing.TotalZusContributions = newCalc.TotalZusContributions;
        existing.TaxDeduction = newCalc.TaxDeduction;
        existing.TaxBase = newCalc.TaxBase;
        existing.TaxAmount = newCalc.TaxAmount;
        existing.HealthContribution = newCalc.HealthContribution;
        existing.PpkEmployeeContribution = newCalc.PpkEmployeeContribution;
        existing.PpkEmployerContribution = newCalc.PpkEmployerContribution;
        existing.NetSalary = newCalc.NetSalary;
        existing.YoungPersonReliefApplied = newCalc.YoungPersonReliefApplied;
        existing.StudentExemptionApplied = newCalc.StudentExemptionApplied;
        existing.AppliedTaxDeductionType = newCalc.AppliedTaxDeductionType;
        existing.ContractTypeUsed = newCalc.ContractTypeUsed;
        existing.WorkingDays = newCalc.WorkingDays;
        existing.DailyRate = newCalc.DailyRate;
        existing.IsPaidBySocialSecurity = newCalc.IsPaidBySocialSecurity;
        existing.CalculatedAt = DateTimeOffset.UtcNow;
    }
}