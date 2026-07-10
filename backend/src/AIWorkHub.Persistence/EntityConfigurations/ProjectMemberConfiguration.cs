using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIWorkHub.Persistence.EntityConfigurations;

public sealed class ProjectMemberConfiguration
    : IEntityTypeConfiguration<ProjectMember>
{
    public void Configure(EntityTypeBuilder<ProjectMember> builder)
    {
        builder.HasIndex(x => new
        {
            x.ProjectId,
            x.UserId
        }).IsUnique();

        builder.HasOne(x => x.Project)
            .WithMany(x => x.Members)
            .HasForeignKey(x => x.ProjectId);

        builder.HasOne(x => x.User)
            .WithMany(x => x.ProjectMemberships)
            .HasForeignKey(x => x.UserId);

        builder.Property(x => x.Role)
            .HasConversion<int>();
    }
}
