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

namespace ClearWork.Application.TimeEntries.Commands.CreateEmploymentContractWorkEntry;

public class CreateEmploymentContractWorkEntryCommandHandler 
(
    ILogger<CreateEmploymentContractWorkEntryCommandHandler> logger,
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
: IRequestHandler<CreateEmploymentContractWorkEntryCommand, int>
{
    public async Task<int> Handle(CreateEmploymentContractWorkEntryCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Creating work entry in employment contract with id [{ContractId}]", request.ContractId);
        
        var userWorkplace = await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);

        var workplaceEmploymentContract =
            await employmentContractRepository.GetWorkplaceEmploymentContractOrThrowAsync(request.WorkplaceId, request.ContractId);
        
        if (request.BusinessTripId.HasValue)
            await businessTripRepository.GetEmploymentContractBusinessTripOrThrowAsync(request.ContractId, request.BusinessTripId.Value);
        
        var workEntry = mapper.Map<WorkEntry>(request);
        workEntry.EmploymentContractId = request.ContractId;
        
        var entryYear = request.StartDateTime.Year;
        var taxRate = await annualTaxRateRepository.GetUserAnnualTaxRateOrThrowAsync(userId, entryYear);
        
        var user = await userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException(nameof(User), userId);
        
        var calculation = salaryCalculationService.CalculateWorkEntry(workEntry, workplaceEmploymentContract, taxRate, user,
            userWorkplace.PpkSettings);
        workEntry.Calculation = calculation;

        var workEntryId = await timeEntryRepository.CreateEmploymentContractTimeEntryAsync(workEntry);
        
        return workEntryId;
    }
}