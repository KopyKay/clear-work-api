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

namespace ClearWork.Application.TimeEntries.Commands.UpdateEmploymentContractWorkEntry;

public class UpdateEmploymentContractWorkEntryCommandHandler 
(
    ILogger<UpdateEmploymentContractWorkEntryCommandHandler> logger,
    IAnnualTaxRateRepository annualTaxRateRepository,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    IBusinessTripRepository businessTripRepository,
    ITimeEntryRepository timeEntryRepository,
    ISalaryCalculationService salaryCalculationService,
    UserManager<User> userManager,
    IUserContext userContext,
    IMapper mapper        
)    
: IRequestHandler<UpdateEmploymentContractWorkEntryCommand>
{
    public async Task Handle(UpdateEmploymentContractWorkEntryCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Updating work entry [{TimeEntryId}] in employment contract [{ContractId}] with {@UpdatedWorkEntry}",
            request.Id, request.ContractId, request);
        
        var userWorkplace = await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);
        
        var workplaceEmploymentContract = 
            await employmentContractRepository.GetWorkplaceEmploymentContractOrThrowAsync(request.WorkplaceId, request.ContractId);
        
        if (request.BusinessTripId.HasValue)
            await businessTripRepository.GetEmploymentContractBusinessTripOrThrowAsync(request.ContractId, request.BusinessTripId.Value);
        
        var timeEntry = await timeEntryRepository.GetEmploymentContractTimeEntryAsync(request.ContractId, request.Id, true)
                        ?? throw NotFoundException.ForChildResource<TimeEntry, EmploymentContract>(request.Id, request.ContractId);
        
        if (timeEntry is not WorkEntry workEntry)
            throw new BusinessRuleException($"Time entry with id [{request.Id}] is not a work entry.");
        
        ValidateDateChanges(request, workEntry);
        
        if (request.ClearBusinessTrip == true && workEntry.BusinessTripId.HasValue)
        {
            workEntry.BusinessTripId = null;
            workEntry.DailyBusinessTripDetail = null;
        }
        
        if (request.ClearDailyBusinessTripDetail == true && workEntry.DailyBusinessTripDetail is not null)
        {
            workEntry.DailyBusinessTripDetail = null;
        }
        
        mapper.Map(request, workEntry);
        
        var entryYear = workEntry.StartDateTime.Year;
        var taxRate = await annualTaxRateRepository.GetUserAnnualTaxRateOrThrowAsync(userId, entryYear);
        
        var user = await userManager.FindByIdAsync(userId)
                   ?? throw new NotFoundException(nameof(User), userId);
        
        var newCalculation = salaryCalculationService.CalculateWorkEntry(workEntry, workplaceEmploymentContract, taxRate, user,
            userWorkplace.PpkSettings);
        
        UpdateCalculation(workEntry.Calculation as WorkEntryCalculation, newCalculation);

        await timeEntryRepository.SaveChangesAsync();
    }
    
    private static void ValidateDateChanges(UpdateEmploymentContractWorkEntryCommand request, WorkEntry existing)
    {
        var newStartDate = request.StartDateTime?.ToUniversalTime() ?? existing.StartDateTime;
        var newEndDate = request.EndDateTime?.ToUniversalTime() ?? existing.EndDateTime;

        if (newEndDate <= newStartDate)
            throw new InvalidDateRangeException();
    }
    
    private static void UpdateCalculation(WorkEntryCalculation? existing, WorkEntryCalculation newCalc)
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
        existing.TotalHours = newCalc.TotalHours;
        existing.ShiftType = newCalc.ShiftType;
        existing.Multiplier = newCalc.Multiplier;
        existing.HourlyRateUsed = newCalc.HourlyRateUsed;
        existing.CalculatedAt = DateTimeOffset.UtcNow;
    }
}