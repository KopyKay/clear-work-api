using ClearWork.Domain.Constants;
using ClearWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearWork.Infrastructure.Configurations;

internal class AnnualTaxRateConfig : IEntityTypeConfiguration<AnnualTaxRate>
{
    public void Configure(EntityTypeBuilder<AnnualTaxRate> builder)
    {
        builder.HasIndex(gtr => new { gtr.UserId, gtr.Year }).IsUnique();
        
        builder.Property(gtr => gtr.PensionRate)
            .HasPrecision(DecimalPrecisions.PercentageCurrency, DecimalPrecisions.PercentageScale)
            .HasDefaultValue(TaxRates.PensionRateDefaultValue);
        
        builder.Property(gtr => gtr.DisabilityRate)
            .HasPrecision(DecimalPrecisions.PercentageCurrency, DecimalPrecisions.PercentageScale)
            .HasDefaultValue(TaxRates.DisabilityRateDefaultValue);
        
        builder.Property(gtr => gtr.SicknessRate)
            .HasPrecision(DecimalPrecisions.PercentageCurrency, DecimalPrecisions.PercentageScale)
            .HasDefaultValue(TaxRates.SicknessRateDefaultValue);
        
        builder.Property(gtr => gtr.HealthRate)
            .HasPrecision(DecimalPrecisions.PercentageCurrency, DecimalPrecisions.PercentageScale)
            .HasDefaultValue(TaxRates.HealthRateDefaultValue);
        
        builder.Property(gtr => gtr.TaxFreeAmount)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale)
            .HasDefaultValue(TaxRates.TaxFreeAmountDefaultValue);
        
        builder.Property(gtr => gtr.TaxThreshold)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale)
            .HasDefaultValue(TaxRates.TaxThresholdDefaultValue);
        
        builder.Property(gtr => gtr.LowerTaxRate)
            .HasPrecision(DecimalPrecisions.PercentageCurrency, DecimalPrecisions.PercentageScale)
            .HasDefaultValue(TaxRates.LowerTaxRateDefaultValue);
        
        builder.Property(gtr => gtr.HigherTaxRate)
            .HasPrecision(DecimalPrecisions.PercentageCurrency, DecimalPrecisions.PercentageScale)
            .HasDefaultValue(TaxRates.HigherTaxRateDefaultValue);
        
        builder.Property(gtr => gtr.StandardTaxDeductionMonthly)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale)
            .HasDefaultValue(TaxRates.StandardTaxDeductionMonthlyDefaultValue);
        
        builder.Property(gtr => gtr.YoungPersonTaxReliefLimit)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale)
            .HasDefaultValue(TaxRates.YoungPersonTaxReliefLimitDefaultValue);

        builder.Property(gtr => gtr.CreatedAt)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone)
            .HasDefaultValueSql(SqlDefaults.CurrentUtcTimestamp);

        builder.HasOne(atr => atr.User)
            .WithMany(u => u.AnnualTaxRate)
            .HasForeignKey(atr => atr.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}