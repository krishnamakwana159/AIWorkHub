namespace AIWorkHub.SharedKernel.Entities;

/// <summary>
/// Base type for entities.
/// </summary>
public abstract class BaseEntity : IEntity
{
    /// <summary>
    /// Gets or sets the entity identifier.
    /// </summary>
    public Guid Id { get; protected set; } = Guid.NewGuid();
}
