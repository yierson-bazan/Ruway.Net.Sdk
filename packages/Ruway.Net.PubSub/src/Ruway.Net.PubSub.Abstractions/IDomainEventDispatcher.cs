using Ruway.Net.Core.Abstractions.DomainContracts;

namespace Ruway.Net.PubSub.Abstractions;

public interface IDomainEventDispatcher
{
    Task DispatchAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken = default)
        where TEvent : IDomainEvent;

    Task DispatchAsync(
        IReadOnlyCollection<IDomainEvent> events,
        CancellationToken cancellationToken = default);
}