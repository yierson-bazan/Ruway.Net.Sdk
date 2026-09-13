using Ruway.Net.Core.Abstractions.DomainContracts;

namespace Ruway.Net.Core.Abstractions.Domains;

public abstract record DomainEvent<TKey> : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();

    public DateTimeOffset OccurredAt { get; } = DateTimeOffset.UtcNow;

    public TKey AggregateId { get; }

    protected DomainEvent(TKey aggregateId)
    {
        AggregateId = aggregateId;
    }
}
