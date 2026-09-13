using Ruway.Net.Core.Abstractions.DomainContracts;

namespace Ruway.Net.PubSub.Abstractions;

public interface IEventQueue
{
    ValueTask EnqueueAsync(
        IDomainEvent @event,
        CancellationToken cancellationToken = default);
}
