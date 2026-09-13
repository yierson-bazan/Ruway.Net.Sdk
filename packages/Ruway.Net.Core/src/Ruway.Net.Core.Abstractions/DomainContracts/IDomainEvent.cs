namespace Ruway.Net.Core.Abstractions.DomainContracts;

public interface IDomainEvent
{
    Guid EventId { get; }

    DateTimeOffset OccurredAt { get; }
}
