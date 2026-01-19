using ClearWork.Domain.Constants;
using ClearWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearWork.Infrastructure.Configurations;

internal class LeaveEntryCalculationConfig : IEntityTypeConfiguration<LeaveEntryCalculation>
{
    public void Configure(EntityTypeBuilder<LeaveEntryCalculation> builder)
    {
        builder.Property(lec => lec.DailyRate)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
    }
}