using Ruway.Net.Core.Abstractions.DomainContracts;

namespace Ruway.Net.Core.Abstractions.Domains;

public abstract class Entity<TKey> : IEntity<TKey>
{
    public TKey Id { get; private set; } = default!;

    protected Entity()
    {
    }

    protected Entity(TKey id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TKey> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        if (EqualityComparer<TKey>.Default.Equals(Id, default))
            return false;

        return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), Id);
    }

    public static bool operator ==(
        Entity<TKey>? left,
        Entity<TKey>? right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(
        Entity<TKey>? left,
        Entity<TKey>? right)
    {
        return !(left == right);
    }
}