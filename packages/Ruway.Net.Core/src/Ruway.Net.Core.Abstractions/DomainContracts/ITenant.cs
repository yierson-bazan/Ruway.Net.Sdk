namespace Ruway.Net.Core.Abstractions.DomainContracts;

public interface ITenant
{
    Guid TenantId { get; }
}