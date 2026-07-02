namespace AIWorkHub.SharedKernel.Entities;

/// <summary>
/// Represents an entity with a stable identifier.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets the entity identifier.
    /// </summary>
    Guid Id { get; }
}
