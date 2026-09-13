namespace Ruway.Net.Core.Abstractions.DomainContracts;

public interface IAggregateRoot<TKey> : IEntity<TKey>
{
    /// <summary>
    /// Gets and clears the domain events associated with the aggregate root.
    /// </summary>
    /// <returns>A read-only list of domain events.</returns>
    //IReadOnlyList<IDomainEvent> GetAndClearEvents();
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
}