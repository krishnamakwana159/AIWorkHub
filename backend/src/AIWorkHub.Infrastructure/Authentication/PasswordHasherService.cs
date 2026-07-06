using AIWorkHub.Application.Interfaces;
using AIWorkHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AIWorkHub.Infrastructure.Authentication;

public sealed class PasswordHasherService: IPasswordHasher
{
    private readonly PasswordHasher <User> _passwordHasher = new();

    public string HashPassword(string password) {
        return _passwordHasher.HashPassword(new User(), password);
    }

    public bool VerifyPassword(string password, string passwordHash) {
        var result = _passwordHasher.VerifyHashedPassword(new User(), passwordHash, password);
        return result != PasswordVerificationResult.Failed;
    }
}
