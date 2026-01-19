using ClearWork.Domain.Constants;
using ClearWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearWork.Infrastructure.Configurations;

internal class BusinessTripConfig : IEntityTypeConfiguration<BusinessTrip>
{
    public void Configure(EntityTypeBuilder<BusinessTrip> builder)
    {
        builder.Property(bt => bt.StartDateTime)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone);

        builder.Property(bt => bt.EndDateTime)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone);
        
        builder.Property(bt => bt.TripType)
            .HasConversion<string>()
            .HasColumnType("varchar");

        builder.Property(bt => bt.DestinationCountry)
            .HasMaxLength(3);
        
        builder.Property(bt => bt.AccommodationType)
            .HasConversion<string>()
            .HasColumnType("varchar");
        
        builder.Property(bt => bt.TransportType)
            .HasConversion<string>()
            .HasColumnType("varchar");

        builder.Property(bt => bt.KilometersDriven)
            .HasPrecision(DecimalPrecisions.TransportCurrency, DecimalPrecisions.TransportScale);
        
        builder.Property(bt => bt.ActualTransportCost)
            .HasPrecision(DecimalPrecisions.TransportCurrency, DecimalPrecisions.TransportScale);

        builder.Property(bt => bt.CreatedAt)
            .HasColumnType(SqlDefaults.TimestampWithTimeZone)
            .HasDefaultValueSql(SqlDefaults.CurrentUtcTimestamp);
    }
}