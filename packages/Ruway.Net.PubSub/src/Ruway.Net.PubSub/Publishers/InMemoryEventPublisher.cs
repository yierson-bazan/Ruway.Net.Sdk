using Ruway.Net.Core.Abstractions.DomainContracts;
using Ruway.Net.PubSub.Abstractions;
using Ruway.Net.PubSub.Resolvers;

namespace Ruway.Net.PubSub.Publishers;


internal sealed class InMemoryEventPublisher : IEventPublisher
{
    private readonly EventKeyResolver _eventKeyResolver;

    public InMemoryEventPublisher(
        EventKeyResolver eventKeyResolver)
    {
        _eventKeyResolver = eventKeyResolver;
    }

    public Task PublishAsync(
        IDomainEvent @event,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(@event);

        var eventKey = _eventKeyResolver.TryResolve(@event);

        // Por ahora solamente resolvemos la key.
        // La publicación real llegará después.

        return Task.CompletedTask;
    }

    public async Task PublishAsync(
        IReadOnlyCollection<IDomainEvent> events,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(events);

        foreach (var @event in events)
        {
            await PublishAsync(
                @event,
                cancellationToken);
        }
    }
}