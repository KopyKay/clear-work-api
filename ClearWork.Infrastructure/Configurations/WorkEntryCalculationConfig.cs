using ClearWork.Domain.Constants;
using ClearWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearWork.Infrastructure.Configurations;

internal class WorkEntryCalculationConfig : IEntityTypeConfiguration<WorkEntryCalculation>
{
    public void Configure(EntityTypeBuilder<WorkEntryCalculation> builder)
    {
        builder.Property(wec => wec.TotalHours)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(wec => wec.ShiftType)
            .HasConversion<string>()
            .HasColumnType("varchar");
        
        builder.Property(wec => wec.Multiplier)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(wec => wec.HourlyRateUsed)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
    }
}