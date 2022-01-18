using Reflection.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reflection
{
    public class Provider : IProviders
    {

        public IConfigurationProvider GetProvider(ProviderType providerType)
        {
            var providers = LoadProviders(@"..\..\..\Plugins\Providers.dll");
            var provider = providers.FirstOrDefault(x => x.ProviderType == providerType);
            if (providerType == ProviderType.ConfigurationManager)
            {
                provider.FilePath = @"..\..\..\app.config";
            }
            if (providerType == ProviderType.File)
            {
                provider.FilePath = @"..\..\..\file.txt";
            }
            return provider;
        }

        public IEnumerable<IConfigurationProvider> LoadProviders(string pluginPath)
        {
            var assembly = Assembly.LoadFrom(pluginPath);
            var types = assembly.GetTypes()
                .Where(x => typeof(IConfigurationProvider).IsAssignableFrom(x));
            var providers = new List<IConfigurationProvider>();
            foreach (var type in types)
            {
                var provider = Activator.CreateInstance(type) as IConfigurationProvider;
                providers.Add(provider);
            }
            return providers;
        }
    }
}
