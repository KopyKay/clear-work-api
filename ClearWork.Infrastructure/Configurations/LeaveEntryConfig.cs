using ClearWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearWork.Infrastructure.Configurations;

internal class LeaveEntryConfig : IEntityTypeConfiguration<LeaveEntry>
{
    public void Configure(EntityTypeBuilder<LeaveEntry> builder)
    {
        builder.Property(le => le.LeaveType)
            .IsRequired()
            .HasConversion<string>()
            .HasColumnType("varchar");
    }
}