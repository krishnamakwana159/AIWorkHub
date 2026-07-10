using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIWorkHub.Persistence.EntityConfigurations;

public sealed class ActivityLogConfiguration
    : IEntityTypeConfiguration<ActivityLog>
{
    public void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        builder.ToTable("ActivityLogs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.EntityType)
            .HasConversion<int>();

        builder.Property(x => x.Action)
            .HasConversion<int>();

        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
