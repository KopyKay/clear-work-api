using ClearWork.Domain.Constants;
using ClearWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearWork.Infrastructure.Configurations;

internal class EmploymentContractConfig : IEntityTypeConfiguration<EmploymentContract>
{
    public void Configure(EntityTypeBuilder<EmploymentContract> builder)
    {
        builder.Property(ec => ec.ContractType)
            .HasConversion<string>()
            .HasColumnType("varchar");
        
        builder.Property(ec => ec.EmploymentLevel)
            .HasConversion<string>()
            .HasColumnType("varchar");

        builder.Property(ec => ec.HourlyRate)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);

        builder.Property(ec => ec.PaymentFrequency)
            .HasConversion<string>()
            .HasColumnType("varchar");
        
        builder.Property(ec => ec.StartDateTime)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone);
        
        builder.Property(ec => ec.EndDateTime)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone);
        
        builder.Property(ec => ec.CreatedAt)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone)
            .HasDefaultValueSql(SqlDefaults.CurrentUtcTimestamp);
        
        builder.HasMany(ec => ec.Paychecks)
            .WithOne(p => p.EmploymentContract)
            .HasForeignKey(p => p.EmploymentContractId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(ec => ec.TimeEntries)
            .WithOne(te => te.EmploymentContract)
            .HasForeignKey(te => te.EmploymentContractId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(ec => ec.BusinessTrips)
            .WithOne(bt => bt.EmploymentContract)
            .HasForeignKey(bt => bt.EmploymentContractId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}