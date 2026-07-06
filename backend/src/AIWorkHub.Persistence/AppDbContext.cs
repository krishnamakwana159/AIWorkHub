using System.Linq.Expressions;
using AIWorkHub.SharedKernel.Entities;
using AIWorkHub.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Persistence;

/// <summary>
/// EF Core database context for AIWorkHub.
/// </summary>
public sealed class AppDbContext(
    DbContextOptions<AppDbContext> options,
    IDateTimeProvider dateTimeProvider,
    ICurrentUserService currentUserService)
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<UserRole> UserRoles => Set<UserRole>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        ApplySoftDeleteQueryFilters(modelBuilder);
    }

    /// <inheritdoc />
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInformation();

        return base.SaveChangesAsync(cancellationToken);
    }

    private static void ApplySoftDeleteQueryFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(SoftDeleteEntity).IsAssignableFrom(entityType.ClrType))
            {
                continue;
            }

            var parameter = Expression.Parameter(entityType.ClrType, "entity");
            var property = Expression.Property(parameter, nameof(SoftDeleteEntity.IsDeleted));
            var comparison = Expression.Equal(property, Expression.Constant(false));
            var lambda = Expression.Lambda(comparison, parameter);

            entityType.SetQueryFilter(lambda);
        }
    }

    private void ApplyAuditInformation()
    {
        var utcNow = dateTimeProvider.UtcNow;
        //var currentUserId = currentUserService.UserId;
        var currentUserId = currentUserService.UserId ?? "system";

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAtUtc = utcNow;
                entry.Entity.CreatedBy = currentUserId;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedAtUtc = utcNow;
                entry.Entity.LastModifiedBy = currentUserId;
            }
        }

        foreach (var entry in ChangeTracker.Entries<SoftDeleteEntity>())
        {
            if (entry.State != EntityState.Deleted)
            {
                continue;
            }

            entry.State = EntityState.Modified;
            entry.Entity.IsDeleted = true;
            entry.Entity.DeletedAtUtc = utcNow;
            entry.Entity.DeletedBy = currentUserId;
        }
    }
}
