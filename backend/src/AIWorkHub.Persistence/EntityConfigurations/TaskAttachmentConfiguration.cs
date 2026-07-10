using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIWorkHub.Persistence.EntityConfigurations;

public sealed class TaskAttachmentConfiguration
    : IEntityTypeConfiguration<TaskAttachment>
{
    public void Configure(EntityTypeBuilder<TaskAttachment> builder)
    {
        builder.ToTable("TaskAttachments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FileName)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.StoredFileName)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.ContentType)
            .HasMaxLength(150);

        builder.Property(x => x.FilePath)
            .HasMaxLength(500);

        builder.HasOne(x => x.Task)
            .WithMany(x => x.Attachments)
            .HasForeignKey(x => x.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.UploadedByUser)
            .WithMany()
            .HasForeignKey(x => x.UploadedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
