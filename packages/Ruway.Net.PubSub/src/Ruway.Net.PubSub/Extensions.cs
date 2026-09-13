using Microsoft.Extensions.DependencyInjection;
using Ruway.Net.PubSub.Abstractions;
using Ruway.Net.PubSub.Dispatchers;
using Ruway.Net.PubSub.Publishers;
using Ruway.Net.PubSub.Resolvers;

namespace Ruway.Net.PubSub;

public static class Extensions
{
    public static IServiceCollection AddRuwayPubSub(
        this IServiceCollection services)
    {
        services.AddSingleton<EventKeyResolver>();

        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();

        services.AddSingleton<IEventPublisher, InMemoryEventPublisher>();

        return services;
    }
}