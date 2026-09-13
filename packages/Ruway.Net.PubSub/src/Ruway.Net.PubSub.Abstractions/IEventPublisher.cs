using Ruway.Net.Core.Abstractions.DomainContracts;

namespace Ruway.Net.PubSub.Abstractions;

public interface IEventPublisher
{
    Task PublishAsync(
        IDomainEvent @event,
        CancellationToken cancellationToken = default);

    Task PublishAsync(
        IReadOnlyCollection<IDomainEvent> events,
        CancellationToken cancellationToken = default);
}

