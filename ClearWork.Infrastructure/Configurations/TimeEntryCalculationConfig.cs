using ClearWork.Domain.Constants;
using ClearWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearWork.Infrastructure.Configurations;

internal class TimeEntryCalculationConfig : IEntityTypeConfiguration<TimeEntryCalculation>
{
    public void Configure(EntityTypeBuilder<TimeEntryCalculation> builder)
    {
        builder.Property(tec => tec.CalculatedAt)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone)
            .HasDefaultValueSql(SqlDefaults.CurrentUtcTimestamp);

        builder.Property(tec => tec.GrossSalary)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(tec => tec.PensionContribution)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(tec => tec.DisabilityContribution)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(tec => tec.SicknessContribution)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(tec => tec.TotalZusContributions)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(tec => tec.TaxDeduction)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(tec => tec.TaxBase)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(tec => tec.TaxAmount)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(tec => tec.HealthContribution)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(tec => tec.PpkEmployeeContribution)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(tec => tec.PpkEmployerContribution)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(tec => tec.NetSalary)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);

        builder.Property(tec => tec.AppliedTaxDeductionType)
            .HasConversion<string>()
            .HasColumnType("varchar");
        
        builder.Property(tec => tec.ContractTypeUsed)
            .HasConversion<string>()
            .HasColumnType("varchar");
        
        builder.HasDiscriminator<string>("CalculationType")
            .HasValue<WorkEntryCalculation>("Work")
            .HasValue<LeaveEntryCalculation>("Leave");
    }
}