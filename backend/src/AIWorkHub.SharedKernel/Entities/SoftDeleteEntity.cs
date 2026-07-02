namespace AIWorkHub.SharedKernel.Entities;

/// <summary>
/// Base type for auditable entities that support soft deletion.
/// </summary>
public abstract class SoftDeleteEntity : AuditableEntity
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity is deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the UTC deletion timestamp.
    /// </summary>
    public DateTimeOffset? DeletedAtUtc { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user that deleted the entity.
    /// </summary>
    public string? DeletedBy { get; set; }
}
