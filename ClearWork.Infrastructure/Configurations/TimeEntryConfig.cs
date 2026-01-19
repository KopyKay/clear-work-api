using ClearWork.Domain.Constants;
using ClearWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearWork.Infrastructure.Configurations;

internal class TimeEntryConfig : IEntityTypeConfiguration<TimeEntry>
{
    public void Configure(EntityTypeBuilder<TimeEntry> builder)
    {
        builder.Property(te => te.StartDateTime)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone);
        
        builder.Property(te => te.EndDateTime)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone);
        
        builder.Property(te => te.TextNote)
            .HasMaxLength(2500);
        
        builder.Property(te => te.CreatedAt)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone)
            .HasDefaultValueSql(SqlDefaults.CurrentUtcTimestamp);
        
        builder.Property(te => te.ModifiedAt)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone);
        
        builder.HasDiscriminator<string>("EntryType")
            .HasValue<WorkEntry>("Work")
            .HasValue<LeaveEntry>("Leave");
        
        builder.OwnsOne(te => te.AudioNote, owned =>
        {
            owned.Property(an => an.AudioData)
                .IsRequired();
            
            owned.Property(an => an.FileFormat)
                .HasMaxLength(3)
                .HasDefaultValue("m4a");
            
            owned.Property(an => an.DurationSeconds)
                .IsRequired();
            
            owned.Property(an => an.RecordedAt)
                .HasColumnType(SqlDefaults.TimestampWithTimeZone)
                .HasDefaultValueSql(SqlDefaults.CurrentUtcTimestamp);
        });

        builder.HasOne(te => te.Calculation)
            .WithOne(tec => tec.TimeEntry)
            .HasForeignKey<TimeEntryCalculation>(tec => tec.TimeEntryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}