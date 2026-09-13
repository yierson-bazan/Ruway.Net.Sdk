namespace Ruway.Net.Core.Abstractions.DomainContracts;

public interface IAuditable
{
    DateTimeOffset CreatedAt { get; }
    string? CreatedBy { get; }

    DateTimeOffset? UpdatedAt { get; }
    string? UpdatedBy { get; }
}