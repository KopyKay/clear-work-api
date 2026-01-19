using ClearWork.Domain.Constants;
using ClearWork.Domain.Entities;
using ClearWork.Domain.Enums;
using ClearWork.Domain.OwnedTypes;
using ClearWork.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace ClearWork.Infrastructure.Seeders;

internal class ClearWorkSeeder(ClearWorkDbContext dbContext, UserManager<User> userManager) : IClearWorkSeeder
{
    public async Task SeedAsync()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Users.Any())
            {
                var users = await SeedUsersAsync();
                SeedAppSettings(users);
                SeedAnnualTaxRates(users);
                var workplaces = SeedWorkplaces(users);
                await dbContext.SaveChangesAsync();

                var contracts = SeedEmploymentContracts(workplaces);
                await dbContext.SaveChangesAsync();

                var businessTrips = SeedBusinessTrips(contracts);
                await dbContext.SaveChangesAsync();

                var timeEntries = SeedTimeEntries(contracts, businessTrips);
                await dbContext.SaveChangesAsync();

                SeedTimeEntryCalculations(timeEntries, contracts);
                SeedPaychecks(contracts);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private async Task<List<User>> SeedUsersAsync()
    {
        var users = new List<User>
        {
            new()
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                DateOfBirth = new DateOnly(2001, 8, 12),
                IsStudent = true,
                Email = "j.kowalski@wp.pl",
                UserName = "j.kowalski@wp.pl",
                NormalizedEmail = "J.KOWALSKI@WP.PL",
                NormalizedUserName = "J.KOWALSKI@WP.PL",
                EmailConfirmed = true,
                PhoneNumber = "+48639371956",
                PhoneNumberConfirmed = true,
                CreatedAt = DateTimeOffset.UtcNow.AddMonths(-6)
            },
            new()
            {
                FirstName = "Anna",
                LastName = "Nowak",
                DateOfBirth = new DateOnly(1990, 3, 22),
                Email = "anna.nowak@o2.pl",
                UserName = "anna.nowak@o2.pl",
                NormalizedEmail = "ANNA.NOWAK@O2.PL",
                NormalizedUserName = "ANNA.NOWAK@O2.PL",
                EmailConfirmed = true,
                PhoneNumber = "+48772916397",
                PhoneNumberConfirmed = true,
                CreatedAt = DateTimeOffset.UtcNow.AddMonths(-3)
            },
            new()
            {
                FirstName = "Piotr",
                LastName = "Wiśniewski",
                DateOfBirth = new DateOnly(1985, 11, 5),
                DisabilityLevel = DisabilityLevel.Light,
                Email = "piotr.wisniewski@gmail.com",
                UserName = "piotr.wisniewski@gmail.com",
                NormalizedEmail = "PIOTR.WISNIEWSKI@GMAIL.COM",
                NormalizedUserName = "PIOTR.WISNIEWSKI@GMAIL.COM",
                EmailConfirmed = true,
                CreatedAt = DateTimeOffset.UtcNow.AddMonths(-12)
            },
            new()
            {
                FirstName = "Maria",
                LastName = "Zielińska",
                DateOfBirth = new DateOnly(2003, 5, 15),
                IsStudent = true,
                Email = "maria.zielinska@student.edu.pl",
                UserName = "maria.zielinska@student.edu.pl",
                NormalizedEmail = "MARIA.ZIELINSKA@STUDENT.EDU.PL",
                NormalizedUserName = "MARIA.ZIELINSKA@STUDENT.EDU.PL",
                EmailConfirmed = true,
                CreatedAt = DateTimeOffset.UtcNow.AddMonths(-2)
            },
            new()
            {
                FirstName = "Tomasz",
                LastName = "Lewandowski",
                DateOfBirth = new DateOnly(1978, 2, 28),
                DisabilityLevel = DisabilityLevel.Moderate,
                Email = "t.lewandowski@firma.pl",
                UserName = "t.lewandowski@firma.pl",
                NormalizedEmail = "T.LEWANDOWSKI@FIRMA.PL",
                NormalizedUserName = "T.LEWANDOWSKI@FIRMA.PL",
                EmailConfirmed = true,
                CreatedAt = DateTimeOffset.UtcNow.AddMonths(-18)
            },
            new()
            {
                FirstName = "Katarzyna",
                LastName = "Wójcik",
                DateOfBirth = new DateOnly(1995, 7, 10),
                Email = "k.wojcik@outlook.com",
                UserName = "k.wojcik@outlook.com",
                NormalizedEmail = "K.WOJCIK@OUTLOOK.COM",
                NormalizedUserName = "K.WOJCIK@OUTLOOK.COM",
                EmailConfirmed = true,
                CreatedAt = DateTimeOffset.UtcNow.AddMonths(-8)
            },
            new()
            {
                FirstName = "Michał",
                LastName = "Kamiński",
                DateOfBirth = new DateOnly(2000, 12, 1),
                IsStudent = true,
                Email = "michal.kaminski@yahoo.com",
                UserName = "michal.kaminski@yahoo.com",
                NormalizedEmail = "MICHAL.KAMINSKI@YAHOO.COM",
                NormalizedUserName = "MICHAL.KAMINSKI@YAHOO.COM",
                EmailConfirmed = true,
                CreatedAt = DateTimeOffset.UtcNow.AddMonths(-4)
            },
            new()
            {
                FirstName = "Agnieszka",
                LastName = "Dąbrowska",
                DateOfBirth = new DateOnly(1992, 9, 18),
                Email = "agnieszka.dabrowska@interia.pl",
                UserName = "agnieszka.dabrowska@interia.pl",
                NormalizedEmail = "AGNIESZKA.DABROWSKA@INTERIA.PL",
                NormalizedUserName = "AGNIESZKA.DABROWSKA@INTERIA.PL",
                EmailConfirmed = true,
                CreatedAt = DateTimeOffset.UtcNow.AddMonths(-10)
            }
        };

        foreach (var user in users)
        {
            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, "Password1!");
        }

        dbContext.Users.AddRange(users);
        await dbContext.SaveChangesAsync();
        return users;
    }

    private void SeedAppSettings(List<User> users)
    {
        var appSettings = users.Select((user, index) => new AppSetting
        {
            UserId = user.Id,
            PushNotificationEnabled = index % 2 == 0,
            PushNotificationReminderTime = new TimeOnly(19 + index % 3, 0),
            AppThemeSameAsSystem = index % 3 == 0,
            DarkModeEnabled = index % 2 == 1,
            DarkModeEnableTime = new TimeOnly(18, 0),
            DarkModeDisableTime = new TimeOnly(8, 0)
        }).ToList();

        dbContext.AppSettings.AddRange(appSettings);
    }

    private void SeedAnnualTaxRates(List<User> users)
    {
        var currentYear = DateTime.UtcNow.Year;
        var taxRates = new List<AnnualTaxRate>();

        foreach (var user in users)
        {
            for (var year = currentYear - 2; year <= currentYear; year++)
            {
                taxRates.Add(new AnnualTaxRate
                {
                    UserId = user.Id,
                    Year = year
                });
            }
        }

        dbContext.AnnualTaxRates.AddRange(taxRates);
    }

    private List<Workplace> SeedWorkplaces(List<User> users)
    {
        var workplaceData = new[]
        {
            ("TechSoft Sp. z o.o.", 35.00m, true),
            ("Restauracja Pod Złotym Lwem", 28.50m, false),
            ("Freelance IT", 75.00m, false),
            ("Kancelaria Prawna Nowak", 45.00m, true),
            ("Supermarket Biedronka", 27.00m, false),
            ("Budimex S.A.", 40.00m, true),
            ("Apteka Zdrowie", 32.00m, false),
            ("Bank PKO BP", 55.00m, true),
            ("Szkoła Podstawowa nr 5", 30.00m, false),
            ("Salon Fryzjerski Elegancja", 25.00m, false),
            ("Warsztat Samochodowy Auto-Service", 38.00m, false),
            ("Studio Graficzne Creative", 50.00m, true),
            ("Klinika Medyczna Zdrowie+", 60.00m, true),
            ("Firma Transportowa SpeedLog", 33.00m, false),
            ("Hotel Marriott", 35.00m, true)
        };

        var workplaces = new List<Workplace>();
        var random = new Random(42);

        foreach (var user in users)
        {
            var workplaceCount = random.Next(1, 4);
            var usedIndices = new HashSet<int>();

            for (var i = 0; i < workplaceCount; i++)
            {
                int index;
                do { index = random.Next(workplaceData.Length); } 
                while (usedIndices.Contains(index));
                usedIndices.Add(index);

                var (name, rate, hasPpk) = workplaceData[index];
                workplaces.Add(new Workplace
                {
                    UserId = user.Id,
                    Name = name,
                    BaseHourlyRate = rate,
                    CreatedAt = DateTimeOffset.UtcNow.AddMonths(-random.Next(1, 12)),
                    IsActive = true,
                    PpkSettings = hasPpk ? new PpkSetting
                    {
                        IsActive = true,
                        EmployeeRate = PpkRates.PpkEmployeeRateDefaultValue,
                        EmployerRate = PpkRates.PpkEmployerRateDefaultValue
                    } : null
                });
            }
        }

        dbContext.Workplaces.AddRange(workplaces);
        return workplaces;
    }

    private List<EmploymentContract> SeedEmploymentContracts(List<Workplace> workplaces)
    {
        var contracts = new List<EmploymentContract>();
        var random = new Random(42);
        var contractTypes = Enum.GetValues<ContractType>();
        var employmentLevels = Enum.GetValues<EmploymentLevel>();
        var paymentFrequencies = Enum.GetValues<PaymentFrequency>();

        foreach (var workplace in workplaces)
        {
            var contractCount = random.Next(1, 3);

            for (var i = 0; i < contractCount; i++)
            {
                var startDate = DateTimeOffset.UtcNow.AddMonths(-random.Next(1, 24));
                var isActive = i == 0;
                var endDate = isActive ? (DateTimeOffset?)null : startDate.AddMonths(random.Next(3, 12));

                contracts.Add(new EmploymentContract
                {
                    WorkplaceId = workplace.Id,
                    ContractType = contractTypes[random.Next(contractTypes.Length)],
                    EmploymentLevel = employmentLevels[random.Next(employmentLevels.Length)],
                    HourlyRate = workplace.BaseHourlyRate + random.Next(-5, 10),
                    PaymentDay = random.Next(1, 28),
                    PaymentFrequency = paymentFrequencies[random.Next(paymentFrequencies.Length)],
                    StartDateTime = startDate,
                    EndDateTime = endDate,
                    IsActive = isActive
                });
            }
        }

        dbContext.EmploymentContracts.AddRange(contracts);
        return contracts;
    }

    private List<BusinessTrip> SeedBusinessTrips(List<EmploymentContract> contracts)
    {
        var trips = new List<BusinessTrip>();
        var random = new Random(42);
        var countries = new[] { "DEU", "FRA", "GBR", "CZE", "POL", "POL", "POL" };
        var transportTypes = Enum.GetValues<TransportType>();
        var accommodationTypes = Enum.GetValues<AccommodationType>();

        foreach (var contract in contracts.Where(c => c.IsActive).Take(10))
        {
            var tripCount = random.Next(0, 4);

            for (var i = 0; i < tripCount; i++)
            {
                var startDate = DateTimeOffset.UtcNow.AddDays(-random.Next(10, 120));
                var duration = random.Next(1, 6);
                var country = countries[random.Next(countries.Length)];

                trips.Add(new BusinessTrip
                {EmploymentContractId = contract.Id,
                    StartDateTime = startDate,
                    EndDateTime = startDate.AddDays(duration),
                    TripType = country == "POL" ? BusinessTripType.Domestic : BusinessTripType.International,
                    DestinationCountry = country,
                    AccommodationType = accommodationTypes[random.Next(accommodationTypes.Length)],
                    TransportType = transportTypes[random.Next(transportTypes.Length)],
                    KilometersDriven = random.Next(0, 2) == 0 ? random.Next(50, 800) : null,
                    ActualTransportCost = random.Next(0, 2) == 0 ? random.Next(50, 500) : null
                });
            }
        }

        dbContext.BusinessTrips.AddRange(trips);
        return trips;
    }

    private List<TimeEntry> SeedTimeEntries(List<EmploymentContract> contracts, List<BusinessTrip> trips)
    {
        var entries = new List<TimeEntry>();
        var random = new Random(42);

        foreach (var contract in contracts.Where(c => c.IsActive))
        {
            var baseDate = DateTime.UtcNow.AddDays(-90);

            for (var day = 0; day < 90; day++)
            {
                var workDate = baseDate.AddDays(day);
                if (workDate.DayOfWeek == DayOfWeek.Saturday && random.Next(0, 5) != 0) continue;
                if (workDate.DayOfWeek == DayOfWeek.Sunday && random.Next(0, 8) != 0) continue;
                if (random.Next(0, 10) == 0) continue;

                var startHour = 6 + random.Next(0, 6);
                var duration = 4 + random.Next(0, 6);

                var trip = trips.FirstOrDefault(t =>
                    t.EmploymentContractId == contract.Id &&
                    t.StartDateTime.UtcDateTime.Date <= workDate.Date &&
                    t.EndDateTime!.Value.UtcDateTime >= workDate.Date);

                entries.Add(new WorkEntry
                {
                    EmploymentContractId = contract.Id,
                    StartDateTime = new DateTimeOffset(workDate.Date.AddHours(startHour), TimeSpan.Zero),
                    EndDateTime = new DateTimeOffset(workDate.Date.AddHours(startHour + duration), TimeSpan.Zero),
                    BusinessTripId = trip?.Id,
                    DailyBusinessTripDetail = trip != null ? new DailyBusinessTripDetail
                    {
                        ProvidedBreakfast = random.Next(0, 2) == 1,
                        ProvidedLunch = random.Next(0, 2) == 1,
                        ProvidedDinner = random.Next(0, 2) == 1,
                        HasOvernightStay = random.Next(0, 2) == 1,
                        ActualAccommodationCostThisDay = random.Next(100, 400)
                    } : null,
                    TextNote = random.Next(0, 5) == 0 ? "Notatka z pracy" : null
                });
            }

            // Leave entries
            if (random.Next(0, 3) == 0)
            {
                var leaveStart = DateTimeOffset.UtcNow.AddDays(-random.Next(30, 80));
                var leaveDays = random.Next(1, 8);
                entries.Add(new LeaveEntry
                {
                    EmploymentContractId = contract.Id,
                    StartDateTime = leaveStart,
                    EndDateTime = leaveStart.AddDays(leaveDays),
                    LeaveType = LeaveType.Vacation,
                    WorkingDays = leaveDays,
                    TextNote = "Urlop wypoczynkowy"
                });
            }

            if (random.Next(0, 4) == 0)
            {
                var sickStart = DateTimeOffset.UtcNow.AddDays(-random.Next(20, 60));
                var sickDays = random.Next(1, 5);
                entries.Add(new LeaveEntry
                {
                    EmploymentContractId = contract.Id,
                    StartDateTime = sickStart,
                    EndDateTime = sickStart.AddDays(sickDays),
                    LeaveType = LeaveType.SickLeave,
                    WorkingDays = sickDays,
                    TextNote = "Zwolnienie lekarskie"
                });
            }
        }

        dbContext.TimeEntries.AddRange(entries);
        return entries;
    }

    private void SeedTimeEntryCalculations(List<TimeEntry> timeEntries, List<EmploymentContract> contracts)
    {
        var calculations = new List<TimeEntryCalculation>();

        foreach (var entry in timeEntries)
        {
            var contract = contracts.First(c => c.Id == entry.EmploymentContractId);

            if (entry is WorkEntry workEntry)
            {
                var hours = (decimal)(workEntry.EndDateTime - workEntry.StartDateTime).TotalHours;
                var shiftType = DetermineShiftType(workEntry);
                var multiplier = GetMultiplier(shiftType);
                var grossSalary = hours * contract.HourlyRate * multiplier;

                var zus = CalculateZus(grossSalary, contract.ContractType);
                var healthContribution = grossSalary * TaxRates.HealthRateDefaultValue;
                var taxBase = Math.Max(0, grossSalary - zus.total - TaxRates.StandardTaxDeductionMonthlyDefaultValue);
                var taxAmount = taxBase * TaxRates.LowerTaxRateDefaultValue;
                var netSalary = grossSalary - zus.total - taxAmount - healthContribution;

                calculations.Add(new WorkEntryCalculation
                {
                    TimeEntryId = entry.Id,
                    GrossSalary = Math.Round(grossSalary, 2),
                    PensionContribution = Math.Round(zus.pension, 2),
                    DisabilityContribution = Math.Round(zus.disability, 2),
                    SicknessContribution = Math.Round(zus.sickness, 2),
                    TotalZusContributions = Math.Round(zus.total, 2),
                    TaxDeduction = TaxRates.StandardTaxDeductionMonthlyDefaultValue,
                    TaxBase = Math.Round(taxBase, 2),
                    TaxAmount = Math.Round(taxAmount, 2),
                    HealthContribution = Math.Round(healthContribution, 2),
                    PpkEmployeeContribution = 0,
                    PpkEmployerContribution = 0,
                    NetSalary = Math.Round(netSalary, 2),
                    AppliedTaxDeductionType = TaxDeductionType.Standard,
                    ContractTypeUsed = contract.ContractType,
                    TotalHours = hours,
                    ShiftType = shiftType,
                    Multiplier = multiplier,
                    HourlyRateUsed = contract.HourlyRate
                });
            }
            else if (entry is LeaveEntry leaveEntry)
            {
                var dailyRate = contract.HourlyRate * 8;
                var grossSalary = leaveEntry.WorkingDays * dailyRate;

                var zus = CalculateZus(grossSalary, contract.ContractType);
                var healthContribution = grossSalary * TaxRates.HealthRateDefaultValue;
                var taxBase = Math.Max(0, grossSalary - zus.total);
                var taxAmount = taxBase * TaxRates.LowerTaxRateDefaultValue;
                var netSalary = grossSalary - zus.total - taxAmount - healthContribution;

                calculations.Add(new LeaveEntryCalculation
                {
                    TimeEntryId = entry.Id,
                    GrossSalary = Math.Round(grossSalary, 2),
                    PensionContribution = Math.Round(zus.pension, 2),
                    DisabilityContribution = Math.Round(zus.disability, 2),
                    SicknessContribution = Math.Round(zus.sickness, 2),
                    TotalZusContributions = Math.Round(zus.total, 2),
                    TaxDeduction = 0,
                    TaxBase = Math.Round(taxBase, 2),
                    TaxAmount = Math.Round(taxAmount, 2),
                    HealthContribution = Math.Round(healthContribution, 2),
                    PpkEmployeeContribution = 0,
                    PpkEmployerContribution = 0,
                    NetSalary = Math.Round(netSalary, 2),
                    AppliedTaxDeductionType = TaxDeductionType.None,
                    ContractTypeUsed = contract.ContractType,
                    WorkingDays = leaveEntry.WorkingDays,
                    DailyRate = dailyRate
                });
            }
        }

        dbContext.TimeEntryCalculations.AddRange(calculations);
    }

    private void SeedPaychecks(List<EmploymentContract> contracts)
    {
        var paychecks = new List<Paycheck>();
        var random = new Random(42);

        foreach (var contract in contracts.Where(c => c.IsActive))
        {
            var paymentDay = contract.PaymentDay ?? 10;

            for (var month = 1; month <= 6; month++)
            {
                var paymentDate = DateTimeOffset.UtcNow.AddMonths(-month);
                var grossAmount = random.Next(3000, 8000);
                var netAmount = grossAmount * 0.72m;

                paychecks.Add(new Paycheck
                {
                    EmploymentContractId = contract.Id,
                    PaymentDate = new DateOnly(paymentDate.Year, paymentDate.Month, Math.Min(paymentDay, 28)),
                    GrossAmount = grossAmount,
                    NetAmount = Math.Round(netAmount, 2)
                });
            }
        }

        dbContext.Paychecks.AddRange(paychecks);
    }

    private static ShiftType DetermineShiftType(WorkEntry entry)
    {
        if (entry.StartDateTime.DayOfWeek == DayOfWeek.Sunday) return ShiftType.SundayOrHoliday;
        if (entry.StartDateTime.DayOfWeek == DayOfWeek.Saturday) return ShiftType.Saturday;
        if (entry.StartDateTime.Hour >= 22 || entry.EndDateTime.Hour <= 6) return ShiftType.Night;
        if ((entry.EndDateTime - entry.StartDateTime).TotalHours > 8) return ShiftType.Overtime;
        return ShiftType.Normal;
    }

    private static decimal GetMultiplier(ShiftType shiftType) => shiftType switch
    {
        ShiftType.Overtime => 1.5m,
        ShiftType.Night => 1.2m,
        ShiftType.Saturday => 1.5m,
        ShiftType.SundayOrHoliday => 2.0m,
        _ => 1.0m
    };

    private static (decimal pension, decimal disability, decimal sickness, decimal total)
        CalculateZus(decimal gross, ContractType type)
    {
        if (type is ContractType.Uz or ContractType.B2B or ContractType.UoD)
            return (0, 0, 0, 0);

        var pension = gross * TaxRates.PensionRateDefaultValue;
        var disability = gross * TaxRates.DisabilityRateDefaultValue;
        var sickness = gross * TaxRates.SicknessRateDefaultValue;
        return (pension, disability, sickness, pension + disability + sickness);
    }
}
