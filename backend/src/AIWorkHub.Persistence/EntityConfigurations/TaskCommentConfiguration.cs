using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIWorkHub.Persistence.EntityConfigurations;

public sealed class TaskCommentConfiguration
    : IEntityTypeConfiguration<TaskComment>
{
    public void Configure(EntityTypeBuilder<TaskComment> builder)
    {
        builder.ToTable("TaskComments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Comment)
            .HasMaxLength(4000)
            .IsRequired();

        builder
            .HasOne(x => x.Task)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
