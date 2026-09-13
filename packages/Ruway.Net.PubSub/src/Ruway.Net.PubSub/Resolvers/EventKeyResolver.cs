using Ruway.Net.Core.Abstractions.Attributes;
using Ruway.Net.Core.Abstractions.DomainContracts;

namespace Ruway.Net.PubSub.Resolvers;


internal sealed class EventKeyResolver
{
    public string? TryResolve(IDomainEvent @event)
    {
        ArgumentNullException.ThrowIfNull(@event);

        var eventType = @event.GetType();

        var attribute = eventType
            .GetCustomAttributes(typeof(EventKeyAttribute), false)
            .FirstOrDefault() as EventKeyAttribute;

        return attribute is null
            ? null
            : BuildKey(attribute);
    }

    private static string BuildKey(
        EventKeyAttribute attribute)
    {
        if (string.IsNullOrWhiteSpace(attribute.AppName))
        {
            return $"{attribute.Entity}." +
                   $"{attribute.Event}." +
                   $"{attribute.Version}";
        }

        return $"{attribute.AppName}." +
               $"{attribute.Entity}." +
               $"{attribute.Event}." +
               $"{attribute.Version}";
    }
}