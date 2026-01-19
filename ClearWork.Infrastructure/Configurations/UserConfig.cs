using ClearWork.Domain.Constants;
using ClearWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearWork.Infrastructure.Configurations;

internal class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.Property(u => u.FirstName)
            .HasMaxLength(50);
        
        builder.Property(u => u.LastName)
            .HasMaxLength(50);

        builder.Property(u => u.DateOfBirth)
            .HasColumnType(SqlDefaults.DateType);

        builder.Property(u => u.DisabilityLevel)
            .HasConversion<string>()
            .HasColumnType("varchar");
        
        builder.Property(u => u.CreatedAt)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone)
            .HasDefaultValueSql(SqlDefaults.CurrentUtcTimestamp);
    }
}