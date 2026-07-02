namespace AIWorkHub.SharedKernel.Entities;

/// <summary>
/// Base type for entities that track creation and modification metadata.
/// </summary>
public abstract class AuditableEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the UTC creation timestamp.
    /// </summary>
    public DateTimeOffset CreatedAtUtc { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user that created the entity.
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the UTC last modification timestamp.
    /// </summary>
    public DateTimeOffset? LastModifiedAtUtc { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user that last modified the entity.
    /// </summary>
    public string? LastModifiedBy { get; set; }
}
