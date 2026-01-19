using ClearWork.Domain.Constants;
using ClearWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearWork.Infrastructure.Configurations;

internal class WorkplaceConfig : IEntityTypeConfiguration<Workplace>
{
    public void Configure(EntityTypeBuilder<Workplace> builder)
    {
        builder.Property(wp => wp.Name)
            .HasMaxLength(255);
        
        builder.Property(wp => wp.BaseHourlyRate)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(wp => wp.CreatedAt)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone)
            .HasDefaultValueSql(SqlDefaults.CurrentUtcTimestamp);

        builder.OwnsOne(wp => wp.PpkSettings, owned =>
        {
            owned.Property(ps => ps.EmployeeRate)
                .HasPrecision(DecimalPrecisions.PercentageCurrency, DecimalPrecisions.PercentageScale)
                .HasDefaultValue(PpkRates.PpkEmployeeRateDefaultValue);
            
            owned.Property(ps => ps.EmployerRate)
                .HasPrecision(DecimalPrecisions.PercentageCurrency, DecimalPrecisions.PercentageScale)
                .HasDefaultValue(PpkRates.PpkEmployerRateDefaultValue);
        });

        builder.HasOne(wp => wp.User)
            .WithMany(u => u.Workplaces)
            .HasForeignKey(wp => wp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(wp => wp.EmploymentContracts)
            .WithOne(ec => ec.Workplace)
            .HasForeignKey(ec => ec.WorkplaceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}