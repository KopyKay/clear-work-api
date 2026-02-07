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

namespace ClearWork.Application.TimeEntries.Commands.CreateEmploymentContractLeaveEntry;

public class CreateEmploymentContractLeaveEntryCommandHandler 
(
    ILogger<CreateEmploymentContractLeaveEntryCommandHandler> logger,
    IAnnualTaxRateRepository annualTaxRateRepository,
    IWorkplaceRepository workplaceRepository,
    IEmploymentContractRepository employmentContractRepository,
    ITimeEntryRepository timeEntryRepository,
    ISalaryCalculationService salaryCalculationService,
    UserManager<User> userManager,
    IUserContext userContext,
    IMapper mapper        
)    
: IRequestHandler<CreateEmploymentContractLeaveEntryCommand, int>
{
    public async Task<int> Handle(CreateEmploymentContractLeaveEntryCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser()!.Id;
        
        logger.LogInformation("Creating leave entry in employment contract with id [{ContractId}]", request.ContractId);

        await workplaceRepository.GetUserWorkplaceOrThrowAsync(userId, request.WorkplaceId);

        var workplaceEmploymentContract =
            await employmentContractRepository.GetWorkplaceEmploymentContractOrThrowAsync(request.WorkplaceId, request.ContractId);

        var leaveEntry = mapper.Map<LeaveEntry>(request);
        leaveEntry.EmploymentContractId = request.ContractId;

        var entryYear = request.StartDateTime.Year;
        var taxRate = await annualTaxRateRepository.GetUserAnnualTaxRateOrThrowAsync(userId, entryYear);

        var user = await userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException(nameof(User), userId);
        
        var calculation = salaryCalculationService.CalculateLeaveEntry(leaveEntry, workplaceEmploymentContract, taxRate, user);
        leaveEntry.Calculation = calculation;
        
        var leaveEntryId = await timeEntryRepository.CreateEmploymentContractTimeEntryAsync(leaveEntry);
        
        return leaveEntryId;
    }
}