using ClearWork.Domain.Entities;
using ClearWork.Domain.Exceptions;
using ClearWork.Domain.Repositories;

namespace ClearWork.Application.Extensions;

internal static class RepositoryExtensions
{
    public static async Task<AppSetting> GetUserAppSettingsOrThrowAsync(
        this IAppSettingRepository repository,
        string userId)
    {
        return await repository.GetUserAppSettingsAsync(userId)
               ?? throw NotFoundException.ForUserResource<AppSetting>("?", userId);
    }
    
    public static async Task<AnnualTaxRate> GetUserAnnualTaxRateOrThrowAsync(
        this IAnnualTaxRateRepository repository,
        string userId,
        int year)
    {
        return await repository.GetUserAnnualTaxRateAsync(userId, year)
               ?? throw NotFoundException.ForUserResource<AnnualTaxRate>($"year: {year}", userId);
    }
    
    public static async Task<Workplace> GetUserWorkplaceOrThrowAsync(
        this IWorkplaceRepository repository,
        string userId,
        int workplaceId)
    {
        return await repository.GetUserWorkplaceAsync(userId, workplaceId)
            ?? throw NotFoundException.ForUserResource<Workplace>(workplaceId, userId);
    }

    public static async Task<EmploymentContract> GetWorkplaceEmploymentContractOrThrowAsync(
        this IEmploymentContractRepository repository,
        int workplaceId,
        int contractId)
    {
        return await repository.GetWorkplaceEmploymentContractAsync(workplaceId, contractId)
            ?? throw NotFoundException.ForChildResource<EmploymentContract, Workplace>(contractId, workplaceId);
    }

    public static async Task<Paycheck> GetEmploymentContractPaycheckOrThrowAsync(
        this IPaycheckRepository repository,
        int contractId,
        int paycheckId)
    {
        return await repository.GetEmploymentContractPaycheckAsync(contractId, paycheckId)
               ?? throw NotFoundException.ForChildResource<Paycheck, EmploymentContract>(paycheckId, contractId);
    }
    
    public static async Task<BusinessTrip> GetEmploymentContractBusinessTripOrThrowAsync(
        this IBusinessTripRepository repository,
        int contractId,
        int businessTripId)
    {
        return await repository.GetEmploymentContractBusinessTripAsync(contractId, businessTripId)
            ?? throw NotFoundException.ForChildResource<BusinessTrip, EmploymentContract>(businessTripId, contractId);
    }
    
    public static async Task<TimeEntry> GetEmploymentContractTimeEntryOrThrowAsync(
        this ITimeEntryRepository repository,
        int contractId,
        int timeEntryId)
    {
        return await repository.GetEmploymentContractTimeEntryAsync(contractId, timeEntryId)
               ?? throw NotFoundException.ForChildResource<TimeEntry, EmploymentContract>(timeEntryId, contractId);
    }
}