namespace Ruway.Net.Core.Abstractions.Types;

public interface IStartupInitializer : IInitializer
{
    void AddInitializer(IInitializer initializer);
}

