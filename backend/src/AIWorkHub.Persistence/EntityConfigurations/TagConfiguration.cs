using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIWorkHub.Persistence.EntityConfigurations;

public sealed class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Color)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.ProjectId,
            x.Name
        }).IsUnique();

        builder.HasOne(x => x.Project)
            .WithMany(x => x.Tags)
            .HasForeignKey(x => x.ProjectId);
    }
}
    