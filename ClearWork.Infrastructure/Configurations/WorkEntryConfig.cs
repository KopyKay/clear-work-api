using ClearWork.Domain.Constants;
using ClearWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearWork.Infrastructure.Configurations;

internal class WorkEntryConfig : IEntityTypeConfiguration<WorkEntry>
{
    public void Configure(EntityTypeBuilder<WorkEntry> builder)
    {
        builder.OwnsOne(we => we.DailyBusinessTripDetail, owned =>
        {
            owned.Property(dbtd => dbtd.ActualAccommodationCostThisDay)
                .HasPrecision(DecimalPrecisions.TransportCurrency, DecimalPrecisions.TransportScale);
        });

        builder.HasOne(we => we.BusinessTrip)
            .WithMany(bt => bt.WorkEntries)
            .HasForeignKey(we => we.BusinessTripId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}