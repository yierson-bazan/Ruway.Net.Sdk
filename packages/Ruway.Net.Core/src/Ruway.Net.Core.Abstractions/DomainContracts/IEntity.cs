namespace Ruway.Net.Core.Abstractions.DomainContracts;

public interface IEntity<TKey>
{
    TKey Id { get; }
}

