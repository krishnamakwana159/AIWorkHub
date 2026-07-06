using AIWorkHub.SharedKernel.Entities;

namespace AIWorkHub.Domain.Entities;

public class RefreshToken : AuditableEntity
{
    public string Token { get; set; } = string.Empty;

    public DateTime ExpiresAtUtc { get; set; }

    public bool IsRevoked { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
}
