using ConfigurationProvider;
using ConfigurationProvider.Enum;

namespace Reflection
{
    public interface IProviders
    {
        IConfigurationProvider GetProvider(ProviderType providerType);
    }
}
