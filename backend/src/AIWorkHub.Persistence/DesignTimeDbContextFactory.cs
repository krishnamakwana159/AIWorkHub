using AIWorkHub.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AIWorkHub.Persistence;

/// <summary>
/// Creates <see cref="AppDbContext"/> instances for EF Core design-time tooling.
/// </summary>
public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    /// <inheritdoc />
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AIWorkHub;Trusted_Connection=True;TrustServerCertificate=True;");

        return new AppDbContext(optionsBuilder.Options, new DesignTimeDateTimeProvider(), new DesignTimeCurrentUserService());
    }

    private sealed class DesignTimeDateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }

    private sealed class DesignTimeCurrentUserService : ICurrentUserService
    {
        public string? UserId => "design-time";

        public bool IsAuthenticated => false;
    }
}
