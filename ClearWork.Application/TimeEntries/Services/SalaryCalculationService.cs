using ClearWork.Domain.Entities;
using ClearWork.Domain.Enums;
using ClearWork.Domain.OwnedTypes;

namespace ClearWork.Application.TimeEntries.Services;

public class SalaryCalculationService : ISalaryCalculationService
{
    private const int StandardWorkDayHours = 8;
    private const int YoungPersonAgeLimit = 26;

    public WorkEntryCalculation CalculateWorkEntry(WorkEntry entry, EmploymentContract contract, AnnualTaxRate taxRate, User user, PpkSetting? ppkSettings)
    {
        var totalHours = CalculateTotalHours(entry);
        var shiftType = DetermineShiftType(entry);
        var multiplier = GetShiftMultiplier(shiftType);
        var grossSalary = totalHours * contract.HourlyRate * multiplier;
        var isStudent = user.IsStudent;
        var isYoungPerson = IsUnder26(user.DateOfBirth, entry.StartDateTime);
        var isStudentOnUz = isStudent && contract.ContractType == ContractType.Uz;
        var zus = CalculateZusContributions(grossSalary, contract.ContractType, taxRate, isStudentOnUz);
        var healthContribution = CalculateHealthContribution(grossSalary, zus.Total, taxRate.HealthRate, isStudentOnUz);
        var taxDeduction = CalculateTaxDeduction(grossSalary, contract.ContractType, taxRate.StandardTaxDeductionMonthly);
        var taxBase = Math.Max(0, Math.Round(grossSalary - zus.Total - taxDeduction, 0));
        var taxAmount = CalculateTaxAmount(taxBase, taxRate.LowerTaxRate, isYoungPerson);
        var ppk = CalculatePpkContributions(grossSalary, ppkSettings);
        var netSalary = grossSalary - zus.Total - taxAmount - healthContribution - ppk.Employee;

        return new WorkEntryCalculation
        {
            TimeEntryId = entry.Id,
            GrossSalary = Round(grossSalary),
            PensionContribution = Round(zus.Pension),
            DisabilityContribution = Round(zus.Disability),
            SicknessContribution = Round(zus.Sickness),
            TotalZusContributions = Round(zus.Total),
            TaxDeduction = Round(taxDeduction),
            TaxBase = Round(taxBase),
            TaxAmount = Round(taxAmount),
            HealthContribution = Round(healthContribution),
            PpkEmployeeContribution = Round(ppk.Employee),
            PpkEmployerContribution = Round(ppk.Employer),
            NetSalary = Round(netSalary),
            YoungPersonReliefApplied = isYoungPerson,
            StudentExemptionApplied = isStudentOnUz,
            AppliedTaxDeductionType = GetTaxDeductionType(contract.ContractType),
            ContractTypeUsed = contract.ContractType,
            TotalHours = totalHours,
            ShiftType = shiftType,
            Multiplier = multiplier,
            HourlyRateUsed = contract.HourlyRate
        };
    }

    public LeaveEntryCalculation CalculateLeaveEntry(LeaveEntry entry, EmploymentContract contract, AnnualTaxRate taxRate, User user)
    {
        var dailyRate = contract.HourlyRate * StandardWorkDayHours;
        var grossSalary = entry.WorkingDays * dailyRate;
        var isStudent = user.IsStudent;
        var isYoungPerson = IsUnder26(user.DateOfBirth, entry.StartDateTime);
        var isStudentOnUz = isStudent && contract.ContractType == ContractType.Uz;
        var isPaidBySocialSecurity = entry.LeaveType == LeaveType.SickLeave && entry.WorkingDays > 33;
        var zus = CalculateZusContributions(grossSalary, contract.ContractType, taxRate, isStudentOnUz);
        var healthContribution = CalculateHealthContribution(grossSalary, zus.Total, taxRate.HealthRate, isStudentOnUz);
        var taxBase = Math.Max(0, Math.Round(grossSalary - zus.Total, 0));
        var taxAmount = CalculateTaxAmount(taxBase, taxRate.LowerTaxRate, isYoungPerson);
        var netSalary = grossSalary - zus.Total - taxAmount - healthContribution;

        return new LeaveEntryCalculation
        {
            TimeEntryId = entry.Id,
            GrossSalary = Round(grossSalary),
            PensionContribution = Round(zus.Pension),
            DisabilityContribution = Round(zus.Disability),
            SicknessContribution = Round(zus.Sickness),
            TotalZusContributions = Round(zus.Total),
            TaxDeduction = 0,
            TaxBase = Round(taxBase),
            TaxAmount = Round(taxAmount),
            HealthContribution = Round(healthContribution),
            PpkEmployeeContribution = 0,
            PpkEmployerContribution = 0,
            NetSalary = Round(netSalary),
            YoungPersonReliefApplied = isYoungPerson,
            StudentExemptionApplied = isStudentOnUz,
            AppliedTaxDeductionType = TaxDeductionType.None,
            ContractTypeUsed = contract.ContractType,
            WorkingDays = entry.WorkingDays,
            DailyRate = dailyRate,
            IsPaidBySocialSecurity = isPaidBySocialSecurity
        };
    }

#region Helpers

    private static decimal CalculateTotalHours(WorkEntry entry) 
        => (decimal)(entry.EndDateTime - entry.StartDateTime).TotalHours;

    private static ShiftType DetermineShiftType(WorkEntry entry)
    {
        var dayOfWeek = entry.StartDateTime.DayOfWeek;
        
        if (dayOfWeek == DayOfWeek.Sunday)
            return ShiftType.SundayOrHoliday;

        if (dayOfWeek == DayOfWeek.Saturday)
            return ShiftType.Saturday;

        var startHour = entry.StartDateTime.Hour;
        var endHour = entry.EndDateTime.Hour;
        
        if (startHour >= 22 || endHour <= 6)
            return ShiftType.Night;
        
        var hours = (entry.EndDateTime - entry.StartDateTime).TotalHours;
        
        if (hours > StandardWorkDayHours)
            return ShiftType.Overtime;

        return ShiftType.Normal;
    }

    private static decimal GetShiftMultiplier(ShiftType shiftType) => shiftType switch
    {
        ShiftType.Normal => 1.0m,
        ShiftType.Overtime => 1.5m,
        ShiftType.Night => 1.2m,
        ShiftType.Saturday => 1.5m,
        ShiftType.SundayOrHoliday => 2.0m,
        _ => 1.0m
    };

    private static (decimal Pension, decimal Disability, decimal Sickness, decimal Total) CalculateZusContributions(
        decimal grossSalary,
        ContractType contractType,
        AnnualTaxRate taxRate,
        bool isStudentOnUz)
    {
        if (isStudentOnUz)
            return (0, 0, 0, 0);

        if (contractType is ContractType.UoD or ContractType.B2B)
            return (0, 0, 0, 0);

        var pension = grossSalary * taxRate.PensionRate;
        var disability = grossSalary * taxRate.DisabilityRate;
        
        var sickness = contractType == ContractType.UoP 
            ? grossSalary * taxRate.SicknessRate 
            : 0;

        var total = pension + disability + sickness;
        return (pension, disability, sickness, total);
    }

    private static decimal CalculateHealthContribution(decimal grossSalary, decimal zusTotal, decimal healthRate, bool isStudentOnUz)
    {
        if (isStudentOnUz)
            return 0;

        var baseForHealth = grossSalary - zusTotal;
        return baseForHealth * healthRate;
    }

    private static decimal CalculateTaxDeduction(decimal grossSalary, ContractType contractType, decimal standardDeduction)
    {
        return contractType switch
        {
            ContractType.UoP => standardDeduction,
            ContractType.Uz => grossSalary * 0.20m,
            ContractType.UoD => grossSalary * 0.50m,
            _ => 0
        };
    }

    private static TaxDeductionType GetTaxDeductionType(ContractType contractType) => contractType switch
    {
        ContractType.UoP => TaxDeductionType.Standard,
        ContractType.Uz => TaxDeductionType.PercentageBased,
        ContractType.UoD => TaxDeductionType.CreativeWork,
        _ => TaxDeductionType.None
    };

    private static decimal CalculateTaxAmount(decimal taxBase, decimal taxRate, bool isYoungPerson)
    {
        if (isYoungPerson)
            return 0;

        return taxBase * taxRate;
    }

    private static (decimal Employee, decimal Employer) CalculatePpkContributions(decimal grossSalary, PpkSetting? ppkSettings)
    {
        if (ppkSettings is null || !ppkSettings.IsActive)
            return (0, 0);

        var employee = grossSalary * ppkSettings.EmployeeRate;
        var employer = grossSalary * ppkSettings.EmployerRate;
        return (employee, employer);
    }

    private static bool IsUnder26(DateOnly dateOfBirth, DateTimeOffset referenceDate)
    {
        var age = referenceDate.Year - dateOfBirth.Year;

        if (referenceDate.Month < dateOfBirth.Month ||
            (referenceDate.Month == dateOfBirth.Month && referenceDate.Day < dateOfBirth.Day))
        {
            age--;
        }

        return age < YoungPersonAgeLimit;
    }

    private static decimal Round(decimal value) => Math.Round(value, 2);

#endregion
}