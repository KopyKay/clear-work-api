using ClearWork.Domain.Constants;
using ClearWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearWork.Infrastructure.Configurations;

internal class PaycheckConfig : IEntityTypeConfiguration<Paycheck>
{
    public void Configure(EntityTypeBuilder<Paycheck> builder)
    {
        builder.Property(pr => pr.PaymentDate)
            .HasColumnType(SqlDefaults.DateType);
        
        builder.Property(pr => pr.GrossAmount)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(pr => pr.NetAmount)
            .HasPrecision(DecimalPrecisions.StandardMoneyCurrency, DecimalPrecisions.StandardMoneyScale);
        
        builder.Property(pr => pr.CreatedAt)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone)
            .HasDefaultValueSql(SqlDefaults.CurrentUtcTimestamp);

        builder.Property(pr => pr.ModifiedAt)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone);
    }
}