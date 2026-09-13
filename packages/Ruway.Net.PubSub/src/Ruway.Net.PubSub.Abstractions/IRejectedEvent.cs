namespace Ruway.Net.PubSub.Abstractions;

public interface IRejectedEvent
{
    string Reason { get; }
    string Code { get; }
}