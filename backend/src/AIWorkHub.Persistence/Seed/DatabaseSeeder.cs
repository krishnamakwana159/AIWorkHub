using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using AIWorkHub.SharedKernel.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AIWorkHub.Persistence.Seed;

/// <summary>
/// Ensures the Administrator/User roles exist and that at least one
/// Administrator account exists, since there is otherwise no way to
/// bootstrap the first admin. Safe to run on every startup - every step
/// checks for existence first.
/// </summary>
public sealed class DatabaseSeeder(
    AppDbContext dbContext,
    IRoleRepository roleRepository,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IConfiguration configuration,
    ILogger<DatabaseSeeder> logger)
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var administratorRole = await EnsureRoleAsync(
            ApplicationRoles.Administrator,
            cancellationToken);

        await EnsureRoleAsync(ApplicationRoles.User, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        var hasAdministrator = await dbContext.UserRoles
            .AnyAsync(x => x.RoleId == administratorRole.Id, cancellationToken);

        if (hasAdministrator)
            return;

        var adminEmail = configuration["Admin:Email"];
        var adminPassword = configuration["Admin:Password"];

        if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
        {
            logger.LogWarning(
                "No Administrator account exists and Admin:Email/Admin:Password " +
                "are not configured, so none was created. Set them in " +
                "appsettings or environment variables to bootstrap an admin.");
            return;
        }

        var normalizedEmail = adminEmail.Trim().ToLowerInvariant();

        var existingUser = await userRepository.GetByEmailAsync(
            normalizedEmail,
            cancellationToken);

        if (existingUser is not null)
        {
            existingUser.UserRoles.Add(new UserRole
            {
                UserId = existingUser.Id,
                RoleId = administratorRole.Id
            });

            await dbContext.SaveChangesAsync(cancellationToken);

            logger.LogInformation(
                "Granted the Administrator role to existing user {Email}.",
                normalizedEmail);

            return;
        }

        var adminUser = new User
        {
            FirstName = "Admin",
            LastName = "User",
            Email = normalizedEmail,
            PasswordHash = passwordHasher.HashPassword(adminPassword),
            IsActive = true
        };

        adminUser.UserRoles.Add(new UserRole
        {
            RoleId = administratorRole.Id
        });

        await userRepository.AddAsync(adminUser, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "Seeded a bootstrap Administrator account for {Email}.",
            normalizedEmail);
    }

    private async Task<Role> EnsureRoleAsync(
        string name,
        CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByNameAsync(name, cancellationToken);

        if (role is not null)
            return role;

        role = new Role { Name = name };

        await roleRepository.AddAsync(role, cancellationToken);

        return role;
    }
}
