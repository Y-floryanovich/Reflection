using Reflection.Enum;

namespace Reflection
{
    public interface IProviders
    {
        IConfigurationProvider GetProvider(ProviderType providerType);
    }
}
