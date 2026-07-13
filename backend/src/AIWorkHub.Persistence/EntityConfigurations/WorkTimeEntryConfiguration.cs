using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIWorkHub.Persistence.EntityConfigurations;

public sealed class WorkTimeEntryConfiguration
    : IEntityTypeConfiguration<WorkTimeEntry>
{
    public void Configure(EntityTypeBuilder<WorkTimeEntry> builder)
    {
        builder.ToTable("WorkTimeEntries");

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.Hours)
            .HasPrecision(8,2);

        builder.HasOne(x => x.WorkTask)
            .WithMany(x => x.TimeEntries)
            .HasForeignKey(x => x.WorkTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
            .WithMany(x => x.TimeEntries)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
