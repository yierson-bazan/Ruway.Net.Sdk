using Ruway.Net.Core.Abstractions.DomainContracts;

namespace Ruway.Net.PubSub.Abstractions;

public interface IEventHandler<in TEvent>
    where TEvent : IDomainEvent
{
    Task HandleAsync(
        TEvent @event,
        CancellationToken cancellationToken = default);
}
