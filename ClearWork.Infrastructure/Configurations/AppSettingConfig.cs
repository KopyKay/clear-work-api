using ClearWork.Domain.Constants;
using ClearWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearWork.Infrastructure.Configurations;

internal class AppSettingConfig : IEntityTypeConfiguration<AppSetting>
{
    public void Configure(EntityTypeBuilder<AppSetting> builder)
    {
        builder.HasIndex(@as => @as.UserId).IsUnique();
        
        builder.Property(@as => @as.UserId)
            .IsRequired();

        builder.Property(@as => @as.PushNotificationReminderTime)
            .HasColumnType(SqlDefaults.TimeType);
        
        builder.Property(@as => @as.DarkModeEnableTime)
            .HasColumnType(SqlDefaults.TimeType);
        
        builder.Property(@as => @as.DarkModeDisableTime)
            .HasColumnType(SqlDefaults.TimeType);

        builder.HasOne(@as => @as.User)
            .WithOne(u => u.AppSetting)
            .HasForeignKey<AppSetting>(@as => @as.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}