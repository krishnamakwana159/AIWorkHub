using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIWorkHub.Persistence.Configurations;

public class TaskTagConfiguration : IEntityTypeConfiguration<TaskTag>
{
    public void Configure(EntityTypeBuilder<TaskTag> builder)
    {
        // 1. Primary Key Configuration
        builder.HasKey(tt => tt.Id);

        // 2. Fix SQL Server Error 1785 (Prevent Cascade Delete Cycles)
        builder.HasOne(tt => tt.WorkTask)
            .WithMany(t => t.TaskTags)
            .HasForeignKey(tt => tt.WorkTaskId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(tt => tt.Tag)
            .WithMany(t => t.TaskTags)
            .HasForeignKey(tt => tt.TagId)
            .OnDelete(DeleteBehavior.Cascade);

        // 3. Fix Query Filter Warning
        builder.HasQueryFilter(tt => !tt.IsDeleted);
    }
}
