using Microsoft.Extensions.DependencyInjection;
using Ruway.Net.Core.Abstractions.DomainContracts;
using Ruway.Net.PubSub.Abstractions;

namespace Ruway.Net.PubSub.Dispatchers;


internal sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken = default)
        where TEvent : IDomainEvent
    {
        ArgumentNullException.ThrowIfNull(@event);

        var handlers = _serviceProvider
            .GetServices<IEventHandler<TEvent>>();

        foreach (var handler in handlers)
        {
            await handler.HandleAsync(
                @event,
                cancellationToken);
        }
    }

    public async Task DispatchAsync(
        IReadOnlyCollection<IDomainEvent> events,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(events);

        foreach (var @event in events)
        {
            await DispatchEventAsync(
                @event,
                cancellationToken);
        }
    }

    private async Task DispatchEventAsync(
        IDomainEvent @event,
        CancellationToken cancellationToken)
    {
        var eventType = @event.GetType();

        var handlerType = typeof(IEventHandler<>)
            .MakeGenericType(eventType);

        var handlers = _serviceProvider
            .GetServices(handlerType);

        foreach (var handler in handlers)
        {
            if (handler is null)
                continue;

            var method = handlerType.GetMethod(
                nameof(IEventHandler<IDomainEvent>.HandleAsync));

            if (method is null)
                continue;

            var task = method.Invoke(
                handler,
                new object?[]
                {
                    @event,
                    cancellationToken
                }) as Task;

            if (task is not null)
            {
                await task;
            }
        }
    }
}