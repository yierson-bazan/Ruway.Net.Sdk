namespace Ruway.Net.Core.Abstractions.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class EventKeyAttribute : Attribute
{
    public string Entity { get; }

    public string Version { get; }

    public string Event { get; }

    public string? AppName { get; }

    public EventKeyAttribute(
        string entity,
        ushort version,
        string @event,
        string? appName = null)
    {
        if (string.IsNullOrWhiteSpace(entity))
            throw new ArgumentException(
                "Entity cannot be empty.",
                nameof(entity));

        if (string.IsNullOrWhiteSpace(@event))
            throw new ArgumentException(
                "Event cannot be empty.",
                nameof(@event));

        Entity = entity;
        Version = $"v{version}";
        Event = @event;
        AppName = appName;
    }
}